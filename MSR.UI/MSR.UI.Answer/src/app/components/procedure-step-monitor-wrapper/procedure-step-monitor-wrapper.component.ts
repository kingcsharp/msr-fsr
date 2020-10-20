import { EventEmitter } from '@angular/core';
import { Component, Input, OnInit, Output } from '@angular/core';
import {
  CreateProcedureStepMonitorRequest, ProcedureStepModel, ProcedureStepMonitor, ProcedureStepMonitorService,
  UpdateProcedureStepMonitorRequest, SensorService
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { LookUpItems } from '../../utils/lookup-items';
import { Globals } from '../../models/lib/globals';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'procedurestepmonitor-wrapper',
  templateUrl: './procedure-step-monitor-wrapper.component.html',
  styleUrls: ['./procedure-step-monitor-wrapper.component.scss'],
  providers: [ProcedureStepMonitorService, SensorService]
})
export class ProcedureStepMonitorWrapperComponent implements OnInit {

  @Input() procedureStep: ProcedureStepModel;
  @Input() canEdit: boolean = false;
  @Input() canDelete: boolean = false;
  @Output() procedureStepChange: EventEmitter<ProcedureStepModel> = new EventEmitter<ProcedureStepModel>();

  procedureStepMonitors: Array<ProcedureStepMonitor> = new Array<ProcedureStepMonitor>();

  procedureStepMonitor: ProcedureStepMonitor = new ProcedureStepMonitor();
  showAddOrEditMonitorDialog: boolean = false;

  showConfirmDeleteMonitorDialog: boolean = false;
  monitorToDelete: any;

  faultHandlingOptions: Array<SelectItem>;
  shouldBeOptions: Array<SelectItem>;
  listSource: Array<SelectItem>;
  monitorTypeOptions: Array<SelectItem>;
  inputTypeOptions: Array<SelectItem>;
  yesNoOptions: Array<SelectItem>;
  passFailOptions: Array<SelectItem>;
  sensorNamesAvailable: Array<SelectItem>;

  constructor(public globals: Globals, private procedureStepMonitorService: ProcedureStepMonitorService, private sensorService: SensorService) { }

  ngOnInit(): void {

    this.yesNoOptions = [
      { label: 'Yes', value: 1 },
      { label: 'No', value: 0 }
    ];

    this.passFailOptions = [
      { label: 'Pass', value: 1 },
      { label: 'Fail', value: 0 }
    ];

    this.monitorTypeOptions = [
      { label: 'Equipment', value: 'Equipment' },
      { label: 'Number', value: 'Number' },
      { label: 'Yes or No', value: 'Yes or No' },
      { label: 'Text', value: 'Text' },
      { label: 'Pass or Fail', value: 'Pass or Fail' },
      { label: 'Select', value: 'Select' },
    ];

    this.inputTypeOptions = [
      { label: 'Manual', value: 'Manual' },
      { label: 'Sensor', value: 'Sensor' }
    ];

    this.faultHandlingOptions = [
      { label: 'RECORD AND CONTINUE', value: 'RECORD AND CONTINUE' },
      { label: 'STOP UNTIL FAULT CLEARED', value: 'STOP UNTIL FAULT CLEARED' }
    ];

    this.shouldBeOptions = [
      { label: 'EQUAL', value: 'EQUAL' },
      { label: 'ABOVE', value: 'ABOVE' },
      { label: 'BELOW', value: 'BELOW' },
      { label: 'BETWEEN', value: 'BETWEEN' }
    ];

    this.listSource = [
      { label: 'NCR Category', value: 'NCR Category' }
    ];

    this.getMonitors();
    this.getSensorNames();
  }

  getMonitors() {

    this.globals.showLoader(true);
    this.procedureStepMonitorService.procedurestep(this.procedureStep.id, null, env.apiVersion).subscribe(responseHandler((response) => {

      if (response.object.length === 0) {
        this.procedureStepMonitors = [];
      } else {
        this.procedureStepMonitors = response.object;
      }

    }));

  }

  getSensorNames() {

    this.sensorService.name(env.apiVersion).subscribe(response => {

      this.sensorNamesAvailable = response.object.map(s => ({ label: s, value: s }));

    });

  }

  openConfirmDeleteMonitorDialog(monitor: ProcedureStepMonitor) {

    this.monitorToDelete = monitor;
    this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
  }

  closeConfirmDeleteMonitorDialog() {
    this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
  }

