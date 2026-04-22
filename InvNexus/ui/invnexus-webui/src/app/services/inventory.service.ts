import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface Product {
  id?: string;
  name: string;
  price: number;
  isActive: boolean;
}

export interface Stock {
  productId: string;
  productName: string;
  quantity: number;
}

@Injectable({
  providedIn: 'root'
})
export class InventoryService {
  private apiUrl = `${environment.apiBaseUrl}/inventory`;

  constructor(private http: HttpClient) {}

  // Product endpoints
  getProducts(): Observable<Product[]> {
    return this.http.get<Product[]>(`${this.apiUrl}/products`);
  }

  getProduct(id: string): Observable<Product> {
    return this.http.get<Product>(`${this.apiUrl}/products/${id}`);
  }

  createProduct(product: Product): Observable<Product> {
    return this.http.post<Product>(`${this.apiUrl}/products`, product);
  }

  updateProduct(id: string, product: Product): Observable<Product> {
    return this.http.put<Product>(`${this.apiUrl}/products/${id}`, product);
  }

  deleteProduct(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/products/${id}`);
  }

  // Stock endpoints
  getStock(): Observable<Stock[]> {
    return this.http.get<Stock[]>(`${this.apiUrl}/stock`);
  }

  getStockByProduct(productId: string): Observable<Stock> {
    return this.http.get<Stock>(`${this.apiUrl}/stock/${productId}`);
  }

  updateStock(productId: string, quantity: number): Observable<Stock> {
    return this.http.put<Stock>(`${this.apiUrl}/stock/${productId}`, { quantity });
  }
}
