import { Component, OnInit, Input, EventEmitter, Output } from '@angular/core';
import * as ClassicEditor from '../../lib/ckeditor/ckeditor';
import { ChangeEvent } from '@ckeditor/ckeditor5-angular/ckeditor.component';
import { environment as env } from '../../../environments/environment';

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

      options.push(plugin.pluginName);

    });
    this.config = {
      fontColor: {
        colors: [
          {
            color: 'hsl(0, 0%, 0%)',
            label: 'Black',
            default: true
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
          'insertTable', 'BlockQuote', 'Bold', 'CKFinder', 'FontBackgroundColor', 'FontColor', 'FontFamily', 'FontSize', 'Heading', 'Highlight', 'HorizontalLine',
           'ImageUpload', 'Indent', 'Italic', 'Link', 'PageBreak', 'RemoveFormat', 'Strikethrough', 'Subscript', 'Superscript'
        ]
      },
      table: {
        contentToolbar: [ 'tableColumn', 'tableRow', 'mergeTableCells' ]
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

        uploadUrl: env.API_BASE_URL + '/v1/file/help',

        withCredentials: true,

         headers: {
          'X-CSRF-TOKEN': 'CSFR-Token',
          Authorization: 'Bearer ' + localStorage.getItem('token')
         }
      },

      language: 'en'
    };
  }

  editorContentChanged() {
    this.editorContentChange.emit(this.editorcontent);
  }

}
