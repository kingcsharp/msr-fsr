import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TrendModule } from 'ngx-trend';
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
import { InputNumberModule } from 'primeng/inputnumber';
import { MultiSelectModule } from 'primeng/multiselect';
import { ConfirmDialogModule } from 'primeng/confirmdialog';
import { SelectButtonModule } from 'primeng/selectbutton';
import { AdminCostSettingsComponent } from './admin-cost-settings/admin-cost-settings.component';
import { EquipmentMaintenanceComponent } from './equipment-maintenance/equipment-maintenance.component';

export const routes = [
  { path: '', redirectTo: 'equipmentmaintenance', pathMatch: 'full' },
  { path: 'equipmentmaintenance', component: EquipmentMaintenanceComponent, pathMatch: 'full' },
  { path: 'admincostsettings', component: AdminCostSettingsComponent, pathMatch: 'full' },
];

@NgModule({
  declarations: [
    AdminCostSettingsComponent,
    EquipmentMaintenanceComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    TrendModule,
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    FormsModule,
    InputSwitchModule,
    TextMaskModule,
    DialogModule,
    TableModule,
    CalendarModule,
    InputNumberModule,
    UtilsModule,
    LiveTileModule,
    WidgetModule,
    MapaelLayersMapModule,
    NewWidgetModule,
    ConfirmDialogModule,
    SelectButtonModule
  ],
  providers: []
})
export class UtilitiesModule {
  static routes = routes;
}
