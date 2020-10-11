import { Component, Output, EventEmitter, ElementRef, Renderer2, OnInit } from '@angular/core';
import { LoginService } from '../../pages/login/login.service';
import { Globals } from '../../models/lib/globals';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

import {
  CustomerService
} from '../../services/api.client.generated';

@Component({
  selector: '[navbar]',
  templateUrl: './navbar.template.html',
  styleUrls: ['./navbar.component.scss']
})
export class Navbar implements OnInit {
  @Output() changeSidebarPosition = new EventEmitter();
  @Output() changeSidebarDisplay = new EventEmitter();
  @Output() openSidebar = new EventEmitter();

  display: string = 'Left';
  radioModel: string = 'Left';
  searchFormState: boolean = true;
  settings: any = {
    isOpen: false
  };
  hideImg: boolean = false;
  customers: any;
  selectedCustomer: any;
  showDropdown: boolean = false;
  roles: any = [{ name: 'Client Buyer', id: 1 }, { name: 'Client Engineer', id: 2 }];
  selectedRole: any;

  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    private loginService: LoginService,
    public globals: Globals,
    private router: Router,
    private toastr: ToastrService,
    private customerService: CustomerService
  ) { }


  ngOnInit(): void {
    this.getCustomers();
  }

  roleChange(ev) {
    this.globals.changeBuyer(ev.value.id === 1)
  }

  customerChange() {
    this.globals.changeCustomer(this.selectedCustomer);
  }

  getCustomers() {
    this.globals.showLoader(true);
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      this.customers = response.object.map((x) => {
        return { name: x.name, id: x.id };
      });
      this.showDropdown = true;
    }));
  }
  // value: { name: x.name, id: x.id } 

  updateUrl(ev) {
    this.hideImg = true;
  }

  sidebarPosition(position): void {
    this.changeSidebarPosition.emit(position);
  }

  sidebarDisplay(position): void {
    this.changeSidebarDisplay.emit(position);
  }

  sidebarOpen(): void {
    this.openSidebar.emit();
  }

  logout() {
    this.loginService.logoutUser();
  }
}
