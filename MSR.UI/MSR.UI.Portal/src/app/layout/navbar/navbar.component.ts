import { Component, Output, EventEmitter, ElementRef, Renderer2, OnInit } from '@angular/core';
import { LoginService } from '../../pages/login/login.service';
import { Globals } from '../../models/lib/globals';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';

@Component({
  selector: '[navbar]',
  templateUrl: './navbar.template.html'
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
  searchValue: string;
  hideImg: boolean = false;

  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    private loginService: LoginService,
    public globals: Globals,
    private router: Router,
    private toastr: ToastrService
  ) { }


  ngOnInit(): void {
    this.searchValue = '';
  }

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
