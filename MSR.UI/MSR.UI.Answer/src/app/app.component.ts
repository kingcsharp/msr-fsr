
import { Component, OnInit } from '@angular/core';
import { SignalRService } from '../app/services/signalr.service';

@Component({
  selector: 'app-root',
  template: `<router-outlet></router-outlet>`
})
export class AppComponent {
  constructor(public signalRService: SignalRService) {
  }

  ngOnInit() {
    this.signalRService.startConnection();
    this.signalRService.addWorkflowNotificationListener();
  }
}
