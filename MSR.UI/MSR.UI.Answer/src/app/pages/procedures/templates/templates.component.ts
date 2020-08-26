import { Component, OnInit,  ElementRef  } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumApprovalTables, ProcedureStepTemplateService, ProcedureService , ProcedureStepTemplateModel,
  ProcedureTemplateService ,EnumMenuItem } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-templates',
  templateUrl: './templates.component.html',
  styleUrls: ['./templates.component.scss'],
  providers: [ProcedureStepTemplateService, ProcedureService,ProcedureTemplateService]
})
export class TemplatesComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAdd: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;
  menuItems = EnumMenuItem;
  statusOptions: any[];
  showConfirmDeleteDialog: boolean = false;
  procedureTemplateToDelete: ProcedureStepTemplateModel;

  constructor(private commonGrid: CommonGrid, private elementReference: ElementRef, 
    public globals: Globals, private procedureTemplateService: ProcedureTemplateService) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'text', label: 'Text', visible: true }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
      ];

    this.canAdd = this.hasPrivilege(this.privileges.CanCreate);
    this.canDelete = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

    this.getProcedureTemplates();

  }

  getProcedureTemplates() {

    this.globals.showLoader(true);
    this.procedureTemplateService.procedureTemplateGet(null, env.apiVersion).subscribe(responseHandler( (response) => {
        this.data = response.object;
        this.statusOptions = this.data.filter(
          (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
        ).map(x => ({ label: x.status, value: x.status }));
        this.loading = false;
    }));

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  openConfirmDeleteDialog(procedureTemplate) {

    this.procedureTemplateToDelete = procedureTemplate;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog(procedureTemplate) {
    this.procedureTemplateToDelete = procedureTemplate;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {

    this.procedureTemplateService.procedureTemplateDelete(this.procedureTemplateToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    }));
    
  }

}
