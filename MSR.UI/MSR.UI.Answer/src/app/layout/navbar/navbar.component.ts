import { Component, Output, EventEmitter, ElementRef, Renderer2, OnInit } from '@angular/core';
import { LoginService } from '../../pages/login/login.service';
import { Globals } from '../../models/lib/globals';
import { NotificationService } from './notification.service';


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

  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    private loginService: LoginService,
    public globals: Globals,
    public notificationservice: NotificationService
  ) { }


  ngOnInit(): void {
    this.notificationservice.getNotifications();
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

  searchFormOpen(): void {
    if (this.searchFormState) {
      this.changeStyleElement('#search-form', 'height', '40px');
      this.changeStyleElement('.notifications ', 'top', '86px');
    } else {
      this.changeStyleElement('#search-form', 'height', '0px');
      this.changeStyleElement('.notifications ', 'top', '46px');
    }
    this.searchFormState = !this.searchFormState;
  }

  private changeStyleElement(selector, styleName, styleValue): void {
    styleValue == null ? this.renderer.removeStyle(this.el.nativeElement
      .querySelector(selector), styleName) : this.renderer.setStyle(this.el.nativeElement
        .querySelector(selector), styleName, styleValue);
  }

  logout() {
    this.loginService.logoutUser();
  }
}
