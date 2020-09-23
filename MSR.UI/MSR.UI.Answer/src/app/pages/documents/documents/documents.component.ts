import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumMenuItem } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { DocumentModel, gridDemoData } from '../mock-data';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
  providers: [],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class DocumentsComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  data: any[];
  getDataFlag: boolean = false;
  showSaveView: boolean = false;
  savedViewsOptions: any;
  viewsSaved: Array<ViewSaved>;
  viewToSave: ViewSaved;
  gridVersion: string;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  documentsPrivileges: AllowedActions;
  approvalStatus: any[] = [
    {
      label: 'Approved',
      value: 1
    }
  ];
  showConfirmDeleteDialog: boolean = false;
  documentToDelete: DocumentModel;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'emGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'ID', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Approval Date', visible: true }),
      new ColumnsSaved({ id: 'lastUpdated.fullName', label: 'Updated By', visible: true }),
      new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true }),
    ];
    this.documentsPrivileges = this.globals.getEnumPrivileges(this.menuItems.Documents);
    this.data = [];
    this.globals.showLoader(true);
    this.getDocuments();
  }

  getDocuments() {
    this.data = gridDemoData;
    this.globals.showLoader(false);
  }

  openConfirmDeleteDialog(document) {
    this.documentToDelete = document;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {
    // this.documentsService.documentDelete(this.documentToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
    //   this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    //   this.getDocuments();
    // }));
  }

}


