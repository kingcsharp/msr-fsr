import { Component, OnInit } from '@angular/core';
import { MenuModel } from '../../../models/menu-model';
import { RoleModel } from '../../../models/role-model';
import { UpdatePermissionsEventModel } from '../../../models/update-permission-event-model';
import { PermissionModel } from '../../../models/permission-model';
import { MenuService, MenuItem, RoleService, Role, Permission, CreateMenuRoleMapRequest, UpdateMenuRoleMapRequest, ICreateMenuRoleMapRequest, IUpdateProcedureStepRequest, IUpdateMenuRoleMapRequest } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-roleassignments',
  templateUrl: './roleassignments.template.html',
  styleUrls: ['./roleassignments.style.scss'],
  providers: [MenuService, RoleService]
})
export class RoleassignmentsComponent implements OnInit {

  menuModules: MenuModel[];
  originalMenuModules: MenuModel[];

  selectedMenuModule: MenuModel;

  selectedRoleModule: RoleModel;

  errorUpdatingPermissions: boolean = false;
  pendingPermissionsUpdate: boolean = false;

  pendingPermissions: UpdatePermissionsEventModel[] = new Array<UpdatePermissionsEventModel>();

  constructor(private menuService: MenuService, private roleService: RoleService, public globals: Globals) {
  }

  ngOnInit(): void {

    this.getMenuItems();

  }

  getMenuItems() {
    this.globals.showLoader(true);
    this.menuService.menu(env.apiVersion).subscribe(responseHandler((response) => {

      let menuItems = response.object;
      this.globals.showLoader(true);
      this.roleService.roleGet(env.apiVersion).subscribe(responseHandler((roleResponse) => {

        let roles = roleResponse.object;

        this.adaptMenuItemsAndRolesToMenuModels(menuItems, roles);
        this.menuModules = [...this.originalMenuModules];
      }));

    }));

  }

