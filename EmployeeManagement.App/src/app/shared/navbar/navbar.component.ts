import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { UtilityService } from 'src/app/core/utility/utility.service';

@Component({
  selector: 'app-navbar',
  templateUrl: './navbar.component.html',
  styleUrls: ['./navbar.component.css']
})
export class NavbarComponent implements OnInit {

  Name: string;
  hideRoles: boolean;
  hideDepartment: boolean;
  constructor(
    private utilityService: UtilityService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.Name = this.utilityService.JWTNameExtractor();
    this.hideRoles = this.utilityService.JWTCheckRoles(['Admin']);
    this.hideDepartment = this.utilityService.JWTCheckRoles(['Admin', 'HR']);
  }
  Logout() {
    localStorage.removeItem('token');
    this.router.navigate(['/login']);
  }
}

