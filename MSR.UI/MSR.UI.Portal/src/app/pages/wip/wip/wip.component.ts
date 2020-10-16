import { Component, OnInit, ElementRef, AfterViewInit, ViewChild } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary, ReportModel, PortalWorkOrderView } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { GridSaved } from '../../../../app/models/lib/GridSaved';
import { EnumColumnType } from '../../../../app/models/enums/EnumColumnType';
import { PortalWorkOrderPartsView } from '../../../models/lib/PortalWorkOrderPartsView';
import { pushIfNotExists } from '../../../models/lib/Utils';
import { EnumReport } from '../../../../app/models/enums/ReportType';

@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService]
})
export class WipComponent implements OnInit, AfterViewInit {
  enumReportTypes = EnumReport;
  // gridColumns: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridSaved: GridSaved;
  selectedReport: EnumReport;

  gridPartsSaved: GridSaved;
  reportPartsModel: ReportModel;
  showReport: boolean = false;
  reportModel: ReportModel;
  showNcrModal: boolean = false;
  ncrWorkOrder: any;
  @ViewChild('ncrItem') ncrItem: ElementRef;
  @ViewChild('expandedRowTemplate') expandedRowTemplate: ElementRef;

  constructor(private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private workOrderService: WorkOrderService) {

  }

  ngOnInit(): void {
    this.gridPartsSaved = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({ id: 'id', label: 'Id', visible: false, type: EnumColumnType.Number }),
        new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
        new ColumnsSaved({ id: 'partNumber', label: 'Company Part #', visible: true, type: EnumColumnType.String }),
        new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: EnumColumnType.Number }),
        new ColumnsSaved({ id: 'qty', label: 'Quantity', visible: true, type: EnumColumnType.Number }),
        new ColumnsSaved({ id: 'name', label: 'Part Name', visible: true, type: EnumColumnType.String }),
      ],
      showMyViewsFeature: false,
      storageId: 'wip_engineering_parts' + this.elementReference.nativeElement.tagName.toLowerCase(),
      version: '1.0.0'
    });

    this.reportPartsModel = new ReportModel({
      name: 'Sub Parts'
    });
  }

  ngAfterViewInit(): void {
    if (this.globals.selectedCustomer !== undefined) {
      this.getGridData();
    }

    this.globals.isBuyerObservable.subscribe(response => {
      if (this.globals.selectedCustomer !== undefined) {
        this.getGridData();
      }
    });

    this.globals.selectCustomerObservable.subscribe(response => {
      if (response !== null) {
        this.getGridData();
      }
    });
  }

  expandRow(data: PortalWorkOrderPartsView) {
    this.globals.showLoader(true);
    this.workOrderService.workOrder(data.id, this.globals.selectedCustomer.id, null, null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object[0].workOrderParts.forEach(element => {
          element.partNumber = element.part.partNumber;
          element.name = element.part.name;
          pushIfNotExists(element, data.workOrderParts, 'id');
        });

      }));


    // [gridSaved]="gridPartsSaved" [reportInfo]="reportPartsModel" [data]="expandRowData.rowData.workOrderParts"
  }


  showReportInfo(reportType: EnumReport, row: any) {
    this.globals.showLoader(true);
    this.selectedReport = reportType;
    if (this.ncrWorkOrder?.id !== row.colData.id)
      this.workOrderService.workOrder(row.colData.id, this.globals.selectedCustomer.id, null, null, null, env.apiVersion).pipe(take(1))
        .subscribe(responseHandler(response => {
          this.ncrWorkOrder = response.object[0];
          this.showNcrModal = true;
        }));
  }

  print() {
    window.print();
  }



  getGridData() {
    this.globals.showLoader(true);
    this.workOrderService.portal(this.globals.selectedCustomer.id, '', null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object.map(x => {
          let ret = new PortalWorkOrderPartsView(x);
          ret.workOrderParts = [];
          return ret;
        });
        this.gridSaved = new GridSaved({
          columnsSaved: this.globals.isBuyer ? this.getBuyerColumns() : this.getEngineerColumns(),
          storageId: 'wip_engineering' + this.elementReference.nativeElement.tagName.toLowerCase(),
          version: '1.0.0',
          expandRows: true,
          expandRowsTemplate: this.expandedRowTemplate
        });

        this.reportModel = new ReportModel({
          name: 'Work Orders'
        });

        this.showReport = true;
      }));
  }

  getEngineerColumns() {
    return [
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WO Item #', type: EnumColumnType.String, visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'companyPartNumber', label: 'Company Part #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'qty', label: 'Quantity', visible: true, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'startDate', label: 'Start Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'dueDate', label: 'Due Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'partName', label: 'Part Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'supportingInfo', label: 'Supporting Info', visible: true, type: EnumColumnType.Template, templateName: this.ncrItem, isRanged: true }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true, type: EnumColumnType.String }),
    ];
  }
  getBuyerColumns() {
    return [
      new ColumnsSaved({ id: 'id', label: 'Id', type: EnumColumnType.Number, visible: false }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', type: EnumColumnType.String, visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'supplier', label: 'Supplier', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO #', type: EnumColumnType.Number, visible: true }),
      new ColumnsSaved({ id: 'qty', label: 'Qty', visible: true, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'startDate', label: 'Start Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'dueDate', label: 'Due Date', visible: true, type: EnumColumnType.Date, isRanged: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true, type: EnumColumnType.String }),
      //Current Step/Status
      new ColumnsSaved({ id: 'supportingInfo', label: 'Supporting Info', visible: true, type: EnumColumnType.Template, templateName: this.ncrItem, isRanged: true }),
      new ColumnsSaved({ id: 'invoiceName', label: 'Invoice #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'price', label: 'Price', visible: true, type: EnumColumnType.Money }),
      new ColumnsSaved({ id: 'invoiceAmount', label: 'Amount', visible: true, type: EnumColumnType.Money }),
      new ColumnsSaved({ id: 'invoiceDate', label: 'Invoice Date', visible: true, type: EnumColumnType.Date, isRanged: true })
    ];
  }
}
