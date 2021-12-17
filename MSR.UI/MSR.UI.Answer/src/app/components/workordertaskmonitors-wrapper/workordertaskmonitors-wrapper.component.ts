import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { SensorService, WorkOrderTaskMonitorService, UpdateWorkOrderTaskMonitorRequest, IUpdateWorkOrderTaskMonitorRequest, WorkOrderModel, AuditActionResultOfWorkOrderTaskMonitorModel } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { take } from 'rxjs/operators';
import { EnumMonitorType } from '../../models/enums/EnumMonitorType';
import { EnumFailAction } from '../../models/enums/EnumFailAction';
import { EnumMonitorInputType } from '../../models/enums/EnumMonitorInputType';
import { EnumMonitorShouldBe } from '../../models/enums/EnumMonitorShouldBe';
import { Globals } from '../../models/lib/globals';
import { EnumStatusSteps } from '../../models/enums/EnumStatusSteps';

declare let jQuery: any;
declare let Parsley: any;

@Component({
  selector: 'workordertaskmonitors-wrapper',
  templateUrl: './workordertaskmonitors-wrapper.component.html',
  styleUrls: ['./workordertaskmonitors-wrapper.component.scss'],
  providers: [SensorService, WorkOrderTaskMonitorService]
})
export class WorkordertaskmonitorsWrapperComponent implements OnInit {

  @Input() workOrderMonitorsToView: Array<any>;
  @Input() locationId: number;
  @Input() doNotAllowEditing: boolean = true;
  @Input() workOrderModel: WorkOrderModel;
  @Output() closeCurrentTaskInProgress = new EventEmitter();
  workOrderMonitorYesOrNoOptions: Array<SelectItem>;
  monitorListItemOptions: Array<SelectItem>;
  sensorsAvailable: Array<SelectItem>;
  workOrderMonitorPassOrFailOptions: Array<SelectItem>;
  wasValidationCalled: boolean = false;
  failActions = EnumFailAction;
  monitorTypes = EnumMonitorType;
  monitorInputTypes = EnumMonitorInputType;
  monitorShouldBe = EnumMonitorShouldBe;
  readonly monitorValueNotAvailable = 'No Sensor Value Available';
  showNcrEmailNotificationDialog: boolean = false;
  ncrEmailDestination: string = '';
  monitorsHaveBeenSaved: boolean = false;

  constructor(private sensorService: SensorService, private workOrderTaskMonitorService: WorkOrderTaskMonitorService, public globals: Globals) { }

  ngOnInit(): void {

    this.monitorListItemOptions = [
      { label: 'Damaged in Handling', value: '1' },
      { label: 'Damaged in Storage', value: '2' },
      { label: 'Damaged in Transit', value: '4' },
      { label: 'Defect Appearance', value: '5' },
      { label: 'Defect Functional', value: '6' },
      { label: 'Defect Material', value: '7' },
      { label: 'Defect Peformance', value: '8' },
      { label: 'Defect Process', value: '9' },
      { label: 'Defect Tolerance', value: '10' },
      { label: 'Wrong Product', value: '11' },
      { label: 'Wrong ID/Traceability', value: '12' },
      { label: 'Cust NC - chips, cracks, breakage', value: '13' },
      { label: 'Cust NC - scratches, pitting', value: '14' },
      { label: 'Cust NC - other damage', value: '15' },
      { label: 'Cust NC - end of life', value: '16' },
      { label: 'Cust NC - false leak check', value: '17' },
      { label: 'Cust NC - CU protocol violation', value: '18' },
      { label: 'Cust NC - inadequate packaging', value: '19' },
      { label: 'Cust NC - missing parts/subparts', value: '20' },
      { label: 'Cust NC - wrong product', value: '21' },
      { label: 'Cust NC - shipped to wrong location', value: '22' },
      { label: 'Cust NC - incorrect paperwork', value: '23' },
      { label: 'Cust NC - cannot disassemble', value: '24' }
    ];

    this.workOrderMonitorYesOrNoOptions = [
      { label: 'Yes', value: 1 },
      { label: 'No', value: 0 }
    ];

    this.workOrderMonitorPassOrFailOptions = [
      { label: 'Pass', value: 1 },
      { label: 'Fail', value: 0 }
    ];

    Parsley.addValidator('equaltotarget', {
      requirementType: 'number',
      validateString: function (value, requirement) {
        return parseFloat(value) === parseFloat(requirement);
      },
      messages: {
        en: 'Value is not equal to the target value'
      }
    });

    Parsley.addValidator('above', {
      requirementType: 'number',
      validateString: function (value, min) {
        return parseFloat(value) >= min;
      },
      messages: {
        en: 'Value is not above or equal to target value'
      }
    });

    Parsley.addValidator('below', {
      requirementType: 'number',
      validateString: function (value, max) {
        return parseFloat(value) <= max;
      },
      messages: {
        en: 'Value is not below or equal to target value'
      }
    });

    Parsley.addValidator('between', {
      requirementType: 'number',
      validateString: function (value, low, high) {
        return parseFloat(value) >= low && parseFloat(value) <= high;
      },
      messages: {
        en: 'Value is not within acceptable range'
      }
    });

  }

