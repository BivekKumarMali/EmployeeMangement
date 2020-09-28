import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Login } from 'src/app/models/Login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url = environment.url;
  constructor(private http: HttpClient) { }


  Login(login: Login) {
    return this.http.post(this.url + '/Account/Login', login);
  }

  ResetPassword(passwordReset: { Email: any; NewPassword: any; ConfirmPassword: any; }) {
    return this.http.post(this.url + '/Account/ResetPassword', passwordReset);
  }
}
