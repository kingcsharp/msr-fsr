import { Component, Output, EventEmitter, ElementRef, Renderer2, OnInit } from '@angular/core';
import { LoginService } from '../../pages/login/login.service';
import { take } from 'rxjs/operators';
import { WorkflowService, PendingApprovalNotification } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Observable } from 'rxjs';
import { Globals } from '../../models/lib/globals';

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
  notificationCount: number = 0;
  settings: any = {
    isOpen: false
  };
  notificationData: PendingApprovalNotification = new PendingApprovalNotification();



  constructor(
    private renderer: Renderer2,
    private el: ElementRef,
    private loginService: LoginService,
    private workflowService: WorkflowService,
    public globals: Globals
  ) { }

  ngOnInit(): void {
    this.workflowService.pending(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.notificationData = response.object;
        this.notificationCount = this.getNotificationCount(this.notificationData);
      }));

  }

  getNotificationCount(notificationData: PendingApprovalNotification) {
    return notificationData.customers + notificationData.locations + notificationData.parts
      + notificationData.procedures + notificationData.purchaseOrders + notificationData.users;
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
