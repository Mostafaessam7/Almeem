using API.Controllers;
using API.Extensions;
using API.SignalR;
using Core.Entities;
using Core.Entities.OrderAggregate;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Stripe;
using Stripe.V2;
using System.IO;
using System.Threading.Tasks;

public class PaymentsController : BaseApiController
{
  private readonly IPaymentService _paymentService;
  private readonly IUnitOfWork _unit;
  private readonly ILogger<PaymentsController> _logger;
  private readonly IHubContext<NotificationHub> _hubContext;
  private readonly string _whSecret;

  public PaymentsController(
      IPaymentService paymentService,
      IUnitOfWork unit,
      ILogger<PaymentsController> logger,
      IConfiguration config,
      IHubContext<NotificationHub> hubContext)
  {
    _paymentService = paymentService;
    _unit = unit;
    _logger = logger;
    _hubContext = hubContext;
    _whSecret = config["StripeSettings:WhSecret"] ??
        throw new ArgumentNullException(nameof(config), "Stripe webhook secret is required");
  }

  [Authorize]
  [HttpPost("{cartId}")]
  public async Task<ActionResult<ShoppingCart>> CreateOrUpdatePaymentIntent(string cartId)
  {
    try
    {
      _logger.LogInformation($"Creating/Updating payment intent for cart: {cartId}");
      var cart = await _paymentService.CreateOrUpdatePaymentIntent(cartId);

      if (cart == null)
      {
        _logger.LogWarning($"Cart not found: {cartId}");
        return BadRequest("Problem with your cart");
      }

      return Ok(cart);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error processing payment intent for cart: {cartId}");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error processing payment");
    }
  }

  [HttpGet("delivery-methods")]
  public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethods()
  {
    try
    {
      var methods = await _unit.Repository<DeliveryMethod>().ListAllAsync();
      return Ok(methods);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Error retrieving delivery methods");
      return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving delivery methods");
    }
  }

  [HttpPost("webhook")]
  public async Task<IActionResult> StripeWebhook()
  {
    _logger.LogInformation("Webhook received");

    try
    {
      // Enable request body buffering
      Request.EnableBuffering();

      var json = await new StreamReader(Request.Body).ReadToEndAsync();
      Request.Body.Position = 0; // Reset the position for potential future middleware

      var stripeEvent = ConstructStripeEvent(json);
      _logger.LogInformation($"Webhook event type: {stripeEvent.Type}");

      // Compare event type strings directly
      if (stripeEvent.Type == "payment_intent.succeeded")
      {
        if (stripeEvent.Data.Object is Stripe.PaymentIntent intent)
        {
          await HandlePaymentIntentSucceeded(intent);
        }
        else
        {
          _logger.LogError("Received payment_intent.succeeded but object is not of type PaymentIntent.");
        }
      }
      else if (stripeEvent.Type == "payment_intent.payment_failed")
      {
        if (stripeEvent.Data.Object is Stripe.PaymentIntent intent)
        {
          await HandlePaymentIntentFailed(intent);
        }
        else
        {
          _logger.LogError("Received payment_intent.payment_failed but object is not of type PaymentIntent.");
        }
      }
      else
      {
        _logger.LogInformation($"Unhandled event type: {stripeEvent.Type}");
      }

      return Ok();
    }
    catch (StripeException ex)
    {
      _logger.LogError(ex, "Stripe webhook error");
      return BadRequest("Webhook error: " + ex.Message);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "An unexpected error occurred");
      return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred");
    }
  }

  private async Task HandlePaymentIntentFailed(Stripe.PaymentIntent intent)
  {
    _logger.LogInformation($"Processing failed payment intent: {intent.Id}");
    // Handle failure scenario (e.g., update order status, notify user)
  }


  private async Task HandlePaymentIntentSucceeded(PaymentIntent intent)
  {
    _logger.LogInformation($"Processing succeeded payment intent: {intent.Id}");

    try
    {
      var spec = new OrderSpecification(intent.Id, true);
      var order = await _unit.Repository<Order>().GetEntityWithSpec(spec);

      if (order == null)
      {
        _logger.LogError($"Order not found for PaymentIntent: {intent.Id}");
        return;
      }

      var orderAmount = (long)(order.GetTotal() * 100);
      if (orderAmount != intent.Amount)
      {
        _logger.LogWarning($"Payment amount mismatch. Order: {orderAmount}, Intent: {intent.Amount}");
        order.Status = OrderStatus.PaymentMismatch;
      }
      else
      {
        _logger.LogInformation($"Payment verified for order {order.Id}");
        order.Status = OrderStatus.PaymentReceived;
      }

      await _unit.Complete();
      await SendOrderNotification(order);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error processing payment for intent {intent.Id}");
    }
  }

  private async Task SendOrderNotification(Order order)
  {
    try
    {
      var connectionIds = NotificationHub.GetConnectionIdsByEmail(order.BuyerEmail);
      if (connectionIds != null && connectionIds.Any())
      {
        foreach (var connectionId in connectionIds)
        {
          await _hubContext.Clients.Client(connectionId)
              .SendAsync("OrderCompleteNotification", order.ToDto());
        }
      }
      else
      {
        _logger.LogInformation($"No active connections found for {order.BuyerEmail}");
      }
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, $"Error sending notification for order {order.Id}");
    }
  }

  private Stripe.Event ConstructStripeEvent(string json)
  {
    try
    {
      var signature = Request.Headers["Stripe-Signature"];
      return EventUtility.ConstructEvent(json, signature, _whSecret, throwOnApiVersionMismatch: false);
    }
    catch (Exception ex)
    {
      _logger.LogError(ex, "Failed to construct Stripe event");
      throw new StripeException("Invalid webhook signature");
    }
  }
}
