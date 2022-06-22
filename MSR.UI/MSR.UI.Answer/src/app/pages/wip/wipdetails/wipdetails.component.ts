import {
  Component,
  OnInit,
  ViewEncapsulation,
  ViewChild,
  ChangeDetectorRef,
  Inject,
  HostListener,
} from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import {
  ProcedureService,
  WorkOrderTaskService,
  LocationService,
  UserService,
  RoleService,
  CustomerService,
  CustomerModel,
  Procedure,
  PurchaseModel,
  WorkOrderPartService,
  InvoiceService,
  DocumentService,
  WorkOrderModel,
  WorkOrderPartModel,
  EnumMenuItem,
  WorkOrderService,
  WorkOrderTaskModel,
  ProcedureStepMonitorService,
  FileModel,
  UpdateWorkOrderPartRequest,
  IUpdateWorkOrderPartRequest,
  UpdateWorkOrderTaskRequest,
  IUpdateWorkOrderTaskRequest,
  ProductModel,
  AuditActionResultOfICollectionOfProcedureStepModel,
  ProcedureStepModel,
  UserModel,
  EnumSegregationType,
  CancelWorkOrderRequest,
  ICancelWorkOrderRequest,
  WorkOrderTaskMonitorModel,
  NCRPartWODetailView,
  INCRPartWODetailView,
  MappedWorkOrderPart,
  IMappedWorkOrderPart,
} from "../../../services/api.client.generated";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { Globals } from "../../../models/lib/globals";
import { WorkordertasktimerWrapperComponent } from "../../../components/workordertasktimer-wrapper/workordertasktimer-wrapper.component";
import { WorkordertaskmonitorsWrapperComponent } from "../../../components/workordertaskmonitors-wrapper/workordertaskmonitors-wrapper.component";
import { PrintotherReportComponent } from "../../../components/printother-report/printother-report.component";
import { CarouselComponent } from "ngx-bootstrap/carousel";
import { SelectItem } from "primeng/api";
import { take } from "rxjs/operators";
import { forkJoin, Observable } from "rxjs";
import { ProductSegregationService } from "../../../services/product-segregation.service";
import { EnumStatusSteps } from "../../../models/enums/EnumStatusSteps";
import { ProcedureStepType } from "../../../models/enums/ProcedureStepType";
import * as _ from "lodash";

@Component({
  selector: "app-wipdetails",
  templateUrl: "./wipdetails.component.html",
  styleUrls: ["./wipdetails.component.scss"],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true,
  providers: [
    WorkOrderService,
    ProcedureStepMonitorService,
    WorkOrderPartService,
    ProcedureService,
    WorkOrderTaskService,
    LocationService,
    UserService,
    UserService,
    InvoiceService,
    DocumentService,
    RoleService,
    CustomerService,
  ],
})
export class WipdetailsComponent implements OnInit {
  @ViewChild("workordertasktimer")
  workOrderTaskTimer: WorkordertasktimerWrapperComponent;
  @ViewChild("stepCarousel") carousel: CarouselComponent;
  @ViewChild("workordertaskmonitors")
  workordertaskmonitors: WorkordertaskmonitorsWrapperComponent;
  @ViewChild("printotherreport") printOtherReport: PrintotherReportComponent;
  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModel = new WorkOrderPartModel();
  procedure: Procedure = new Procedure();
  customer: CustomerModel = new CustomerModel();
  product: ProductModel = new ProductModel();
  purchase: PurchaseModel = new PurchaseModel();
  workOrderParts: Array<WorkOrderPartModel> = new Array<WorkOrderPartModel>();
  workOrderTaskInProgress: WorkOrderTaskModel;
  workOrderTaskToView: WorkOrderTaskModel;
  menuItems = EnumMenuItem;
  originalSerialNumbers: Array<any> = new Array<any>();
  hideCompletedWorkOrders: boolean = false;
  showCancelRemainingStepsDialog: boolean = false;
  activeSlideIndex = 0;
  showCarousel = true;
  hasSerializationStep: boolean;
  workOrderIsComplete: boolean;
  startSlideIndex: number = 0;
  monitorTypes: Array<SelectItem>;
  itemsPerASlide: number = 6;
  rolesRequiredToViewTask: Array<string> = new Array<string>();
  rolesRequiredMessage: string = undefined;
  nameOfTaskThatIsRestricted: string;
  hasAccessToTaskBeingViewed: boolean = false;
  areMonitorsValid: boolean = false;
  monitorsAreInvalidDialog: boolean = false;
  allUserRoleIds: Array<number>;
  EnumSegregationType = EnumSegregationType;
  showButtons: boolean = false;
  showNCRReportByPartDialog: boolean = false;
  technicianFullName: string;
  ncrAssociatedDigitalPictures: Array<FileModel> = new Array<FileModel>();
  ncrAssociatedDocuments: Array<FileModel> = new Array<FileModel>();
  showImagePreview: boolean = false;
  imagePreview: FileModel = new FileModel();
  ncrPartDetail: NCRPartWODetailView;
  ncrParts: Array<any>;
  ncrTaskIds: Array<any>;
  showNCParts: boolean = false;
  tagTypeOptions: Array<SelectItem>;
  enumProcedureStepType = ProcedureStepType;
  tagTypesValid: boolean = true;

