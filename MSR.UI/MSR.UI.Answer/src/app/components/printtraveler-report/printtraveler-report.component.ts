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
  parentPartImageUrl: string;

  showPrintTravelerDialog: boolean = false;

  constructor() { }

  ngOnInit(): void {

    if(this.WorkOrder.workOrderParts?.length > 0 && this.WorkOrder.workOrderParts[0].part?.files?.length > 0){
      this.parentPartImageUrl = this.WorkOrder.workOrderParts[0].part.files[0].fileURL;
    }

  }

  togglePrintTravelerDialog() {
    this.showPrintTravelerDialog = !this.showPrintTravelerDialog;
  }

  printTravelerReport() {
    window.print();
  }

}
