import { AppConfig } from '../../app.config';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { JwtHelperService } from '@auth0/angular-jwt';
import { Injectable } from '@angular/core';

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

  loginUser(creds) {
    // We check if app runs with backend mode
    if (!this.config.isBackend) {
      this.receiveToken('token');
    } else {
      this.requestLogin();
      if (creds.email.length > 0 && creds.password.length > 0) {
        this.http.post('/Account/login', { userName: creds.email, password: creds.password }).subscribe((res: any) => {
          this.receiveToken(res);
        }, err => {
          this.loginError(err.error.errorMessages[0].message);
        });

      } else {
        this.loginError('Something was wrong. Try again');
      }
    }
  }

  async forgotUserPassword(email) {
    return this.http.post('/Account/forgotpassword', { userName: email }).toPromise().then((res: any) => {
      this.loginError("Reset password email sent.");
      return true;
    }, err => {
      this.loginError(err.error.errorMessages[0].message);
      return false;
    });
  }

  receiveToken(token) {
    let user: any = {};
    // We check if app runs with backend mode
    if (this.config.isBackend) {
      localStorage.setItem('token', token.token);
      delete token.token;
      localStorage.setItem('user', JSON.stringify(token));
    } else {
      user = {
        email: this.config.auth.email
      };
      localStorage.setItem('token', token);
      localStorage.setItem('user', JSON.stringify(user));
    }

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
    this.router.navigate(['/app/main/visits']);
  }

  requestLogin() {
    this.isFetching = true;
  }
}
