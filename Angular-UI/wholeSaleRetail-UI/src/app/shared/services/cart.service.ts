import { Injectable } from '@angular/core';
import { Product } from '../models/product';
import { ToastrService } from 'ngx-toastr';


/**
 * Service to manage shopping cart operations.
 */
@Injectable({
  providedIn: 'root'
})
export class CartService {

  constructor(private toastr: ToastrService ) { }

  private items: { product: Product, quantity: number, price: number }[] = [];


  /**
   * Returns all items in the cart.
   */
  getCart() {
    return this.items;
  }

    /**
   * Adds a product to the cart. If the product already exists, increment quantity.
   * @param item Product and its calculated price to add
   */
  addToCart(item: { product: Product; price: number }) {
    const existing = this.items.find(i => i.product.name === item.product.name);
    if (existing) {
      existing.quantity++; // Increment quantity if product exists
    } else {
      this.items.push({ product: item.product, quantity: 1, price: item.price });
    }
  }


    /**
   * Removes a product from the cart by ID.
   * @param productId The ID of the product to remove
   */
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
