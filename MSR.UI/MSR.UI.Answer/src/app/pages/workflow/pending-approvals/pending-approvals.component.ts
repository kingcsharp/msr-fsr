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
declare let jQuery: any;

@Component({
  selector: 'app-pending-approvals',
  templateUrl: './pending-approvals.component.html',
  styleUrls: ['./pending-approvals.component.scss']
})
export class PendingApprovalsComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {

    this.currWorkflow = new WorkflowModel();
    this.gridStorageId = 'workflowGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Workflow Name', visible: true }),
      new ColumnsSaved({ id: 'memberStages', label: 'Member Stages', visible: true }),
      new ColumnsSaved({ id: 'activityMaps', label: 'Applicable Activities', visible: true }),
      new ColumnsSaved({ id: 'isActive', label: 'Active', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: true }),
      new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
      new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    // "id": 0,
    // "name": "string",
    // "workflowName": "string",
    // "workflowId": "string",
    // "workflowCreatedByName": "string",
    // "workflowGroupName": "string",
    // "workflowGroupId": "string",
    // "status": "string",
    // "statusId": 0

  }




}
