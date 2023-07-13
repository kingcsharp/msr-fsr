import { MenuItem, Role } from "../services/api.client.generated";
import { MenuModel } from "./menu-model";
import { PermissionModel } from "./permission-model";

export class RoleModel {
  id?: number;
  name: string;
  value: boolean = false;
  inheritedAccessToRole: boolean = false;
  menuItemsChildrenHaveAccessTo: Array<MenuItem> = new Array<MenuItem>();
  childRoles: Array<Role> = new Array<Role>();
  childRoleHasAccessToMenuModule: boolean = false;
  permissions: PermissionModel[] = new Array<PermissionModel>();
}
