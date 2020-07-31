import 'jquery-flot/jquery.flot.js';
import 'jquery.flot.animator/jquery.flot.animator';
import 'jquery-flot/jquery.flot.pie.js';
import 'jquery-flot/jquery.flot.selection.js';
import 'jquery-flot/jquery.flot.resize.js';
import 'flot.dashes/jquery.flot.dashes';
import 'jquery.animate-number/jquery.animateNumber.js';
import 'jQuery-Mapael/js/jquery.mapael.js';
import 'jQuery-Mapael/js/maps/usa_states';
import 'jQuery-Mapael/js/maps/world_countries.js';

import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';

import { ProgressAnimateDirective } from './directives/progress-animate.directive';
import { AnimateNumberDirective } from './directives/animate-number.directive';
import { MultiSelectModule } from 'primeng/multiselect';
import { CalendarModule } from 'primeng/calendar';
import { DialogModule } from 'primeng/dialog';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { MultiselectWrapperComponent } from '../../../app/components/multiselect-wrapper/multiselect-wrapper.component';
import { MultiselectWrapperFormComponent } from '../../../app/components/multiselect-wrapper-form/multiselect-wrapper-form.component';

import { PcalendarWrapperComponent } from '../../../app/components/pcalendar-wrapper/pcalendar-wrapper.component';
import { GridOptionsComponent } from '../../../app/components/grid-options/grid-options.component';
import { GridFileViewerComponent } from '../../components/grid-viewer/grid-file-viewer.component';
import { ApproveEntityComponent } from '../../../app/components/aproove-entity/approve-entity.component';
import { NgxDocViewerModule } from 'ngx-doc-viewer';
import { FormsModule } from '@angular/forms';
import { FileUploadModule } from 'primeng/fileupload';
import { CsvImportComponent } from '../../../app/components/csv-import/csv-import.component';

@NgModule({
  declarations: [
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    MultiselectWrapperFormComponent,
    PcalendarWrapperComponent,
    GridOptionsComponent,
    GridFileViewerComponent,
    ApproveEntityComponent,
    CsvImportComponent
  ],
  exports: [
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    MultiselectWrapperFormComponent,
    PcalendarWrapperComponent,
    GridOptionsComponent,
    FileUploadModule,
    GridFileViewerComponent,
    ApproveEntityComponent,
    CsvImportComponent
  ],
  imports: [
    CommonModule,
    CalendarModule,
    FormsModule,
    MultiSelectModule,
    DialogModule,
    FileUploadModule,
    TooltipModule,
    NgxDocViewerModule
  ]
})
export class UtilsModule {
}
