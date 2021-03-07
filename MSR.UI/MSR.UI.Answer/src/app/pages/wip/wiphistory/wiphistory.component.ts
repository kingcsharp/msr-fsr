import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary, EnumSegregationType, WorkOrderHistoryView } from '../../../services/api.client.generated';
import { Router } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-wiphistory',
  templateUrl: './wiphistory.component.html',
  styleUrls: ['./wiphistory.component.scss'],
  providers: [WorkOrderService]
})
export class WiphistoryComponent implements OnInit {

  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  data: Array<WorkOrderHistoryView>;
  statusOptions: Array<SelectItem>;
  canRead: boolean = false;
  privileges = EnumPrivilege;
  locationOptions: Array<SelectItem>;
  gridVersion: string;
  EnumSegregationType = EnumSegregationType;
  totalRecords: number = 0;
  constructor(public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private router: Router, private workOrderService: WorkOrderService) { }

  ngOnInit(): void {

    this.gridStorageId = 'wiphistory' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'purchaseId', label: 'Purchase Id', visible: false }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', visible: true }),
      new ColumnsSaved({ id: 'customer', label: 'Customer', visible: true }),
      new ColumnsSaved({ id: 'location', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO #', visible: true }),
      new ColumnsSaved({ id: 'qty', label: 'Quantity', visible: true }),
      new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true }),
      new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true }),
      new ColumnsSaved({ id: 'actualStartDate', label: 'Actual Start Date', visible: true }),
      new ColumnsSaved({ id: 'actualEndDate', label: 'Actual End Date', visible: true }),
      new ColumnsSaved({ id: 'product', label: 'Product', visible: true }),
      new ColumnsSaved({ id: 'procedure', label: 'Procedure', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true })
    ];

    this.locationOptions = this.globals.getTopLevelLocations();
    this.statusOptions = [{ label: 'Complete', value: 'Complete' },
    { label: 'Cancelled', value: 'Cancelled' }];
    
    this.canRead = this.globals.hasPrivilege(EnumMenuItem.WIPHistory, this.privileges.CanRead);

    if (this.canRead === false) {
      this.router.navigate(['app/wip/wipstatus']);
    }
  }

  getWorkOrdersHistory(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(this.workOrderService, this.workOrderService.history, event, this.globals.functionDic)
        .pipe(take(1)).subscribe(responseHandler(response => {
          this.totalRecords = response.totalNumberOfRecords;
          this.data = response.object;
        }));
    }, 10);
  }

}
