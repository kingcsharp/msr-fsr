import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../../app.config';


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
        return this.http.get('/Users').toPromise().then((res: any) => {
            this.isReceiving = false;
            return res;
        }, err => {
            this.isReceiving = false;
            console.log(err);
            return [];
        });
    }

    get isReceiving() {
        return this._isReceiving;
    }

    set isReceiving(isReceiving) {
        this._isReceiving = isReceiving;
    }
}
