import { Component, OnInit, Input } from '@angular/core';
import * as ClassicEditor from '../../lib/ckeditor/ckeditor';

@Component({
  selector: 'ckeditor-wrapper',
  templateUrl: './ckeditor-wrapper.component.html',
  styleUrls: ['./ckeditor-wrapper.component.scss']
})
export class CkeditorWrapperComponent implements OnInit {

  public Editor = ClassicEditor;
  public config: any;
  @Input() editorcontent:string;

  constructor() { }

  ngOnInit(): void {
    this.config = {
      toolbar: {
        items: [
          'bold',
          'italic',
          'link',
          'bulletedList',
          'numberedList',
          'imageUpload',
          'blockQuote',
          'undo',
          'redo'
        ]
      },
      image: {
        toolbar: [
          'imageStyle:full',
          'imageStyle:side',
          '|',
          'imageTextAlternative'
        ]
      },
      simpleUpload: {
        // The URL that the images are uploaded to.
        uploadUrl: 'http://example.com',

        // Enable the XMLHttpRequest.withCredentials property.
        withCredentials: false,

        // Headers sent along with the XMLHttpRequest to the upload server.
        //headers: {
        //  'X-CSRF-TOKEN': 'CSFR-Token',
        //  Authorization: 'Bearer <JSON Web Token>'
        //}
      },
      // This value must be kept in sync with the language defined in webpack.config.js.
      language: 'en'
    };
  }

}
