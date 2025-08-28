import { Component, OnInit } from '@angular/core';
import { CurrencyPipe } from '@angular/common';
import { NgFor } from '@angular/common';
import { Product } from '../../../shared/models/product';
import { ProductService } from '../../../shared/services/product.service';
import { CartService } from '../../../shared/services/cart.service';
import { ToastrService } from 'ngx-toastr';


/**
 * Component to display product catalog and allow adding products to cart.
 */
@Component({
  selector: 'app-product-list',
  imports: [CurrencyPipe, NgFor],
  templateUrl: './product-list.component.html',
  styleUrl: './product-list.component.css'
})
export class ProductListComponent implements OnInit  {

  products: Product[] = [];
  filtered: Product[] = [];

  constructor(private productService: ProductService, private cartService: CartService, private toastr: ToastrService ) {}

  /**
   * Fetch all products from backend on component initialization.
   */
  ngOnInit(): void {
    this.productService.getProducts().subscribe(data => {
      this.products = data;
      this.filtered = data;
    });
  }

   /**
   * Filters products by category.
   * @param category Category name or 'all'
   */
  applyFilter(category: string) {
    this.filtered = category === 'all' ? this.products : this.products.filter(p => p.category === category);
  }

  /**
   * Adds product to cart and shows toastr notification.
   * Determines price based on customer type.
   * @param product Product to add
   */
  addToCart(product: Product) {
    this.cartService.addToCart({ product, price: product.price });

    this.toastr.success(`${product.name} added to cart`, 'Cart Updated' , {
        timeOut: 3000,
        closeButton: true,
        positionClass: 'toast-bottom-right'
      });

  }
  
}
