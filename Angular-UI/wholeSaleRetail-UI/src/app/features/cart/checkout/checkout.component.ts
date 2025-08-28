import { Component } from '@angular/core';
import { CartService } from '../../../shared/services/cart.service';
import { CheckoutService } from '../../../shared/services/checkout.service';
import { NgFor, NgIf } from '@angular/common';
import { CurrencyPipe } from '@angular/common';

@Component({
  selector: 'app-checkout',
  imports: [NgFor, NgIf, CurrencyPipe],
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {

  constructor(private cartService: CartService, private checkoutService: CheckoutService) {}

  ngOnInit() {
    this.cartItems = this.cartService.getCart();
  }

  quote: any = null;
  customerId = localStorage.getItem('customerId');
  cartItems: any;


  getQuote() {
    const items = this.cartItems.map((i: { product: { id: any; }; quantity: any; }) => ({ productId: i.product.id, quantity: i.quantity }));

    this.checkoutService.getQuote(this.customerId, items).subscribe({
      next: (res) => this.quote = res,
      error: (err) => console.error(err)
    });
  }

  placeOrder() {
    const items = this.cartItems.map((i: { product: { id: any; }; quantity: any; }) => ({ productId: i.product.id, quantity: i.quantity }));

    this.checkoutService.placeOrder(this.customerId, items).subscribe({
      next: (res: any) => {
        alert(`Order placed successfully! Order #${res.id}`);
        this.cartService.clearCart();
        this.quote = null;
      },
      error: (err) => console.error(err)
    });
  }
  

}
