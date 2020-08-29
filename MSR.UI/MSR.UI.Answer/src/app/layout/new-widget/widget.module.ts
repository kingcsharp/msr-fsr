import { NgModule } from '@angular/core';

import { WidgetComponent } from './widget';
import { UtilsModule } from '../../layout/utils/utils.module';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { LoaderModule } from '../../components/loader/loader.module';

@NgModule({
  imports: [
    CommonModule,
    FormsModule,
    UtilsModule,
    LoaderModule
  ],
  exports: [WidgetComponent],
  declarations: [WidgetComponent]
})
export class NewWidgetModule {
}
