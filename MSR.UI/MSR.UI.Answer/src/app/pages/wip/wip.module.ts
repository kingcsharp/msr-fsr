import { NgModule } from "@angular/core";
import { RouterModule, Routes } from "@angular/router";
import { CommonModule } from "@angular/common";
import { MultiSelectModule } from "primeng/multiselect";
import { UtilsModule } from "../../layout/utils/utils.module";
import { WipComponent } from "./wip/wip.component";
import { WiphistoryComponent } from "./wiphistory/wiphistory.component";
import { WipstatusComponent } from "./wipstatus/wipstatus.component";
import { WipstatusWrapperComponent } from "../../components/wipstatus-wrapper/wipstatus.component";
import { TableModule } from "primeng/table";
import { NewWidgetModule } from "../../layout/new-widget/widget.module";
import { CalendarModule } from "primeng/calendar";
import { TooltipModule } from "primeng/tooltip";
import { WidgetModule } from "../../layout/widget/widget.module";
import { ProgressbarModule } from "ngx-bootstrap/progressbar";
import { AlertModule } from "ngx-bootstrap/alert";
import { WipdetailsComponent } from "./wipdetails/wipdetails.component";
import { TabViewModule } from "primeng/tabview";
import { CarouselModule } from "ngx-bootstrap/carousel";
import { WorkordertasktimerWrapperComponent } from "../../components/workordertasktimer-wrapper/workordertasktimer-wrapper.component";
import { DropdownModule } from "primeng/dropdown";
import { FormsModule } from "@angular/forms";
import { WorkordertaskmonitorsWrapperComponent } from "../../components/workordertaskmonitors-wrapper/workordertaskmonitors-wrapper.component";
import { EmPmButtonWrapperComponent } from "../../components/em-pm-button-wrapper/em-pm-button-wrapper.component";
import { AddNcrButtonWrapperComponent } from "../../components/add-ncr-button-wrapper/add-ncr-button-wrapper.component";
import { TakeOverTaskButtonWrapperComponent } from "../../components/take-over-task-button-wrapper/take-over-task-button-wrapper.component";
import { WipListButtonWrapperComponent } from "../../components/wip-list-button-wrapper/wip-list-button-wrapper.component";
import { SelectWorkOrderDropDownWrapperComponent } from "../../components/select-work-order-drop-down-wrapper/select-work-order-drop-down-wrapper.component";
import { PrinttravelerReportComponent } from "../../components/printtraveler-report/printtraveler-report.component";
import { QRCodeModule } from "angularx-qrcode";
import { PrintotherReportComponent } from "../../components/printother-report/printother-report.component";
import { DeliveryTicketReportComponent } from "../../components/delivery-ticket-report/delivery-ticket-report.component";
import { DetailedPackingListComponent } from "../../components/detailed-packing-list/detailed-packing-list.component";
import { CertOfComplianceComponent } from "../../components/cert-of-compliance/cert-of-compliance.component";
import { NgxBarcodeModule } from "ngx-barcode";
import { WipHistoryReportComponent } from "../../components/wip-history-report/wip-history-report.component";
import { NcrReportComponent } from "../../components/ncr-report/ncr-report.component";
import { PartLabelRollComponent } from "../../components/part-label-roll/part-label-roll.component";
import { NcrLabelComponent } from "../../components/ncr-label/ncr-label.component";
import { TechnicalDataLabelComponent } from "../../components/technical-data-label/technical-data-label.component";
import { WorkReportComponent } from "../../components/work-report/work-report.component";
import { EditableCellComponent } from "./wipdetails/editable-cell/editable-cell.component";
import { OuterInnerLabelsComponent } from "../../components/outer-inner-labels/outer-inner-labels.component";

export const routes: Routes = [
  { path: "", redirectTo: "wipstatus", pathMatch: "full" },
  { path: "wip", component: WipComponent, pathMatch: "full" },
  { path: "wiphistory", component: WiphistoryComponent, pathMatch: "full" },
  { path: "wipstatus", component: WipstatusComponent, pathMatch: "full" },
  { path: "details/:id", component: WipdetailsComponent, pathMatch: "full" },
];

@NgModule({
  declarations: [
    WipComponent,
    WiphistoryComponent,
    WipstatusComponent,
    WipstatusWrapperComponent,
    WipdetailsComponent,
    WorkordertasktimerWrapperComponent,
    WorkordertaskmonitorsWrapperComponent,
    EmPmButtonWrapperComponent,
    AddNcrButtonWrapperComponent,
    WipListButtonWrapperComponent,
    SelectWorkOrderDropDownWrapperComponent,
    PrinttravelerReportComponent,
    PrintotherReportComponent,
    DeliveryTicketReportComponent,
    WipHistoryReportComponent,
    NcrReportComponent,
    PartLabelRollComponent,
    OuterInnerLabelsComponent,
    TechnicalDataLabelComponent,
    NcrLabelComponent,
    WorkReportComponent,
    TakeOverTaskButtonWrapperComponent,
    DetailedPackingListComponent,
    CertOfComplianceComponent,
    EditableCellComponent,
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MultiSelectModule,
    TableModule,
    CalendarModule,
    TooltipModule,
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
    NgxBarcodeModule,
  ],
})
export class WipModule {
  static routes = routes;
}
