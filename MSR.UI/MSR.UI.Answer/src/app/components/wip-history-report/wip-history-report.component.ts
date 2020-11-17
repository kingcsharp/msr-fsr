import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel, WorkOrderTaskModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';
import * as moment from 'moment';
import { EnumMonitorType } from '../../models/enums/EnumMonitorType';
import { EnumMonitorPassFailStatus } from '../../models/enums/EnumMonitorPassFailStatus';
import { EnumMonitorInputType } from '../../models/enums/EnumMonitorInputType';

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


    });

  }

}
