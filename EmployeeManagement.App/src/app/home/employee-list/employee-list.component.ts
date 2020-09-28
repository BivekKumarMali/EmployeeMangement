import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/core/services/employee/employee.service';
import { UtilityService } from 'src/app/core/utility/utility.service';
import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-employee-list',
  templateUrl: './employee-list.component.html',
  styleUrls: ['./employee-list.component.css']
})
export class EmployeeListComponent implements OnInit {

  employees: Employee[];
  errorMessage: any;
  constructor(
    private utilityService: UtilityService,
    private employeeService: EmployeeService
  ) { }

  ngOnInit(): void {
    if (!this.utilityService.JWTCheckRoles(['Admin', 'HR'])) {
      const DepartmentID = this.utilityService.GetDepartmentID();
      this.employeeService.GetEmployeesByDepartment(DepartmentID).subscribe({
        next: employee => this.employees = employee,
        error: err => this.errorMessage = err,
      });
    }
    else {
      this.employeeService.GetEmployees().subscribe({
        next: employee => this.employees = employee,
        error: err => this.errorMessage = err,
      });
    }
  }

}
