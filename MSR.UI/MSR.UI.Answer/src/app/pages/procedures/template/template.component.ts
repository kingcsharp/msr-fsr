import { Component, OnInit, ElementRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { take } from "rxjs/operators";
import {
  RoleService,
  ProcedureStepTemplateService,
  ProcedureStepTemplateModel,
  CreateProcedureStepTemplateRequest,
  UpdateProcedureStepTemplateRequest,
  EnumMenuItem,
  FileRequest,
  Role,
  DocumentService
} from "../../../services/api.client.generated";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { Globals } from "../../../models/lib/globals";
import { SelectItem } from "primeng/api";

@Component({
  selector: "app-template",
  templateUrl: "./template.component.html",
  styleUrls: ["./template.component.scss"],
  providers: [RoleService, ProcedureStepTemplateService, DocumentService],
})
export class TemplateComponent implements OnInit {
  menuItems = EnumMenuItem;
  procedureStepTemplate: ProcedureStepTemplateModel =
    new ProcedureStepTemplateModel();
  availableRoles: Array<Role>;
  selectedRoles: Array<Role>;
  documentsAvailable: Array<SelectItem> = [];
  referenceDocuments: Array<SelectItem> = [];

  constructor(
    private procedureStepTemplateService: ProcedureStepTemplateService,
    private documentService: DocumentService,
    private route: ActivatedRoute,
    public elementReference: ElementRef,
    public roleService: RoleService,
    private router: Router,
    public globals: Globals
  ) { }

  ngOnInit(): void {
    this.procedureStepTemplate.text = "";
    this.procedureStepTemplate.comments = "";
    this.procedureStepTemplate.referenceFiles = new Array<any>();

    this.globals.showLoader(true);
    this.roleService
      .roleGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        env.apiVersion
      )
      .subscribe(
        responseHandler((response) => {
          this.availableRoles = response.object;
          this.setProcedureTemplateForEditOrCreate();
        })
      );
  }

  setProcedureTemplateForEditOrCreate() {
    this.route.queryParams.subscribe((params) => {
      this.procedureStepTemplate.id =
        params["id"] == null ? 0 : Number(params["id"]);

      if (this.procedureStepTemplate.id !== 0) {
        this.globals.showLoader(true);
        this.procedureStepTemplateService
          .procedureStepTemplateGet(
            this.procedureStepTemplate.id,
            null,
            null,
            null,
            null,
            null,
            null,
            env.apiVersion
          )
          .subscribe(
            responseHandler((response) => {
              this.procedureStepTemplate = response.object[0];
              this.selectedRoles = new Array<Role>();
              this.procedureStepTemplate?.roles?.forEach((id) => {
                let preSelectedRole = this.availableRoles.find(
                  (s) => s.id === id
                );
                this.selectedRoles.push(preSelectedRole);
              });

              if (this.procedureStepTemplate.referenceFiles === undefined) {
                this.procedureStepTemplate.referenceFiles = [];
              }

              this.loadDocument();
            })
          );
      } else {
        this.selectedRoles = new Array<Role>();
        this.loadDocument();
      }
    });
  }

  loadDocument() {
    this.documentService
      .documentGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        env.apiVersion
      )
      .pipe(take(1))
      .subscribe(
        responseHandler((documentServiceResponse) => {
          const documentsAvailable = documentServiceResponse.object.map((s) => ({
            label: s.name,
            value: s.id,
          }));

          const referenceDocuments = [];
          if (documentsAvailable.length > 0 && (this.procedureStepTemplate as any)?.referenceDocumentIds?.length > 0) {
            (this.procedureStepTemplate as any).referenceDocumentIds.forEach(Id => {
              for (let doc of documentsAvailable) {
                if (Id === parseInt(doc.value)) {
                  referenceDocuments.push({
                    value: doc.value,
                    label: doc.label
                  });
                  break;
                }
              }
            });

            this.referenceDocuments = referenceDocuments;
          }
          this.documentsAvailable = documentsAvailable;
        })
      );
  }

  onSubmit(isUpdate: boolean) {
    this.globals.showLoader(true);
    const newFiles = new Array<FileRequest>();
    const uploadedFileIds = new Array<number>();
    this.procedureStepTemplate.referenceFiles.forEach((file) => {
      if (file.fileId !== undefined) {
        uploadedFileIds.push(file.fileId);
      } else {
        newFiles.push(new FileRequest(file));
      }
    });
    const createRequest = new CreateProcedureStepTemplateRequest();
    createRequest.comments = this.procedureStepTemplate.comments;
    createRequest.referenceFileIds = uploadedFileIds;
    createRequest.referenceFiles = newFiles;
    createRequest.referenceProcedures =
      this.procedureStepTemplate.referenceProcedures;
    createRequest.replacementCost = this.procedureStepTemplate.replacementCost;
    createRequest.roles = this.selectedRoles.map((s) => s.id);
    createRequest.stepText = this.procedureStepTemplate.text;
    createRequest.title = this.procedureStepTemplate.title;
    createRequest.usefulLife = this.procedureStepTemplate.usefulLife;
    createRequest.utilization = this.procedureStepTemplate.utilization;

    createRequest.referenceDocuments = this.referenceDocuments.map((s) => s.value);

    if (isUpdate) {
      const updateRequest = new UpdateProcedureStepTemplateRequest();
      updateRequest.init(createRequest);
      updateRequest.id = this.procedureStepTemplate.id;
      this.procedureStepTemplateService
        .procedureStepTemplatePatch(env.apiVersion, updateRequest)
        .subscribe(
          responseHandler((response) => {
            this.router.navigate(["app/procedures/proceduretemplates"]);
          })
        );
    } else {
      this.procedureStepTemplateService
        .procedureStepTemplatePost(env.apiVersion, createRequest)
        .subscribe(
          responseHandler((response) => {
            this.router.navigate(["app/procedures/proceduretemplates"]);
          })
        );
    }
  }
}
