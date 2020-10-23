import 'jquery-flot/jquery.flot.js';
import 'jquery.flot.animator/jquery.flot.animator';
import 'jquery-flot/jquery.flot.pie.js';
import 'jquery-flot/jquery.flot.selection.js';
import 'jquery-flot/jquery.flot.resize.js';
import 'flot.dashes/jquery.flot.dashes';
import 'jquery.animate-number/jquery.animateNumber.js';

import { CommonModule, DatePipe } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProgressAnimateDirective } from './directives/progress-animate.directive';
import { AnimateNumberDirective } from './directives/animate-number.directive';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { CheckboxModule } from 'primeng/checkbox';
import { MultiselectWrapperComponent } from '../../../app/components/multiselect-wrapper/multiselect-wrapper.component';
import { MultiselectWrapperFormComponent } from '../../../app/components/multiselect-wrapper-form/multiselect-wrapper-form.component';
import { PcalendarWrapperComponent } from '../../../app/components/pcalendar-wrapper/pcalendar-wrapper.component';
import { GridOptionsComponent } from '../../../app/components/grid-options/grid-options.component';
import { GridFileViewerComponent } from '../../components/grid-viewer/grid-file-viewer.component';
import { NgxDocViewerModule } from 'ngx-doc-viewer';
import { FormsModule } from '@angular/forms';
import { FileUploadModule } from 'primeng/fileupload';
import { CsvImportComponent } from '../../../app/components/csv-import/csv-import.component';
import { CmhFileUploaderComponent } from '../../../app/components/cmh-file-uploader/cmh-file-uploader.component';
import { GridInputFilterComponent } from '../../../app/components/grid-input-filter/grid-input-filter.component';
import { HelpbuttonWrapperComponent } from '../../components/helpbutton-wrapper/helpbutton-wrapper.component';
import { TimeZonePipe } from '../../../app/pipes/timezone.pipe';
import { BootstrapSwitchComponent } from '../../../app/components/bootstrap-switch/bootstrap-switch.component';
import { CkeditorWrapperComponent } from '../../components/ckeditor-wrapper/ckeditor-wrapper.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { TableModule } from 'primeng/table';
import { GridComponent } from '../../../app/components/grid/grid.component';
import { HighchartsChartModule } from 'highcharts-angular';

@NgModule({
  declarations: [
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    MultiselectWrapperFormComponent,
    PcalendarWrapperComponent,
    GridFileViewerComponent,
    CsvImportComponent,
    CmhFileUploaderComponent,
    GridInputFilterComponent,
    HelpbuttonWrapperComponent,
    GridOptionsComponent,
    TimeZonePipe,
    BootstrapSwitchComponent,
    CkeditorWrapperComponent,
    GridComponent
  ],
  exports: [
    GridComponent,
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    MultiselectWrapperFormComponent,
    PcalendarWrapperComponent,
    GridFileViewerComponent,
    CsvImportComponent,
    CmhFileUploaderComponent,
    GridInputFilterComponent,
    FileUploadModule,
    TooltipModule,
    CheckboxModule,
    FormsModule,
    MultiSelectModule,
    DialogModule,
    CalendarModule,
    GridOptionsComponent,
    HelpbuttonWrapperComponent,
    TimeZonePipe,
    BootstrapSwitchComponent,
    CkeditorWrapperComponent
  ],
  imports: [
    TableModule,
    CommonModule,
    CalendarModule,
    FormsModule,
    MultiSelectModule,
    DialogModule,
    FileUploadModule,
    CheckboxModule,
    TooltipModule.forRoot(),
    NgxDocViewerModule,
    CKEditorModule,
    TableModule,
    HighchartsChartModule,
  ],
  providers: [DatePipe]
})
export class UtilsModule {
}
