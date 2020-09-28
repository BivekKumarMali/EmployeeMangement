import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/core/auth/auth.service';
import { Login } from 'src/app/models/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;
  errormessage: string;
  constructor(
    private router: Router,
    private authService: AuthService
  ) { }

  ngOnInit(): void {
  }

  ToLogin(form: NgForm): void {
    this.login = form.value;
    this.authService.Login(this.login).subscribe(
      (res: any) => {
        console.log(res.token);
        localStorage.setItem('token', res.token);
        this.router.navigateByUrl('/home');
      },
      err => this.errormessage = err
    );
  }

}
