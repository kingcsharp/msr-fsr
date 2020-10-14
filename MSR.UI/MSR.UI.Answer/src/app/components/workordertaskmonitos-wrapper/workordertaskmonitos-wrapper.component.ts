import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { SensorService, WorkOrderTaskMonitorService, UpdateWorkOrderTaskMonitorRequest, IUpdateWorkOrderTaskMonitorRequest, WorkOrderModel } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { forkJoin } from 'rxjs';

declare let jQuery: any;

@Component({
  selector: 'workordertaskmonitos-wrapper',
  templateUrl: './workordertaskmonitos-wrapper.component.html',
  styleUrls: ['./workordertaskmonitos-wrapper.component.scss'],
  providers: [SensorService, WorkOrderTaskMonitorService]
})
export class WorkordertaskmonitosWrapperComponent implements OnInit {

  @Input() workOrderMonitorsToView: Array<any>;
  @Input() locationId: number;
  @Input() doNotAllowEditing: boolean = true;
  @Input() workOrderModel: WorkOrderModel;
  @Output() closeCurrentTaskInProgress = new EventEmitter();
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
    ];

    this.workOrderMonitorYesOrNoOptions = [
      { label: 'Yes', value: '1' },
      { label: 'No', value: '0' }
    ];

    this.workOrderMonitorPassOrFailOptions = [
      { label: 'Pass', value: '1' },
      { label: 'Fail', value: '0' }
    ];

    this.workOrderMonitorsToView.map(monitor => {

      monitor.holdIfFails = monitor.procedureStepMonitor.faultHandling === 'STOP UNTIL FAULT CLEARED' ? true : false;

    });

  }

  areDropDownsValid(): boolean {

    let dropDownsAreValid = true;

    this.workOrderMonitorsToView.filter(s => (s.procedureStepMonitor.monitorType === 'Pass or Fail'
      || s.procedureStepMonitor.monitorType === 'Yes or No'
      || s.procedureStepMonitor.monitorType === 'Select') && s.procedureStepMonitor.faultHandling === 'STOP UNTIL FAULT CLEARED').forEach(m => {

        if (m.procedureStepMonitor.targetValue !== m.numVal && (m.procedureStepMonitor.monitorType === 'Pass or Fail'
          || m.procedureStepMonitor.monitorType === 'Yes or No')) {
          dropDownsAreValid = false;
        } else if (m.procedureStepMonitor.monitorType === 'Select' && m.numVal === undefined) {
          dropDownsAreValid = false;
        }


      });

    return dropDownsAreValid;
  }

  updateMonitors(closeTask: boolean = false) {

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

      updateMonitorsRequests.push(this.workOrderTaskMonitorService.workOrderTaskMonitor(env.apiVersion, updateWorkOrderTaskMonitorRequest));

    });

    forkJoin(updateMonitorsRequests).subscribe(() => {
      if (closeTask) {
        this.closeCurrentTaskInProgress.emit();
      }
    });


  }



  saveMonitors() {
    jQuery('.parsleyjs').parsley().validate();

    if (jQuery('.parsleyjs').parsley().isValid() && this.areDropDownsValid()) {

      this.updateMonitors(false);

    }
  }

  saveMonitorsAndCloseTask() {
    jQuery('.parsleyjs').parsley().validate();

    if (jQuery('.parsleyjs').parsley().isValid() && this.areDropDownsValid()) {

      this.updateMonitors(true);

    }
  }

  getSensorValue(indexOfMonitor: number, sensorName: string) {

    let siteId = this.workOrderModel.location.site === undefined || this.workOrderModel.location.site === null ? this.workOrderModel.location.id : this.workOrderModel.location.site;

    this.sensorService.value(sensorName, siteId, env.apiVersion).subscribe(response => {

      if (response.object === undefined) {
        this.workOrderMonitorsToView[indexOfMonitor].sensorValue = 'No Sensor Value Available';
      } else {
        this.workOrderMonitorsToView[indexOfMonitor].sensorValue = response.object.itemCurrentValue;
      }


    });

  }

}
