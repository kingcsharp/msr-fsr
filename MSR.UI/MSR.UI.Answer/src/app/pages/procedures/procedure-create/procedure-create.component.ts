import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { LookUpItems } from '../../../utils/lookup-items';
import { Globals } from '../../../models/lib/globals';
import { Procedure, ProcedureService, CreateProcedureRequest, ProcedureTypeService, EnumMenuItem, FileRequest, ProcedureType } from '../../../services/api.client.generated';
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
  durationTypeOptions: Array<SelectItem>;
  ncrProcedureType: ProcedureType;
  tagTypes: Array<SelectItem> = [
    {label: 'Yellow Tag', value: 'Yellow Tag'},
    {label: 'Red Tag', value: 'Red Tag'},
    {label: 'No Tag', value: 'No Tag'}
  ];

  constructor(private route: ActivatedRoute, public globals: Globals, public elementReference: ElementRef,
    private router: Router, private procedureService: ProcedureService, private procedureTypeService: ProcedureTypeService) { }

  ngOnInit(): void {

    this.globals.showLoader(true);
    this.durationTypeOptions = new LookUpItems().DurationType();
    this.procedureTypeService.procedureTypeGet(null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((procedureTypeGetResponse) => {
      this.availableProcedureTypes = procedureTypeGetResponse.object.map(s => {
        if (s.name === 'Conformance Action (NCR)') { this.ncrProcedureType = s; }
        return { label: s.name, value: s.id };
      });
    }));

    this.procedure.comments = '';
    this.procedure.referenceFiles = [];
  }

  save() {
    const referenceFiles = new Array<FileRequest>();
    const referenceFileIds = new Array<number>();

    this.procedure.referenceFiles.forEach(file => {
      if (file.fileId !== undefined) {
        referenceFileIds.push(file.fileId);
      } else {
        referenceFiles.push(new FileRequest(file));
      }
    });

    let createProcedureRequest = new CreateProcedureRequest();
    createProcedureRequest.comments = this.procedure.comments;
    createProcedureRequest.duration = this.procedure.duration;
    createProcedureRequest.durationType = this.procedure.durationType;
    createProcedureRequest.name = this.procedure.name;
    createProcedureRequest.procedureTypeId = this.selectedProcedureType === undefined ? undefined : Number(this.selectedProcedureType);
    createProcedureRequest.tagType = createProcedureRequest.procedureTypeId === this.ncrProcedureType?.id ?
    this.procedure.tagType : undefined;
    createProcedureRequest.referenceFiles = referenceFiles;
    createProcedureRequest.referenceFileIds = referenceFileIds;

    this.globals.showLoader(true);
    this.procedureService.procedurePost(env.apiVersion, createProcedureRequest).subscribe(responseHandler((response) => {

      this.router.navigate(['app/procedures/procedures']);

    }));


  }

}
