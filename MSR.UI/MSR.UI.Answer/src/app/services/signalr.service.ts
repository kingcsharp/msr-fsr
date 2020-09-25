import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { NotificationService } from '../layout/navbar/notification.service';
import { environment as env } from '../../environments/environment';
import { ToastrService } from 'ngx-toastr';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnDestroy {
  workflowNotificationIds: Array<number>;
  hubConnection: signalR.HubConnection;
  constructor(private notificationService: NotificationService, private toastr: ToastrService) {
    this.workflowNotificationIds = new Array<number>();

  }

  public startConnection = () => {
    const token : string = localStorage.getItem('token');

    if (token == null || token == '') {
        return;
    }

    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(env.url + '/msg', {
        accessTokenFactory: () => token
      })
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Signalr Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
  }

  public discconecctHub = () => {
    this.hubConnection.stop();
    console.log('Signalr Connection stopped');
  }

  public addWorkflowNotificationListener = () => {
    this.hubConnection.on('WorkflowNotification', (evId, data) => {
      var index = this.workflowNotificationIds.findIndex(x => x === evId);
      if (index === -1) {
        this.workflowNotificationIds.push(evId);
        this.notificationService.addNotification(data);
      }
    });
  }

  public addToasterMessageNotificationListener = () => {
    this.hubConnection.on('ToasterMessage', (data) => {
      switch (data.status) {
        case 0:
          this.toastr.warning(data.message);
          break;
        case 1:
          this.toastr.success(data.message);
          break;
        case 2:
          this.toastr.error(data.message);
          break;
      }
    });
  }

  ngOnDestroy() {
    this.discconecctHub();
  }

}
