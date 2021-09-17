import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { DraggableItemService } from 'ngx-bootstrap/sortable';
import { SelectItem } from 'primeng/api';
import { EnumPrivilege } from '../../../models/enums/privileges';
import {
  RoleService, Procedure, ProcedureStepModel, ProcedureTemplateService, UpdateProcedureRequest, ProcedureStepTypeService, SensorService,
  ProcedureService, ProcedureStepMonitorService, EnumMenuItem, ProcedureTypeService, UpdateProcedureStepRequest, DocumentService,
  RoleRequest, CreateProcedureStepRequest, Role, FileModel, ICreateProcedureStepRequest, IUpdateProcedureStepRequest, ProcedureStepTemplateModel, IRoleRequest, FileRequest
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { Globals } from '../../../models/lib/globals';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-procedure-edit',
  templateUrl: './procedure-edit.component.html',
  styleUrls: ['./procedure-edit.component.scss'],
  providers: [DraggableItemService, ProcedureTemplateService, ProcedureService, ProcedureStepMonitorService,
    ProcedureTypeService, ProcedureStepTypeService, SensorService, DocumentService]
})
export class ProcedureEditComponent implements OnInit {

  privileges = EnumPrivilege;
  procedure: Procedure = new Procedure();
  procedureSteps: Array<any>;
  availableProcedureTypes: Array<SelectItem>;
  menuItems = EnumMenuItem;
  availableRoles: Array<Role>;
  durationTypeOptions: Array<SelectItem>;
  procedureStepTypeOptions: Array<SelectItem>;
  canEdit: boolean = false;
  canDelete: boolean = false;
  showConfirmDeleteStepDialog: boolean = false;
  procedureStepToDelete: ProcedureStepModel;
  availableProcedureStepTemplates: Array<SelectItem>;
  selectedProcedureStepTemplate: number;
  lastSavedProcedureStepOrder: Array<number>;
  documentsAvailable: Array<SelectItem>;


  constructor(private route: ActivatedRoute, public globals: Globals, public elementReference: ElementRef, private documentService: DocumentService,
    private router: Router, private roleService: RoleService, private procedureTemplateService: ProcedureTemplateService,
    private procedureService: ProcedureService, private procedureStepTypeService: ProcedureStepTypeService, private procedureTypeService: ProcedureTypeService) { }

  ngOnInit(): void {

    this.canDelete = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

    this.globals.showLoader(true);
    this.durationTypeOptions = new LookUpItems().DurationType();

    this.procedureTemplateService.procedureTemplateGet(null, env.apiVersion).pipe(take(1)).subscribe(responseHandler((procedureTemplateGetResponse) => {
      this.availableProcedureStepTemplates = procedureTemplateGetResponse.object.map(s => ({ label: s.title, value: s.id }));

      this.globals.showLoader(true);
      this.roleService.roleGet(null, null, null, null, null, null, null, null, null, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler((roleResponse) => {

        this.availableRoles = roleResponse.object.sort((a, b) => (a.name > b.name) ? 1 : -1);

        this.globals.showLoader(true);
        this.procedureTypeService.procedureTypeGet(null, null, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe((procedureTypeGetResponse) => {

          this.availableProcedureTypes = procedureTypeGetResponse.object.map(s => ({ label: s.name, value: s.id }));

          this.globals.showLoader(true);
          this.procedureStepTypeService.procedureStepType(null, env.apiVersion).pipe(take(1)).subscribe(responseHandler((procedureStepTypeResponse) => {

            this.procedureStepTypeOptions = procedureStepTypeResponse.object.map(s => ({ label: s.name, value: s.id }));

            this.globals.showLoader(true);
            this.documentService.documentGet(null, null, null, null, null, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler(documentServiceResponse => {

              this.documentsAvailable = documentServiceResponse.object.map(s => ({ label: s.name, value: s.id }));
              this.getProcedure();

            }));

          }));

        });

      }));

    }));



  }

  getProcedure() {

    this.route.queryParams.subscribe(params => {

      this.procedure.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.procedure.id !== 0) {

        this.globals.showLoader(true);
        this.procedureService.procedureGet(this.procedure.id, null, null, null, null, null, null, null,
          null, null, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler((procedrueGetResponse) => {

          this.procedure = procedrueGetResponse.object[0];

          if (this.procedure.referenceFiles === undefined) {
            this.procedure.referenceFiles = [];
          }

          this.getProcedureSteps();

        }));

      } else {

        this.router.navigate(['app/procedures/procedures']);

      }

    });

  }

