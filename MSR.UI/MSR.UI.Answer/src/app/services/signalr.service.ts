import { Injectable } from '@angular/core';
import * as signalR from "@aspnet/signalr";
import { NotificationService } from '../layout/navbar/notification.service';
import { environment as env } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class SignalRService {
  workflowNotificationIds: Array<number>;
  constructor(private notificationService: NotificationService) {
    this.workflowNotificationIds = new Array<number>();
  
  }

  private hubConnection: signalR.HubConnection
  public startConnection = () => {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(env.url + '/msg')
      .build();
    this.hubConnection
      .start()
      .then(() => console.log('Signalr Connection started'))
      .catch(err => console.log('Error while starting connection: ' + err))
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
}