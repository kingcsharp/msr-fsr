import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowStageService, WorkflowStageModel, WorkflowGroupService, WorkflowGroupStageMapModel,
  AuditActionResultOfWorkflowStageModel, CreateWorkflowStageRequest, UpdateWorkflowStageRequest
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
declare let jQuery: any;

@Component({
  selector: 'app-approval-stages',
  templateUrl: './approval-stages.component.html',
  styleUrls: ['./approval-stages.component.scss']
})
export class ApprovalStagesComponent implements OnInit {
  privileges = EnumPrivilege;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canAddStages: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currWorkflowStage: any;
  data: any;
  statuses: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private workflowStageService: WorkflowStageService, private workflowGroupService: WorkflowGroupService) {

  }

  ngOnInit(): void {
    this.currWorkflowStage = new WorkflowStageModel();
    this.gridStorageId = 'workflowStageGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Approval Stage Name', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];
    this.roles = [];

    this.canAddStages = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivateStages = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditStages = this.hasPrivilege(this.privileges.CanEdit);
    this.getWorkflowStages();
    this.getWorkflowGroups();
  }

  getWorkflowStages() {
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowStageService.workflowStageGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        ctrl.data = response.object;
        this.setGroupsSaved();
      }));
  }

  setGroupsSaved() {
    const ctrl = this;
    if (!this.getWorkflowGroupsDone) {
      setTimeout(() => {
        this.setGroupsSaved()
      }, 100);
      return;
    }

    ctrl.data.forEach(element => {
      element.groupsSaved = [];
      element.groups.forEach(elem => {
        const foundItem = ctrl.workflowGroups.find(r => r.value === elem.workflowGroupId);
        if (foundItem !== undefined) {
          elem.name = foundItem.label;
          element.groupsSaved.push(foundItem.value);
        }
      });
    });
  }

  getWorkflowGroups() {
    const ctrl = this;
    this.workflowGroupService.workflowGroupGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.forEach(element => {
          ctrl.workflowGroups.push({ label: element.name, value: element.id });
        });
        this.getWorkflowGroupsDone = true;
      }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege('ApprovalStages', privName);
  }

  showDialog(workflowStage: WorkflowStageModel) {
    this.display = true;
    this.currWorkflowStage = this.getWorkflowStage(workflowStage);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getWorkflowStage(workflowStage: WorkflowStageModel) {
    if (workflowStage === undefined) {
      let ret = new WorkflowStageModel();
      ret.name = '';
      ret.isActive = true;
      ret.groups = [];
      return ret;
    } else {
      return workflowStage;
    }
  }

  workflowStageStatus(workflowStage: WorkflowStageModel) {
    const ctrl = this;
    const updateWorkflow = new UpdateWorkflowStageRequest({
      id: workflowStage.id, name: workflowStage.name,
      isActive: workflowStage.isActive,
      workflowGroupStageMapModel: workflowStage.groups
    });
    this.globals.showLoader(true);
    this.workflowStageService.workflowStagePatch(env.apiVersion, updateWorkflow).pipe(take(1)).subscribe(responseHandler((resp) => {
      if (resp.hasErrors) {
        workflowStage.isActive = !workflowStage.isActive;
      }
    }, () => {
      workflowStage.isActive = !workflowStage.isActive;
    }));
  }

  removeRow(workflowStage) {
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowStageService.workflowStageDelete(workflowStage.id, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler((resp) => {
        const index = this.data.findIndex(x => x.id === workflowStage.id);
        this.data.splice(index, 1);
      }, () => {
        // DO not update user
      }));
  }
  //onWorkflowSubmit
  onWorkflowSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method: Observable<AuditActionResultOfWorkflowStageModel> = null;
      this.globals.showLoader(true);

      this.currWorkflowStage.groups = [];
      this.currWorkflowStage.groupsSaved.forEach(x => {
        this.currWorkflowStage.groups
          .push(new WorkflowGroupStageMapModel({ workflowGroupId: x, workflowStageId: this.currWorkflowStage.id }));
      });

      if (this.currWorkflowStage.id === undefined) {
        const createWorkflow = new CreateWorkflowStageRequest({ name: this.currWorkflowStage.name, isActive: this.currWorkflowStage.isActive, workflowGroupStageMapModel: this.currWorkflowStage.groups });
        method = this.workflowStageService.workflowStagePost(env.apiVersion, createWorkflow);
      } else {
        const updateWorkflow = new UpdateWorkflowStageRequest({ id: this.currWorkflowStage.id, name: this.currWorkflowStage.name, isActive: this.currWorkflowStage.isActive, workflowGroupStageMapModel: this.currWorkflowStage.groups });
        method = this.workflowStageService.workflowStagePatch(env.apiVersion, updateWorkflow);
      }
      this.globals.showLoader(true);
      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currWorkflowStage.id === undefined) {
            ctrl.data.push(resp.object);
          }
          ctrl.clseDialog();
        }
      }, () => {
        // DO not update user
      }));
    }
  }

}
