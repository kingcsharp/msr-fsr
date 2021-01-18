import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import {
  IStatusModel, IUpdateWorkOrderTaskRequest, StatusModel, UpdateWorkOrderTaskRequest,
  WorkOrderTaskModel, WorkOrderTaskService, UserService, UserModel
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import * as moment from 'moment';
import { Router } from '@angular/router';


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
  @Input() workOrderIsComplete: boolean = false;
  @Input() hasAccessToTaskBeingViewed: boolean = true;
  @Input() areMonitorsValid: boolean = false;
  @Output() workOrderTasksChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  @Output() workOrderTaskToViewChange = new EventEmitter<any>();
  @Output() updateWorkOrderTaskToViewAndInProgress = new EventEmitter<any>();
  @Output() areMonitorsValidCheck = new EventEmitter<{ areValid: Function }>();

  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;
  timerStartTime: Date;

  constructor(private workOrderTaskService: WorkOrderTaskService, private userService: UserService, private globals: Globals, private router: Router) { }

  ngOnInit(): void {

    this.startTimer();

  }

  ngOnChanges() {
    if (this.workOrderIsComplete) {
      this.updateTimerDisplay();
    }

  }

  completeTask() {

    this.areMonitorsValidCheck.emit({
      areValid: (result) => {

        this.workOrderTaskInProgress.taskIsRunning = false;
        this.workOrderTaskInProgress.taskRunningSince = null;
        this.stopTimer();
        this.resetTimerDisplay();
        this.setTaskToCompleted();
        this.resetTimerDisplay();
        this.workOrderTaskInProgress.lastUpdatedOn = moment().toDate();

        this.saveTaskTimerState(true);

      }
    });
  }

  saveTaskTimerState(closeStep: boolean) {

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.globals.getCurrentUser().id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince === null ? null : this.convertDateToUTC(this.workOrderTaskInProgress.taskRunningSince),
      totalTaskTime: this.workOrderTaskInProgress.totalTaskTime,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id,
      startedOn: this.workOrderTaskInProgress.startedOn === null ? null : this.convertDateToUTC(this.workOrderTaskInProgress.startedOn)
    } as IUpdateWorkOrderTaskRequest);

    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(workOrderTaskPatchResponse => {

      if (closeStep) {
        let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
        if ((indexOfNextTask + 1) === this.workOrderTasks.length) {
          this.router.navigate(['/app/wip/wipstatus']);
        } else {

          let nextWorkOrderTask = this.workOrderTasks[indexOfNextTask + 1];
          nextWorkOrderTask.statusId = 2;
          nextWorkOrderTask.status = new StatusModel({
            id: 2,
            name: 'In Progress'
          } as IStatusModel);

          let updateNextWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
            assignedUserId: this.globals.getCurrentUser().id,
            status: 'In Progress',
            taskIsRunning: nextWorkOrderTask.taskIsRunning,
            taskRunningSince: nextWorkOrderTask.taskRunningSince,
            totalTaskTime: nextWorkOrderTask.totalTaskTime,
            taskStepOrder: nextWorkOrderTask.taskStepOrder,
            workOrderTaskId: nextWorkOrderTask.id,
            startedOn: nextWorkOrderTask.startedOn
          } as IUpdateWorkOrderTaskRequest);

          this.globals.showLoader(true);
          this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateNextWorkOrderTaskRequest).subscribe(responseHandler(nextWorkOrderTaskPatchResponse => {
            this.updateWorkOrderTaskToViewAndInProgress.emit(nextWorkOrderTask);
            this.resetTimerDisplay();
          }));


        }
      }

    }));

  }


  start() {
    this.globals.showLoader(true);
    this.setTaskToInProgressStatus();
    this.workOrderTaskInProgress.startedOn = moment().toDate();
    this.workOrderTaskInProgress.taskIsRunning = true;
    this.workOrderTaskInProgress.taskRunningSince = moment().toDate();
    this.startTimer();
    this.saveTaskTimerState(false);
  }

  pause() {
    this.globals.showLoader(true);
    this.stopTimer();
    this.updateTotalTaskTime();
    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = null;
    this.saveTaskTimerState(false);
  }

  resume() {
    this.globals.showLoader(true);
    this.workOrderTaskInProgress.taskIsRunning = true;
    this.workOrderTaskInProgress.taskRunningSince = moment().toDate();
    this.startTimer();
    this.saveTaskTimerState(false);
  }

  done() {
    this.globals.showLoader(true);
    this.stopTimer();
    this.setTaskToCompleted();
    this.updateTotalTaskTime();
    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = null;
    this.saveTaskTimerState(true);
  }

  startTimer() {
    this.stepTimer = setInterval(() => {

      this.updateTimerDisplay();

    }, 1000);
  }

  stopTimer() {
    clearInterval(this.stepTimer);
  }

  updateTimerDisplay() {

    if (this.workOrderTaskInProgress.taskIsRunning) {

      const taskHasBeenRunningSince = moment(this.workOrderTaskInProgress.taskRunningSince);
      const currentDate = moment();

      this.stepSeconds = this.workOrderTaskInProgress.totalTaskTime + (-taskHasBeenRunningSince.diff(currentDate, 'seconds'));

    } else {
      this.stepSeconds = this.workOrderTaskInProgress.totalTaskTime;
    }

    this.stepMinutes = Math.floor(this.stepSeconds / 60);
    this.stepHours = Math.floor(this.stepMinutes / 60);

  }

  resetTimerDisplay() {
    this.stepSeconds = 0;
    this.stepMinutes = 0;
    this.stepHours = 0;
  }

  setTaskToInProgressStatus() {
    this.workOrderTaskInProgress.statusId = 2;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 2,
      name: 'In Progress'
    } as IStatusModel);
  }

  setTaskToCompleted() {
    this.workOrderTaskInProgress.statusId = 3;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 3,
      name: 'Complete'
    } as IStatusModel);
  }

  updateTotalTaskTime() {
    const taskHasBeenRunningSince = moment(this.workOrderTaskInProgress.taskRunningSince);
    const currentDate = moment();
    this.workOrderTaskInProgress.totalTaskTime = this.workOrderTaskInProgress.totalTaskTime + (-taskHasBeenRunningSince.diff(currentDate, 'seconds'));
  }

  convertDateToUTC(date): Date {
    return new Date(date.getUTCFullYear(), date.getUTCMonth(), date.getUTCDate(), date.getUTCHours(), date.getUTCMinutes(), date.getUTCSeconds());
  }
}
