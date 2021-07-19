import { Component, Input, OnInit } from '@angular/core';
import { FileModel, WorkOrderModel, WorkOrderPartModel, WorkOrderTaskMonitorModel, EnumMenuItem } from '../../services/api.client.generated';
import { Globals } from '../../models/lib/globals';
import { EnumProcedureType } from '../../models/enums/EnumProcedureType';

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
  associatedDocuments: Array<FileModel> = new Array<FileModel>();
  showImagePreview: boolean = false;
  imagePreview: FileModel = new FileModel();
  menuItems = EnumMenuItem;
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
    this.WorkOrder.workOrderTasks.filter(s =>
      (s.procedureStep?.procedure?.procedureTypeId === EnumProcedureType.NCR)
    || s.isNCRTask).map(workOrderTask => {

        let taskSummary = {
          taskName: workOrderTask.procedureStep === undefined ? workOrderTask.title : workOrderTask.procedureStep.title,
          taskId: workOrderTask.id,
          monitors: new Array<any>(),
          taskStepOrder: workOrderTask.taskStepOrder
        };

        workOrderTask.workOrderTaskMonitors.map(workOrderTaskMonitor => {
          taskSummary.monitors.push({
            monitorTitle: workOrderTask.procedureStep === undefined  !== null ? workOrderTaskMonitor.procedureStepMonitor?.description : workOrderTaskMonitor.description,
            result: workOrderTaskMonitor,
            comment: workOrderTaskMonitor.comment
          });
        });

        this.taskSummaries.push(taskSummary);
        if (typeof workOrderTask.referenceFiles !== 'undefined' &&
            workOrderTask.referenceFiles !== null) {
            workOrderTask.referenceFiles.map(referenceFile => {
              if (this.getViewerType(referenceFile.contentType) === 'img') {
                this.associatedDigitalPictures.push(referenceFile);
              } else {
                this.associatedDocuments.push(referenceFile);
              }
            });
        }
    });

    this.taskSummaries.sort((taskA, taskB) => taskA.taskStepOrder - taskB.taskStepOrder);
  }

  showImagePreviewDialog(fileModel: FileModel) {
    this.imagePreview = fileModel;
    this.showImagePreview = !this.showImagePreview;
  }

  getViewerType(contentType) {
    switch (contentType) {
      case 'application/msword':
      case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document':
      case 'application/vnd.ms-excel':
      case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
      case 'application/vnd.openxmlformats-officedocument.presentationml.presentation':
        return 'office';
      case 'text/plain':
      case 'text/html':
      case 'text/csv':
        return 'google';
      case 'application/pdf':
        return 'pdf';
      case 'image/gif':
      case 'image/tiff':
      case 'image/webp':
      case 'image/jpeg':
      case 'image/png':
        return 'img';
      case 'text/plain':
      default:
        return 'url';
    }
  }
}
