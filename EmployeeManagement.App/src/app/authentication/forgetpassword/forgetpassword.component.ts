import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';

@Component({
  selector: 'app-forgetpassword',
  templateUrl: './forgetpassword.component.html',
  styleUrls: ['./forgetpassword.component.css']
})
export class ForgetpasswordComponent implements OnInit {

  errormessage: string;
  constructor(private router: Router) { }

  ngOnInit(): void {
  }

  ResetPassword(form: NgForm): void {
    const passwordReset = {
      Email: form.value.email,
      NewPassword: form.value.newpassword,
      ConfirmPassword: form.value.confirmpassword
    };
    console.log(passwordReset);
    this.router.navigate(['/login']);
  }
}
