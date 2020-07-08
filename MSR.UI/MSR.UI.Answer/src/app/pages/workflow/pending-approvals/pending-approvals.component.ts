import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowService, WorkflowModel, WorkflowStageMapModel, WorkflowActivityMapModel,
  AuditActionResultOfWorkflowModel, CreateWorkflowRequest, UpdateWorkflowRequest, WorkflowActivityModel,
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
import { Router, ActivatedRoute, ParamMap } from '@angular/router';
declare let jQuery: any;

@Component({
  selector: 'app-pending-approvals',
  templateUrl: './pending-approvals.component.html',
  styleUrls: ['./pending-approvals.component.scss']
})
export class PendingApprovalsComponent implements OnInit {

  privileges = EnumPrivilege;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridStorageId2: string;
  gridSettings2: ColumnsSaved[];
  productData: any = [];
  data: any = [];


  roles: any[];
  allRoles: any[] = [];
  canAdd: boolean = false;
  canActivate: boolean = false;
  canEdit: boolean = false;
  display: boolean = false;
  currWorkflow: any;
  statuses: any[];
  memberStages: any[];
  activityMaps: any[];
  allStages: any[] = [];
  getstagesDr: boolean = false;
  getGroupsDr: boolean = false;
  allActivities: any[] = [];
  getAllActivities: boolean = false;
  tables: any[] = [];
  statuss: any[] = [];

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private workflowService: WorkflowService, private route: ActivatedRoute,
    private workflowGroupService: WorkflowGroupService, private workflowStageService: WorkflowStageService) {

  }

  ngOnInit(): void {
    this.currWorkflow = new WorkflowModel();
    this.gridStorageId = 'approvalGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'activityType', label: 'Activity Type', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'comments', label: 'Comments', visible: true }),
      new ColumnsSaved({ id: 'workflowName', label: 'Workflow Name', visible: true }),
      new ColumnsSaved({ id: 'workflowGroupName', label: 'Workflow Group', visible: true }),
      new ColumnsSaved({ id: 'workflowCreatedByName', label: 'Initiatior', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: true })
    ];

    this.gridStorageId2 = 'approvalGridProducts' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings2 = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'activityType', label: 'Activity Type', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'comments', label: 'Comments', visible: true }),
      new ColumnsSaved({ id: 'workflowName', label: 'Workflow Name', visible: true }),
      new ColumnsSaved({ id: 'workflowGroupName', label: 'Workflow Group', visible: true }),
      new ColumnsSaved({ id: 'workflowCreatedByName', label: 'Initiatior', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: true })
    ];

    this.getApprovals(8, this.productData);
    this.route.params.subscribe(routeParams => {
      this.getApprovals(routeParams.table, this.data);
    });
  }

  getApprovals(table, data) {
    const dataArr = data;
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowService.pendingApproval(table, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.emptyArr(dataArr);
        dataArr.push(...response.object);
        dataArr.map((elem) => {
          this.addToGridStatusDropdown(elem);
          this.addToGridTableDropdown(elem);
          return elem;
        });
      }));
  }

  addToGridTableDropdown(elem: any) {
    const ctrl = this;
    if (ctrl.tables.findIndex(z => z.value === elem.activityType) === -1) {
      ctrl.tables.push({ label: elem.activityType, value: elem.activityType });
    }
  }

  addToGridStatusDropdown(elem: any) {
    const ctrl = this;
    if (ctrl.statuss.findIndex(z => z.value === elem.status) === -1) {
      ctrl.statuss.push({ label: elem.status, value: elem.status });
    }
  }

  emptyArr(arr) {
    while (arr.length > 0) {
      arr.pop();
    }
  }




}
