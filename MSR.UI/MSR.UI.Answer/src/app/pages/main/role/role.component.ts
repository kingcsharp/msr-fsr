import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  EnumMenuItem, EnumApprovalTables,
  RoleService,
  MenuItem, RoleView,
  CreateRoleRequest,
  AuditActionResultOfRoleView,
  UpdateUserRoleRequest,
  UpdateRoleRequest
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
import { copyObj } from '../../../../app/models/lib/Utils';

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
  currentRole: RoleView;
  data: Array<RoleView>;
  availableRoles: Array<RoleView>;
  isCertificationRole: any[];

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

    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.isCertificationRole = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.userPrivileges = this.globals.getEnumPrivileges(EnumMenuItem.Roles);
    this.getRoles();
    this.data = [];

  }

  getRoles() {
    this.roleService.roles(env.apiVersion).subscribe(responseHandler(response => {
      this.globals.showLoader(false);
      this.data = response.object;
    }));
  }

  showDialog(roleView: RoleView) {
    this.currentRole = this.getCurrentRole(roleView);
    this.availableRoles = copyObj(this.data.filter((elem) => { return elem.id !== this.currentRole.id }));
    this.display = true;
  }

  submitRole() {
    const ctrl = this;
    let method: Observable<AuditActionResultOfRoleView> = null;

    let data = {
      id: this.currentRole.id,
      name: this.currentRole.name, isCertificationRole: this.currentRole.isCertificationRole,
      parentRoleIds: this.currentRole.parentRoles.map((role) => { return role.id })
    };

    if (this.currentRole.id !== undefined) {
      let postRoleData = new UpdateRoleRequest(data);
      method = this.roleService.rolePatch(env.apiVersion, postRoleData);
    } else {
      let postRoleData = new CreateRoleRequest(data);
      method = this.roleService.rolePost(env.apiVersion, postRoleData);
    }
    
    method.pipe(take(1)).subscribe(responseHandler((resp) => {
      if (!resp.hasErrors) {
        if (ctrl.currentRole.id === undefined) {
          ctrl.data.push(resp.object);
        } else {
          const index = ctrl.data.findIndex(x => x.id === ctrl.currentRole.id);
          ctrl.data.splice(index, 1);
          ctrl.data.splice(index, 0, resp.object);
        }
        ctrl.clseDialog();
      }
    }, () => {

    }));
  }

  getCurrentRole(role) {
    if (role === undefined) {
      let ret = new RoleView();
      ret.parentRoles = [];
      return ret;
    } else {
      return copyObj(role);
    }
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

}