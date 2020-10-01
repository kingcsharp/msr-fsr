import { Component, Input, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { ProcedureStepMonitor, WorkOrderModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';

@Component({
  selector: 'work-report',
  templateUrl: './work-report.component.html',
  styleUrls: ['./work-report.component.scss']
})
export class WorkReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  reportTypeOptions: Array<SelectItem>;
  yesOrNoOptions: Array<SelectItem>;
  selectedReportType: string;
  selectedShowProcedureStepOption: boolean = true;
  selectedShowPurchaseItemOption: boolean = true;

  constructor() { }

  ngOnInit(): void {

    this.reportTypeOptions = [
      { label: 'Purchase Summary', value: 'PurchaseSummary'},
      { label: 'Technical Summary', value: 'TechnicalSummary'}
    ];

    this.yesOrNoOptions = [
      { label: 'Yes', value: true},
      { label: 'No', value: false}
    ];

    this.WorkOrder.workOrderTasks.map(s => {

      if (s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined) {
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>();
      }
    });

  }


}
