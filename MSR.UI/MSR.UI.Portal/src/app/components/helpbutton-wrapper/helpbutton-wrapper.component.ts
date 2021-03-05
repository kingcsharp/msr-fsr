import { Component, OnDestroy, OnInit } from '@angular/core';
import { NavigationEnd, Router } from '@angular/router';
import { Globals } from '../../models/lib/globals';
import { HelpService } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { Subscription } from 'rxjs';

@Component({
  selector: 'helpbutton-wrapper',
  templateUrl: './helpbutton-wrapper.component.html',
  styleUrls: ['./helpbutton-wrapper.component.scss'],
  providers: [HelpService]
})
export class HelpbuttonWrapperComponent implements OnInit, OnDestroy {
  canViewHelpPage = false;
  helpMenuUrl: string;
  display: boolean = false;
  helpContent: string;
  modalTitle: string;
  subscriptions: Subscription[] = [];

  constructor(private router: Router, private globals: Globals, private helpService: HelpService) { }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  ngOnInit(): void {
    this.loadHelp(this.router.url);
    let subscription = this.router.events.subscribe((event) => {
      if (event instanceof NavigationEnd) {
        this.loadHelp(event.url);
      }
    });
    this.subscriptions.push(subscription);
  }

  loadHelp(url) {
    this.helpMenuUrl = url.replace('/app', '');
    if (this.helpMenuUrl.includes('/wip/details')) {
      this.helpMenuUrl = '/wip/details/:id';
    }
    this.globals.showLoader(true);
    //TODO: Alec
    // this.helpService.helpGet(null, this.helpMenuUrl, env.apiVersion).pipe(take(1))
    //   .subscribe(responseHandler(response => {
    //     if (response.object.length !== 0) {
    //       this.helpContent = response.object[0].content;
    //       this.modalTitle = response.object[0].title;
    //       this.canViewHelpPage = true;
    //     } else {
    //       this.canViewHelpPage = false;
    //     }
    //   }));
  }
}
