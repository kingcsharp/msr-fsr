import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  WorkflowService, WorkflowModel,
  WorkflowGroupService, WorkflowStageService, WorkflowPendingApprovalService
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege, EnumApprovalTables } from '../../../models/enums/privileges';
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
  approvalTables = EnumApprovalTables; //EnumApprovalTables.ProductApproval = 6 in the backend
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridStorageId2: string;
  gridSettings2: ColumnsSaved[];
  productData: any = [];
  data: any = [];
  currAction: any = {};


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
  approveAction: any;
  cancelAction: any;
  globals: Globals;

  constructor(private _globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private workflowService: WorkflowService, private route: ActivatedRoute,
    private workflowGroupService: WorkflowGroupService, private workflowStageService: WorkflowStageService,
    private workflowPendingApprovalService: WorkflowPendingApprovalService) {

  }

  ngOnInit(): void {
    this.globals = this._globals;
    this.currWorkflow = new WorkflowModel();
    this.gridStorageId = 'approvalGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.approveAction = { 'Header': 'Pending Approval', 'Action': 'Approve' };
    this.cancelAction = { 'Header': 'Pending Approval', 'Action': 'Cancel' };
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

    this.getApprovals(EnumApprovalTables.ProductApproval, this.productData);
    this.route.params.subscribe(routeParams => {
      this.getApprovals(routeParams.table, this.data);
    });
  }

  getApprovals(table, data) {
    const dataArr = data;
    const ctrl = this;
    this.globals.showLoader(true);
    this.workflowPendingApprovalService.workflowPendingApprovalGet(table, env.apiVersion).pipe(take(1))
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

  showDialog(action, approval: any) {
    this.display = true;
    this.currAction.action = action;
    this.currAction.approval = approval;
    this.currAction.id = approval.id;
    this.currAction.activityType = approval.activityType;
  }

  clseDialog() {
    this.display = false;
  }

  onWorkflowSubmit() {
    this.globals.showLoader(true);
    const ctrl = this;
    if (this.currAction.action === this.approveAction) {
      this.workflowPendingApprovalService.workflowPendingApprovalPost(this.currAction.activityType, this.currAction.id, env.apiVersion)
        .pipe(take(1)).subscribe(responseHandler((resp) => {
          this.currAction.approval.status = resp.object.status;
          this.addToGridStatusDropdown(resp.object);
          ctrl.clseDialog();
        }, () => {
          
        }))
    }
    else {
      this.workflowPendingApprovalService.workflowPendingApprovalDelete(this.currAction.activityType, this.currAction.id, env.apiVersion)
        .pipe(take(1)).subscribe(responseHandler((resp) => {
          this.currAction.approval.status = resp.object.status;
          this.addToGridStatusDropdown(resp.object);
          ctrl.clseDialog();
        }, () => {
          // DO not update user
        }));
    }
  }


}
