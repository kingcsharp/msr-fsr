import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized } from '@angular/router';
import { MenuItem } from '../../services/api.client.generated';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';
import { ColumnsSaved } from './ColumnsSaved';
import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';
import { EnumApprovalTables } from '../../models/enums/privileges';



@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader: boolean;
    activeMenu: MenuItem = new MenuItem();
    user;
    views: Array<ViewSaved>;

    constructor(private router: Router, private toastr: ToastrService, @Inject(DOCUMENT) document) {
        this.loadUserFromLocalStorage();
        this.setActiveMenuItem(router);
        this.loader = true;
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
        setTimeout(() => {
            this.loader = isOn;
        }, 100);
    }

    // hasPrivilege(controllerName, privilege) {
    //     const menuItem = this.user.roles[0].menus.filter(x => x.name.replace(' ', '').replace('/', '').toLowerCase() === controllerName.toLowerCase());
    //     if (menuItem.length === 0 || menuItem[0] === undefined) {
    //         return false;
    //     }
    //     const ret = menuItem[0].permissions.indexOf(privilege) > -1;
    //     return ret;
    // }

    hasPrivilege(controllerEnum, privilege) {
        var ret = this.user.privileges[controllerEnum].indexOf(privilege) > -1;
        return ret;
    }

    hasActivityPrivilegeByTableName(tableName, privilege) {
        var ret = this.user.approvalPrivileges[EnumApprovalTables[tableName]].indexOf(privilege) > -1;
        return ret;
    }

    hasActivityPrivilege(activityEnumVal, privilege) {
        var ret = this.user.approvalPrivileges[activityEnumVal].indexOf(privilege) > -1;
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
