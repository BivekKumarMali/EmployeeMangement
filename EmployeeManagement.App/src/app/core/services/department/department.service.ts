import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, map, tap } from 'rxjs/operators';
import { Department } from 'src/app/models/Department';
import { environment } from 'src/environments/environment';
@Injectable({
  providedIn: 'root'
})
export class DepartmentService {

  url = environment.url;
  constructor(private http: HttpClient) { }

  ResetDepartment(): Department {
    return {
      Did: 0,
      DepartmentName: ''
    };
  }


  GetAllDepartments(): Observable<Department[]> {
    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.get<Department[]>(this.url + '/Department', { headers })
      .pipe(
        catchError(this.handleError)
      );
  }

  EditandAddDepartment(department: Department): Observable<Department> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.post<Department>(this.url + '/Department', department, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }
  DeleteDepartment(did: number): Observable<{}> {

    const headers = new HttpHeaders({
      'Content-Type': 'application/json; charset=utf-8',
      Authorization: 'Bearer ' + localStorage.getItem('token')
    });
    return this.http.delete<Department>(this.url + '/Department/' + did, { headers })
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
