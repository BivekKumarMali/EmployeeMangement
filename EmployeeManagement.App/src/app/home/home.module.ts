import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { FormsModule } from '@angular/forms';

import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { DepartmentComponent } from './department/department.component';
import { HomeComponent } from './home.component';
import { SharedModule } from '../shared/shared.module';
import { ProfileComponent } from './profile/profile.component';
import { AuthGuard } from '../core/auth/auth.guard';



@NgModule({
  declarations: [
    EmployeeListComponent,
    EditEmployeeComponent,
    DepartmentComponent,
    HomeComponent,
    ProfileComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild([
      { path: 'employee', component: EmployeeListComponent },
      { path: '', redirectTo: 'employee', pathMatch: 'full' },
      { path: 'department', component: DepartmentComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin', 'HR'] } },
      { path: 'profile', component: ProfileComponent },
      { path: 'employee/:eid/edit', component: EditEmployeeComponent, canActivate: [AuthGuard], data: { permittedRoles: ['Admin', 'HR'] } }
    ])
  ]
})
export class HomeModule { }
