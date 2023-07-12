import { Component, Input, OnInit, ViewEncapsulation } from "@angular/core";
import { SelectItem } from "primeng/api";
import { WorkOrderModel } from "../../services/api.client.generated";
import { EnumWipPrintLogo } from "../../models/enums/EnumWipPrintLogo";

@Component({
  selector: "printother-report",
  templateUrl: "./printother-report.component.html",
  styleUrls: ["./printother-report.component.scss"],
  encapsulation: ViewEncapsulation.Emulated,
})
export class PrintotherReportComponent implements OnInit {
  @Input() WorkOrder: WorkOrderModel;
  showPrintOtherDialog: boolean = false;
  selectedReport: string;
  reportsAvailable: Array<SelectItem>;
  enumWipPrintLogo = EnumWipPrintLogo;

  constructor() {}

  ngOnInit(): void {
    this.reportsAvailable = [
      { label: "Work Report", value: "WorkReport" },
      { label: "Delivery Ticket", value: "DeliveryTicket" },
      { label: "Delivery Ticket List KoMiCo", value: "DeliveryTicketKoMiCo" },
      { label: "Delivery Ticket List No Logo", value: "DeliveryTicketNoLogo" },
      { label: "Detailed Packing List", value: "DetailedPackingList" },
      {
        label: "Detailed Packing List KoMiCo",
        value: "DetailedPackingListKoMiCo",
      },
      {
        label: "Detailed Packing List No Logo",
        value: "DetailedPackingListNoLogo",
      },
      { label: "Cert of Compliance", value: "CertOfCompliance" },
      { label: "WIP History Report", value: "WIPHistoryReport" },
      { label: "NCR Report", value: "NCRReport" },
      { label: "NCR Label", value: "NCRLabel" },
      { label: "Technical Data Label", value: "TechnicalDataLabel" },
      { label: "Part Label Roll 4in", value: "PartLabelRoll4in" },
      { label: "Part Label Roll 4in KoMiCo", value: "PartLabelRoll4inKoMiCo" },
      { label: "Part Label Roll 4in No Logo", value: "PartLabelRoll4inNoLogo" },
    ];
  }

  togglePrintOtherDialog() {
    this.showPrintOtherDialog = !this.showPrintOtherDialog;
  }

  onHideePrintOtherDialog() {
    this.selectedReport = undefined;
  }

  print() {
    window.print();
  }
}
