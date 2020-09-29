import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { UtilsModule } from '../../layout/utils/utils.module';
import { LiveTileModule } from '../../components/tile/tile.module';
import { MapaelLayersMapModule } from '../../components/mapael/mapael.module';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { FormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';

import { PopoverModule } from 'ngx-bootstrap/popover';
import { OperationsComponent } from './operational/operations.component';
import { FinancialComponent } from './financial/financial.component';

export const routes = [
  { path: '', redirectTo: 'report/operationsdashboard', pathMatch: 'full' },
  { path: 'report/operationsdashboard', component: FinancialComponent, pathMatch: 'full' },
  { path: 'report/financialdashboard', component: OperationsComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    FinancialComponent, OperationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    FormsModule,
    InputSwitchModule,
    TextMaskModule,
    DialogModule,
    TableModule,
    CalendarModule,
    UtilsModule,
    LiveTileModule,
    WidgetModule,
    MapaelLayersMapModule,
    NewWidgetModule,
    PopoverModule.forRoot()
  ]
})
export class DashboardModule {
  static routes = routes;
} 
