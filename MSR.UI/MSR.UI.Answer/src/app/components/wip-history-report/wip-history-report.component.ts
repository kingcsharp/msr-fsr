import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel } from '../../services/api.client.generated';

@Component({
  selector: 'wip-history-report',
  templateUrl: './wip-history-report.component.html',
  styleUrls: ['./wip-history-report.component.scss']
})
export class WipHistoryReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;

  constructor() { }

  ngOnInit(): void {

    this.workOrderPart = this.WorkOrder.workOrderParts[0];

  }

}
