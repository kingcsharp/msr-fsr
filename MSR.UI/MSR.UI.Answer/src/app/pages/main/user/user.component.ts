import { Component, OnInit, ViewEncapsulation, Injector } from '@angular/core';
import { UserService } from './user.service';
import { ɵDomSharedStylesHost } from '@angular/platform-browser';
import { User } from '../../../models/lib/user';
import { ToastrService } from 'ngx-toastr';

declare let jQuery: any;

@Component({
  selector: 'user',
  templateUrl: './user.template.html',
  styleUrls: ['./user.style.scss'],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class UserComponent implements OnInit {
  config: any;
  month: any;
  year: any;
  data: any;
  loading: boolean = true;
  domSharedStylesHost: any;
  display: boolean = false;
  currUser: User;
  injector: Injector;
  phoneValue = '';
  statuses: any[];
  instance: any;
  phoneMask = {
    mask: ['(', /[1-9]/, /\d/, /\d/, ')',
      ' ', /\d/, /\d/, /\d/,
      '-', /\d/, /\d/, /\d/, /\d/]
  };

  constructor(public userService: UserService, injector: Injector, private toastr: ToastrService) {
    this.domSharedStylesHost = injector.get(ɵDomSharedStylesHost);
    this.domSharedStylesHost.__onStylesAdded__ = this.domSharedStylesHost.onStylesAdded;
    this.domSharedStylesHost.onStylesAdded = (additions) => {
      const style = additions[0];
      if (!style || !style.trim().startsWith('.select2-container')) {
        this.domSharedStylesHost.__onStylesAdded__(additions);
      }
    };
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
    this.instance = jQuery('.parsleyjs').parsley();
  }

  async getUsers() {
    //data
    this.data = await this.userService.getUsers();
    this.loading = false;
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
    this.userService.deleteUser(user).then(function (resp) {
      if (resp.hasErrors === null || !resp.hasErrors) {
        ctrl.toastr.success(`User has been successfully ${user.isActive ? 'deactivated' : 'activated'}!`);
      }
      else {
        user.isActive = !user.isActive;
      }
    });
  }

  onUserSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method = null;
      if (this.currUser.id === undefined) {
        method = this.userService.postUser(this.currUser);
      } else {
        method = this.userService.putUser(this.currUser);
      }
      method.then(function (resp) {
        if (!resp.hasErrors) {
          if (ctrl.currUser.id === undefined) {
            ctrl.data.push(new User(resp.returnedObject));
            ctrl.toastr.success('User has been successfully created!');
          } else {
            ctrl.toastr.success('User has been successfully updated!');
            method = ctrl.userService.putUser(ctrl.currUser);
          }
          ctrl.clseDialog();
        }
      });
    }
  }

  getUser(user: User) {
    if (user === undefined) {
      let ret = new User();
      ret.isActive = true;
      ret.firstName = '';
      return ret;
    }
    else {
      return user;
    }
  }

}
