import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../../app.config';
import { User } from '../../../models/lib/user';


@Injectable()
export class UserService {

    config: any;
    _isReceiving: any = false;

    onReceiveDataSuccess: EventEmitter<boolean> = new EventEmitter();

    constructor(private http: HttpClient, appConfig: AppConfig) {
        this.config = appConfig.getConfig();
    }

    async getUsers() {
        this.isReceiving = true;
        return this.http.get(User.PATH).toPromise().then((res: any) => {
            this.isReceiving = false;
            return res.returnedObject.map(data => new User(data));
        }, err => {
            this.isReceiving = false;
            console.log(err);
            return [];
        });
    }

    async deleteUser(user) {
        return this.http.delete(User.PATH + '/' + user.id).toPromise().then((res: any) => {
            return true;
        }, err => {
            return err.error;
        });
    }

    async postUser(user) {
        this.isReceiving = true;
        return this.http.post(User.PATH, user).toPromise().then((res: any) => {
            this.isReceiving = false;
            return res;
        }, err => {
            this.isReceiving = false;
            console.log(err);
            return err;
        });
    }

    async putUser(user) {
        return this.http.patch(User.PATH, user).toPromise().then((res: any) => {
            return res;
        }, err => {
            return err;
        });
    }

    get isReceiving() {
        return this._isReceiving;
    }

    set isReceiving(isReceiving) {
        this._isReceiving = isReceiving;
    }
}
