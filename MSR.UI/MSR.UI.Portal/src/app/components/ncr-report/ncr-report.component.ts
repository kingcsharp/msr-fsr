import { Component, Input, OnInit } from '@angular/core';
import { ProcedureStepMonitor, WorkOrderModel, WorkOrderPartModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';
import { Globals } from '../../models/lib/globals';
@Component({
  selector: 'ncr-report',
  templateUrl: './ncr-report.component.html',
  styleUrls: ['./ncr-report.component.scss']
})
export class NcrReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;
  taskSummaries: Array<any> = new Array<any>();
  technicianFullName: string;
  date: Date;
  constructor(private globals: Globals) { }

  ngOnInit(): void {

    this.technicianFullName = this.globals.getCurrentUser().fullName;
    this.date =  new Date();

    this.WorkOrder.workOrderTasks.map(s => {

      if (s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined) {
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>();
      }
    });

    this.generateMonitorSummaries();

    this.workOrderPart = this.WorkOrder.workOrderParts[0];
  }

  generateMonitorSummaries() {

    this.WorkOrder.workOrderTasks.forEach(workOrderTask => {

      let taskSummary = {
        taskName: workOrderTask.procedureStep.title,
        taskId: workOrderTask.id,
        procedureStepType: workOrderTask.procedureStepType.name,
        monitors: new Array<any>()
      };

      workOrderTask.workOrderTaskMonitors.forEach(workOrderTaskMonitor => {

        taskSummary.monitors.push({
          monitorTitle: workOrderTaskMonitor.procedureStepMonitor?.description,
          result: workOrderTaskMonitor.textVal,
          comment: workOrderTaskMonitor.comment

        });

      });

      this.taskSummaries.push(taskSummary);
    });

  }

}