  adaptMenuItemsAndRolesToMenuModels(menuItems: Array<MenuItem>, roles: Array<Role>) {

    this.originalMenuModules = new Array<MenuModel>();

    menuItems.forEach(menuItem => {
      let menuModel = new MenuModel();
      menuModel.id = menuItem.id;
      menuModel.name = menuItem.name;

      roles.forEach(role => {

        let roleModel = new RoleModel();
        roleModel.id = role.id;
        roleModel.name = role.name;
        roleModel.value = menuItem.roles.find(s => s.id === role.id) != null ? true : false;

        if (roleModel.value) {

          let permissions;
          if (menuItem.roles.find(s => s.id === role.id).permissions != null) {
            permissions = menuItem.roles.find(s => s.id === role.id).permissions;
          } else {
            permissions = new Permission();
            permissions.canRead = false;
            permissions.canCreate = false;
            permissions.canEdit = false;
            permissions.canActivate = false;
            permissions.canApprove = false;
            permissions.canDelete = false;
          }
          let inheritedPermissions = menuItem.roles.find(s => s.id === role.id).inheritedPermissions;


          let canActivatePermissionModel = new PermissionModel();
          canActivatePermissionModel.inheritedPermission = inheritedPermissions.canActivate;
          canActivatePermissionModel.value = permissions.canActivate;
          canActivatePermissionModel.name = 'Activate';
          roleModel.permissions.push(canActivatePermissionModel);

          let canApprovePermissionModel = new PermissionModel();
          canApprovePermissionModel.inheritedPermission = inheritedPermissions.canApprove;
          canApprovePermissionModel.value = permissions.canApprove;
          canApprovePermissionModel.name = 'Approve';
          roleModel.permissions.push(canApprovePermissionModel);

          let canCreatePermissionModel = new PermissionModel();
          canCreatePermissionModel.inheritedPermission = inheritedPermissions.canCreate;
          canCreatePermissionModel.value = permissions.canCreate;
          canCreatePermissionModel.name = 'Create';
          roleModel.permissions.push(canCreatePermissionModel);

          let canDeletePermissionModel = new PermissionModel();
          canDeletePermissionModel.inheritedPermission = inheritedPermissions.canDelete;
          canDeletePermissionModel.value = permissions.canDelete;
          canDeletePermissionModel.name = 'Delete';
          roleModel.permissions.push(canDeletePermissionModel);

          let canEditPermissionModel = new PermissionModel();
          canEditPermissionModel.inheritedPermission = inheritedPermissions.canEdit;
          canEditPermissionModel.value = permissions.canEdit;
          canEditPermissionModel.name = 'Edit';
          roleModel.permissions.push(canEditPermissionModel);

          let canReadPermissionModel = new PermissionModel();
          canReadPermissionModel.inheritedPermission = inheritedPermissions.canRead;
          canReadPermissionModel.value = permissions.canRead;
          canReadPermissionModel.name = 'Read';
          roleModel.permissions.push(canReadPermissionModel);

        } else {

          let canActivatePermissionModel = new PermissionModel();
          canActivatePermissionModel.inheritedPermission = false;
          canActivatePermissionModel.value = false;
          canActivatePermissionModel.name = 'Activate';
          roleModel.permissions.push(canActivatePermissionModel);

          let canApprovePermissionModel = new PermissionModel();
          canApprovePermissionModel.inheritedPermission = false;
          canApprovePermissionModel.value = false;
          canApprovePermissionModel.name = 'Approve';
          roleModel.permissions.push(canApprovePermissionModel);

          let canCreatePermissionModel = new PermissionModel();
          canCreatePermissionModel.inheritedPermission = false;
          canCreatePermissionModel.value = false;
          canCreatePermissionModel.name = 'Create';
          roleModel.permissions.push(canCreatePermissionModel);

          let canDeletePermissionModel = new PermissionModel();
          canDeletePermissionModel.inheritedPermission = false;
          canDeletePermissionModel.value = false;
          canDeletePermissionModel.name = 'Delete';
          roleModel.permissions.push(canDeletePermissionModel);

          let canEditPermissionModel = new PermissionModel();
          canEditPermissionModel.inheritedPermission = false;
          canEditPermissionModel.value = false;
          canEditPermissionModel.name = 'Edit';
          roleModel.permissions.push(canEditPermissionModel);

          let canReadPermissionModel = new PermissionModel();
          canReadPermissionModel.inheritedPermission = false;
          canReadPermissionModel.value = false;
          canReadPermissionModel.name = 'Read';
          roleModel.permissions.push(canReadPermissionModel);

        }


        menuModel.roles.push(roleModel);
      });


      this.originalMenuModules.push(menuModel);
    });

  }

  selectMenuModule(menuModule: MenuModel) {
    this.selectedMenuModule = menuModule;
  }

  editRolePermissions(roleModule: RoleModel) {
    this.selectedRoleModule = roleModule;
  }

  roleChanged(event: Event, menuModule: MenuModel, roleModule: RoleModel) {
    roleModule.value = !roleModule.value;
    this.pendingPermissionsUpdate = true;


      if (roleModule.value) {

        this.addRoleAndPermissions(menuModule, roleModule, null);

        this.selectedRoleModule = roleModule;
      } else {

        this.removeRoleAndPermissions(menuModule, roleModule, null);

        this.selectedRoleModule = roleModule;
      }

  }

  permissionChanged(event: Event, menuModule: MenuModel, roleModule: RoleModel, permissionModule: PermissionModel) {
    permissionModule.value = !permissionModule.value;
    this.pendingPermissionsUpdate = true;

    let pendingPermissionChangeIndex = this.pendingPermissions.findIndex(s => s.menuModule.id === menuModule.id
      && s.roleModule.id === roleModule.id
      && s.permissionModule !== null
      && s.permissionModule?.name === permissionModule.name);

    if (pendingPermissionChangeIndex === -1) {

      if (permissionModule.value) {
        this.pendingPermissions.push({
          menuModule: menuModule,
          roleModule: roleModule,
          permissionModule: permissionModule,
          event: 'add'
        } as UpdatePermissionsEventModel);

      } else {
        this.pendingPermissions.push({
          menuModule: menuModule,
          roleModule: roleModule,
          permissionModule: permissionModule,
          event: 'remove'
        } as UpdatePermissionsEventModel);
      }


    } else {

      this.pendingPermissions.splice(pendingPermissionChangeIndex, 1);

    }

  }

