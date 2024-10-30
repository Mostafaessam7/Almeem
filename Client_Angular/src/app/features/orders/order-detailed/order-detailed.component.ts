import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { OrderService } from '../../../core/services/order.service';
import { ActivatedRoute, Router, RouterLink } from '@angular/router';
import { Order } from '../../../shared/models/order';
import { MatCardModule } from '@angular/material/card';
import { DatePipe, CurrencyPipe } from '@angular/common';
import { MatButton } from '@angular/material/button';
import { AddressPipe } from '../../../shared/pipes/address.pipe';
import { PaymentCardPipe } from '../../../shared/pipes/payment-card.pipe';
import { SignalrService } from '../../../core/services/signalr.service';
import { AccountService } from '../../../core/services/account.service';
import { AdminService } from '../../../core/services/admin.service';

@Component({
  selector: 'app-order-detailed',
  standalone: true,
  imports: [
    MatCardModule,
    MatButton,
    DatePipe,
    CurrencyPipe,
    AddressPipe,
    PaymentCardPipe,
    RouterLink
  ],
  templateUrl: './order-detailed.component.html',
  styleUrl: './order-detailed.component.scss'
})
export class OrderDetailedComponent implements OnInit , OnDestroy {
  // private orderService = inject(OrderService);
  // private activatedRoute = inject(ActivatedRoute);
  // order?: Order;

  // ngOnInit(): void {
  //   this.loadOrder();
  // }

  // loadOrder() {
  //   const id = this.activatedRoute.snapshot.paramMap.get('id');
  //   if (!id) return;
  //   this.orderService.getOrderDetailed(+id).subscribe({
  //     next: order => this.order = order
  //   })
  // }
  private orderService = inject(OrderService);
  private signalrService = inject(SignalrService);
  private activatedRoute = inject(ActivatedRoute);
  order = signal<Order | undefined>(undefined);
  private accountService = inject(AccountService);
  private adminService = inject(AdminService);
  private router = inject(Router);
   buttonText = this.accountService.isAdmin() ? 'Return to admin' : 'Return to orders'

  ngOnInit(): void {
    const id = this.activatedRoute.snapshot.paramMap.get('id');
    if (!id) return;

    // First check if we have the order from SignalR
    const signalrOrder = this.signalrService.orderSignal();
    if (signalrOrder && signalrOrder.id === +id) {
      this.order.set(signalrOrder);
    } else {
      // If not, load it normally
      this.loadOrder(+id);
    }
  }

  // private loadOrder(id: number) {
  //   this.orderService.getOrderDetailed(id).subscribe({
  //     next: order => this.order.set(order)
  //   });
  // }

  private loadOrder(id: number) {
    const loadOrderData = this.accountService.isAdmin()
      ? this.adminService.getOrder(id)
      : this.orderService.getOrderDetailed(id);
  
    loadOrderData.subscribe({
      next: order => this.order.set(order)
    });
  }

  ngOnDestroy() {
    this.signalrService.stopHubConnection();
  }

  onReturnClick() {
    this.accountService.isAdmin() 
      ? this.router.navigateByUrl('/admin')
      : this.router.navigateByUrl('/orders')
  }

}