  constructor(
    private route: ActivatedRoute,
    private workOrdersService: WorkOrderService,
    private workOrderPartService: WorkOrderPartService,
    private customerService: CustomerService,
    @Inject(ChangeDetectorRef) private changeDetectorRef: ChangeDetectorRef,
    private procedureService: ProcedureService,
    private documentService: DocumentService,
    public globals: Globals,
    private router: Router,
    private workOrderTaskService: WorkOrderTaskService,
    private roleService: RoleService,
    public productSegregationService: ProductSegregationService
  ) {}

  @HostListener("window:resize", ["$event"])
  getScreenSize() {
    if (window.innerWidth > 1100 && window.innerWidth < 1400) {
      this.itemsPerASlide = 7;
    } else if (window.innerWidth > 1400) {
      this.itemsPerASlide = 11;
    }
  }

  ngOnDestroy() {
    this.productSegregationService.SegregationType = EnumSegregationType.NONCU;
    this.productSegregationService.WipDetailsBeingDisplayed = false;
  }

  ngOnInit(): void {
    this.technicianFullName = this.globals.getCurrentUser().fullName;
    this.showButtons = false;
    this.getScreenSize();

    this.monitorTypes = [
      { label: "Equipment", value: 1 },
      { label: "Number", value: 2 },
      { label: "Yes or No", value: 3 },
      { label: "Text", value: 4 },
      { label: "Pass or Fail", value: 5 },
      { label: "Select", value: 6 },
    ];

    this.tagTypeOptions = [
      {
        label: "Yellow Tag",
        value: "Yellow Tag",
        styleClass: "tag-type-yellow",
      },
      { label: "Red Tag", value: "Red Tag", styleClass: "tag-type-red" },
    ];

    this.globals.showLoader(true);

    this.route.params.subscribe((params) => {
      let workOrderId = params["id"] == null ? 0 : Number(params["id"]);
      this.getWorkOrder(workOrderId);
    });
  }

