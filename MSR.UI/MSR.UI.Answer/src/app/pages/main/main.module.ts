
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { UtilsModule } from '../../layout/utils/utils.module';
import { LiveTileModule } from '../../components/tile/tile.module';
import { MapaelLayersMapModule } from '../../components/mapael/mapael.module';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { UserComponent } from './user/user.component';
import { FormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { RoleassignmentsComponent } from './roleassignments/roleassignments.component';
import { ListboxModule } from 'primeng/listbox';
import { CustomersComponent } from './customers/customers.component';
import { CustomerCreateComponent } from './customer-create/customer-create.component';
import { CertificationsComponent } from './certifications/certifications.component';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'people', component: UserComponent, pathMatch: 'full' },
  { path: 'roleassignments', component: RoleassignmentsComponent, pathMatch: 'full' },
  { path: 'customers', component: CustomersComponent, pathMatch: 'full' },
  { path: 'customer-create', component: CustomerCreateComponent, pathMatch: 'full' },
  { path: 'training', component: CertificationsComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    UserComponent,
    RoleassignmentsComponent,
    CustomersComponent,
    CustomerCreateComponent,
    CertificationsComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    // TrendModule,
    CheckboxModule,
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    TooltipModule.forRoot(),
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
    ListboxModule
  ],
  providers: []
})
export class MainModule {
  static routes = routes;
}
