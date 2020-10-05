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
  AdminHrService: boolean;
  constructor(
    private utilityService: UtilityService,
    private employeeService: EmployeeService
  ) { }

  ngOnInit(): void {
    this.AdminHrService = this.utilityService.JWTCheckRoles(['Admin', 'HR']);
    if (!this.AdminHrService) {
      const DepartmentID = this.utilityService.GetDepartmentID();
      this.employeeService.GetEmployeesByDepartment(DepartmentID).subscribe({
        next: employee => this.employees = employee,
        error: err => this.errorMessage = err,
      });
    }
    else {
      this.employeeService.GetEmployees().subscribe({
        next: employee => {
          console.log(employee);
          this.employees = employee;
        },
        error: err => this.errorMessage = err,
      });
    }
  }
  DeleteEmployee(eid: number) {
    if (confirm('Are you sure')) {
      this.employeeService.DeleteEmployee(eid).subscribe({
        error: err => this.errorMessage = err,
        complete: () => location.reload()
      });
      location.reload();
    }
  }

}
