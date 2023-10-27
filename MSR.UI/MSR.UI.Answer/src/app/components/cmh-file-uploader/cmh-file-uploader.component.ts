import { Component, OnInit, Input, Output, EventEmitter } from "@angular/core";
import { Globals } from "../../models/lib/globals";
import {
  FileService,
  FileModel,
  EnumMenuItem,
} from "../../services/api.client.generated";
import { take } from "rxjs/operators";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { emptyArray } from "../../models/lib/Utils";
import { CommonGrid } from "../../models/lib/CommonGrid";
declare let jQuery: any;

@Component({
  selector: "cmh-file-uploader",
  templateUrl: "./cmh-file-uploader.component.html",
  styleUrls: ["./cmh-file-uploader.component.scss"],
})
export class CmhFileUploaderComponent implements OnInit {
  @Input() files: FileModel[];
  @Input() menuItem: EnumMenuItem; // Need to pick which view you are trying to get the files from
  @Output() filesChange: EventEmitter<Array<FileModel>> = new EventEmitter<
    Array<FileModel>
  >();
  @Output() onChangeFileStatus: EventEmitter<boolean> =
    new EventEmitter<boolean>();
  @Input() showUploadButton: boolean;
  @Input() showCancelButton: boolean;
  @Input() showSelectButton: boolean;
  @Input() multiple: string;
  @Input() maxFileSize: number;
  @Input() accept: string;
  @Input() chooseLabel: string;
  @Input() selectLabel: string;
  @Input() useLoader: boolean = true;
  @Input() showDescription: boolean = false;
  @Input() editDescription: boolean = false;
  uploadedFiles: FileModel[] = [];
  showLi: boolean = false;
  showSelectModal: boolean = false;
  fileTypes: any[] = [];
  selectAll: boolean = false;
  selectedFiles: FileModel[] = [];
  fileDescriptions: boolean[] = [];

  constructor(
    private fileService: FileService,
    private globals: Globals,
    public cg: CommonGrid
  ) {}

  ngOnInit(): void {
    if (this.chooseLabel === "" || this.chooseLabel === undefined) {
      this.chooseLabel = "Browse";
    }

    if (this.files.length > 0) {
      if (!this.useLoader) {
        this.globals.showLoader(false);
      } else  {
        this.globals.showLoader(true);
      }

      this.fileService
        .fileGet(
          this.globals.getSingularMenuName(this.menuItem),
          this.files[0].entityId,
          null,
          env.apiVersion
        )
        .pipe(take(1))
        .subscribe(
          responseHandler((resp) => {
            if (resp.object.length > 0) {
              this.files.forEach((currFile: FileModel) => {
                resp.object.forEach((getFile: FileModel) => {
                  if (currFile.fileId === getFile.fileId) {
                    currFile.fileURL = getFile.fileURL;
                  }
                });
              });
            }
            this.fileDescriptions = this.files.map((file) => true);
            this.showLi = true;
          })
        );
    } else {
      this.showLi = true;
    }

    if (this.showSelectButton) {
      if (this.useLoader) {
        this.globals.showLoader(true);
      } else {
        this.globals.showLoader(false);
      }

      this.fileService
        .fileGet(null, null, null, env.apiVersion)
        .pipe(take(1))
        .subscribe(
          responseHandler((resp) => {
            this.uploadedFiles = resp.object;
            this.fileTypes = this.uploadedFiles
              .filter(
                (thing, i, arr) =>
                  arr.findIndex((t) => t.contentType === thing.contentType) ===
                  i
              )
              .map((x) => ({ label: x.contentType, value: x.contentType }));
          })
        );
    }
  }

  removeFile(file) {
    const currIndex = this.files.findIndex((x) => x.fileId === file.fileId);
    this.files.splice(currIndex, 1);
    this.fileDescriptions.splice(currIndex, 1);
    this.isReadyToSubmit();
  }

  removeuploadFile(event) {
    const index = this.files.findIndex((x) => x.name === event.file.name);
    this.files.splice(index, 1);
    this.fileDescriptions.splice(index, 1);
    this.isReadyToSubmit();
  }

  openSelectModal() {
    this.selectedFiles = [];
    this.showSelectModal = true;
  }

  closeSelectModal() {
    this.showSelectModal = false;
  }

  myUploader(event) {
    const ctrl = this;
    if (ctrl.multiple !== "multiple") {
      emptyArray(ctrl.files);
    }
    for (let file of event.files) {
      if (ctrl.files.findIndex((x) => x.name === file.name) === -1) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(file);
        fileReader.onload = function () {
          let fileModel = new FileModel();
          fileModel.name = file.name;
          fileModel.base64String = fileReader.result.toString();
          fileModel.contentType = file.type;
          fileModel.description = null;
          ctrl.files.unshift(fileModel);
          ctrl.fileDescriptions.unshift(false);
          ctrl.isReadyToSubmit();
        };
      }
    }
  }

  selectUploadedFiles() {
    for (let file of this.selectedFiles) {
      if (this.files.findIndex((x) => x.fileId === file.fileId) === -1) {
        this.files.unshift(file);
        this.fileDescriptions.unshift(true);
        this.isReadyToSubmit();
      }
    }
    this.closeSelectModal();
  }

  saveFileDescription(index) {
    jQuery(`.file-list-${index}`).parsley().validate();

    if (jQuery(`.file-list-${index}`).parsley().isValid()) {
      this.fileDescriptions[index] = true;
      this.isReadyToSubmit();
    }
  }

  selectAllFiles($event) {
    this.selectedFiles = this.selectAll ? this.uploadedFiles : [];
  }

  isReadyToSubmit() {
    const valid = !this.files.some(
      (file, index) =>
        !file.fileId && (!file.description || !this.fileDescriptions[index])
    );
    this.onChangeFileStatus.emit(valid);
  }

  onChangeFileDescription(index) {
    this.fileDescriptions[index] = false;
    this.onChangeFileStatus.emit(false);
  }
}
