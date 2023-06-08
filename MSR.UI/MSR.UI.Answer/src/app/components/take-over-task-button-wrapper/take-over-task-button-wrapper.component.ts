import { WorkOrderItem } from "./../../models/work-order-item";
import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
  UpdateWorkOrderTaskRequest,
  WorkOrderService,
  WorkOrderModel,
  WorkOrderTaskModel,
  WorkOrderTaskService,
  UserService,
  UserModel,
  IUpdateWorkOrderTaskRequest,
  TakeOverWorkOrderRequest,
  ITakeOverWorkOrderRequest,
} from "../../services/api.client.generated";
import { Router } from "@angular/router";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { Globals } from "../../models/lib/globals";
import { forkJoin } from "rxjs";
import { take } from "rxjs/operators";

@Component({
  selector: "take-over-task-button-wrapper",
  templateUrl: "./take-over-task-button-wrapper.component.html",
  styleUrls: ["./take-over-task-button-wrapper.component.scss"],
  providers: [WorkOrderService, WorkOrderTaskService, UserService],
})
export class TakeOverTaskButtonWrapperComponent implements OnInit {
  @Input() takeOverSteps: boolean = false;
  @Input() isDisplayedInWipList: boolean = false;
  @Input() displayWipListDialog: boolean;
  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Input() workOrderModel: WorkOrderModel;
  @Output() displayWipListDialogChange = new EventEmitter();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOverId: number;

  constructor(
    private router: Router,
    private workOrderService: WorkOrderService,
    public globals: Globals,
    private workOrderTaskService: WorkOrderTaskService,
    private userService: UserService
  ) {}

  ngOnInit(): void {}

  openTakeOverAsUserConfirmationDialog(workOrderId: number) {
    if (this.isDisplayedInWipList) {
      this.router.navigate(["app/wip/details", workOrderId]);
      this.displayWipListDialogChange.emit(false);
    } else {
      this.showTakeOverAsUserConfirmationDialog =
        !this.showTakeOverAsUserConfirmationDialog;
      this.workOrderToTakeOverId = workOrderId;
    }
  }

  closeTakeOverAsUserConfirmationDialog() {
    this.showTakeOverAsUserConfirmationDialog =
      !this.showTakeOverAsUserConfirmationDialog;
  }

  takeOverWorkOrder() {
    let takeOverWorkOrderRequest = new TakeOverWorkOrderRequest({
      userId: this.globals.getCurrentUser().id,
      workOrderId: this.workOrderModel.id,
    } as ITakeOverWorkOrderRequest);

    this.globals.showLoader(true);
    this.workOrderService
      .takeOver(env.apiVersion, takeOverWorkOrderRequest)
      .pipe(take(1))
      .subscribe((takeOverWorkOrderResponse) => {
        let workOrdersTaksTakenOver = takeOverWorkOrderResponse.object;
        workOrdersTaksTakenOver.forEach((workOrderTaskTakenOver) => {
          let workOrderTaskToTakeOver = this.workOrderModel.workOrderTasks.find(
            (s) => s.id === workOrderTaskTakenOver.id
          );
          workOrderTaskToTakeOver.assignedTo =
            workOrderTaskTakenOver.assignedTo;
          workOrderTaskToTakeOver.assignedToUser =
            workOrderTaskTakenOver.assignedToUser;
        });

        this.closeTakeOverAsUserConfirmationDialog();
      });
  }
}
