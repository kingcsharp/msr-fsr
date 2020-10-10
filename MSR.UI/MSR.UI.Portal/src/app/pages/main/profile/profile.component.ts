import { Component, OnInit } from '@angular/core';
import { UserModel, UserService, AccountService, ResetMyPasswordRequest, TimeZoneModel, TimezoneService, EnumMenuItem, UpdateUserRequest, FileModel } from '../../../../app/services/api.client.generated';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { copyObj } from '../../../models/lib/Utils';
declare let jQuery: any;
declare let Parsley: any;
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss'] 
})
export class ProfileComponent implements OnInit {

  confirmpassword: string = '';
  user: UserModel;
  userPwObj: ResetMyPasswordRequest;
  parsleyInstance: any;
  allTimezones: Array<TimeZoneModel>;
  selectedTimezone: any;
  menuItem: EnumMenuItem = EnumMenuItem.Users;
  uploadedFiles: FileModel[] = [];
  constructor(public globals: Globals, private userService: UserService, private accountService: AccountService, private timezoneService: TimezoneService) {

  }

  ngOnInit(): void {
    this.user = copyObj(this.globals.user);
    this.userPwObj = new ResetMyPasswordRequest({ newPassword: '', oldPassword: '' });
    this.confirmpassword = '';
    this.getUser();
    this.getTimeZones();

    Parsley.addValidator('uppercase', {
      requirementType: 'number',
      validateString: function (value, requirement) {
        const uppercases = value.match(/[A-Z]/g) || [];
        return uppercases.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) uppercase letter.'
      }
    });

    Parsley.addValidator('lowercase', {
      requirementType: 'number',
      validateString: function (value, requirement) {
        const lowecases = value.match(/[a-z]/g) || [];
        return lowecases.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) lowercase letter.'
      }
    });

    Parsley.addValidator('number', {
      requirementType: 'number',
      validateString: function (value, requirement) {
        const numbers = value.match(/[0-9]/g) || [];
        return numbers.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) number.'
      }
    });

    Parsley.addValidator('special', {
      requirementType: 'number',
      validateString: function (value, requirement) {
        const specials = value.match(/[^a-zA-Z0-9]/g) || [];
        return specials.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) special characters.'
      }
    });
  }

  getTimeZones() {
    this.timezoneService.timezone(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.allTimezones = response.object.map((elem) => {
          elem.description = elem.description + this.parseOffset(elem.offset);
          return elem;
        });
        this.allTimezones = this.allTimezones.sort((a, b) => (a.offset > b.offset) ? 1 : -1);
      }));
  }

  parseOffset(offset) {
    if (offset > 0) {
      return ' (+' + offset + ')';
    } else {
      return ' (' + offset + ')';
    }
  }

  submitPwChange() {
    this.resetParsleyjs(1);
    this.globals.showLoader(true);
    jQuery(jQuery('.parsleyjs')[1]).parsley().validate();
    const ctrl = this;
    if (jQuery(jQuery('.parsleyjs')[1]).parsley().isValid()) {
      this.accountService.resetmypassword(env.apiVersion, this.userPwObj).pipe(take(1))
        .subscribe(responseHandler(response => {
          this.userPwObj = new ResetMyPasswordRequest({ newPassword: '', oldPassword: '' });
          this.confirmpassword = '';
        }));
    } else {
      this.globals.showLoader(false);
    }
  }

  resetParsleyjs(item) {
    if (jQuery(jQuery('.parsleyjs')[item]).parsley() !== undefined) {
      jQuery(jQuery('.parsleyjs')[item]).parsley().reset();
    }
  }

  submitDetailsChange() {
    let updateUserReq = new UpdateUserRequest();
    Object.assign(updateUserReq, this.user);
    updateUserReq.customerId = updateUserReq.customerId === 0 ? null : updateUserReq.customerId;
    if (this.uploadedFiles.length > 0) {
      updateUserReq.file = this.uploadedFiles[0];
    }
    if (this.user.timeZone !== undefined) {
      updateUserReq.timeZoneId = this.user.timeZone.id;
    }
    this.globals.showLoader(true);
    this.userService.userPatch(env.apiVersion, updateUserReq)
      .pipe(take(1)).subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        let user = this.globals.getCurrentUser();
        user.timezone = this.user.timeZone;
        user.file = this.user.fileModel;
        this.globals.updateUser(user);
      }));
  }

  getUser() {
    this.globals.showLoader(true);
    this.userService.userGet(this.user.id, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.user = response.object[0];
        if (this.user.fileModel !== undefined) {
          this.uploadedFiles.push(this.user.fileModel);
        }
      }));
  }

}
