import { Component, Input, OnInit } from "@angular/core";
import {
  FileModel,
  WorkOrderModel,
  WorkOrderPartModel,
  WorkOrderTaskMonitorModel,
  EnumMenuItem,
} from "../../services/api.client.generated";
import { Globals } from "../../models/lib/globals";
import { EnumProcedureType } from "../../models/enums/EnumProcedureType";
import * as _ from "lodash";
import { ProcedureStepType } from "../../models/enums/ProcedureStepType";

interface TaskSummary {
  ncrNumber: string;
  tasks: Array<any>;
  ncrParts: Array<any>;
  taskStepOrder?: number;
  associatedDigitalPictures: Array<FileModel>;
  associatedDocuments: Array<FileModel>;
}

@Component({
  selector: "ncr-report",
  templateUrl: "./ncr-report.component.html",
  styleUrls: ["./ncr-report.component.scss"],
})
export class NcrReportComponent implements OnInit {
  @Input() WorkOrder: WorkOrderModel;
  workOrderPart: WorkOrderPartModel;
  taskSummaries: Array<TaskSummary> = [];
  technicianFullName: string;
  date: Date;
  showImagePreview: boolean = false;
  imagePreview: FileModel = new FileModel();
  menuItems = EnumMenuItem;
  showNcrParts: boolean;
  ncrNumbers: Array<any>;

  constructor(private globals: Globals) {}

  ngOnInit(): void {
    this.technicianFullName = this.globals.getCurrentUser().fullName;
    this.date = new Date();
    this.WorkOrder.workOrderTasks.map((s) => {
      if (
        s.workOrderTaskMonitors === null ||
        s.workOrderTaskMonitors === undefined
      ) {
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>();
      }
    });
    this.showNcrParts = false;
    this.generateMonitorSummaries();
    this.workOrderPart = this.WorkOrder.workOrderParts[0];
  }

  /**
   * Maps a work order task to a task summary object.
   * @param workOrderTask - The work order task to be mapped.
   * @returns The task summary object.
   */
  private mapToTaskSummary(workOrderTask) {
    return {
      taskName:
        workOrderTask.procedureStep === undefined
          ? workOrderTask.title
          : workOrderTask.procedureStep.title,
      taskId: workOrderTask.id,
      monitors: workOrderTask.workOrderTaskMonitors.map((monitor) => {
        return {
          monitorTitle:
            (workOrderTask.procedureStep === undefined) !== null
              ? monitor.procedureStepMonitor?.description
              : monitor.description,
          result: monitor,
          comment: monitor.comment,
        };
      }),
      taskStepOrder: workOrderTask.taskStepOrder,
      referenceFiles: (!workOrderTask.referenceFiles || workOrderTask.referenceFiles.length < 1) ? [] : workOrderTask.referenceFiles
    };
  }

  /**
   * Generates monitor summaries based on the work order tasks.
   * Updates the `ncrNumbers` array and `taskSummaries` array.
   * Sorts the `taskSummaries` array based on task step order.
   */
  private generateMonitorSummaries() {
    this.ncrNumbers = _.uniq(
      this.WorkOrder.workOrderTasks.map((s) => s.ncNumber)
    ).filter((s) => s !== null && s !== undefined);

    const totalNcrParts = this.WorkOrder.workOrderParts.map((part) => ({
      id: part.id,
      name: part.part?.name || "",
      serialNumber: part.serialNumber,
      partNumber: part.part?.partNumber || "",
      detail: part.detail,
      tagType: ""
    }));

    const ncrWorkOrderTasks = _.map(
      _.groupBy(
        _.filter(
          this.WorkOrder.workOrderTasks || [],
          (task) => !!task.ncNumber
        ),
        "ncNumber"
      ),
      (tasks) => {
        return _.orderBy(tasks, ["taskStepOrder"], ["asc"])[0];
      }
    );
    
    this.ncrNumbers.forEach((ncrNumber) => {
      const woTasks = this.WorkOrder.workOrderTasks
        .filter((workOrderTask) => workOrderTask.ncNumber === ncrNumber)
        .map((workOrderTask) => this.mapToTaskSummary(workOrderTask));
      
      const referenceFiles = woTasks.reduce((total, task) => {
        const uniquePhotoIds = new Set(total.map(file => file.fileId));
        
        task.referenceFiles.forEach(file => {
          if (!uniquePhotoIds.has(file.fileId)) {
            uniquePhotoIds.add(file.fileId);
            total.push(file);
          }
        });
      
        return total;
      }, []);

      const associatedDigitalPictures = referenceFiles.filter((file) => this.getViewerType(file.contentType) === 'img');
      const associatedDocuments = referenceFiles.filter((file) => this.getViewerType(file.contentType) !== 'img');

      const matchingWorkOrderTask = ncrWorkOrderTasks.find(task => task.ncNumber === ncrNumber);
      const mappedWorkOrderParts = matchingWorkOrderTask ? matchingWorkOrderTask.mappedWorkOrderParts : [];
      const ncrParts = totalNcrParts.reduce((acc, part) => {
        const mappedWorkOrderPart = mappedWorkOrderParts.find(s => s.id === part.id);
        if (mappedWorkOrderPart) {
          this.showNcrParts = true;
          acc.push({
            ...part,
            tagType: mappedWorkOrderPart.tagType || ""
          });
        }
        return acc;
      }, []);

      this.taskSummaries.push({ tasks: woTasks, ncrNumber, ncrParts, associatedDigitalPictures, associatedDocuments});
    });

    this.taskSummaries.sort(
      (taskA, taskB) => taskA.taskStepOrder - taskB.taskStepOrder
    );
  }

  showImagePreviewDialog(fileModel: FileModel) {
    this.imagePreview = fileModel;
    this.showImagePreview = !this.showImagePreview;
  }

  getViewerType(contentType) {
    switch (contentType) {
      case "application/msword":
      case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
      case "application/vnd.ms-excel":
      case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
      case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
        return "office";
      case "text/plain":
      case "text/html":
      case "text/csv":
        return "google";
      case "application/pdf":
        return "pdf";
      case "image/gif":
      case "image/tiff":
      case "image/webp":
      case "image/jpeg":
      case "image/png":
        return "img";
      case "text/plain":
      default:
        return "url";
    }
  }
}
