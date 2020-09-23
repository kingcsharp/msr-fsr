import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { ProcedureStepMonitor, WorkOrderTaskMonitorModel, SensorService, SensorModel, WorkOrderTaskMonitorService, UpdateWorkOrderTaskMonitorRequest, IUpdateWorkOrderTaskMonitorRequest } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { forkJoin } from 'rxjs';

declare let jQuery: any;

@Component({
  selector: 'workordertaskmonitos-wrapper',
  templateUrl: './workordertaskmonitos-wrapper.component.html',
  styleUrls: ['./workordertaskmonitos-wrapper.component.scss'],
  providers: [SensorService,WorkOrderTaskMonitorService]
})
export class WorkordertaskmonitosWrapperComponent implements OnInit {

  @Input() workOrderTaskMonitors: Array<WorkOrderTaskMonitorModel>;
  @Input() locationId: number;
  @Output() closeCurrentTaskInProgress = new EventEmitter();
  workOrderMonitorsToView: Array<any>;
  workOrderMonitorYesOrNoOptions: Array<SelectItem>;
  monitorListItemOptions: Array<SelectItem>;
  sensorsAvailable: Array<SelectItem>;
  workOrderMonitorPassOrFailOptions: Array<SelectItem>;

  constructor(private sensorService: SensorService, private workOrderTaskMonitorService: WorkOrderTaskMonitorService) { }

  ngOnInit(): void {

    this.monitorListItemOptions = [
      { label: 'Damaged in Handling', value: 1 },
      { label: 'Damaged in Storage', value: 2 },
      { label: 'Damaged in Transit', value: 4 },
      { label: 'Defect Appearance', value: 5 },
      { label: 'Defect Functional', value: 6 },
      { label: 'Defect Material', value: 7 },
      { label: 'Defect Peformance', value: 8 },
      { label: 'Defect Process', value: 9 },
      { label: 'Defect Tolerance', value: 10 },
      { label: 'Wrong Product', value: 12 },
      { label: 'Wrong ID/Traceability', value: 12 },
      { label: 'Cust NC - chips, cracks, breakage', value: 13 },
      { label: 'Cust NC - scratches, pitting', value: 14 },
      { label: 'Cust NC - other damage', value: 15 },
      { label: 'Cust NC - end of life', value: 16 },
      { label: 'Cust NC - false leak check', value: 17 },
      { label: 'Cust NC - CU protocol violation', value: 18 },
      { label: 'Cust NC - inadequate packaging', value: 19 },
      { label: 'Cust NC - missing parts/subparts', value: 20 },
      { label: 'Cust NC - wrong product', value: 21 },
      { label: 'Cust NC - shipped to wrong location', value: 22 },
      { label: 'Cust NC - incorrect paperwork', value: 23 },
      { label: 'Cust NC - cannot disassemble', value: 24 }
    ]

    this.workOrderMonitorYesOrNoOptions = [
      { label: 'Yes', value: '1' },
      { label: 'No', value: '0' }
    ]

    this.workOrderMonitorPassOrFailOptions = [
      { label: 'Pass', value: '1' },
      { label: 'Fail', value: '0' }
    ]

    this.getMockMonitors();

    this.workOrderMonitorsToView.map(monitor => {

      monitor.holdIfFails = monitor.procedureStepMonitor.faultHandling === 'STOP UNTIL FAULT CLEARED' ? true : false;

    })

    this.sensorService.sensor(null, 415, env.apiVersion).subscribe(responseHandler(response => {

      this.sensorsAvailable = response.object.map(s => ({ label: s.sensorName, value: s.id }));

      this.workOrderMonitorsToView.forEach(workOrderMonitor => {

        if (workOrderMonitor.procedureStepMonitor.monitorType === 'Equipment' && workOrderMonitor.procedureStepMonitor.inputType === 'Sensor') {

          workOrderMonitor.sensorName = this.sensorsAvailable.find(s => s.value === Number(workOrderMonitor.procedureStepMonitor.targetValue)).label;

        }

      })
    }));



  }

  areDropDownsValid():boolean {

    let dropDownsAreValid = true;

    this.workOrderMonitorsToView.filter(s => (s.procedureStepMonitor.monitorType === 'Pass or Fail'
      || s.procedureStepMonitor.monitorType === 'Yes or No'
      || s.procedureStepMonitor.monitorType === 'Select') && s.procedureStepMonitor.faultHandling === 'STOP UNTIL FAULT CLEARED').forEach(m => {
        
        if(m.procedureStepMonitor.targetValue !== m.numVal && (m.procedureStepMonitor.monitorType === 'Pass or Fail'
        || m.procedureStepMonitor.monitorType === 'Yes or No')){
          dropDownsAreValid = false;
        }else if(m.procedureStepMonitor.monitorType === 'Select' && m.numVal === undefined){
          dropDownsAreValid = false;
        }
        

      });

      return dropDownsAreValid;
  }

