import { RoleModel } from './role-model';

export class MenuModel {
    id?: number;
    name: string;
    roles: RoleModel[] = new Array<RoleModel>();
  }
