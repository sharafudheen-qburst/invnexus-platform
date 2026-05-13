import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Router, RouterLink } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-navbar',
  standalone: true,
  imports: [CommonModule, RouterLink],
  template: `
    <header *ngIf="authService.isAuthenticated()" style="background:#0f172a;color:#fff;">
      <div class="container" style="display:flex;justify-content:space-between;align-items:center;gap:1rem;">
        <div style="font-weight:700;">InvNexus</div>
        <nav style="display:flex;gap:0.75rem;align-items:center;">
          <a routerLink="/dashboard">Dashboard</a>
          <a routerLink="/products">Products</a>
          <a routerLink="/stock">Stock</a>
          <a routerLink="/purchase">Purchase</a>
          <a routerLink="/sales">Sales</a>
          <button class="button" style="background:#334155;color:#fff;" (click)="logout()">Logout</button>
        </nav>
      </div>
    </header>
  `
})
export class NavbarComponent {
  constructor(
    public readonly authService: AuthService,
    private readonly router: Router
  ) {}

  logout(): void {
    this.authService.logout();
    this.router.navigate(['/login']);
  }
}
