import { Component, OnInit, ElementRef } from '@angular/core';
import { Procedure } from '../../../services/mocks/models/procedure'
import { ProcedureStep } from '../../../services/mocks/models/procedureStep'
import { SelectItem } from 'primeng/api';
import { EnumMenuItem } from '../../../models/enums/privileges';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { RoleService, Role } from '../../../services/api.client.generated'
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { Globals } from '../../../models/lib/globals';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  selector: 'app-procedure-edit',
  templateUrl: './procedure-edit.component.html',
  styleUrls: ['./procedure-edit.component.scss'],
  providers: [MockServices]
})
export class ProcedureEditComponent implements OnInit {

  procedure: Procedure = new Procedure();
  procedureSteps: Array<ProcedureStep>;
  availableProcedureTypes: Array<SelectItem>;
  selectedProcedureType: string;
  menuItems = EnumMenuItem;
  availableRoles: Array<SelectItem>;
  selectedRoles: Array<number> = new Array<number>();
  durationTypeOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  
  constructor(private route: ActivatedRoute, private mockServices: MockServices, public globals: Globals, public elementReference: ElementRef, 
    private router: Router, private roleService: RoleService) { }

  ngOnInit(): void {

    this.procedureStepTypeOptions = [
      {label:'Procedure Step Type A', value:1},
      {label:'Procedure Step Type B', value:2}
    ]

    this.globals.showLoader(true);
    this.durationTypeOptions = new LookUpItems().DurationType();

    this.roleService.role(env.apiVersion).subscribe(responseHandler((response) => {

      this.availableRoles = response.object.map(s => ({ label: s.name, value: s.id }));

      this.availableProcedureTypes = this.mockServices.procedureTypesGet(null).map(s => ({ label: s.name, value: s.id }));

      this.procedure.comment = '';
      this.procedure.referenceFiles = [];

      this.route.queryParams.subscribe(params => {

        this.procedure.id = params['id'] == null ? 0 : Number(params['id']);
        
        if(this.procedure.id !== 0){
  
          let procedures = this.mockServices.procedureGet(this.procedure.id);
  
          this.procedure = procedures[0];

          this.procedureSteps = this.mockServices.procedureStepGet(null).slice(0,3);

        } else {

          this.router.navigate(['app/procedures/procedures']);

        }
  
      });

    }))

  }

}
