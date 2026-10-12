
import { Injectable, inject } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface RegisterCustomerRequest {
  firstName: string;
  lastName: string;
  email: string;
  phoneNumber: string;
  password: string;
  dateOfBirth?: string | null;
  gender?: string | null;
}

@Injectable({
  providedIn: 'root',
})
export class CustomerService {
  private readonly http = inject(HttpClient);

  // Confirm this port against TripTastic.API launchSettings.json.
  private readonly apiUrl = 'https://localhost:7028/api/Auth';

  registerCustomer(
    request: RegisterCustomerRequest
  ): Observable<string> {
    return this.http.post(
      `${this.apiUrl}/register-customer`,
      request,
      { responseType: 'text' }
    );
  }
}
