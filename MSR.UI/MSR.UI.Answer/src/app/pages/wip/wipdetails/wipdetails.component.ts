import { Component, OnInit, ViewEncapsulation, ViewChild, ElementRef, ChangeDetectorRef, Inject, HostListener } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  Customer, Procedure, PurchaseModel, WorkOrderPartService, InvoiceService,
  WorkOrderModel, WorkOrderPartModel, EnumMenuItem, WorkOrderService, WorkOrderTaskModel,
  ProcedureStepMonitorService, FileModel, UpdateWorkOrderPartRequest, IUpdateWorkOrderPartRequest,
  UpdateWorkOrderTaskRequest, IUpdateWorkOrderTaskRequest, ProductModel
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';
import { WorkordertasktimerWrapperComponent } from '../../../components/workordertasktimer-wrapper/workordertasktimer-wrapper.component';
import { SelectWorkOrderDropDownWrapperComponent } from '../../../components/select-work-order-drop-down-wrapper/select-work-order-drop-down-wrapper.component';
import { CarouselComponent } from 'ngx-bootstrap/carousel';
import { SelectItem } from 'primeng/api';

const moment = require('moment');

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss'],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true,
  providers: [WorkOrderService, ProcedureStepMonitorService, WorkOrderPartService, ProcedureService, WorkOrderTaskService,
    LocationService, UserService, UserService, InvoiceService]
})
export class WipdetailsComponent implements OnInit {

