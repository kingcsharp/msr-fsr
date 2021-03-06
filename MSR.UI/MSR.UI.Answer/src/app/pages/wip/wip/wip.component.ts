import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { WorkOrderService, EnumSegregationType } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { LazyLoadEvent } from 'primeng/api';
import { callFunctionWithFiltersViews } from '../../../models/lib/Utils';

@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService]
})
export class WipComponent implements OnInit {

  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridVersion: string;
  EnumSegregationType = EnumSegregationType;
  totalRecords: number = 0;

  constructor(public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private workOrderService: WorkOrderService) { }

  ngOnInit(): void {

    this.gridStorageId = 'wogrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'purchaseId', label: 'Purchase Id', visible: false }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', visible: true }),
      new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true }),
      new ColumnsSaved({ id: 'locationName', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true }),
      new ColumnsSaved({ id: 'referencePO', label: 'PO #', visible: true }),
      new ColumnsSaved({ id: 'quantity', label: 'Quantity', visible: true }),
      new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true }),
      new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true }),
      new ColumnsSaved({ id: 'actualStartDate', label: 'Actual Start Date', visible: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product', visible: true }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true })
    ];

    this.statusOptions = this.globals.getTopLevelStatus();
    this.locationOptions = this.globals.getTopLevelLocations();
  }

  getWorkOrders(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      // this.workOrderService.menu(0, 100, null, null, env.apiVersion)
      callFunctionWithFiltersViews(this.workOrderService, this.workOrderService.menu, {}, this.gridSettings, event)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          debugger;
          this.totalRecords = response.totalNumberOfRecords;
          this.data = response.object;
          this.data.map((elem) => this.setElementStyle(elem));
        }));
    }, 10);
  }

  setElementStyle(elem) {
    elem.timeLoggedType = 'danger';
    if (elem.percentageOfExpectedDurationTimeLogged > .25) {
      elem.timeLoggedType = 'warning';
    }
    if (elem.percentageOfExpectedDurationTimeLogged > .50) {
      elem.timeLoggedType = 'info';
    }
    if (elem.percentageOfExpectedDurationTimeLogged > .75) {
      elem.timeLoggedType = 'success';
    }
    elem.tasksCompletedType = 'danger';
    if (elem.percentageOfTasksCompleted > .25) {
      elem.tasksCompletedType = 'warning';
    }
    if (elem.percentageOfTasksCompleted > .50) {
      elem.tasksCompletedType = 'info';
    }
    if (elem.percentageOfTasksCompleted > .75) {
      elem.tasksCompletedType = 'success';
    }
  }

}
