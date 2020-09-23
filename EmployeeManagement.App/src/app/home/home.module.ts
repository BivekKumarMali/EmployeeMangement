import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { EditEmployeeComponent } from './edit-employee/edit-employee.component';
import { EmployeeListComponent } from './employee-list/employee-list.component';
import { FormsModule } from '@angular/forms';
import { DepartmentComponent } from './department/department.component';
import { HomeComponent } from './home.component';
import { RouterModule } from '@angular/router';
import { SharedModule } from '../shared/shared.module';



@NgModule({
  declarations: [
    EmployeeListComponent,
    EditEmployeeComponent,
    DepartmentComponent,
    HomeComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    SharedModule,
    RouterModule.forChild([
      { path: 'employee', component: EmployeeListComponent },
      { path: '', redirectTo: 'employee', pathMatch: 'full' },
      { path: 'department', component: DepartmentComponent },
      { path: 'employee/:eid/edit', component: EditEmployeeComponent }
    ])
  ]
})
export class HomeModule { }
