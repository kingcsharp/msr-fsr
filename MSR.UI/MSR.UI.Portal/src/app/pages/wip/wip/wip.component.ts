import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary, ReportModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { GridSaved } from '../../../../app/models/lib/GridSaved';
import { EnumColumnType } from '../../../../app/models/enums/EnumColumnType';

@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService]
})
export class WipComponent implements OnInit {

  gridColumns: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridSaved: GridSaved;
  showReport: boolean = false;
  reportModel: ReportModel;

  constructor(private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private workOrderService: WorkOrderService) { }

  ngOnInit(): void {
    this.gridColumns = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: false, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'purchaseId', label: 'Purchase Id', visible: false, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', type: EnumColumnType.String, visible: true }),
      new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'locationName', label: 'Location', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'Purchase Order Number', type: EnumColumnType.Number, visible: true }),
      new ColumnsSaved({ id: 'quantity', label: 'Quantity', visible: true, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'actualStartDate', label: 'Actual Start Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'actualEndDate', label: 'Actual End Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true, type: EnumColumnType.String })
    ];

    this.getGridData();
  }

  getGridData() {
    this.globals.showLoader(true);
    this.workOrderService.history(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.gridSaved = new GridSaved({
          columnsSaved: this.gridColumns,
          storageId: 'wip_engineering' + this.elementReference.nativeElement.tagName.toLowerCase(),
          version: '1.0.0'
        });

        this.reportModel = new ReportModel({
          name: 'Work Orders'
        });

        this.showReport = true;
      }));
  }
}
