import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService]
})
export class WipComponent implements OnInit {

  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridVersion: string;

  constructor(public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private workOrderService: WorkOrderService) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: false }),
      new ColumnsSaved({ id: 'purchaseId', label: 'Purchase Id', visible: false }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', visible: true }),
      new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true }),
      new ColumnsSaved({ id: 'locationName', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'Purchase Order Number', visible: true }),
      new ColumnsSaved({ id: 'quantity', label: 'Quantity', visible: true }),
      new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true }),
      new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true }),
      new ColumnsSaved({ id: 'actualStartDate', label: 'Actual Start Date', visible: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product', visible: true }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true })
    ];



    this.workOrderService.menu(env.apiVersion).subscribe(responseHandler(response => {

      this.data = response.object;

      this.statusOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
      ).map(x => ({ label: x.status, value: x.status }));
      this.data.map((elem) => {

        elem.timeLoggedType = 'danger';

        if (elem.percentageOfExpectedDurationTimeLogged < 25) {
          elem.timeLoggedType = 'warning';
        } else if (elem.percentageOfExpectedDurationTimeLogged < 50) {
          elem.timeLoggedType = 'info';
        } else if (elem.percentageOfExpectedDurationTimeLogged < 75) {
          elem.timeLoggedType = 'success';
        }

        elem.tasksCompletedType = 'danger';

        if (elem.percentageOfTasksCompleted < 25) {
          elem.tasksCompletedType = 'warning';
        } else if (elem.percentageOfTasksCompleted < 50) {
          elem.tasksCompletedType = 'info';
        } else if (elem.percentageOfTasksCompleted < 75) {
          elem.tasksCompletedType = 'success';
        }

      });
      this.locationOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.locationName === thing.locationName) === i
      ).map(x => ({ label: x.locationName, value: x.locationName }));
      this.loading = false;

    }));


  }

}
