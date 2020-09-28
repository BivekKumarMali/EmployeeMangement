import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth/auth.service';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetpasswordComponent implements OnInit {

  errormessage: string;
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  ResetPassword(form: NgForm): void {
    const passwordReset = {
      Email: form.value.email,
      NewPassword: form.value.newpassword,
      ConfirmPassword: form.value.confirmpassword
    };

    this.authService.ResetPassword(passwordReset).subscribe({
      error: err => this.errormessage = err,
      complete: () => this.router.navigate(['/login'])
    });
  }
}
