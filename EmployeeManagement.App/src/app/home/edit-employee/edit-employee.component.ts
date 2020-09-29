import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DepartmentService } from 'src/app/core/services/department/department.service';
import { EmployeeService } from 'src/app/core/services/employee/employee.service';
import { RolesService } from 'src/app/core/services/roles/roles.service';
import { Department } from 'src/app/models/Department';
import { Employee } from 'src/app/models/employee';
import { Roles } from 'src/app/models/Roles';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  Employee: Employee;
  Departments: Department[];
  Roles: Roles[];
  errorMessage: any;
  constructor(
    private employeeService: EmployeeService,
    private route: ActivatedRoute,
    private departmentService: DepartmentService,
    private roleService: RolesService,
    private router: Router
  ) { }

  ngOnInit(): void {
    const eid = +this.route.snapshot.paramMap.get('eid');
    if (eid === 0) { this.ResetEmployee(); }
    else {
      this.employeeService.GetEmployeesByEid(eid).subscribe({
        next: employee => this.Employee = employee,
        error: err => this.errorMessage = err,
        complete: () => console.log(this.Employee)

      });
    }
    this.FetchAllDepartments();
  }
  FetchAllDepartments() {
    this.departmentService.GetAllDepartments().subscribe({
      next: department => this.Departments = department,
      error: err => this.errorMessage = err,
      complete: () => this.FetchAllRoles()
    });
  }

  FetchAllRoles() {
    this.roleService.GetAllRoles().subscribe({
      next: role => this.Roles = role,
      error: err => this.errorMessage = err,
    });
  }

  AddEmployee() {
    console.log(this.Employee);
    if (this.Employee.eid === 0) {
      this.Employee.did = Number(this.Employee.did);
      this.employeeService.AddEmployee(this.Employee).subscribe({
        error: err => this.errorMessage = err
      });
    }
    else {
      this.employeeService.EditEmployee(this.Employee).subscribe({
        error: err => this.errorMessage = err
      });
    }
    this.RouteToHome();
  }
  RouteToHome() {
    this.router.navigate(['/home']);
  }

  ResetEmployee(): void {
    this.Employee = this.employeeService.IntailizeEmployee();
  }
}
