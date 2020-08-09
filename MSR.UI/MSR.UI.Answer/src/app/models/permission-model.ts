export class PermissionModel {
  id?: number;
  name: string;
  value: boolean = false;
  inheritedPermission: boolean = false;
}
