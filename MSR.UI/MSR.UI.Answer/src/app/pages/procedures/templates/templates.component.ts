import { Component, OnInit,  ElementRef  } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { ProcedureTemplate } from '../../../services/mocks/models/procedureTemplate';

@Component({
  selector: 'app-templates',
  templateUrl: './templates.component.html',
  styleUrls: ['./templates.component.scss'],
  providers: [MockServices]
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
  procedureTemplateToDelete: ProcedureTemplate;

  constructor(private mockService: MockServices, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

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

    this.getProcedureTemplates()

  }

  getProcedureTemplates(){

    this.data = this.mockService.procedureTemplateGet(null);
    this.statusOptions = this.data.filter(
      (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
    ).map(x => ({ label: x.status, value: x.status }));
    this.loading = false;
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

  delete(){

    this.mockService.procedureTemplateDelete(this.procedureTemplateToDelete.id);
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

}
