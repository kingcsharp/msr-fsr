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
import { MultiselectWrapperComponent } from '../../../app/components/multiselect-wrapper/multiselect-wrapper.component';
import { PcalendarWrapperComponent } from '../../../app/components/pcalendar-wrapper/pcalendar-wrapper.component';
import { GridOptionsComponent } from '../../../app/components/grid-options/grid-options.component';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    PcalendarWrapperComponent,
    GridOptionsComponent
  ],
  exports: [
    ProgressAnimateDirective,
    AnimateNumberDirective,
    MultiselectWrapperComponent,
    PcalendarWrapperComponent,
    GridOptionsComponent
  ],
  imports: [
    CommonModule,
    CalendarModule,
    FormsModule,
    MultiSelectModule,
    DialogModule
  ]
})
export class UtilsModule {
}