  deleteMonitor() {
    this.globals.showLoader(true);
    this.procedureStepMonitorService.procedureStepMonitorDelete(this.monitorToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      let indexOfMonitor = this.procedureStepMonitors.findIndex(s => s.id === this.monitorToDelete.id);
      this.procedureStepMonitors.splice(indexOfMonitor, 1);
      this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
    }));

  }

  openAddOrEditMonitorDialog(monitor: ProcedureStepMonitor = new ProcedureStepMonitor()) {
    this.procedureStepMonitor = monitor;
    this.procedureStepMonitor.faultHandling = this.faultHandlingOptions.find(s => s.value === monitor.faultHandling)?.value;
    this.procedureStepMonitor.monitorType = this.monitorTypeOptions.find(s => s.value === monitor.monitorType)?.value;
    this.procedureStepMonitor.inputType = this.inputTypeOptions.find(s => s.value === monitor.inputType)?.value;
    this.procedureStepMonitor.shouldBe = this.shouldBeOptions.find(s => s.value === monitor.shouldBe)?.value;
    this.procedureStepMonitor.sendEmailNotification = monitor.sendEmailNotification;
    this.procedureStepMonitor.highTarget = monitor.highTarget;
    this.procedureStepMonitor.lowTarget = monitor.lowTarget;
    this.procedureStepMonitor.description = monitor.description === undefined ? '' : monitor.description;
    this.showAddOrEditMonitorDialog = !this.showAddOrEditMonitorDialog;
  }

  saveOrUpdateProcedureStepMonitor() {

    if (this.procedureStepMonitor.id === undefined) {

      let createProcedureStepMonitorRequest = new CreateProcedureStepMonitorRequest();
      createProcedureStepMonitorRequest.monitorType = this.procedureStepMonitor.monitorType;
      createProcedureStepMonitorRequest.inputType = this.procedureStepMonitor.inputType;
      createProcedureStepMonitorRequest.shouldBe = this.procedureStepMonitor.shouldBe;
      createProcedureStepMonitorRequest.targetValue = this.procedureStepMonitor.targetValue?.toString();
      createProcedureStepMonitorRequest.faultHandling = this.procedureStepMonitor.faultHandling;
      createProcedureStepMonitorRequest.description = this.procedureStepMonitor.description;
      createProcedureStepMonitorRequest.sendEmailNotification = this.procedureStepMonitor.sendEmailNotification;
      createProcedureStepMonitorRequest.procedureStepId = this.procedureStep.id;
      createProcedureStepMonitorRequest.lowTarget = this.procedureStepMonitor.lowTarget;
      createProcedureStepMonitorRequest.highTarget = this.procedureStepMonitor.highTarget;
      createProcedureStepMonitorRequest.sensorName = this.procedureStepMonitor.sensorName;
      this.globals.showLoader(true);
      this.procedureStepMonitorService.procedureStepMonitorPost(env.apiVersion, createProcedureStepMonitorRequest).subscribe(responseHandler((response) => {

        this.procedureStepMonitors.push(this.procedureStepMonitor);
        this.showAddOrEditMonitorDialog = !this.showAddOrEditMonitorDialog;

      }));

    } else {

      let updateProcedureStepMonitorRequest = new UpdateProcedureStepMonitorRequest();
      updateProcedureStepMonitorRequest.monitorType = this.procedureStepMonitor.monitorType;
      updateProcedureStepMonitorRequest.inputType = this.procedureStepMonitor.inputType;
      updateProcedureStepMonitorRequest.shouldBe = this.procedureStepMonitor.shouldBe;
      updateProcedureStepMonitorRequest.targetValue = this.procedureStepMonitor.targetValue?.toString();
      updateProcedureStepMonitorRequest.faultHandling = this.procedureStepMonitor.faultHandling;
      updateProcedureStepMonitorRequest.description = this.procedureStepMonitor.description;
      updateProcedureStepMonitorRequest.sendEmailNotification = this.procedureStepMonitor.sendEmailNotification;
      updateProcedureStepMonitorRequest.id = this.procedureStepMonitor.id;
      updateProcedureStepMonitorRequest.lowTarget = this.procedureStepMonitor.lowTarget;
      updateProcedureStepMonitorRequest.highTarget = this.procedureStepMonitor.highTarget;
      updateProcedureStepMonitorRequest.sensorName = this.procedureStepMonitor.sensorName;
      this.globals.showLoader(true);
      this.procedureStepMonitorService.procedureStepMonitorPatch(env.apiVersion, updateProcedureStepMonitorRequest).subscribe(responseHandler((response) => {

        this.showAddOrEditMonitorDialog = !this.showAddOrEditMonitorDialog;

      }));

    }

  }

}
