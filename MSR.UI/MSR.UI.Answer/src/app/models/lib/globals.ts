import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized } from '@angular/router';

@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader = true;
    user;

    constructor(private router: Router) {
        router.events.forEach((event) => {
            if (event instanceof NavigationEnd) {
                const userInfo = JSON.parse(localStorage.getItem('user'));
                debugger;
                let splitUrl = event.url.split('/');

                const menu = userInfo.roles[0].menus.filter(x => x.url.toLowerCase() === splitUrl[splitUrl.length-1]);
                

                // this.sidebarItems = this.generateMenu(userInfo.roles[0].menus);
            }

        });

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

    generateMenu(menuItems: any) {
        let menuStructure: any = [];
        //show tooltip add .
        //description
        menuItems.forEach(function (item) {
            const elem = menuStructure.find(x => x.name === item.menuGroup.name);
            if (elem === undefined) {
                let menuItem = { submenu: [{ name: item.name, url: item.url, icon: item.icon, orderNr: item.orderNumber, info: item.info }] };
                Object.assign(menuItem, item.menuGroup);
                menuStructure.push(menuItem);
            } else {
                const submenuItem = elem.submenu.find(x => x.name === item.name);
                if (submenuItem === undefined) {
                    elem.submenu.push({ name: item.name, url: item.url, icon: item.icon, orderNr: item.orderNumber, info: item.info });
                }
            }
        });
        return menuStructure;
    }

    showLoader(isOn) {
        this.loader = isOn;
    }

    hasPrivilege(controllerName, privilege) {
        const menuItem = this.user.roles[0].menus.filter(x => x.name.replace(' ', '').replace('/', '').toLowerCase() === controllerName.toLowerCase())
        const ret = menuItem[0].permissions.indexOf(privilege) > -1;
        return ret;
    }

    getGridTitle() {

    }
    setGridTitle() {

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