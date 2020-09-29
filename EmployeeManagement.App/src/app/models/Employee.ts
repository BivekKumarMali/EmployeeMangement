import { Department } from './Department';
import { Roles } from './Roles';

export interface Employee {
  eid: number;
  name: string;
  surname: string;
  address: string;
  qualification: string;
  contactNumber: number;
  did: number;
  userId: string;
  roleId: string;

  department?: Department;
}
