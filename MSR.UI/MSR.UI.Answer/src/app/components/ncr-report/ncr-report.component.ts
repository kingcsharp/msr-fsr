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
  taskStepOrder?: number;
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
  associatedDigitalPictures: Array<FileModel> = new Array<FileModel>();
  associatedDocuments: Array<FileModel> = new Array<FileModel>();
  showImagePreview: boolean = false;
  imagePreview: FileModel = new FileModel();
  menuItems = EnumMenuItem;
  ncrParts: Array<any>;
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
    this.generateMonitorSummaries();
    this.workOrderPart = this.WorkOrder.workOrderParts[0];
    this.showNcrParts = false;
    this.getNCRParts();
  }

  /**
   * Maps the reference files of a work order task.
   * @param workOrderTask - The work order task object.
   * @returns An array of mapped reference files.
   */
  private mapWoTaskReferenceFiles = (workOrderTask) => {
    if (
      !workOrderTask.referenceFiles ||
      workOrderTask.referenceFiles.length < 1
    )
      return [];

    return workOrderTask.referenceFiles.map((referenceFile) => {
      if (this.getViewerType(referenceFile.contentType) === "img") {
        this.associatedDigitalPictures.push(referenceFile);
      } else {
        this.associatedDocuments.push(referenceFile);
      }
    });
  };
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
      referenceFiles: this.mapWoTaskReferenceFiles(workOrderTask),
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
    debugger;
    this.ncrNumbers.forEach((ncrNumber) => {
      const woTasks = this.WorkOrder.workOrderTasks
        .filter((workOrderTask) => workOrderTask.ncNumber === ncrNumber)
        .map((workOrderTask) => this.mapToTaskSummary(workOrderTask));
      // do task
      this.taskSummaries.push({ tasks: woTasks, ncrNumber: ncrNumber});
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

  getNCRParts() {
    this.ncrParts = this.WorkOrder.workOrderParts.map((part) => ({
      id: part.id,
      name: part.part?.name || "",
      serialNumber: part.serialNumber,
      partNumber: part.part?.partNumber || "",
      detail: part.detail,
      tagType: "",
      mapped: false,
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

    _.map(ncrWorkOrderTasks, (workOrderTask) => {
      this.ncrParts.map((part, index) => {
        const mappedWorkOrderPart = _.find(
          workOrderTask.mappedWorkOrderParts,
          (s) => s.id === part.id
        );
        if (mappedWorkOrderPart) {
          this.ncrParts[index].mapped = true;
          this.ncrParts[index].tagType = mappedWorkOrderPart.tagType || "";
        }
      });
    });

    this.showNcrParts = _.some(this.ncrParts, (part) => part.mapped);
  }
}
