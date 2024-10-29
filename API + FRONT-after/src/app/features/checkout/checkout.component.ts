import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { OrderSummaryComponent } from '../../shared/components/order-summary/order-summary.component';
import { MatStepper, MatStepperModule } from '@angular/material/stepper';
import { MatButton } from '@angular/material/button';
import { Router, RouterLink } from '@angular/router';
import { StripeService } from '../../core/services/stripe.service';
import {
  ConfirmationToken,
  StripeAddressElement,
  StripeAddressElementChangeEvent,
  StripePaymentElement,
  StripePaymentElementChangeEvent,
} from '@stripe/stripe-js';
import { SnackbarService } from '../../core/services/snackbar.service';
import {
  MatCheckboxChange,
  MatCheckboxModule,
} from '@angular/material/checkbox';
import { StepperSelectionEvent } from '@angular/cdk/stepper';
import { Address } from '../../shared/models/user';
import { AccountService } from '../../core/services/account.service';
import { firstValueFrom } from 'rxjs';
import { CheckoutDeliveryComponent } from './checkout-delivery/checkout-delivery.component';
import { CheckoutReviewComponent } from './checkout-review/checkout-review.component';
import { CartService } from '../../core/services/cart.service';
import { CurrencyPipe, JsonPipe } from '@angular/common';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { OrderService } from '../../core/services/order.service';
import { OrderToCreate, ShippingAddress } from '../../shared/models/order';
import { SignalrService } from '../../core/services/signalr.service';

@Component({
  selector: 'app-checkout',
  standalone: true,
  imports: [
    OrderSummaryComponent,
    MatStepperModule,
    MatButton,
    RouterLink,
    MatCheckboxModule,
    CheckoutDeliveryComponent,
    CheckoutReviewComponent,
    CurrencyPipe,
    JsonPipe,
    MatProgressSpinnerModule,
  ],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.scss',
})
export class CheckoutComponent implements OnInit, OnDestroy {
//Added 
private signalrService = inject(SignalrService);

  private stripeService = inject(StripeService);
  private snackbar = inject(SnackbarService);
  private router = inject(Router);
  private accountService = inject(AccountService);
  private orderService = inject(OrderService);
  cartService = inject(CartService);
  addressElement?: StripeAddressElement;
  paymentElement?: StripePaymentElement;
  saveAddress = false;
  completionStatus = signal<{address: boolean, card: boolean, delivery: boolean}>(
    {address: false, card: false, delivery: false}
  );
  confirmationToken?: ConfirmationToken;
  loading = false;

  // NEW  imp for clearing cart
  private async clearCartAndNavigate(orderId: number) {
    try {
      // Clear cart data
      await this.cartService.deleteCart().toPromise();
      
      // Clear delivery method
      this.cartService.selectedDelivery.set(null);
      
      // Set order as complete
      this.orderService.orderComplete = true;
      
      // Navigate to order details
      await this.router.navigateByUrl(`/orders/${orderId}`);
    } catch (error) {
      console.error('Error clearing cart:', error);
      // Even if cart clearing fails, still navigate to order
      await this.router.navigateByUrl(`/orders/${orderId}`);
    }
  }

  ////// new added Confirm
  async confirmPayment(stepper: MatStepper) {
    this.loading = true;
    try {
      if (this.confirmationToken) {
        const result = await this.stripeService.confirmPayment(this.confirmationToken);

        if (result.paymentIntent?.status === 'succeeded') {
          const order = await this.createOrderModel();
          const orderResult = await firstValueFrom(this.orderService.createOrder(order));
          
          if (orderResult) {
            // Start waiting for SignalR notification
            this.signalrService.createHubConnection(orderResult.id);
            
            try {
              // Wait for real-time order confirmation
              const confirmedOrder = await this.signalrService.waitForOrder();
              await this.clearCartAndNavigate(confirmedOrder.id);
            } catch (error) {
              // If SignalR timeout occurs, still clear cart and navigate
              console.warn('SignalR timeout, falling back to regular navigation');
              await this.clearCartAndNavigate(orderResult.id);
            }
          } else {
            throw new Error('Order creation failed');
          }
        } else if (result.error) {
          throw new Error(result.error.message);
        } else {
          throw new Error('Something went wrong with payment confirmation');
        }
      }
    } catch (error: any) {
      this.snackbar.error(error.message || 'Something went wrong');
      stepper.previous();
    } finally {
      this.loading = false;
      this.signalrService.stopHubConnection();
    }
  }

