import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';

import {
  EnumMenuItem, PartService, FileModel
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
  @Input() fileName:string; //file needs to be placed in assets/CsvFiles/yourfilename.csv
  constructor(private partService: PartService) {

  }

  ngOnInit(): void {

  }

  submitImport() {
    switch (this.menuItem) {
      case EnumMenuItem.Parts:
        this.partService.import(env.apiVersion, this.uploadedFiles[0].base64String).pipe(take(1))
          .subscribe(responseHandler((resp) => {
            this.change.emit(this.uploadedFiles);
          }));
        break;
    }
  }

  showDialog() {
    this.uploadedFiles = [];
    this.display = true;
  }

  clseDialog() {
    this.uploadedFiles = undefined;
    this.display = false;
  }

}
