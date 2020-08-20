import { Component, OnInit, ElementRef, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  PartService, FileService, FileModel, EntityModel, EnumMenuItem
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { INFERRED_TYPE } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'cmh-file-uploader',
  templateUrl: './cmh-file-uploader.component.html',
  styleUrls: ['./cmh-file-uploader.component.scss']
})
export class CmhFileUploaderComponent implements OnInit {
  uploadedFiles: any = [];
  showLi: boolean = false;
  constructor(private fileService: FileService, private globals: Globals) {

  }

  @Input() files: FileModel[];
  @Input() menuItem: EnumMenuItem; // Need to pick which view you are trying to get the files from
  @Output() filesChange: EventEmitter<Array<FileModel>> = new EventEmitter<Array<FileModel>>();
  @Input() showUploadButton: boolean;
  @Input() showCancelButton: boolean;
  @Input() multiple: string;
  @Input() maxFileSize: number;
  @Input() accept: string;
  @Input() chooseLabel: string;
  ngOnInit(): void {
    if (this.chooseLabel === '' || this.chooseLabel === undefined) {
      this.chooseLabel = 'Select Files';
    }

    if (this.files.length > 0) {
      this.globals.showLoader(true);
      this.fileService.fileGet(this.globals.getSingularMenuName(this.menuItem), this.files[0].entityId, null, env.apiVersion)
        .pipe(take(1)).subscribe(responseHandler((resp) => {
          if (resp.object.length > 0) {
            this.files.forEach((currFile: FileModel) => {
              resp.object.forEach((getFile: FileModel) => {
                if (currFile.fileId === getFile.fileId) {
                  currFile.fileURL = getFile.fileURL;
                }
              });
            });
          }
          this.showLi = true;
        }));
    } else {
      this.showLi = true;
    }

  }

  removeFile(file) {
    const currIndex = this.files.findIndex(x => x.fileId === file.fileId);
    this.files.splice(currIndex, 1);
  }

  removeuploadFile(event) {
    const index = this.files.findIndex(x => x.name === event.file.name);
    this.files.splice(index, 1);
  }

  myUploader(event) {
    const ctrl = this;
    for (let file of event.files) {
      if (ctrl.files.findIndex(x => x.name === file.name) === -1) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(file);
        fileReader.onload = function () {
          let fileModel = new FileModel();
          fileModel.name = file.name;
          fileModel.base64String = fileReader.result.toString();
          fileModel.contentType = file.type;
          ctrl.files.unshift(fileModel);
        };
      }
    }
  }
}
