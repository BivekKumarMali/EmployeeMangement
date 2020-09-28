import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';
import { UtilityService } from '../utility/utility.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {


  constructor(private router: Router, private utilityService: UtilityService) {
  }


  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): boolean {
    if (localStorage.getItem('token') != null) {
      const roles = next.data.permittedRoles as Array<string>;
      if (roles) {
        if (this.utilityService.JWTCheckRoles(roles)) { return true; }
        else {
          this.router.navigate(['/pagenotfound']);
          return false;
        }
      }
      return true;
    }
    else {
      this.router.navigate(['/login']);
      return false;
    }
  }
}