  getWorkOrder(workOrderId: number) {
    this.globals.showLoader(true);
    this.workOrdersService
      .workOrder(workOrderId, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.workOrderModel = this.cleanData(response.object[0]);
          this.showButtons =
            this.globals.hasRole("Administrator") ||
            this.globals.hasRole("GM - General Manager") ||
            this.globals.hasLocation(this.workOrderModel.locationId);
          this.getDocumentsAndReferenceFilesForProcedureSteps(
            this.workOrderModel
          );
          this.hasSerializationStep =
            this.workOrderModel.workOrderTasks
              .map((s) => s.title)
              ?.find((m) => m?.trim().toLocaleUpperCase() === "SERIALIZE") !==
            undefined;
          this.workOrderIsComplete =
            this.workOrderModel.workOrderTasks.find(
              (s) =>
                s.status?.name?.trim() === "Waiting to Start" ||
                s.status?.name?.trim() === "In Progress" ||
                s.status.name.trim() === "Approved"
            ) === undefined;
          this.workOrderParts = this.workOrderModel.workOrderParts;
          this.parentPart = this.workOrderModel.workOrderParts[0];
          this.procedure = this.workOrderModel.product.procedure;
          this.customer = this.workOrderModel.product.customer;
          this.product = this.workOrderModel.product;
          this.purchase = this.workOrderModel.purchase;
          this.showNCParts = false;

          this.getNCRTaskIds();

          for (
            let index = 0;
            index < this.workOrderModel.workOrderTasks.length;
            index++
          ) {
            const status = this.workOrderModel.workOrderTasks[index].status;
            if (
              status.name === "In Progress" ||
              status.name === "Approved" ||
              status.name === "Waiting to Start"
            ) {
              this.checkRoleAccessAndSetTaskAsViewable(
                this.workOrderModel.workOrderTasks[index]
              );
              this.startSlideIndex = index;
              break;
            }
          }

          if (this.workOrderTaskInProgress === undefined) {
            this.checkRoleAccessAndSetTaskAsViewable(
              this.workOrderModel.workOrderTasks[0]
            );
          }

          this.productSegregationService.SegregationType =
            this.parentPart?.part?.segregationType;
          this.productSegregationService.PartTitle = `Customer Part # ${this.parentPart?.part?.partNumber}`;
          this.productSegregationService.WipDetailsBeingDisplayed = true;

          if (this.parentPart?.serialNumber === null) {
            this.productSegregationService.PartTitle += `, (Serial #: N/A), ${this.parentPart?.part?.name}`;
          } else {
            this.productSegregationService.PartTitle += `, (Serial #: ${this.parentPart?.serialNumber}), ${this.parentPart?.part?.name}`;
          }

          this.globals.showLoader(false);
        })
      );
  }

  getDocumentsAndReferenceFilesForProcedureSteps(
    workOrderModel: WorkOrderModel
  ) {
    let procedureStepGetRequests = new Array<
      Observable<AuditActionResultOfICollectionOfProcedureStepModel>
    >();

    workOrderModel.workOrderTasks.map((workOrderTask) => {
      if (workOrderTask.procedureStep !== undefined) {
        procedureStepGetRequests.push(
          this.procedureService.stepGet(
            workOrderTask.procedureStep?.procedureId,
            workOrderTask.procedureStep?.id,
            env.apiVersion
          )
        );
      }
    });

    forkJoin(procedureStepGetRequests).subscribe(
      responseHandler((responses) => {
        let procedureStepModels = <Array<ProcedureStepModel>>(
          responses.map((s) => s.object[0])
        );

        workOrderModel.workOrderTasks
          ?.filter((s) => s.procedureStep !== undefined)
          .map((workOrderTask) => {
            let procedureStepModel = procedureStepModels.find(
              (s) => s.id === workOrderTask.procedureStep.id
            );
            workOrderTask.procedureStep.referenceDocumentIds =
              procedureStepModel.referenceDocumentIds;
            workOrderTask.procedureStep.referenceFiles =
              procedureStepModel.referenceFiles;
          });

        let documentRequests = new Array<any>();

        let documentIds = []
          .concat(
            ...workOrderModel.workOrderTasks.map(
              (workOrderTask) =>
                workOrderTask.procedureStep?.referenceDocumentIds
            )
          )
          .filter((item, i, ar) => ar.indexOf(item) === i);

        documentIds.forEach((documentId) => {
          documentRequests.push(
            this.documentService.documentGet(
              documentId,
              null,
              null,
              null,
              null,
              null,
              null,
              null,
              null,
              env.apiVersion
            )
          );
        });

        forkJoin(documentRequests).subscribe(
          responseHandler((documentResponses) => {
            let documents = documentResponses.map((s) => s.object[0]);
            workOrderModel.workOrderTasks
              .filter((s) => s.procedureStep !== undefined)
              .map((workOrderTask) => {
                workOrderTask.procedureStep.referenceDocument =
                  new Array<FileModel>();

                workOrderTask.procedureStep.referenceDocumentIds.forEach(
                  (documentId) => {
                    let documentReferenceFiles = <Array<FileModel>>(
                      documents.find((s) => s.id === documentId)?.referenceFiles
                    );
                    documentReferenceFiles.map((documentReferenceFile) => {
                      workOrderTask.procedureStep.referenceDocument.push(
                        documentReferenceFile
                      );
                    });
                  }
                );
              });
          })
        );
      })
    );
  }

  checkRoleAccessAndSetTaskAsViewable(workOrderTask: WorkOrderTaskModel) {
    if (
      this.canUserAccessWorkOrderTask(workOrderTask) &&
      workOrderTask.procedureStep !== undefined
    ) {
      this.workOrderTaskInProgress = workOrderTask;
      this.workOrderTaskToView = workOrderTask;
      this.rolesRequiredToViewTask.length = 0;
      this.hasAccessToTaskBeingViewed = true;
      this.rolesRequiredMessage = undefined;
    }

    if (
      workOrderTask.procedureStep !== undefined &&
      !this.canUserAccessWorkOrderTask(workOrderTask)
    ) {
      this.nameOfTaskThatIsRestricted =
        workOrderTask.procedureStep === undefined
          ? workOrderTask.title
          : workOrderTask.procedureStep?.title;
      this.generateRolesRequiredMessage(
        workOrderTask.procedureStep.roles.map((s) => s.name)
      );
      this.hasAccessToTaskBeingViewed = false;
      this.workOrderTaskInProgress = workOrderTask;
      this.workOrderTaskToView = workOrderTask;
    }

    if (workOrderTask.procedureStep === undefined) {
      this.workOrderTaskInProgress = workOrderTask;
      this.workOrderTaskToView = workOrderTask;
      this.rolesRequiredToViewTask.length = 0;
      this.hasAccessToTaskBeingViewed = true;
      this.rolesRequiredMessage = undefined;
    }

    this.getNCRParts();
  }

  cleanData(workOrderModel: WorkOrderModel): WorkOrderModel {
    workOrderModel.workOrderTasks.sort(function (a, b) {
      return a.taskStepOrder - b.taskStepOrder;
    });

    workOrderModel.workOrderTasks.map((s) => {
      if (s.taskIsRunning === null || s.taskIsRunning === undefined) {
        s.taskIsRunning = false;
      }

      if (s.totalTaskTime === null || s.totalTaskTime === undefined) {
        s.totalTaskTime = 0;
      }

      if (s.referenceFiles === undefined) {
        s.referenceFiles = new Array<FileModel>();
      }

      s.workOrderTaskMonitors
        .filter((m) => m.procedureStepMonitor !== undefined)
        .map((u) => u.procedureStepMonitor)
        .map((u) => {
          if (u.monitorTypeId === 1 && u.inputTypeId === 1) {
            u.inputType = "Manual";
          }

          if (u.monitorTypeId === 2 && u.inputTypeId === 5) {
            u.inputType = "Manual";
          }

          if (u.monitorTypeId === 2 && u.inputTypeId === 6) {
            u.inputType = "Sensor";
          }

          if (u.monitorTypeId === 3 && u.inputTypeId === 8) {
            u.inputType = "Manual";
          }

          if (u.monitorTypeId === 4 && u.inputTypeId === 12) {
            u.inputType = "Manual";
          }

          if (u.monitorTypeId === 5 && u.inputTypeId === 16) {
            u.inputType = "Manual";
          }

          if (u.monitorTypeId === 6 && u.inputTypeId === 19) {
            u.inputType = "Manual";
          }
        });

      s.workOrderTaskMonitors
        .filter((m) => m.procedureStepMonitor !== undefined)
        .map((u) => u.procedureStepMonitor)
        .map((u) => {
          u.monitorType = this.monitorTypes.find(
            (t) => t.value === u.monitorTypeId
          ).label;
        });
    });

    return workOrderModel;
  }

  canUserAccessWorkOrderTask(workOrderTask: WorkOrderTaskModel): boolean {
    if (workOrderTask.procedureStep?.roles.length === 0) {
      return false;
    }
    let canUserAccessStep = workOrderTask.procedureStep?.roles
      ?.map((s) => s.id)
      .some((m) => this.getAllRoleIdsForCurrentUser().includes(m));
    return canUserAccessStep;
  }

  getAllRoleIdsForCurrentUser() {
    let currentUser = <UserModel>this.globals.user;
    return currentUser.roles
      .map((s) => s.id)
      .filter((n, i) => currentUser.roles.map((s) => s.id).indexOf(n) === i);
  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel) {
    if (
      this.canUserAccessWorkOrderTask(workOrderTask) ||
      workOrderTask.procedureStep === undefined
    ) {
      this.workOrderTaskToView = workOrderTask;
      this.rolesRequiredToViewTask.length = 0;
      this.hasAccessToTaskBeingViewed = true;
      this.rolesRequiredMessage = undefined;
    } else if (
      !this.canUserAccessWorkOrderTask(workOrderTask) &&
      workOrderTask.procedureStep !== undefined
    ) {
      this.nameOfTaskThatIsRestricted = workOrderTask.procedureStep.title;
      this.generateRolesRequiredMessage(
        workOrderTask.procedureStep.roles.map((s) => s.name)
      );
      this.hasAccessToTaskBeingViewed = false;
      this.workOrderTaskInProgress = workOrderTask;
      this.workOrderTaskToView = workOrderTask;
    }

    if (this.workOrderIsComplete) {
      this.workOrderTaskInProgress = workOrderTask;
    }

    this.getNCRParts();
  }

  generateRolesRequiredMessage(roles: Array<string>) {
    if (roles.length === 0) {
      return;
    }
    this.rolesRequiredMessage = "";
    roles.map((role, index) => {
      if (index === 0) {
        this.rolesRequiredMessage += role;
      } else if (index === roles.length - 1) {
        this.rolesRequiredMessage += " or " + role;
      } else {
        this.rolesRequiredMessage += ", " + role;
      }
    });
  }

  changeSerialNumber(index, serialNumber, partId) {
    if (this.originalSerialNumbers.find((s) => s.id === partId) === undefined) {
      this.originalSerialNumbers.push({
        id: partId,
        serialNumber: serialNumber,
      });
    }

    let inputElement = <HTMLInputElement>(
      document.getElementById("serialnumber" + index)
    );
    inputElement.disabled = false;

    let changebuttonElement = <HTMLInputElement>(
      document.getElementById("changebutton" + index)
    );
    changebuttonElement.classList.add("d-none");

    let submitbuttonElement = <HTMLInputElement>(
      document.getElementById("submitbutton" + index)
    );
    submitbuttonElement.classList.remove("d-none");

    let cancelbuttonElement = <HTMLInputElement>(
      document.getElementById("cancelbutton" + index)
    );
    cancelbuttonElement.classList.remove("d-none");
  }

  submitSerialNumber(index, partId) {
    let updateWorkOrderRequest = new UpdateWorkOrderPartRequest({
      workOrderPartId: partId,
      serialNumber: this.workOrderModel.workOrderParts.find(
        (s) => s.id === partId
      ).serialNumber,
    } as IUpdateWorkOrderPartRequest);

    this.globals.showLoader(true);
    this.workOrderPartService
      .workOrderPartPatch(env.apiVersion, updateWorkOrderRequest)
      .pipe(take(1))
      .subscribe((response) => {
        let workOrderPart = this.workOrderModel.workOrderParts.find(
          (s) => s.id === partId
        );
        workOrderPart.cycleCount = response.object?.cycleCount || 0;
        workOrderPart.ncrHistoryItems = response.object?.ncrHistoryItems || [];
        this.originalSerialNumbers.find((s) => s.id === partId).serialNumber =
          workOrderPart.serialNumber;

        let changebuttonElement = <HTMLInputElement>(
          document.getElementById("changebutton" + index)
        );
        changebuttonElement.classList.remove("d-none");

        let submitbuttonElement = <HTMLInputElement>(
          document.getElementById("submitbutton" + index)
        );
        submitbuttonElement.classList.add("d-none");

        let cancelbuttonElement = <HTMLInputElement>(
          document.getElementById("cancelbutton" + index)
        );
        cancelbuttonElement.classList.add("d-none");
      });
  }

  cancelSerialNumber(index, partId) {
    let originalSerialNumber = this.originalSerialNumbers.find(
      (s) => s.id === partId
    );
    let inputElement = <HTMLInputElement>(
      document.getElementById("serialnumber" + index)
    );
    inputElement.disabled = true;
    inputElement.value = originalSerialNumber.serialNumber;
    this.workOrderModel.workOrderParts.find(
      (s) => s.id === partId
    ).serialNumber = originalSerialNumber.serialNumber;

    let changebuttonElement = <HTMLInputElement>(
      document.getElementById("changebutton" + index)
    );
    changebuttonElement.classList.remove("d-none");

    let submitbuttonElement = <HTMLInputElement>(
      document.getElementById("submitbutton" + index)
    );
    submitbuttonElement.classList.add("d-none");

    let cancelbuttonElement = <HTMLInputElement>(
      document.getElementById("cancelbutton" + index)
    );
    cancelbuttonElement.classList.add("d-none");
  }

  updateWorkOrderTaskToViewAndInProgress(
    workOrderTaskModel: WorkOrderTaskModel
  ) {
    this.workOrderTaskInProgress = workOrderTaskModel;
    this.workOrderTaskToView = workOrderTaskModel;
    this.workOrderIsComplete = this.isWorkOrderComplete();
    this.getNCRParts();
  }

  closeCurrentTask() {
    this.workOrderTaskTimer.completeTask();
  }

  toggleCancelRemainingStepsDialog() {
    this.showCancelRemainingStepsDialog = !this.showCancelRemainingStepsDialog;
  }

  isWorkOrderComplete(): boolean {
    return (this.workOrderIsComplete =
      this.workOrderModel.workOrderTasks.find(
        (s) =>
          s.status?.id === EnumStatusSteps.WaitingtoStart ||
          s.status?.id === EnumStatusSteps.InProgress ||
          s.status.id === EnumStatusSteps.Approved
      ) === undefined);
  }

  cancelRemainingStepsAndInvoice(invoice: boolean) {
    this.showCancelRemainingStepsDialog = !this.showCancelRemainingStepsDialog;
    this.workOrderIsComplete = this.isWorkOrderComplete();

    let cancelWorkOrderRequest = new CancelWorkOrderRequest({
      workOrderId: this.workOrderModel.id,
      invoiceable: invoice,
    } as ICancelWorkOrderRequest);

    this.globals.showLoader(true);
    this.workOrdersService
      .workOrderCancel(env.apiVersion, cancelWorkOrderRequest)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          if (response.object) {
            this.workOrderModel.workOrderTasks = response.object;
          }
          this.workOrderIsComplete = this.isWorkOrderComplete();
        })
      );
  }

  takeOverThisStep() {
    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.globals.getCurrentUser().id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id,
    } as IUpdateWorkOrderTaskRequest);

    this.globals.showLoader(true);
    this.workOrderTaskService
      .workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest)
      .pipe(take(1))
      .subscribe(
        responseHandler(() => {
          this.workOrderTaskInProgress.assignedTo =
            this.globals.getCurrentUser().id;
          this.workOrderTaskInProgress.assignedToUser =
            this.globals.getCurrentUser();
        })
      );
  }

  addNcrTasks(newWorkOrderTasks: Array<WorkOrderTaskModel>) {
    this.showCarousel = false;

    let indexOfCurrentActiveTask =
      this.workOrderModel.workOrderTasks.findIndex(
        (s) => s.id === this.workOrderTaskInProgress.id
      ) + 1;

    let workOrderTasksToAddBack = new Array<WorkOrderTaskModel>();

    while (
      this.workOrderModel.workOrderTasks.length > indexOfCurrentActiveTask
    ) {
      workOrderTasksToAddBack.push(this.workOrderModel.workOrderTasks.pop());
    }

    newWorkOrderTasks.reverse().map((updatedWorkOrderTask) => {
      workOrderTasksToAddBack.push(updatedWorkOrderTask);
    });

    workOrderTasksToAddBack.reverse().map((workOrderTask) => {
      this.workOrderModel.workOrderTasks.push(workOrderTask);
    });

    this.getDocumentsAndReferenceFilesForProcedureSteps(this.workOrderModel);

    this.changeDetectorRef.detectChanges();
    this.getNCRTaskIds();
    this.showCarousel = true;
  }

  uploadFilesAndDocumentsForTask() {
    this.globals.showLoader(true);
    let updatedWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.workOrderTaskToView.assignedTo,
      status: this.workOrderTaskToView.status.name,
      taskIsRunning: this.workOrderTaskToView.taskIsRunning,
      taskRunningSince: this.workOrderTaskToView.taskRunningSince,
      taskStepOrder: this.workOrderTaskToView.taskStepOrder,
      totalTaskTime: this.workOrderTaskToView.totalTaskTime,
      workOrderTaskId: this.workOrderTaskToView.id,
      referenceFiles: new Array<FileModel>(),
      referenceFilesIds: new Array<number>(),
    } as IUpdateWorkOrderTaskRequest);

    this.workOrderTaskToView.referenceFiles.map((referenceFile) => {
      if (referenceFile.fileId === undefined) {
        updatedWorkOrderTaskRequest.referenceFiles.push(referenceFile);
      } else {
        updatedWorkOrderTaskRequest.referenceFilesIds.push(
          referenceFile.fileId
        );
      }
    });

    this.workOrderTaskService
      .workOrderTaskPatch(env.apiVersion, updatedWorkOrderTaskRequest)
      .pipe(take(1))
      .subscribe((response) => {
        this.workOrderModel.workOrderTasks.find(
          (s) => s.id === this.workOrderTaskToView.id
        ).referenceFiles = response.object.referenceFiles;
      });
  }

  updateNCRPartsMap() {
    let workOrderTask = this.workOrderTaskToView;
    if (
      this.workOrderTaskToView.procedureStepTypeId === ProcedureStepType.NCR
    ) {
      workOrderTask = _.find(
        this.workOrderModel.workOrderTasks,
        (task) =>
          _.some(this.ncrTaskIds, (id) => id === task.id) &&
          task.ncNumber === this.workOrderTaskToView.ncNumber
      );
    }
    let updatedWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: workOrderTask.assignedTo,
      status: workOrderTask.status.name,
      taskIsRunning: workOrderTask.taskIsRunning,
      taskRunningSince: workOrderTask.taskRunningSince,
      taskStepOrder: workOrderTask.taskStepOrder,
      totalTaskTime: workOrderTask.totalTaskTime,
      workOrderTaskId: workOrderTask.id,
      mappedWorkOrderParts: this.ncrParts
        .filter((part) => part.selected)
        .map((part) => {
          return new MappedWorkOrderPart({
            id: part.id,
            tagType: part.tagType,
          } as IMappedWorkOrderPart);
        }),
    } as IUpdateWorkOrderTaskRequest);

    this.workOrderTaskService
      .workOrderTaskPatch(env.apiVersion, updatedWorkOrderTaskRequest)
      .pipe(take(1))
      .subscribe((response) => {
        workOrderTask.mappedWorkOrderParts =
          response.object?.mappedWorkOrderParts || [];
        this.getWorkOrder(this.workOrderModel.id);
      });
  }

  areMonitorsValidCheck() {
    this.areMonitorsValid =
      this.workordertaskmonitors.areMonitorsInValidStateToCloseTask();
    this.tagTypesValid = true;

    if (
      this.workOrderTaskInProgress.procedureStepTypeId === ProcedureStepType.NCR
    ) {
      this.tagTypesValid = !_.some(
        this.ncrParts,
        (part) => part.selected && !part.tagType
      );
    }

    if (this.areMonitorsValid && this.tagTypesValid) {
      this.workordertaskmonitors.saveMonitors(true);
    } else {
      this.monitorsAreInvalidDialog = true;
    }
  }

  showNcrReport() {
    this.printOtherReport.togglePrintOtherDialog();
    this.printOtherReport.selectedReport = "NCRReport";
  }

  setStartDate(startDate: Date) {
    this.workOrderModel.actualStartDate = startDate;
  }

  closeDialog() {
    this.monitorsAreInvalidDialog = false;
    this.globals.showLoader(false);
  }

  showNCRReportByPart(workOrderPartId: any) {
    this.ncrPartDetail = null;
    this.globals.showLoader(true);
    this.workOrderPartService
      .detail(workOrderPartId, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          const ncrPartDetail = response.object;
          if (ncrPartDetail) {
            this.ncrPartDetail = new NCRPartWODetailView(
              ncrPartDetail as INCRPartWODetailView
            );
            this.generateMonitorSummaries();
          }
        })
      );
  }

  hideNCRReportByPart() {
    this.showNCRReportByPartDialog = false;
  }

  print() {
    window.print();
  }

  generateMonitorSummaries() {
    this.ncrPartDetail.workOrderTasks.map((workOrderTask) => {
      if (workOrderTask.files) {
        workOrderTask.files.map((referenceFile) => {
          if (this.getViewerType(referenceFile.contentType) === "img") {
            this.ncrAssociatedDigitalPictures.push(referenceFile);
          } else {
            this.ncrAssociatedDocuments.push(referenceFile);
          }
        });
      }

      if (workOrderTask.documents) {
        workOrderTask.files.map((referenceFile) => {
          if (this.getViewerType(referenceFile.contentType) === "img") {
            this.ncrAssociatedDigitalPictures.push(referenceFile);
          } else {
            this.ncrAssociatedDocuments.push(referenceFile);
          }
        });
      }
    });

    this.showNCRReportByPartDialog = true;
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

  showImagePreviewDialog(fileModel: FileModel) {
    this.imagePreview = fileModel;
    this.showImagePreview = !this.showImagePreview;
  }

  getNCRParts() {
    this.showNCParts = _.some(
      this.ncrTaskIds,
      (id) => id === this.workOrderTaskToView.id
    );
    let workOrderTask = this.workOrderTaskToView;
    if (
      this.workOrderTaskToView.procedureStepTypeId === ProcedureStepType.NCR
    ) {
      workOrderTask = _.find(
        this.workOrderModel.workOrderTasks,
        (task) =>
          _.some(this.ncrTaskIds, (id) => id === task.id) &&
          task.ncNumber === this.workOrderTaskToView.ncNumber
      );
    }
    this.ncrParts = workOrderTask
      ? _.map(this.workOrderModel.workOrderParts, (part) => {
          const mappedWorkOrderParts = _.find(
            workOrderTask.mappedWorkOrderParts,
            (s) => s.id === part.id
          );
          return {
            id: part.id,
            serialNumber: part.serialNumber,
            partNumber: part.part?.partNumber || "",
            selected: !!mappedWorkOrderParts,
            tagType: mappedWorkOrderParts?.tagType || null,
          };
        })
      : [];
  }

  getNCRTaskIds() {
    this.ncrTaskIds = _.map(
      _.groupBy(
        _.filter(
          this.workOrderModel.workOrderTasks || [],
          (task) => !!task.ncNumber
        ),
        "ncNumber"
      ),
      (tasks) => {
        return _.orderBy(tasks, ["taskStepOrder"], ["asc"])[0].id;
      }
    );
  }
}
