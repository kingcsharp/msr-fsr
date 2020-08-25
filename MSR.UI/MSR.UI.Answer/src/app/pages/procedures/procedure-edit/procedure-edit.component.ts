import { Component, OnInit, ElementRef } from '@angular/core';
import { Procedure } from '../../../services/mocks/models/procedure'
import { ProcedureStep } from '../../../services/mocks/models/procedureStep'
import { SelectItem } from 'primeng/api';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { RoleService} from '../../../services/api.client.generated'
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { Globals } from '../../../models/lib/globals';
import { ActivatedRoute, Router } from '@angular/router';
import { Monitor } from '../../../services/mocks/models/monitor';
import { ProcedureStepMonitor } from '../../../services/mocks/models/procedureStepMonitor';
import { CreateProcedureStepMonitorRequest } from '../../../services/mocks/models/createProcedureStepMonitorRequest';
import { UpdateProcedureStepRequest } from '../../../services/mocks/models/updateProcedureStepRequest';
import { UpdateProcedureRequest } from '../../../services/mocks/models/updateProcedureRequest';
import { ProcedureTemplate } from '../../../services/mocks/models/procedureTemplate';
import { CreateProcedureStepRequest } from '../../../services/mocks/models/createProcedureStepRequest';
import { DraggableItemService } from 'ngx-bootstrap/sortable';

@Component({
  selector: 'app-procedure-edit',
  templateUrl: './procedure-edit.component.html',
  styleUrls: ['./procedure-edit.component.scss'],
  providers: [MockServices, DraggableItemService ]
})
export class ProcedureEditComponent implements OnInit {

  privileges = EnumPrivilege;
  procedure: Procedure = new Procedure();
  procedureSteps: Array<any>;
  availableProcedureTypes: Array<SelectItem>;
  selectedProcedureType: string;
  menuItems = EnumMenuItem;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();
  durationTypeOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  canEdit: boolean = false;
  canDelete: boolean = false;
  showConfirmDeleteMonitorDialog: boolean = false;
  monitorToDelete: any;
  procedureStepToRemoveMonitorFrom: any;
  showEditMonitorDialog: boolean = false;
  monitorToEdit: ProcedureStepMonitor;
  monitorTypeOptions: Array<SelectItem>;
  inputTypeOptions: Array<SelectItem>;
  faultHandlingOptions: Array<SelectItem>;
  shouldBeOptions: Array<SelectItem>;
  listSource: Array<SelectItem>;
  monitorToAdd: any;
  procedureStepToAddMonitorTo: any;
  showAddMonitorDialog: boolean = false;
  showConfirmDeleteStepDialog: boolean = false;
  procedureStepToDelete: ProcedureStep;
  availableProcedureStepTemplates: Array<SelectItem>;
  selectedProcedureStepTemplate: number;

  constructor(private route: ActivatedRoute, private mockServices: MockServices, public globals: Globals, public elementReference: ElementRef,
    private router: Router, private roleService: RoleService) { }

  ngOnInit(): void {

    this.canDelete = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

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
      { label: 'QR Code', value: 'QR Code' },
      { label: 'Sensor', value: 'Sensor' }
    ]

    this.procedureStepTypeOptions = [
      { label: 'Procedure Step Type A', value: 1 },
      { label: 'Procedure Step Type B', value: 2 }
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
    ]

    this.globals.showLoader(true);
    this.durationTypeOptions = new LookUpItems().DurationType();

