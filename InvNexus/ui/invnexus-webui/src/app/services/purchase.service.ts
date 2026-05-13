import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';

export interface PurchaseOrderItemRequest {
  productId: string;
  quantity: number;
  unitPrice: number;
}

export interface CreatePurchaseOrderRequest {
  items: PurchaseOrderItemRequest[];
}

export interface PurchaseOrderActionResponse {
  id: string;
  purchaseNumber: string;
  status: string;
}

export interface PurchaseOrderListItemResponse {
  id: string;
  purchaseNumber: string;
  status: string;
  createdAt: string;
}

@Injectable({ providedIn: 'root' })
export class PurchaseService {
  private readonly baseUrl = environment.purchaseServiceUrl;

  constructor(private readonly http: HttpClient) {}

  createOrder(payload: CreatePurchaseOrderRequest): Observable<PurchaseOrderActionResponse> {
    return this.http.post<PurchaseOrderActionResponse>(`${this.baseUrl}/api/purchases`, payload);
  }

  getOrders(): Observable<PurchaseOrderListItemResponse[]> {
    return this.http.get<PurchaseOrderListItemResponse[]>(`${this.baseUrl}/api/purchases`);
  }

  receiveOrder(orderId: string): Observable<PurchaseOrderActionResponse> {
    return this.http.post<PurchaseOrderActionResponse>(`${this.baseUrl}/api/purchases/${orderId}/receive`, {});
  }
}
