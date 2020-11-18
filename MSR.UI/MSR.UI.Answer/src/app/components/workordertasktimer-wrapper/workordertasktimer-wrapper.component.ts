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
  @Output() slideToTaskInProgress = new EventEmitter<any>();
  @Output() areMonitorsValidCheck = new EventEmitter<{ areValid: Function }>();

  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;
  timerStartTime: Date;

  constructor(private workOrderTaskService: WorkOrderTaskService, private userService: UserService, private globals: Globals, private router: Router) { }

  ngOnInit(): void {

    this.setTimerDisplay(this.workOrderTaskInProgress.totalTaskTime, true);

    if (this.workOrderTaskInProgress.taskIsRunning === true) {
      this.startTask();
    }

  }

  ngOnChanges() {
    if (this.workOrderIsComplete) {
      this.setTimerDisplay(this.workOrderTaskInProgress.totalTaskTime, true);
    }

  }

  startTask() {

    this.workOrderTaskInProgress.status.id = 2;
    this.workOrderTaskInProgress.status.name = 'In Progress';
    this.workOrderTaskInProgress.startedOn = moment().toDate();
    this.resumeAndStartTask(true);

  }

  pauseTask() {
    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = undefined;
    clearInterval(this.stepTimer);
    this.saveTaskTimerState(false, false);
  }

  resumeAndStartTask(isStartingTask: boolean = false) {

    this.workOrderTaskInProgress.taskIsRunning = true;
    this.workOrderTaskInProgress.taskRunningSince = new Date();
    this.stepTimer = setInterval(() => {

      this.setTimerDisplay(this.workOrderTaskInProgress.totalTaskTime, false);

    }, 1000);
    this.saveTaskTimerState(false, true);

  }

  setTimerDisplay(seconds: number, justForDisplay: boolean) {

    this.stepSeconds = justForDisplay ? this.workOrderTaskInProgress.totalTaskTime : this.workOrderTaskInProgress.totalTaskTime++;
    this.stepMinutes = Math.floor(this.stepSeconds / 60);
    this.stepHours = Math.floor(this.stepMinutes / 60);

  }


  completeTask() {

    this.areMonitorsValidCheck.emit({
      areValid: (result) => {

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
        this.workOrderTaskInProgress.lastUpdatedOn = moment().toDate();

        this.saveTaskTimerState(true, false);

      }
    });
  }

  saveTaskTimerState(closeStep: boolean, isStartingTask: boolean) {



    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.globals.getCurrentUser().id,
      status: closeStep ? 'Complete' : this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      totalTaskTime: this.workOrderTaskInProgress.totalTaskTime,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id,
      startedOn: isStartingTask ? moment() : this.workOrderTaskInProgress.startedOn
    } as IUpdateWorkOrderTaskRequest);

    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion, updateWorkOrderTaskRequest).subscribe(responseHandler(workOrderTaskPatchResponse => {

      if (closeStep) {
        let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
        if ((indexOfNextTask + 1) === this.workOrderTasks.length) {
          this.router.navigate(['/app/wip/wipstatus']);
        } else {
          this.updateWorkOrderTaskToViewAndInProgress.emit(this.workOrderTasks[indexOfNextTask + 1]);
        }
      }

    }));

  }

  slideToTask() {

    this.slideToTaskInProgress.emit();

  }

}
