import { inject, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment.development';
import { Order } from '../../shared/models/order';
import {HubConnection, HubConnectionBuilder, HubConnectionState} from '@microsoft/signalr';
import { CartService } from './cart.service';

@Injectable({
  providedIn: 'root'
})
export class SignalrService {
  hubUrl = environment.hubUrl;
  hubConnection?: HubConnection;
  orderSignal = signal<Order | null>(null);
  private orderReadyPromise?: Promise<Order>;
  private orderReadyResolve?: (order: Order) => void;

  createHubConnection(orderId?: number) {
    if (orderId) {
      this.orderReadyPromise = new Promise((resolve) => {
        this.orderReadyResolve = resolve;
      });
    }

    this.hubConnection = new HubConnectionBuilder()
      .withUrl(this.hubUrl, {
        withCredentials: true
      })
      .withAutomaticReconnect()
      .build();

    this.hubConnection.start()
      .catch(error => console.log(error));

    this.hubConnection.on('OrderCompleteNotification', (order: Order) => {
      this.orderSignal.set(order);
      if (this.orderReadyResolve && order.id === orderId) {
        this.orderReadyResolve(order);
      }
    });
  }

  async waitForOrder(timeoutMs = 3000): Promise<Order> {
    if (!this.orderReadyPromise) {
      throw new Error('No order being waited for');
    }

    const timeoutPromise = new Promise<Order>((_, reject) => {
      setTimeout(() => reject(new Error('Order notification timeout')), timeoutMs);
    });

    return Promise.race([this.orderReadyPromise, timeoutPromise]);
  }

  stopHubConnection() {
    if (this.hubConnection?.state === HubConnectionState.Connected) {
      this.hubConnection.stop().catch(error => console.log(error));
      this.orderReadyPromise = undefined;
      this.orderReadyResolve = undefined;
    }
  }
 
}