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
import { DropdownModule } from 'primeng/dropdown';
import { MultiSelectModule } from 'primeng/multiselect';
import { ReportComponent } from './report.component';
import { AdhocComponent } from './adhocreports/adhocreport.component';
import { PopoverModule } from 'ngx-bootstrap/popover';
import { ReportCubeService } from './reportcube.service';

export const routes = [
  { path: '', redirectTo: 'report/adhocreports', pathMatch: 'full' },
  { path: 'report/adhocreports', component: ReportComponent, pathMatch: 'full' },
  { path: 'report/adhocreports/:id', component: AdhocComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    ReportComponent,
    AdhocComponent
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
    UtilsModule,
    LiveTileModule,
    WidgetModule,
    MapaelLayersMapModule,
    NewWidgetModule,
    PopoverModule.forRoot()
  ],
  providers: [ReportCubeService]
})
export class ReportModule {
  static routes = routes;
}
