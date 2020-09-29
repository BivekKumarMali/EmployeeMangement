import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/core/services/department/department.service';
import { UtilityService } from 'src/app/core/utility/utility.service';
import { Department } from 'src/app/models/Department';

@Component({
  selector: 'app-department',
  templateUrl: './department.component.html',
  styleUrls: ['./department.component.css']
})
export class DepartmentComponent implements OnInit {

  departments: Department[];
  department: Department;
  errorMessage: string;
  CheckAdmin: boolean;
  constructor(
    private departmentService: DepartmentService,
    private utilityService: UtilityService
  ) { }

  ngOnInit(): void {
    this.departmentService.GetAllDepartments().subscribe({
      next: department => this.departments = department,
      error: err => this.errorMessage = err,
    });
    this.department = this.departmentService.ResetDepartment();

    this.CheckAdmin = this.utilityService.JWTCheckRoles(['Admin']);
  }

  AddDepartment(): void {
    this.departmentService.EditandAddDepartment(this.department).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.ngOnInit()
    });
  }

  DeleteDepartment(did: number): void {
    this.departmentService.DeleteDepartment(did).subscribe({
      error: err => this.errorMessage = err,
    });
    location.reload();
  }

  Edit(department: Department) {
    this.department = { ...department };
  }

  ResetDepartment() {
    this.department = this.departmentService.ResetDepartment();
  }
}
