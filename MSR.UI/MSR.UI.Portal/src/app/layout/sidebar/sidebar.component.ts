import { Component, ElementRef, EventEmitter, Output, OnInit, OnDestroy } from '@angular/core';
import { Renderer2 } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  QuoteService,
  QuotesProductsView,
  EnumMenuItem,
  CustomerService,
  Customer,
  CreateQuoteRequest,
} from '../../services/api.client.generated';
import { CSRJsonModel, ProcessModel, PartModel } from '../../models/csr-json-model';
import { responseHandler } from '../../utils/responseHandler';
import { environment as env } from '../../../environments/environment';
import { take } from 'rxjs/operators';
import { Subscription } from 'rxjs';
declare let jQuery: any;
//export class AdhocComponent implements OnInit, AfterViewInit, OnDestroy {
@Component({
  selector: '[sidebar]',
  templateUrl: './sidebar.template.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class Sidebar implements OnDestroy {
  @Output() displaySupportTicketModalDisplay = new EventEmitter();
  sidebarHeight: number = 0;
  sidebarMenu: any = 0;
  sidebarItems: any;
  supportTicketModalDisplay: boolean = false;
  buyerMenus = [{
    'url': 'wip/engineering',
    'name': 'Purchasing',
    'info': '',
    'icon': 'far fa-dollar-sign',
    'orderNumber': 1,
    'menuGroup': {
      'url': '#',
      'name': 'WIP Views',
      'info': '',
      'icon': 'fas fa-desktop',
      'orderNumber': 1
    },
    'permissions': null,
    'inheritedPermissions': null,
    'roles': [],
    'enumMenuItem': 11
  },
  {
    'url': 'Specifications',
    'name': 'New Requirements',
    'info': '',
    'icon': 'far fa-plus-circle',
    'orderNumber': 1,
    'menuGroup': {
      'url': '#',
      'name': 'Specifications',
      'info': '',
      'icon': 'far fa-thermometer-half',
      'orderNumber': 2
    },
    'permissions': null,
    'inheritedPermissions': null,
    'roles': [],
    'enumMenuItem': 31
  },
  {
    'menuGroup': {
      'url': '/#/app/reporting/report/adhocreports',
      'name': 'Part Reporting',
      'info': '',
      'icon': 'fas fa-line-chart',
      'orderNumber': 3
    }
  },
  {
    'menuGroup': {
      'url': '/#/app/people/profile',
      'name': 'Profile',
      'info': '',
      'icon': 'fal fa-user-edit',
      'orderNumber': 4
    }
  }];
  engineerMenus = [{
    'url': 'wip/engineering',
    'name': 'Engineering',
    'info': '',
    'icon': 'fas fa-cogs',
    'orderNumber': 1,
    'menuGroup': {
      'url': '#',
      'name': 'WIP Views',
      'info': '',
      'icon': 'fas fa-desktop',
      'orderNumber': 1
    },
    'permissions': null,
    'inheritedPermissions': null,
    'roles': [],
    'enumMenuItem': 11
  },
  {
    'url': 'Specifications',
    'name': 'New Requirements',
    'info': '',
    'icon': 'far fa-plus-circle',
    'orderNumber': 1,
    'menuGroup': {
      'url': '#',
      'name': 'Specifications',
      'info': '',
      'icon': 'far fa-thermometer-half',
      'orderNumber': 2
    },
    'permissions': null,
    'inheritedPermissions': null,
    'roles': [],
    'enumMenuItem': 31
  },
  {
    'menuGroup': {
      'url': '/#/app/reporting/report/adhocreports',
      'name': 'Part Reporting',
      'info': '',
      'icon': 'fas fa-line-chart',
      'orderNumber': 3
    }
  },
  {
    'menuGroup': {
      'url': '/#/app/people/profile',
      'name': 'Profile',
      'info': '',
      'icon': 'fal fa-user-edit',
      'orderNumber': 4
    }
  }];
  CSRFormValidErrors: string[] = [];
  CSRToCreate: CSRJsonModel;
  showCSRDialog: boolean = false;
  subscriptions: Subscription[] = [];

  constructor(private renderer: Renderer2, private el: ElementRef, public globals: Globals, private quoteService: QuoteService) {
    const subscription1 = this.globals.isBuyerObservable.subscribe(response => {
      if (this.globals.isBuyer !== undefined) {
        this.setAndGenerateMenu();
      }
    });
    this.subscriptions.push(subscription1);
    this.setAndGenerateMenu();
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  openCSRDialog() {
    this.CSRFormValidErrors = [];
    this.CSRToCreate = new CSRJsonModel();
    this.CSRToCreate.SubmittedBy = this.globals.user.fullName;
    this.CSRToCreate.Process = [new ProcessModel()];
    this.CSRToCreate.Parts = [new PartModel()];
    this.showCSRDialog = true;
  }

  onCSRSubmit() {
    this.CSRFormValidErrors = [];
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      const requestData = new CreateQuoteRequest();
      requestData.customerId = this.globals.selectedCustomer.id;
      const process = [];
      this.CSRToCreate.Process.forEach(v => {
        if (v.Contaminents) {
          process.push(v);
        }
      });
      this.CSRToCreate.Process = process;
      requestData.customerRequirementJson = JSON.stringify(this.CSRToCreate);
      this.quoteService.quotePost(env.apiVersion, requestData)
        .pipe(take(1))
        .subscribe(responseHandler((resp) => {
          this.closeCSRDialog();
        }));
    }
  }

  closeCSRDialog() {
    this.showCSRDialog = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  addCSRProcess() {
    this.CSRToCreate.Process.push(new ProcessModel());
  }

  removeCSRProcess() {
    if (this.CSRToCreate.Process.length > 1) {
      this.CSRToCreate.Process.pop();
    }
  }

  addCSRPart() {
    this.CSRToCreate.Parts.push(new PartModel());
  }

  removeCSRPart() {
    if (this.CSRToCreate.Parts.length > 1) {
      this.CSRToCreate.Parts.pop();
    }
  }


  setAndGenerateMenu() {
    if (this.globals.isBuyer === undefined) {
      return;
    }
    const roles = [
      {
        id: 0,
        menus: this.globals.isBuyer ? this.buyerMenus : this.engineerMenus,
        name: 'Administrator',
      }
    ];

    this.sidebarItems = this.generateMenu(roles);
  }

  generateMenu(roles: any) {
    let menuStructure: any = [];
    roles.forEach(role => {
      this.generateMenuItems(role.menus, menuStructure);
    });

    menuStructure.sort((a, b) => (a.orderNumber > b.orderNumber) ? 1 : -1);
    menuStructure.forEach(function (item) {
      item.submenu.sort((a, b) => (a.orderNumber > b.orderNumber) ? -1 : 1);
    });

    return menuStructure;
  }

  generateMenuItems(menuItems: any, menuStructure: any[]) {
    // show tooltip add .
    // description
    menuItems.forEach(function (item) {
      const elem = menuStructure.find(x => x.name === item.menuGroup?.name);
      if (elem === undefined) {
        let menuItem = { submenu: [] };
        if (!isNaN(item.orderNumber)) {
          menuItem.submenu = [
            {
              name: item.name,
              url: item.url,
              icon: item.icon,
              orderNr: item.orderNumber,
              info: item.info
            }];
        }

        Object.assign(menuItem, item.menuGroup);
        menuStructure.push(menuItem);
      } else {
        const submenuItem = elem.submenu.find(x => x.name === item.name);
        if (submenuItem === undefined) {
          elem.submenu.push({ name: item.name, url: item.url, icon: item.icon, orderNr: item.orderNumber, info: item.info });
        }
      }
    });
  }

  setSidebarHeight(event) {
    if (window.innerWidth < 768) {
      let sidebarMarginTop = parseInt(
        window.getComputedStyle(this.sidebarMenu).marginTop, 10
      );
      let sidebarMarginBottom = parseInt(
        window.getComputedStyle(this.sidebarMenu).marginBottom, 10
      );
      this.sidebarHeight = this.sidebarMenu.offsetHeight + sidebarMarginTop + sidebarMarginBottom;
      let closestAccordionGroup = event.target.closest('.accordion-group');
      let submenuHeight = 0;
      let submenuItems = closestAccordionGroup.querySelectorAll('ul > li');
      submenuItems.forEach(() => {
        submenuHeight += 26;
      });
      let expandedMenu = closestAccordionGroup
        .querySelector('.accordion-body')
        .getAttribute('aria-expanded');
      if (expandedMenu === 'false') {
        this.sidebarHeight += submenuHeight;
      } else {
        this.sidebarHeight -= submenuHeight;
      }
    }
  }

  sidebarBehavior(event) {
    this.setSidebarHeight(event);
    this.renderer.setStyle(document
      .querySelector('.content'), 'margin-top', this.sidebarHeight + 'px');
  }

  toggleSupportTicketModal() {
    this.displaySupportTicketModalDisplay.emit();
  }

}
