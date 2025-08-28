import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';


interface QuoteRequestItem {
  productId: number;
  quantity: number;
}

const httpOptions = {
  headers: new HttpHeaders({
    'Content-Type': "application/json",
    'Access-Control-Allow-Origin': "*"
  })
}

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  private readonly baseUrl: string = "https://localhost:44325/api/Orders";

  constructor(private http: HttpClient) { }

  getQuote(customerId: string | null, items: QuoteRequestItem[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/getQuote`, { customerId, items }, httpOptions);
  }

  // Place actual order
  placeOrder(customerId: string | null, items: QuoteRequestItem[]): Observable<any> {
    return this.http.post(`${this.baseUrl}/createOrder`, { customerId, items }, httpOptions);
  }
}
