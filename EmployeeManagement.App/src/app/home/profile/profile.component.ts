import { Component, OnInit } from '@angular/core';
import { Employee } from 'src/app/models/employee';

@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.css']
})
export class ProfileComponent implements OnInit {

  Employee: Employee;
  Edit = false;
  constructor() { }

  ngOnInit(): void {
  }

  EnableEdit() {
    this.Edit = this.Edit ? false : true;
  }
}
