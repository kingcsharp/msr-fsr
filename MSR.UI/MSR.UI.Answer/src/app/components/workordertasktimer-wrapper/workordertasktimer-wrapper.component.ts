import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IStatusModel, IUpdateWorkOrderTaskRequest, StatusModel, UpdateWorkOrderTaskRequest, WorkOrderTaskModel, WorkOrderTaskService } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

@Component({
  selector: 'workordertasktimer-wrapper',
  templateUrl: './workordertasktimer-wrapper.component.html',
  styleUrls: ['./workordertasktimer-wrapper.component.scss'],
  providers: [WorkOrderTaskService]
})
export class WorkordertasktimerWrapperComponent implements OnInit {

  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Input() workOrderTasks: Array<WorkOrderTaskModel>;
  @Output() workOrderTaskInProgressUpdateParent = new EventEmitter();
  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;
  timerStartTime: Date;

  constructor(private workOrderTaskService: WorkOrderTaskService) { }

  ngOnInit(): void {
  }

  startTask(){
    this.workOrderTaskInProgress.statusId = 2;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 2,
      name: 'In Progress'
    } as IStatusModel);
    this.workOrderTaskInProgress.taskIsRunning = true;
    this.timerStartTime = new Date();

    this.stepTimer = setInterval( () => {

      let runningSeconds = Math.trunc((new Date().getTime() - this.timerStartTime.getTime())/1000);
      this.stepSeconds = runningSeconds;
      this.stepMinutes = Math.floor(this.stepSeconds / 60);
      this.stepHours= Math.floor(this.stepMinutes / 60);

    },1000);
  }

  resumeTask(){

    this.workOrderTaskInProgress.taskIsRunning = true;
    this.stepTimer = setInterval( () => {

      let runningSeconds = Math.trunc((new Date().getTime() - this.timerStartTime.getTime())/1000);
      this.stepSeconds = runningSeconds;
      this.stepMinutes = Math.floor(this.stepSeconds / 60);
      this.stepHours= Math.floor(this.stepMinutes / 60);

    },1000);

  }

  pauseTask(){
    this.workOrderTaskInProgress.taskIsRunning = false;
    clearInterval(this.stepTimer);
  }

  completeTask(){
    this.workOrderTaskInProgress.statusId = 3;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 3,
      name: 'Completed'
    } as IStatusModel);

    this.stepSeconds = 0;
    this.stepMinutes = 0;
    this.stepHours = 0;
    clearInterval(this.stepTimer);

    let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    if((indexOfNextTask + 1) > this.workOrderTasks.length){
      this.workOrderTaskInProgress = undefined;
    }else{
      this.workOrderTaskInProgress = this.workOrderTasks[indexOfNextTask + 1];
      this.workOrderTaskInProgress.status = new StatusModel({
        id: 2,
        name: 'In Progress'
      } as IStatusModel);
    }
    
    this.workOrderTaskInProgressUpdateParent.emit(this.workOrderTaskInProgress);
  }

  saveTaskHasStarted(){

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.workOrderTaskInProgress.assignedToUser.id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: true,
      taskRunningSince: new Date(),
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
     } as IUpdateWorkOrderTaskRequest);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion,updateWorkOrderTaskRequest).subscribe(responseHandler(response => {
      console.log(response);
    }));

  }

  saveTaskHasPausedOrFinished(){
    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.workOrderTaskInProgress.assignedToUser.id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: false,
      taskRunningSince: new Date(),
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
     } as IUpdateWorkOrderTaskRequest);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion,updateWorkOrderTaskRequest).subscribe(responseHandler(response => {
      console.log(response);
    }));
  }

}
