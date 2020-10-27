import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel, WorkOrderPartService } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { take } from 'rxjs/operators';

@Component({
  selector: 'printtraveler-report',
  templateUrl: './printtraveler-report.component.html',
  styleUrls: ['./printtraveler-report.component.scss'],
  encapsulation: ViewEncapsulation.Emulated,
  providers: [WorkOrderPartService]
})
export class PrinttravelerReportComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel = undefined;
  parentPartImageUrl: string;
  showPrintTravelerDialog: boolean = false;
  urlToWipDetailsPage: string;
  parentPart: WorkOrderPartModel;

  constructor(private workOrderPartService: WorkOrderPartService) { }

  ngOnInit(): void {

    this.urlToWipDetailsPage = window.location.href;

    this.workOrderPartService.workOrderPartGet(this.WorkOrder.workOrderParts[0].id, null, env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {
      this.parentPart = response.object[0];
      this.parentPartImageUrl = this.parentPart?.part?.files[0]?.fileURL;
    }));
  }

  togglePrintTravelerDialog() {
    this.showPrintTravelerDialog = !this.showPrintTravelerDialog;
  }

  printTravelerReport() {
    window.print();
  }

}
