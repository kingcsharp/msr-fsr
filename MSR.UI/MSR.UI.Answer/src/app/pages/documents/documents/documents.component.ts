import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import {
  EnumMenuItem,
  DocumentService,
  DocumentView,
  FileService,
  FileModel,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';

@Component({
  selector: 'app-documents',
  templateUrl: './documents.component.html',
  styleUrls: ['./documents.component.scss'],
  providers: [ DocumentService ],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class DocumentsComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  data: DocumentView[];
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
  showConfirmDeleteDialog: boolean = false;
  documentToDelete: DocumentView;
  fileToViewDetail: FileModel;
  getFileFlag: boolean = false;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private documentService: DocumentService,
    private fileService: FileService,
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
    this.documentService.documentGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.getDataFlag = true;
      }));
  }

  getFile(fileId: number) {
    this.getFileFlag = false;
    this.globals.showLoader(true);
    this.fileService.fileGet('Documents', null, fileId, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.fileToViewDetail = response.object[0];
        this.getDataFlag = true;
      }));
  }

  openConfirmDeleteDialog(document) {
    this.documentToDelete = document;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {
    this.globals.showLoader(true);
    this.documentService.documentDelete(this.documentToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      const index = this.data.findIndex(x => x.id === this.documentToDelete.id);
      this.data.splice(index, 1);
      this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    }));
  }

}


