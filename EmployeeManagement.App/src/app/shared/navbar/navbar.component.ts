import { HttpClient } from '@angular/common/http';
import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { SignalRService } from 'src/app/core/utility/signal-r.service';
import { UtilityService } from 'src/app/core/utility/utility.service';
import { NotificationDetails } from 'src/app/models/Notification';
import { environment } from 'src/environments/environment';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  Name: string;
  hideRoles: boolean;
  hideDepartment: boolean;
  UserID: string;
  NotificationDetails: object;
  url = environment.url;
  constructor(
    private utilityService: UtilityService,
    private router: Router,
    public signalRService: SignalRService,
    private http: HttpClient
  ) { }

  ngOnInit(): void {
    this.UserID = this.utilityService.JwtUserIDExtractor();
    this.Name = this.utilityService.JWTNameExtractor();
    this.hideRoles = this.utilityService.JWTCheckRoles(['Admin']);
    this.hideDepartment = this.utilityService.JWTCheckRoles(['Admin', 'HR']);
    this.signalRService.startConnection();
    this.signalRService.addTransferChartDataListener();
    this.startHttpRequest();
  }
  Logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
  private startHttpRequest = () => {
    this.http.get(this.url + '/notification/' + this.UserID)
      .subscribe(res => {
        this.NotificationDetails = res;
      });
  }
}

