import { Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AccountService, ResetPasswordRequest } from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

@Injectable()
export class ResetpasswordService {
    _isFetching: boolean = false;
    _errorMessage: string = '';

    constructor(
        private router: Router,
        private accountService: AccountService
    ) {
        
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
            this.accountService.resetpassword(new ResetPasswordRequest({ token: creds.token, newPassword: creds.password }), env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(() => {
                this.loginError('Password was updated!');
                setTimeout(() => {
                    this.logoutUser();
                }, 3000);
            }));
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
