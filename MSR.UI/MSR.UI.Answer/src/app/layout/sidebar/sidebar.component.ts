import { Component, ElementRef } from '@angular/core';
import { Renderer2 } from '@angular/core';

declare let jQuery: any;

@Component({
  selector: '[sidebar]',
  templateUrl: './sidebar.template.html'
})
export class Sidebar {
  sidebarHeight: number = 0;
  sidebarMenu: any = 0;
  sidebarItems: any;

  constructor(private renderer: Renderer2, private el: ElementRef) {
    const userInfo = JSON.parse(localStorage.getItem('user'));
    this.sidebarItems = this.generateMenu(userInfo.roles[0].menus);
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

  ngAfterViewInit() {
    this.sidebarMenu = this.el.nativeElement.querySelector('#side-nav');
    if (window.innerWidth > 768) {
      setTimeout(() => {
        jQuery(this.sidebarMenu).find('.accordion-group.active .accordion-body').collapse('show');
      });
    }
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

  collapseSubMenu(event) {
    let currentMenu = event.target
      .closest('.accordion-group')
      .querySelector('.accordion-body');
    let collapsingMenu = this.sidebarMenu
      .querySelector('.accordion-group .accordion-body.collapse.show');
    jQuery(collapsingMenu).collapse('hide');
    jQuery(currentMenu).collapse('show');
    if (collapsingMenu && currentMenu !== collapsingMenu && window.innerWidth < 768) {
      let submenuHeight = 0;
      let submenuItems = collapsingMenu.querySelectorAll('li');
      submenuItems.forEach(() => {
        submenuHeight += 26;
      });
      this.sidebarHeight -= submenuHeight;
    }
  }

  sidebarBehavior(event) {
    this.setSidebarHeight(event);
    this.collapseSubMenu(event);
    this.renderer.setStyle(document
      .querySelector('.content'), 'margin-top', this.sidebarHeight + 'px');
  }
}
