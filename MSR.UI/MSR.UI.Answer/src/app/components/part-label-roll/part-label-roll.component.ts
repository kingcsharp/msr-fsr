import { Component, Input, OnInit } from "@angular/core";
import {
  WorkOrderModel,
  WorkOrderPartModel,
  WorkOrderPartService,
} from "../../services/api.client.generated";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { take } from "rxjs/operators";
import * as _ from "lodash";

@Component({
  selector: "part-label-roll",
  templateUrl: "./part-label-roll.component.html",
  styleUrls: ["./part-label-roll.component.scss"],
  providers: [WorkOrderPartService],
})
export class PartLabelRollComponent implements OnInit {
  todayDate: Date = new Date();

  @Input() WorkOrder: WorkOrderModel;
  workOrderParts: Array<WorkOrderPartModel>;

  constructor(private workOrderPartService: WorkOrderPartService) {}

  ngOnInit(): void {
    this.workOrderPartService
      .workOrderPartGet(null, this.WorkOrder.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.workOrderParts = [];
          const sortData = _.sortBy(response.object, (woPart) => woPart.id);

          const parentParts = _.filter(sortData, (part) => !part.parentId);
          parentParts.forEach((parentPart) => {
            this.workOrderParts.push(parentPart);
            const subParts = _.filter(
              sortData,
              (subPart) => subPart.parentId === parentPart.id
            );
            this.workOrderParts.push(...subParts);
          });
        })
      );
  }

  getWidth(value) {
    let width = 2;

    if (!value) return width;
    if (value.length <= 5) {
      width = 5;
    } else if (value.length <= 10) {
      width = 4;
    } else if (value.length <= 15) {
      width = 3;
    } else if (value.length <= 20) {
      width = 2;
    } else if (value.length > 20) {
      width = 1;
    }

    return width;
  }
}