  async ngOnInit() {
    try {
      this.addressElement = await this.stripeService.createAddressElement();
      this.addressElement.mount('#address-element');
      this.addressElement.on('change', this.handleAddressChange)

      this.paymentElement = await this.stripeService.createPaymentElement();
      this.paymentElement.mount('#payment-element');
      this.paymentElement.on('change', this.handlePaymentChange);
    } catch (error: any) {
      this.snackbar.error(error.message);
    }
  }

  handleAddressChange = (event: StripeAddressElementChangeEvent) => {
    this.completionStatus.update(state => {
      state.address = event.complete;
      return state;
    })
  }

  handlePaymentChange = (event: StripePaymentElementChangeEvent) => {
    this.completionStatus.update(state => {
      state.card = event.complete;
      return state;
    })
  }

  handleDeliveryChange(event: boolean) {
    this.completionStatus.update(state => {
      state.delivery = event;
      return state;
    })
  }

  async getConfirmationToken() {
    try {
      if (Object.values(this.completionStatus()).every(status => status === true)) {
        const result = await this.stripeService.createConfirmationToken();
        if (result.error) throw new Error(result.error.message);
        this.confirmationToken = result.confirmationToken;
        console.log(this.confirmationToken);
      }
    } catch (error: any) {
      this.snackbar.error(error.message);
    }

  }

  async onStepChange(event: StepperSelectionEvent) {
    if (event.selectedIndex === 1) {
      if (this.saveAddress) {
        const address = await this.getAddressFromStripeAddress() as Address;
        address && firstValueFrom(this.accountService.updateAddress(address));
      }
    }
    if (event.selectedIndex === 2) {
      await firstValueFrom(this.stripeService.createOrUpdatePaymentIntent());
    }
    if (event.selectedIndex === 3) {
      await this.getConfirmationToken();
    }
  }



  // async confirmPayment(stepper: MatStepper) {
  //   this.loading = true;
  //   try {
  //     if (this.confirmationToken) {
  //       const result = await this.stripeService.confirmPayment(this.confirmationToken);

  //       if (result.paymentIntent?.status === 'succeeded') {
  //         const order = await this.createOrderModel();
  //         const orderResult = await firstValueFrom(this.orderService.createOrder(order));
          
  //         if (orderResult) {
  //           // Start waiting for SignalR notification
  //           this.signalrService.createHubConnection(orderResult.id);
  //           try {
  //             const confirmedOrder = await this.signalrService.waitForOrder();
  //             this.orderService.orderComplete = true;
  //             this.cartService.deleteCart();
  //             this.cartService.selectedDelivery.set(null);
  //             this.router.navigateByUrl(`/orders/${confirmedOrder.id}`);
  //           } catch (error) {
  //             // If timeout occurs, fall back to regular order loading
  //             this.router.navigateByUrl(`/orders/${orderResult.id}`);
  //           }
  //         } else {
  //           throw new Error('Order creation failed');
  //         }
  //       } else if (result.error) {
  //         throw new Error(result.error.message);
  //       } else {
  //         throw new Error('Something went wrong');
  //       }
  //     }
  //   } catch (error: any) {
  //     this.snackbar.error(error.message || 'Something went wrong');
  //     stepper.previous();
  //   } finally {
  //     this.loading = false;
  //     this.signalrService.stopHubConnection();
  //   }
  // }



  private async createOrderModel(): Promise<OrderToCreate> {
    const cart = this.cartService.cart();
    const shippingAddress = await this.getAddressFromStripeAddress() as ShippingAddress;
    const card = this.confirmationToken?.payment_method_preview.card;

    if (!cart?.id || !cart.deliveryMethodId || !card || !shippingAddress) {
      throw new Error('Problem creating order');
    }

    return {
      cartId: cart.id,
      paymentSummary: {
        last4: +card.last4,
        brand: card.brand,
        expMonth: card.exp_month,
        expYear: card.exp_year
      },
      deliveryMethodId: cart.deliveryMethodId,
      shippingAddress
    }
  }

  private async getAddressFromStripeAddress(): Promise<Address | ShippingAddress | null> {
    const result = await this.addressElement?.getValue();
    const address = result?.value.address;

    if (address) {
      return {
        name: result.value.name,
        line1: address.line1,
        line2: address.line2 || undefined,
        city: address.city,
        country: address.country,
        state: address.state,
        postalCode: address.postal_code
      }
    } else return null;
  }

  onSaveAddressCheckboxChange(event: MatCheckboxChange) {
    this.saveAddress = event.checked;
  }

  ngOnDestroy(): void {
    this.stripeService.disposeElements();
    this.signalrService.stopHubConnection();
  }
}

