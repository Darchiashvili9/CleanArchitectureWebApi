import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormControl, Validators } from '@angular/forms';
import { Router } from '@angular/router';

import { AuthenticationService } from '../../services/authentication.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  styleUrls: ['./register.component.scss']
})
export class RegisterComponent implements OnInit {

  registrationForm: FormGroup;
  loading = false;
  serverErrorMessage = '';

  constructor(
    private fb: FormBuilder,
    private router: Router,
    private authenticationService: AuthenticationService) {
      this.registrationForm = this.fb.group({
        username: new FormControl('', [Validators.required]),
        email: new FormControl('', [Validators.required, Validators.email]),
        password: new FormControl('', [Validators.required]),
    });
  }

  submitRegistrationForm(): void {
    if (this.registrationForm.invalid) {
      return;
    }

    this.loading = true;

    const username = this.registrationForm.get('username')?.value;
    const email = this.registrationForm.get('email')?.value;
    const password = this.registrationForm.get('password')?.value;

    this.authenticationService.register(username, email, password).subscribe((data) => {
      this.router.navigate(['login']);
    }, (error) => {
      this.serverErrorMessage = error;
      this.loading = false;
    });
  }

  ngOnInit(): void {
  }
}
