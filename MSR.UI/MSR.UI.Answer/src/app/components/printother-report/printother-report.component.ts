import { Component, Input, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { WorkOrderModel } from '../../services/api.client.generated';

@Component({
  selector: 'printother-report',
  templateUrl: './printother-report.component.html',
  styleUrls: ['./printother-report.component.scss']
})
export class PrintotherReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  showPrintOtherDialog: boolean = false;
  selectedReport: string;
  reportsAvailable: Array<SelectItem>;

  constructor() { }

  ngOnInit(): void {

    this.reportsAvailable = [
      { label: 'Work Report', value: 'WorkReport'},
      { label: 'Delivery Ticket', value: 'DeliveryTicket'},
      { label: 'WIP History Report', value: 'WIPHistoryReport'},
      { label: 'NCR Report', value: 'NCRReport'},
      { label: 'Technical Data Label', value: 'TechnicalDataLabel'},
      { label: 'Part Label Roll 4in', value: 'PartLabelRoll4in'}
    ];

  }

  togglePrintOtherDialog() {
    this.showPrintOtherDialog = !this.showPrintOtherDialog;
    if(this.showPrintOtherDialog === false){
      this.selectedReport = undefined;
    }
  }

  print() {
    window.print();
  }

}
