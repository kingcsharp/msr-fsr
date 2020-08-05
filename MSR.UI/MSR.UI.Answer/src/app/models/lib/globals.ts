import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized } from '@angular/router';
import { MenuItem, EnumMenuItem } from '../../services/api.client.generated';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';
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

    hasPrivilege(controllerEnum, privilege) {
        const privileges = this.user.privileges[controllerEnum];
        if (privileges === undefined) {
            return false;
        }
        const ret = privileges.indexOf(privilege) > -1;
        return ret;
    }

    hasActivityPrivilegeByTableName(tableName, privilege) {
        const approvalEnum = EnumApprovalTables[tableName];
        if (approvalEnum === undefined) {
            console.error('tableName does not exist in EnumApprovalTables, please select an enum that exists in EnumApprovalTables', EnumApprovalTables);
        }

        const ret = this.user.approvalPrivileges[approvalEnum].indexOf(privilege) > -1;
        return ret;
    }

    hasActivityPrivilege(activityEnumVal, privilege) {
        const privileges = this.user.approvalPrivileges[activityEnumVal];
        if (privileges === undefined) {
            return false;
        }
        const ret = privileges.indexOf(privilege) > -1;
        return ret;
    }

    getSingularMenuName(menuItem) {
        let name = EnumMenuItem[menuItem];
        return name.replace(/s$/, '');
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
