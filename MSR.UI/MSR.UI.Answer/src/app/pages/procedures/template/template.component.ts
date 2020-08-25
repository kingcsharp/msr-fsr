import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { SelectItem } from 'primeng/api';
import { RoleService, ProcedureStepTemplateService , ProcedureStepTemplateModel, CreateProcedureTemplateRequest,
  UpdateProcedureTemplateRequest, ProcedureTemplateService, ProcedureService } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { EnumMenuItem } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.scss'],
  providers: [MockServices, RoleService, ProcedureStepTemplateService, ProcedureTemplateService, ProcedureService]
})
export class TemplateComponent implements OnInit {

  menuItems = EnumMenuItem;
  procedureTemplate: any;
  baseStartOnCounterOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  availableProceduresForReference: Array<SelectItem>;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();

  constructor(private procedureService: ProcedureService, private procedureTemplateService: ProcedureTemplateService, private route: ActivatedRoute, private mockServices: MockServices,
    public elementReference: ElementRef, public roleService: RoleService, private router: Router, public globals: Globals) { }

  ngOnInit(): void {

    this.baseStartOnCounterOptions = [
      {label: 'Yes', value: true},
      {label: 'No', value: false}
    ];

    this.procedureStepTypeOptions = [
      {label: 'Procedure Step Type A', value: 1},
      {label: 'Procedure Step Type B', value: 2}
    ];

    this.procedureTemplate.text = '';
    this.procedureTemplate.comments = '';
    this.procedureTemplate.referenceFiles = new Array<any>();

    this.globals.showLoader(true);
    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({label: s.name, value: s.id}));

      this.globals.showLoader(true);
      this.procedureService.procedureGet(null, env.apiVersion).subscribe(responseHandler((procedureGetResponse) => {

        this.availableProceduresForReference = procedureGetResponse.object.map(s => ({label: s.name, value: s.id}));

        this.setProcedureTemplateForEditOrCreate();

      }));

    }));



  }

  setProcedureTemplateForEditOrCreate() {


    this.route.queryParams.subscribe(params => {

      this.procedureTemplate.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.procedureTemplate.id !== 0) {

        this.globals.showLoader(true);
        this.procedureTemplateService.procedureTemplateGet(this.procedureTemplate.id, env.apiVersion).subscribe(responseHandler((response) => {

          let procedureTemplates = response.object;

          this.procedureTemplate = procedureTemplates[0];

          procedureTemplates[0].referenceProcedures.forEach(id => {

            this.procedureTemplate.referenceProcedures.push(this.availableProceduresForReference.find(s => s.value === id).value);

          });

          this.procedureTemplate.roles.forEach(role => {

            this.selectedRoles.push(this.availableRoles.find(s => s.value === role.id).value);

          });

        }));

      }

    });

  }

  save() {

    let createProcedureTemplateRequest = new CreateProcedureTemplateRequest();
    createProcedureTemplateRequest.baseStartOnCounter = this.procedureTemplate.baseStartOnCounter;
    createProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    createProcedureTemplateRequest.estimatedStepDuration = this.procedureTemplate.estimatedStepDuration;
    createProcedureTemplateRequest.numberOfQuestionsToUse = this.procedureTemplate.numberOfQuestionsToUse;
    createProcedureTemplateRequest.procedureStepId = this.procedureTemplate.procedureStepId;
    // TODO: Uncomment when ProcedureStepTypeId is added to UpdateProcedureTemplateREquest
    // createProcedureTemplateRequest.procedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === this.procedureTemplate.procedureStepTypeId)?.value;
    createProcedureTemplateRequest.referenceDocuments = this.procedureTemplate.referenceDocuments;
    createProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    createProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    createProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    // TODO: Uncomment when CreateProcedureTemplateRequest.roles is changed from an array of roles to an array of ids (number[])
    // createProcedureTemplateRequest.roles = this.selectedRoles;
    createProcedureTemplateRequest.text = this.procedureTemplate.text;
    createProcedureTemplateRequest.title = this.procedureTemplate.title;
    createProcedureTemplateRequest.usefulLife = this.procedureTemplate.usefulLife;
    createProcedureTemplateRequest.utilization = this.procedureTemplate.utilization;

    this.globals.showLoader(true);
    this.procedureTemplateService.procedureTemplatePost(env.apiVersion, createProcedureTemplateRequest).subscribe(responseHandler((response) => {
      this.router.navigate(['app/procedures/proceduretemplates']);
    }));

  }

  update() {

    let updateProcedureTemplateRequest = new UpdateProcedureTemplateRequest();
    updateProcedureTemplateRequest.baseStartOnCounter = this.procedureTemplate.baseStartOnCounter;
    updateProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    updateProcedureTemplateRequest.estimatedStepDuration = this.procedureTemplate.estimatedStepDuration;
    updateProcedureTemplateRequest.numberOfQuestionsToUse = this.procedureTemplate.numberOfQuestionsToUse;
    updateProcedureTemplateRequest.procedureStepId = this.procedureTemplate.procedureStepId;
    // TODO: Uncomment when ProcedureStepTypeId is added to UpdateProcedureTemplateREquest
    // updateProcedureTemplateRequest.procedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === this.procedureTemplate.procedureStepTypeId)?.value;
    updateProcedureTemplateRequest.referenceDocuments = this.procedureTemplate.referenceDocuments;
    updateProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    updateProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    updateProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    // TODO: Uncomment when CreateProcedureTemplateRequest.roles is changed from an array of roles to an array of ids (number[])
    // updateProcedureTemplateRequest.roles = this.selectedRoles;
    updateProcedureTemplateRequest.text = this.procedureTemplate.text;
    updateProcedureTemplateRequest.title = this.procedureTemplate.title;
    updateProcedureTemplateRequest.usefulLife = this.procedureTemplate.usefulLife;
    updateProcedureTemplateRequest.utilization = this.procedureTemplate.utilization;

    this.globals.showLoader(true);
    this.procedureTemplateService.procedureTemplatePatch(env.apiVersion, updateProcedureTemplateRequest).subscribe(responseHandler((response) => {
      this.router.navigate(['app/procedures/proceduretemplates']);
    }));

  }

}
