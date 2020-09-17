import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { IStatusModel, StatusModel, WorkOrderTaskModel } from '../../services/api.client.generated';

@Component({
  selector: 'workordertasktimer-wrapper',
  templateUrl: './workordertasktimer-wrapper.component.html',
  styleUrls: ['./workordertasktimer-wrapper.component.scss']
})
export class WorkordertasktimerWrapperComponent implements OnInit {

  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Input() workOrderTasks: Array<WorkOrderTaskModel>;
  @Output() workOrderTaskInProgressChange = new EventEmitter();
  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;

  constructor() { }

  ngOnInit(): void {
  }

  startTask(){
    this.workOrderTaskInProgress.statusId = 2;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 2,
      name: 'In Progress'
    } as IStatusModel);
    this.workOrderTaskInProgress.taskIsRunning = true;
    this.stepTimer = setInterval( () => {

      this.stepSeconds += 1;
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
    
  }

  workOrderTaskInProgressUpdateParent(){
    this.workOrderTaskInProgressChange.emit(this.workOrderTaskInProgress);
  }

}
