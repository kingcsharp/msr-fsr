import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized } from '@angular/router';
import { MenuItem } from '../../services/api.client.generated';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';
import { ColumnsSaved } from './ColumnsSaved';
import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';



@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader = true;
    activeMenu: MenuItem = new MenuItem();
    user;
    views: Array<ViewSaved>;

    constructor(private router: Router, private toastr: ToastrService, @Inject(DOCUMENT) document) {
        this.loadUserFromLocalStorage();
        this.setActiveMenuItem(router);
    }

    setActiveMenuItem(router) {
        router.events.forEach((event) => {
            if (event instanceof NavigationEnd && this.user !== undefined) {
                let splitUrl = event.url.split('/');
                const currMenuItem: [MenuItem] = this.user.roles[0].menus.filter(x => x.url.toLowerCase() === splitUrl[splitUrl.length - 1]);
                if (currMenuItem.length > 0) {
                    this.activeMenu = currMenuItem[0];
                }
            }
        });
    }

    loadUserFromLocalStorage() {
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