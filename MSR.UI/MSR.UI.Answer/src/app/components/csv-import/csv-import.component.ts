import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';

import {
  FileModel, EnumMenuItem, FileService
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';

@Component({
  selector: 'csv-import',
  templateUrl: './csv-import.component.html',
  styleUrls: ['./csv-import.component.scss']
})
export class CsvImportComponent implements OnInit {
  uploadedFiles: any[] = [];
  display: boolean = false;

  @Input() menuItem: EnumMenuItem;
  @Output('onUpload') change = new EventEmitter<Array<any>>();
  @Input() showButton: boolean;
  @Input() title: string;
  constructor() {

  }

  ngOnInit(): void {

  }

  submitImport() {
    // this.uploadedFiles. pipe(take(1)).subscribe(responseHandler((resp) => {

    // }));

    this.change.emit(this.uploadedFiles);
  }

  showDialog() {
    this.uploadedFiles = [];
    this.display = true;
  }

  clseDialog() {
    this.display = false;
  }

  removeuploadFile(event) {
    var index = this.uploadedFiles.findIndex(x => x.name === event.file.name);
    this.uploadedFiles.splice(index, 1);
  }

  myUploader(event) {
    const ctrl = this;
    for (let file of event.files) {
      if (ctrl.uploadedFiles.findIndex(x => x.name === file.name) === -1) {
        let fileReader = new FileReader();
        fileReader.readAsDataURL(file);
        fileReader.onload = function () {
          var fileModel = new FileModel();
          fileModel.name = file.name;
          fileModel.base64String = fileReader.result.toString();
          fileModel.contentType = file.type;
          ctrl.uploadedFiles.push(fileModel);
        };
      }
    }
  }

}
