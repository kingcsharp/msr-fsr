import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel } from '../../services/api.client.generated';
import { EnumMonitorType } from '../../models/enums/EnumMonitorType';

@Component({
  selector: 'portal-monitor-report',
  templateUrl: './portal-monitor-report.component.html',
  styleUrls: ['./portal-monitor-report.component.scss']
})
export class PortalMonitorReportComponent implements OnInit {

  @Input() workOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;
  enumMonitorType = EnumMonitorType;
  constructor() { }

  ngOnInit(): void {

    this.workOrderPart = this.workOrder.workOrderParts[0];

  }

}
