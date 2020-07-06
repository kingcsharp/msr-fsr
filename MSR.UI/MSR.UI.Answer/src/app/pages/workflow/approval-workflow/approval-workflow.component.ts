import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowService, WorkflowModel, WorkflowStageMapModel, WorkflowActivityMapModel,
  AuditActionResultOfWorkflowGroupModel, CreateWorkflowRequest, UpdateWorkflowRequest,
  WorkflowGroupService, WorkflowStageService
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
  allGroups: any[] = [];
  getstagesDr: boolean = false;
  getGroupsDr: boolean = false;

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

    this.canAdd = true;
    this.canActivate = true;
    this.canEdit = true;
    this.getWorkflowGroupDropdown();
    this.getWorkflowStageDropdown();
    this.getWorkflows();
  }

  getWorkflowGroupDropdown() {
    const ctrl = this;
    this.globals.showLoader(true);
    return this.workflowGroupService.workflowGroupGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.returnedObject.map((x) => {
          ctrl.allGroups.push({ label: x.name, value: x.id });
        });
        this.getGroupsDr = true;
      }));
  }

  getWorkflowStageDropdown() {
    const ctrl = this;
    this.globals.showLoader(true);
    return this.workflowStageService.workflowStageGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.returnedObject.map((x) => {
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
        ctrl.data = response.object;
        ctrl.data.map((elem) => {
          this.updateGroupsSavedForItem(elem);
          this.updateStagesSavedForItem(elem);
          this.addToGridActivityDropdown(elem.activityMaps);
          this.addToGridMemberStagesDropdown(elem.memberStages);
          return elem;
        });
      }));
  }

  updateGroupsSavedForItem(elem: any) {
    if (!this.getGroupsDr) {
      setTimeout(() => {
        this.updateGroupsSavedForItem(elem)
      }, 100);
      return;
    }
    const ctrl = this;
    elem.groupsSaved = [];
    elem.activityMaps.forEach(group => {
      const foundItem = ctrl.allGroups.find(r => r.value === group.workflowActivityId);
      if (foundItem !== undefined) {
        group.name = foundItem.label;
        elem.groupsSaved.push(foundItem.value);
      }
    });
  }

  updateStagesSavedForItem(elem: any) {
    if (!this.getstagesDr) {
      setTimeout(() => {
        this.updateStagesSavedForItem(elem)
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
  }

  addToGridMemberStagesDropdown(activityMaps: any) {
    const ctrl = this;
    activityMaps.forEach(role => {
      if (ctrl.activityMaps.findIndex(z => z.value === role.name) === -1) {
        ctrl.activityMaps.push({ label: role.name, value: role.name });
      }
    });
  }

  addToGridActivityDropdown(memberStages: any) {
    const ctrl = this;
    memberStages.forEach(role => {
      if (ctrl.memberStages.findIndex(z => z.value === role.name) === -1) {
        ctrl.memberStages.push({ label: role.name, value: role.name });
      }
    });
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege('workflow', privName);
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
}
