import { Component, OnInit } from '@angular/core';
import { DepartmentService } from 'src/app/core/service/department/department.service';
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
  constructor(private departmentService: DepartmentService) { }

  ngOnInit(): void {
    this.departmentService.GetAllDepartments().subscribe({
      next: department => this.departments = department,
      error: err => this.errorMessage = err,
      complete: () => console.log(this.departments)
    });
    console.log(this.departments);

  }

}
