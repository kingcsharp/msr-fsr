import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-roleassignments',
  templateUrl: './roleassignments.template.html',
  styleUrls: ['./roleassignments.style.scss']
})
export class RoleassignmentsComponent implements OnInit {

  menuModules: MenuModule[];
  originalMenuModules: MenuModule[];

  selectedMenuModule:MenuModule;

  selectedRoleModule:RoleModule;

  updateSuccessful: boolean = false;
  errorUpdatingPermissions:boolean = false;
  pendingPermissionsUpdate:boolean = false;

  pendingPermissions:UpdatePermissionsEvent[] = new Array<UpdatePermissionsEvent>();
 
  constructor() {

    this.originalMenuModules = this.generateMockData();
    this.menuModules = [...this.originalMenuModules];
  }

  ngOnInit(): void {
  }

  //TODO: There are here just for development
  generateMockData(): MenuModule[] {

    let permissionModules = new Array<PermissionModule>();
    permissionModules.push({
      id:1,
      name: 'Read',
      value:true,
      inheritedPermission:false
    } as PermissionModule)
    permissionModules.push({
      id:2,
      name: 'Write',
      value:true,
      inheritedPermission:false
    } as PermissionModule)
    permissionModules.push({
      id:3,
      name: 'Approve',
      value:true,
      inheritedPermission:false
    } as PermissionModule)

    let inheritedPermissionModules = new Array<PermissionModule>();
    inheritedPermissionModules.push({
      id:1,
      name: 'Read',
      value:true,
      inheritedPermission:true
    } as PermissionModule)
    inheritedPermissionModules.push({
      id:2,
      name: 'Write',
      value:true,
      inheritedPermission:true
    } as PermissionModule)
    inheritedPermissionModules.push({
      id:3,
      name: 'Approve',
      value:false,
      inheritedPermission:false
    } as PermissionModule)

    let roleModules = new Array<RoleModule>();
    roleModules.push({
      id:1,
      name: "Admin",
      value:true,
      permissions:permissionModules
    } as RoleModule);
    roleModules.push({
      id:2,
      name: "CEO",
      value:false,
      permissions:inheritedPermissionModules
    } as RoleModule);

    let menuModules = new Array<MenuModule>();
    menuModules.push({
      id:1,
      name:"People",
      roles:roleModules,
    } as MenuModule);
    menuModules.push({
      id:2,
      name:"WIP Menu",
      roles:roleModules,
    } as MenuModule);


    return menuModules;
  }

  selectMenuModule(menuModule:MenuModule){
    this.selectedMenuModule = menuModule;
  }

  editRolePermissions(roleModule:RoleModule){
    this.selectedRoleModule = roleModule;
  }

  roleChanged(event:Event, menuModule:MenuModule, roleModule:RoleModule){
    roleModule.value = !roleModule.value;
    this.pendingPermissionsUpdate = true;
    this.updateSuccessful = false;

    if(roleModule.value){
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:null,
        event:'add'
      } as UpdatePermissionsEvent);

      this.selectedRoleModule = roleModule;
    }else{
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:null,
        event:'remove'
      } as UpdatePermissionsEvent);

      this.selectedRoleModule = null;
    }
  }

  permissionChanged(event:Event, menuModule:MenuModule, roleModule:RoleModule, permissionModule:PermissionModule){
    permissionModule.value = !permissionModule.value;
    this.pendingPermissionsUpdate = true;
    this.updateSuccessful = false;

    if(permissionModule.value){
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:permissionModule,
        event:'add'
      } as UpdatePermissionsEvent);

    }else{
      this.pendingPermissions.push({
        menuModule:menuModule,
        roleModule:roleModule,
        permissionModule:permissionModule,
        event:'remove'
      } as UpdatePermissionsEvent);
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

    this.clearPendingChanges();
    this.updateSuccessful = true;
  }
}
//TODO: There are here just for development and can possibly be moved to the models folder or removed based on the web api
export class UpdatePermissionsEvent{
  menuModule: MenuModule;
  roleModule: RoleModule;
  permissionModule?: PermissionModule ;
  event: string;
}

export class MenuModule {
  id?: number;
  name: string;
  roles: RoleModule[] = new Array<RoleModule>();
}

export class RoleModule{
  id?: number;
  name: string;
  value:boolean = false;
  permissions: PermissionModule[] = new Array<PermissionModule>();
}

export class PermissionModule{
  id?: number;
  name: string;
  value:boolean = false;
  inheritedPermission: boolean = false;
}
