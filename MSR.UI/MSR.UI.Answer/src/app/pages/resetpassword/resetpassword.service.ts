import { AppConfig } from '../../app.config';
import { HttpClient } from '@angular/common/http';
import { Router } from '@angular/router';
import { Injectable } from '@angular/core';

@Injectable()
export class ResetpasswordService {
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

    resetPassword(creds) {
        this.requestLogin();
        if (creds.token.length > 0 && creds.password.length > 0) {
            this.http.patch('/Account/resetpassword', { token: creds.token, newPassword: creds.password }).subscribe((res: any) => {
                this.loginError('Password was updated!');
                setTimeout(() => {
                    this.logoutUser();
                }, 3000);
            }, err => {
                this.loginError(err.error.errorMessages[0].message);
            });
        } else {
            this.loginError('Something was wrong. Try again');
        }
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

    requestLogin() {
        this.isFetching = true;
    }
}
