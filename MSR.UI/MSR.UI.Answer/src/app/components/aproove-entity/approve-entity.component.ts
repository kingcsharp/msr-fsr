import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import { EnumPrivilege } from '../../models/enums/privileges';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';
import { environment as env } from '../../../environments/environment';
import {
  WorkflowPendingApprovalService, EnumApprovalTables
} from '../../services/api.client.generated';

//USE: 
// approvalTables is of type: EnumApprovalTables
//<approve-entity [(show)]="showApproveButtons" [entityId]="entity.id" 
// [activityType]="approvalTables.PartApproval">
// </approve-entity>

@Component({
  selector: 'approve-entity',
  templateUrl: './approve-entity.component.html',
  styleUrls: ['./approve-entity.component.scss']
})
export class ApproveEntityComponent implements OnInit {
  display: boolean = false;
  privileges = EnumPrivilege;
  comments: string = "";
  approve: boolean = false;
  title: string = "";
  bodyText: string = "";

  @Input() activityType: EnumApprovalTables;
  @Input() show: boolean;
  @Output() showchange: EventEmitter<boolean> = new EventEmitter<boolean>();
  @Input() entityId: number;
  constructor(public _globals: Globals, private workflowPendingApprovalService: WorkflowPendingApprovalService) {

  }

  ngOnInit(): void {

  }

  showDialog(approve) {
    this.display = true;
    this.approve = approve;
    this.title = approve ? 'Submit Approval Workflow' : 'Attention';
    this.bodyText = approve ? 'Approval Workflow & Submit' : 'If you proceed you will lose any edits you made. Are you sure?'
  }

  clseDialog() {
    this.display = false;
  }

  onWorkflowSubmit() {
    this._globals.showLoader(true);
    const ctrl = this;
    if (this.approve) {
      this.workflowPendingApprovalService.workflowPendingApprovalPost(this.activityType, this.entityId, this.comments, env.apiVersion)
        .pipe(take(1)).subscribe(responseHandler((resp) => {
          this.show = false;
          this.showchange.emit(this.show);
          ctrl.clseDialog();
        }));
    }
    else {
      this.workflowPendingApprovalService.workflowPendingApprovalDelete(this.activityType, this.entityId, env.apiVersion)
        .pipe(take(1)).subscribe(responseHandler((resp) => {
          this.show = false;
          this.showchange.emit(this.show);
          ctrl.clseDialog();
        }));
    }
  }

}
