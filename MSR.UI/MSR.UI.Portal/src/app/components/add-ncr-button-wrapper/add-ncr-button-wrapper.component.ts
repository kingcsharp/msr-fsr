import {  Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  Procedure, WorkOrderPartService,
  WorkOrderModel, WorkOrderService, ProcedureStepMonitorService, ProcedureStepModel, CreateWorkOrderTaskRequest, ICreateWorkOrderTaskRequest,
  AuditActionResultOfWorkOrderTaskModel, UpdateWorkOrderTaskRequest, IUpdateWorkOrderTaskRequest, StatusModel,
  IStatusModel} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'addncrbutton-wrapper',
  templateUrl: './add-ncr-button-wrapper.component.html',
  styleUrls: ['./add-ncr-button-wrapper.component.scss'],
  providers: [WorkOrderService, ProcedureStepMonitorService, WorkOrderPartService, ProcedureService, WorkOrderTaskService,
    LocationService, UserService]
})
export class AddNcrButtonWrapperComponent implements OnInit {

  @Input() workOrderModel: WorkOrderModel;
  @Input() workOrderTaskInProgress: WorkOrderModel;
  @Output() workOrderModelChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  showAddNcrDialog: boolean = false;
  ncrProceduresAvailable: Array<Procedure>;

  constructor(public globals: Globals, private procedureService: ProcedureService,
    private workOrderTaskService: WorkOrderTaskService) { }

  ngOnInit(): void {
  }

  addNcr() {

    this.globals.showLoader(true);
    this.procedureService.procedureGet(null, env.apiVersion).subscribe(responseHandler(response => {

      this.ncrProceduresAvailable = response.object.filter(s => s.procedureType.name === 'Non-Conformation Operation');
      this.showAddNcrDialog = !this.showAddNcrDialog;

    }));

  }

  insertNCR(ncrProcedure: Procedure) {

    this.procedureService.stepGet(ncrProcedure.id, null, env.apiVersion).subscribe(responseHandler((response) => {

      let procedureSteps = <Array<ProcedureStepModel>>response.object;
      let indexOfWorkOrderTaskInProgress = this.workOrderModel.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
      let numberOfStepsToAdd = procedureSteps.length;
      let arrayOfPatchWorkOrderTaskRequests = new Array<any>();

      for (let index = indexOfWorkOrderTaskInProgress + 1; index < this.workOrderModel.workOrderTasks?.length; index++) {

        this.workOrderModel.workOrderTasks[index].taskStepOrder = index + numberOfStepsToAdd;
        let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
          assignedUserId: this.workOrderModel.workOrderTasks[index].assignedTo,
          status: this.workOrderModel.workOrderTasks[index].status.name,
          taskIsRunning: this.workOrderModel.workOrderTasks[index].taskIsRunning,
          taskRunningSince: this.workOrderModel.workOrderTasks[index].taskRunningSince,
          taskStepOrder: this.workOrderModel.workOrderTasks[index].taskStepOrder,
          workOrderTaskId: this.workOrderModel.workOrderTasks[index].id
        } as IUpdateWorkOrderTaskRequest);

        arrayOfPatchWorkOrderTaskRequests.push(this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest));
      }

      forkJoin(arrayOfPatchWorkOrderTaskRequests).subscribe(() => {

        let arrayOfPostWorkOrderTaskRequests = new Array<any>();

        let newTaskStepOrder = this.workOrderModel.workOrderTasks[indexOfWorkOrderTaskInProgress].taskStepOrder + 1;
        procedureSteps.forEach(procedureStep => {

          let createWorkOrderTaskRequest = new CreateWorkOrderTaskRequest({
            procedureId: procedureStep.procedureId,
            procedureStepId: procedureStep.id,
            workOrderId: this.workOrderModel.id,
            taskStepOrder: newTaskStepOrder++
          } as ICreateWorkOrderTaskRequest);

          arrayOfPostWorkOrderTaskRequests.push(this.workOrderTaskService.workOrderTaskPost(env.apiVersion, createWorkOrderTaskRequest));

        });

        forkJoin(arrayOfPostWorkOrderTaskRequests).subscribe(responses => {

          this.getProcedureSteps(responses as Array<AuditActionResultOfWorkOrderTaskModel>);

        });

      });

    }));

  }

  getProcedureSteps(responses: Array<AuditActionResultOfWorkOrderTaskModel>) {
    window.location.reload();
    responses.map(s => {

      let sample = s as AuditActionResultOfWorkOrderTaskModel;
      let newWorkOrderTaskModel = sample.object;

      if (newWorkOrderTaskModel.status === undefined && newWorkOrderTaskModel.statusId === 1) {
        newWorkOrderTaskModel.status = new StatusModel({
          id: 1,
          name: 'Approved'
        } as IStatusModel);
      }
      newWorkOrderTaskModel.assignedTo = this.globals.getCurrentUser();
      newWorkOrderTaskModel.assignedTo = this.globals.getCurrentUser()?.id;
      this.workOrderModel.workOrderTasks.push(newWorkOrderTaskModel);
    });

    this.workOrderModel.workOrderTasks = this.workOrderModel.workOrderTasks;
    this.workOrderModel.workOrderTasks.sort((a, b) => (a.taskStepOrder > b.taskStepOrder) ? 1 : -1);
    this.showAddNcrDialog = !this.showAddNcrDialog;

  }

}
