import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { ProcedureTemplate } from '../../../services/mocks/models/procedureTemplate';
import { CreateProcedureTemplateRequest } from '../../../services/mocks/models/createProcedureTemplateRequest';
import { UpdateProcedureTemplateRequest } from '../../../services/mocks/models/updateProcedureTemplateRequest';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { SelectItem } from 'primeng/api';
import { RoleService, Role } from '../../../services/api.client.generated'
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.scss'],
  providers: [MockServices, RoleService]
})
export class TemplateComponent implements OnInit {

  menuItems = EnumMenuItem;
  procedureTemplate:ProcedureTemplate = new ProcedureTemplate();
  baseStartOnCounterOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  availableProceduresForReference: Array<SelectItem>;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();

  constructor(private route: ActivatedRoute, private mockServices:MockServices, public elementReference: ElementRef, public roleService: RoleService, private router: Router) { }

  ngOnInit(): void {

    this.baseStartOnCounterOptions = [
      {label:'Yes', value:true},
      {label:'No', value:false}
    ];

    this.procedureStepTypeOptions = [
      {label:'Procedure Step Type A', value:1},
      {label:'Procedure Step Type B', value:2}
    ]

    this.procedureTemplate.text = '';
    this.procedureTemplate.comments = '';
    this.procedureTemplate.referenceFiles = new Array<any>();

    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({label: s.name, value:s.id}));

      this.setProcedureTemplateForEditOrCreate();

    }))



  }

  setProcedureTemplateForEditOrCreate(){

    let proceduresForReference = this.mockServices.procedureGet(null);

    this.availableProceduresForReference = proceduresForReference.map(s => ({label: s.name, value:s.id}));

    this.route.queryParams.subscribe(params => {

      this.procedureTemplate.id = params['id'] == null ? 0 : Number(params['id']);

      if(this.procedureTemplate.id !== 0){

        let procedureTemplates = this.mockServices.procedureTemplateGet(this.procedureTemplate.id);

        this.procedureTemplate = procedureTemplates[0];
        
        procedureTemplates[0].referenceProcedures.forEach(id => {

          this.procedureTemplate.referenceProcedures.push(this.availableProceduresForReference.find(s => s.value === id).value); 

        });

        this.procedureTemplate.roles.forEach(role => {

          this.selectedRoles.push(this.availableRoles.find(s => s.value === role.id).value);

        });
      }

    });

  }

  save(){

    let createProcedureTemplateRequest = new CreateProcedureTemplateRequest();
    createProcedureTemplateRequest.baseStartOnCounter = this.procedureTemplate.baseStartOnCounter;
    createProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    createProcedureTemplateRequest.estimatedStepDuration = this.procedureTemplate.estimatedStepDuration;
    createProcedureTemplateRequest.numberOfQuestionsToUse = this.procedureTemplate.numberOfQuestionsToUse;
    createProcedureTemplateRequest.procedureStepId = this.procedureTemplate.procedureStepId;
    createProcedureTemplateRequest.procedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === this.procedureTemplate.procedureStepTypeId)?.value;
    createProcedureTemplateRequest.referenceDocuments = this.procedureTemplate.referenceDocuments;
    createProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    createProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    createProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    createProcedureTemplateRequest.roles = this.selectedRoles;
    createProcedureTemplateRequest.text = this.procedureTemplate.text;
    createProcedureTemplateRequest.title = this.procedureTemplate.title;
    createProcedureTemplateRequest.usefulLife = this.procedureTemplate.usefulLife;
    createProcedureTemplateRequest.utilization = this.procedureTemplate.utilization;

    this.mockServices.procedureTemplatePost(createProcedureTemplateRequest);
    this.router.navigate(['app/procedures/procedures']);

  }

  update(){

    let updateProcedureTemplateRequest = new UpdateProcedureTemplateRequest();
    updateProcedureTemplateRequest.baseStartOnCounter = this.procedureTemplate.baseStartOnCounter;
    updateProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    updateProcedureTemplateRequest.estimatedStepDuration = this.procedureTemplate.estimatedStepDuration;
    updateProcedureTemplateRequest.numberOfQuestionsToUse = this.procedureTemplate.numberOfQuestionsToUse;
    updateProcedureTemplateRequest.procedureStepId = this.procedureTemplate.procedureStepId;
    updateProcedureTemplateRequest.procedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === this.procedureTemplate.procedureStepTypeId)?.value;
    updateProcedureTemplateRequest.referenceDocuments = this.procedureTemplate.referenceDocuments;
    updateProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    updateProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    updateProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    updateProcedureTemplateRequest.roles = this.selectedRoles;
    updateProcedureTemplateRequest.text = this.procedureTemplate.text;
    updateProcedureTemplateRequest.title = this.procedureTemplate.title;
    updateProcedureTemplateRequest.usefulLife = this.procedureTemplate.usefulLife;
    updateProcedureTemplateRequest.utilization = this.procedureTemplate.utilization;

    this.mockServices.procedureTemplatePost(updateProcedureTemplateRequest);
    this.router.navigate(['app/procedures/procedures']);


  }

}
