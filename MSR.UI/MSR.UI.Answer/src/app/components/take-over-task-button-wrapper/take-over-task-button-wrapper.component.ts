import { WorkOrderItem } from './../../models/work-order-item';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UpdateWorkOrderTaskRequest, WorkOrderService, WorkOrderModel, WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel
} from '../../services/api.client.generated';
import { Router } from '@angular/router';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'take-over-task-button-wrapper',
  templateUrl: './take-over-task-button-wrapper.component.html',
  styleUrls: ['./take-over-task-button-wrapper.component.scss'],
  providers: [WorkOrderService, WorkOrderTaskService, UserService]
})
export class TakeOverTaskButtonWrapperComponent implements OnInit {
  @Input() takeOverSteps: boolean = false;
  @Input() isDisplayedInWipList: boolean = false;
  @Input() displayWipListDialog: boolean;
  @Output() displayWipListDialogChange = new EventEmitter();
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOverId: number;

  constructor(private router: Router, private workOrderService: WorkOrderService, public globals: Globals,
    private workOrderTaskService: WorkOrderTaskService, private userService: UserService) { }

  ngOnInit(): void {

  }

  openTakeOverAsUserConfirmationDialog(workOrderId: number) {

  if (this.isDisplayedInWipList) {
      this.router.navigate(['app/wip/details', workOrderId]);
      this.displayWipListDialogChange.emit(false);
    } else {
      this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
      this.workOrderToTakeOverId = workOrderId;
    }
  }

  closeTakeOverAsUserConfirmationDialog() {
    this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
  }

  takeOverAsUserConfirmationDialog() {

    if (this.takeOverSteps) {

      this.globals.showLoader(true);
      this.userService.loggedInUser(env.apiVersion).subscribe(responseHandler(response => {

        let loggedInUser = <UserModel>response.object;

        this.globals.showLoader(true);
        this.workOrderService.workOrder(this.workOrderToTakeOverId, null, null, null, null , env.apiVersion).subscribe(responseHandler(workOrderGetResponse => {

          let tasks = <Array<WorkOrderTaskModel>>workOrderGetResponse.object[0].workOrderTasks;

          let workOrderTaskPatchRequests = new Array<any>();

          tasks.forEach(task => {

            if (task.status?.name === 'Waiting to Start' || task.status?.name === 'Approved') {

              let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest();
              updateWorkOrderTaskRequest.status = 'Waiting to Start';
              updateWorkOrderTaskRequest.taskIsRunning = false;
              updateWorkOrderTaskRequest.taskRunningSince = task.taskRunningSince;
              updateWorkOrderTaskRequest.taskStepOrder = task.taskStepOrder;
              updateWorkOrderTaskRequest.workOrderTaskId = task.id;
              updateWorkOrderTaskRequest.assignedUserId = loggedInUser.id;

              workOrderTaskPatchRequests.push(this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest));
            }
          });

          if (workOrderTaskPatchRequests.length === 0) {
            this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
          } else {
            this.globals.showLoader(true);
            forkJoin(workOrderTaskPatchRequests).subscribe(responseHandler(responses => {
              this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
            }));
          }
        }));
      }));
    }
  }

}
