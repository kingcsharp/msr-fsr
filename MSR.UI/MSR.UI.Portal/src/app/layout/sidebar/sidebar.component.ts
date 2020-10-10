import { Component, ElementRef, EventEmitter, Output, OnInit } from '@angular/core';
import { Renderer2 } from '@angular/core';
import { Globals } from '../../models/lib/globals';
declare let jQuery: any;

@Component({
  selector: '[sidebar]',
  templateUrl: './sidebar.template.html',
  styleUrls: ['./sidebar.component.scss'],
})
export class Sidebar {
  @Output() displaySupportTicketModalDisplay = new EventEmitter();
  sidebarHeight: number = 0;
  sidebarMenu: any = 0;
  sidebarItems: any;
  supportTicketModalDisplay: boolean = false;


  constructor(private renderer: Renderer2, private el: ElementRef, private globals: Globals) {
    const menus = [{
      "url": "WIPStatus",
      "name": "Engineering",
      "info": "",
      "icon": "fas fa-cogs",
      "orderNumber": 1,
      "menuGroup": {
        "url": "#",
        "name": "WIP Views",
        "info": "",
        "icon": "fas fa-desktop",
        "orderNumber": 1
      },
      "permissions": null,
      "inheritedPermissions": null,
      "roles": [],
      "enumMenuItem": 31
    },
    {
      "url": "Specifications",
      "name": "New Requirements",
      "info": "",
      "icon": "far fa-plus-circle",
      "orderNumber": 1,
      "menuGroup": {
        "url": "#",
        "name": "Specifications",
        "info": "",
        "icon": "far fa-thermometer-half",
        "orderNumber": 2
      },
      "permissions": null,
      "inheritedPermissions": null,
      "roles": [],
      "enumMenuItem": 31
    },
    {
      // "url": "WIPStatus",
      // "name": "New Requirements",
      // "info": "",
      // "icon": "far fa-plus-circle",
      // "orderNumber": 1,
      "menuGroup": {
        "url": "#",
        "name": "Part Reporting",
        "info": "",
        "icon": "fas fa-line-chart",
        "orderNumber": 3
      },
      // "permissions": null,
      // "inheritedPermissions": null,
      // "roles": [],
      // "enumMenuItem": 31
    },
    {
      // "url": "WIPStatus",
      // "name": "New Requirements",
      // "info": "",
      // "icon": "far fa-plus-circle",
      // "orderNumber": 1,
      "menuGroup": {
        "url": "#",
        "name": "Profile",
        "info": "",
        "icon": "fal fa-user-edit",
        "orderNumber": 4
      },
      // "permissions": null,
      // "inheritedPermissions": null,
      // "roles": [],
      // "enumMenuItem": 31
    }];
    const roles = [
      {
        id: 0,
        menus: menus,
        name: "Administrator",
      }
    ];

    this.sidebarItems = this.generateMenu(roles);//globals.user.roles
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
            }]
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
