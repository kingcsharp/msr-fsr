import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowGroupService, WorkflowGroupModel, WorkflowGroupRoleMapModel, RoleService, UserService, WorkflowGroupUserMapModel,
  Role, AuditActionResultOfWorkflowGroupModel, CreateWorkflowGroupRequest, UpdateWorkflowGroupRequest
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
declare let jQuery: any;

@Component({
  selector: 'app-approval-groups',
  templateUrl: './approval-groups.component.html',
  styleUrls: ['./approval-groups.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ApprovalGroupsComponent implements OnInit {
  privileges = EnumPrivilege;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canAddGroups: boolean = false;
  canActivateGroups: boolean = false;
  canEditGroups: boolean = false;
  display: boolean = false;
  currWorkflowGroup: any;
  backendRoles: Array<Role>;
  data: any;
  statuses: any[];
  users: any[] = [];
  getBackendRoles: boolean = false;

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService, private userService: UserService,
    private elem: ElementRef, private roleService: RoleService, private workflowGroupService: WorkflowGroupService) {

  }

  ngOnInit(): void {
    this.currWorkflowGroup = new WorkflowGroupModel();
    this.gridStorageId = 'workflowGroupGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Approval Group Name', visible: true }),
    new ColumnsSaved({ id: 'groupRoles', label: 'Group Roles', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];
    this.roles = [];

    this.canAddGroups = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivateGroups = this.hasPrivilege(this.privileges.CanActivate);;
    this.canEditGroups = this.hasPrivilege(this.privileges.CanEdit);;
    this.getWorkflowGroups();
    this.getUsers();
    this.getRoles();
  }

  getUsers() {
    const ctrl = this;
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((elem) => {
          ctrl.users.push({ label: elem.firstName + ' ' + elem.lastName, value: elem.id });
        });
      }));
  }

  getWorkflowGroups() {
    // const ctrl = this;
    this.globals.showLoader(true);
    this.workflowGroupService.workflowGroupGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.mapData();
      }));
  }

  mapData() {
    if (this.getBackendRoles) {
      this.data.map((elem) => {
        this.updateRolesUsersSavedForItem(elem);
        this.addToGridRolesDropdown(elem.groupRoles);
        return elem;
      });
    }
    else {
      setTimeout(() => {
        this.mapData();
      }, 100);
    }
  }

  updateRolesUsersSavedForItem(elem: any) {
    const ctrl = this;
    elem.rolesSaved = [];
    elem.usersSaved = [];
    elem.groupRoles.forEach(role => {
      const foundRole = ctrl.backendRoles.find(r => r.id === role.roleId);
      if (foundRole !== undefined) {
        role.name = foundRole.name;
        elem.rolesSaved.push(role.roleId);
      }
    });
    elem.groupUsers.forEach(user => {
      const foundUser = ctrl.users.find(r => r.value === user.id);
      if (foundUser !== undefined) {
        elem.usersSaved.push(foundUser.value);
      }
    });
  }

  addToGridRolesDropdown(groupRoles: any) {
    const ctrl = this;
    groupRoles.forEach(role => {
      if (ctrl.roles.findIndex(z => z.value === role.name) === -1) {
        ctrl.roles.push({ label: role.name, value: role.name });
      }
    });
  }

  getRoles() {
    const ctrl = this;
    this.roleService.role(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.allRoles.push({ label: x.name, value: x.id });
        });
        ctrl.backendRoles = response.object;
        ctrl.getBackendRoles = true;
      }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.ApprovalGroups, privName);
  }

  showDialog(workflowGroup: WorkflowGroupModel) {
    this.display = true;
    this.currWorkflowGroup = this.getWorkflowGroup(workflowGroup);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getWorkflowGroup(workflowGroup: WorkflowGroupModel) {
    if (workflowGroup === undefined) {
      let ret = new WorkflowGroupModel();
      ret.name = '';
      ret.isActive = true;
      return ret;
    } else {
      return workflowGroup;
    }
  }

  workflowGroupStatus(workflowGroup: WorkflowGroupModel) {
    const ctrl = this;
    const updateWorkflow = new UpdateWorkflowGroupRequest({
      id: workflowGroup.id, name: workflowGroup.name,
      roles: workflowGroup.groupRoles, isActive: workflowGroup.isActive
    });
    this.globals.showLoader(true);
    this.workflowGroupService.workflowGroupPatch(env.apiVersion, updateWorkflow).pipe(take(1)).subscribe(responseHandler((resp) => {
      if (resp.hasErrors) {
        workflowGroup.isActive = !workflowGroup.isActive;
      }
    }, () => {
      workflowGroup.isActive = !workflowGroup.isActive;
    }));
  }

  removeRow(workflowGroup) {
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowGroupService.workflowGroupDelete(workflowGroup.id, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler((resp) => {
        const index = this.data.findIndex(x => x.id === workflowGroup.id);
        this.data.splice(index, 1);
      }, () => {
        // DO not update user
      }));
  }
  //onWorkflowSubmit
  onWorkflowSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method: Observable<AuditActionResultOfWorkflowGroupModel> = null;
      this.globals.showLoader(true);
      this.currWorkflowGroup.groupRoles = [];
      this.currWorkflowGroup.groupUsers = [];
      this.currWorkflowGroup.rolesSaved.forEach(x => {
        const role = ctrl.backendRoles.find(r => r.id === x);
        this.currWorkflowGroup.groupRoles
          .push(new WorkflowGroupRoleMapModel({ roleId: x, name: role.name, workflowGroupId: this.currWorkflowGroup.id }));
      });

      this.currWorkflowGroup.usersSaved.forEach(x => {
        this.currWorkflowGroup.groupUsers
          .push(new WorkflowGroupUserMapModel({ userId: x, workflowGroupId: this.currWorkflowGroup.id }));
      });

      if (this.currWorkflowGroup.id === undefined) {
        const createWorkflow = new CreateWorkflowGroupRequest({
          name: this.currWorkflowGroup.name, users: this.currWorkflowGroup.groupUsers,
          roles: this.currWorkflowGroup.groupRoles, isActive: this.currWorkflowGroup.isActive
        });
        method = this.workflowGroupService.workflowGroupPost(env.apiVersion, createWorkflow);
      } else {
        const updateWorkflow = new UpdateWorkflowGroupRequest({
          id: this.currWorkflowGroup.id,
          name: this.currWorkflowGroup.name, roles: this.currWorkflowGroup.groupRoles,
          isActive: this.currWorkflowGroup.isActive, users: this.currWorkflowGroup.groupUsers
        });
        method = this.workflowGroupService.workflowGroupPatch(env.apiVersion, updateWorkflow);
      }
      this.globals.showLoader(true);
      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currWorkflowGroup.id === undefined) {
            this.updateRolesUsersSavedForItem(resp.object);
            this.addToGridRolesDropdown(resp.object.groupRoles);
            ctrl.data.push(resp.object);
          }
          ctrl.clseDialog();
        }
      }, () => {
        // DO not update user
      }));
    }
  }

}
