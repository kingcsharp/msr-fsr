import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from '@aspnet/signalr';
import { NotificationService } from '../layout/navbar/notification.service';
import { environment as env } from '../../environments/environment';
import { ToastrService } from 'ngx-toastr';
import { Globals } from '../models/lib/globals';

@Injectable({
  providedIn: 'root'
})
export class SignalRService implements OnDestroy {
  workflowNotificationIds: Array<number>;
  hubConnection: signalR.HubConnection;
  constructor(private notificationService: NotificationService, private toastr: ToastrService, private globalService: Globals) {
    this.workflowNotificationIds = new Array<number>();

  }

  public startConnection = (waitIteration: number = 1) => {
    if (!this.globalService.userLogged) {
      if (waitIteration < 9) {
        setTimeout(() => {
          this.startConnection(++waitIteration);
        }, 2000 * waitIteration);
      } else {
        this.toastr.error('We were not able to connect with signalr.')
      }
      return;
    }

    const token: string = localStorage.getItem('token');
    if (token === null || token === '' || token === undefined) {
      return;
    }
    
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(env.url + '/msg', {
        accessTokenFactory: () => token
      })
      .build();
    this.hubConnection
      .start()
      .then(() => {
        this.addToasterMessageNotificationListener();
        this.addWorkflowNotificationListener();
      })
      .catch(err => console.log('Error while starting connection: ' + err));
  }

  public discconecctHub = () => {
    this.hubConnection.stop();
    console.log('Signalr Connection stopped');
  }

  public addWorkflowNotificationListener = () => {
    this.hubConnection.on('WorkflowNotification', (evId, data) => {
      let index = this.workflowNotificationIds.findIndex(x => x === evId);
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
