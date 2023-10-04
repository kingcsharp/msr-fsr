import { EventEmitter } from "@angular/core";
import { Component, Input, OnInit, Output } from "@angular/core";
import {
  CreateProcedureStepMonitorRequest,
  ProcedureStepModel,
  ProcedureStepMonitor,
  ProcedureStepMonitorService,
  UpdateProcedureStepMonitorRequest,
  SensorService,
  ICreateProcedureStepMonitorRequest,
  IUpdateProcedureStepMonitorRequest,
} from "../../services/api.client.generated";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { LookUpItems } from "../../utils/lookup-items";
import { Globals } from "../../models/lib/globals";
import { SelectItem } from "primeng/api";

declare let jQuery: any;

@Component({
  selector: "procedurestepmonitor-wrapper",
  templateUrl: "./procedure-step-monitor-wrapper.component.html",
  styleUrls: ["./procedure-step-monitor-wrapper.component.scss"],
  providers: [ProcedureStepMonitorService, SensorService],
})
export class ProcedureStepMonitorWrapperComponent implements OnInit {
  @Input() procedureStep: ProcedureStepModel;
  @Input() canEdit: boolean = false;
  @Input() canDelete: boolean = false;
  @Input() unitOfMeasureOptions: Array<SelectItem> = [];
  @Output() procedureStepChange: EventEmitter<ProcedureStepModel> =
    new EventEmitter<ProcedureStepModel>();

  procedureStepMonitors: Array<ProcedureStepMonitor> =
    new Array<ProcedureStepMonitor>();

  procedureStepMonitor: ProcedureStepMonitor = new ProcedureStepMonitor();
  showAddOrEditMonitorDialog: boolean = false;

  showConfirmDeleteMonitorDialog: boolean = false;
  monitorToDelete: any;

  faultHandlingOptions: Array<SelectItem>;
  shouldBeOptions: Array<SelectItem>;
  listSource: Array<SelectItem>;
  monitorTypeOptions: Array<SelectItem>;
  inputTypeOptionsWithoutSensor: Array<SelectItem>;
  inputTypeOptionsWithSensor: Array<SelectItem>;
  yesNoOptions: Array<SelectItem>;
  passFailOptions: Array<SelectItem>;
  sensorNamesAvailable: Array<SelectItem>;

  constructor(
    public globals: Globals,
    private procedureStepMonitorService: ProcedureStepMonitorService,
    private sensorService: SensorService
  ) {}

  ngOnInit(): void {
    this.yesNoOptions = [
      { label: "Yes", value: 1 },
      { label: "No", value: 0 },
    ];

    this.passFailOptions = [
      { label: "Pass", value: 1 },
      { label: "Fail", value: 0 },
    ];

    this.monitorTypeOptions = [
      { label: "Equipment", value: "Equipment" },
      { label: "Number", value: "Number" },
      { label: "Yes or No", value: "Yes or No" },
      { label: "Text", value: "Text" },
      { label: "Pass or Fail", value: "Pass or Fail" },
      { label: "Select", value: "Select" },
    ];

    this.inputTypeOptionsWithSensor = [
      { label: "Manual", value: "Manual" },
      { label: "Sensor", value: "Sensor" },
    ];

    this.inputTypeOptionsWithoutSensor = [{ label: "Manual", value: "Manual" }];

    this.faultHandlingOptions = [
      { label: "RECORD AND CONTINUE", value: "RECORD AND CONTINUE" },
      { label: "STOP UNTIL FAULT CLEARED", value: "STOP UNTIL FAULT CLEARED" },
    ];

    this.shouldBeOptions = [
      { label: "EQUAL", value: "EQUAL" },
      { label: "ABOVE", value: "ABOVE" },
      { label: "BELOW", value: "BELOW" },
      { label: "BETWEEN", value: "BETWEEN" },
    ];

    this.listSource = [
      { label: "NCR Category", value: 1 },
      { label: "NC Disposition", value: 2 },
    ];

    this.getMonitors();
    this.getSensorNames();
  }

  getMonitors() {
    this.globals.showLoader(true);
    this.procedureStepMonitorService
      .procedurestep(this.procedureStep.id, null, env.apiVersion)
      .subscribe(
        responseHandler((response) => {
          if (response.object.length === 0) {
            this.procedureStepMonitors = [];
          } else {
            this.procedureStepMonitors = response.object;
          }
        })
      );
  }