  updateMonitors(closeTask: boolean = false){

    let updateMonitorsRequests = new Array<any>();

      this.workOrderMonitorsToView.forEach(monitor => {

        let updateWorkOrderTaskMonitorRequest = new UpdateWorkOrderTaskMonitorRequest({
          comment: monitor.comment === undefined ? '' : monitor.comment,
          multiVal: monitor.multiVal === undefined ? '' : monitor.multiVal,
          sensorValue: monitor.sensorValue === undefined ? '' : monitor.sensorValue,
          textVal: monitor.textVal === undefined ? '' : monitor.textVal,
          numVal: monitor.numVal === undefined ? undefined : monitor.numVal,
          workOrderTaskMonitorId: monitor.id
        } as IUpdateWorkOrderTaskMonitorRequest);

        updateMonitorsRequests.push(this.workOrderTaskMonitorService.workOrderTaskMonitor(env.apiVersion,updateWorkOrderTaskMonitorRequest));

      });
      //TODO: Remove when you fix permission issue
      this.closeCurrentTaskInProgress.emit();

      /*
      forkJoin(updateMonitorsRequests).subscribe(responses => {
        console.log(closeTask);
        if(closeTask){
          this.closeCurrentTaskInProgress.emit();
        }
      });
      */

  }



  saveMonitors() {
    jQuery('.parsleyjs').parsley().validate();

    if (jQuery('.parsleyjs').parsley().isValid() && this.areDropDownsValid()) {
      
      this.updateMonitors(false);

    } else {
      alert('invalid');
    }
  }

  saveMonitorsAndCloseTask() {
    jQuery('.parsleyjs').parsley().validate();

    if (jQuery('.parsleyjs').parsley().isValid() && this.areDropDownsValid()) {
      
      this.updateMonitors(true);

    } else {
      alert('invalid');
    }
  }

  getSensorValue(indexOfMonitor: number, elementName: string, sensorId: number) {

    this.sensorService.sensor(sensorId, null, env.apiVersion).subscribe(responseHandler(response => {
      this.workOrderMonitorsToView[indexOfMonitor].sensorValue = response.object[0]?.itemCurrentValue == undefined ? '1 p/ft³' : '1 p/ft³';
    }));
  }

