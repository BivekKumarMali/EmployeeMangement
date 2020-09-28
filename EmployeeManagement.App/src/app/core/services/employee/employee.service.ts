import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, tap } from 'rxjs/operators';
import { Department } from 'src/app/models/Department';
import { environment } from 'src/environments/environment';
import { Employee } from 'src/app/models/employee';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {

  url = environment.url;
  constructor(private http: HttpClient) { }

  GetEmployeesByDepartment(Did: number): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url + '/Employee/' + Did)
      .pipe(
        catchError(this.handleError)
      );
  }
  GetEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url + '/Employee')
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
