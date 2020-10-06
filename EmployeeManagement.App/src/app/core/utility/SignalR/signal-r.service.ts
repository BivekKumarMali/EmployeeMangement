import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { IsRead, NotificationDetails } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  url = environment.url;
  public data: any;
  private hubConnection: signalR.HubConnection;
  loginToken: string;

  constructor() {
    this.loginToken = localStorage.getItem('token');
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.url + '/signalServer', { accessTokenFactory: () => this.loginToken })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }


  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data) => {
      console.log(data);
      this.data = data;
    });
  }
}
