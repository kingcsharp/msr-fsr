import { Component, Input, OnInit } from '@angular/core';
import { ProcedureStepMonitor, WorkOrderModel, WorkOrderTaskModel, WorkOrderTaskMonitorModel, WorkOrderTaskService } from '../../services/api.client.generated';

@Component({
  selector: 'technical-data-label',
  templateUrl: './technical-data-label.component.html',
  styleUrls: ['./technical-data-label.component.scss']
})

export class TechnicalDataLabelComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  labels: Array<DataLabel> = new Array<DataLabel>();

  constructor() { }

  ngOnInit(): void {

    this.WorkOrder.workOrderTasks.map(s => {

      if(s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined){
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>()
      }

    });


    if(this.WorkOrder.workOrderTasks.map(s => s.workOrderTaskMonitors).map(s => s.length).reduce((sum, b) => sum + b, 0) > 0){

      this.generateLabels();     

    }else{

      this.getMockMonitors();
      this.generateLabels();
    }

  }

  generateLabels(){

    this.WorkOrder.workOrderTasks.forEach(workOrderTask => {

      for(let index = 0; index < workOrderTask.workOrderTaskMonitors.length; index++){

        let dataLabel = new DataLabel();
        dataLabel.CustomerPurchaseNumber  = this.WorkOrder.purchase.customerPurchaseNumber;
        dataLabel.Date = this.WorkOrder.scheduledEndDate;
        dataLabel.MonitorName = workOrderTask.workOrderTaskMonitors[index].procedureStepMonitor?.description;
        dataLabel.PartName = this.WorkOrder.product?.part?.name;
        dataLabel.PartNumber = this.WorkOrder.product?.part?.partNumber;
        dataLabel.Requestee = workOrderTask.assignedToUser?.fullName;
        dataLabel.TaskDescription = workOrderTask.procedureStep?.stepText;
        this.labels.push(dataLabel);

      }

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

export class DataLabel{
  TaskDescription: string;
  MonitorName: string;
  PartNumber: string;
  CustomerPurchaseNumber: string;
  PartName: string;
  Requestee: string;
  Date: Date;
}
