import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../models/lib/user';
 
@Injectable()
export class CommonService {

    config: any;

    constructor(private http: HttpClient) {
    }

    async getLoggedUser() {
        return this.http.get(User.PATH + '/LoggedInUser').toPromise().then((res: any) => {
            return res.returnedObject;
        }, err => {
            console.log(err);
            return {};
        });
    }
}
