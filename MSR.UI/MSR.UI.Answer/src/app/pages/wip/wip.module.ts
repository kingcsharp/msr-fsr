import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MultiSelectModule } from 'primeng/multiselect';
import { UtilsModule } from '../../layout/utils/utils.module';
import { WipComponent } from './wip/wip.component';
import { WiphistoryComponent } from './wiphistory/wiphistory.component';
import { WipstatusComponent } from './wipstatus/wipstatus.component';
import { WipstatusWrapperComponent } from '../../components/wipstatus-wrapper/wipstatus.component'
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { CalendarModule } from 'primeng/calendar';
import { WidgetModule } from '../../layout/widget/widget.module';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'wip', component: WipComponent, pathMatch: 'full' },
  { path: 'wiphistory', component: WiphistoryComponent, pathMatch: 'full' },
  { path: 'wipstatus', component: WipstatusComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [WipComponent, WiphistoryComponent, WipstatusComponent, WipstatusWrapperComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MultiSelectModule,
    TableModule,
    CalendarModule,
    UtilsModule,
    WidgetModule,
    NewWidgetModule,
  ]
})
export class WipModule { static routes = routes; }
