import { Injectable, OnDestroy } from '@angular/core';
import * as signalR from '@microsoft/signalr';
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

  public startConnection = () => {
    setTimeout(() => {
      if (this.globalService.userLogged) {
        this.connectToSignalR();
      }
    }, 1000);
  }

  private connectToSignalR() {
    const token: string = localStorage.getItem('token');
    if (token === null || token === '' || token === undefined) {
      return;
    }

    const messageUrl = env.MESSAGE_URL + '/msg';
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withAutomaticReconnect()
      .withUrl(messageUrl, {
        accessTokenFactory: () => token
      })
      .build();
    this.hubConnection
      .start()
      .then(() => {
        this.addToasterMessageNotificationListener();
        this.addWorkflowNotificationListener();
      })
      .catch(err => {
          console.log('Error while starting connection to ' + messageUrl);
          console.log(err);
      });
  }

  public subscribeWorkOrderUpdate = (callback) => {
    setTimeout(() => {
        if (this.hubConnection === undefined ||
            this.hubConnection.state !== 'Connected') {
            this.subscribeWorkOrderUpdate(callback);
            return;
        }
        this.hubConnection.invoke('SubscribeWorkOrderUpdate');
        this.addWorkOrderUpdateListener(callback);
    }, 1000);
  }

  public discconecctHub = () => {
    if (this.hubConnection !== undefined) {
      this.hubConnection.stop();
      console.log('Signalr Connection stopped');
    }
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

  public addWorkOrderUpdateListener = (callback) => {
    this.hubConnection.on('WorkOrderUpdate', (data) => {
        callback(data);
    });
  }

  ngOnDestroy() {
    this.discconecctHub();
  }

}
