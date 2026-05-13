import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, ReactiveFormsModule, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  template: `
    <section class="card" style="max-width:420px;margin:3rem auto;">
      <h2 style="margin-top:0;">Login</h2>

      <form [formGroup]="form" (ngSubmit)="submit()" class="grid">
        <label>
          <div>Email</div>
          <input class="input" type="email" formControlName="email" placeholder="admin@invnexus.com" />
        </label>

        <label>
          <div>Password</div>
          <input class="input" type="password" formControlName="password" placeholder="********" />
        </label>

        <div class="error" *ngIf="errorMessage">{{ errorMessage }}</div>

        <button class="button button-primary" type="submit" [disabled]="form.invalid || isSubmitting">
          {{ isSubmitting ? 'Signing in...' : 'Login' }}
        </button>
      </form>
    </section>
  `
})
export class LoginComponent {
  readonly form = this.formBuilder.group({
    email: ['', [Validators.required, Validators.email]],
    password: ['', [Validators.required]]
  });

  isSubmitting = false;
  errorMessage = '';

  constructor(
    private readonly formBuilder: FormBuilder,
    private readonly authService: AuthService,
    private readonly router: Router
  ) {}

  submit(): void {
    if (this.form.invalid || this.isSubmitting) {
      return;
    }

    const email = this.form.controls.email.value ?? '';
    const password = this.form.controls.password.value ?? '';

    this.errorMessage = '';
    this.isSubmitting = true;

    this.authService.login(email, password).subscribe({
      next: () => {
        this.isSubmitting = false;
        this.router.navigate(['/dashboard']);
      },
      error: () => {
        this.isSubmitting = false;
        this.errorMessage = 'Login failed. Please check credentials.';
      }
    });
  }
}