    this.availableProcedureStepTemplates = this.mockServices.procedureTemplateGet(null).slice(0,10).map(s => ({ label: s.title, value: s.id }));

    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({ label: s.name, value: s.id }));

      this.availableProcedureTypes = this.mockServices.procedureTypesGet(null).map(s => ({ label: s.name, value: s.id }));

      this.procedure.comment = '';
      this.procedure.referenceFiles = [];

      this.route.queryParams.subscribe(params => {

        this.procedure.id = params['id'] == null ? 0 : Number(params['id']);

        if (this.procedure.id !== 0) {

          let procedures = this.mockServices.procedureGet(this.procedure.id);

          this.procedure = procedures[0];

          this.procedureSteps = this.mockServices.procedureStepGet(null).slice(0, 3);
          this.procedureSteps.forEach(procedureStep => {
            procedureStep.monitors = this.mockServices.procedureStepMonitorsGet(null).slice(0, 10);
            procedureStep.selectedProcedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === procedureStep.procedureStepTypeId).value;
            procedureStep.selectedRoles = new Array<number>();
            procedureStep.roles.forEach(role => {

              procedureStep.selectedRoles.push(this.availableRoles.find(s => s.value === role.id).value);

            });

          });

        } else {

          this.router.navigate(['app/procedures/procedures']);

        }

      });

    }))

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  openConfirmDeleteMonitorDialog(procedureStep, monitor) {

    this.procedureStepToRemoveMonitorFrom = procedureStep
    this.monitorToDelete = monitor;
    this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
  }

  closeConfirmDeleteMonitorDialog() {
    this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
  }

  deleteMonitor() {
    let indexOfMonitor = this.procedureStepToRemoveMonitorFrom.monitors.findIndex(s => s.id === this.monitorToDelete.id);
    this.procedureStepToRemoveMonitorFrom.monitors.splice(indexOfMonitor, 1);
    this.mockServices.procedureStepMonitorsDelete(this.monitorToDelete.id);
    this.showConfirmDeleteMonitorDialog = !this.showConfirmDeleteMonitorDialog;
  }

  openEditMonitorDialog(monitor: ProcedureStepMonitor) {
    this.monitorToEdit = monitor;
    this.monitorToEdit.faultHandling = this.faultHandlingOptions.find(s => s.value === monitor.faultHandling).value;
    this.monitorToEdit.type = this.monitorTypeOptions.find(s => s.value === monitor.type).value;
    this.monitorToEdit.inputType = this.inputTypeOptions.find(s => s.value === monitor.inputType).value;
    this.monitorToEdit.shouldBe = this.shouldBeOptions.find(s => s.value === monitor.shouldBe).value;
    this.showEditMonitorDialog = !this.showEditMonitorDialog;
  }

  closeEditMonitorDialog() {
    this.showEditMonitorDialog = !this.showEditMonitorDialog;
  }

  updateMonitor() {
    this.mockServices.procedureStepMonitorsPatch(this.monitorToEdit);
    this.showEditMonitorDialog = !this.showEditMonitorDialog;
  }

  openAddMonitorDialog(procedureStep) {
    this.procedureStepToAddMonitorTo = procedureStep;
    this.monitorToAdd = new ProcedureStepMonitor();
    this.monitorToAdd.description = '';
    this.showAddMonitorDialog = !this.showAddMonitorDialog;
  }

  closeAddMonitorDialog() {
    this.showAddMonitorDialog = !this.showAddMonitorDialog;
  }

  addMonitorToStep() {
    let createProcedureStepMonitorRequest = new CreateProcedureStepMonitorRequest();
    createProcedureStepMonitorRequest.inputType = this.monitorToAdd.inputType;
    createProcedureStepMonitorRequest.shouldBe = this.monitorToAdd.shouldBe;
    createProcedureStepMonitorRequest.targetValue = this.monitorToAdd.targetValue;
    createProcedureStepMonitorRequest.faultHandling = this.monitorToAdd.faultHandling;
    createProcedureStepMonitorRequest.description = this.monitorToAdd.description;
    createProcedureStepMonitorRequest.sendEmailNotification = this.monitorToAdd.sendEmailNotification;
    this.mockServices.procedureStepMonitorsPost(createProcedureStepMonitorRequest)

    this.procedureStepToAddMonitorTo.monitors.push(this.monitorToAdd);
    this.showAddMonitorDialog = !this.showAddMonitorDialog;

  }

  openConfirmDeleteStepDialog(procedureStep) {
    this.procedureStepToDelete = procedureStep;
    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;

  }

  closeConfirmDeleteStepDialog(procedureStep) {

    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;
  }

  deleteStep() {

    this.mockServices.procedureStepDelete(this.procedureStepToDelete.id);
    let procedureStepToDeleteIndex = this.procedureSteps.findIndex(s => s.id === this.procedureStepToDelete.id);
    this.procedureSteps.splice(procedureStepToDeleteIndex, 1);
    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;
  }

  updateProcedureStep(procedureStep: any) {

    let updateProcedureStepRequest = new UpdateProcedureStepRequest();
    updateProcedureStepRequest.duration = procedureStep.duration;
    updateProcedureStepRequest.durationType = procedureStep.durationType;
    updateProcedureStepRequest.equipmentTime = procedureStep.equipmentTime;
    updateProcedureStepRequest.laborTime = procedureStep.laborTime;
    updateProcedureStepRequest.predecessorStepId = procedureStep.predecessorStepId;
    updateProcedureStepRequest.printOrder = procedureStep.printOrder;
    updateProcedureStepRequest.procedureId = procedureStep.procedureId;
    updateProcedureStepRequest.referenceFiles = procedureStep.referenceFiles;
    updateProcedureStepRequest.replacementCost = procedureStep.replacementCost
    updateProcedureStepRequest.roles = procedureStep.selectedRoles;
    updateProcedureStepRequest.text = procedureStep.text;
    updateProcedureStepRequest.title = procedureStep.title;
    updateProcedureStepRequest.usefulLife = procedureStep.usefulLife;
    updateProcedureStepRequest.utilizationTime = procedureStep.utilizationTime;
    updateProcedureStepRequest.procedureStepTypeId = procedureStep.selectedProcedureStepTypeId;
    this.mockServices.procedureStepPatch(updateProcedureStepRequest);

  }

  updateProcedure(procedure: Procedure) {

    let updateProcedureRequest = new UpdateProcedureRequest();
      updateProcedureRequest.id = procedure.id;
      updateProcedureRequest.comments = procedure.comment;
      updateProcedureRequest.duration = procedure.duration;
      updateProcedureRequest.durationType = procedure.durationType;
      updateProcedureRequest.name = procedure.name;
      updateProcedureRequest.procedureTypeId = Number(this.selectedProcedureType);
      updateProcedureRequest.referenceFiles = procedure.referenceFiles;
      updateProcedureRequest.roleIds = this.selectedRoles;

    this.mockServices.procedurePatch(updateProcedureRequest);
  }

  addProcedureStep(){

    if(this.selectedProcedureStepTemplate === undefined){

      let procedureStepToAdd = new ProcedureStep();
      procedureStepToAdd.id = 0;
      procedureStepToAdd.duration = 0;
      procedureStepToAdd.durationType = '0';
      procedureStepToAdd.equipmentTime = 0;
      procedureStepToAdd.laborTime = 0;
      procedureStepToAdd.printOrder = this.procedureSteps.length;
      procedureStepToAdd.procedureId = this.procedure.id;
      procedureStepToAdd.referenceFiles = [];
      procedureStepToAdd.replacementCost = 0;
      procedureStepToAdd.roles = [];
      procedureStepToAdd.text = '';
      procedureStepToAdd.title = '';
      procedureStepToAdd.usefulLife = 0;
      procedureStepToAdd.utilizationTime = 0;
      this.procedureSteps.push(procedureStepToAdd);
      console.log(this.procedureSteps);

    }else{

      let procedureStepTemplateToAdd = this.mockServices.procedureTemplateGet(this.selectedProcedureStepTemplate)[0];
      let procedureStepToAdd = new ProcedureStep();
      procedureStepToAdd.id = 0;
      procedureStepToAdd.duration = procedureStepTemplateToAdd.estimatedStepDuration;
      procedureStepToAdd.durationType = '0';
      procedureStepToAdd.laborTime = 0;
      procedureStepToAdd.printOrder = this.procedureSteps.length;
      procedureStepToAdd.procedureId = this.procedure.id;
      procedureStepToAdd.referenceFiles = procedureStepTemplateToAdd.referenceFiles;
      procedureStepToAdd.replacementCost = 0;
      procedureStepToAdd.roles = procedureStepTemplateToAdd.roles;
      procedureStepToAdd.text = procedureStepTemplateToAdd.text;
      procedureStepToAdd.title = procedureStepTemplateToAdd.title;
      procedureStepToAdd.usefulLife = procedureStepTemplateToAdd.usefulLife;
      procedureStepToAdd.equipmentTime = 0;
      procedureStepToAdd.utilizationTime = procedureStepTemplateToAdd.utilization;
      this.procedureSteps.push(procedureStepToAdd);
      this.selectedProcedureStepTemplate = undefined;
      this.procedureSteps =  [...this.procedureSteps];
    }

  }

  saveProcedureStep(procedureStep: any){

    let createProcedureStepRequest = new CreateProcedureStepRequest();
    createProcedureStepRequest.duration = procedureStep.duration;
    createProcedureStepRequest.durationType = procedureStep.durationType;
    createProcedureStepRequest.equipmentTime = procedureStep.equipmentTime;
    createProcedureStepRequest.laborTime = procedureStep.laborTime;
    createProcedureStepRequest.predecessorStepId = procedureStep.predecessorStepId;
    createProcedureStepRequest.printOrder = procedureStep.printOrder;
    createProcedureStepRequest.procedureId = procedureStep.procedureId;
    createProcedureStepRequest.referenceFiles = procedureStep.referenceFiles;
    createProcedureStepRequest.replacementCost = procedureStep.replacementCost
    createProcedureStepRequest.roles = procedureStep.selectedRoles;
    createProcedureStepRequest.text = procedureStep.text;
    createProcedureStepRequest.title = procedureStep.title;
    createProcedureStepRequest.usefulLife = procedureStep.usefulLife;
    createProcedureStepRequest.utilizationTime = procedureStep.utilizationTime;
    createProcedureStepRequest.procedureStepTypeId = procedureStep.selectedProcedureStepTypeId;

    this.mockServices.procedureStepPost(createProcedureStepRequest);
    procedureStep.id = 100;

  }

  orderOfStepsChanged(){
    console.log('Order Changed');
  }
}
