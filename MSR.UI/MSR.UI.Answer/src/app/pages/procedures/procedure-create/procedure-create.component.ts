import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { EnumMenuItem } from '../../../models/enums/privileges';
import { RoleService, Role } from '../../../services/api.client.generated';
import { LookUpItems } from '../../../utils/lookup-items';
import { Globals } from '../../../models/lib/globals';
import { Procedure, ProcedureService, CreateProcedureRequest, ProcedureTypeService} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-procedure-create',
  templateUrl: './procedure-create.component.html',
  styleUrls: ['./procedure-create.component.scss'],
  providers: [ ProcedureService, ProcedureTypeService]
})
export class ProcedureCreateComponent implements OnInit {

  procedure: Procedure = new Procedure();
  availableProcedureTypes: Array<SelectItem>;
  selectedProcedureType: string;
  menuItems = EnumMenuItem;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();
  durationTypeOptions: Array<SelectItem>;

  constructor(private route: ActivatedRoute, public globals: Globals, public elementReference: ElementRef,
    private router: Router, private roleService: RoleService, private procedureService: ProcedureService, private procedureTypeService: ProcedureTypeService) { }

  ngOnInit(): void {

    this.globals.showLoader(true);
    this.durationTypeOptions = new LookUpItems().DurationType();

    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({ label: s.name, value: s.id }));

      this.procedureTypeService.procedureTypeGet(null, env.apiVersion).subscribe(responseHandler((procedureTypeGetResponse) => {
        this.availableProcedureTypes = procedureTypeGetResponse.object.map(s => ({ label: s.name, value: s.id }));
      }));

      this.procedure.comment = '';
      this.procedure.referenceFiles = [];

    }));

  }

  save() {

    let createProcedureRequest = new CreateProcedureRequest();
    createProcedureRequest.comments = this.procedure.comment;
    createProcedureRequest.duration = this.procedure.duration;
    createProcedureRequest.durationType = this.procedure.durationType;
    createProcedureRequest.name = this.procedure.name;
    createProcedureRequest.procedureTypeId = this.selectedProcedureType === undefined ? undefined : Number(this.selectedProcedureType);
    createProcedureRequest.referenceFiles = this.procedure.referenceFiles;
    createProcedureRequest.roleIds = this.selectedRoles.map(s => s);

    this.procedureService.procedurePost(env.apiVersion, createProcedureRequest).subscribe(responseHandler((response) => {

      this.router.navigate(['app/procedures/procedures']);

    }));


  }

}
