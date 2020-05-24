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
  month: any;
  year: any;
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

  constructor(public userService: UserService, injector: Injector, private toastr: ToastrService,
    public globals: Globals, private elem: ElementRef) {
  }

  ngOnInit(): void {
    const now = new Date();
    this.currUser = new User();
    this.month = now.getMonth() + 1;
    this.year = now.getFullYear();
    this.gridVersion = "1.0.0";
    this.controllerName = this.elem.nativeElement.tagName.toLowerCase();
    this.viewToSave = new ViewSaved({ controllerName: this.controllerName, version: this.gridVersion, isDefault: false });
    this.viewsSaved = this.globals.getViews(this.controllerName);

    this.getUsers();
    // <option selected>Options</option>
    //               <option>Set as Default</option>
    //               <option>Delete View</option>

    this.savedViewsOptions = [
      { label: 'Set as Default', value: true },
      { label: 'Delete View', value: false },
    ]

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
  }

  public savedViewChange(event, view: ViewSaved) {
    if (event.value) {
      this.globals.setAsDefault(view);
    }
    else {
      this.globals.deleteView(view);
    }
  }

  public stopEvent(event) {
    event.preventDefault();
    event.stopPropagation();
  }

  public showSaveViewDiv($event) {
    // this.stopEvent($event);
    this.showSaveView = !this.showSaveView;
    if (this.showSaveView) {
      this.viewToSave = new ViewSaved({ controllerName: this.controllerName, version: this.gridVersion, isDefault: false });
    }
  }

  public saveView() {
    const savedView = new ViewSaved({ controllerName: this.controllerName, version: this.gridVersion, isDefault: false });
    Object.assign(savedView, this.viewToSave);
    this.globals.addView(savedView);
    this.viewsSaved = this.globals.getViews(this.controllerName);
  }

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
