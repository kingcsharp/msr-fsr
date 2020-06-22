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
