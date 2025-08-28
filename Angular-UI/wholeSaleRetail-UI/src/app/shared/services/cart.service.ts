import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private toastr: ToastrService ) { }

  private items: { product: Product, quantity: number, price: number }[] = [];

  getCart() {
    return this.items;
  }

  // addToCart(product: Product) {
  //   const existing = this.items.find(i => i.product.id === product.id);
  //   if (existing) {
  //     existing.quantity++;
  //   } else {
  //     this.items.push({ product, quantity: 1 });
  //   }
  // }

  addToCart(item: { product: Product; price: number }) {
    const existing = this.items.find(i => i.product.name === item.product.name);
    if (existing) {
      existing.quantity++;
    } else {
      this.items.push({ product: item.product, quantity: 1, price: item.price });
    }
  }

  removeFromCart(productName: string) {
    this.items = this.items.filter(i => i.product.name !== productName && i.product.name !== productName);

    this.toastr.warning(`Item removed to cart`, 'Cart Updated' , {
        timeOut: 3000,
        closeButton: true,
        positionClass: 'toast-bottom-right'
      })
  }

  clearCart() {
    this.items = [];
  }
}
