import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Router } from '@angular/router';
import { UpdateWorkOrderTaskRequest, WorkOrderService, WorkOrderStatus, WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel, WorkOrderSummary } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { SignalRService } from '../../services/signalr.service';
import { Globals } from '../../models/lib/globals';
import { forkJoin } from 'rxjs';
import { AxisDateTimeLabelFormatsOptions } from 'highcharts';
import { take } from 'rxjs/operators';

@Component({
  selector: 'wipstatus',
  templateUrl: './wipstatus.component.html',
  styleUrls: ['./wipstatus.component.scss'],
  providers: [WorkOrderService, WorkOrderTaskService, UserService]
})
export class WipstatusWrapperComponent implements OnInit {

  @Input() takeOverSteps: boolean = false;
  @Input() isDisplayedInWipList: boolean = false;
  @Input() displayWipListDialog: boolean;
  @Output() displayWipListDialogChange = new EventEmitter();
  workOrderStatuses: Array<any>;
  displayWorkOrderStatuses: Array<any> = new Array<any>();
  locationOptions: Array<SelectItem> = new Array<SelectItem>();
  selectedLocations: Array<string>;
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOverId: number;
  currentDate: Date = new Date;
  warningDate: Date = new Date(new Date().setDate(new Date().getDate() - 1));
  constructor(private router: Router, private workOrderService: WorkOrderService, public globals: Globals,
    private workOrderTaskService: WorkOrderTaskService, private userService: UserService,
    private signalrService: SignalRService) { }


