import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { InventoryService, Stock, Product } from '../../services/inventory.service';

@Component({
  selector: 'app-stock',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <div class="py-12">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h1 class="text-3xl font-bold text-gray-900 mb-8">Stock Management</h1>

        <!-- Stock Filter -->
        <div class="bg-white rounded-lg shadow p-6 mb-8">
          <h2 class="text-xl font-bold text-gray-900 mb-4">View Stock by Product</h2>
          
          <form [formGroup]="filterForm" class="space-y-4">
            <div>
              <label class="block text-sm font-medium text-gray-700 mb-1">Select Product</label>
              <select
                formControlName="productId"
                (change)="onProductSelected()"
                class="w-full px-3 py-2 border border-gray-300 rounded-md focus:outline-none focus:ring-blue-500 focus:border-blue-500"
              >
                <option value="">-- Select a product --</option>
                <option *ngFor="let product of products" [value]="product.id">
                  {{ product.name }}
                </option>
              </select>
            </div>

            <div *ngIf="selectedStock" class="bg-blue-50 border border-blue-200 rounded-md p-4">
              <p class="text-sm text-gray-700">
                <span class="font-semibold">Product:</span> {{ selectedStock.productName }}
              </p>
              <p class="text-sm text-gray-700 mt-2">
                <span class="font-semibold">Current Stock:</span> {{ selectedStock.quantity }} units
              </p>
            </div>

            <div *ngIf="errorMessage" class="rounded-md bg-red-50 p-4">
              <p class="text-sm text-red-700">{{ errorMessage }}</p>
            </div>
          </form>
        </div>

        <!-- Stock Table -->
        <div class="bg-white rounded-lg shadow overflow-hidden">
          <div class="px-6 py-4 border-b border-gray-200">
            <h2 class="text-lg font-bold text-gray-900">All Stock Levels</h2>
          </div>
          <table class="min-w-full divide-y divide-gray-200">
            <thead class="bg-gray-50">
              <tr>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-700 uppercase tracking-wider">Product Name</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-700 uppercase tracking-wider">Quantity</th>
                <th class="px-6 py-3 text-left text-xs font-medium text-gray-700 uppercase tracking-wider">Status</th>
              </tr>
            </thead>
            <tbody class="bg-white divide-y divide-gray-200">
              <tr *ngFor="let stock of stocks" class="hover:bg-gray-50">
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ stock.productName }}</td>
                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-900">{{ stock.quantity }} units</td>
                <td class="px-6 py-4 whitespace-nowrap text-sm">
                  <span *ngIf="stock.quantity > 10" class="px-2 py-1 inline-flex text-xs leading-5 font-semibold rounded-full bg-green-100 text-green-800">
                    In Stock
                  </span>
                  <span *ngIf="stock.quantity > 0 && stock.quantity <= 10" class="px-2 py-1 inline-flex text-xs leading-5 font-semibold rounded-full bg-yellow-100 text-yellow-800">
                    Low Stock
                  </span>
                  <span *ngIf="stock.quantity === 0" class="px-2 py-1 inline-flex text-xs leading-5 font-semibold rounded-full bg-red-100 text-red-800">
                    Out of Stock
                  </span>
                </td>
              </tr>
              <tr *ngIf="stocks.length === 0">
                <td colspan="3" class="px-6 py-4 text-center text-sm text-gray-500">No stock data available</td>
              </tr>
            </tbody>
          </table>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class StockComponent implements OnInit {
  filterForm!: FormGroup;
  stocks: Stock[] = [];
  products: Product[] = [];
  selectedStock: Stock | null = null;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private inventoryService: InventoryService
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadProducts();
    this.loadStocks();
  }

  initForm(): void {
    this.filterForm = this.fb.group({
      productId: ['']
    });
  }

  loadProducts(): void {
    this.inventoryService.getProducts().subscribe({
      next: (products) => {
        this.products = products;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load products';
        console.error('Error loading products:', err);
      }
    });
  }

  loadStocks(): void {
    this.inventoryService.getStock().subscribe({
      next: (stocks) => {
        this.stocks = stocks;
      },
      error: (err) => {
        this.errorMessage = 'Failed to load stock data';
        console.error('Error loading stock:', err);
      }
    });
  }

  onProductSelected(): void {
    const productId = this.filterForm.get('productId')?.value;
    
    if (!productId) {
      this.selectedStock = null;
      return;
    }

    this.inventoryService.getStockByProduct(productId).subscribe({
      next: (stock) => {
        this.selectedStock = stock;
        this.errorMessage = '';
      },
      error: (err) => {
        this.selectedStock = null;
        this.errorMessage = err.error?.message || 'Failed to load stock details';
      }
    });
  }
}
