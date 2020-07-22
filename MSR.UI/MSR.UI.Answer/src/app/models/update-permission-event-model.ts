import { MenuModel } from './menu-model';
import { RoleModel } from './role-model';
import { PermissionModel } from './permission-model';

export class UpdatePermissionsEventModel{
    menuModule: MenuModel;
    roleModule: RoleModel;
    permissionModule?: PermissionModel ;
    event: string;
  }