  ngOnChanges(changes) {
    this.wasValidationCalled = false;
  }

  areDropDownsValid(): boolean {

    let dropDownsAreValid = true;

    let passAndFailAndYesOrNoMonitors = this.workOrderMonitorsToView.filter(s => s.procedureStepMonitor.monitorTypeId === EnumMonitorType.PassOrFail
      || s.procedureStepMonitor.monitorTypeId === EnumMonitorType.YesOrNo || s.monitorTypeId === EnumMonitorType.PassOrFail
      || s.monitorTypeId === EnumMonitorType.YesOrNo);

    passAndFailAndYesOrNoMonitors.map(monitor => {

      if (monitor.procedureStepMonitor.failAction === EnumFailAction.StopUntilFaultCleared || monitor.failAction === EnumFailAction.StopUntilFaultCleared) {

        if ((monitor.numVal === undefined || monitor.numVal === null) && (monitor.target === undefined || monitor.target === null)) {
          dropDownsAreValid = true;
        } else {

          if (monitor.procedureStepMonitor.target !== monitor.numVal &&
           monitor.target !== monitor.numVal && monitor.target !== null && monitor.procedureStepMonitor.target !== null) {
            dropDownsAreValid = false;
          }

        }

      } else if (monitor.procedureStepMonitor.failAction === EnumFailAction.RecordAndContinue || monitor.failAction === EnumFailAction.RecordAndContinue) {

        if (monitor.target == null && monitor.numVal === null) {
          dropDownsAreValid = false;
        }

      }

    });

    let selectMonitors = this.workOrderMonitorsToView.filter(s => s.procedureStepMonitor.monitorTypeId === EnumMonitorType.Select);
    selectMonitors.map(selectMonitor => {

      if (selectMonitor.multiVal === undefined || selectMonitor.multiVal === null) {
        dropDownsAreValid = false;
      }
    });


    let sensorMonitors = this.workOrderMonitorsToView.filter(s => (s.procedureStepMonitor.monitorTypeId === EnumMonitorType.Number && s.procedureStepMonitor.inputTypeId === EnumMonitorInputType.Sensor) ||
     (s.monitorTypeId === EnumMonitorType.Number && s.inputTypeId === EnumMonitorInputType.Sensor));
    sensorMonitors.map(sensorMonitor => {

      if (sensorMonitor.textVal === undefined || sensorMonitor.textVal === null || sensorMonitor.textVal === '') {
        dropDownsAreValid = false;
      }
    });

    return dropDownsAreValid;
  }

  updateMonitors(closeTask: boolean = false) {

    let sendAnNcrEmailNotification = this.workOrderMonitorsToView.map(s => s.procedureStepMonitor?.sendNCREmail).find(m => m === true);
    let primaryContactUserEmail = this.workOrderModel.purchase?.purchaseOrder?.customer?.primaryContactUser?.email;

    if (sendAnNcrEmailNotification && (primaryContactUserEmail === undefined || primaryContactUserEmail === null || primaryContactUserEmail === '') && !this.monitorsHaveBeenSaved) {
      this.showNcrEmailNotificationDialog = true;
    } else {
      this.createEndSendMonitorRequests(closeTask);
    }

  }

