import { Component, OnInit, ElementRef } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { SelectItem } from 'primeng/api';
import { RoleService, EnumMenuItem, FileRequest, Role } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';
import { DocumentModel, CreateDocumentRequest, UpdateDocumentRequest} from '../mock-data';

@Component({
  selector: 'app-document',
  templateUrl: './document.component.html',
  styleUrls: ['./document.component.scss'],
  providers: [RoleService],
})
export class DocumentComponent implements OnInit {
  menuItems = EnumMenuItem;
  document: DocumentModel = new DocumentModel();
  availableRoles: Array<Role>;
  getAvailableRolesFlag: boolean = false;
  selectedRoles: Array<Role>;

  constructor(
    private route: ActivatedRoute,
    public elementReference: ElementRef,
    public roleService: RoleService,
    private router: Router,
    public globals: Globals,
  ) { }

  ngOnInit(): void {
    this.globals.showLoader(true);
    this.document.name = '';
    this.document.comments = '';
    this.document.referenceFiles = new Array<any>();
    this.getAvailableRoles();
  }

  getAvailableRoles() {
    this.roleService.roleGet(env.apiVersion).subscribe(responseHandler((response) => {
      this.availableRoles = response.object;
      this.getAvailableRolesFlag = true;
      this.initForm();
    }));
  }

  initForm() {
    this.route.queryParams.subscribe(params => {
      this.document.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.document.id !== 0) {
        // this.globals.showLoader(true);
        // this.documentService.DocumentsGet(this.document.id, env.apiVersion).subscribe(responseHandler((response) => {
        //   this.document = response.object[0];
        //   this.selectedRoles = new Array<Role>();
        //   this.document?.roles?.forEach(id => {
        //       let preSelectedRole = this.availableRoles.find(s => s.id === id);
        //       this.selectedRoles.push(preSelectedRole);
        //   });
        //   if (this.document.referenceFiles === undefined) {
        //     this.document.referenceFiles = [];
        //   }
        // }));

      } else {
        this.selectedRoles = new Array<Role>();
      }
    });

  }

  save() {

    // let createDocumentRequest = new CreateDocumentRequest();
    // createDocumentRequest.name = this.document.name;
    // createDocumentRequest.comments = this.document.comments;
    // createDocumentRequest.referenceFiles = this.document.referenceFiles;
    // createDocumentRequest.roles = this.selectedRoles.map(s => s.id);

    // this.globals.showLoader(true);
    // this.documentService.documentPost(env.apiVersion, createDocumentRequest).subscribe(responseHandler((response) => {
    //   this.router.navigate(['app/documents/documents']);
    // }));

  }

  update() {

    // let updateDocumentRequest = new UpdateDocumentRequest();
    // updateDocumentRequest.id = this.document.id;
    // updateDocumentRequest.name = this.document.name;
    // updateDocumentRequest.comments = this.document.comments;
    // updateDocumentRequest.referenceFiles = this.document.referenceFiles;
    // updateDocumentRequest.roles = this.selectedRoles.map(s => s.id);

    // this.globals.showLoader(true);
    // this.documentService.documentPatch(env.apiVersion, updateDocumentRequest).subscribe(responseHandler((response) => {
    //   this.router.navigate(['app/documents/documents']);
    // }));

  }


}
