import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';

import { ForgetpasswordComponent } from './forgetpassword/forgetpassword.component';
import { LoginComponent } from './login/login.component';
import { FormsModule } from '@angular/forms';



@NgModule({
  declarations: [
    LoginComponent,
    ForgetpasswordComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild([
      { path: '', component: LoginComponent },
      { path: 'forgetpassword', component: ForgetpasswordComponent }
    ]),
    FormsModule
  ]
})
export class AuthenticationModule { }
