import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { CreateProcedureRequest } from '../../../services/mocks/models/createProcedureRequest'
import { UpdateProcedureRequest } from '../../../services/mocks/models/updateProcedureRequest'
import { Procedure } from '../../../services/mocks/models/procedure'
import { MockServices } from '../../../services/mocks/services/mockservices';
import { SelectItem } from 'primeng/api';
import { EnumMenuItem } from '../../../models/enums/privileges';
import { RoleService, Role } from '../../../services/api.client.generated'
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';

@Component({
  selector: 'app-procedure',
  templateUrl: './procedure.component.html',
  styleUrls: ['./procedure.component.scss'],
  providers: [MockServices]
})
export class ProcedureComponent implements OnInit {

  procedure: Procedure = new Procedure();
  availableProcedureTypes: Array<SelectItem>;
  selectedProcedureType: string;
  menuItems = EnumMenuItem;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();
  durationTypeOptions: Array<SelectItem>;

  constructor(private route: ActivatedRoute, private mockServices: MockServices, public elementReference: ElementRef, private router: Router, private roleService: RoleService) { }

  ngOnInit(): void {

    this.durationTypeOptions = new LookUpItems().DurationType();

    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({ label: s.name, value: s.id }));

      this.availableProcedureTypes = this.mockServices.procedureTypesGet(null).map(s => ({ label: s.name, value: s.id }));

      this.setProcedureForEditOrCreate();

    }))

  }

  setProcedureForEditOrCreate() {

    this.route.queryParams.subscribe(params => {

      this.procedure.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.procedure.id !== 0) {

        let procedure = this.mockServices.procedureGet(this.procedure.id);

        this.procedure = procedure[0];
        this.selectedProcedureType = this.availableProcedureTypes.find(s => s.value === this.procedure.procedureType.id).value;

        this.procedure.roles.forEach(role => {

          this.selectedRoles.push(this.availableRoles.find(s => s.value === role.id).value);

        });
      } else {

        this.procedure.comment = '';
        this.procedure.referenceFiles = [];

      }

    });

  }

  save(){

    let createProcedureRequest = new CreateProcedureRequest();

    this.mockServices.procedurePost(createProcedureRequest);
    this.router.navigate(['app/procedures/procedures']);

  }

  update(){

    let updateProcedureRequest = new UpdateProcedureRequest();


    this.mockServices.procedurePatch(updateProcedureRequest);
    this.router.navigate(['app/procedures/procedures']);

  }

}
