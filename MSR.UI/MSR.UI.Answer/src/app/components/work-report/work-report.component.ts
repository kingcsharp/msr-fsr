import { Component, Input, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { ProcedureStepMonitor, WorkOrderModel, WorkOrderTaskMonitorModel } from '../../services/api.client.generated';

@Component({
  selector: 'work-report',
  templateUrl: './work-report.component.html',
  styleUrls: ['./work-report.component.scss']
})
export class WorkReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  reportTypeOptions: Array<SelectItem>;
  yesOrNoOptions: Array<SelectItem>;
  selectedReportType: string;
  selectedShowProcedureStepOption:boolean = true;
  selectedShowPurchaseItemOption:boolean = true;

  constructor() { }

  ngOnInit(): void {

    this.reportTypeOptions = [
      { label: 'Purchase Summary', value: 'PurchaseSummary'},
      { label: 'Technical Summary', value: 'TechnicalSummary'}
    ];

    this.yesOrNoOptions = [
      { label: 'Yes', value: true},
      { label: 'No', value: false}
    ];

    this.WorkOrder.workOrderTasks.map(s => {

      if(s.workOrderTaskMonitors === null || s.workOrderTaskMonitors === undefined){
        s.workOrderTaskMonitors = new Array<WorkOrderTaskMonitorModel>()
      }
    });

    this.getMockMonitors(); 
    
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
