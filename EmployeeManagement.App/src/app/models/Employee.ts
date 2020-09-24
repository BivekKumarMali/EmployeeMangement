import { Department } from './Department';
import { Roles } from './Roles';

export interface Employee {
  Eid: number;
  Name: string;
  Surname: string;
  Address: string;
  Qualification: string;
  ContactNumber: number;
  Did: number;
  UserId: string;
  RoleId: string;

  Roles?: Roles;
  Department?: Department;
}
