import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { MatDialog } from '@angular/material/dialog';
import { FailureDialogComponent } from '../modals/failure-dialog/failure-dialog.component';
import { AuthService } from '../services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrl: './login.component.css',
})
export class LoginComponent {
  loginForm: FormGroup;
  errorMessage: string = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authService: AuthService,
    private dialog: MatDialog
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onLogin(): void {
    if (this.loginForm.invalid) return;

    const { username, password } = this.loginForm.value;

    this.authService.login(username, password).subscribe({
      next: (data) => {
        this.router.navigate(['/home']);
      },
      error: (err) => {
        console.error(err);
        this.errorMessage = '';
        if (err.status == 500)
          this.dialog.open(FailureDialogComponent, {
            data: {
              message: 'Error connecting to Oracle. ',
            },
          });
        else if (err.status == 0)
          this.dialog.open(FailureDialogComponent, {
            data: {
              message: 'Error connecting to API.',
            },
          });
        else if (err.status == 401)
          this.errorMessage = 'Invalid credentials. Please try again.';
      },
    });
  }
}
