
import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../app/services/signalr.service';
import { Globals } from './models/lib/globals';
import * as Highcharts from 'highcharts';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`
})
export class AppComponent {
  constructor(private signalrService: SignalRService, private globalService: Globals) {
  }

  ngOnInit() {
    if (this.globalService.userLogged) {
      setTimeout(() => {
        this.signalrService.startConnection();
        this.signalrService.addWorkflowNotificationListener();
      }, 1000);
    }
  }
}
