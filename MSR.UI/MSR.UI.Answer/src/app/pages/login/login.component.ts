import { Component, HostBinding } from '@angular/core';
import { LoginService } from './login.service';
import { ActivatedRoute } from '@angular/router';
import { AccountService, ForgotPasswordRequest, ForgotUserNameRequest } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';

@Component({
  selector: 'login',
  templateUrl: './login.template.html',
})
export class Login {
  @HostBinding('class') classes = 'auth-page app';

  email: string = '';
  password: string = '';
  username: string = '';
  forgotUsername: boolean = false;
  showLogin: boolean = true;
  forgotPassword: boolean = false; 

  constructor(public loginService: LoginService, private route: ActivatedRoute, private accountService: AccountService) {
    if (this.loginService.isAuthenticated()) {
      this.loginService.receiveLogin();
    }
    
    this.route.queryParams.subscribe((params) => {
      if (params.token) {
        this.loginService.receiveToken(params.token);
      }
    });
  }

  public async forgotUserPassword() {
    if (this.username.length <= 0) {
      this.loginService.loginError('Please fill Username Field.');
      return;
    }
    const ctrl = this;
    this.loginService.isFetching = true;
    this.accountService.forgotpassword(env.apiVersion, new ForgotPasswordRequest({ userName: this.username }))
      .pipe(take(1))
      .subscribe(responseHandler(() => {
        this.loginService.isFetching = false;
        ctrl.loginService.loginError('Reset password email has been sent.');
        ctrl.showLoginDiv();
      }, () => {
        this.loginService.isFetching = false;
        ctrl.fogotPassword();
      }));
  }

  public async forgotUserName() {
    if (this.email.length <= 0) {
      this.loginService.loginError('Please fill Username Field.');
      return;
    }
    const ctrl = this;
    this.loginService.isFetching = true;
    this.accountService.forgotusername(env.apiVersion, new ForgotUserNameRequest({ email: this.email }))
      .pipe(take(1))
      .subscribe(responseHandler(() => {
        this.loginService.isFetching = false;
        ctrl.loginService.loginError('An email has been sent with your information.');
        ctrl.showLoginDiv();
      }, () => {
        this.loginService.isFetching = false;
        ctrl.fogotUserName();
      }));
  }

  public showLoginDiv() {
    this.forgotUsername = false;
    this.showLogin = true;
    this.forgotPassword = false;
  }

  public fogotUserName() {
    this.forgotUsername = true;
    this.showLogin = false;
    this.forgotPassword = false;
  }

  public fogotPassword() {
    this.forgotUsername = false;
    this.showLogin = false;
    this.forgotPassword = true;
  }

  public login() {
    const { email, password } = this;

    if (email.length !== 0 && password.length !== 0) {
      this.loginService.loginUser({ email, password });
    }
  }
}
