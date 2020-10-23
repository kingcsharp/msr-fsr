import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';

import {
  FileModel, EnumMenuItem, FileService
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { Globals } from '../../../app/models/lib/globals';
@Component({
  selector: 'grid-file-viewer',
  templateUrl: './grid-file-viewer.component.html',
  styleUrls: ['./grid-file-viewer.component.scss']
})
export class GridFileViewerComponent implements OnInit {

  selectedDocUrl: string;
  display: boolean = false;
  viewer: string;
  selectedFile: FileModel;

  @Input() files: FileModel[];
  @Input() menuItem: EnumMenuItem;
  constructor(private fileService: FileService, private globals: Globals) {

  }

  ngOnInit(): void {


  }

  showViewer(file: FileModel) {
    this.selectedFile = file;
    this.viewer = this.getViewerType(file.contentType);
    this.fileService.fileGet(this.globals.getSingularMenuName(this.menuItem), file.entityId, file.fileId, env.apiVersion)
      .pipe(take(1)).subscribe(responseHandler((resp) => {
        if (resp.object.length === 0) {
          // err
        } else {
          this.selectedDocUrl = resp.object[0].fileURL;
          this.display = true;
        }
      }));
  }

  clseDialog() {
    this.display = false;
  }

  getViewerType(contentType) {
    switch (contentType) {
      case 'application/msword':
      case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document':
      case 'application/vnd.ms-excel':
      case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
      case 'application/vnd.openxmlformats-officedocument.presentationml.presentation':
        return 'office';
      case 'text/plain':
      case 'text/html':
      case 'text/csv':
        return 'google';
      case 'application/pdf':
        return 'pdf';
      case 'text/plain':
      case 'image/gif':
      case 'image/tiff':
      case 'image/webp':
      case 'image/jpeg':
      case 'image/png':
      default:
        return 'url';
    }
  }
}
