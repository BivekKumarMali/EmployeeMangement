import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NotificationService } from 'src/app/core/services/notification/notification.service';
import { SignalRService } from 'src/app/core/utility/SignalR/signal-r.service';
import { UtilityService } from 'src/app/core/utility/utility.service';
import { IsRead, NotificationDetails } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  Name: string;
  isRead: IsRead[];
  hideRoles: boolean;
  hideDepartment: boolean;
  UserID: string;
  NotificationDetails: NotificationDetails[];
  url = environment.url;
  errorMessage: any;
  Role: string;
  Did: number;
  constructor(
    private utilityService: UtilityService,
    private router: Router,
    public signalRService: SignalRService,
    private http: HttpClient,
    private notificationService: NotificationService
  ) { }

  ngOnInit(): void {
    this.UserID = this.utilityService.JwtUserIDExtractor();
    this.Role = this.utilityService.JWTRoleExtractor();
    this.Name = this.utilityService.JWTNameExtractor();
    this.Did = this.utilityService.GetDepartmentID();
    this.hideRoles = this.utilityService.JWTCheckRoles(['Admin']);
    this.hideDepartment = this.utilityService.JWTCheckRoles(['Admin', 'HR']);
    this.fetchIsRead();
  }
  Logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  private startHttpRequest = () => {
    this.http.get(this.url + '/notification/GetNotification/')
      .subscribe(res => {
        console.log();
      });
  }

  ReaddNotifications(nid: number) {
    const userId = this.utilityService.JwtUserIDExtractor();
    const index = this.signalRService.data.findIndex(x => x.nid === nid);

    this.notificationService.IsReadNotifications(nid, userId).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.signalRService.RemoveIndex(index)
    });
  }

  fetchIsRead() {
    this.notificationService.GetIsReadNotifications(this.UserID).subscribe({
      next: data => this.isRead = data,
      error: err => this.errorMessage = err,
      complete: () => {
        this.startHttpRequest();
        this.signalRService.addTransferChartDataListener(this.isRead, this.UserID, this.Role, this.Did);

      }
    });
  }
}