  ngOnInit(): void {

    if (!this.isDisplayedInWipList) {
      this.globals.showLoader(true);
    }
    this.signalrService.subscribeWorkOrderUpdate((data) =>
        this.workOrderStatusUpdate(data));

    this.workOrderService.status(env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {

      let workOrderStatuses = new Array<any>();

      response.object.map(workOrderStatus => {
        this.displayWorkOrderByProduct(workOrderStatuses, workOrderStatus);
      });

      this.workOrderStatuses = workOrderStatuses;
      this.displayWorkOrderStatuses = workOrderStatuses;

      this.locationOptions = this.workOrderStatuses?.map(s => s.locationName).filter((v, i, a) => a.indexOf(v) === i).map(s => ({ label: s, value: s }));

      if (this.isDisplayedInWipList) {
        this.selectedLocations = this.locationOptions.map(s => s.value);
      } else {

        if (localStorage.getItem('wipstatus') === undefined || localStorage.getItem('wipstatus') === null) {
          this.selectedLocations = this.locationOptions.map(s => s.value);
          localStorage.setItem('wipstatus', this.selectedLocations.toString());
        } else {

          let locationsAlreadySaved = localStorage.getItem('wipstatus').split(',');
          let newlocationSavedList = new Array<string>();
          locationsAlreadySaved.forEach(locationSaved => {

            if (this.locationOptions.find(s => s.value === locationSaved) !== undefined) {
              newlocationSavedList.push(locationSaved);
            }

          });
          localStorage.setItem('wipstatus', newlocationSavedList.toString());
          this.selectedLocations = this.locationOptions.map(s => s.value).filter(m => newlocationSavedList.includes(m));
          this.locationsSelectedUpdated();
        }

      }


    }));

  }

  displayWorkOrderByProduct(workOrderStatuses: Array<any>, workOrderStatus: any) {
    let workOrderStatusToUse = workOrderStatuses.find(s =>
      s.productName === workOrderStatus.productName &&
      s.locationName === workOrderStatus.locationName);

    if (workOrderStatusToUse === undefined) {

      let newWorkOrderStatus = {
        productName: workOrderStatus.productName,
        partNumber: workOrderStatus.partNumber,
        procedureName: workOrderStatus.procedureName,
        locationName: workOrderStatus.locationName,
        workOrderSummaries: new Array<WorkOrderSummary>()
      };

      newWorkOrderStatus.workOrderSummaries.push(workOrderStatus.workOrderSummary);

      workOrderStatuses.push(newWorkOrderStatus);

    } else {

      workOrderStatusToUse.workOrderSummaries.push(workOrderStatus.workOrderSummary);

    }
  }

  workOrderStatusUpdate(data): void {
    let workOrderSummary : WorkOrderSummary = undefined;
    for (let productIndex : number = 0;
         productIndex < this.workOrderStatuses.length;
         productIndex += 1) {
      let product = this.workOrderStatuses[productIndex];
      for (let summaryIndex : number = 0;
           summaryIndex < product.workOrderSummaries.length;
           summaryIndex += 1) {
        if (product.workOrderSummaries[summaryIndex].workOrderId !==
            data.workOrderId) {
          continue;
        }

        workOrderSummary = product.workOrderSummaries[summaryIndex];

        if (data.workOrderStatus === 'Complete' ||
            data.workOrderStatus === 'Cancelled') {
          product.workOrderSummaries.splice(summaryIndex, 1);
        } else {
          workOrderSummary.workOrderStatus = data.workOrderStatus;
        }
        break;
      }

      // product has no more work orders, remove it
      if (product.workOrderSummaries.length === 0) {
        this.workOrderStatuses.splice(productIndex, 1);
      }

      if (workOrderSummary) {
        break;
      }
    }
    if (workOrderSummary === undefined &&
        data.workOrderStatus === 'Waiting to Start') {
        // work order created
        data.workOrderSummary = {
            workOrderId: data.workOrderId,
            workOrderStatus: data.workOrderStatus,
            workOrderItemNumber: data.customerName.toUpperCase() + '-' + data.workOrderId,
            workOrderPartSerialNumber: data.serialNumber,
            procedureName: data.procedureName
        };
        this.displayWorkOrderByProduct(this.workOrderStatuses, data);
    }
    // refresh table
    this.locationsSelectedUpdated()
  }

  openTakeOverAsUserConfirmationDialog(workOrderId: number, assignedToFullName: string) {

    if (this.isDisplayedInWipList) {
      this.router.navigate(['app/wip/details', workOrderId]);
      this.displayWipListDialogChange.emit(false);
    } else {

      let currentUsersFullName = this.globals.getCurrentUser().fullName;
      if (assignedToFullName === currentUsersFullName) {
        this.router.navigate(['app/wip/details', workOrderId]);
      }

      this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
      this.workOrderToTakeOverId = workOrderId;
    }
  }

  closeTakeOverAsUserConfirmationDialog() {
    this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
  }

  takeOverAsUserConfirmationDialog() {

    if (this.takeOverSteps) {

      this.closeTakeOverAsUserConfirmationDialog();
      this.globals.showLoader(true);
      this.userService.loggedInUser(env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {

        let loggedInUser = <UserModel>response.object;

        this.globals.showLoader(true);
        this.workOrderService.workOrder(this.workOrderToTakeOverId, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler(workOrderGetResponse => {

          let tasks = <Array<WorkOrderTaskModel>>workOrderGetResponse.object[0].workOrderTasks;

          let workOrderTaskPatchRequests = new Array<any>();

          tasks.map(task => {

            if (task.status?.name === 'Waiting to Start' || task.status?.name === 'Approved' || task.assignedTo !== this.globals.getCurrentUser().id) {

              let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest();
              updateWorkOrderTaskRequest.status = 'Waiting to Start';
              updateWorkOrderTaskRequest.taskIsRunning = false;
              updateWorkOrderTaskRequest.taskRunningSince = task.taskRunningSince;
              updateWorkOrderTaskRequest.taskStepOrder = task.taskStepOrder;
              updateWorkOrderTaskRequest.workOrderTaskId = task.id;
              updateWorkOrderTaskRequest.assignedUserId = loggedInUser.id;

              workOrderTaskPatchRequests.push(this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest));

            }

          });

          if (workOrderTaskPatchRequests.length === 0) {
            this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
          } else {
            this.globals.showLoader(true);
            forkJoin(workOrderTaskPatchRequests).subscribe(responseHandler(responses => {
              this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
            }));
          }
        }));
      }));
    }
  }

  locationsSelectedUpdated() {
    localStorage.setItem('wipstatus', this.selectedLocations.toString());
    this.displayWorkOrderStatuses = this.workOrderStatuses.filter(s => this.selectedLocations.includes(s.locationName));
  }

  isLate(currDate): boolean {
    let thisDate = currDate;
    return this.workOrderStatuses.filter((workOrderStatus: any) =>
      thisDate < this.currentDate).length > 0;
  }
  isWarning(currDate): boolean {
    let thisDate = currDate;
    return this.workOrderStatuses.filter((workOrderStatus: any) =>
      thisDate <= this.warningDate && !(thisDate < this.currentDate)).length > 0;
  }

}
