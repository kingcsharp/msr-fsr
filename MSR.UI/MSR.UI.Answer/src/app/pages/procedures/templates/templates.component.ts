import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import {
  EnumApprovalTables, ProcedureStepTemplateService, ProcedureStepTemplateModel,
  EnumMenuItem
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { SelectItem } from 'primeng/api';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import { LazyLoadEvent } from 'primeng/api';
import { take } from 'rxjs/operators';

@Component({
  selector: 'app-templates',
  templateUrl: './templates.component.html',
  styleUrls: ['./templates.component.scss'],
  providers: [ProcedureStepTemplateService]
})
export class TemplatesComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridVersion: string;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  menuItems = EnumMenuItem;
  statusOptions: Array<SelectItem>;
  showConfirmDeleteDialog: boolean = false;
  procedureTemplateToDelete: ProcedureStepTemplateModel;
  templatesPrivileges: AllowedActions;
  totalRecords: number = 0;
  currentEvent: LazyLoadEvent;

  constructor(
    public commonGrid: CommonGrid,
    private elementReference: ElementRef,
    public globals: Globals, private procedureStepTemplateService: ProcedureStepTemplateService) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'templateGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'text', label: 'Text', visible: true }),
      new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];
    this.templatesPrivileges = this.globals.getEnumPrivileges(this.menuItems.Templates);
  }

  getProcedureTemplates(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(this.procedureStepTemplateService, this.procedureStepTemplateService.procedureStepTemplateGet, event).pipe(take(1)).subscribe(responseHandler((response) => {
        this.totalRecords = response.totalNumberOfRecords;
        this.currentEvent = event;
        this.data = response.object;
        this.data.map((elem) => {
          if (elem.status === null) {
            elem.status = 'Approved';
          }

        });
        this.statusOptions = this.data.filter(
          (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
        ).map(x => ({ label: x.status, value: x.status }));
        this.loading = false;
      }));
    }, 10);
  }

  openConfirmDeleteDialog(procedureTemplate) {
    this.procedureTemplateToDelete = procedureTemplate;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {
    this.procedureStepTemplateService.procedureStepTemplateDelete(this.procedureTemplateToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
      this.getProcedureTemplates(this.currentEvent);
    }));

  }

}
