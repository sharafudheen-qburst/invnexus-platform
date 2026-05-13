import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface SalesOrderItemRequest {
  productId: string;
  quantity: number;
  unitPrice: number;
}

export interface CreateSalesOrderRequest {
  items: SalesOrderItemRequest[];
}

export interface SalesOrderActionResponse {
  id: string;
  salesNumber: string;
  status: string;
}

export interface SalesOrderListItemResponse {
  id: string;
  salesNumber: string;
  status: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class SalesService {
  private readonly baseUrl = environment.salesServiceUrl;

  constructor(private readonly http: HttpClient) {}

  createOrder(payload: CreateSalesOrderRequest): Observable<SalesOrderActionResponse> {
    return this.http.post<SalesOrderActionResponse>(`${this.baseUrl}/api/sales`, payload);
  }

  getOrders(): Observable<SalesOrderListItemResponse[]> {
    return this.http.get<SalesOrderListItemResponse[]>(`${this.baseUrl}/api/sales`);
  }

  completeOrder(orderId: string): Observable<SalesOrderActionResponse> {
    return this.http.post<SalesOrderActionResponse>(`${this.baseUrl}/api/sales/${orderId}/complete`, {});
  }
}
