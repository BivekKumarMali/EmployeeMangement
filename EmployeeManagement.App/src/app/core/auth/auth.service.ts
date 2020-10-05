import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { Login } from 'src/app/models/Login';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  url = environment.url;
  constructor(private http: HttpClient, private router: Router) { }


  Login(login: Login): void {
    // return this.http.post(this.url + '/Account/Login', login);

    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    const as = JSON.stringify(login);

    this.http.post(this.url + '/Account/Login', login, { headers }).subscribe(
      (next: any) => {
        localStorage.setItem('token', next.token);

        const token = localStorage.getItem('token');
        const jwtData = token.split('.')[1];
        const decodedJwtJsonData = window.atob(jwtData);
        const decodedJwtData = JSON.parse(decodedJwtJsonData);
        const roleName = decodedJwtData.role;
        localStorage.setItem('userRole', roleName);
        localStorage.setItem('userName', decodedJwtData.name);

        console.log('isrole: ' + roleName);
        console.log('name: ' + decodedJwtData.name);
        console.log(token);

        catchError(this.handleError),
          this.router.navigate(['/home']);
      });
  }

  ResetPassword(passwordReset: { Email: any; NewPassword: any; ConfirmPassword: any; }) {
    return this.http.post(this.url + '/Account/ResetPassword', passwordReset);
  }

  private handleError(err) {
    let errorMessage: string;
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Backend returned code ${err.status}: ${err.body.error}`;
    }
    console.error(err);
    alert('Try again');
    return throwError(errorMessage);

  }
}
