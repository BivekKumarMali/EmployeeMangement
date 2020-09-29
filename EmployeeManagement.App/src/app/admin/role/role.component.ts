import { Component, OnInit } from '@angular/core';
import { RolesService } from 'src/app/core/services/roles/roles.service';
import { Roles } from 'src/app/models/Roles';

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.css']
})
export class RoleComponent implements OnInit {
  roles: Roles[];
  role: Roles;
  errorMessage: string;

  constructor(
    private roleService: RolesService
  ) { }

  ngOnInit(): void {
    this.roleService.GetAllRoles().subscribe({
      next: role => this.roles = role,
      error: err => this.errorMessage = err
    });
    this.role = this.roleService.ResetRoles();
  }

  AddRoles(): void {
    this.roleService.EditandAddRoles(this.role).subscribe({
      error: err => this.errorMessage = err,
      complete: () => this.ngOnInit()
    });
  }

  DeleteRoles(id: string): void {
    this.roleService.DeleteRoles(id).subscribe({
      error: err => this.errorMessage = err,
    });
    location.reload();
  }

  Edit(role: Roles) {
    this.role = { ...role };
  }

  ResetRoles() {
    this.role = this.roleService.ResetRoles();
  }

}
