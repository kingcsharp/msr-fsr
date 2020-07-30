import { PermissionModel } from './permission-model';

export class RoleModel{
    id?: number;
    name: string;
    value:boolean = false;
    permissions: PermissionModel[] = new Array<PermissionModel>();
  }
