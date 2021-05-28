import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';
import { PackingListViewModel } from './detailed-packing-list-view-model';

@Component({
  selector: 'detailed-packing-list',
  templateUrl: './detailed-packing-list.component.html',
  styleUrls: ['./detailed-packing-list.component.scss']
})
export class DetailedPackingListComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;

  packingList: PackingListViewModel;

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
  }

}
