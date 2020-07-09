import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { UserService, CreateUserRequest, User, IAuditActionResultOfUser, LocationService, Location, UpdateUserRequest, RoleService, Role } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Observable } from 'rxjs';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';

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
  loading: boolean = true;
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
  elems: any;
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
    this.roleService.roleGet(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.returnedObject.map((x) => {
          ctrl.allRoles.push({ label: x.name, value: x.id });
        });
        ctrl.backendRoles = response.returnedObject;
      }));
  }

  getLocations() {
    const ctrl = this;
    if (ctrl.getLocationsFlag) {
      return ctrl.locations;
    }
    this.locationService.locationGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.returnedObject.map((x) => {
          ctrl.locations.push({ label: x.name, value: x.id });
        });
        ctrl.getLocationsFlag = true;
      }));
  }

  async getUsers() {
    const ctrl = this;
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.data = response.object;
        ctrl.data = ctrl.data.map((x) => {
          ctrl.allUsers.push({ label: x.firstName + ' ' + x.lastName, value: x.id });
          x.rolesSaved = x.roles.map(u => u.id);
          const roles = [];
          x.roles.forEach((role) => {
            roles.push(role.name);
            if (ctrl.roles.findIndex(z => z.value === role.name) === -1) {
              ctrl.roles.push({ label: role.name, value: role.name });
            }
          });
          x.rolesStr = roles.join(',');
          return x;
        });


        ctrl.allUsers.sort((a, b) => (a.label > b.label) ? 1 : -1);

        ctrl.loading = false;
      }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege('ApprovalWorkflows', privName);
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
      this.currUser.roles = [];
      this.currUser.rolesSaved.map(x => {
        this.currUser.roles.push(new Role(ctrl.backendRoles.find(r => r.id === x)));
      });

      if (this.currUser.id === undefined) {
        method = this.userService.userPost(env.apiVersion, this.currUser);
      } else {
        let updateUserReq = new UpdateUserRequest();
        Object.assign(updateUserReq, this.currUser);
        method = this.userService.userPatch(env.apiVersion, updateUserReq);
      }

      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currUser.id === undefined) {
            ctrl.data.push(new User(resp.returnedObject));
          }
          ctrl.clseDialog();
        }
      }, () => {
        // DO not update user
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
      return user;
    }
  }

}
