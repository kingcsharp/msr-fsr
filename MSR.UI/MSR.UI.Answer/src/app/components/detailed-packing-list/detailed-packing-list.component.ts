import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';
import { PackingListViewModel } from './detailed-packing-list-view-model';
import { formatDate } from '@angular/common';
import { EnumWipPrintLogo } from '../../models/enums/EnumWipPrintLogo';

@Component({
  selector: 'detailed-packing-list',
  templateUrl: './detailed-packing-list.component.html',
  styleUrls: ['./detailed-packing-list.component.scss'],
  encapsulation: ViewEncapsulation.Emulated,
})
export class DetailedPackingListComponent implements OnInit {
  HEADING_MSRFSR = 'MSR_FSR';
  HEADING_KOMICO = 'KoMiCo';
  HEADING_NOLOGO = '';

  @Input() WorkOrder: WorkOrderModel;
  @Input() printLogo: EnumWipPrintLogo;
  packingList: PackingListViewModel;
  currentDate: string;
  heading: string;
  displayShipFromDetails: boolean = false;

  constructor() {
    this.packingList = new PackingListViewModel();
  }

  ngOnInit(): void {
    this.packingList.populate(this.WorkOrder, this.WorkOrder.purchase);
    this.currentDate = formatDate(new Date(), 'MM/dd/yyyy', 'en');
    this.setPropertiesBasedOnPrintLogo();
  }

  generateArray(qty: number = 1) {
    return new Array(qty);
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
