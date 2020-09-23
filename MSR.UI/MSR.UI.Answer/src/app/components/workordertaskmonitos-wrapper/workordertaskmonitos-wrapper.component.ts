import { Component, Input, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { ProcedureStepMonitor, WorkOrderTaskMonitorModel, SensorService, SensorModel } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

declare let jQuery: any;

@Component({
  selector: 'workordertaskmonitos-wrapper',
  templateUrl: './workordertaskmonitos-wrapper.component.html',
  styleUrls: ['./workordertaskmonitos-wrapper.component.scss'],
  providers: [SensorService]
})
export class WorkordertaskmonitosWrapperComponent implements OnInit {

  @Input() workOrderTaskMonitors: Array<WorkOrderTaskMonitorModel>;
  @Input() locationId: number;
  workOrderMonitorsToView: Array<any>;
  workOrderMonitorYesOrNoOptions: Array<SelectItem>;
  monitorListItemOptions: Array<SelectItem>;
  sensorsAvailable: Array<SelectItem>;
  
  constructor(private sensorService: SensorService) { }

  ngOnInit(): void {

    this.monitorListItemOptions = [
      { label: 'Damaged in Handling', value: 1},
      { label: 'Damaged in Storage', value: 2},
      { label: 'Damaged in Transit', value: 4},
      { label: 'Defect Appearance', value: 5},
      { label: 'Defect Functional', value: 6},
      { label: 'Defect Material', value: 7},
      { label: 'Defect Peformance', value: 8},
      { label: 'Defect Process', value: 9},
      { label: 'Defect Tolerance', value: 10},
      { label: 'Wrong Product', value: 12},
      { label: 'Wrong ID/Traceability', value: 12},
      { label: 'Cust NC - chips, cracks, breakage', value: 13},
      { label: 'Cust NC - scratches, pitting', value: 14},
      { label: 'Cust NC - other damage', value: 15},
      { label: 'Cust NC - end of life', value: 16},
      { label: 'Cust NC - false leak check', value: 17},
      { label: 'Cust NC - CU protocol violation', value: 18},
      { label: 'Cust NC - inadequate packaging', value: 19},
      { label: 'Cust NC - missing parts/subparts', value: 20},
      { label: 'Cust NC - wrong product', value: 21},
      { label: 'Cust NC - shipped to wrong location', value: 22},
      { label: 'Cust NC - incorrect paperwork', value: 23},
      { label: 'Cust NC - cannot disassemble', value: 24}
    ]

    this.workOrderMonitorYesOrNoOptions = [
      { label: 'Yes', value: '1' },
      { label: 'No', value: '0' }
    ]

    this.getMockMonitors();

    this.sensorService.sensor(null,415,env.apiVersion).subscribe(responseHandler(response => {

      this.sensorsAvailable = response.object.map(s => ({ label: s.sensorName, value: s.id}));

      this.workOrderMonitorsToView.forEach(workOrderMonitor => {

        if(workOrderMonitor.procedureStepMonitor.monitorType === 'Equipment' && workOrderMonitor.procedureStepMonitor.inputType === 'Sensor'){

          workOrderMonitor.sensorName = this.sensorsAvailable.find(s => s.value === Number(workOrderMonitor.procedureStepMonitor.targetValue)).label;

        }

      })
    }));

    

  }

  saveMonitors(){
    jQuery('.parsleyjs').parsley().validate();

    if (!jQuery('.parsleyjs').parsley().isValid()) {
      alert('invalid');
    }else{
      alert('valid');
    }
  }

  getSensorValue(indexOfMonitor: number,elementName: string, sensorId: number){

    this.sensorService.sensor(sensorId, null, env.apiVersion).subscribe(responseHandler(response => {
        this.workOrderMonitorsToView[indexOfMonitor].sensorValue = response.object[0]?.itemCurrentValue == undefined ? '1 p/ft³' : '1 p/ft³';
    }));
  }

  getMockMonitors(){

    if(this.workOrderMonitorsToView === undefined){
      this.workOrderMonitorsToView = new Array<WorkOrderTaskMonitorExtended>();
    }else{
      this.workOrderMonitorsToView.length = 0;
    }

    //Equipment, Pass or Fail, Select, Text, Yes or No, Number
    let equipmentMonitor = new WorkOrderTaskMonitorExtended();
    equipmentMonitor.id = 1;
    equipmentMonitor.monitorOrderNumber = 1;
    equipmentMonitor.procedureMonitorId = 1;
    equipmentMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    equipmentMonitor.procedureStepMonitor.id = 1;
    equipmentMonitor.procedureStepMonitor.inputType = 'Manual';
    equipmentMonitor.procedureStepMonitor.monitorType = 'Equipment';
    equipmentMonitor.procedureStepMonitor.targetValue = '10';
    equipmentMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    equipmentMonitor.procedureStepMonitor.description = "This is a sample Equipment Monitor"
    this.workOrderMonitorsToView.push(equipmentMonitor);

    let passOrdFailMonitor = new WorkOrderTaskMonitorExtended();
    passOrdFailMonitor.id = 2;
    passOrdFailMonitor.monitorOrderNumber = 2;
    passOrdFailMonitor.procedureMonitorId = 2;
    passOrdFailMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    passOrdFailMonitor.procedureStepMonitor.id = 2;
    passOrdFailMonitor.procedureStepMonitor.inputType = 'Manual';
    passOrdFailMonitor.procedureStepMonitor.monitorType = 'Pass or Fail';
    passOrdFailMonitor.procedureStepMonitor.targetValue = '1';
    passOrdFailMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    passOrdFailMonitor.procedureStepMonitor.description = "This is a sample Pass or Fail Monitor"
    this.workOrderMonitorsToView.push(passOrdFailMonitor);

    let textMonitor = new WorkOrderTaskMonitorExtended();
    textMonitor.id = 3;
    textMonitor.monitorOrderNumber = 3;
    textMonitor.procedureMonitorId = 3;
    textMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    textMonitor.procedureStepMonitor.id = 3;
    textMonitor.procedureStepMonitor.inputType = 'Manual';
    textMonitor.procedureStepMonitor.monitorType = 'Text';
    textMonitor.procedureStepMonitor.targetValue = '33';
    textMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    textMonitor.procedureStepMonitor.description = "This is a sample Text Monitor"
    this.workOrderMonitorsToView.push(textMonitor);

    let yesOrNotMonitor = new WorkOrderTaskMonitorExtended();
    yesOrNotMonitor.id = 4;
    yesOrNotMonitor.monitorOrderNumber = 4;
    yesOrNotMonitor.procedureMonitorId = 4;
    yesOrNotMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    yesOrNotMonitor.procedureStepMonitor.id = 4;
    yesOrNotMonitor.procedureStepMonitor.inputType = 'Manual';
    yesOrNotMonitor.procedureStepMonitor.monitorType = 'Yes or No';
    yesOrNotMonitor.procedureStepMonitor.targetValue = '1';
    yesOrNotMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    yesOrNotMonitor.procedureStepMonitor.description = "This is a sample Yes or No Monitor"
    this.workOrderMonitorsToView.push(yesOrNotMonitor);

    let numberEqualMonitor = new WorkOrderTaskMonitorExtended();
    numberEqualMonitor.id = 5;
    numberEqualMonitor.monitorOrderNumber = 5;
    numberEqualMonitor.procedureMonitorId = 5;
    numberEqualMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberEqualMonitor.procedureStepMonitor.id = 5;
    numberEqualMonitor.procedureStepMonitor.inputType = 'Manual';
    numberEqualMonitor.procedureStepMonitor.monitorType = 'Number';
    numberEqualMonitor.procedureStepMonitor.shouldBe = 'EQUAL';
    numberEqualMonitor.procedureStepMonitor.targetValue = '10';
    numberEqualMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberEqualMonitor.procedureStepMonitor.description = "This is a sample Equal Number Monitor"
    this.workOrderMonitorsToView.push(numberEqualMonitor);

    let numberAboveMonitor = new WorkOrderTaskMonitorExtended();
    numberAboveMonitor.id = 6;
    numberAboveMonitor.monitorOrderNumber = 6;
    numberAboveMonitor.procedureMonitorId = 6;
    numberAboveMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberAboveMonitor.procedureStepMonitor.id = 6;
    numberAboveMonitor.procedureStepMonitor.inputType = 'Manual';
    numberAboveMonitor.procedureStepMonitor.monitorType = 'Number';
    numberAboveMonitor.procedureStepMonitor.shouldBe = 'ABOVE';
    numberAboveMonitor.procedureStepMonitor.targetValue = '10';
    numberAboveMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberAboveMonitor.procedureStepMonitor.description = "This is a sample Above Number Monitor"
    this.workOrderMonitorsToView.push(numberAboveMonitor);

    let numberBelowMonitor = new WorkOrderTaskMonitorExtended();
    numberBelowMonitor.id = 7;
    numberBelowMonitor.monitorOrderNumber = 7;
    numberBelowMonitor.procedureMonitorId = 7;
    numberBelowMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBelowMonitor.procedureStepMonitor.id = 7;
    numberBelowMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBelowMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBelowMonitor.procedureStepMonitor.shouldBe = 'BELOW';
    numberBelowMonitor.procedureStepMonitor.targetValue = '10';
    numberBelowMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberBelowMonitor.procedureStepMonitor.description = "This is a sample Below Number Monitor"
    this.workOrderMonitorsToView.push(numberBelowMonitor);

    let numberBetweenMonitor = new WorkOrderTaskMonitorExtended();
    numberBetweenMonitor.id = 8;
    numberBetweenMonitor.monitorOrderNumber = 8;
    numberBetweenMonitor.procedureMonitorId = 8;
    numberBetweenMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBetweenMonitor.procedureStepMonitor.id = 8;
    numberBetweenMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBetweenMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBetweenMonitor.procedureStepMonitor.shouldBe = 'BETWEEN';
    numberBetweenMonitor.procedureStepMonitor.lowTarget = 10;
    numberBetweenMonitor.procedureStepMonitor.highTarget = 20;
    numberBetweenMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberBetweenMonitor.procedureStepMonitor.description = "This is a sample Between Number Monitor"
    this.workOrderMonitorsToView.push(numberBetweenMonitor);

    let selectMonitor = new WorkOrderTaskMonitorExtended();
    selectMonitor.id = 8;
    selectMonitor.monitorOrderNumber = 8;
    selectMonitor.procedureMonitorId = 8;
    selectMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    selectMonitor.procedureStepMonitor.id = 8;
    selectMonitor.procedureStepMonitor.inputType = 'Manual';
    selectMonitor.procedureStepMonitor.monitorType = 'Select';
    selectMonitor.procedureStepMonitor.targetValue = '1';
    selectMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    selectMonitor.procedureStepMonitor.description = "This is a sample Select Monitor"
    this.workOrderMonitorsToView.push(selectMonitor);

    let equipmentSensorMonitor = new WorkOrderTaskMonitorExtended();
    equipmentSensorMonitor.id = 8;
    equipmentSensorMonitor.monitorOrderNumber = 8;
    equipmentSensorMonitor.procedureMonitorId = 8;
    equipmentSensorMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    equipmentSensorMonitor.procedureStepMonitor.id = 8;
    equipmentSensorMonitor.procedureStepMonitor.inputType = 'Sensor';
    equipmentSensorMonitor.procedureStepMonitor.targetValue = '9';
    equipmentSensorMonitor.procedureStepMonitor.monitorType = 'Equipment';
    equipmentSensorMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    equipmentSensorMonitor.procedureStepMonitor.description = "This is a sample Equpment Sensor Monitor"
    this.workOrderMonitorsToView.push(equipmentSensorMonitor);
    

  }

}

export class WorkOrderTaskMonitorExtended extends WorkOrderTaskMonitorModel {
  procedureStepMonitor: ProcedureStepMonitor;
  monitorOrderNumber: number;
}