  getProcedureSteps() {

    this.globals.showLoader(true);
    this.procedureService.stepGet(this.procedure.id, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler((getGetResponse) => {

      this.lastSavedProcedureStepOrder = getGetResponse.object?.map(s => s.id);
      getGetResponse.object.forEach(procedureStep => {

        if (procedureStep.referenceFiles === undefined) {
          procedureStep.referenceFiles = [];
        }

        procedureStep.referenceDocuments = new Array<SelectItem>();

        procedureStep.referenceDocumentIds?.forEach(referenceDocumentId => {
          let document = this.documentsAvailable.find(s => s.value === referenceDocumentId);
          procedureStep.referenceDocuments.push(document);
        });

        procedureStep.originalPrintOrder = procedureStep.printOrder - 1;
        if (procedureStep.printOrder !== 1) {
          procedureStep.predecessorStepId = getGetResponse.object.find(s => s.printOrder === procedureStep.printOrder - 1)?.id;
          procedureStep.predecessorStepName = getGetResponse.object.find(s => s.printOrder === procedureStep.printOrder - 1)?.title;
        } else {
          procedureStep.predecessorStepId = undefined;
          procedureStep.predecessorStepName = undefined;
        }

        if (procedureStep.text === undefined) {
          procedureStep.text = '';
        }


        procedureStep.selectedRoles = new Array<Role>();
        procedureStep.roles?.forEach(role => {
          procedureStep.selectedRoles.push(this.availableRoles.find(s => s.id === role.id));
        });


      });

      this.procedureSteps = getGetResponse.object.sort((a, b) => a.printOrder < b.printOrder ? -1 : a.printOrder > b.printOrder ? 1 : 0);

      this.procedureSteps.forEach(procedureStep => {



        procedureStep.selectedProcedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === procedureStep.procedureStepTypeId)?.value;

        procedureStep.selectedRoles = new Array<Role>();
        procedureStep.roles?.forEach(role => {

          procedureStep.selectedRoles.push(role);

        });

      });

    }));

  }


  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.RunnableProcedures, privName);
  }

  openConfirmDeleteStepDialog(procedureStep) {
    this.procedureStepToDelete = procedureStep;
    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;

  }

  closeConfirmDeleteStepDialog() {
    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;
  }

  deleteStep() {
    this.showConfirmDeleteStepDialog = !this.showConfirmDeleteStepDialog;
    this.globals.showLoader(true);
    this.procedureService.stepDelete(this.procedure.id, this.procedureStepToDelete.id, env.apiVersion).pipe(take(1)).subscribe(responseHandler(() => {
      this.procedureSteps = this.procedureSteps.filter(s => s.id !== this.procedureStepToDelete.id);
    }));

  }

  updateProcedureStep(procedureStep: any) {

    let updateProcedureStepRequest = new UpdateProcedureStepRequest({
      procedureStepId: procedureStep.id,
      equipmentTime: procedureStep.equipmentTime,
      laborTime: procedureStep.laborTime,
      predecessorStepId: procedureStep.predecessorStepId,
      procedureId: procedureStep.procedureId,
      replacementCost: procedureStep.replacementCost,
      roles: procedureStep.selectedRoles.map(s => new RoleRequest({ id: s.id })),
      stepText: procedureStep.stepText,
      title: procedureStep.title,
      usefulLife: procedureStep.usefulLife,
      utilization: procedureStep.utilization,
      procedureStepTypeId: procedureStep.selectedProcedureStepTypeId,
      printOrder: this.procedureSteps.findIndex(s => s.id === procedureStep.id) + 1,
      referenceFiles: new Array<FileModel>(),
      referenceFileIds: new Array<number>(),
      referenceDocumentIds: procedureStep.referenceDocuments.map(s => s.value)
    } as IUpdateProcedureStepRequest);

    procedureStep.referenceFiles.map((referenceFile: FileModel) => {

      if (referenceFile.fileId === undefined) {
        updateProcedureStepRequest.referenceFiles.push(referenceFile);
      } else {
        updateProcedureStepRequest.referenceFileIds.push(referenceFile.fileId);
      }

    });

    this.globals.showLoader(true);
    this.procedureService.stepPatch(this.procedure.id, env.apiVersion, updateProcedureStepRequest).pipe(take(1)).subscribe(responseHandler((response) => {

      let procedureStepsToUpdate = this.lastSavedProcedureStepOrder.filter(s => s !== procedureStep.id);

      let updateAffectedProcedureStepRequests = new Array<UpdateProcedureStepRequest>();
      procedureStepsToUpdate.forEach(procedureStepId => {

        let originalStepIndex = this.lastSavedProcedureStepOrder.findIndex(s => s === procedureStepId);
        let currentStepIndex = this.procedureSteps.findIndex(s => s.id === procedureStepId);

        if (originalStepIndex !== currentStepIndex) {

          this.globals.showLoader(true);
          let procedureStepToUpdate = this.procedureSteps.find(s => s.id === procedureStepId);
          let updateAffectedProcedureStepRequest = new UpdateProcedureStepRequest();
          updateAffectedProcedureStepRequest.procedureStepId = procedureStepToUpdate.id;
          updateAffectedProcedureStepRequest.equipmentTime = procedureStepToUpdate.equipmentTime;
          updateAffectedProcedureStepRequest.laborTime = procedureStepToUpdate.laborTime;
          updateAffectedProcedureStepRequest.predecessorStepId = procedureStepToUpdate.predecessorStepId;
          updateAffectedProcedureStepRequest.printOrder = procedureStepToUpdate.printOrder;
          updateAffectedProcedureStepRequest.procedureId = procedureStepToUpdate.procedureId;
          updateAffectedProcedureStepRequest.referenceFiles = procedureStepToUpdate.referenceFiles;
          updateAffectedProcedureStepRequest.replacementCost = procedureStepToUpdate.replacementCost;
          updateAffectedProcedureStepRequest.roles = procedureStepToUpdate.selectedRoles.map(s => new RoleRequest({ id: s.id }));
          updateAffectedProcedureStepRequest.stepText = procedureStepToUpdate.stepText;
          updateAffectedProcedureStepRequest.title = procedureStepToUpdate.title;
          updateAffectedProcedureStepRequest.usefulLife = procedureStepToUpdate.usefulLife;
          updateAffectedProcedureStepRequest.utilization = procedureStepToUpdate.utilization;
          updateAffectedProcedureStepRequest.procedureStepTypeId = procedureStepToUpdate.selectedProcedureStepTypeId;
          updateAffectedProcedureStepRequest.printOrder = this.procedureSteps.findIndex(s => s.id === procedureStepToUpdate.id) + 1;
          updateAffectedProcedureStepRequest.referenceDocumentIds = procedureStepToUpdate.referenceDocuments.map(s => s.value);
          updateAffectedProcedureStepRequests.push(updateAffectedProcedureStepRequest);

        }

        updateAffectedProcedureStepRequests.forEach(updateAffectedProcedureStepRequestNow => {
          this.procedureService.stepPatch(this.procedure.id, env.apiVersion, updateAffectedProcedureStepRequestNow).pipe(take(1)).subscribe(responseHandler(() => {

          }));
        });

        this.lastSavedProcedureStepOrder = this.procedureSteps.map(s => s.id);

      });

    }));

  }

  updateProcedure(procedure: Procedure) {
    const referenceFiles = new Array<FileRequest>();
    const referenceFileIds = new Array<number>();

    procedure.referenceFiles.forEach(file => {
      if (file.fileId !== undefined) {
        referenceFileIds.push(file.fileId);
      } else {
        referenceFiles.push(new FileRequest(file));
      }
    });

    let updateProcedureRequest = new UpdateProcedureRequest();
    updateProcedureRequest.id = procedure.id;
    updateProcedureRequest.comments = procedure.comments;
    updateProcedureRequest.duration = procedure.duration;
    updateProcedureRequest.durationType = procedure.durationType;
    updateProcedureRequest.name = procedure.name;
    updateProcedureRequest.procedureTypeId = procedure.procedureTypeId;
    updateProcedureRequest.referenceFiles = referenceFiles;
    updateProcedureRequest.referenceFileIds = referenceFileIds;
    this.globals.showLoader(true);
    this.procedureService.procedurePatch(env.apiVersion, updateProcedureRequest).pipe(take(1)).subscribe(() => {

    });
  }

  addProcedureStep() {

    if (this.selectedProcedureStepTemplate === undefined || this.selectedProcedureStepTemplate === null) {

      let createProcedureStepRequest = new CreateProcedureStepRequest({
        duration: 0,
        durationType: null,
        equipmentTime: 0,
        laborTime: 0,
        predecessorStepId: this.procedureSteps[this.procedureSteps.length - 1]?.id,
        printOrder: this.procedureSteps.length === 0 ? 1 : this.procedureSteps[this.procedureSteps.length - 1]?.printOrder + 1,
        procedureId: this.procedure.id,
        replacementCost: 0,
        roles: new Array<RoleRequest>(),
        stepText: '',
        title: '',
        usefulLife: 0,
        utilization: 0,
        procedureStepTypeId: 1,
        referenceFiles: new Array<FileModel>(),
        referenceFileIds: new Array<number>(),
        referenceDocumentIds: new Array<number>()
      } as ICreateProcedureStepRequest);

      this.addProcedureStepToProcedure(createProcedureStepRequest, undefined);



    } else {

      this.globals.showLoader(true);
      this.procedureTemplateService.procedureTemplateGet(this.selectedProcedureStepTemplate, env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {

        let procedureStepTemplateToAdd = <ProcedureStepTemplateModel>response.object[0];

        let createProcedureStepRequest = new CreateProcedureStepRequest({
          duration: 0,
          durationType: null,
          equipmentTime: procedureStepTemplateToAdd.equipmentTime === null ? 0 : procedureStepTemplateToAdd.equipmentTime,
          laborTime: procedureStepTemplateToAdd.laborTime === null ? 0 : procedureStepTemplateToAdd.laborTime,
          predecessorStepId: this.procedureSteps[this.procedureSteps.length - 1]?.id,
          printOrder: this.procedureSteps.length === 0 ? 1 : this.procedureSteps[this.procedureSteps.length - 1]?.printOrder + 1,
          procedureId: this.procedure.id,
          replacementCost: procedureStepTemplateToAdd.replacementCost,
          roles: procedureStepTemplateToAdd.roles.map(s => new RoleRequest({
            id: s
          } as IRoleRequest)),
          stepText: procedureStepTemplateToAdd.text,
          title: procedureStepTemplateToAdd.title,
          usefulLife: procedureStepTemplateToAdd.usefulLife,
          utilization: procedureStepTemplateToAdd.utilization,
          procedureStepTypeId: 1,
          referenceFiles: new Array<FileRequest>(),
          referenceFileIds: procedureStepTemplateToAdd.referenceFiles.map(s => s.fileId),
          referenceDocumentIds: new Array<number>()
        } as ICreateProcedureStepRequest);

        this.addProcedureStepToProcedure(createProcedureStepRequest, procedureStepTemplateToAdd.referenceFiles);

      }));

    }

  }

  addProcedureStepToProcedure(createProcedureStepRequest: CreateProcedureStepRequest, fileModels: Array<FileModel>) {

    this.globals.showLoader(true);
    this.procedureService.stepPost(this.procedure.id, env.apiVersion, createProcedureStepRequest).pipe(take(1)).subscribe(responseHandler(stepPostResponse => {

      let procedureStepToAdd: any = {};
      procedureStepToAdd.id = stepPostResponse.object.id;
      procedureStepToAdd.duration = stepPostResponse.object.duration;
      procedureStepToAdd.durationType = stepPostResponse.object.durationType;
      procedureStepToAdd.equipmentTime = stepPostResponse.object.equipmentTime;
      procedureStepToAdd.laborTime = stepPostResponse.object.laborTime;
      procedureStepToAdd.printOrder = stepPostResponse.object.printOrder;
      procedureStepToAdd.procedureId = stepPostResponse.object.procedureId;
      procedureStepToAdd.referenceFiles = stepPostResponse.object.referenceFiles;
      procedureStepToAdd.replacementCost = stepPostResponse.object.replacementCost;
      procedureStepToAdd.roles = stepPostResponse.object.roles;
      procedureStepToAdd.selectedRoles = new Array<Role>();
      procedureStepToAdd.roles?.forEach(role => {
        procedureStepToAdd.selectedRoles.push(this.availableRoles.find(s => s.id === role.id));
      });
      procedureStepToAdd.stepText = stepPostResponse.object.stepText;
      procedureStepToAdd.title = stepPostResponse.object.title;
      procedureStepToAdd.usefulLife = stepPostResponse.object.usefulLife;
      procedureStepToAdd.utilization = stepPostResponse.object.utilization;
      procedureStepToAdd.predecessorStepName = this.procedureSteps[this.procedureSteps.length - 1]?.title;
      procedureStepToAdd.selectedProcedureStepTypeId = this.procedureStepTypeOptions.find(s => s.value === 1)?.value;
      procedureStepToAdd.referenceFiles = fileModels === undefined ? new Array<FileModel>() : fileModels;
      procedureStepToAdd.referenceDocuments = new Array<SelectItem>();
      this.procedureSteps.push(procedureStepToAdd);
      this.procedureSteps = [...this.procedureSteps];

    }));

  }

  saveProcedureStep(procedureStep: any) {

    let createProcedureStepRequest = new CreateProcedureStepRequest({
      duration: procedureStep.duration,
      durationType: procedureStep.durationType,
      equipmentTime: procedureStep.equipmentTime,
      laborTime: procedureStep.laborTime,
      predecessorStepId: procedureStep.predecessorStepId,
      printOrder: procedureStep.printOrder,
      procedureId: procedureStep.procedureId,
      replacementCost: procedureStep.replacementCost,
      roles: procedureStep.selectedRoles.map(s => new RoleRequest({ id: s.id })),
      stepText: procedureStep.stepText,
      title: procedureStep.title,
      usefulLife: procedureStep.usefulLife,
      utilization: procedureStep.utilization,
      procedureStepTypeId: procedureStep.selectedProcedureStepTypeId,
      referenceFiles: new Array<FileModel>(),
      referenceFileIds: new Array<number>()
    } as ICreateProcedureStepRequest);

    procedureStep.referenceFiles.map((referenceFile: FileModel) => {

      if (referenceFile.fileId === undefined) {
        createProcedureStepRequest.referenceFiles.push(referenceFile);
      } else {
        createProcedureStepRequest.referenceFileIds.push(referenceFile.fileId);
      }

    });

    this.globals.showLoader(true);
    this.procedureService.stepPost(this.procedure.id, env.apiVersion, createProcedureStepRequest).pipe(take(1)).subscribe(responseHandler((response) => {
      procedureStep.id = response.object.id;
    }));


  }

  updateProcedurePredecessorAndOrder() {

    this.procedureSteps.forEach(procedureStep => {

      procedureStep.printOrder = this.procedureSteps.findIndex(s => s.id === procedureStep.id) + 1;
      if (procedureStep.printOrder !== 1) {
        procedureStep.predecessorStepId = this.procedureSteps[procedureStep.printOrder - 2].id;
        procedureStep.predecessorStepName = this.procedureSteps[procedureStep.printOrder - 2].title;
      } else {
        procedureStep.predecessorStepId = undefined;
        procedureStep.predecessorStepName = '';
      }

    });

  }

}
