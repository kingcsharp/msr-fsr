import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { WorkOrderModel } from '../../services/api.client.generated';

@Component({
  selector: 'printtraveler-report',
  templateUrl: './printtraveler-report.component.html',
  styleUrls: ['./printtraveler-report.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class PrinttravelerReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  
  showPrintTravelerDialog: boolean = true;

  constructor() { }

  ngOnInit(): void {
  }

  togglePrintTravelerDialog(){
    this.showPrintTravelerDialog = !this.showPrintTravelerDialog;
  }

  printTravelerReport(){
    window.print();
  }

}
