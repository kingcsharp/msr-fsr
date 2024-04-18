import { Component, Input, OnInit } from "@angular/core";
import { SelectItem } from "primeng/api";
import {
  ProcedureStepMonitorService,
  WorkOrderModel,
} from "../../services/api.client.generated";
import { PackingListViewModel } from "../detailed-packing-list/detailed-packing-list-view-model";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";

@Component({
  selector: "technical-data-label",
  templateUrl: "./technical-data-label.component.html",
  styleUrls: ["./technical-data-label.component.scss"],
})
export class TechnicalDataLabelComponent implements OnInit {
  @Input() WorkOrder: WorkOrderModel;
  @Input() unitOfMeasureOptions: Array<SelectItem> = [];

  packingList: PackingListViewModel;

  constructor(private procedureStepMonitorService: ProcedureStepMonitorService) {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.getUnitOfMeasures();
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
  }

  truncateHTML(text: string): string {
    let charlimit = 30;
    if(!text || text.length <= charlimit )
    {
        return text;
    }

    let without_html = text.replace(/<(?:.|\n)*?>/gm, '');
    let shortened = without_html.substring(0, charlimit) + "...";
    return shortened;
  }

  getUnitOfMeasureName(id: number) {
    return this.unitOfMeasureOptions.find(
      (s) => s.value === id
    )?.label;
  }

  getUnitOfMeasures() {
    this.procedureStepMonitorService
      .measureUnit(null, env.apiVersion)
      .subscribe(
        responseHandler((response) => {
          if (response.object.length === 0) {
            this.unitOfMeasureOptions = [];
          } else {
            this.unitOfMeasureOptions = response.object.map((unit) => ({
              label: unit.name,
              value: unit.id,
            }));
          }
        })
      );
  }

  checkApprovedMonitors(allMonitors) {
    return allMonitors.some(monitor => monitor.monitorTypeId === 2 && monitor.description);
  }

}
