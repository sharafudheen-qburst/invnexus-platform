import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryService, ProductResponse } from '../services/inventory.service';

@Component({
  selector: 'app-products',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section class="grid" style="gap:1.5rem;">
      <div class="card">
        <h2 style="margin-top:0;">Create Product</h2>

        <form [formGroup]="form" (ngSubmit)="createProduct()" class="grid">
          <label>
            <div>Name</div>
            <input class="input" type="text" formControlName="name" />
          </label>

          <label>
            <div>Price</div>
            <input class="input" type="number" min="0" step="0.01" formControlName="price" />
          </label>

          <label style="display:flex;align-items:center;gap:0.5rem;">
            <input type="checkbox" formControlName="isActive" />
            <span>Is Active</span>
          </label>

          <div class="error" *ngIf="errorMessage">{{ errorMessage }}</div>

          <button class="button button-primary" type="submit" [disabled]="form.invalid || isSubmitting">
            {{ isSubmitting ? 'Saving...' : 'Create' }}
          </button>
        </form>
      </div>

      <div class="card">
        <div style="display:flex;justify-content:space-between;align-items:center;gap:0.75rem;">
          <h2 style="margin:0;">Products</h2>
          <button class="button button-secondary" type="button" (click)="loadProducts()">Refresh</button>
        </div>

        <table class="table" *ngIf="products.length > 0; else emptyState">
          <thead>
            <tr>
              <th>Name</th>
              <th>Price</th>
              <th>Active</th>
              <th>Id</th>
            </tr>
          </thead>
          <tbody>
            <tr *ngFor="let product of products">
              <td>{{ product.name }}</td>
              <td>{{ product.price }}</td>
              <td>{{ product.isActive ? 'Yes' : 'No' }}</td>
              <td>{{ product.id }}</td>
            </tr>
          </tbody>
        </table>

        <ng-template #emptyState>
          <p style="margin-bottom:0;">No products found.</p>
        </ng-template>
      </div>
    </section>
  `
})
export class ProductsComponent implements OnInit {
  readonly form = this.formBuilder.group({
    name: ['', [Validators.required, Validators.maxLength(200)]],
    price: [0, [Validators.required, Validators.min(0)]],
    isActive: [true]
  });

  products: ProductResponse[] = [];
  isSubmitting = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    this.loadProducts();
  }

  createProduct(): void {
    if (this.form.invalid || this.isSubmitting) {
      return;
    }

    const payload = {
      name: this.form.controls.name.value ?? '',
      price: Number(this.form.controls.price.value ?? 0),
      isActive: this.form.controls.isActive.value ?? true
    };

    this.errorMessage = '';
    this.isSubmitting = true;

    this.inventoryService.createProduct(payload).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.form.reset({ name: '', price: 0, isActive: true });
        this.loadProducts();
      },
      error: () => {
        this.isSubmitting = false;
        this.errorMessage = 'Failed to create product.';
      }
    });
  }

  loadProducts(): void {
    this.inventoryService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
      },
      error: () => {
        this.errorMessage = 'Failed to load products.';
      }
    });
  }
}
