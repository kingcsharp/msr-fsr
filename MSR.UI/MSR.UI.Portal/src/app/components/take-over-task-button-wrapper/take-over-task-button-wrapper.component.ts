import { WorkOrderItem } from './../../models/work-order-item';
import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  UpdateWorkOrderTaskRequest, WorkOrderService, WorkOrderModel, WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel, IUpdateWorkOrderTaskRequest
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
  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Output() displayWipListDialogChange = new EventEmitter();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
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

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.globals.getCurrentUser().id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
    } as IUpdateWorkOrderTaskRequest);

    this.globals.showLoader(true);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(() => {
      this.workOrderTaskInProgress.assignedTo = this.globals.getCurrentUser().id;
      this.workOrderTaskInProgress.assignedToUser = this.globals.getCurrentUser();
      this.closeTakeOverAsUserConfirmationDialog();
    }));
  }

}
