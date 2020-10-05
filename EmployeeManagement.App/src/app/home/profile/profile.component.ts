import { Component, OnInit } from '@angular/core';
import { EmployeeService } from 'src/app/core/services/employee/employee.service';
import { UtilityService } from 'src/app/core/utility/utility.service';
import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  Employee: Employee;
  Edit = false;
  errorMessage: any;
  constructor(
    private employeeService: EmployeeService,
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    const userId = this.utilityService.JwtUserIDExtractor();
    this.employeeService.GetEmployeesByUserId(userId).subscribe({
      next: data => this.Employee = data,
      error: err => this.errorMessage = err,
    });
  }

  EnableEdit() {
    this.Edit = this.Edit ? false : true;
  }

  EditEmployee() {
    this.employeeService.EditEmployee(this.Employee).subscribe({
      error: err => this.errorMessage = err,
      complete: () => {
        this.EnableEdit();
        this.ngOnInit();
      }
    });
  }
}
