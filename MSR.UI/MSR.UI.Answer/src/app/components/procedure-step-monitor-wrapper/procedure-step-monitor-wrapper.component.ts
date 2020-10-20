import { EventEmitter } from '@angular/core';
import { Component, Input, OnInit, Output } from '@angular/core';
import { CreateProcedureStepMonitorRequest, ProcedureStepModel, ProcedureStepMonitor, ProcedureStepMonitorService, 
  UpdateProcedureStepMonitorRequest, SensorService } from '../../services/api.client.generated';
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
  monitorToAdd: ProcedureStepMonitor;
  monitorToEdit: ProcedureStepMonitor;
  showAddMonitorDialog: boolean = false;
  showEditMonitorDialog: boolean = false;
  showConfirmDeleteMonitorDialog: boolean = false;
  monitorToDelete: any;
  procedureStepToAddMonitorTo: any;
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

  openConfirmDeleteMonitorDialog(monitor) {

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

  openEditMonitorDialog(monitor: ProcedureStepMonitor) {
    this.monitorToEdit = monitor;
    this.monitorToEdit.faultHandling = this.faultHandlingOptions.find(s => s.value === monitor.faultHandling)?.value;
    this.monitorToEdit.monitorType = this.monitorTypeOptions.find(s => s.value === monitor.monitorType)?.value;
    this.monitorToEdit.inputType = this.inputTypeOptions.find(s => s.value === monitor.inputType)?.value;
    this.monitorToEdit.shouldBe = this.shouldBeOptions.find(s => s.value === monitor.shouldBe)?.value;
    this.monitorToEdit.id = monitor.id;
    this.monitorToEdit.sendEmailNotification = monitor.sendEmailNotification;
    this.monitorToEdit.highTarget = monitor.highTarget;
    this.monitorToEdit.lowTarget = monitor.lowTarget;
    this.showEditMonitorDialog = !this.showEditMonitorDialog;
  }

  closeEditMonitorDialog() {
    this.showEditMonitorDialog = !this.showEditMonitorDialog;
  }

  updateMonitor() {

    let updateProcedureStepMonitorRequest = new UpdateProcedureStepMonitorRequest();
    updateProcedureStepMonitorRequest.monitorType = this.monitorToEdit.monitorType;
    updateProcedureStepMonitorRequest.inputType = this.monitorToEdit.inputType;
    updateProcedureStepMonitorRequest.shouldBe = this.monitorToEdit.shouldBe;
    updateProcedureStepMonitorRequest.targetValue = this.monitorToEdit.targetValue?.toString();
    updateProcedureStepMonitorRequest.faultHandling = this.monitorToEdit.faultHandling;
    updateProcedureStepMonitorRequest.description = this.monitorToEdit.description;
    updateProcedureStepMonitorRequest.sendEmailNotification = this.monitorToEdit.sendEmailNotification;
    updateProcedureStepMonitorRequest.id = this.monitorToEdit.id;
    updateProcedureStepMonitorRequest.lowTarget = this.monitorToEdit.lowTarget;
    updateProcedureStepMonitorRequest.highTarget = this.monitorToEdit.highTarget;
    updateProcedureStepMonitorRequest.sensorName = this.monitorToEdit.sensorName;
    this.globals.showLoader(true);
    this.procedureStepMonitorService.procedureStepMonitorPatch(env.apiVersion, updateProcedureStepMonitorRequest).subscribe(responseHandler((response) => {

      this.showEditMonitorDialog = !this.showEditMonitorDialog;

    }));
  }

  openAddMonitorDialog(procedureStep) {
    this.procedureStepToAddMonitorTo = procedureStep;
    this.monitorToAdd = new ProcedureStepMonitor();
    this.monitorToAdd.description = '';
    this.monitorToAdd.monitorType = this.monitorTypeOptions.find(s => s.label === 'Number').value;
    this.showAddMonitorDialog = !this.showAddMonitorDialog;
  }

  closeAddMonitorDialog() {
    this.showAddMonitorDialog = !this.showAddMonitorDialog;
  }

  addMonitorToStep() {
    let createProcedureStepMonitorRequest = new CreateProcedureStepMonitorRequest();
    createProcedureStepMonitorRequest.monitorType = this.monitorToAdd.monitorType;
    createProcedureStepMonitorRequest.inputType = this.monitorToAdd.inputType;
    createProcedureStepMonitorRequest.shouldBe = this.monitorToAdd.shouldBe;
    createProcedureStepMonitorRequest.targetValue = this.monitorToAdd.targetValue?.toString();
    createProcedureStepMonitorRequest.faultHandling = this.monitorToAdd.faultHandling;
    createProcedureStepMonitorRequest.description = this.monitorToAdd.description;
    createProcedureStepMonitorRequest.sendEmailNotification = this.monitorToAdd.sendEmailNotification;
    createProcedureStepMonitorRequest.procedureStepId = this.procedureStepToAddMonitorTo.id;
    createProcedureStepMonitorRequest.lowTarget = this.monitorToAdd.lowTarget;
    createProcedureStepMonitorRequest.highTarget = this.monitorToAdd.highTarget;
    createProcedureStepMonitorRequest.sensorName = this.monitorToAdd.sensorName;
    this.globals.showLoader(true);
    this.procedureStepMonitorService.procedureStepMonitorPost(env.apiVersion, createProcedureStepMonitorRequest).subscribe(responseHandler((response) => {

      this.procedureStepMonitors.push(this.monitorToAdd);
      this.showAddMonitorDialog = !this.showAddMonitorDialog;

    }));

  }

}
