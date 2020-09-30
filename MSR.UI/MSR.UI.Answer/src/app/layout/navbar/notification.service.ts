import { take } from 'rxjs/operators';
import { WorkflowService, PendingApprovalNotification, PendingNotificationItem } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { Injectable } from '@angular/core';
import { responseHandler } from '../../utils/responseHandler';

@Injectable()
export class NotificationService {
  notificationCount: number = 0;
  notificationData: PendingApprovalNotification = new PendingNotificationItem();

  constructor(private workflowService: WorkflowService) {
    this.notificationData.items = new Array<PendingNotificationItem>();
  }

  addNotification(notificationItem: PendingNotificationItem) {
    var itemFound = this.notificationData.items.find(x => x.table === notificationItem.table);
    if (itemFound) {
      itemFound.count += notificationItem.count;
    } else {
      this.notificationData.items.push(notificationItem);
    }
    this.updateNotificationCount();
  }

  getNotifications() {
    this.workflowService.pending(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.items.forEach(element => {
          this.addNotification(element);
        });
      }));
  }

  updateNotificationCount() {
    let total = 0;
    this.notificationData.items.forEach(element => {
      total += element.count;
    });
    this.notificationCount = total;
  }

}
