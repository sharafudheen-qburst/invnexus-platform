import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface ProductRequest {
  name: string;
  price: number;
  isActive: boolean;
}

export interface ProductResponse {
  id: string;
  name: string;
  price: number;
  isActive: boolean;
}

export interface StockResponse {
  productId: string;
  quantity: number;
}

@Injectable({ providedIn: 'root' })
export class InventoryService {
  private readonly baseUrl = environment.inventoryServiceUrl;

  constructor(private readonly http: HttpClient) {}

  createProduct(payload: ProductRequest): Observable<ProductResponse> {
    return this.http.post<ProductResponse>(`${this.baseUrl}/api/products`, payload);
  }

  getProducts(): Observable<ProductResponse[]> {
    return this.http.get<ProductResponse[]>(`${this.baseUrl}/api/products`);
  }

  getStockByProductId(productId: string): Observable<StockResponse> {
    return this.http.get<StockResponse>(`${this.baseUrl}/api/stock/${productId}`);
  }
}
