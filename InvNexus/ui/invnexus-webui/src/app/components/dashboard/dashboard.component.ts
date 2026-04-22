import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <div class="py-12">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <h1 class="text-3xl font-bold text-gray-900 mb-8">Dashboard</h1>
        
        <div class="grid grid-cols-1 md:grid-cols-2 gap-6">
          <!-- Products Card -->
          <div class="bg-white rounded-lg shadow p-6 hover:shadow-lg transition-shadow">
            <div class="flex items-center justify-between">
              <div>
                <p class="text-gray-500 text-sm font-medium">Products</p>
                <p class="text-3xl font-bold text-gray-900 mt-2">Manage your products</p>
              </div>
              <div class="text-blue-600 text-4xl">📦</div>
            </div>
            <p class="text-gray-600 text-sm mt-4">Create, view, and manage your product inventory</p>
            <a routerLink="/products" 
               class="inline-block mt-4 bg-blue-600 hover:bg-blue-700 text-white px-4 py-2 rounded text-sm font-medium">
              Go to Products
            </a>
          </div>

          <!-- Stock Card -->
          <div class="bg-white rounded-lg shadow p-6 hover:shadow-lg transition-shadow">
            <div class="flex items-center justify-between">
              <div>
                <p class="text-gray-500 text-sm font-medium">Stock</p>
                <p class="text-3xl font-bold text-gray-900 mt-2">View stock levels</p>
              </div>
              <div class="text-green-600 text-4xl">📊</div>
            </div>
            <p class="text-gray-600 text-sm mt-4">Monitor your inventory stock levels by product</p>
            <a routerLink="/stock" 
               class="inline-block mt-4 bg-green-600 hover:bg-green-700 text-white px-4 py-2 rounded text-sm font-medium">
              View Stock
            </a>
          </div>
        </div>
      </div>
    </div>
  `,
  styles: []
})
export class DashboardComponent {}
