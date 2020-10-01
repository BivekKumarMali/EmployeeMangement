import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { NotificationDetails } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  url = environment.url;
  public data: NotificationDetails[];
  private hubConnection: signalR.HubConnection;


  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.url + '/signalServer')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }


  public addTransferChartDataListener = () => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      console.log('Message', data);
    });
  }
}
