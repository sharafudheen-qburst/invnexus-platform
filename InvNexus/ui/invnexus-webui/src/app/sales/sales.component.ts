import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { SalesOrderListItemResponse, SalesService } from '../services/sales.service';

@Component({
  selector: 'app-sales',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section class="grid" style="gap:1.5rem;">
      <div class="card">
        <h2 style="margin-top:0;">Create Sales Order</h2>

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
            {{ isSubmitting ? 'Saving...' : 'Create Sales Order' }}
          </button>
        </form>
      </div>

      <div class="card">
        <div style="display:flex;justify-content:space-between;align-items:center;gap:0.75rem;">
          <h2 style="margin:0;">Sales Orders</h2>
          <button class="button button-secondary" type="button" (click)="loadOrders()">Refresh</button>
        </div>

        <table class="table" *ngIf="orders.length > 0; else emptyState">
          <thead>
            <tr>
              <th>Sales No</th>
              <th>Status</th>
              <th>Created At</th>
              <th>Actions</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let order of orders">
              <td>{{ order.salesNumber }}</td>
              <td>{{ order.status }}</td>
              <td>{{ order.createdAt | date: 'medium' }}</td>
              <td>
                <button
                  class="button button-secondary"
                  type="button"
                  (click)="completeOrder(order.id)"
                  [disabled]="isActionRunning">
                  Complete
                </button>
              </td>
            </tr>
          </tbody>
        </table>

        <ng-template #emptyState>
          <p style="margin-bottom:0;">No sales orders found.</p>
        </ng-template>
      </div>
    </section>
  `
})
export class SalesComponent implements OnInit {
  readonly form = this.formBuilder.group({
    productId: ['', [Validators.required]],
    quantity: [1, [Validators.required, Validators.min(1)]],
    unitPrice: [0, [Validators.required, Validators.min(0)]]
  });

  orders: SalesOrderListItemResponse[] = [];
  isSubmitting = false;
  isActionRunning = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly salesService: SalesService
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

    this.salesService
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
          this.errorMessage = 'Failed to create sales order.';
        }
      });
  }

  loadOrders(): void {
    this.salesService.getOrders().subscribe({
      next: (orders) => {
        this.orders = orders;
      },
      error: () => {
        this.errorMessage = 'Failed to load sales orders.';
      }
    });
  }

  completeOrder(orderId: string): void {
    if (this.isActionRunning) {
      return;
    }

    this.isActionRunning = true;
    this.errorMessage = '';

    this.salesService.completeOrder(orderId).subscribe({
      next: () => {
        this.isActionRunning = false;
        this.loadOrders();
      },
      error: () => {
        this.isActionRunning = false;
        this.errorMessage = 'Failed to complete sales order.';
      }
    });
  }
}
