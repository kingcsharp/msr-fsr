import { Component, Input, OnInit } from "@angular/core";
import {
  WorkOrderModel,
  WorkOrderPartModel,
  WorkOrderPartService,
} from "../../services/api.client.generated";
import * as _ from "lodash";

@Component({
  selector: "ncr-label",
  templateUrl: "./ncr-label.component.html",
  styleUrls: ["./ncr-label.component.scss"],
  providers: [WorkOrderPartService],
})
export class NcrLabelComponent implements OnInit {
  todayDate: Date = new Date();
  @Input() WorkOrder: WorkOrderModel;
  workOrderParts: Array<WorkOrderPartModel>;
  workOrderTasks: Array<any>;

  constructor() {}

  ngOnInit(): void {
    this.workOrderParts = [];

    const sortData = _.sortBy(
      this.WorkOrder.workOrderParts || [],
      (woPart) => woPart.id
    );

    const parentParts = _.filter(sortData, (part) => !part.parentId);
    parentParts.forEach((parentPart) => {
      this.workOrderParts.push(parentPart);
      const subParts = _.filter(
        sortData,
        (subPart) => subPart.parentId === parentPart.id
      );
      this.workOrderParts.push(...subParts);
    });

    this.workOrderTasks = [];
    _.map(this.workOrderParts, (workOrderPart) => {
      const associatedFirstNCRTask = _.orderBy(
        _.filter(
          this.WorkOrder.workOrderTasks || [],
          (task) =>
            !!task.ncNumber &&
            _.some(
              task.mappedWorkOrderParts,
              (part) => part.id === workOrderPart.id
            )
        ),
        ["ncNumber"],
        ["desc"]
      )[0];
      this.workOrderTasks.push(
        associatedFirstNCRTask
          ? _.filter(
              this.WorkOrder.workOrderTasks,
              (task) => task.ncNumber === associatedFirstNCRTask.ncNumber
            )
          : null
      );
      return {
        ...workOrderPart,
        tagType: _.find(
          associatedFirstNCRTask?.mappedWorkOrderParts,
          (part) => part.id === workOrderPart.id
        )?.tagType,
      };
    });
  }
}
