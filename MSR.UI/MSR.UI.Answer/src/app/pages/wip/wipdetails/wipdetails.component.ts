import { Component, OnInit, ViewEncapsulation, ElementRef, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  Customer, IStatusModel, PartModel, Procedure, ProcedureStepMonitor, ProductModel, PurchaseModel, StatusModel, WorkOrderPartService,
  WorkOrderModel, WorkOrderPartModel, EnumMenuItem, WorkOrderService, WorkOrderTaskModel, ProcedureStepMonitorService, WorkOrderTaskMonitorModel, FileModel, UpdateWorkOrderPartRequest, IUpdateWorkOrderPartRequest, Role, IRole, ProcedureStepModel, CreateWorkOrderTaskRequest, ICreateWorkOrderTaskRequest, AuditActionResultOfWorkOrderTaskModel, UpdateWorkOrderTaskRequest, IUpdateWorkOrderTaskRequest, LocationModel
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Product } from '../../ecommerce/products.service';
import { Globals } from '../../../models/lib/globals';
import { forkJoin } from "rxjs";
import { tap } from "rxjs/operators";
import { SelectItem } from 'primeng/api';
import { WorkordertasktimerWrapperComponent } from '../../../components/workordertasktimer-wrapper/workordertasktimer-wrapper.component';
import { SelectWorkOrderDropDownWrapperComponent } from '../../../components/select-work-order-drop-down-wrapper/select-work-order-drop-down-wrapper.component';

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss'],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true,
  providers: [WorkOrderService, ProcedureStepMonitorService, WorkOrderPartService, ProcedureService, WorkOrderTaskService, LocationService, UserService]
})
export class WipdetailsComponent implements OnInit {

  @ViewChild('workordertasktimer') workOrderTaskTimer: WorkordertasktimerWrapperComponent;
  @ViewChild('selectworkorderdropdown') selectWorkOrderDropDown: SelectWorkOrderDropDownWrapperComponent;
  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModel = new WorkOrderPartModel();
  procedure: Procedure = new Procedure();
  customer: Customer = new Customer();
  product: Product = new Product();
  purchase: PurchaseModel = new PurchaseModel();
  workOrderParts: Array<WorkOrderPartModel> = new Array<WorkOrderPartModel>()
  workOrderTaskInProgress: WorkOrderTaskModel;
  workOrderTaskToView: WorkOrderTaskModel;
  menuItems = EnumMenuItem;
  originalSerialNumbers: Array<any> = new Array<any>();
  hideCompletedWorkOrders: boolean = false;
  showCancelRemainingStepsDialog: boolean = false;
  activeSlideIndex = 0;
  
  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService, private procedureStepMonitorService: ProcedureStepMonitorService,
    private workOrderPartService: WorkOrderPartService, public globals: Globals, private procedureService: ProcedureService, private router: Router,
    private workOrderTaskService: WorkOrderTaskService, private locationService: LocationService, private userService: UserService) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrdersService.workOrder(workOrderId, null, null, null, null, env.apiVersion).subscribe(responseHandler(response => {


        this.workOrderModel = this.cleanData(response.object[0]);
        this.workOrderParts = this.workOrderModel.workOrderParts;
        this.parentPart = this.workOrderModel.workOrderParts[0];
        this.procedure = this.workOrderModel.product.procedure;
        this.customer = this.workOrderModel.product.customer;
        this.product = this.workOrderModel.product;
        this.purchase = this.workOrderModel.purchase;

        // TODO: Remove ! when roles are included in WorkOrder.workOrderTaskModel.procedureStepModel.roles
        if (!this.canUserAccessWorkOrderTask(this.workOrderModel.workOrderTasks[0])) {

          
          for(let index = 0; index < this.workOrderModel.workOrderTasks.length; index++){
            if(this.workOrderModel.workOrderTasks[index].status.name === 'In Progress'  || this.workOrderModel.workOrderTasks[index].status.name === 'Approved' || this.workOrderModel.workOrderTasks[index].status.name === 'Waiting to Start'){
              this.workOrderTaskInProgress = this.workOrderModel.workOrderTasks[index];
              this.workOrderTaskToView = this.workOrderModel.workOrderTasks[index];
              break;
            }
          }
        }

      }));

    });

  }

  cleanData(workOrderModel: WorkOrderModel): WorkOrderModel {

    workOrderModel.workOrderTasks.sort((a, b) => (a.taskStepOrder > b.taskStepOrder) ? 1 : -1);
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

    });

    return workOrderModel;

  }

  canUserAccessWorkOrderTask(workOrderTask: WorkOrderTaskModel) {
    return workOrderTask.procedureStep?.roles?.map(s => s.name).some(s => this.globals.getCurrentUser().roles?.map(s => s.name).includes(s));
  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel) {

    // TODO: Remove ! when roles are included in WorkOrder.workOrderTaskModel.procedureStepModel.roles
    if (!this.canUserAccessWorkOrderTask(workOrderTask)) {
      this.workOrderTaskToView = workOrderTask;
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
    this.workOrderPartService.workOrderPart(env.apiVersion, updateWorkOrderRequest).subscribe(responseHandler(response => {

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

    let originalSerialNumber = this.originalSerialNumbers.find(s => s.id === partId)
    let inputElement = <HTMLInputElement>document.getElementById('serialnumber' + index);
    inputElement.disabled = true;
    inputElement.value = originalSerialNumber.serialNumber;

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

  cancelRemainingSteps(invoice: boolean) {

    if (invoice) {

      // TODO: Call Invoice

    }

    let indexOfCurrentWorkOrderInProgress = 0;
    if (this.workOrderTaskInProgress != null) {
        indexOfCurrentWorkOrderInProgress = this.workOrderModel .workOrderTasks .findIndex(s => s.id === this.workOrderTaskInProgress.id);
    }

    for (let index = indexOfCurrentWorkOrderInProgress; index < this.workOrderModel.workOrderTasks.length; index++) {

      let workOrderTaskToClose = this.workOrderModel.workOrderTasks[index];

      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.globals.getCurrentUser().id,
        status: 'Cancelled',
        taskIsRunning: false,
        taskRunningSince: workOrderTaskToClose.taskRunningSince,
        taskStepOrder: workOrderTaskToClose.taskStepOrder,
        workOrderTaskId: workOrderTaskToClose.id
      } as IUpdateWorkOrderTaskRequest);

      this.globals.showLoader(true);
      this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(response => {

        this.router.navigate(['/app/wip/wipstatus']);

      }));

    }

  }

}
