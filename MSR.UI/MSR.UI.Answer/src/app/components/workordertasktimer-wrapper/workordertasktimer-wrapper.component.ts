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
  @Input() workOrderTaskToView: WorkOrderTaskModel;
  @Output() workOrderTasksChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  @Output() workOrderTaskToViewChange = new EventEmitter<any>();
  @Output() updateWorkOrderTaskToViewAndInProgress = new EventEmitter<any>();

  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;
  timerStartTime: Date;

  constructor(private workOrderTaskService: WorkOrderTaskService) { }

  ngOnInit(): void {
  }

  startTask(){

    this.workOrderTaskInProgress.status.id = 2;
    this.workOrderTaskInProgress.status.name = 'In Progress';
    this.resumeAndStartTask();

  }

  pauseTask(){
    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = undefined;
    clearInterval(this.stepTimer);
    //this.saveTaskTimerState();
  }

  resumeAndStartTask(){

    if(this.workOrderTaskInProgress.taskRunningSince !== undefined && this.workOrderTaskInProgress.taskRunningSince !== null){
      this.workOrderTaskInProgress.totalTaskTime += new Date().getTime() - this.workOrderTaskInProgress.taskRunningSince.getTime()/1000;
    }

    this.workOrderTaskInProgress.taskIsRunning = true;
    this.workOrderTaskInProgress.taskRunningSince = new Date();
    this.stepTimer = setInterval( () => {

      this.stepSeconds = this.workOrderTaskInProgress.totalTaskTime++;
      this.stepMinutes = Math.floor(this.stepSeconds / 60);
      this.stepHours= Math.floor(this.stepMinutes / 60);

    },1000);
    //this.saveTaskTimerState();

  }

  completeTask(){

    this.workOrderTaskInProgress.taskIsRunning = false;
    this.workOrderTaskInProgress.taskRunningSince = null;
    clearInterval(this.stepTimer);
    this.stepSeconds = 0;
    this.stepMinutes = 0;
    this.stepHours = 0;
    //this.saveTaskTimerState();
    
    this.workOrderTaskInProgress.statusId = 3;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 3,
      name: 'Completed'
    } as IStatusModel);

    let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    if((indexOfNextTask + 1) > this.workOrderTasks.length){
      this.workOrderTaskInProgress = undefined;
    }else{
      this.workOrderTaskInProgress = this.workOrderTasks[indexOfNextTask + 1];
      this.workOrderTaskToView = this.workOrderTasks[indexOfNextTask + 1];
      this.updateWorkOrderTaskToViewAndInProgress.emit(this.workOrderTasks[indexOfNextTask + 1]);
    }
    
  }

  saveTaskTimerState(){

    let updateWorkOrderTaskRequest = new UpdateWorkOrderTaskRequest({
      assignedUserId: this.workOrderTaskInProgress.assignedToUser?.id,
      status: this.workOrderTaskInProgress.status.name,
      taskIsRunning: this.workOrderTaskInProgress.taskIsRunning,
      taskRunningSince: this.workOrderTaskInProgress.taskRunningSince,
      // TODO: Remove comment when property is added
      //totalTaskTime:this.workOrderTaskInProgress.totalTaskTime,
      taskStepOrder: this.workOrderTaskInProgress.taskStepOrder,
      workOrderTaskId: this.workOrderTaskInProgress.id
     } as IUpdateWorkOrderTaskRequest);
    this.workOrderTaskService.workOrderTaskPatch(env.apiVersion,updateWorkOrderTaskRequest).subscribe(responseHandler(response => {
      console.log(response);
    }));

  }

}
