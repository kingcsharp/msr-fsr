import { Component, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { User } from '../../../models/lib/user';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { UserService, CreateUserRequest } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';

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
  canAddUsers: boolean = false;
  canEditUsers: boolean = false;
  elems: any;
  phoneMask = {
    mask: ['(', /[1-9]/, /\d/, /\d/, ')',
      ' ', /\d/, /\d/, /\d/,
      '-', /\d/, /\d/, /\d/, /\d/]
  };

  constructor(public userService: UserService, injector: Injector, private toastr: ToastrService,
    private globals: Globals) {

  }


  ngOnInit(): void {
    const now = new Date();
    this.currUser = new User();
    this.month = now.getMonth() + 1;
    this.year = now.getFullYear();
    this.getUsers();
    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];
    this.canAddUsers = this.hasPrivilege(this.privileges.CanCreate);
    this.canEditUsers = this.hasPrivilege(this.privileges.CanEdit);
    // this.canAddUsers = true;
    // this.canEditUsers = true;
  }

  async getUsers() {
    //data
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion)
    .pipe(take(1))
    .subscribe(response => {

      this.elems = response.object;
      // response.map(data => new CreateUserRequest(data));
      // this.elems = response.data;
      // response[0].
      console.log(response);
    });
    // this.data = await this.userService.getUsers();
    this.data = [];
    this.loading = false;
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

  changeUserStatus(user) {
    const ctrl = this;
    // this.userService.deleteUser(user).then(function (resp) {
    //   if (resp.hasErrors === null || !resp.hasErrors) {
    //     ctrl.toastr.success(`User has been successfully ${user.isActive ? 'deactivated' : 'activated'}!`);
    //   }
    //   else {
    //     user.isActive = !user.isActive;
    //   }
    // });
  }

  onUserSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    // if (jQuery('.parsleyjs').parsley().isValid()) {
    //   let method = null;
    //   this.globals.showLoader(true);
    //   if (this.currUser.id === undefined) {
    //     method = this.userService.postUser(this.currUser);
    //   } else {
    //     method = this.userService.putUser(this.currUser);
    //   }
    //   method.then(function (resp) {
    //     if (!resp.hasErrors) {
    //       if (ctrl.currUser.id === undefined) {
    //         ctrl.data.push(new User(resp.returnedObject));
    //         ctrl.toastr.success('User has been successfully created!');
    //       } else {
    //         ctrl.toastr.success('User has been successfully updated!');
    //         method = ctrl.userService.putUser(ctrl.currUser);
    //       }
    //       ctrl.clseDialog();
    //     }
    //   });
    // }
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
