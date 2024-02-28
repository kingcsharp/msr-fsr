import { Component, Input, OnInit } from "@angular/core";
import { DataLabel } from "./../../models/data-label-model";
import {
  WorkOrderModel,
  WorkOrderTaskMonitorModel,
} from "../../services/api.client.generated";
import { PackingListViewModel } from "../detailed-packing-list/detailed-packing-list-view-model";

@Component({
  selector: "technical-data-label",
  templateUrl: "./technical-data-label.component.html",
  styleUrls: ["./technical-data-label.component.scss"],
})
export class TechnicalDataLabelComponent implements OnInit {
  @Input() WorkOrder: WorkOrderModel;

  packingList: PackingListViewModel;
  labels: Array<DataLabel> = new Array<DataLabel>();

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.WorkOrder.workOrderTasks.map((s) => {

      if (
        s.workOrderTaskMonitors === null ||
        s.workOrderTaskMonitors === undefined
      ) {
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>();
      }
    });

    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
    console.log(this.packingList)
    // this.generateLabels();
  }

  generateLabels() {
    this.WorkOrder.workOrderTasks.map((workOrderTask) => {
      for (
        let index = 0;
        index < workOrderTask.workOrderTaskMonitors.length;
        index++
      ) {
        let dataLabel = new DataLabel();
        dataLabel.workOrderTaskMonitors = workOrderTask.workOrderTaskMonitors
        dataLabel.CustomerPurchaseNumber =
          this.WorkOrder.purchase.customerPurchaseNumber;
        dataLabel.Date = this.WorkOrder.scheduledEndDate;
        dataLabel.MonitorName =
          workOrderTask.workOrderTaskMonitors[index].procedureMonitorId !== null
            ? workOrderTask.workOrderTaskMonitors[index].procedureStepMonitor
                ?.description
            : workOrderTask.workOrderTaskMonitors[index]?.description;
        dataLabel.PartName = this.WorkOrder.product?.part?.name;
        dataLabel.PartNumber = this.WorkOrder.product?.part?.partNumber;
        dataLabel.Requestee = workOrderTask.assignedToUser?.fullName;
        dataLabel.TaskDescription =
          workOrderTask.procedureStepId !== null
            ? workOrderTask.procedureStep?.stepText
            : workOrderTask.stepText;
        this.labels.push(dataLabel);
      }
    });
  }

  checkMonitorType(label: DataLabel): boolean {
    return label.workOrderTaskMonitors.some(
      workOrder => workOrder.monitorTypeId === 2
    );
  }
}