  validateSendNcrEmail() {
    jQuery('.ncremail-form').parsley().validate();

    if (jQuery('.ncremail-form').parsley().isValid()) {
      this.createEndSendMonitorRequests(false);
    }
  }

  createEndSendMonitorRequests(closeTask: boolean = false) {
    let toatlRequests = this.workOrderMonitorsToView.length;

    this.showNcrEmailNotificationDialog = false;
    this.workOrderMonitorsToView.map(monitor => {
      const numVal = monitor.numVal?.toString();
      let updateWorkOrderTaskMonitorRequest = new UpdateWorkOrderTaskMonitorRequest({
        comment: monitor.comment === undefined ? '' : monitor.comment,
        multiVal: monitor.multiVal === undefined ? '' : monitor.multiVal,
        textVal: monitor.textVal === undefined || monitor.textVal === this.monitorValueNotAvailable ? '' : monitor.textVal,
        numVal :numVal ? parseFloat(monitor.numVal) : null,
        workOrderTaskMonitorId: monitor.id,
        sendNCREmail: monitor.procedureStepMonitor.sendNCREmail ? this.ncrEmailDestination : undefined
      } as IUpdateWorkOrderTaskMonitorRequest);

      this.workOrderTaskMonitorService.workOrderTaskMonitor(env.apiVersion, updateWorkOrderTaskMonitorRequest)
        .pipe(take(1)).subscribe((result: AuditActionResultOfWorkOrderTaskMonitorModel) => {
          toatlRequests--;

          if (toatlRequests === 0) {
            this.monitorsHaveBeenSaved = true;
          }

          if (closeTask && toatlRequests === 0) {
            this.wasValidationCalled = false;
            this.monitorsHaveBeenSaved = false;
          }
          monitor.lastUpdated = result.object.lastUpdated;
          monitor.lastUpdatedBy = result.object.lastUpdatedBy;

          if (result.object.procedureStepMonitor.monitorTypeId !== EnumMonitorType.Number
            && result.object.procedureStepMonitor.inputTypeId !== EnumMonitorInputType.Sensor
            && result.object.procedureStepMonitor.failAction !== EnumFailAction.StopUntilFaultCleared
            && monitor.textVal !== this.monitorValueNotAvailable) {
            monitor.textVal = result.object.textVal;
          }

        });
    });

  }

  areMonitorsInValidStateToCloseTask(): boolean {

    this.wasValidationCalled = true;

    if (this.workOrderMonitorsToView.length === 0) {
      return true;
    }

    jQuery('.parsleyjs').parsley().validate();

    return (jQuery('.parsleyjs').parsley().isValid() && this.areDropDownsValid());
  }

  saveMonitors() {
    if (this.areMonitorsInValidStateToCloseTask()) {
      this.updateMonitors(true);
    }
  }

  saveMonitorsAndCloseTask() {
    if (this.areMonitorsInValidStateToCloseTask()) {
      this.updateMonitors(true);
    }
  }

  getSensorValue(indexOfMonitor: number, sensorName: string) {

    let siteId = this.workOrderModel.location.site === undefined || this.workOrderModel.location.site === null ? this.workOrderModel.location.id : this.workOrderModel.location.site;

    this.sensorService.value(sensorName, siteId, env.apiVersion).subscribe(response => {

      if (response.object === undefined) {
        this.workOrderMonitorsToView[indexOfMonitor].textVal = this.monitorValueNotAvailable;
      } else {
        this.workOrderMonitorsToView[indexOfMonitor].textVal = response.object.itemCurrentValue;
      }


    });

  }

  public get WorkOrderIsComplete(): boolean {
    return this.workOrderModel.workOrderTasks.find(s => s.status?.id === EnumStatusSteps.WaitingtoStart || s.status?.id === EnumStatusSteps.InProgress || s.status.id === EnumStatusSteps.Approved) === undefined;
  }

}
