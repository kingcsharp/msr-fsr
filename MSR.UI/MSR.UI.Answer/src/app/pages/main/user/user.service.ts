import { EventEmitter, Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AppConfig } from '../../../app.config';


@Injectable()
export class UserService {

    config: any;

    onReceiveDataSuccess: EventEmitter<boolean> = new EventEmitter();

    constructor(private http: HttpClient, appConfig: AppConfig) {
        this.config = appConfig.getConfig();
    }

    getUsers() {
        return this.http.get('/Users').toPromise().then((res: any) => {
            console.log(res);
            return true;
        }, err => {
            console.log(err);
            return false;
        });
    }
}
