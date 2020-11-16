import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel, WorkOrderTaskModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';
import * as moment from 'moment';
import { EnumMonitorType } from '../../models/enums/EnumMonitorType';
import { EnumMonitorPassFailStatus } from '../../models/enums/EnumMonitorPassFailStatus';

@Component({
  selector: 'wip-history-report',
  templateUrl: './wip-history-report.component.html',
  styleUrls: ['./wip-history-report.component.scss']
})
export class WipHistoryReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;
  workOrderTasks: Array<any>;

  constructor() { }

  ngOnInit(): void {

    this.workOrderPart = this.WorkOrder.workOrderParts[0];
    this.workOrderTasks = this.WorkOrder.workOrderTasks;
    this.workOrderTasks.map(workOrderTask => {

      if (workOrderTask.status.name === 'Completed' || workOrderTask.status.name === 'Cancelled' || workOrderTask.status.name === 'Complete') {
        workOrderTask.completedOn = workOrderTask.lastUpdatedOn;
      }

      let formattedTotalTaskTime = moment.utc(Number(workOrderTask.totalTaskTime) * 1000).format('HH:mm:ss');
      workOrderTask.taskTime = formattedTotalTaskTime === '00:00:00' ? '' : formattedTotalTaskTime;

      workOrderTask.workOrderTaskMonitors.map(workOrderTaskMonitor => {
        workOrderTaskMonitor.isPassing = this.isMonitorPassing(workOrderTaskMonitor);
      });

    });

  }

  onChange(event){

  }

  isMonitorPassing(workOrderTaskMonitor: WorkOrderTaskMonitorModel) {

    const monitorType = workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId;

    switch (monitorType) {
        case EnumMonitorType.Equipment:
            return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
        case EnumMonitorType.Number:
            return workOrderTaskMonitor.numVal;
        case EnumMonitorType.YesOrNo:
            return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
        case EnumMonitorType.Text:
            return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
        case EnumMonitorType.Select:
            return (workOrderTaskMonitor.multiVal !== undefined && workOrderTaskMonitor.multiVal !== null);
        case EnumMonitorType.PassOrFail:
            return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
        default:
            break;
    }

    return false;
}

}
