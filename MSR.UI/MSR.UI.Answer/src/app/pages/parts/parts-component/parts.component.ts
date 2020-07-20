import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  PartService, PartModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
declare let jQuery: any;

@Component({
  selector: 'app-parts',
  templateUrl: './parts.component.html'
})

export class PartsComponent implements OnInit {
  privileges = EnumPrivilege;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canAddStages: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currPart: any;
  data: any;
  isKitStatus: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;
  allParts: any[] = [];

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private partsService: PartService) {
  }

  ngOnInit(): void {
    this.currPart = new PartModel();
    this.gridStorageId = 'partsGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
    new ColumnsSaved({ id: 'partNumber', label: 'Part Number', visible: true }),
    new ColumnsSaved({ id: 'oemPartNumber', label: 'OEM Part Number', visible: true }),
    new ColumnsSaved({ id: 'isKit', label: 'Is Kit', visible: true }),
    new ColumnsSaved({ id: 'qty', label: 'QTY', visible: true }),
    new ColumnsSaved({ id: 'parentId', label: 'Parent Id', visible: true }),
    new ColumnsSaved({ id: 'maximumCycles', label: 'Maximun Cycles', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];
    this.isKitStatus = [{ label: 'Is Kit', value: true },
    { label: 'Is Not Kit', value: false }];

    this.canAddStages = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivateStages = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditStages = this.hasPrivilege(this.privileges.CanEdit);
    this.getParts();
  }

  getParts() {
    // const ctrl = this;
    this.globals.showLoader(true);
    this.partsService.partGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
        this.updatePartsDropdown();
      }));
  }

  updatePartsDropdown() {
    const ctrl = this;
    this.data.forEach(element => {
      var userIndex = ctrl.allParts.findIndex(z => z.value === element.id && element.isKit);
      if (userIndex < 0) {
        ctrl.allParts.push({ label:`${element.name} [${element.partNumber}] Used In`, value: element.id });
      }
    });
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege('Parts', privName);
  }

  showDialog(part: PartModel) {
    this.display = true;
    this.currPart = this.getPart(part);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getPart(part: PartModel) {
    if (part === undefined) {
      let ret = new PartModel();
      ret.isKit = false;
      ret.name = '';
      return ret;
    } else {
      part.isKit = (part.isKit === null || part.isKit === undefined) ? false : part.isKit;
      return part;
    }
  }

  removeRow(part) {
    const ctrl = this;
    this.globals.showLoader(true);
    // this.workflowStageService.workflowStageDelete(part.id, env.apiVersion)
    //   .pipe(take(1)).subscribe(responseHandler((resp) => {
    //     const index = this.data.findIndex(x => x.id === workflowStage.id);
    //     this.data.splice(index, 1);
    //   }, () => {
    //     // DO not update user
    //   }));
  }

  //onWorkflowSubmit
  onpartSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      // let method: Observable<AuditActionResultOfWorkflowStageModel> = null;
      // this.globals.showLoader(true);

      // this.currWorkflowStage.groups = [];
      // this.currWorkflowStage.groupsSaved.forEach(x => {
      //   this.currWorkflowStage.groups
      //     .push(new WorkflowGroupStageMapModel({ workflowGroupId: x, workflowStageId: this.currWorkflowStage.id }));
      // });

      // if (this.currWorkflowStage.id === undefined) {
      //   const createWorkflow = new CreateWorkflowStageRequest({ name: this.currWorkflowStage.name, isActive: this.currWorkflowStage.isActive, workflowGroupStageMapModel: this.currWorkflowStage.groups });
      //   method = this.workflowStageService.workflowStagePost(env.apiVersion, createWorkflow);
      // } else {
      //   const updateWorkflow = new UpdateWorkflowStageRequest({ id: this.currWorkflowStage.id, name: this.currWorkflowStage.name, isActive: this.currWorkflowStage.isActive, workflowGroupStageMapModel: this.currWorkflowStage.groups });
      //   method = this.workflowStageService.workflowStagePatch(env.apiVersion, updateWorkflow);
      // }
      // this.globals.showLoader(true);
      // method.pipe(take(1)).subscribe(responseHandler((resp) => {
      //   if (!resp.hasErrors) {
      //     if (ctrl.currWorkflowStage.id === undefined) {
      //       ctrl.data.push(resp.object);
      //     }
      //     ctrl.clseDialog();
      //   }
      // }, () => {
      //   // DO not update user
      // }));
    }
  }

}
