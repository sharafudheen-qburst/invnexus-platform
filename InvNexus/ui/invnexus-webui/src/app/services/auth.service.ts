import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, map, tap } from 'rxjs';
import { environment } from '../../environments/environment';

interface LoginRequest {
  email: string;
  password: string;
}

interface LoginResponse {
  token: string;
}

@Injectable({ providedIn: 'root' })
export class AuthService {
  private readonly tokenStorageKey = 'invnexus_token';
  private readonly baseUrl = environment.authServiceUrl;

  constructor(private readonly http: HttpClient) {}

  login(email: string, password: string): Observable<void> {
    const body: LoginRequest = { email, password };

    return this.http
      .post<LoginResponse>(`${this.baseUrl}/api/auth/login`, body)
      .pipe(
        tap((response) => localStorage.setItem(this.tokenStorageKey, response.token)),
        map(() => void 0)
      );
  }

  getToken(): string | null {
    return localStorage.getItem(this.tokenStorageKey);
  }

  isAuthenticated(): boolean {
    return !!this.getToken();
  }

  logout(): void {
    localStorage.removeItem(this.tokenStorageKey);
  }
}
