import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
//import { CreateProcedureTypeRequest } from '../../../services/mocks/models/createProcedureTypeRequest';
//import { UpdateProcedureTypeRequest } from '../../../services/mocks/models/updateProcedureTypeRequest';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { ProcedureType, ProcedureTypeService, CreateProcedureTypeRequest, UpdateProcedureTypeRequest} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-proceduretype',
  templateUrl: './proceduretype.component.html',
  styleUrls: ['./proceduretype.component.scss'],
  providers: [MockServices, ProcedureTypeService]
})
export class ProceduretypeComponent implements OnInit {

  procedureType: ProcedureType = new ProcedureType();
  availableTypes: Array<SelectItem>;

  constructor(private procedureTypeService: ProcedureTypeService, private route: ActivatedRoute, private mockServices: MockServices, public elementReference: ElementRef, private router: Router) {

  }

  ngOnInit(): void {

      this.availableTypes = [
        {label: 'Corrective Action', value: 'Corrective Action'},
        {label: 'Operate', value: 'Operate'},
        {label: 'Preventitive Action', value: 'Preventitive Action'},
        {label: 'Setup', value: 'Setup'},
        {label: 'Test', value: 'Test'}
      ];

      this.route.queryParams.subscribe(params => {

        this.procedureType.id = params['id'] == null ? 0 : Number(params['id']);

        if (this.procedureType.id !== 0) {

          this.procedureTypeService.procedureTypeGet(this.procedureType.id,env.apiVersion).subscribe(responseHandler((response) => {

            this.procedureType = response.object[0];

          }));

        }

      });

  }

  saveProcedureType() {

    let createProcedureTypeRequest = new CreateProcedureTypeRequest();
    createProcedureTypeRequest.name = this.procedureType.name;
    // TODO: Uncomment when type is added to ProcedureType
    // createProcedureTypeRequest.majorGroup = this.procedureType.type;

    this.procedureTypeService.procedureTypePost(env.apiVersion, createProcedureTypeRequest).subscribe(responseHandler((response) => {
      this.router.navigate(['app/procedures/proceduretypes']);
    }));
    
  }


  updateProcedureType() {

    let updateProcedureTypeRequest = new UpdateProcedureTypeRequest();
    updateProcedureTypeRequest.id = this.procedureType.id;
    updateProcedureTypeRequest.name = this.procedureType.name;
    // TODO: Uncomment when type is added to ProcedureType
    // updateProcedureTypeRequest.majorGroup = this.procedureType.type;

    this.procedureTypeService.procedureTypePatch(env.apiVersion, updateProcedureTypeRequest).subscribe(responseHandler((response) => {
      this.router.navigate(['app/procedures/proceduretypes']);
    }));

  }

}
