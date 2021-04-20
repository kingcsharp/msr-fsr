import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  Procedure, WorkOrderPartService,
  WorkOrderModel, WorkOrderService, ProcedureStepMonitorService, ProcedureStepModel, CreateWorkOrderTaskRequest, ICreateWorkOrderTaskRequest,
  WorkOrderTaskModel, UpdateWorkOrderTaskRequest, IUpdateWorkOrderTaskRequest,
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { take } from 'rxjs/operators';

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
  workOrderTasks: Array<any>;

  constructor(public globals: Globals, private procedureService: ProcedureService,
    private workOrderTaskService: WorkOrderTaskService) { }

  ngOnInit(): void {
  }

  addNcr() {
    this.globals.showLoader(true);
    this.procedureService.procedureGet(null, null, null, null, null, null, null, null, null,
      null, null, null, null, null, env.apiVersion).subscribe(responseHandler(response => {

      this.ncrProceduresAvailable = response.object.filter(s => s.procedureType.name === 'Conformance Action (NCR)');
      this.showAddNcrDialog = !this.showAddNcrDialog;
    }));
  }

  insertNCR(ncrProcedure: Procedure) {
    this.showAddNcrDialog = !this.showAddNcrDialog;
    this.procedureService.stepGet(ncrProcedure.id, null, env.apiVersion).subscribe(responseHandler((response) => {

      let procedureSteps = <Array<ProcedureStepModel>>response.object;
      this.workOrderTasks = new Array<any>();

      procedureSteps.map((procedureStep) => {
        procedureStep.procedure = ncrProcedure;
      });

      this.addNCRWorkOrderTask(procedureSteps, 0);
    }));
  }

  addNCRWorkOrderTask(procedureSteps, index) {

    let seedStepOrderNumber = this.workOrderTaskInProgress.taskStepOrder;

    let createWorkOrderTaskRequest = new CreateWorkOrderTaskRequest({
      procedureId: procedureSteps[index].procedureId,
      procedureStepId: procedureSteps[index].id,
      workOrderId: this.workOrderModel.id,
      taskStepOrder: seedStepOrderNumber + (index + 1)
    } as ICreateWorkOrderTaskRequest);

    this.globals.showLoader(true);
    this.workOrderTaskService.workOrderTaskPost(env.apiVersion, createWorkOrderTaskRequest).pipe(take(1)).subscribe(responseHandler((postWorkOrderTaskResponse) => {

      let workOrderTask = postWorkOrderTaskResponse.object;
      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.globals.getCurrentUser().id,
        status: 'Waiting to Start',
        taskIsRunning: false,
        taskRunningSince: null,
        taskStepOrder: workOrderTask.taskStepOrder,
        totalTaskTime: 0,
        workOrderTaskId: workOrderTask.id
      } as IUpdateWorkOrderTaskRequest);

      this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).pipe(take(1)).subscribe(responseHandler((patchWorkOrderTaskResponse) => {

        let updatedWorkOrderTask = patchWorkOrderTaskResponse.object;
        updatedWorkOrderTask.procedureStep = procedureSteps[index];
        updatedWorkOrderTask.workOrderTaskMonitors = workOrderTask.workOrderTaskMonitors;

        this.workOrderTasks.push(updatedWorkOrderTask);

        if (index < procedureSteps.length - 1) {
          this.addNCRWorkOrderTask(procedureSteps, index + 1);
        }

        if (index = procedureSteps.length - 1) {
          this.workOrderModel.hasNCR = true;
          this.addNcrTasks.emit(this.workOrderTasks);
        }
      }));
    }));
  }

}
