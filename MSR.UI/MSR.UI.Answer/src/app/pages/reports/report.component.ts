import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  WorkflowGroupService, WorkflowGroupModel, WorkflowGroupRoleMapModel, RoleService, UserService, WorkflowGroupUserMapModel,
  Role, AuditActionResultOfWorkflowGroupModel, CreateWorkflowGroupRequest, UpdateWorkflowGroupRequest, EnumMenuItem
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../models/lib/Utils';
declare let jQuery: any;

@Component({
    selector: 'app-reports',
    templateUrl: './report.component.html',
    styleUrls: ['./report.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class ReportComponent implements OnInit {

    
    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService, private userService: UserService,
        private elem: ElementRef, private roleService: RoleService, private workflowGroupService: WorkflowGroupService) {
        
        
    }

    ngOnInit(): void {
        throw new Error('Method not implemented.');
    }
}