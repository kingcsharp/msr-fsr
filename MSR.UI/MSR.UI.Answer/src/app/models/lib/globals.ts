import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized } from '@angular/router';
import { MenuItem } from '../../services/api.client.generated';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';


@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader = true;
    activeMenu: MenuItem = new MenuItem();
    user;
    views: Array<ViewSaved>;

    constructor(private router: Router, private toastr: ToastrService) {
        this.loadViewsFromLocalStorage();
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

    loadViewsFromLocalStorage() {
        const savedViews = localStorage.getItem('viewsSaved');
        if (savedViews !== undefined && savedViews !== null) {
            this.views = JSON.parse(savedViews).map(x => new ViewSaved(x));
        } else {
            this.views = new Array<ViewSaved>();
        }
    }

    addView(view: ViewSaved) {
        if (this.views.findIndex(x => x.viewName === view.viewName && x.controllerName === view.controllerName) !== -1) {
            this.toastr.error(`Sorry a view with the name ${view.viewName} alread exists.`);
        }
        else {
            this.views.push(view);
            if (view.isDefault) {
                this.setAsDefault(view, false);
            }
            localStorage.setItem('viewsSaved', JSON.stringify(this.views));
        }
    }

    deleteView(view: ViewSaved) {
        const index = this.views.findIndex(x => x.viewName === view.viewName && x.controllerName === view.controllerName);
        if (index !== -1) {
            this.views.splice(index, 1);
            localStorage.setItem('viewsSaved', JSON.stringify(this.views));
        }
        else {
            this.toastr.error(`View ${view.viewName} not found.`)
        }
    }

    setAsDefault(view: ViewSaved, showSuccess: boolean = true) {
        const views = this.views.filter(x => x.controllerName === view.controllerName);
        if (views.length === 0) {
            this.toastr.error(`View ${view.viewName} not found.`);
            return
        }
        views.forEach(x => {
            x.isDefault = view.viewName === x.viewName
        });
        if (showSuccess) {
            this.toastr.success(`View ${view.viewName} is now default.`);
        }
    }

    getViews(controllerName: string): Array<ViewSaved> {
        const views = this.views.filter(x => x.controllerName === controllerName);
        return views;
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