import { Component, OnInit, ElementRef } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import {
  RoleService,
  ProcedureStepTemplateService,
  ProcedureStepTemplateModel,
  CreateProcedureStepTemplateRequest,
  UpdateProcedureStepTemplateRequest,
  EnumMenuItem,
  FileRequest,
  Role,
} from "../../../services/api.client.generated";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { Globals } from "../../../models/lib/globals";

@Component({
  selector: "app-template",
  templateUrl: "./template.component.html",
  styleUrls: ["./template.component.scss"],
  providers: [RoleService, ProcedureStepTemplateService],
})
export class TemplateComponent implements OnInit {
  menuItems = EnumMenuItem;
  procedureStepTemplate: ProcedureStepTemplateModel =
    new ProcedureStepTemplateModel();
  availableRoles: Array<Role>;
  selectedRoles: Array<Role>;

  constructor(
    private procedureStepTemplateService: ProcedureStepTemplateService,
    private route: ActivatedRoute,
    public elementReference: ElementRef,
    public roleService: RoleService,
    private router: Router,
    public globals: Globals
  ) {}

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
            })
          );
      } else {
        this.selectedRoles = new Array<Role>();
      }
    });
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
