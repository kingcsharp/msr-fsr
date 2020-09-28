import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import {
  EnumMenuItem,
  DocumentService,
  DocumentView,
  CreateDocumentRequest,
  UpdateDocumentRequest,
  FileRequest,
  RoleService,
  Role,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';

declare let jQuery: any;

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
  showDocumentDialog: boolean = false;
  document: DocumentView;
  availableRoles: Array<Role>;
  getAvailableRolesFlag: boolean = false;
  selectedRoles: Array<Role>;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private documentService: DocumentService,
    public roleService: RoleService,
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
    this.getAvailableRoles();
  }

  getDocuments() {
    this.documentService.documentGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.getDataFlag = true;
      }));
  }

  getAvailableRoles() {
    this.roleService.roleGet(env.apiVersion).subscribe(responseHandler((response) => {
      this.availableRoles = response.object;
      this.getAvailableRolesFlag = true;
    }));
  }

  openConfirmDeleteDialog(document: DocumentView) {
    this.documentToDelete = document;
    this.showConfirmDeleteDialog = true;
  }

  closeConfirmDeleteDialog() {
    this.showConfirmDeleteDialog = false;
  }

  delete() {
    this.globals.showLoader(true);
    this.documentService.documentDelete(this.documentToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      const index = this.data.findIndex(x => x.id === this.documentToDelete.id);
      this.data.splice(index, 1);
      this.showConfirmDeleteDialog = false;
    }));
  }

  openDocumentDialog(document: DocumentView) {
    this.selectedRoles = new Array<Role>();
    if (document) {
      this.document = document;
      this.document?.roleIds?.forEach(id => {
        let preSelectedRole = this.availableRoles.find(s => s.id === id);
        this.selectedRoles.push(preSelectedRole);
      });
      if (this.document.referenceFiles === undefined) {
        this.document.referenceFiles = [];
      }
    } else {
      this.document = new DocumentView();
      this.document.comments = '';
      this.document.referenceFiles = [];
      this.selectedRoles = new Array<Role>();
    }
    this.showDocumentDialog = true;
  }

  closeDocumentDialog() {
    this.document = null;
    this.showDocumentDialog = false;
    this.selectedRoles = null;
    jQuery('.parsleyjs').parsley().reset();
  }

  documentSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      const newFiles = new Array<FileRequest>();
      const uploadedFileIds = new Array<number>();
      this.document.referenceFiles.forEach(file => {
        if (file.fileId !== undefined) {
          uploadedFileIds.push(file.fileId);
        } else {
          newFiles.push(new FileRequest(file));
        }
      });
      if (this.document.id) {
        let updateDocumentRequest = new UpdateDocumentRequest();
        updateDocumentRequest.id = this.document.id;
        updateDocumentRequest.name = this.document.name;
        updateDocumentRequest.comments = this.document.comments;
        updateDocumentRequest.revision = this.document.revision;
        updateDocumentRequest.referenceFileIds = this.document.referenceFiles.map(file => file.fileId);
        // updateDocumentRequest.referenceFileIds = uploadedFileIds;
        // updateDocumentRequest.referenceFiles = newFiles;
        updateDocumentRequest.roleIds = this.selectedRoles.map(s => s.id);

        this.globals.showLoader(true);
        this.documentService.documentPatch(env.apiVersion, updateDocumentRequest).subscribe(responseHandler((response) => {
          const index = this.data.findIndex(x => x.id === this.document.id);
          this.data.splice(index, 1);
          this.data.splice(index, 0, response.object);
          this.closeDocumentDialog();
        }));
      } else {
        let createDocumentRequest = new CreateDocumentRequest();
        createDocumentRequest.name = this.document.name;
        createDocumentRequest.comments = this.document.comments;
        createDocumentRequest.revision = 0;
        createDocumentRequest.referenceFileIds = this.document.referenceFiles.map(file => file.fileId);
        // createDocumentRequest.referenceFileIds = uploadedFileIds;
        // createDocumentRequest.referenceFiles = newFiles;
        createDocumentRequest.roleIds = this.selectedRoles.map(s => s.id);

        this.globals.showLoader(true);
        this.documentService.documentPost(env.apiVersion, createDocumentRequest).subscribe(responseHandler((response) => {
          this.data.unshift(response.object);
          this.closeDocumentDialog();
        }));
      }
    }
  }

}


