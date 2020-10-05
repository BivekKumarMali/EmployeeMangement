import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
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


  IntailizeEmployee(): Employee {
    return {
      address: '',
      contactNumber: null,
      did: 0,
      eid: 0,
      name: '',
      qualification: '',
      roleId: '',
      surname: '',
      userId: '',
      department: null
    };
  }

  GetEmployees(): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url + '/Employee')
      .pipe(
        catchError(this.handleError)
      );
  }

  GetEmployeesByDepartment(Did: number): Observable<Employee[]> {
    return this.http.get<Employee[]>(this.url + '/Employee/ByDid/' + Did)
      .pipe(
        catchError(this.handleError)
      );
  }

  GetEmployeesByEid(eid: number): Observable<Employee> {
    return this.http.get<Employee>(this.url + '/Employee/ByEid/' + eid)
      .pipe(
        catchError(this.handleError)
      );
  }


  GetEmployeesByUserId(userId: string): Observable<Employee> {
    return this.http.get<Employee>(this.url + '/Employee/ByUserID/' + userId)
      .pipe(
        catchError(this.handleError)
      );
  }

  AddEmployee(employee: Employee): Observable<Employee> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<Employee>(this.url + '/Employee', employee, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  EditEmployee(employee: Employee): Observable<Employee> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.put<Employee>(this.url + '/Employee', employee, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  DeleteEmployee(eid: number): Observable<{}> {
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.delete<Employee>(this.url + '/Employee/' + eid)
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
