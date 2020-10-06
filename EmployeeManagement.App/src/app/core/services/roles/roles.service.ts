import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { environment } from 'src/environments/environment';
import { Roles } from 'src/app/models/Roles';

@Injectable({
  providedIn: 'root'
})
export class RolesService {

  url = environment.url;
  constructor(private http: HttpClient) { }

  ResetRoles(): Roles {
    return {
      Id: '',
      Name: ''
    };
  }


  GetAllRoles(): Observable<Roles[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Roles[]>(this.url + '/Roles', { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  EditandAddRoles(roles: Roles): Observable<Roles> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Roles>(this.url + '/Roles', roles, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }
  DeleteRoles(id: string): Observable<{}> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<Roles>(this.url + '/Roles/' + id)
      .pipe(
        catchError(this.handleError)
      );
  }


  private handleError(err: HttpErrorResponse) {
    let errorMessage = '';
    if (err.error instanceof ErrorEvent) {
      errorMessage = `An error occurred: ${err.error.message}`;
    } else {
      errorMessage = `Server returned code: ${err.status}, error message is: ${err.message}`;
    }
    console.log(err);
    console.error(errorMessage);
    return throwError(errorMessage);
  }
}
