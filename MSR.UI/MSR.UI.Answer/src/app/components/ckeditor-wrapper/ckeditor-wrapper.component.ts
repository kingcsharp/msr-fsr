import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import * as ClassicEditor from '../../lib/ckeditor/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';

@Component({
  selector: 'ckeditor-wrapper',
  templateUrl: './ckeditor-wrapper.component.html',
  styleUrls: ['./ckeditor-wrapper.component.scss']
})
export class CkeditorWrapperComponent implements OnInit {

  public Editor = ClassicEditor;
  public config: any;
  @Input() editorcontent: string;
  @Output() editorContentChange = new EventEmitter();
  constructor() { }

  ngOnInit(): void {

    let options = new Array<string>();
    ClassicEditor.builtinPlugins.map(plugin => {
      console.log(plugin.pluginName);
      options.push(plugin.pluginName);

    });

    console.log(options);

    this.config = {
      fontColor: {
        colors: [
          {
            color: 'hsl(0, 0%, 0%)',
            label: 'Black',
            default:true
          },
          {
            color: 'hsl(0, 0%, 30%)',
            label: 'Dim grey'
          },
          {
            color: 'hsl(0, 0%, 60%)',
            label: 'Grey'
          },
          {
            color: 'hsl(0, 0%, 90%)',
            label: 'Light grey'
          },
          {
            color: 'hsl(0, 0%, 100%)',
            label: 'White',
            hasBorder: true
          }
        ]
      },
      toolbar: {
        items: [
          "BlockQuote", "Bold", "CKFinder", "FontBackgroundColor", "FontColor", "FontFamily", "FontSize", "Heading", "Highlight", "HorizontalLine", "ImageUpload", "Indent", "Italic", "Link", "PageBreak", "RemoveFormat", "Strikethrough", "Subscript", "Superscript"
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
        // headers: {
        //  'X-CSRF-TOKEN': 'CSFR-Token',
        //  Authorization: 'Bearer <JSON Web Token>'
        // }
      },
      // This value must be kept in sync with the language defined in webpack.config.js.
      language: 'en'
    };
  }

  editorContentChanged() {
    this.editorContentChange.emit(this.editorcontent);
  }

}
