import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  IStatusModel, IUpdateWorkOrderTaskRequest, StatusModel, UpdateWorkOrderTaskRequest,
  WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';

@Component({
  selector: 'workordertasktimer-wrapper',
  templateUrl: './workordertasktimer-wrapper.component.html',
  styleUrls: ['./workordertasktimer-wrapper.component.scss'],
  providers: [WorkOrderTaskService, UserService]
})
export class WorkordertasktimerWrapperComponent implements OnInit {

  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Input() workOrderTasks: Array<WorkOrderTaskModel>;
  @Input() workOrderTaskToView: WorkOrderTaskModel;
  @Input() currentUser: UserModel;
  @Output() workOrderTasksChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  @Output() workOrderTaskToViewChange = new EventEmitter<any>();
  @Output() updateWorkOrderTaskToViewAndInProgress = new EventEmitter<any>();

  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;
  timerStartTime: Date;

  constructor(private workOrderTaskService: WorkOrderTaskService, private userService: UserService, private globals: Globals) { }

  ngOnInit(): void {

    this.setTimerDisplay(this.workOrderTaskInProgress.totalTaskTime, true);

    if (this.workOrderTaskInProgress.taskIsRunning === true) {
      this.startTask();
    }

  }

  startTask() {

    this.workOrderTaskInProgress.status.id = 2;
    this.workOrderTaskInProgress.status.name = 'In Progress';
    this.resumeAndStartTask();

  }

  pauseTask() {
    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = undefined;
    clearInterval(this.stepTimer);
    this.saveTaskTimerState(false);
  }

  resumeAndStartTask() {

    this.workOrderTaskInProgress.taskIsRunning = true;
    this.workOrderTaskInProgress.taskRunningSince = new Date();
    this.stepTimer = setInterval(() => {

      this.setTimerDisplay(this.workOrderTaskInProgress.totalTaskTime, true);

    }, 1000);
    this.saveTaskTimerState(false);

  }

  setTimerDisplay(seconds: number, incrementTotalTaskTime: boolean) {

    this.stepSeconds = this.workOrderTaskInProgress.totalTaskTime++;
    this.stepMinutes = Math.floor(this.stepSeconds / 60);
    this.stepHours = Math.floor(this.stepMinutes / 60);

  }

  completeTask() {

    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = null;
    clearInterval(this.stepTimer);
    this.stepSeconds = 0;
    this.stepMinutes = 0;
    this.stepHours = 0;

    this.workOrderTaskInProgress.statusId = 3;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 3,
      name: 'Complete'
    } as IStatusModel);

    this.saveTaskTimerState(true);
    this.setStateOfNextTaskToInProgressIfExist();

  }

  setStateOfNextTaskToInProgressIfExist() {

    let currentTaskIndex = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);

    if (currentTaskIndex + 1 < this.workOrderTasks.length) {
      let nextWorkOrderTask = this.workOrderTasks[currentTaskIndex + 1];
      nextWorkOrderTask.statusId = 2;
      nextWorkOrderTask.status.id = 2;
      nextWorkOrderTask.status.name = 'In Progress';

      let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
        assignedUserId: this.currentUser.id,
        status: nextWorkOrderTask.status.name,
        taskIsRunning: nextWorkOrderTask.taskIsRunning,
        taskRunningSince: nextWorkOrderTask.taskRunningSince,
        totalTaskTime: nextWorkOrderTask.totalTaskTime,
        taskStepOrder: nextWorkOrderTask.taskStepOrder,
        workOrderTaskId: nextWorkOrderTask.id
      } as IUpdateWorkOrderTaskRequest);
      this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(() => { }));
    }


  }

  saveTaskTimerState(closeStep: boolean) {

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.currentUser.id,
      status: closeStep ? 'Complete' : this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      totalTaskTime: this.workOrderTaskInProgress.totalTaskTime,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
    } as IUpdateWorkOrderTaskRequest);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(workOrderTaskPatchResponse => {

      if (closeStep) {
        let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
        if ((indexOfNextTask + 1) > this.workOrderTasks.length) {
          this.workOrderTaskInProgress = undefined;
        } else {
          this.workOrderTaskInProgress = this.workOrderTasks[indexOfNextTask + 1];
          this.workOrderTaskToView = this.workOrderTasks[indexOfNextTask + 1];
          this.updateWorkOrderTaskToViewAndInProgress.emit(this.workOrderTasks[indexOfNextTask + 1]);
        }
      }

    }));





  }

}
