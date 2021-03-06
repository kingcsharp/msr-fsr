import { Injectable } from '@angular/core';
import { Router, NavigationStart, NavigationEnd, NavigationError, NavigationCancel, RoutesRecognized, RouteConfigLoadStart } from '@angular/router';
import { MenuItem, EnumMenuItem, EnumApprovalTables, UserModel } from '../../services/api.client.generated';
import { ViewSaved } from './ViewSaved';
import { ToastrService } from 'ngx-toastr';
import { DOCUMENT } from '@angular/common';
import { Inject } from '@angular/core';
import { EnumPrivilege } from '../../models/enums/privileges';
import { AllowedActions } from './AllowedActions';
import { Observable, Observer, BehaviorSubject, Subject } from 'rxjs';
import { ModalData } from './ModalData';

@Injectable()
export class Globals {
    login = false;
    openMainMenu = false;
    userLogged = false;
    loader: boolean;
    activeMenu: MenuItem = new MenuItem();
    user;
    views: Array<ViewSaved>;
    showApprovalModal: boolean = false;
    comment: string;
    showComment: BehaviorSubject<boolean> = new BehaviorSubject<boolean>(false);
    private requestsToIgnoreModal: Array<string> = new Array<string>();
    modalData: ModalData = {
        showModal: false,
        comment: new Subject<string>()
    };

    constructor(private router: Router, private toastr: ToastrService, @Inject(DOCUMENT) document) {
        this.loadUserFromLocalStorage();
        this.setActiveMenuItem(router);
        this.loader = true;
    }

    async showApprovalCommentModal(approvalEntity: any, activityType: EnumApprovalTables) {
        if (this.hasActivityPrivilege(activityType, EnumPrivilege.CanApprove)) {
            this.modalData.showModal = true;
            this.modalData.comment = new Subject<string>();
            return this.modalData.comment.asObservable().toPromise().then((comment) => {
                if (comment === null || comment === '' || comment === undefined) {
                    this.toastr.error('Can not save without adding a comment.');
                    throw new Error();
                }
                approvalEntity.comment = comment;
                return;
            });
        } else {
            return new Promise((resolve) => {
                return resolve();
            });
        }
    }