  getMockMonitors() {

    if (this.workOrderMonitorsToView === undefined) {
      this.workOrderMonitorsToView = new Array<WorkOrderTaskMonitorExtended>();
    } else {
      this.workOrderMonitorsToView.length = 0;
    }

    //Equipment, Pass or Fail, Select, Text, Yes or No, Number
    let equipmentMonitor = new WorkOrderTaskMonitorExtended();
    equipmentMonitor.id = 1;
    equipmentMonitor.procedureMonitorId = 1;
    equipmentMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    equipmentMonitor.procedureStepMonitor.id = 1;
    equipmentMonitor.procedureStepMonitor.inputType = 'Manual';
    equipmentMonitor.procedureStepMonitor.monitorType = 'Equipment';
    equipmentMonitor.procedureStepMonitor.targetValue = '10';
    equipmentMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    equipmentMonitor.procedureStepMonitor.description = "This is a sample Equipment Monitor"
    this.workOrderMonitorsToView.push(equipmentMonitor);

    let passOrdFailMonitor = new WorkOrderTaskMonitorExtended();
    passOrdFailMonitor.id = 2;
    passOrdFailMonitor.procedureMonitorId = 2;
    passOrdFailMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    passOrdFailMonitor.procedureStepMonitor.id = 2;
    passOrdFailMonitor.procedureStepMonitor.inputType = 'Manual';
    passOrdFailMonitor.procedureStepMonitor.monitorType = 'Pass or Fail';
    passOrdFailMonitor.procedureStepMonitor.targetValue = '1';
    passOrdFailMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    passOrdFailMonitor.procedureStepMonitor.description = "This is a sample Pass or Fail Monitor"
    this.workOrderMonitorsToView.push(passOrdFailMonitor);

    let textMonitor = new WorkOrderTaskMonitorExtended();
    textMonitor.id = 3;
    textMonitor.procedureMonitorId = 3;
    textMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    textMonitor.procedureStepMonitor.id = 3;
    textMonitor.procedureStepMonitor.inputType = 'Manual';
    textMonitor.procedureStepMonitor.monitorType = 'Text';
    textMonitor.procedureStepMonitor.targetValue = '33';
    textMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    textMonitor.procedureStepMonitor.description = "This is a sample Text Monitor"
    this.workOrderMonitorsToView.push(textMonitor);

    let yesOrNotMonitor = new WorkOrderTaskMonitorExtended();
    yesOrNotMonitor.id = 4;
    yesOrNotMonitor.procedureMonitorId = 4;
    yesOrNotMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    yesOrNotMonitor.procedureStepMonitor.id = 4;
    yesOrNotMonitor.procedureStepMonitor.inputType = 'Manual';
    yesOrNotMonitor.procedureStepMonitor.monitorType = 'Yes or No';
    yesOrNotMonitor.procedureStepMonitor.targetValue = '1';
    yesOrNotMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    yesOrNotMonitor.procedureStepMonitor.description = "This is a sample Yes or No Monitor"
    this.workOrderMonitorsToView.push(yesOrNotMonitor);

    let numberEqualMonitor = new WorkOrderTaskMonitorExtended();
    numberEqualMonitor.id = 5;
    numberEqualMonitor.procedureMonitorId = 5;
    numberEqualMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberEqualMonitor.procedureStepMonitor.id = 5;
    numberEqualMonitor.procedureStepMonitor.inputType = 'Manual';
    numberEqualMonitor.procedureStepMonitor.monitorType = 'Number';
    numberEqualMonitor.procedureStepMonitor.shouldBe = 'EQUAL';
    numberEqualMonitor.procedureStepMonitor.targetValue = '10';
    numberEqualMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    numberEqualMonitor.procedureStepMonitor.description = "This is a sample Equal Number Monitor"
    this.workOrderMonitorsToView.push(numberEqualMonitor);

    let numberAboveMonitor = new WorkOrderTaskMonitorExtended();
    numberAboveMonitor.id = 6;
    numberAboveMonitor.procedureMonitorId = 6;
    numberAboveMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberAboveMonitor.procedureStepMonitor.id = 6;
    numberAboveMonitor.procedureStepMonitor.inputType = 'Manual';
    numberAboveMonitor.procedureStepMonitor.monitorType = 'Number';
    numberAboveMonitor.procedureStepMonitor.shouldBe = 'ABOVE';
    numberAboveMonitor.procedureStepMonitor.targetValue = '10';
    numberAboveMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    numberAboveMonitor.procedureStepMonitor.description = "This is a sample Above Number Monitor"
    this.workOrderMonitorsToView.push(numberAboveMonitor);

    let numberBelowMonitor = new WorkOrderTaskMonitorExtended();
    numberBelowMonitor.id = 7;
    numberBelowMonitor.procedureMonitorId = 7;
    numberBelowMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBelowMonitor.procedureStepMonitor.id = 7;
    numberBelowMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBelowMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBelowMonitor.procedureStepMonitor.shouldBe = 'BELOW';
    numberBelowMonitor.procedureStepMonitor.targetValue = '10';
    numberBelowMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    numberBelowMonitor.procedureStepMonitor.description = "This is a sample Below Number Monitor"
    this.workOrderMonitorsToView.push(numberBelowMonitor);

    let numberBetweenMonitor = new WorkOrderTaskMonitorExtended();
    numberBetweenMonitor.id = 8;
    numberBetweenMonitor.procedureMonitorId = 8;
    numberBetweenMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBetweenMonitor.procedureStepMonitor.id = 8;
    numberBetweenMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBetweenMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBetweenMonitor.procedureStepMonitor.shouldBe = 'BETWEEN';
    numberBetweenMonitor.procedureStepMonitor.lowTarget = 10;
    numberBetweenMonitor.procedureStepMonitor.highTarget = 20;
    numberBetweenMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    numberBetweenMonitor.procedureStepMonitor.description = "This is a sample Between Number Monitor"
    this.workOrderMonitorsToView.push(numberBetweenMonitor);

    let selectMonitor = new WorkOrderTaskMonitorExtended();
    selectMonitor.id = 8;
    selectMonitor.procedureMonitorId = 8;
    selectMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    selectMonitor.procedureStepMonitor.id = 8;
    selectMonitor.procedureStepMonitor.inputType = 'Manual';
    selectMonitor.procedureStepMonitor.monitorType = 'Select';
    selectMonitor.procedureStepMonitor.targetValue = '1';
    selectMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    selectMonitor.procedureStepMonitor.description = "This is a sample Select Monitor"
    this.workOrderMonitorsToView.push(selectMonitor);

    let equipmentSensorMonitor = new WorkOrderTaskMonitorExtended();
    equipmentSensorMonitor.id = 8;
    equipmentSensorMonitor.procedureMonitorId = 8;
    equipmentSensorMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    equipmentSensorMonitor.procedureStepMonitor.id = 8;
    equipmentSensorMonitor.procedureStepMonitor.inputType = 'Sensor';
    equipmentSensorMonitor.procedureStepMonitor.targetValue = '9';
    equipmentSensorMonitor.procedureStepMonitor.monitorType = 'Equipment';
    equipmentSensorMonitor.procedureStepMonitor.faultHandling = 'STOP UNTIL FAULT CLEARED';
    equipmentSensorMonitor.procedureStepMonitor.description = "This is a sample Equpment Sensor Monitor"
    this.workOrderMonitorsToView.push(equipmentSensorMonitor);


  }

}

export class WorkOrderTaskMonitorExtended extends WorkOrderTaskMonitorModel {
  procedureStepMonitor: ProcedureStepMonitor;
}
