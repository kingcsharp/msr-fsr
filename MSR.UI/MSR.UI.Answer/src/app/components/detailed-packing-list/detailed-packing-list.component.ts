import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';
import { PackingListViewModel } from './detailed-packing-list-view-model';
import { formatDate } from '@angular/common';

@Component({
  selector: 'detailed-packing-list',
  templateUrl: './detailed-packing-list.component.html',
  styleUrls: ['./detailed-packing-list.component.scss'],
  encapsulation: ViewEncapsulation.Emulated,
})
export class DetailedPackingListComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;

  packingList: PackingListViewModel;
  currentDate: string;

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
    this.currentDate = formatDate(new Date(), 'MM/dd/yyyy', 'en');
  }

  generateArray(qty: number = 1) {
    return new Array(qty);
  }

}
