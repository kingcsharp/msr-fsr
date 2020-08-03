import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { HelpComponent } from './help/help.component';
import { HelpCreateComponent } from './help-create/help-create.component';
import { AutoCompleteModule } from 'primeng/autocomplete';
import { FormsModule } from '@angular/forms';
import { MultiSelectModule } from 'primeng/multiselect';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { DropdownModule } from 'primeng/dropdown';
import { GridOptionsComponent } from '../../components/grid-options/grid-options.component';
import { MultiselectWrapperComponent } from '../../components/multiselect-wrapper/multiselect-wrapper.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CkeditorWrapperComponent } from '../../components/ckeditor-wrapper/ckeditor-wrapper.component';
import { HelpbuttonWrapperComponent } from '../../components/helpbutton-wrapper/helpbutton-wrapper.component';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'help', component: HelpComponent, pathMatch: 'full' },
  { path: 'help-create', component: HelpCreateComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [HelpComponent, HelpCreateComponent, GridOptionsComponent,
    MultiselectWrapperComponent, CkeditorWrapperComponent, HelpbuttonWrapperComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    DialogModule,
    AutoCompleteModule,
    FormsModule,
    MultiSelectModule,
    TableModule,
    NewWidgetModule,
    DropdownModule,
    CKEditorModule
  ]
})
export class HelpModule {
  static routes = routes;
}
