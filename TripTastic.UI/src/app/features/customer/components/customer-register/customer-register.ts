
import { Component, inject } from '@angular/core';
import {
  FormBuilder,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { finalize } from 'rxjs';
import { CustomerService } from '../../services/customer.Service';

@Component({
  selector: 'app-customer-register',
  imports: [ReactiveFormsModule],
  templateUrl: './customer-register.html',
  styleUrl: './customer-register.scss',
})
export class CustomerRegister {
  private readonly fb = inject(FormBuilder).nonNullable;
  private readonly customerService = inject(CustomerService);

  isSubmitting = false;
  successMessage = '';
  errorMessage = '';

  registerForm = this.fb.group({
    firstName: ['', Validators.required],
    lastName: ['', Validators.required],
    email: ['', [Validators.required, Validators.email]],
    phoneNumber: [
      '',
      [Validators.required, Validators.pattern(/^[0-9]{10}$/)],
    ],
    password: ['', [Validators.required, Validators.minLength(8)]],
  });

  onSubmit(): void {
    this.successMessage = '';
    this.errorMessage = '';

    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    this.isSubmitting = true;

    this.customerService
      .registerCustomer(this.registerForm.getRawValue())
      .pipe(finalize(() => (this.isSubmitting = false)))
      .subscribe({
        next: (message) => {
          this.successMessage =
            message || 'Customer registered successfully.';
          this.registerForm.reset();
        },
        error: (error) => {
          if (error.status === 400) {
            this.errorMessage =
              typeof error.error === 'string'
                ? error.error
                : 'Please check your registration details.';
          } else if (error.status === 0) {
            this.errorMessage =
              'Cannot connect to the server. Check that the .NET API is running.';
          } else {
            this.errorMessage = 'Registration failed. Please try again.';
          }
        },
      });
  }
}
