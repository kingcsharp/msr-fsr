import { Component, Input, OnInit } from "@angular/core";
import {
  WorkOrderModel,
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

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
  }
}
