import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import {
  UserService, User, IAuditActionResultOfUser, LocationService
  , UpdateUserRequest, RoleService, Role
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Observable } from 'rxjs';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../../models/lib/Utils';

declare let jQuery: any;

@Component({
  selector: 'user',
  templateUrl: './user.template.html',
  styleUrls: ['./user.style.scss'],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class UserComponent implements OnInit {
  privileges = EnumPrivilege;
  data: any;
  display: boolean = false;
  currUser: any;
  phoneValue = '';
  statuses: any[];
  roles: any[];
  allRoles: any[] = [];
  allUsers: any[];
  userTypes: any[];
  canAddUsers: boolean = false;
  canEditUsers: boolean = false;
  showSaveView: boolean = false;
  savedViewsOptions: any;
  phoneMask = {
    mask: ['(', /[1-9]/, /\d/, /\d/, ')',
      ' ', /\d/, /\d/, /\d/,
      '-', /\d/, /\d/, /\d/, /\d/]
  };
  canActivate: boolean;
  viewsSaved: Array<ViewSaved>;
  viewToSave: ViewSaved;
  controllerName: string;
  gridVersion: string; // IF YOU ADD COLUMNS OR EDIT DATA TYPE YOU NEED TO UPGRADE THIS VERSION
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  selectedColumns: any;
  columnPicker: any;
  columnDropdown: boolean = false;
  gridOptionsRotate: boolean = false;
  locations: any[] = [];
  getLocationsFlag: boolean = false;
  backendRoles: Array<Role>;

  constructor(public userService: UserService, public cg: CommonGrid, private toastr: ToastrService,
    public globals: Globals, private elem: ElementRef, public locationService: LocationService, public roleService: RoleService) {
  }

  ngOnInit(): void {
    this.data = [];
    this.currUser = new User();
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'userGrid' + this.elem.nativeElement.tagName.toLowerCase();
    // SET DEFAULT VIEW COLS
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
    new ColumnsSaved({ id: 'isAnswerUser', label: 'User Type', visible: true }),
    new ColumnsSaved({ id: 'firstName', label: 'First Name', visible: true }),
    new ColumnsSaved({ id: 'lastName', label: 'Last Name', visible: true }),
    new ColumnsSaved({ id: 'userName', label: 'Username', visible: true }),
    new ColumnsSaved({ id: 'email', label: 'Email', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'roles', label: 'Roles', visible: false }),
    new ColumnsSaved({ id: 'locationName', label: 'Location', visible: false }),
    new ColumnsSaved({ id: 'supervisorName', label: 'Supervisor', visible: false })
    ];

    this.roles = [];
    this.allUsers = [];

    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];
    this.userTypes = [
      { label: 'Is Portal User', value: true },
      { label: 'Is Not Portal User', value: false },
    ];

    this.canAddUsers = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivate = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditUsers = this.hasPrivilege(this.privileges.CanEdit);
    this.getUsers();
    this.getLocations();
    this.getRoles();
  }

  getRoles() {
    const ctrl = this;
    this.roleService.role(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.allRoles = response.object;
        ctrl.backendRoles = response.object;
      }));
  }

  getLocations() {
    const ctrl = this;
    if (ctrl.getLocationsFlag) {
      return ctrl.locations;
    }
    this.locationService.locationGet(null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.locations.push({ label: x.name, value: x.id });
        });
        ctrl.getLocationsFlag = true;
      }));
  }

  async getUsers() {
    const ctrl = this;
    this.globals.showLoader(true);
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
        this.updateUsersData(this.data);
      }));
  }

  updateUsersData(usersData) {
    const ctrl = this;
    usersData.forEach((x)  => {
      const userIndex = ctrl.allUsers.findIndex(z => z.value === x.id);
      if (userIndex < 0) {
        ctrl.allUsers.push({ label: x.firstName + ' ' + x.lastName, value: x.id });
      }
    });
    ctrl.allUsers.sort((a, b) => (a.label > b.label) ? 1 : -1);
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Users, privName);
  }

  unmask(event) {
    return event.replace(/\D+/g, '');
  }

  showDialog(user: User) {
    this.display = true;
    this.currUser = this.getUser(user);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  changeUserStatus(user: User) {
    const ctrl = this;
    this.userService.userDelete(user.customerId, env.apiVersion).pipe(take(1)).subscribe(responseHandler(() => {
      ctrl.toastr.success(`User has been successfully ${user.isActive ? 'activated' : 'deactivated'}!`);
    }, () => {
      user.isActive = !user.isActive;
    })
    );
  }

  changeUserType(user: UpdateUserRequest) {
    const ctrl = this;

    this.userService.userPatch(env.apiVersion, user).pipe(take(1)).subscribe(responseHandler((resp) => {
      if (!resp.hasErrors) {
        if (user.isAnswerUser) {
          ctrl.toastr.success('User type changed to Is Answer User!');
        } else {
          ctrl.toastr.success('User type changed to Not Answer User!');
        }
      }
    }, () => {
      user.isAnswerUser = !user.isAnswerUser;
    }));
  }

  onUserSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method: Observable<IAuditActionResultOfUser> = null;
      this.globals.showLoader(true);

      if (this.currUser.id === undefined) {
        method = this.userService.userPost(env.apiVersion, this.currUser);
      } else {
        let updateUserReq = new UpdateUserRequest();
        Object.assign(updateUserReq, this.currUser);
        method = this.userService.userPatch(env.apiVersion, updateUserReq);
      }

      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currUser.id !== undefined) {
            const index = ctrl.data.findIndex(x => x.id === resp.object.id);
            ctrl.data.splice(index, 1);
          }

          ctrl.data.push(new User(resp.object));
          this.updateUsersData(ctrl.data);
          ctrl.clseDialog();
        }
      }));
    }
  }

  getUser(user: User) {
    if (user === undefined) {
      let ret = new User();
      ret.isActive = true;
      ret.isAnswerUser = true;
      ret.firstName = '';
      return ret;
    } else {
      return copyObj(user);
    }
  }

}
