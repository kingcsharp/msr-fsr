import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { SelectItem } from 'primeng/api';
import { RoleService, ProcedureStepTemplateService  ,ProcedureStepTemplateModel, CreateProcedureTemplateRequest,
  UpdateProcedureTemplateRequest, ProcedureTemplateService, ProcedureService, EnumMenuItem, FileRequest } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-template',
  templateUrl: './template.component.html',
  styleUrls: ['./template.component.scss'],
  providers: [MockServices, RoleService, ProcedureStepTemplateService, ProcedureTemplateService, ProcedureService]
})
export class TemplateComponent implements OnInit {

  menuItems = EnumMenuItem;
  procedureTemplate: ProcedureStepTemplate = new ProcedureStepTemplate();
  baseStartOnCounterOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();

  constructor(private procedureTemplateService: ProcedureTemplateService, private route: ActivatedRoute,
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
    this.roleService.roleGet(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({label: s.name, value: s.id}));

      this.setProcedureTemplateForEditOrCreate();

    }));



  }

  setProcedureTemplateForEditOrCreate() {


    this.route.queryParams.subscribe(params => {

      this.procedureTemplate.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.procedureTemplate.id !== 0) {

        this.globals.showLoader(true);
        this.procedureTemplateService.procedureTemplateGet(this.procedureTemplate.id, env.apiVersion).subscribe(responseHandler((response) => {


          this.procedureTemplate = response.object[0];

          if(this.procedureTemplate.referenceFiles === undefined){
            this.procedureTemplate.referenceFiles = [];
          }

          this.procedureTemplate.roles.forEach(id => {

            this.selectedRoles.push(this.availableRoles.find(s => s.value === id).value);

          });

        }));

      }

    });

  }

  save() {

    let createProcedureTemplateRequest = new CreateProcedureTemplateRequest();
    createProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    createProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    createProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    createProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    createProcedureTemplateRequest.roles = this.selectedRoles;
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
    updateProcedureTemplateRequest.id = this.procedureTemplate.id;
    updateProcedureTemplateRequest.comments = this.procedureTemplate.comments;
    updateProcedureTemplateRequest.referenceFiles = this.procedureTemplate.referenceFiles;
    updateProcedureTemplateRequest.referenceProcedures = this.procedureTemplate.referenceProcedures;
    updateProcedureTemplateRequest.replacementCost = this.procedureTemplate.replacementCost;
    updateProcedureTemplateRequest.roles = this.selectedRoles;
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

export class ProcedureStepTemplate extends ProcedureStepTemplateModel{
  referenceFiles?: FileRequest[] | undefined;
}
