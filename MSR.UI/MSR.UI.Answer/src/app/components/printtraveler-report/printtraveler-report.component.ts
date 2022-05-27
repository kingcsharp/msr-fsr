import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel, WorkOrderPartService } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { filter, take } from 'rxjs/operators';
import { Router,  NavigationEnd } from '@angular/router';


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

  constructor(private workOrderPartService: WorkOrderPartService, private router: Router) { }

  ngOnInit(): void {
    this.router.events.pipe(
      filter.call(
        this.router.events,
        (event: Event) => event instanceof NavigationEnd
      )).subscribe(x => {this.urlToWipDetailsPage = window.location.href; });

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
