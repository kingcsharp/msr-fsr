import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { CreateProcedureTypeRequest } from '../../../services/mocks/models/createProcedureTypeRequest';
import { UpdateProcedureTypeRequest } from '../../../services/mocks/models/updateProcedureTypeRequest';
import { ProcedureTypeMock } from '../../../services/mocks/models/ProcedureTypeMock';
import { MockServices } from '../../../services/mocks/services/mockservices';

@Component({
  selector: 'app-proceduretype',
  templateUrl: './proceduretype.component.html',
  styleUrls: ['./proceduretype.component.scss'],
  providers: [MockServices]
})
export class ProceduretypeComponent implements OnInit {

  procedureType: ProcedureTypeMock = new ProcedureTypeMock();
  availableTypes: Array<SelectItem>;

  constructor( private route: ActivatedRoute, private mockServices: MockServices, public elementReference: ElementRef, private router: Router) {

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

          let procedureTypes = this.mockServices.procedureTypesGet(this.procedureType.id);

          this.procedureType = procedureTypes[0];
        }

      });

  }

  saveProcedureType() {

    let createProcedureTypeRequest = new CreateProcedureTypeRequest();
    createProcedureTypeRequest.name = this.procedureType.name;
    createProcedureTypeRequest.majorGroup = this.procedureType.type;

    this.mockServices.procedureTypesPost(createProcedureTypeRequest);
    this.router.navigate(['app/procedures/proceduretypes']);
  }


  updateProcedureType() {

    let updateProcedureTypeRequest = new UpdateProcedureTypeRequest();
    updateProcedureTypeRequest.id = this.procedureType.id;
    updateProcedureTypeRequest.name = this.procedureType.name;
    updateProcedureTypeRequest.majorGroup = this.procedureType.type;

    this.mockServices.procedureTypesPatch(updateProcedureTypeRequest);
    this.router.navigate(['app/procedures/proceduretypes']);
  }

}
