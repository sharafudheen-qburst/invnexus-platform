import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import {
  PurchaseOrderListItemResponse,
  PurchaseService
} from '../services/purchase.service';

@Component({
  selector: 'app-purchase',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section class="grid" style="gap:1.5rem;">
      <div class="card">
        <h2 style="margin-top:0;">Create Purchase Order</h2>

        <form [formGroup]="form" (ngSubmit)="createOrder()" class="grid">
          <label>
            <div>Product Id</div>
            <input class="input" type="text" formControlName="productId" placeholder="guid" />
          </label>

          <label>
            <div>Quantity</div>
            <input class="input" type="number" min="1" step="1" formControlName="quantity" />
          </label>

          <label>
            <div>Unit Price</div>
            <input class="input" type="number" min="0" step="0.01" formControlName="unitPrice" />
          </label>

          <div class="error" *ngIf="errorMessage">{{ errorMessage }}</div>

          <button class="button button-primary" type="submit" [disabled]="form.invalid || isSubmitting">
            {{ isSubmitting ? 'Saving...' : 'Create Purchase Order' }}
          </button>
        </form>
      </div>

      <div class="card">
        <div style="display:flex;justify-content:space-between;align-items:center;gap:0.75rem;">
          <h2 style="margin:0;">Purchase Orders</h2>
          <button class="button button-secondary" type="button" (click)="loadOrders()">Refresh</button>
        </div>

        <table class="table" *ngIf="orders.length > 0; else emptyState">
          <thead>
            <tr>
              <th>Purchase No</th>
              <th>Status</th>
              <th>Created At</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let order of orders">
              <td>{{ order.purchaseNumber }}</td>
              <td>{{ order.status }}</td>
              <td>{{ order.createdAt | date: 'medium' }}</td>
              <td>
                <button
                  class="button button-secondary"
                  type="button"
                  (click)="receiveOrder(order.id)"
                  [disabled]="isActionRunning">
                  Receive
                </button>
              </td>
            </tr>
          </tbody>
        </table>

        <ng-template #emptyState>
          <p style="margin-bottom:0;">No purchase orders found.</p>
        </ng-template>
      </div>
    </section>
  `
})
export class PurchaseComponent implements OnInit {
  readonly form = this.formBuilder.group({
    productId: ['', [Validators.required]],
    quantity: [1, [Validators.required, Validators.min(1)]],
    unitPrice: [0, [Validators.required, Validators.min(0)]]
  });

  orders: PurchaseOrderListItemResponse[] = [];
  isSubmitting = false;
  isActionRunning = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly purchaseService: PurchaseService
  ) {}

  ngOnInit(): void {
    this.loadOrders();
  }

  createOrder(): void {
    if (this.form.invalid || this.isSubmitting) {
      return;
    }

    this.errorMessage = '';
    this.isSubmitting = true;

    const productId = this.form.controls.productId.value?.trim() ?? '';
    const quantity = Number(this.form.controls.quantity.value ?? 1);
    const unitPrice = Number(this.form.controls.unitPrice.value ?? 0);

    this.purchaseService
      .createOrder({
        items: [{ productId, quantity, unitPrice }]
      })
      .subscribe({
        next: () => {
          this.isSubmitting = false;
          this.form.reset({ productId: '', quantity: 1, unitPrice: 0 });
          this.loadOrders();
        },
        error: () => {
          this.isSubmitting = false;
          this.errorMessage = 'Failed to create purchase order.';
        }
      });
  }

  loadOrders(): void {
    this.purchaseService.getOrders().subscribe({
      next: (orders) => {
        this.orders = orders;
      },
      error: () => {
        this.errorMessage = 'Failed to load purchase orders.';
      }
    });
  }

  receiveOrder(orderId: string): void {
    if (this.isActionRunning) {
      return;
    }

    this.isActionRunning = true;
    this.errorMessage = '';

    this.purchaseService.receiveOrder(orderId).subscribe({
      next: () => {
        this.isActionRunning = false;
        this.loadOrders();
      },
      error: () => {
        this.isActionRunning = false;
        this.errorMessage = 'Failed to receive purchase order.';
      }
    });
  }
}
