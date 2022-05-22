import { Component, OnInit, ElementRef, ViewEncapsulation } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { WorkOrderService, EnumSegregationType, ReportModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { LazyLoadEvent } from 'primeng/api';
import { callFunctionWithFiltersViews } from '../../../models/lib/Utils';
import { EnumColumnType } from '../../../../app/models/enums/EnumColumnType';
import { GridSaved } from '../../../models/lib/GridSaved';

@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService],
  encapsulation: ViewEncapsulation.Emulated,
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
  gridPartsSaved: GridSaved;
  reportPartsModel: ReportModel;
  prices: Array<number> = [];
  currentRowIndex: number = -1;
  invalidPriceError: boolean = false;
  adminOrManager: boolean = false;

  constructor(public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, private workOrderService: WorkOrderService) { }

  ngOnInit(): void {
    this.adminOrManager = this.globals.getCurrentUser().roles.some(role => role.name === 'Administrator' || role.name === 'Production Manager')

    this.gridPartsSaved = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({ id: 'id', label: 'Id', visible: false, disableSort: true, disableFilter: true, type: EnumColumnType.Number }),
        new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, disableSort: true, disableFilter: true, type: EnumColumnType.String,  styles: { 'text-align' : 'center' } }),
        new ColumnsSaved({ id: 'partNumber', label: 'Company Part #', visible: true, disableSort: true, disableFilter: true, type: EnumColumnType.String,  styles: { 'text-align' : 'center' } }),
        new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, disableSort: true, disableFilter: true, type: EnumColumnType.Number, styles: { 'width': '10rem', 'text-align' : 'center' } }),
        new ColumnsSaved({ id: 'qty', label: 'Qty', visible: true, disableSort: true, disableFilter: true, type: EnumColumnType.Number, styles: { 'width': '6rem', 'text-align' : 'center' } }),
        new ColumnsSaved({ id: 'name', label: 'Part Name', visible: true, disableSort: true, disableFilter: true, type: EnumColumnType.String, styles: { 'width': '30rem', 'text-align' : 'center' } }),
      ],
      showMyViewsFeature: false,
      paginator: false,
      storageId: 'wiphistory_parts' + this.elementReference.nativeElement.tagName.toLowerCase(),
      version: '1.0.0'
    });

    this.reportPartsModel = new ReportModel({
      name: ''
    });

    this.gridStorageId = 'wogrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'purchaseId', label: 'Purchase Id', visible: false, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'workOrderItemNumber', label: 'WorkOrder Item Number', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'locationName', label: 'Location', visible: true, type: EnumColumnType.StringArray }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'referencePO', label: 'PO #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'quantity', label: 'Quantity', visible: true, type: EnumColumnType.Number }),
      new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true, type: EnumColumnType.Date }),
      new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true, type: EnumColumnType.Date }),
      new ColumnsSaved({ id: 'actualStartDate', label: 'Actual Start Date', visible: true, type: EnumColumnType.Date }),
      new ColumnsSaved({ id: 'productName', label: 'Product', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.StringArray }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true, type: EnumColumnType.String }),
    ];

    if (this.adminOrManager)
      this.gridSettings.push(new ColumnsSaved({ id: 'price', label: 'Price', visible: true, type: EnumColumnType.Number }));

    this.statusOptions = [
      { label: 'In Progress', value: 'In Progress' },
      { label: 'Waiting to Start', value: 'Waiting to Start' }
  ];
    this.locationOptions = this.globals.getTopLevelLocations();
  }

  getWorkOrders(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      // this.workOrderService.menu(0, 100, null, null, env.apiVersion)
      callFunctionWithFiltersViews(this.workOrderService, this.workOrderService.menu, {}, this.gridSettings, event, this.globals.functionDic)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          this.totalRecords = response.totalNumberOfRecords;
          this.data = response.object;
          this.prices = this.data.map((workorderMenu) => workorderMenu.price);
          this.currentRowIndex = -1;
          this.invalidPriceError = false;
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

  getVisibleColumns() {
    return this.gridSettings.filter(x => x.visible).length;
  }

  onEditInit(): void {
    this.currentRowIndex = -1;
    this.invalidPriceError = false;
  }

  onEditCancel(): void {
    if (this.currentRowIndex !== -1) {
      this.data[this.currentRowIndex].price = this.prices[this.currentRowIndex];
    }
  }

  onEditComplete(): void {
    if (this.currentRowIndex !== -1) {
      if (this.invalidPriceError) {
        this.data[this.currentRowIndex].price = this.prices[this.currentRowIndex];
      } else {
        const newPrice = parseFloat(Number(this.data[this.currentRowIndex].price).toFixed(2));
        this.data[this.currentRowIndex].price = newPrice;
      }
    }
  }

  onChangePrice(rowIndex: number) {
    this.currentRowIndex = rowIndex;
    let invalidPriceError = false;
    if (!Number(this.data[this.currentRowIndex].price)) {
      invalidPriceError = true;
    }
    this.invalidPriceError = invalidPriceError;
  }

}
