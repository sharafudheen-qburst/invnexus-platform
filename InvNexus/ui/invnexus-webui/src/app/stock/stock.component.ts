import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryService, StockResponse } from '../services/inventory.service';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section class="card" style="max-width:720px;">
      <h2 style="margin-top:0;">Stock</h2>

      <form [formGroup]="form" (ngSubmit)="search()" class="grid">
        <label>
          <div>Product Id</div>
          <input class="input" type="text" formControlName="productId" placeholder="guid" />
        </label>

        <div class="error" *ngIf="errorMessage">{{ errorMessage }}</div>

        <button class="button button-primary" type="submit" [disabled]="form.invalid || isLoading">
          {{ isLoading ? 'Loading...' : 'Get Stock' }}
        </button>
      </form>

      <div *ngIf="stock" style="margin-top:1rem;padding-top:1rem;border-top:1px solid #e2e8f0;">
        <div><strong>Product Id:</strong> {{ stock.productId }}</div>
        <div><strong>Quantity:</strong> {{ stock.quantity }}</div>
      </div>
    </section>
  `
})
export class StockComponent {
  readonly form = this.formBuilder.group({
    productId: ['', [Validators.required]]
  });

  stock: StockResponse | null = null;
  isLoading = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly inventoryService: InventoryService
  ) {}

  search(): void {
    if (this.form.invalid || this.isLoading) {
      return;
    }

    this.isLoading = true;
    this.errorMessage = '';
    this.stock = null;

    const productId = this.form.controls.productId.value?.trim() ?? '';

    this.inventoryService.getStockByProductId(productId).subscribe({
      next: (stock) => {
        this.isLoading = false;
        this.stock = stock;
      },
      error: () => {
        this.isLoading = false;
        this.errorMessage = 'Stock not found for the given product id.';
      }
    });
  }
}
