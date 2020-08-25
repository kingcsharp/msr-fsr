import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { MonitorsComponent } from './monitors/monitors.component';
import { UtilsModule } from '../../layout/utils/utils.module';
import { HelpbuttonWrapperComponent } from '../../components/helpbutton-wrapper/helpbutton-wrapper.component';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'monitors', component: MonitorsComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [MonitorsComponent, HelpbuttonWrapperComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    UtilsModule,
    DialogModule,
    TableModule,
    NewWidgetModule
  ]
})
export class MonitorsModule { static routes = routes; }
