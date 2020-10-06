import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  Procedure, WorkOrderPartService,
  WorkOrderModel, WorkOrderService, ProcedureStepMonitorService, ProcedureStepModel, CreateWorkOrderTaskRequest, ICreateWorkOrderTaskRequest,
  WorkOrderTaskModel, WorkOrderTaskMonitorModel, UpdateWorkOrderTaskRequest, IUpdateWorkOrderTaskRequest, AuditActionResultOfWorkOrderTaskModel
} from '../../services/api.client.generated';
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
  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Output() workOrderModelChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  @Output() addNcrTasks = new EventEmitter<any>();
  showAddNcrDialog: boolean = false;
  ncrProceduresAvailable: Array<Procedure>;

  constructor(public globals: Globals, private procedureService: ProcedureService,
    private workOrderTaskService: WorkOrderTaskService) { }

  ngOnInit(): void {
  }

  addNcr() {

    this.globals.showLoader(true);
    this.procedureService.procedureGet(null, env.apiVersion).subscribe(responseHandler(response => {

      this.ncrProceduresAvailable = response.object.filter(s => s.procedureType.name === 'Conformance Action (NCR)');
      this.showAddNcrDialog = !this.showAddNcrDialog;

    }));

  }

  insertNCR(ncrProcedure: Procedure) {

    this.showAddNcrDialog = !this.showAddNcrDialog;
    this.procedureService.stepGet(ncrProcedure.id, null, env.apiVersion).subscribe(responseHandler((response) => {

      let procedureSteps = <Array<ProcedureStepModel>>response.object;
      let seedStepOrderNumber = this.workOrderTaskInProgress.taskStepOrder;
      let arrayOfPostWorkOrderTaskRequests = new Array<any>();

      procedureSteps.map((procedureStep, index) => {

        let createWorkOrderTaskRequest = new CreateWorkOrderTaskRequest({
          procedureId: procedureStep.procedureId,
          procedureStepId: procedureStep.id,
          workOrderId: this.workOrderModel.id,
          taskStepOrder: seedStepOrderNumber + (index + 1)
        } as ICreateWorkOrderTaskRequest);


        arrayOfPostWorkOrderTaskRequests.push(this.workOrderTaskService.workOrderTaskPost(env.apiVersion, createWorkOrderTaskRequest));


      });

      forkJoin(arrayOfPostWorkOrderTaskRequests).subscribe((postWorkOrderTaskResponses) => {
        this.setDefaultsOfWorkOrderTasks(postWorkOrderTaskResponses.map(s => (<AuditActionResultOfWorkOrderTaskModel>s).object), procedureSteps);
      });


    }));

  }

  setDefaultsOfWorkOrderTasks(workOrderTasks: Array<WorkOrderTaskModel>, procedureSteps: Array<ProcedureStepModel>) {

    let arrayOfPatchWorkOrderTaskRequests = new Array<any>();
    workOrderTasks.map((workOrderTask) => {

      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.globals.getCurrentUser().id,
        status: 'Waiting to Start',
        taskIsRunning: false,
        taskRunningSince: null,
        taskStepOrder: workOrderTask.taskStepOrder,
        totalTaskTime: 0,
        workOrderTaskId: workOrderTask.id
      } as IUpdateWorkOrderTaskRequest);

      arrayOfPatchWorkOrderTaskRequests.push(this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest));
    });

    forkJoin(arrayOfPatchWorkOrderTaskRequests).subscribe((patchWorkOrderTaskResponses) => {

      let updatedWorkOrderTasks = patchWorkOrderTaskResponses.map(s => (<AuditActionResultOfWorkOrderTaskModel>s).object);
      updatedWorkOrderTasks.map(workOrderTask => {

        workOrderTask.procedureStep = procedureSteps.find(s => s.id === workOrderTask.procedureStepId);

      });

      this.addNcrTasks.emit(updatedWorkOrderTasks);
    });


  }

}
