import { Component } from '@angular/core';
import { RouterLink } from '@angular/router';

@Component({
  selector: 'app-dashboard',
  standalone: true,
  imports: [RouterLink],
  template: `
    <section>
      <h2>Dashboard</h2>
      <p>Welcome to InvNexus phase 1.</p>

      <div class="grid grid-2">
        <a class="card" routerLink="/products">
          <h3 style="margin-top:0;">Products</h3>
          <p>Create and list products.</p>
        </a>

        <a class="card" routerLink="/stock">
          <h3 style="margin-top:0;">Stock</h3>
          <p>View current stock by product id.</p>
        </a>
      </div>
    </section>
  `
})
export class DashboardComponent {}
