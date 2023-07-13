import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';
import { PackingListViewModel } from '../detailed-packing-list/detailed-packing-list-view-model';
import { EnumWipPrintLogo } from '../../models/enums/EnumWipPrintLogo';

@Component({
  selector: 'delivery-ticket-report',
  templateUrl: './delivery-ticket-report.component.html',
  styleUrls: ['./delivery-ticket-report.component.scss'],
})
export class DeliveryTicketReportComponent implements OnInit {
  HEADING_MSRFSR = 'MSR_FSR';
  HEADING_KOMICO = 'KoMiCo';
  HEADING_NOLOGO = '';

  @Input() WorkOrder: WorkOrderModel;
  @Input() printLogo: EnumWipPrintLogo;
  packingList: PackingListViewModel;
  heading: string;
  displayShipFromDetails: boolean = false;

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
    this.setPropertiesBasedOnPrintLogo();
  }

  setPropertiesBasedOnPrintLogo(): void {
    if (this.printLogo === EnumWipPrintLogo.KoMiCo) {
      this.heading = this.HEADING_KOMICO;
    } else if (this.printLogo === EnumWipPrintLogo.NoLogo) {
      this.heading = this.HEADING_NOLOGO;
    } else {
      this.heading = this.HEADING_MSRFSR;
      this.displayShipFromDetails = true;
    }
  }
}
