import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { FileModel, ProcedureStepMonitor, WorkOrderModel, WorkOrderPartModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';
import { Globals } from '../../models/lib/globals';
import { ProcedureStepType } from '../../models/enums/ProcedureStepType';
import { ProcedureType } from '../../models/enums/ProcedureType';

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
  associatedDigitalPictures: Array<FileModel> = new Array<FileModel>();
  showImagePreview: boolean = false;
  imagePreview: FileModel = new FileModel();

  constructor(private globals: Globals) { }

  ngOnInit(): void {

    this.technicianFullName = this.globals.getCurrentUser().fullName;
    this.date = new Date();

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

      if (workOrderTask.procedureStep?.procedure?.procedureTypeId === ProcedureType.NCR) {

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

        this.associatedDigitalPictures = this.associatedDigitalPictures.concat(workOrderTask.referenceFiles);

      }


    });
  }

  showImagePreviewDialog(fileModel: FileModel) {
      this.imagePreview = fileModel;
      this.showImagePreview = !this.showImagePreview;
  }
}
