import { Component } from '@angular/core';
import { CartService } from '../../../shared/services/cart.service';
import { CurrencyPipe } from '@angular/common';
import { NgFor, NgIf } from '@angular/common';
import { CheckoutService } from '../../../shared/services/checkout.service';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: 'app-cart',
  imports: [CurrencyPipe, NgFor, NgIf],
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {


  quote: any = null;
  customerId = localStorage.getItem('customerId');

  constructor(public cartService: CartService, private checkoutService: CheckoutService, private toastr: ToastrService) {}

  getCart() {
    return this.cartService.getCart();
  }

  fetchQuote() {
    const items = this.getCart().map(i => ({ productId: i.product.id, quantity: i.quantity }));
    this.checkoutService.getQuote(this.customerId, items).subscribe({
      next: (res) => this.quote = res,
      error: (err) => console.error(err)
    });
  }

  placeOrder() {
    const items = this.getCart().map(i => ({ productId: i.product.id, quantity: i.quantity }));
    this.checkoutService.placeOrder(this.customerId, items).subscribe({
      next: (res: any) => {
        this.toastr.success(`Order has been place`, 'Cart Updated' , {
        timeOut: 3000,
        closeButton: true,
        positionClass: 'toast-bottom-right'
      });
        this.cartService.clearCart();
        this.quote = null;
      },
      error: (err) => console.error(err)
    });
  }
}
