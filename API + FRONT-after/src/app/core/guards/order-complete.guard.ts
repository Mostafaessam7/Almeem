import { CanActivateFn, Router } from '@angular/router';
import { OrderService } from '../services/order.service';
import { inject } from '@angular/core';

export const orderCompleteGuard: CanActivateFn = (route, state) => {

  const orderService = inject(OrderService);
  const router = inject(Router);

  if (orderService.orderComplete) {
    return true;
  } else { 
    router.navigateByUrl('/shop');
    return false;
  }
};
