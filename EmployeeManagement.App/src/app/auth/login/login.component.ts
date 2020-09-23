import { Component, OnInit } from '@angular/core';
import { NgForm } from '@angular/forms';
import { Router } from '@angular/router';
import { Login } from 'src/app/models/Login';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  login: Login;
  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }

  ToLogin(form: NgForm): void {
    this.login = form.value;
    console.log(this.login);
    this.router.navigate(['/home']);
  }

}
