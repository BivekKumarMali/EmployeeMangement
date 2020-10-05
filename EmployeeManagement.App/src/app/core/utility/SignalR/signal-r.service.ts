import { Injectable } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { IsRead, NotificationDetails } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  url = environment.url;
  public data: NotificationDetails[];
  private hubConnection: signalR.HubConnection;

  constructor() {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(this.url + '/signalServer')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err));
  }


  public addTransferChartDataListener = (isread: IsRead[], userId: string, role: string, did: number) => {
    this.hubConnection.on('transferchartdata', (data) => {
      this.data = data;
      this.SetNotification(isread, userId, role, did);
    });
  }

  SetNotification(isRead: IsRead[], userId: string, role: string, did: number) {
    console.log(this.data, isRead, role);
    this.data.reverse();
    did = Number(did);
    isRead.forEach(element => {
      const index = this.data.findIndex(x => x.nid === element.nid);
      this.RemoveIndex(index);
    });
    if (role !== 'Admin' && role !== 'HR') {
      console.log(this.data[19].did, did);
      this.data = this.data.filter(x => x.did === did);

    }
  }
  RemoveIndex(index: number): void {
    this.data.splice(index, 1);
  }
}
