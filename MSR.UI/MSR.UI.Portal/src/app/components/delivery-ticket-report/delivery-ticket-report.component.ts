import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';

@Component({
  selector: 'delivery-ticket-report',
  templateUrl: './delivery-ticket-report.component.html',
  styleUrls: ['./delivery-ticket-report.component.scss']
})
export class DeliveryTicketReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;

  constructor() { }

  ngOnInit(): void {
  }

}
