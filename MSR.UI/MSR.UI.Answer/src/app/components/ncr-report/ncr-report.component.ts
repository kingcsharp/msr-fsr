import { Component, Input, OnInit } from '@angular/core';
import { ProcedureStepMonitor, WorkOrderModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';

@Component({
  selector: 'ncr-report',
  templateUrl: './ncr-report.component.html',
  styleUrls: ['./ncr-report.component.scss']
})
export class NcrReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  taskSummaries: Array<any> = new Array<any>();
  constructor() { }

  ngOnInit(): void {

    this.WorkOrder.workOrderTasks.map(s => {

      if(s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined){
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>()
      }
    });

    this.getMockMonitors(); 

    this.generateMonitorSummaries();

    console.log(this.taskSummaries);
    
  }

  generateMonitorSummaries(){

    this.WorkOrder.workOrderTasks.forEach(workOrderTask => {

      let taskSummary = {
        taskName: workOrderTask.procedureStep.title,
        taskId: workOrderTask.id,
        procedureStepType: workOrderTask.procedureStepType.name,
        monitors: new Array<any>()
      };

      workOrderTask.workOrderTaskMonitors.forEach(workOrderTaskMonitor => {

        taskSummary.monitors.push({
          monitorTitle: workOrderTaskMonitor.procedureStepMonitor?.description,
          result: workOrderTaskMonitor.textVal,
          comment: workOrderTaskMonitor.comment
          
        })

      });

      this.taskSummaries.push(taskSummary);
    });

  }

  getMockMonitors(){
    let index = 0;
    this.WorkOrder.workOrderTasks.map(s => {

      let sampleMonitorA = new WorkOrderTaskMonitorModel();	
      sampleMonitorA.id = 1;	
      sampleMonitorA.procedureMonitorId = 1;	
      sampleMonitorA.procedureStepMonitor = new ProcedureStepMonitor();	
      sampleMonitorA.procedureStepMonitor.id = 1;	
      sampleMonitorA.procedureStepMonitor.inputType = 'Manual';	
      sampleMonitorA.procedureStepMonitor.monitorType = 'Equipment';	
      sampleMonitorA.procedureStepMonitor.targetValue = '10';	
      sampleMonitorA.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';	
      sampleMonitorA.procedureStepMonitor.description = "This is a sample Monitor A" + index++;
      

      let sampleMonitorB = new WorkOrderTaskMonitorModel();	
      sampleMonitorB.id = 2;	
      sampleMonitorB.procedureMonitorId = 2;	
      sampleMonitorB.procedureStepMonitor = new ProcedureStepMonitor();	
      sampleMonitorB.procedureStepMonitor.id = 2;	
      sampleMonitorB.procedureStepMonitor.inputType = 'Manual';	
      sampleMonitorB.procedureStepMonitor.monitorType = 'Equipment';	
      sampleMonitorB.procedureStepMonitor.targetValue = '10';	
      sampleMonitorB.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';	
      sampleMonitorB.procedureStepMonitor.description = "This is a sample Monitor B" + index++;
      
      if(index<8){
        s.workOrderTaskMonitors.push(sampleMonitorA);
        s.workOrderTaskMonitors.push(sampleMonitorB);
      }


    });

  }

}
