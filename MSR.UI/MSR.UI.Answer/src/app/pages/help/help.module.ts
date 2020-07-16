import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { DialogModule } from 'primeng/dialog';
import { HelpComponent } from './help/help.component';
import { HelpCreateComponent } from './help-create/help-create.component';
import {AutoCompleteModule} from 'primeng/autocomplete';
import { FormsModule } from '@angular/forms';
import {MultiSelectModule} from 'primeng/multiselect';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'help', component: HelpComponent, pathMatch: 'full' },
  { path: 'help-create', component: HelpCreateComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [HelpComponent, HelpCreateComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    DialogModule,
    AutoCompleteModule,
    FormsModule,
    MultiSelectModule
  ]
})
export class HelpModule {
  static routes = routes;
 }
