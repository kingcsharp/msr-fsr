import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  PartService, PartModel, SubPartModel, EnumMenuItem, EnumApprovalTables, AuditActionResultOfPartModel, CreatePartRequest, UpdatePartRequest, FileModel
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
  styleUrls: ['./parts.style.scss'],
  templateUrl: './parts.component.html'
})

export class PartsComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  approvalTables = EnumApprovalTables;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canCreate: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currPart: any;
  data: any;
  isKitStatus: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;
  allParts: any[] = [];
  uploadedFiles: FileModel[] = [];
  isActive: any[];
  uploadedFinished: boolean = false;
  showApproveButtons: boolean = true;

  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private partsService: PartService) {
  }

  ngOnInit(): void {
    this.currPart = this.getPart(undefined);
    this.gridStorageId = 'partsGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
    new ColumnsSaved({ id: 'partNumber', label: 'Part Number', visible: true }),
    new ColumnsSaved({ id: 'oemPartNumber', label: 'OEM Part Number', visible: true }),
    new ColumnsSaved({ id: 'isKit', label: 'Is Kit', visible: true }),
    new ColumnsSaved({ id: 'isActive', label: 'Is Active', visible: true }),
    new ColumnsSaved({ id: 'maximumCycles', label: 'Maximun Cycles', visible: true }),
    new ColumnsSaved({ id: 'files', label: 'Reference Files', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];
    this.isKitStatus = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.isActive = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];

    this.canCreate = this.hasPrivilege(this.privileges.CanCreate);
    this.canActivateStages = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditStages = this.hasPrivilege(this.privileges.CanEdit);
    this.getParts();
    this.data = [];
  }

  getParts() {
    this.globals.showLoader(true);
    this.partsService.partGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object.map((elem) => {
          elem.isActive = elem.isActive === null ? false : elem.isActive;
          return elem;
        });
      }));
  }

  getPartsDropdown() {
    const ctrl = this;
    this.emptyArr(ctrl.allParts);
    const allPartsObjects = this.getAllPartsAndUsedIn();
    Object.keys(allPartsObjects).forEach(function (key) {
      const item = allPartsObjects[key];
      const usedIn = item.usedIn.join(',');
      const usedInStr = usedIn.length > 0 ? ` Used In [${usedIn}]` : '';
      ctrl.allParts.push({ label: `${item.element.name} [${item.element.partNumber}]${usedInStr}`, value: item.element.id });
    });
  }

  uploadParts(ev) {
    ev.forEach((element: PartModel) => {
      const index = this.data.findIndex(x => x.id === element.id);
      if (index !== -1) {
        this.data.splice(index, 1);
        this.data.splice(index, 0, element);
      } else {
        this.data.push(element);
      }
    });
  }

  getAllPartsAndUsedIn() {
    let partsDictionary = {};
    this.data.forEach((element: PartModel) => {
      if (partsDictionary[element.id] === undefined) {
        partsDictionary[element.id] = { element: element, usedIn: [] };
        if (element.isKit) {
          element.createSubParts.forEach((subpart: SubPartModel) => {
            if (partsDictionary[subpart.parentId] === undefined) {
              partsDictionary[subpart.parentId] = { element: undefined, usedIn: [element.partNumber] };
            } else {
              partsDictionary[subpart.parentId].usedIn.push(element.partNumber);
            }
          });
        }
      }
    });
    return partsDictionary;
  }

  getPartIdFromSubparts(subparts: SubPartModel[], partId: number) {
    if (subparts === undefined) {
      return -1;
    }
    const index = subparts.findIndex(x => x.parentId === partId);
    return subparts[index].partId;
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(this.menuItems.Parts, privName);
  }

  showDialog(part: PartModel) {
    this.getPartsDropdown();
    this.uploadedFiles = [];
    this.currPart = this.getPart(part);
    this.display = true;
  }

  resetParsleyjs() {
    if (jQuery('.parsleyjs').parsley() !== undefined) {
      jQuery('.parsleyjs').parsley().reset();
    }
  }

  clseDialog() {
    this.resetParsleyjs();
    this.display = false;
  }

  emptyArr(arr) {
    while (arr.length > 0) {
      arr.pop();
    }
  }

  getPart(part: PartModel) {
    if (part === undefined) {
      let ret = new PartModel();
      ret.isKit = false;
      ret.name = '';
      ret.createSubParts = [];
      ret.isActive = true;
      ret.files = [];
      return ret;
    } else {
      let copyPart: PartModel = new PartModel();
      Object.assign(copyPart, part);
      if (copyPart.createSubParts === null || copyPart.createSubParts === undefined) {
        copyPart.createSubParts = [];
      }
      return copyPart;
    }
  }

  addSubPart(part: PartModel) {
    this.currPart.createSubParts.push(this.getSubPart());
    part.isKit = part.createSubParts.length > 0;
  }

  removeSubPart(subpart: SubPartModel) {
    let length = this.currPart.createSubParts.length;
    while (length--) {
      if (this.currPart.createSubParts[length] === subpart) {
        this.currPart.createSubParts.splice(length, 1);
        this.currPart.isKit = this.currPart.createSubParts.length > 0;
        return;
      }
    }
  }

  getSubPart() {
    let ret = new SubPartModel();
    ret.qty = 0;
    return ret;
  }

  removeRow(part) {
    const ctrl = this;
    this.globals.showLoader(true);
    this.partsService.partDelete(part.id, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler((resp) => {
        const index = this.data.findIndex(x => x.id === part.id);
        this.data.splice(index, 1);
      }, () => {
        // DO not update user
      }));
  }

  onpartSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let method: Observable<AuditActionResultOfPartModel> = null;
      this.currPart.files.push(...this.uploadedFiles);

      this.globals.showApprovalCommentModal(this.currPart, EnumApprovalTables.PartApproval).then(() => {
        if (this.currPart.id === undefined) {
          let createPartRequest = this.getCreatePartRequest(this.currPart);
          createPartRequest.comment = this.currPart.comment;
          method = this.partsService.partPost(env.apiVersion, createPartRequest);
        } else {
          let updatePartRequest = this.getUpdatePartRequest(this.currPart);
          updatePartRequest.comment = this.currPart.comment;
          method = this.partsService.partPatch(env.apiVersion, updatePartRequest);
        }

        this.globals.showLoader(true);
        method.pipe(take(1)).subscribe(responseHandler((resp) => {
          if (!resp.hasErrors) {
            if (ctrl.currPart.id === undefined) {
              ctrl.data.push(resp.object);
            } else {
              const index = this.data.findIndex(x => x.id === this.currPart.id);
              this.data.splice(index, 1);
              this.data.splice(index, 0, resp.object);
            }
            ctrl.clseDialog();
          }
        }));
      });
    }
  }

  removeFile(file) {
    const currIndex = this.currPart.files.findIndex(x => x.fileId === file.fileId);
    this.currPart.files.splice(currIndex, 1);
  }

  removeuploadFile(event) {
    const index = this.uploadedFiles.findIndex(x => x.name === event.file.name);
    this.uploadedFiles.splice(index, 1);
  }

  myUploader(event) {
    const ctrl = this;
    for (let file of event.files) {
      if (ctrl.uploadedFiles.findIndex(x => x.name === file.name) === -1) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(file);
        fileReader.onload = function () {
          let fileModel = new FileModel();
          fileModel.name = file.name;
          fileModel.base64String = fileReader.result.toString();
          fileModel.contentType = file.type;
          ctrl.uploadedFiles.push(fileModel);
        };
      }
    }
  }

  getCreatePartRequest(currentPart: PartModel): CreatePartRequest {
    let ret = new CreatePartRequest({
      name: currentPart.name,
      partNumber: currentPart.partNumber,
      isActive: currentPart.isActive,
      createSubParts: currentPart.createSubParts,
      maximumCycles: currentPart.maximumCycles,
      nickName: currentPart.nickName,
      oemPartNumber: currentPart.oemPartNumber,
      files: currentPart.files
    });
    return ret;
  }

  getUpdatePartRequest(currentPart: PartModel): UpdatePartRequest {
    let partUpdate: UpdatePartRequest = new UpdatePartRequest();
    partUpdate.id = currentPart.id;
    Object.assign(partUpdate, this.getCreatePartRequest(currentPart));
    return partUpdate;
  }

}