    setActiveMenuItem(router) {
        router.events
            .subscribe((event) => {
                if (event instanceof NavigationEnd) {

                    if (this.user !== undefined) {
                        let splitUrl = event.url.split('/');
                        const urlTocheck = splitUrl[splitUrl.length - 1];
                        let currMenuItem: [MenuItem];
                        let length = this.user.roles.length;
                        while (length--) {
                            const elem = this.user.roles[length].menus.filter(x => x.url.toLowerCase() === urlTocheck);
                            if (elem !== undefined && elem.length > 0) {
                                this.activeMenu = elem[0];
                                length = 0;
                            }
                        }
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
            this.user.timezoneSTDPipe = this.getOffset()['STD'];
            this.user.timezoneDSTPipe = this.getOffset()['DST'];
        }
    }

    showLoader(isOn) {
        setTimeout(() => {
            this.loader = isOn;
        }, 100);
    }

    hasPrivilege(controllerEnum, privilege) {
        const privileges = this.user.privileges[controllerEnum];
        if (privileges === undefined || privileges === null) {
            return false;
        }
        const ret = privileges.indexOf(privilege) > -1;
        return ret;
    }

    getCalendarDefault() {
        const en = {
            firstDayOfWeek: 0,
            dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
            dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
            dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
            monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
            monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            today: 'Today',
            clear: 'Clear',
            dateFormat: 'yyy-mm-dd'
        };
        return en;
    }

    getEnumPrivileges(controllerEnum): AllowedActions {
        const allowedActions = new AllowedActions({
            canCreate: this.hasPrivilege(controllerEnum, EnumPrivilege.CanCreate),
            canActivate: this.hasPrivilege(controllerEnum, EnumPrivilege.CanActivate),
            canDelete: this.hasPrivilege(controllerEnum, EnumPrivilege.CanDelete),
            canEdit: this.hasPrivilege(controllerEnum, EnumPrivilege.CanEdit),
            canRead: this.hasPrivilege(controllerEnum, EnumPrivilege.CanRead),
            canApprove: this.hasPrivilege(controllerEnum, EnumPrivilege.CanApprove)
        });

        return allowedActions;
    }

    hasActivityPrivilegeByTableName(tableName, privilege) {
        const approvalEnum = EnumApprovalTables[tableName];
        if (approvalEnum === undefined) {
            console.error('tableName does not exist in EnumApprovalTables, please select an enum that exists in EnumApprovalTables', EnumApprovalTables);
        }

        if (Object.keys(this.user.approvalPrivileges).length === 0) {
            return false;
        }
        const ret = this.user.approvalPrivileges[approvalEnum].indexOf(privilege) > -1;
        return ret;
    }

    hasActivityPrivilege(activityEnumVal: EnumApprovalTables, privilege) {
        if (Object.keys(this.user.approvalPrivileges).length === 0) {
            return false;
        }
        const privileges = this.user.approvalPrivileges[activityEnumVal];
        if (privileges === undefined) {
            return false;
        }
        const ret = privileges.indexOf(privilege) > -1;
        return ret;
    }

    getSingularMenuName(menuItem) {
        let name = EnumMenuItem[menuItem];
        if (menuItem === EnumMenuItem.Templates) {
          name = 'ProcedureStepTemplate';
        }
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
        this.user.timezoneSTDPipe = this.getOffset()['STD'];
        this.user.timezoneDSTPipe = this.getOffset()['DST'];
        localStorage.setItem('user', JSON.stringify(val));
    }

    getCurrentUser() {
        return this.user;
    }

    getOffset() {
        if (this.user.timeZone === undefined) {
            return {
              DST: '',
              STD: ''
            };
        }
        const offset = this.user.timeZone.offset;
        let intPart = Math.floor(offset);
        let fraction = Math.floor((offset - Math.floor(offset)) * 100) * 60 / 100;
        let fractionPart = '';
        if (fraction > 0) {
            fractionPart = ':' + fraction.toString();
            if (fraction.toString().length === 1) {
                fractionPart += '0';
            }
        }
        // return 'GMT' + intPart + fractionPart;

        return {
          DST: 'GMT' + intPart.toString() + fractionPart,
          STD: 'GMT' + (intPart - this.user.timeZone.useDalightSavings).toString() + fractionPart
        };
    }

    getLogin() {
        return this.login;
    }

    hasRole(roleName) {
      if (this.user.roles.length > 0) {
        const index = this.user.roles.findIndex((role) => role.name === roleName);
        if (index > -1) {
          return true;
        }
      }

      return false;
    }

    addRequestToIgnore(requestToIgnore: string): void {
        this.requestsToIgnoreModal.push(requestToIgnore);
    }

    removeRequestToIgnore(requestToIgnore: string): void {
        let indexOfRequestToRemove = this.requestsToIgnoreModal.findIndex(s => s === requestToIgnore);
        this.requestsToIgnoreModal.splice(indexOfRequestToRemove, 1);
    }

    isRequestNotOnListToIgnore(url: string): boolean {

        let requestIsNotOnList = true;
        this.requestsToIgnoreModal.map(requestToIgnore => {

            if (url.includes(requestToIgnore)) {
                requestIsNotOnList = false;
            }

        });

        return requestIsNotOnList;
    }

    getTopLevelLocations(){
        return [
            { label: 'Chandler', value: 'Chandler' },
            { label: 'Hillsboro', value: 'Hillsboro' },
            { label: 'Kiryat Gat', value: 'Kiryat Gat' },
            { label: 'Naas', value: 'Naas' },
        ]
    }

    getTopLevelStatus(){
        return [
            { label: 'Approved', value: 'Approved' },
            { label: 'In Progress', value: 'In Progress' },
            { label: 'Complete', value: 'Complete' },
            { label: 'Cancelled', value: 'Cancelled' },
            { label: 'Pending', value: 'Pending' },
            { label: 'Rejected', value: 'Rejected' },
            { label: 'Open', value: 'Open' },
            { label: 'Closed', value: 'Closed' },
            { label: 'Requested', value: 'Requested' },
            { label: 'Assigned', value: 'Assigned' },
            { label: 'Waiting to Start', value: 'Waiting to Start' },
            { label: 'Scheduled', value: 'Scheduled' }
        ]
    }
}
