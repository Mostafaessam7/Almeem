using Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities.OrderAggregate
{
  public class Order : BaseEntity , IDtoConvertible
  {
    public DateTime OrderDate { get; set; } = DateTime.UtcNow;
    public required string BuyerEmail { get; set; }
    public ShippingAddress ShippingAddress { get; set; } = null!;
    public DeliveryMethod DeliveryMethod { get; set; } = null!;
    public PaymentSummary PaymentSummary { get; set; } = null!;
    public List<OrderItem> OrderItems { get; set; } = [];
    public decimal Subtotal { get; set; }
    public OrderStatus Status { get; set; } = OrderStatus.Pending;
    public required string PaymentIntentId { get; set; }

    //public decimal GetTotal()
    //{
    //  return Subtotal + DeliveryMethod.Price;
    //}
    public decimal GetTotal()
    {
      // Safeguard against null DeliveryMethod
      decimal deliveryPrice = DeliveryMethod?.Price ?? 0m; // If DeliveryMethod is null, use 0

      return Subtotal + deliveryPrice;
    }

  }
}
