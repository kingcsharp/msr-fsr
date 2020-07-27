import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import {
  FileModel
} from '../../services/api.client.generated';
import { TooltipModule } from 'ngx-bootstrap/tooltip';

@Component({
  selector: 'grid-file-viewer',
  templateUrl: './grid-file-viewer.component.html',
  styleUrls: ['./grid-file-viewer.component.scss']
})
export class GridFileViewerComponent implements OnInit {

  @Input() files: FileModel[];
  constructor() {

  }

  ngOnInit(): void {

  }

}