  getSensorNames() {
    this.sensorService.name(env.apiVersion).subscribe((response) => {
      this.sensorNamesAvailable = response.object.map((s) => ({
        label: s,
        value: s,
      }));
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
    this.procedureStepMonitorService
      .procedureStepMonitorDelete(this.monitorToDelete.id, env.apiVersion)
      .subscribe(
        responseHandler((response) => {
          let indexOfMonitor = this.procedureStepMonitors.findIndex(
            (s) => s.id === this.monitorToDelete.id
          );
          this.procedureStepMonitors.splice(indexOfMonitor, 1);
          this.showConfirmDeleteMonitorDialog =
            !this.showConfirmDeleteMonitorDialog;
        })
      );
  }

  openAddOrEditMonitorDialog(
    monitor: ProcedureStepMonitor = new ProcedureStepMonitor()
  ) {
    this.procedureStepMonitor = monitor;
    this.procedureStepMonitor.failAction = this.faultHandlingOptions.find(
      (s) => s.value === monitor.failAction
    )?.value;
    this.procedureStepMonitor.monitorType = this.monitorTypeOptions.find(
      (s) => s.value === monitor.monitorType
    )?.value;
    this.procedureStepMonitor.unitofMeasureId = this.unitOfMeasureOptions.find(
      (s) => s.value === monitor.unitofMeasureId
    )?.value;
    this.procedureStepMonitor.inputType = this.inputTypeOptionsWithSensor.find(
      (s) => s.value === monitor.inputType
    )?.value;
    this.procedureStepMonitor.shouldBe = this.shouldBeOptions.find(
      (s) => s.value === monitor.shouldBe
    )?.value;
    this.procedureStepMonitor.monitorListId = this.listSource.find(
      (s) => s.value === monitor.shouldBe
    )?.value;
    this.procedureStepMonitor.sendNCREmail = monitor.sendNCREmail;
    this.procedureStepMonitor.highTarget = monitor.highTarget;
    this.procedureStepMonitor.lowTarget = monitor.lowTarget;
    this.procedureStepMonitor.description =
      monitor.description === undefined ? "" : monitor.description;
    this.showAddOrEditMonitorDialog = !this.showAddOrEditMonitorDialog;
  }

  getUnitOfMeasureName(id: number) {
    return this.unitOfMeasureOptions.find(
      (s) => s.value === id
    )?.label;
  }

  saveOrUpdateProcedureStepMonitor() {
    jQuery(".parsleyjs").parsley().validate();
    if (jQuery(".parsleyjs").parsley().isValid()) {
      const lowTarget = this.procedureStepMonitor.lowTarget?.toString();
      const highTarget = this.procedureStepMonitor.highTarget?.toString();
      const target = this.procedureStepMonitor.target?.toString();

      if (this.procedureStepMonitor.id === undefined) {
        let createProcedureStepMonitorRequest =
          new CreateProcedureStepMonitorRequest({
            monitorType: this.procedureStepMonitor.monitorType,
            inputType: this.procedureStepMonitor.inputType,
            shouldBe: this.procedureStepMonitor.shouldBe,
            target: target ? parseFloat(target) : null,
            failAction: this.procedureStepMonitor.failAction,
            description: this.procedureStepMonitor.description,
            sendNCREmail: this.procedureStepMonitor.sendNCREmail,
            procedureStepId: this.procedureStep.id,
            lowTarget: lowTarget ? parseFloat(lowTarget) : null,
            highTarget: highTarget ? parseFloat(highTarget) : null,
            sensorName: this.procedureStepMonitor.sensorName,
            monitorListId: this.procedureStepMonitor.monitorListId,
            unitofMeasureId: this.procedureStepMonitor.unitofMeasureId,
          } as ICreateProcedureStepMonitorRequest);

        this.globals.showLoader(true);
        this.procedureStepMonitorService
          .procedureStepMonitorPost(
            env.apiVersion,
            createProcedureStepMonitorRequest
          )
          .subscribe(
            responseHandler((response) => {
              this.procedureStepMonitors.push(response.object);
              this.showAddOrEditMonitorDialog =
                !this.showAddOrEditMonitorDialog;
            })
          );
      } else {
        let updateProcedureStepMonitorRequest =
          new UpdateProcedureStepMonitorRequest({
            monitorType: this.procedureStepMonitor.monitorType,
            inputType: this.procedureStepMonitor.inputType,
            shouldBe: this.procedureStepMonitor.shouldBe,
            description: this.procedureStepMonitor.description,
            id: this.procedureStepMonitor.id,
            lowTarget: lowTarget ? parseFloat(lowTarget) : null,
            highTarget: highTarget ? parseFloat(highTarget) : null,
            sensorName: this.procedureStepMonitor.sensorName,
            failAction: this.procedureStepMonitor.failAction,
            monitorListId: this.procedureStepMonitor.monitorListId,
            unitofMeasureId: this.procedureStepMonitor.unitofMeasureId,
            sendNCREmail: this.procedureStepMonitor.sendNCREmail,
            target: target ? parseFloat(target) : null,
          } as IUpdateProcedureStepMonitorRequest);

        this.globals.showLoader(true);
        this.procedureStepMonitorService
          .procedureStepMonitorPatch(
            env.apiVersion,
            updateProcedureStepMonitorRequest
          )
          .subscribe(
            responseHandler((response) => {
              this.showAddOrEditMonitorDialog =
                !this.showAddOrEditMonitorDialog;
            })
          );
      }
    }
  }
}
