import { Component, OnInit, ViewEncapsulation, Injector, ElementRef } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { UserService, CreateUserRequest, User, IAuditActionResultOfUser } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Observable } from 'rxjs';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';

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
  config: any;
  data: any;
  loading: boolean = true;
  display: boolean = false;
  currUser: User;
  injector: Injector;
  phoneValue = '';
  statuses: any[];
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
  gridVersion: string; //IF YOU ADD COLUMNS OR EDIT DATA TYPE YOU NEED TO UPGRADE THIS VERSION
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  selectedColumns: any;
  columnPicker: any;
  columnDropdown: boolean = false;
  gridOptionsRotate: boolean = false;
  constructor(public userService: UserService, injector: Injector, private toastr: ToastrService,
    public globals: Globals, private elem: ElementRef) {
  }

  ngOnInit(): void {
    this.currUser = new User();
    this.gridVersion = "1.0.0";
    this.gridStorageId = 'userGrid' + this.elem.nativeElement.tagName.toLowerCase();
    //SET DEFAULT VIEW COLS
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
    new ColumnsSaved({ id: 'isAnswerUser', label: 'User Type', visible: true }),
    new ColumnsSaved({ id: 'firstName', label: 'First Name', visible: true }),
    new ColumnsSaved({ id: 'lastName', label: 'Last Name', visible: true }),
    new ColumnsSaved({ id: 'userName', label: 'Username', visible: true }),
    new ColumnsSaved({ id: 'email', label: 'Email', visible: true })];


    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];
    this.userTypes = [
      { label: 'Is Answer User', value: true },
      { label: 'Is Not Anser User', value: false },
    ];

    this.canAddUsers = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivate = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditUsers = this.hasPrivilege(this.privileges.CanEdit);
    this.getUsers();
  }

  // public columnDropdownFn() {
  //   this.columnDropdown = !this.columnDropdown;
  //   this.gridOptionsRotate = true;
  //   setTimeout(() => { this.gridOptionsRotate = false }, 900);
  // }

  public isVisibleCol(id) {
    return this.gridSettings.filter(x => x.id === id)[0].visible;
  }

  // public changeColVisibility(value) {
  //   this.gridSettings.forEach((elem) => {
  //     elem.visible = value.findIndex(x => x.id == elem.id) > -1;
  //   });

  //   console.log(value);
  // }

  /*Comp starts */
  // public savedViewChange(view: ViewSaved) {
  //   view.isDefault = !view.isDefault;
  //   this.globals.setAsDefault(view);
  //   this.defaultView = view;
  // }

  // public deleteView(view: ViewSaved) {
  //   this.globals.deleteView(view);
  //   this.viewsSaved = this.globals.getViews(this.controllerName);
  // }

  // public showSaveViewDiv() {
  //   this.showSaveView = !this.showSaveView;
  //   if (this.showSaveView) {
  //     this.viewToSave = new ViewSaved({ controllerName: this.controllerName, version: this.gridVersion, isDefault: false, gridId: this.gridStorageId });
  //   }
  // }

  // public saveView() {
  //   const savedView = new ViewSaved({ controllerName: this.controllerName, version: this.gridVersion, isDefault: false });
  //   Object.assign(savedView, this.viewToSave);
  //   this.globals.addView(savedView);
  //   this.viewsSaved = this.globals.getViews(this.controllerName);
  // }

  // public stopEvent(event) {
  //   event.preventDefault();
  //   event.stopPropagation();
  // }
  /*Comp end */


  async getUsers() {
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.loading = false;
      }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege('users', privName);
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
    }))
  }

  changeUserType(user: User) {
    const ctrl = this;
    this.userService.userPatch(user, env.apiVersion).pipe(take(1)).subscribe(responseHandler((resp) => {
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
        method = this.userService.userPost(this.currUser, env.apiVersion);
      } else {
        method = this.userService.userPatch(this.currUser, env.apiVersion);
      }

      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currUser.id === undefined) {
            ctrl.data.push(new User(resp.returnedObject));
            ctrl.toastr.success('User has been successfully created!');
          } else {
            ctrl.toastr.success('User has been successfully updated!');
          }
          ctrl.clseDialog();
        }
      }, () => {
        //DO not update user
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
    }
    else {
      return user;
    }
  }

}
