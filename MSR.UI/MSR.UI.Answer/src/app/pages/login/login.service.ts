import { AppConfig } from '../../app.config';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';
import { CommonService } from '../../services/common';

const jwt = new JwtHelperService();

@Injectable()
export class LoginService {
  config: any;
  _isFetching: boolean = false;
  _errorMessage: string = '';

  constructor(
    appConfig: AppConfig,
    private http: HttpClient,
    private router: Router,
    private commonService: CommonService,
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
    if (creds.email.length > 0 && creds.password.length > 0) {
      this.http.post('/Account/login', { userName: creds.email, password: creds.password }).subscribe(async (res: any) => {
        await this.receiveToken(res.object);
      });
    } else {
      this.loginError('Something was wrong. Try again');
    }
  }

  async forgotUserPassword(email) {
    return this.http.post('/Account/forgotpassword', { userName: email }).toPromise().then((res: any) => {
      this.loginError("Reset password email sent.");
      return true;
    });
  }

  async forgotUserName(email) {
    return this.http.post('/Account/forgotusername', { email: email }).toPromise().then((res: any) => {
      this.loginError("An email has been sent with your information.");
      return true;
    });
  }


  async receiveToken(token) {
    let user: any = {};
    // We check if app runs with backend mode
    if (this.config.isBackend) {
      localStorage.setItem('token', token);
      delete token.token;
    } else {
      user = {
        email: this.config.auth.email
      };
      localStorage.setItem('token', token);
    }
    var userData = await this.commonService.getLoggedUser();
    Object.assign(user, userData);
    localStorage.setItem('user', JSON.stringify(user));

    this.receiveLogin();
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
    this.router.navigate(['/app/main/user']);
  }

  requestLogin() {
    this.isFetching = true;
  }
}
