import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowService, WorkflowModel, WorkflowStageMapModel, WorkflowActivityMapModel,
  AuditActionResultOfWorkflowModel, CreateWorkflowRequest, UpdateWorkflowRequest, WorkflowActivityModel,
  WorkflowGroupService, WorkflowStageService,EnumMenuItem
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable, forkJoin, of } from 'rxjs';
declare let jQuery: any;

@Component({
  selector: 'app-approval-workflow',
  templateUrl: './approval-workflow.component.html',
  styleUrls: ['./approval-workflow.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ApprovalWorkflowComponent implements OnInit {
  privileges = EnumPrivilege;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canAdd: boolean = false;
  canActivate: boolean = false;
  canEdit: boolean = false;
  display: boolean = false;
  currWorkflow: any;
  data: any;
  statuses: any[];
  memberStages: any[];
  activityMaps: any[];
  allStages: any[] = [];
  getstagesDr: boolean = false;
  getGroupsDr: boolean = false;
  allActivities: any[] = [];
  getAllActivities: boolean = false;

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private workflowService: WorkflowService,
    private workflowGroupService: WorkflowGroupService, private workflowStageService: WorkflowStageService) {

  }

  ngOnInit(): void {
    this.memberStages = [];
    this.activityMaps = [];
    this.currWorkflow = new WorkflowModel();
    this.gridStorageId = 'workflowGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Workflow Name', visible: true }),
    new ColumnsSaved({ id: 'memberStages', label: 'Member Stages', visible: true }),
    new ColumnsSaved({ id: 'activityMaps', label: 'Applicable Activities', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: true }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.statuses = [
      { label: 'Active', value: true },
      { label: 'InActive', value: false },
    ];

    this.canAdd = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivate = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

    this.getWorkflowStageDropdown();
    this.getWorkflowActivityDropdown();
    this.getWorkflows();
  }

  getWorkflowActivityDropdown() {
    const ctrl = this;
    return this.workflowService.activity(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.allActivities.push({ label: x.name, value: x.id });
        });
        this.getAllActivities = true;
      }));
  }

  getWorkflowStageDropdown() {
    const ctrl = this;
    return this.workflowStageService.workflowStageGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.allStages.push({ label: x.name, value: x.id });
        });
        this.getstagesDr = true;
      }));
  }

  getWorkflows() {
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowService.workflowGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        ctrl.data = response.object;
        ctrl.data.map((elem) => {
          this.updateStagesSavedForItem(elem);
          this.updateActivitiesSavedForItem(elem);
          return elem;
        });
      }));
  }

  updateActivitiesSavedForItem(elem: any) {
    if (!this.getAllActivities) {
      setTimeout(() => {
        this.updateStagesSavedForItem(elem);
      }, 100);
      return;
    }
    const ctrl = this;
    elem.activitiesSaved = [];
    elem.activityMaps.forEach(activityMap => {
      const foundItem = ctrl.allActivities.find(r => r.value === activityMap.workflowActivityId);
      if (foundItem !== undefined) {
        activityMap.name = foundItem.label;
        elem.activitiesSaved.push(foundItem.value);
      }
    });
    this.addToGridActivityDropdown(elem.activityMaps);
  }

  updateStagesSavedForItem(elem: any) {
    if (!this.getstagesDr) {
      setTimeout(() => {
        this.updateStagesSavedForItem(elem);
      }, 100);
      return;
    }
    const ctrl = this;
    elem.stagesSaved = [];
    elem.memberStages.forEach(activityMap => {
      const foundItem = ctrl.allStages.find(r => r.value === activityMap.workflowStageId);
      if (foundItem !== undefined) {
        activityMap.name = foundItem.label;
        elem.stagesSaved.push(foundItem.value);
      }
    });
    this.addToGridMemberStagesDropdown(elem.memberStages);
  }

  addToGridActivityDropdown(activityMaps: any) {
    const ctrl = this;
    activityMaps.forEach(item => {
      if (ctrl.activityMaps.findIndex(z => z.value === item.workflowActivityId) === -1) {
        ctrl.activityMaps.push({ label: item.name, value: item.workflowActivityId });
      }
    });
  }

  addToGridMemberStagesDropdown(memberStages: any) {
    const ctrl = this;
    memberStages.forEach(item => {
      if (ctrl.memberStages.findIndex(z => z.value === item.workflowStageId) === -1) {
        ctrl.memberStages.push({ label: item.name, value: item.workflowStageId });
      }
    });
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.ApprovalWorkflows, privName);
  }

  showDialog(workflow: WorkflowModel) {
    this.display = true;
    this.currWorkflow = this.getWorkflow(workflow);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getWorkflow(workflow: WorkflowModel) {
    if (workflow === undefined) {
      let ret = new WorkflowModel();
      ret.name = '';
      ret.isActive = true;
      return ret;
    } else {
      return workflow;
    }
  }

  removeRow(workflow) {
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowService.workflowDelete(workflow.id, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler((resp) => {
        const index = this.data.findIndex(x => x.id === workflow.id);
        this.data.splice(index, 1);
      }, () => {
        // DO not update user
      }));
  }

  onWorkflowSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method: Observable<AuditActionResultOfWorkflowModel> = null;
      this.globals.showLoader(true);

      this.currWorkflow.memberStages = [];
      this.currWorkflow.activityMaps = [];
      this.currWorkflow.stagesSaved.forEach(x => {
        this.currWorkflow.memberStages
          .push(new WorkflowStageMapModel({ workflowStageId: x, workflowId: this.currWorkflow.id }));
      });

      this.currWorkflow.activitiesSaved.forEach(x => {
        this.currWorkflow.activityMaps
          .push(new WorkflowActivityMapModel({ workflowActivityId: x, workflowId: this.currWorkflow.id }));
      });

      if (this.currWorkflow.id === undefined) {
        const createWorkflow = new CreateWorkflowRequest({
          name: this.currWorkflow.name, isActive: this.currWorkflow.isActive
          , activityMaps: this.currWorkflow.activityMaps, memberStages: this.currWorkflow.memberStages
        });
        method = this.workflowService.workflowPost(env.apiVersion, createWorkflow);
      } else {
        const updateWorkflow = new UpdateWorkflowRequest({
          id: this.currWorkflow.id,
          name: this.currWorkflow.name, isActive: this.currWorkflow.isActive
          , activityMaps: this.currWorkflow.activityMaps, memberStages: this.currWorkflow.memberStages
        });
        method = this.workflowService.workflowPatch(env.apiVersion, updateWorkflow);
      }
      this.globals.showLoader(true);
      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currWorkflow.id === undefined) {
            this.updateStagesSavedForItem(resp.object);
            this.updateActivitiesSavedForItem(resp.object);
            ctrl.data.push(resp.object);
          } else {
            this.updateStagesSavedForItem(this.currWorkflow);
            this.updateActivitiesSavedForItem(this.currWorkflow);
          }
          ctrl.clseDialog();
        }
      }, () => {
        // DO not update user
      }));
    }
  }
}
