import { Component, OnInit } from "@angular/core";
import {
  WorkOrderSelectItem,
  WorkOrderService,
} from "../../services/api.client.generated";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { Router } from "@angular/router";
import { Globals } from "../../models/lib/globals";
import { WorkOrderItem } from "../../models/work-order-item";
import { take } from "rxjs/operators";

@Component({
  selector: "selectworkorderdropdown-wrapper",
  templateUrl: "./select-work-order-drop-down-wrapper.component.html",
  styleUrls: ["./select-work-order-drop-down-wrapper.component.scss"],
  providers: [WorkOrderService],
})
export class SelectWorkOrderDropDownWrapperComponent implements OnInit {
  workOrdersAvailable: Array<WorkOrderItem>;
  selectedWorkOrder: string;
  disableDropDown: boolean = true;
  placeHolder: string = "Loading Available WorkOrders...";

  constructor(
    private workOrderService: WorkOrderService,
    private router: Router,
    public globals: Globals
  ) {}

  ngOnInit(): void {
    this.globals.addRequestToIgnore("v1/WorkOrder?assignedToId");

    this.workOrderService
      .selectItems(this.globals.getCurrentUser().id, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          let workOrderSelectItems = <Array<WorkOrderSelectItem>>(
            response.object
          );
          this.workOrdersAvailable = new Array<WorkOrderItem>();
          workOrderSelectItems.map((workOrderSelectItem) => {
            let workOrderItem = new WorkOrderItem();
            workOrderItem.WorkOrderId = workOrderSelectItem.workOrderId;
            workOrderItem.CustomerPurchaseNumber =
              workOrderSelectItem.customerPurchaseNumber;
            workOrderItem.ProcedureName = workOrderSelectItem.procedureName;
            workOrderItem.SerialNumber =
              workOrderSelectItem.purchaseSerialNumber;
            this.workOrdersAvailable.push(workOrderItem);
            // TODO: WorkOrder Status is Needed
            /*
        if (workOrder.workOrderTasks.map(s => s.statusId).every(m => m === EnumStatusSteps.WaitingtoStart)) {

          workOrderItem.Status = 'Requested';
          this.workOrdersAvailable.push(workOrderItem);

        } else if (workOrder.workOrderTasks.map(s => s.statusId).find(s =>
          s === EnumStatusSteps.InProgress ||
          s === EnumStatusSteps.Complete)) {

          workOrderItem.Status = 'Accepted';
          this.workOrdersAvailable.push(workOrderItem);

        }
        */
          });

          this.placeHolder = "Select a WorkOrder";
          this.disableDropDown = false;
        })
      );
  }

  workOrderSelected($event) {
    this.selectedWorkOrder = "";
    this.router.navigate(["app/wip/details", $event.value.WorkOrderId]);
  }
}
