import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    WorkflowService, WorkflowModel, WorkflowStageMapModel, WorkflowActivityMapModel,
    AuditActionResultOfWorkflowModel, CreateWorkflowRequest, UpdateWorkflowRequest, WorkflowActivityModel,
    WorkflowGroupService, WorkflowStageService, EnumMenuItem
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
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../../models/lib/Utils';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../../models/enums/EnumColumnType';
import { GridSaved } from '../../../models/lib/GridSaved';
declare let jQuery: any;

@Component({
    selector: 'app-adhocreport',
    templateUrl: './adhocreport.component.html',
    styleUrls: ['./adhocreport.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AdhocComponent implements OnInit {
    privileges = EnumPrivilege;
    enumColumnType = EnumColumnType;
    defaultView: ViewSaved;
    data: any;
    gridSaved: GridSaved;
    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
        private elem: ElementRef, private workflowService: WorkflowService, private route: ActivatedRoute,
        private workflowGroupService: WorkflowGroupService, private workflowStageService: WorkflowStageService) {
    }

    ngOnInit(): void {
        this.route.params.subscribe(routeParams => {
            console.log(routeParams.id)
        });

        this.gridSaved = new GridSaved({
            columnsSaved: [new ColumnsSaved({ id: 'id', label: 'Id', visible: true, type: this.enumColumnType.Number }),
            new ColumnsSaved({ id: 'name', label: 'Approval Group Name', visible: true, type: this.enumColumnType.String }),
            new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true, type: this.enumColumnType.Date }),
            new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: true, type: this.enumColumnType.String }),
            new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: true, type: this.enumColumnType.Date }),
            new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: true, type: this.enumColumnType.String })
            ],
            storageId: 'avaca' + this.elem.nativeElement.tagName.toLowerCase(),
            version: '1.0.0'
        });

        this.getWorkflowGroups();
        
    }

    getWorkflowGroups() {
        this.globals.showLoader(true);
        this.workflowGroupService.workflowGroupGet(null, env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.globals.showLoader(false);
                this.data = response.object;
            }));
    }
}
