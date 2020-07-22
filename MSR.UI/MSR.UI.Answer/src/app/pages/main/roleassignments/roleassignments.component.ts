import { Component, OnInit } from '@angular/core';
import { MenuModel } from '../../../models/menu-model';
import { RoleModel } from '../../../models/role-model';
import { UpdatePermissionsEventModel } from '../../../models/update-permission-event-model';
import { PermissionModel } from '../../../models/permission-model';
import { MenuService,MenuItem, RoleService, Role, Permission, CreateMenuRoleMapRequest, UpdateMenuRoleMapRequest } from '../../../services/api.client.generated';
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

  selectedMenuModule:MenuModel;

  selectedRoleModule:RoleModel;

  updateSuccessful: boolean = false;
  errorUpdatingPermissions:boolean = false;
  pendingPermissionsUpdate:boolean = false;

  pendingPermissions:UpdatePermissionsEventModel[] = new Array<UpdatePermissionsEventModel>();
 
  constructor(private menuService: MenuService, private roleService: RoleService, public globals: Globals) {
  }

  ngOnInit(): void {

    this.getMenuItems();

  }

  getMenuItems(){

    this.menuService.menu(env.apiVersion).subscribe(responseHandler((response) => {

        let menuItems = response.object;
        this.globals.showLoader(true);
        this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

            let roles = response.object;

            this.adaptMenuItemsAndRolesToMenuModels(menuItems, roles);
            this.menuModules = [...this.originalMenuModules];
        }));

    }));

  }

  adaptMenuItemsAndRolesToMenuModels(menuItems: Array<MenuItem>, roles:Array<Role>){

    this.originalMenuModules = new Array<MenuModel>()

    menuItems.forEach(menuItem => {
      let menuModel = new MenuModel();
      menuModel.id = menuItem.id;
      menuModel.name = menuItem.name;

      roles.forEach(role => {

        let roleModel = new RoleModel();
        roleModel.id = role.id;
        roleModel.name = role.name;
        roleModel.value = menuItem.roles.find(s => s.id === role.id) != null ? true : false;

        if(roleModel.value){
          
          let permissions;
          if(menuItem.roles.find(s => s.id === role.id).permissions != null){
            permissions = menuItem.roles.find(s => s.id === role.id).permissions;
          }else{
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
          canActivatePermissionModel.name = "Activate";
          roleModel.permissions.push(canActivatePermissionModel);

          let canApprovePermissionModel = new PermissionModel();
          canApprovePermissionModel.inheritedPermission = inheritedPermissions.canApprove;
          canApprovePermissionModel.value = permissions.canApprove;
          canApprovePermissionModel.name = "Approve";
          roleModel.permissions.push(canApprovePermissionModel);

          let canCreatePermissionModel = new PermissionModel();
          canCreatePermissionModel.inheritedPermission = inheritedPermissions.canCreate;
          canCreatePermissionModel.value = permissions.canCreate;
          canCreatePermissionModel.name = "Create";
          roleModel.permissions.push(canCreatePermissionModel);

          let canDeletePermissionModel = new PermissionModel();
          canDeletePermissionModel.inheritedPermission = inheritedPermissions.canDelete;
          canDeletePermissionModel.value = permissions.canDelete;
          canDeletePermissionModel.name = "Delete";
          roleModel.permissions.push(canDeletePermissionModel);

          let canEditPermissionModel = new PermissionModel();
          canEditPermissionModel.inheritedPermission = inheritedPermissions.canEdit;
          canEditPermissionModel.value = permissions.canEdit;
          canEditPermissionModel.name = "Edit";
          roleModel.permissions.push(canEditPermissionModel);

          let canReadPermissionModel = new PermissionModel();
          canReadPermissionModel.inheritedPermission = inheritedPermissions.canRead;
          canReadPermissionModel.value = permissions.canRead;
          canReadPermissionModel.name = "Read";
          roleModel.permissions.push(canReadPermissionModel);

        }else{

          let canActivatePermissionModel = new PermissionModel();
          canActivatePermissionModel.inheritedPermission = false;
          canActivatePermissionModel.value = false;
          canActivatePermissionModel.name = "Activate";
          roleModel.permissions.push(canActivatePermissionModel);

          let canApprovePermissionModel = new PermissionModel();
          canApprovePermissionModel.inheritedPermission = false;
          canApprovePermissionModel.value = false;
          canApprovePermissionModel.name = "Approve";
          roleModel.permissions.push(canApprovePermissionModel);

          let canCreatePermissionModel = new PermissionModel();
          canCreatePermissionModel.inheritedPermission = false;
          canCreatePermissionModel.value = false;
          canCreatePermissionModel.name = "Create";
          roleModel.permissions.push(canCreatePermissionModel);

          let canDeletePermissionModel = new PermissionModel();
          canDeletePermissionModel.inheritedPermission = false;
          canDeletePermissionModel.value = false;
          canDeletePermissionModel.name = "Delete";
          roleModel.permissions.push(canDeletePermissionModel);

          let canEditPermissionModel = new PermissionModel();
          canEditPermissionModel.inheritedPermission = false;
          canEditPermissionModel.value = false;
          canEditPermissionModel.name = "Edit";
          roleModel.permissions.push(canEditPermissionModel);

          let canReadPermissionModel = new PermissionModel();
          canReadPermissionModel.inheritedPermission = false;
          canReadPermissionModel.value = false;
          canReadPermissionModel.name = "Read";
          roleModel.permissions.push(canReadPermissionModel);

        }
        

        menuModel.roles.push(roleModel);
      });

      
      this.originalMenuModules.push(menuModel);
    });

  }

  selectMenuModule(menuModule:MenuModel){
    this.selectedMenuModule = menuModule;
  }

  editRolePermissions(roleModule:RoleModel){
    this.selectedRoleModule = roleModule;
  }

  roleChanged(event:Event, menuModule:MenuModel, roleModule:RoleModel){
    roleModule.value = !roleModule.value;
    this.pendingPermissionsUpdate = true;
    this.updateSuccessful = false;

    if(roleModule.value){
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:null,
        event:'add'
      } as UpdatePermissionsEventModel);

      this.selectedRoleModule = roleModule;
    }else{
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:null,
        event:'remove'
      } as UpdatePermissionsEventModel);

      this.selectedRoleModule = null;
    }
  }

  permissionChanged(event:Event, menuModule:MenuModel, roleModule:RoleModel, permissionModule:PermissionModel){
    permissionModule.value = !permissionModule.value;
    this.pendingPermissionsUpdate = true;
    this.updateSuccessful = false;

    if(permissionModule.value){
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:permissionModule,
        event:'add'
      } as UpdatePermissionsEventModel);

    }else{
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:permissionModule,
        event:'remove'
      } as UpdatePermissionsEventModel);
    }
  }

  clearPendingChanges(){

    this.pendingPermissions.length = 0;
    this.menuModules = this.originalMenuModules;
    this.selectedMenuModule = null;
    this.selectedRoleModule = null;
    this.updateSuccessful = false;
    this.errorUpdatingPermissions = false;
    this.pendingPermissionsUpdate = false;

  }

  savePendingChanges(){

    this.pendingPermissions.forEach(pendingPermission =>{

      this.globals.showLoader(true);
      if(pendingPermission.permissionModule == null){
        
        if(pendingPermission.event === 'add'){

          let createMenuRoleMapRequest = new CreateMenuRoleMapRequest();
          createMenuRoleMapRequest.menuId = pendingPermission.menuModule.id;
          createMenuRoleMapRequest.roleId = pendingPermission.roleModule.id;
          this.menuService.rolePost(env.apiVersion,createMenuRoleMapRequest).subscribe(responseHandler((response) => {
              console.log(response);
          }));

        }else{

          let createMenuRoleMapRequest = new CreateMenuRoleMapRequest();
          createMenuRoleMapRequest.menuId = pendingPermission.menuModule.id;
          createMenuRoleMapRequest.roleId = pendingPermission.roleModule.id;
          //this.menuService.roleDelete(createMenuRoleMapRequest.roleId,env.apiVersion).subscribe(responseHandler((response) => {
          //    console.log(response);
          //}));

        }

      }else{

          let updateMenuRoleMapRequest = new UpdateMenuRoleMapRequest();
          updateMenuRoleMapRequest.menuRoleId = pendingPermission.menuModule.id;
          updateMenuRoleMapRequest.canActivate = pendingPermission.roleModule.permissions.find(s => s.name === "Activate").value;
          updateMenuRoleMapRequest.canApprove = pendingPermission.roleModule.permissions.find(s => s.name === "Approve").value;
          updateMenuRoleMapRequest.canCreate = pendingPermission.roleModule.permissions.find(s => s.name === "Create").value;
          updateMenuRoleMapRequest.canDelete = pendingPermission.roleModule.permissions.find(s => s.name === "Delete").value;
          updateMenuRoleMapRequest.canEdit = pendingPermission.roleModule.permissions.find(s => s.name === "Edit").value;
          updateMenuRoleMapRequest.canRead = pendingPermission.roleModule.permissions.find(s => s.name === "Read").value;
          this.menuService.rolePatch(env.apiVersion,updateMenuRoleMapRequest).subscribe(responseHandler((response) => {
              console.log(response);
          }));

      }

    });

    this.clearPendingChanges();
    this.updateSuccessful = true;
  }
}