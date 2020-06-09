import { AppConfig } from '../../app.config';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import { AccountService, SystemLoginRequest, UserService, ForgotPasswordRequest, ForgotUserNameRequest } from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

const jwt = new JwtHelperService();

@Injectable()
export class LoginService {
  config: any;
  _isFetching: boolean = false;
  _errorMessage: string = '';

  constructor(
    appConfig: AppConfig,
    private globals: Globals,
    private http: HttpClient,
    private router: Router, private accountService: AccountService, private userService: UserService
  ) {
    this.config = appConfig.getConfig();
  }

  get isFetching() {
    return this._isFetching;
  }

  set isFetching(val: boolean) {
    this._isFetching = val;
  }

  get errorMessage() {
    return this._errorMessage;
  }

  set errorMessage(val: string) {
    this._errorMessage = val;
  }

  isAuthenticated() {
    const token = localStorage.getItem('token');
    // We check if app runs with backend mode
    if (!this.config.isBackend && token) {
      return true;
    }
    if (!token) {
      return;
    }
    const date = new Date().getTime() / 1000;
    const data = jwt.decodeToken(token);
    return date < data.exp;
  }

  async loginUser(creds) {
    // We check if app runs with backend mode
    this.requestLogin();
    const ctrl = this;
    if (creds.email.length <= 0 || creds.password.length <= 0) {
      this.loginError('Something was wrong. Try again');
    }

    this.accountService.login(env.apiVersion, new SystemLoginRequest({ userName: creds.email, password: creds.password }))
      .pipe(take(1))
      .subscribe(responseHandler((result) => {
        ctrl.receiveToken(result.object);
      }, () => {
        ctrl.loginError('Username or Password is invalid.');
      }));
  }

  async receiveToken(token) {
    let user: any = {};
    // We check if app runs with backend mode
    user = {
      email: this.config.auth.email
    };
    localStorage.setItem('token', token);
    this.userService.loggedInUser(env.apiVersion).pipe(take(1))
      .subscribe((result) => {
        Object.assign(user, result.object);
        this.globals.updateUser(user);
        localStorage.setItem('user', JSON.stringify(user));
        if (user.roles.length === 0) {
          this.logoutUser();
          this.loginError('Sorry you do not have roles associated with your user.');
          return;
        }
        this.receiveLogin();
      });
  }

  logoutUser() {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
    document.cookie = 'token=;expires=Thu, 01 Jan 1970 00:00:01 GMT;';
    this.router.navigate(['/login']);
  }

  loginError(payload) {
    this.isFetching = false;
    this.errorMessage = payload;
  }

  receiveLogin() {
    this.isFetching = false;
    this.errorMessage = '';
    this.router.navigate(['/app/people/people']);
  }

  requestLogin() {
    this.isFetching = true;
  }
}
