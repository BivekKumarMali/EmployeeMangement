import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class UtilityService {

  token: string;
  constructor() {
    this.token = localStorage.getItem('token');
  }

  JWTNameExtractor(): string {
    const payLoad = this.GetPayLoad();
    if (payLoad != null) {
      return payLoad.Name;
    }
  }

  JWTCheckRoles(allowedRoles: string[]): boolean {
    let isAllowed = false;
    const payLoad = this.GetPayLoad();
    if (payLoad != null) {
      const roles = payLoad.role;

      allowedRoles.forEach(element => {
        if (roles === element) {
          isAllowed = true;
        }
      });
    }
    return isAllowed;
  }

  GetPayLoad(): any {
    if (this.token != null) {
      const payLoad = JSON.parse(window.atob(this.token.split('.')[1]));
      return payLoad;
    }
    return null;
  }


  GetDepartmentID(): number {
    const payLoad = this.GetPayLoad();
    if (payLoad != null) {
      return payLoad.Did;
    }
  }

  JwtUserIDExtractor(): string {
    const payLoad = this.GetPayLoad();

    if (payLoad != null) {
      return payLoad.UserID;
    }
  }
}
