import { Injectable } from '@angular/core';

@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader = true;
    user;

    constructor() {
        if (localStorage.user === undefined || localStorage.user === undefined) {
            delete localStorage.user;
            delete localStorage.token;
        }

        if (localStorage.user !== undefined) {
            this.userLogged = true;
        }
        if (localStorage.user !== undefined) {
            this.user = JSON.parse(localStorage.user);
        }
    }

    showLoader(isOn) {
        this.loader = isOn;
    }

    hasPrivilege(controllerName, privilege) {
        const menuItem = this.user.roles[0].menus.filter(x => x.name.replace(' ', '').replace('/', '').toLowerCase() === controllerName.toLowerCase())
        const ret = menuItem[0].permissions.indexOf(privilege) > -1;
        return ret;
    }

    updateLogin(val) {
        this.login = val;
    }

    updateUserLogged(val) {
        this.userLogged = val;
    }

    updateUser(val) {
        this.user = val;
    }

    getLogin() {
        return this.login;
    }

}