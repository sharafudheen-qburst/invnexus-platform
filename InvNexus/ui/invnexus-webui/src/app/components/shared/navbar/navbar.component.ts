import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { AuthService } from '../../../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterModule],
  template: `
    <nav *ngIf="isAuthenticated$ | async" class="bg-white border-b border-gray-200 shadow-sm">
      <div class="max-w-7xl mx-auto px-4 sm:px-6 lg:px-8">
        <div class="flex justify-between items-center h-16">
          <div class="flex items-center">
            <h1 class="text-xl font-bold text-gray-900">InvNexus</h1>
          </div>
          
          <div class="flex space-x-4">
            <a routerLink="/dashboard" routerLinkActive="text-blue-600" 
               [routerLinkActiveOptions]="{ exact: true }"
               class="text-gray-700 hover:text-blue-600 px-3 py-2 text-sm font-medium">
              Dashboard
            </a>
            <a routerLink="/products" routerLinkActive="text-blue-600"
               class="text-gray-700 hover:text-blue-600 px-3 py-2 text-sm font-medium">
              Products
            </a>
            <a routerLink="/stock" routerLinkActive="text-blue-600"
               class="text-gray-700 hover:text-blue-600 px-3 py-2 text-sm font-medium">
              Stock
            </a>
          </div>

          <div>
            <button (click)="logout()" 
                    class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded text-sm font-medium">
              Logout
            </button>
          </div>
        </div>
      </div>
    </nav>
  `,
  styles: [`
    nav {
      position: sticky;
      top: 0;
      z-index: 100;
    }
  `]
})
export class NavbarComponent implements OnInit {
  isAuthenticated$ = this.authService.isAuthenticated$;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  logout(): void {
    this.authService.logout();
    window.location.href = '/login';
  }
}
