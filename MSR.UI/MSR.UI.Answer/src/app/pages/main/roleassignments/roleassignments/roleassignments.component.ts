import { Component, OnInit } from '@angular/core';
import { ListboxModule } from 'primeng/listbox';
import { SelectItem } from 'primeng/api/selectitem';
import { ButtonModule } from 'primeng/button';

@Component({
  selector: 'app-roleassignments',
  templateUrl: './roleassignments.template.html',
  styleUrls: ['./roleassignments.style.scss']
})
export class RoleassignmentsComponent implements OnInit {

  menuModules: MenuModule[];

  selectedMenuModule:MenuModule;

  selectedRoleModule:RoleModule;

  updateSuccessful: boolean = false;
  errorUpdatingPermissions:boolean = false;
  pendingPermissionsUpdate:boolean = true;

  pendingPermissions:object[] = new Array();
 
  constructor() {

    this.menuModules = this.generateMockData();
    
    console.log(this.menuModules);
  }

  ngOnInit(): void {
  }

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

  roleChanged(event:Event, roleModule:RoleModule){
    roleModule.value = !roleModule.value;
    console.log(roleModule);
  }

  permissionChanged(event:Event, permissionModule:PermissionModule){
    permissionModule.value = !permissionModule.value;
    console.log(permissionModule);
  }

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
