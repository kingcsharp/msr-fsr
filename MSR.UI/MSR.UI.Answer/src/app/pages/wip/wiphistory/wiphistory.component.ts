import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary } from '../../../services/api.client.generated';
import { Router } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-wiphistory',
  templateUrl: './wiphistory.component.html',
  styleUrls: ['./wiphistory.component.scss'],
  providers: [WorkOrderService]
})
export class WiphistoryComponent implements OnInit {

  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  data: Array<WorkOrderGridSummary>;
  statusOptions: Array<SelectItem>;
  canRead: boolean = false;
  privileges = EnumPrivilege;
  locationOptions: Array<SelectItem>;
  gridVersion: string;

  constructor(public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private router: Router, private workOrderService: WorkOrderService) { }

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
      new ColumnsSaved({ id: 'actualEndDate', label: 'Actual End Date', visible: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product', visible: true }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true })
    ];


    this.canRead = this.globals.hasPrivilege(EnumMenuItem.WIPHistory, this.privileges.CanRead);

    if (this.canRead === false) {
      this.router.navigate(['app/wip/wipstatus']);
    }

    this.workOrderService.history(env.apiVersion).subscribe(responseHandler(response => {
      this.data = response.object;
      this.statusOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
      ).map(x => ({ label: x.status, value: x.status }));
      this.locationOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.locationName === thing.locationName) === i
      ).map(x => ({ label: x.locationName, value: x.locationName }));
      this.loading = false;
    }));

  }

}
