import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  EnumMenuItem, EnumApprovalTables,
  RoleService,
  MenuItem, Role,
  CreateRoleRequest,
  AuditActionResultOfRole,
  UpdateUserRoleRequest, UserRoleModel,
  UpdateRoleRequest, RolesUsersView, ReportModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { AllowedActions } from '../../../../app/models/lib/AllowedActions';
import { copyObj, pushIfNotExists, emptyArray } from '../../../../app/models/lib/Utils';
import { GridSaved } from '../../../../app/models/lib/GridSaved';
import { EnumColumnType } from '../../../../app/models/enums/EnumColumnType';
import * as moment from 'moment';

declare let jQuery: any;

@Component({
  selector: 'app-role',
  templateUrl: './role.component.html',
  styleUrls: ['./role.component.scss']
})
export class RoleComponent implements OnInit {
  userPrivileges: AllowedActions;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  display: boolean = false;
  currentRole: Role;
  data: Array<Role>;
  availableRoles: Array<Role>;
  isCertificationRole: any[];
  rolesUsers: RolesUsersView[];
  roleUsers: RolesUsersView[] = [];
  showGrid: boolean = false;

  roleUsersPopupGrid: GridSaved;
  roleUsersPopupModel: ReportModel;

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private roleService: RoleService) {
  }

  ngOnInit(): void {
    this.currentRole = this.getCurrentRole(undefined);

    this.gridStorageId = 'rolesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
    new ColumnsSaved({ id: 'isCertificationRole', label: 'Is Certification Role', visible: true }),
    new ColumnsSaved({ id: 'parentRoles', label: 'Parent Roles', visible: true }),
    new ColumnsSaved({ id: 'assignedUsers', label: 'Assigned Users', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.isCertificationRole = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.userPrivileges = this.globals.getEnumPrivileges(EnumMenuItem.Roles);

    this.roleUsersPopupGrid = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({ id: 'fullName', label: 'Name', type: EnumColumnType.String, visible: true, styles: { 'width': '23rem' } }),
        new ColumnsSaved({ id: 'certificationFromDate', label: 'Issue Date', visible: true, type: EnumColumnType.InputDateTime, styles: { 'width': '10rem' } }),
        new ColumnsSaved({ id: 'certificationToDate', label: 'Expiration Date', visible: true, type: EnumColumnType.InputDateTime, styles: { 'width': '10rem' } }),
      ],
      gridClass: 'formTbl',
      showMyViewsFeature: false,
      paginator: false,
      storageId: 'roleUsersPopupGrid',
      version: '1.0.0'
    });

    this.roleUsersPopupModel = new ReportModel({
      name: ''
    });

    this.getRolesUsers();
    this.data = [];
  }

  getRoles() {
    this.globals.showLoader(true);
    this.roleService.roleGet(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.setAssignedUsers(response.object);
        this.showGrid = true;
      }));
  }

  setAssignedUsers(data) {
    this.data = data.map(x => {
      x.assignedUsers = this.getRoleUsersByRoleId(x.id);
      return x;
    });
  }

  getRolesUsers() {
    this.roleService.rolesUsers(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.rolesUsers = response.object.map(x => {
          x.fullName = x.user.fullName;
          return x;
        });
        this.getRoles();
      }));
  }

  getRoleUsersByRoleId(roleId) {
    const assignedUsers = [];
    this.rolesUsers.forEach(x => {
      if (x.roleId === roleId) {
        pushIfNotExists(x, assignedUsers, 'userId');
      }
    });
    return assignedUsers;
  }

  showDialog(roleView: Role) {
    this.currentRole = this.getCurrentRole(roleView);
    this.availableRoles = this.data.filter((elem) => elem.id !== this.currentRole.id);
    if (roleView?.id) {
      this.roleUsers = this.getRoleUsersByRoleId(roleView.id);
    }

    this.display = true;
  }

  submitRole() {
    const ctrl = this;
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      let method: Observable<AuditActionResultOfRole> = null;


      let data = {
        id: this.currentRole.id,
        name: this.currentRole.name, isCertificationRole: this.currentRole.isCertificationRole,
        parentRoleIds: this.currentRole.parentRoles.map((role) => role.id),
        userRoles: []
      };

      if (this.currentRole.id !== undefined) {
        if (data.isCertificationRole) {
          data.userRoles = [];
          this.roleUsers.forEach((x: any) => {
            let userRoleModel = new UserRoleModel(x);
            userRoleModel.userRoleId = x.id;
            if (userRoleModel.certificationFromDate !== undefined) {
              userRoleModel.certificationFromDate = moment(userRoleModel.certificationFromDate, 'MM/DD/YYYY').toDate();
            }
            if (userRoleModel.certificationToDate !== undefined) {
              userRoleModel.certificationToDate = moment(userRoleModel.certificationToDate, 'MM/DD/YYYY').toDate();
            }
            data.userRoles.push(userRoleModel);
          });
        }

        let postRoleData = new UpdateRoleRequest(data);
        method = this.roleService.rolePatch(env.apiVersion, postRoleData);
      } else {
        let postRoleData = new CreateRoleRequest(data);
        method = this.roleService.rolePost(env.apiVersion, postRoleData);
      }

      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          resp.object.parentRoles = this.currentRole.parentRoles;
          resp.object.assignedUsers = this.getRoleUsersByRoleId(this.currentRole.id);
          if (ctrl.currentRole.id === undefined) {
            ctrl.data.push(resp.object);
            this.data = this.data.slice(0);
          } else {
            const index = ctrl.data.findIndex(x => x.id === ctrl.currentRole.id);
            ctrl.data.splice(index, 1);
            ctrl.data.splice(index, 0, resp.object);
            ctrl.data = ctrl.data.slice(0);
          }
          ctrl.clseDialog();
        }
      }, () => {

      }));
    }
  }

  getCurrentRole(role) {
    if (role === undefined) {
      let ret = new Role();
      ret.parentRoles = [];
      return ret;
    } else {
      return copyObj(role);
    }
  }

  removeRow(role: Role) {
    this.globals.showLoader(true);
    this.roleService.roleDelete(role.id, env.apiVersion).pipe(take(1)).subscribe(responseHandler((resp) => {
      if (!resp.hasErrors) {
        const index = this.data.findIndex(x => x.id === role.id);
        this.data.splice(index, 1);
        this.data = this.data.slice(0);
        this.clseDialog();
      }
    }, () => {

    }));
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

}