  @ViewChild('workordertasktimer') workOrderTaskTimer: WorkordertasktimerWrapperComponent;
  @ViewChild('selectworkorderdropdown') selectWorkOrderDropDown: SelectWorkOrderDropDownWrapperComponent;
  @ViewChild('stepCarousel') carousel: CarouselComponent;
  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModel = new WorkOrderPartModel();
  procedure: Procedure = new Procedure();
  customer: Customer = new Customer();
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
  slideConfig;

  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService, private workOrderPartService: WorkOrderPartService,
    @Inject(ChangeDetectorRef) private changeDetectorRef: ChangeDetectorRef,
    public globals: Globals, private router: Router, private workOrderTaskService: WorkOrderTaskService) { }

  @HostListener('window:resize', ['$event'])
  getScreenSize() {

    if (window.innerWidth > 1100 && window.innerWidth < 1400) {
      this.itemsPerASlide = 7;
    } else if (window.innerWidth > 1400) {
      this.itemsPerASlide = 11;
    }

  }

  ngOnInit(): void {
    this.getScreenSize();

    this.monitorTypes = [
      { label: 'Equipment', value: 1 },
      { label: 'Number', value: 2 },
      { label: 'Yes or No', value: 3 },
      { label: 'Text', value: 4 },
      { label: 'Pass or Fail', value: 5 },
      { label: 'Select', value: 6 },
    ];


    this.route.params.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrdersService.workOrder(workOrderId, null, null, null, null, env.apiVersion).subscribe(responseHandler(response => {


        this.workOrderModel = this.cleanData(response.object[0]);
        this.hasSerializationStep = this.workOrderModel.workOrderTasks.map(s => s.procedureStep.title).find(m => m.trim().toLocaleUpperCase() === 'SERIALIZE') !== undefined;
        this.workOrderIsComplete = this.workOrderModel.workOrderTasks.find(s => s.status.name.trim() === 'Waiting to Start' || s.status.name.trim() === 'In Progress' || s.status.name.trim() === 'Approved') === undefined;
        this.workOrderParts = this.workOrderModel.workOrderParts;
        this.parentPart = this.workOrderModel.workOrderParts[0];
        this.procedure = this.workOrderModel.product.procedure;
        this.customer = this.workOrderModel.product.customer;
        this.product = this.workOrderModel.product;
        this.purchase = this.workOrderModel.purchase;

        // TODO: Remove ! when roles are included in WorkOrder.workOrderTaskModel.procedureStepModel.roles
        if (!this.canUserAccessWorkOrderTask(this.workOrderModel.workOrderTasks[0])) {


          for (let index = 0; index < this.workOrderModel.workOrderTasks.length; index++) {
            if (this.workOrderModel.workOrderTasks[index].status.name === 'In Progress' || this.workOrderModel.workOrderTasks[index].status.name === 'Approved' || this.workOrderModel.workOrderTasks[index].status.name === 'Waiting to Start') {
              this.workOrderTaskInProgress = this.workOrderModel.workOrderTasks[index];
              this.workOrderTaskToView = this.workOrderModel.workOrderTasks[index];
              this.startSlideIndex = index;
              break;
            }
          }

          if (this.workOrderTaskInProgress === undefined) {
            this.workOrderTaskInProgress = this.workOrderModel.workOrderTasks[0];
            this.workOrderTaskToView = this.workOrderModel.workOrderTasks[0];
          }
        }

      }));

    });

  }

  cleanData(workOrderModel: WorkOrderModel): WorkOrderModel {

    workOrderModel.workOrderTasks.sort(function (a, b) {
      return a.taskStepOrder - b.taskStepOrder;
    });

    workOrderModel.workOrderTasks.map(s => {

      if (s.taskIsRunning === null || s.taskIsRunning === undefined) {
        s.taskIsRunning = false;
      }

      if (s.totalTaskTime === null || s.totalTaskTime === undefined) {
        s.totalTaskTime = 0;
      }

      if (s.referenceFiles === undefined) {
        s.referenceFiles = new Array<FileModel>();
      }

      if (s.procedureStep.referenceFiles === undefined) {
        s.procedureStep.referenceFiles = new Array<FileModel>();
      }

      s.workOrderTaskMonitors.map(m => m.procedureStepMonitor).map(u => {

        if (u.monitorTypeId === 1 && u.inputTypeId === 1) {
          u.inputType = 'Manual';
        }

        if (u.monitorTypeId === 2 && u.inputTypeId === 5) {
          u.inputType = 'Manual';
        }

        if (u.monitorTypeId === 2 && u.inputTypeId === 6) {
          u.inputType = 'Sensor';
        }

        if (u.monitorTypeId === 3 && u.inputTypeId === 8) {
          u.inputType = 'Manual';
        }

        if (u.monitorTypeId === 4 && u.inputTypeId === 12) {
          u.inputType = 'Manual';
        }

        if (u.monitorTypeId === 5 && u.inputTypeId === 16) {
          u.inputType = 'Manual';
        }

        if (u.monitorTypeId === 6 && u.inputTypeId === 19) {
          u.inputType = 'Manual';
        }


      });

      s.workOrderTaskMonitors.map(m => m.procedureStepMonitor).map(u => {

        u.monitorType = this.monitorTypes.find(t => t.value === u.monitorTypeId).label;

      });

    });


    return workOrderModel;

  }

  canUserAccessWorkOrderTask(workOrderTask: WorkOrderTaskModel) {
    return workOrderTask.procedureStep?.roles?.map(s => s.name).some(s => this.globals.getCurrentUser().roles?.map(m => m.name).includes(s));
  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel) {

    // TODO: Remove ! when roles are included in WorkOrder.workOrderTaskModel.procedureStepModel.roles
    if (!this.canUserAccessWorkOrderTask(workOrderTask)) {
      this.workOrderTaskToView = workOrderTask;
    }

    if (this.workOrderIsComplete) {
      this.workOrderTaskInProgress = workOrderTask;
    }

  }

  changeSerialNumber(index, serialNumber, partId) {

    if (this.originalSerialNumbers.find(s => s.id === partId) === undefined) {
      this.originalSerialNumbers.push(({
        id: partId, serialNumber: serialNumber
      }));
    }

    let inputElement = <HTMLInputElement>document.getElementById('serialnumber' + index);
    inputElement.disabled = false;

    let changebuttonElement = <HTMLInputElement>document.getElementById('changebutton' + index);
    changebuttonElement.classList.add('d-none');

    let submitbuttonElement = <HTMLInputElement>document.getElementById('submitbutton' + index);
    submitbuttonElement.classList.remove('d-none');

    let cancelbuttonElement = <HTMLInputElement>document.getElementById('cancelbutton' + index);
    cancelbuttonElement.classList.remove('d-none');
  }

  submitSerialNumber(index, partId) {

    let updateWorkOrderRequest = new UpdateWorkOrderPartRequest({
      workOrderPartId: partId,
      serialNumber: this.workOrderModel.workOrderParts.find(s => s.id === partId).serialNumber
    } as IUpdateWorkOrderPartRequest);

    this.globals.showLoader(true);
    this.workOrderPartService.workOrderPart(env.apiVersion, updateWorkOrderRequest).subscribe(responseHandler(() => {

      this.originalSerialNumbers.find(s => s.id === partId).serialNumber = this.workOrderModel.workOrderParts.find(s => s.id === partId).serialNumber;

      let changebuttonElement = <HTMLInputElement>document.getElementById('changebutton' + index);
      changebuttonElement.classList.remove('d-none');

      let submitbuttonElement = <HTMLInputElement>document.getElementById('submitbutton' + index);
      submitbuttonElement.classList.add('d-none');

      let cancelbuttonElement = <HTMLInputElement>document.getElementById('cancelbutton' + index);
      cancelbuttonElement.classList.add('d-none');
    }));
  }

  cancelSerialNumber(index, partId) {

    let originalSerialNumber = this.originalSerialNumbers.find(s => s.id === partId);
    let inputElement = <HTMLInputElement>document.getElementById('serialnumber' + index);
    inputElement.disabled = true;
    inputElement.value = originalSerialNumber.serialNumber;
    this.workOrderModel.workOrderParts.find(s => s.id === partId).serialNumber = originalSerialNumber.serialNumber;

    let changebuttonElement = <HTMLInputElement>document.getElementById('changebutton' + index);
    changebuttonElement.classList.remove('d-none');

    let submitbuttonElement = <HTMLInputElement>document.getElementById('submitbutton' + index);
    submitbuttonElement.classList.add('d-none');

    let cancelbuttonElement = <HTMLInputElement>document.getElementById('cancelbutton' + index);
    cancelbuttonElement.classList.add('d-none');
  }

  updateWorkOrderTaskToViewAndInProgress(workOrderTaskModel: WorkOrderTaskModel) {
    this.workOrderTaskInProgress = workOrderTaskModel;
    this.workOrderTaskToView = workOrderTaskModel;
  }

  closeCurrentTask() {
    this.workOrderTaskTimer.completeTask();
  }

  toggleHideCompletedWorkOrders() {
    this.hideCompletedWorkOrders = !this.hideCompletedWorkOrders;
    this.selectWorkOrderDropDown.updateWorkOrders();
  }

  toggleCancelRemainingStepsDialog() {
    this.showCancelRemainingStepsDialog = !this.showCancelRemainingStepsDialog;
  }

  cancelRemainingStepsAndInvoice(invoice: boolean) {

    if (invoice) {

      this.completeRemainingSteps();

    } else {

      this.cancelRemainingSteps();

    }

  }
  completeRemainingSteps() {

    let indexOfCurrentWorkOrderInProgress = 0;
    if (this.workOrderTaskInProgress != null) {
      indexOfCurrentWorkOrderInProgress = this.workOrderModel.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    }

    for (let index = indexOfCurrentWorkOrderInProgress; index < this.workOrderModel.workOrderTasks.length; index++) {

      let workOrderTaskToClose = this.workOrderModel.workOrderTasks[index];

      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.globals.getCurrentUser().id,
        status: 'Complete',
        taskIsRunning: false,
        taskRunningSince: null,
        taskStepOrder: workOrderTaskToClose.taskStepOrder,
        workOrderTaskId: workOrderTaskToClose.id,
        totalTaskTime: 0
      } as IUpdateWorkOrderTaskRequest);

      this.globals.showLoader(true);
      this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(() => {

        this.router.navigate(['/app/wip/wipstatus']);

      }));

    }
  }

  cancelRemainingSteps() {

    let indexOfCurrentWorkOrderInProgress = 0;
    if (this.workOrderTaskInProgress != null) {
      indexOfCurrentWorkOrderInProgress = this.workOrderModel.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    }

    for (let index = indexOfCurrentWorkOrderInProgress; index < this.workOrderModel.workOrderTasks.length; index++) {

      let workOrderTaskToClose = this.workOrderModel.workOrderTasks[index];

      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.globals.getCurrentUser().id,
        status: 'Cancelled',
        taskIsRunning: false,
        taskRunningSince: workOrderTaskToClose.taskRunningSince,
        taskStepOrder: workOrderTaskToClose.taskStepOrder,
        workOrderTaskId: workOrderTaskToClose.id,
        totalTaskTime: 0
      } as IUpdateWorkOrderTaskRequest);

      this.globals.showLoader(true);
      this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(() => {

        this.router.navigate(['/app/wip/wipstatus']);

      }));

    }
  }

  takeOverThisStep() {

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.globals.getCurrentUser().id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
    } as IUpdateWorkOrderTaskRequest);

    this.globals.showLoader(true);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(() => {
      this.workOrderTaskInProgress.assignedTo = this.globals.getCurrentUser().id;
      this.workOrderTaskInProgress.assignedToUser = this.globals.getCurrentUser();
    }));
  }

  addNcrTasks(newWorkOrderTasks: Array<WorkOrderTaskModel>) {

    this.showCarousel = false;

    let indexOfCurrentActiveTask = this.workOrderModel.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id) + 1;

    let workOrderTasksToAddBack = new Array<WorkOrderTaskModel>();

    while (this.workOrderModel.workOrderTasks.length > (indexOfCurrentActiveTask)) {

      workOrderTasksToAddBack.push(this.workOrderModel.workOrderTasks.pop());

    }

    newWorkOrderTasks.reverse().map((updatedWorkOrderTask) => {

      workOrderTasksToAddBack.push(updatedWorkOrderTask);

    });

    workOrderTasksToAddBack.reverse().map((workOrderTask) => {
      this.workOrderModel.workOrderTasks.push(workOrderTask);
    });

    this.changeDetectorRef.detectChanges();
    this.showCarousel = true;
  }


  slideToTaskInProgress() {
    let index = this.workOrderModel.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    this.carousel.selectSlide(index);
  }

  uploadFilesAndDocumentsForTask() {
    // TODO: Uncomment when backend change comes in from David
    /*
    let updatedWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.workOrderTaskInProgress.assignedTo,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      totalTaskTime: this.workOrderTaskInProgress.totalTaskTime,
      workOrderTaskId: this.workOrderTaskInProgress.id,
      referenceFiles: new Array<FileModel>(),
      referenceFileIds: new Array<number>()
    } as IUpdateWorkOrderTaskRequest);


    this.workOrderTaskInProgress.referenceFiles.map(referenceFile => {

      if (referenceFile.fileId === undefined) {
        updatedWorkOrderTaskRequest.referenceFiles.push(referenceFile);
      } else {
        updatedWorkOrderTaskRequest.referenceFileIds.push(referenceFile.fileId);
      }

    });

    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion,updatedWorkOrderTaskRequest).subscribe(response => {

      this.workOrderModel.workOrderTasks.find(s => s.id === this.workOrderTaskInProgress.id).referenceFiles = response.object.referenceFiles;

    });
    */
  }
}
