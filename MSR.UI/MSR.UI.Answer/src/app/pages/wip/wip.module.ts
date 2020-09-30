import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MultiSelectModule } from 'primeng/multiselect';
import { UtilsModule } from '../../layout/utils/utils.module';
import { WipComponent } from './wip/wip.component';
import { WiphistoryComponent } from './wiphistory/wiphistory.component';
import { WipstatusComponent } from './wipstatus/wipstatus.component';
import { WipstatusWrapperComponent } from '../../components/wipstatus-wrapper/wipstatus.component';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { CalendarModule } from 'primeng/calendar';
import { WidgetModule } from '../../layout/widget/widget.module';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { AlertModule } from 'ngx-bootstrap/alert';
import { WipdetailsComponent } from './wipdetails/wipdetails.component';
import {TabViewModule} from 'primeng/tabview';
import { CarouselModule } from 'ngx-bootstrap/carousel';
import { WorkordertasktimerWrapperComponent } from '../../components/workordertasktimer-wrapper/workordertasktimer-wrapper.component';
import {DropdownModule} from 'primeng/dropdown';
import { FormsModule } from '@angular/forms';
import { WorkordertaskmonitosWrapperComponent } from '../../components/workordertaskmonitos-wrapper/workordertaskmonitos-wrapper.component';
import { EmPmButtonWrapperComponent } from '../../components/em-pm-button-wrapper/em-pm-button-wrapper.component';
import { AddNcrButtonWrapperComponent} from '../../components/add-ncr-button-wrapper/add-ncr-button-wrapper.component';
import { WipListButtonWrapperComponent } from '../../components/wip-list-button-wrapper/wip-list-button-wrapper.component';
import { SelectWorkOrderDropDownWrapperComponent } from '../../components/select-work-order-drop-down-wrapper/select-work-order-drop-down-wrapper.component';
import { PrinttravelerReportComponent } from '../../components/printtraveler-report/printtraveler-report.component';
import { QRCodeModule } from 'angularx-qrcode';
import { PrintotherReportComponent } from '../../components/printother-report/printother-report.component';
import { DeliveryTicketReportComponent } from '../../components/delivery-ticket-report/delivery-ticket-report.component';
import { NgxBarcodeModule } from 'ngx-barcode';
import { WipHistoryReportComponent}  from '../../components/wip-history-report/wip-history-report.component';
import { NcrReportComponent } from '../../components/ncr-report/ncr-report.component';
import { PartLabelRollComponent } from '../../components/part-label-roll/part-label-roll.component';
import { TechnicalDataLabelComponent } from '../../components/technical-data-label/technical-data-label.component';
import { WorkReportComponent } from '../../components/work-report/work-report.component';

export const routes = [
  { path: '', redirectTo: 'wipstatus', pathMatch: 'full' },
  { path: 'wip', component: WipComponent, pathMatch: 'full' },
  { path: 'wiphistory', component: WiphistoryComponent, pathMatch: 'full' },
  { path: 'wipstatus', component: WipstatusComponent, pathMatch: 'full' },
  { path: 'details/:id', component: WipdetailsComponent, pathMatch: 'full'}
];


@NgModule({
  declarations: [WipComponent, WiphistoryComponent, WipstatusComponent, WipstatusWrapperComponent, WipdetailsComponent,
    WorkordertasktimerWrapperComponent, WorkordertaskmonitosWrapperComponent, EmPmButtonWrapperComponent, AddNcrButtonWrapperComponent,
    WipListButtonWrapperComponent, SelectWorkOrderDropDownWrapperComponent, PrinttravelerReportComponent, PrintotherReportComponent,
    DeliveryTicketReportComponent, WipHistoryReportComponent, NcrReportComponent, PartLabelRollComponent, TechnicalDataLabelComponent,
    WorkReportComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MultiSelectModule,
    TableModule,
    CalendarModule,
    UtilsModule,
    WidgetModule,
    NewWidgetModule,
    ProgressbarModule,
    AlertModule,
    TabViewModule,
    CarouselModule,
    DropdownModule,
    FormsModule,
    QRCodeModule,
    NgxBarcodeModule
  ]
})
export class WipModule { static routes = routes; }
