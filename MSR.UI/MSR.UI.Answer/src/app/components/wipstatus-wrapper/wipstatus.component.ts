import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Router } from '@angular/router';
import { UpdateWorkOrderTaskRequest, WorkOrderService, WorkOrderStatus, WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { forkJoin } from 'rxjs';

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
  workOrderStatuses: Array<WorkOrderStatus>;
  displayWorkOrderStatuses: Array<WorkOrderStatus> = new Array<WorkOrderStatus>();
  locationOptions: Array<SelectItem> = new Array<SelectItem>();
  selectedLocations: Array<string>;
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOverId: number;
  constructor(private router: Router, private workOrderService: WorkOrderService, public globals: Globals,
    private workOrderTaskService: WorkOrderTaskService, private userService: UserService) { }


  ngOnInit(): void {

    if(!this.isDisplayedInWipList){
      this.globals.showLoader(true);
    }
    
    this.workOrderService.status(env.apiVersion).subscribe(responseHandler(response => {

      //TODO: Remove when data is clean
      response.object.map(workOrderStatus => {

        if (workOrderStatus.workOrderSummary.workOrderStatus === 'Waiting Start') {
          workOrderStatus.workOrderSummary.workOrderStatus = 'Waiting to Start';
        }

      });

      this.workOrderStatuses = response.object;
      this.displayWorkOrderStatuses = response.object;

      this.locationOptions = this.workOrderStatuses?.map(s => s.locationName).filter((v, i, a) => a.indexOf(v) === i).map(s => ({ label: s, value: s }));

      if(this.isDisplayedInWipList){
        this.selectedLocations = this.locationOptions.map(s => s.value);
      } else{

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

  openTakeOverAsUserConfirmationDialog(workOrderId: number) {

    if(this.isDisplayedInWipList){
      this.router.navigate(['app/wip/details', workOrderId]);
      this.displayWipListDialogChange.emit(false);
    }else {
      this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
      this.workOrderToTakeOverId = workOrderId;
    }
  }

  closeTakeOverAsUserConfirmationDialog() {
    this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
  }

  takeOverAsUserConfirmationDialog() {

    if (this.takeOverSteps) {

      this.globals.showLoader(true);
      this.userService.loggedInUser(env.apiVersion).subscribe(responseHandler(response => {

        let loggedInUser = <UserModel>response.object;

        this.globals.showLoader(true);
        this.workOrderService.workOrder(this.workOrderToTakeOverId, null, null, null, env.apiVersion).subscribe(responseHandler(response => {

          let tasks = <Array<WorkOrderTaskModel>>response.object[0].workOrderTasks;

          let workOrderTaskPatchRequests = new Array<any>();

          tasks.forEach(task => {

            if (task.status?.name === 'Waiting to Start') {

              let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest();
              updateWorkOrderTaskRequest.status = task.status?.name;
              updateWorkOrderTaskRequest.taskIsRunning = false;
              updateWorkOrderTaskRequest.taskRunningSince = task.taskRunningSince;
              updateWorkOrderTaskRequest.taskStepOrder = task.taskStepOrder;
              updateWorkOrderTaskRequest.workOrderTaskId = task.id;
              updateWorkOrderTaskRequest.assignedUserId = loggedInUser.id;

              workOrderTaskPatchRequests.push(this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest));

            }

          });

          // TODO: Uncomment and use actual request when Endpoint is fixed
          this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
          //this.globals.showLoader(true);
          //forkJoin(workOrderTaskPatchRequests).subscribe(responseHandler(responses => {
          //  this.router.navigate(['app/wip/details', this.workOrderToTakeOverId]);
          //}));

        }));

      }));



    }

  }

  locationsSelectedUpdated() {
    localStorage.setItem('wipstatus', this.selectedLocations.toString());
    this.displayWorkOrderStatuses = this.workOrderStatuses.filter(s => this.selectedLocations.includes(s.locationName));
  }

}