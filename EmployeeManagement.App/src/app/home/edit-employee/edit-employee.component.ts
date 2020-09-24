import { Component, OnInit } from '@angular/core';
import { Department } from 'src/app/models/Department';
import { Employee } from 'src/app/models/employee';
import { Roles } from 'src/app/models/Roles';

@Component({
  selector: 'app-edit-employee',
  templateUrl: './edit-employee.component.html',
  styleUrls: ['./edit-employee.component.css']
})
export class EditEmployeeComponent implements OnInit {

  Employee: Employee = {
    Address: '',
    ContactNumber: null,
    Did: 0,
    Eid: 0,
    Name: '',
    Qualification: '',
    RoleId: '',
    Surname: '',
    UserId: ''
  };
  Departments: Department[];
  Roles: Roles[];
  constructor() { }

  ngOnInit(): void {
  }

}
