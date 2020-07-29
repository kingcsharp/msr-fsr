import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import {
  FileModel
} from '../../services/api.client.generated';

@Component({
  selector: 'grid-file-viewer',
  templateUrl: './grid-file-viewer.component.html',
  styleUrls: ['./grid-file-viewer.component.scss']
})
export class GridFileViewerComponent implements OnInit {

  selectedDocUrl: string;
  display: boolean = false;
  viewer: string;
  @Input() files: FileModel[];
  constructor() {

  }

  ngOnInit(): void {


  }

  showViewer(file) {
    this.viewer = this.getViewerType(file.contentType);

  }

  getViewerType(contentType) {
    switch (contentType) {
      case 'application/msword':
      case 'application/vnd.openxmlformats-officedocument.wordprocessingml.document':
      case 'application/vnd.ms-excel':
      case 'application/vnd.openxmlformats-officedocument.spreadsheetml.sheet':
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
