import { HttpClient, HttpErrorResponse, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IsRead } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class NotificationService {
  url = environment.url;

  constructor(
    private http: HttpClient
  ) { }


  IsReadNotifications(nid: number, userId: string): Observable<string> {
    // tslint:disable-next-line:variable-name
    const nid_userId = nid + ' ' + userId;
    const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
    return this.http.post<string>(this.url + '/notification/' + nid_userId, { headers })
      .pipe(
        catchError(this.handleError)
      );
  }


  // GetIsReadNotifications(userID: string): Observable<IsRead[]> {
  //   const headers = new HttpHeaders({ 'Content-Type': 'application/json' });
  //   return this.http.get<IsRead[]>(this.url + '/Notification/GetIsRead/' + userID, { headers })
  //     .pipe(
  //       catchError(this.handleError)
  //     );
  // }

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
