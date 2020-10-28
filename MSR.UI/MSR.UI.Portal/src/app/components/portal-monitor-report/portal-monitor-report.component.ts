import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel } from '../../services/api.client.generated';

@Component({
  selector: 'portal-monitor-report',
  templateUrl: './portal-monitor-report.component.html',
  styleUrls: ['./portal-monitor-report.component.scss']
})
export class PortalMonitorReportComponent implements OnInit {

  @Input() workOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;
  constructor() { }

  ngOnInit(): void {

    this.workOrderPart = this.workOrder.workOrderParts[0];

  }

}
