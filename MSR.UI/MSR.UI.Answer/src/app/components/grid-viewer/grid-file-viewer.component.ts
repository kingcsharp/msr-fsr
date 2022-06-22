import {
  Component,
  OnInit,
  Output,
  Input,
  EventEmitter,
  ElementRef,
} from "@angular/core";
import { take } from "rxjs/operators";
import { responseHandler } from "../../utils/responseHandler";

import {
  FileModel,
  EnumMenuItem,
  FileService,
} from "../../services/api.client.generated";
import { environment as env } from "../../../environments/environment";
import { Globals } from "../../../app/models/lib/globals";
@Component({
  selector: "grid-file-viewer",
  templateUrl: "./grid-file-viewer.component.html",
  styleUrls: ["./grid-file-viewer.component.scss"],
})
export class GridFileViewerComponent implements OnInit {
  selectedDocUrl: string;
  display: boolean = false;
  viewer: string;
  selectedFile: FileModel;
  images: any;

  @Input() files: FileModel[];
  @Input() menuItem: EnumMenuItem;
  @Input() showDeleteButton: Boolean;
  @Input() showDescription: Boolean = false;

  constructor(private fileService: FileService, private globals: Globals) {}

  ngOnInit(): void {}

  showViewer(file: FileModel) {
    this.selectedFile = file;
    this.viewer = this.getViewerType(file.contentType);
    this.fileService
      .fileGet(
        this.globals.getSingularMenuName(this.menuItem),
        file.entityId,
        file.fileId,
        env.apiVersion
      )
      .pipe(take(1))
      .subscribe(
        responseHandler((resp) => {
          if (resp.object.length === 0) {
            // err
          } else {
            if (this.viewer === "img") {
              this.images = [];
              this.images.push({
                id: resp.object[0].fileId,
                previewImageSrc: resp.object[0].fileURL,
                thumbnailImageSrc: resp.object[0].fileURL,
                alt: "",
                title: "",
                description: resp.object[0].description,
              });
            }
            this.selectedDocUrl = resp.object[0].fileURL;
            this.display = true;
          }
        })
      );
  }

  closeDialog() {
    this.display = false;
  }

  getViewerType(contentType) {
    switch (contentType) {
      case "application/msword":
      case "application/vnd.openxmlformats-officedocument.wordprocessingml.document":
      case "application/vnd.ms-excel":
      case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
      case "application/vnd.openxmlformats-officedocument.presentationml.presentation":
        return "office";
      case "text/plain":
      case "text/html":
      case "text/csv":
        return "google";
      case "application/pdf":
        return "pdf";
      case "image/gif":
      case "image/tiff":
      case "image/webp":
      case "image/jpeg":
      case "image/png":
        return "img";
      case "text/plain":
      default:
        return "url";
    }
  }

  onClickDelete() {
    this.globals.showLoader(true);
    this.fileService
      .fileDelete(
        this.globals.getSingularMenuName(this.menuItem),
        this.selectedFile.entityId,
        this.selectedFile.fileId,
        env.apiVersion
      )
      .pipe(take(1))
      .subscribe(
        responseHandler((resp) => {
          const index = this.files.findIndex(
            (x) => x.fileId === this.selectedFile.fileId
          );
          this.files.splice(index, 1);
          this.display = false;
        })
      );
  }
}
