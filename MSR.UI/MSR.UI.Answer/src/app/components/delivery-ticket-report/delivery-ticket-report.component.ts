import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';
import { PackingListViewModel } from '../detailed-packing-list/detailed-packing-list-view-model';

@Component({
  selector: 'delivery-ticket-report',
  templateUrl: './delivery-ticket-report.component.html',
  styleUrls: ['./delivery-ticket-report.component.scss']
})
export class DeliveryTicketReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;

  packingList: PackingListViewModel;

  constructor() {  this.packingList = new PackingListViewModel(); }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
  }

}
