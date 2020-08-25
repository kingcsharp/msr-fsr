import { Component, OnInit } from '@angular/core';
import { User, UserService, AccountService, ResetMyPasswordRequest } from '../../../../app/services/api.client.generated';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { copyObj } from '../../../models/lib/Utils';
declare let jQuery: any;
@Component({
  selector: 'app-profile',
  templateUrl: './profile.component.html',
  styleUrls: ['./profile.component.scss']
})
export class ProfileComponent implements OnInit {

  user: User;
  userPwObj: ResetMyPasswordRequest;
  parsleyInstance:any;
  constructor(public globals: Globals, private userService: UserService, private accountService: AccountService) {

  }

  ngOnInit(): void {
    this.parsleyInstance = jQuery('.parsleyjs').parsley();
    this.user = copyObj(this.globals.user);
    this.userPwObj = new ResetMyPasswordRequest({newPassword:'',oldPassword:''});
    this.getUser();

    this.parsleyInstance.addValidator('uppercase', {
      requirementType: 'number',
      validateString: function(value, requirement) {
        var uppercases = value.match(/[A-Z]/g) || [];
        return uppercases.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) uppercase letter.'
      }
    });
    
    //has lowercase
    this.parsleyInstance.addValidator('lowercase', {
      requirementType: 'number',
      validateString: function(value, requirement) {
        var lowecases = value.match(/[a-z]/g) || [];
        return lowecases.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) lowercase letter.'
      }
    });
    
    //has number
    this.parsleyInstance.addValidator('number', {
      requirementType: 'number',
      validateString: function(value, requirement) {
        var numbers = value.match(/[0-9]/g) || [];
        return numbers.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) number.'
      }
    });
    
    //has special char
    this.parsleyInstance.addValidator('special', {
      requirementType: 'number',
      validateString: function(value, requirement) {
        var specials = value.match(/[^a-zA-Z0-9]/g) || [];
        return specials.length >= requirement;
      },
      messages: {
        en: 'Your password must contain at least (%s) special characters.'
      }
    });
  }

  submitPwChange() {
    this.accountService.resetmypassword(env.apiVersion,this.userPwObj).pipe(take(1))
    .subscribe(responseHandler(response => {
      this.userPwObj = new ResetMyPasswordRequest({newPassword:'',oldPassword:''});
    }));
  }

  getUser() {
    this.userService.userGet(this.user.id, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.user = response.object[0];
      }));
  }

}