  clearPendingChanges() {

    this.pendingPermissions.length = 0;
    this.menuModules = this.originalMenuModules;
    this.selectedMenuModule = null;
    this.selectedRoleModule = null;
    this.errorUpdatingPermissions = false;
    this.pendingPermissionsUpdate = false;

  }

  generateUniqueUpdateRoleModelRequests(permissionChanges): Array<UpdateMenuRoleMapRequest> {

    let uniquePermissionChanges = new Array<UpdateMenuRoleMapRequest>();

    permissionChanges.map(permissionChange => {

      let uniquePermissionChangeIndex = uniquePermissionChanges.findIndex(s => s.menuId === permissionChange.menuModule.id && s.roleId === permissionChange.roleModule.id);

      if (uniquePermissionChangeIndex === -1) {

        let updateMenuRoleMapRequest = new UpdateMenuRoleMapRequest({
          menuId: permissionChange.menuModule.id,
          roleId: permissionChange.roleModule.id,
          canActivate: permissionChange.roleModule.permissions.find(s => s.name === 'Activate').value,
          canApprove: permissionChange.roleModule.permissions.find(s => s.name === 'Approve').value,
          canCreate: permissionChange.roleModule.permissions.find(s => s.name === 'Create').value,
          canDelete: permissionChange.roleModule.permissions.find(s => s.name === 'Delete').value,
          canEdit: permissionChange.roleModule.permissions.find(s => s.name === 'Edit').value,
          canRead: permissionChange.roleModule.permissions.find(s => s.name === 'Read').value
        } as IUpdateMenuRoleMapRequest);

        uniquePermissionChanges.push(updateMenuRoleMapRequest);

      }

    });

    return uniquePermissionChanges;

  }

  savePendingChanges() {

    let roleChanges = this.pendingPermissions.filter(s => s.permissionModule === null);
    let permissionChanges = this.pendingPermissions.filter(s => s.permissionModule !== null);

    let uniqueUpdateRoleModelRequests = this.generateUniqueUpdateRoleModelRequests(permissionChanges);


    roleChanges.map(roleChange => {

      if (roleChange.event === 'add') {

        let createMenuRoleMapRequest = new CreateMenuRoleMapRequest({
          menuId: roleChange.menuModule.id,
          roleId: roleChange.roleModule.id
        } as ICreateMenuRoleMapRequest);

        this.menuService.rolePost(env.apiVersion, createMenuRoleMapRequest).subscribe(responseHandler((response) => {

        }));

      } else {

        this.menuService.roleDelete(roleChange.menuModule.id, roleChange.roleModule.id, env.apiVersion).subscribe(responseHandler((response) => {

        }));

      }

    });


    uniqueUpdateRoleModelRequests.map(uniquePermissionChange => {
      this.menuService.rolePatch(env.apiVersion, uniquePermissionChange).subscribe(responseHandler(() => {

      }));
    });

    this.clearPendingChanges();
  }

  removeRoleAndPermissions(menuModule: MenuModel, roleModule: RoleModel, permissionModule: PermissionModel){

    this.pendingPermissions = this.pendingPermissions.filter(s => s.menuModule.id !== menuModule.id && s.roleModule.id !== roleModule.id);

    this.pendingPermissions.push({
      menuModule: menuModule,
      roleModule: roleModule,
      permissionModule: null,
      event: 'remove'
    } as UpdatePermissionsEventModel);

    roleModule.permissions.map(permission => {
      permission.value = false;
    });

  }

  addRoleAndPermissions(menuModule: MenuModel, roleModule: RoleModel, permissionModule: PermissionModel){

    this.pendingPermissions = this.pendingPermissions.filter(s => s.menuModule.id !== menuModule.id && s.roleModule.id !== roleModule.id);

    this.pendingPermissions.push({
      menuModule: menuModule,
      roleModule: roleModule,
      permissionModule: null,
      event: 'add'
    } as UpdatePermissionsEventModel);

    roleModule.permissions.map(permission => {
      permission.value = false;
      this.permissionChanged(null, menuModule, roleModule, permission);
    });
  }
}
