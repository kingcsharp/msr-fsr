import { Component, OnInit, ElementRef, AfterViewInit, ViewChild, OnDestroy } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { LazyLoadEvent, SelectItem } from 'primeng/api';
import {
  EnumMenuItem, EnumApprovalTables, WorkOrderService, WorkOrderGridSummary,
  ReportModel, PortalWorkOrderView, CreateWorkOrderMessageRequest, FileService, FileModel, WorkOrderMessageModel, WorkOrderTaskMonitorModel
} from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { take } from 'rxjs/operators';
import { GridSaved } from '../../../../app/models/lib/GridSaved';
import { EnumColumnType } from '../../../../app/models/enums/EnumColumnType';
import { PortalWorkOrderPartsView } from '../../../models/lib/PortalWorkOrderPartsView';
import { pushIfNotExists, callFunctionWithFilters, emptyArray } from '../../../models/lib/Utils';
import { EnumReport } from '../../../../app/models/enums/ReportType';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { EnumMonitorInputType } from '../../../models/enums/EnumMonitorInputType';
import { EnumMonitorShouldBe } from '../../../models/enums/EnumMonitorShouldBe';
import { EnumMonitorType } from '../../../models/enums/EnumMonitorType';
import { EnumMonitorPassFailStatus } from '../../../models/enums/EnumMonitorPassFailStatus';
import { Subscription } from 'rxjs';
@Component({
  selector: 'app-wip',
  templateUrl: './wip.component.html',
  styleUrls: ['./wip.component.scss'],
  providers: [WorkOrderService]
})
export class WipComponent implements OnInit, AfterViewInit, OnDestroy {
  menuItems = EnumMenuItem;
  enumReportTypes = EnumReport;
  // gridColumns: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridSaved: GridSaved;
  gridFilesSaved: GridSaved;
  reportFilesModel: ReportModel;
  selectedReport: EnumReport;
  gridPartsSaved: GridSaved;
  reportPartsModel: ReportModel;
  showReport: boolean = false;
  reportModel: ReportModel;
  showNcrModal: boolean = false;
  ncrWorkOrder: any;
  instructions: string;
  selectedColData: any;
  showInstructionDialog: boolean;
  subpartTextSearch: string;
  title: string;
  displayBasic2: boolean;
  activeIndex: number = 0;
  displayCustom: boolean;
  images: any[];
  files: any[];
  showFilesDialog: boolean = false;
  fromDate: Date = moment().subtract(6, 'weeks').toDate();
  toDate: Date = moment().toDate();
  totalRecords: number = 0;
  en = {
    firstDayOfWeek: 0,
    dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
    dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
    dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
    monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
    monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
    today: 'Today',
    clear: 'Clear',
    dateFormat: 'yyy-mm-dd'
  };
  subscriptions: Subscription[] = [];

  responsiveOptions: any[] = [
    {
      breakpoint: '1024px',
      numVisible: 5
    },
    {
      breakpoint: '768px',
      numVisible: 3
    },
    {
      breakpoint: '560px',
      numVisible: 1
    }
  ];


  @ViewChild('fileItem') fileItem: ElementRef;
  @ViewChild('statusCol') statusCol: ElementRef;
  @ViewChild('ncrItem') ncrItem: ElementRef;
  @ViewChild('disposition') disposition: ElementRef;
  @ViewChild('expandedRowTemplate') expandedRowTemplate: ElementRef;

  constructor(private elementReference: ElementRef, public globals: Globals,
    private workOrderService: WorkOrderService, public fileService: FileService,
    private toastr: ToastrService) {

  }

  ngOnInit(): void {
    this.setCustomerName();
    this.gridSaved = new GridSaved({
      columnsSaved: this.globals.isBuyer ? this.getBuyerColumns() : this.getEngineerColumns(),
      storageId: 'wip_engineering' + this.elementReference.nativeElement.tagName.toLowerCase(),
      version: '1.0.0',
      expandRows: true,
      expandRowProperty: 'subParts',
      expandRowsTemplate: this.expandedRowTemplate
    });

    this.reportModel = new ReportModel({
      name: ''
    });

    this.gridPartsSaved = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({ id: 'id', label: 'Id', visible: false, type: EnumColumnType.Number }),
        new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
        new ColumnsSaved({ id: 'partNumber', label: 'Company Part #', visible: true, type: EnumColumnType.String }),
        new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: EnumColumnType.Number, styles: { 'width': '10rem' } }),
        new ColumnsSaved({ id: 'qty', label: 'Qty', visible: true, type: EnumColumnType.Number, styles: { 'width': '6rem' } }),
        new ColumnsSaved({ id: 'name', label: 'Part Name', visible: true, type: EnumColumnType.String, styles: { 'width': '40rem' } }),
      ],
      showMyViewsFeature: false,
      paginator: false,
      storageId: 'wip_engineering_parts' + this.elementReference.nativeElement.tagName.toLowerCase(),
      version: '1.0.0'
    });

    this.reportPartsModel = new ReportModel({
      name: ''
    });
  }

  ngOnDestroy(): void {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  showPhotos(rowData) {
    this.globals.showLoader(true);
    this.workOrderService.workOrder(rowData.workOrderId, this.globals.selectedCustomer.id, null, null, null, null,
      env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(true);
        this.images = [];
        let count = response.object[0].workOrderTasks.length - 1;
        response.object[0].workOrderTasks.map(element => {
          this.globals.showLoader(true);
          this.fileService.fileGet(this.globals.getSingularMenuName(EnumMenuItem.WorkOrderTasks),
            element.id, null, env.apiVersion)
            .pipe(take(1)).subscribe(responseHandler((resp) => {
              if (resp.object.length > 0) {
                resp.object.forEach((file: FileModel) => {
                  if (file.contentType === 'image/gif' || file.contentType === 'image/tiff' ||
                    file.contentType === 'image/webp' || file.contentType === 'image/jpeg'
                    || file.contentType === 'image/png') {
                    this.images.push({
                      id: file.entityId,
                      previewImageSrc: file.fileURL,
                      thumbnailImageSrc: file.fileURL,
                      alt: '',
                      title: ''
                    });
                  }
                });
              }
              if (count === 0) {
                this.globals.showLoader(false);
                if (this.images.length > 0) {
                  this.displayBasic2 = true;
                } else {
                  this.toastr.error('Sorry, there are no pictures for the selected Work Order');
                }
              }
              count--;
            }));
        });
      }));
  }

  showFiles(rowData) {
    this.globals.showLoader(true);
    this.workOrderService.workOrder(rowData.workOrderId, this.globals.selectedCustomer.id, null, null, null, null,
      env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(true);
        this.files = [];
        let count = response.object[0].workOrderTasks.length - 1;
        this.gridFilesSaved = new GridSaved({
          columnsSaved: [
            new ColumnsSaved({ id: 'fileId', label: 'Id', type: EnumColumnType.Number, visible: false }),
            new ColumnsSaved({ id: 'fileURL', label: 'fileURL', type: EnumColumnType.Number, visible: false }),
            new ColumnsSaved({ id: 'name', label: 'Name', type: EnumColumnType.String, visible: true }),
            new ColumnsSaved({ id: 'supportingInfo', label: 'Actions', visible: true, type: EnumColumnType.Template, templateName: this.fileItem }),
          ],
          gridClass: 'formTbl',
          showMyViewsFeature: false,
          storageId: 'wip_engineering_filesGr' + this.elementReference.nativeElement.tagName.toLowerCase(),
          version: '1.0.0'
        });

        this.reportFilesModel = new ReportModel({
          // name: 'Work Orders' + custNameAdd
          name: ''
        });

        response.object[0].workOrderTasks.map(element => {
          this.globals.showLoader(true);
          this.fileService.fileGet(this.globals.getSingularMenuName(EnumMenuItem.WorkOrderTasks),
            element.id, null, env.apiVersion)
            .pipe(take(1)).subscribe(responseHandler((resp) => {
              if (resp.object.length > 0) {
                resp.object.forEach((file: FileModel) => {
                  if (!(file.contentType === 'image/gif' || file.contentType === 'image/tiff' ||
                    file.contentType === 'image/webp' || file.contentType === 'image/jpeg'
                    || file.contentType === 'image/png')) {
                    this.files.push(file);
                  }
                });
              }
              if (count === 0) {
                this.globals.showLoader(false);
                if (this.files.length > 0) {
                  this.showFilesDialog = true;
                } else {
                  this.toastr.error('Sorry, there are no files for the selected Work Order');
                }
              }
              count--;
            }));
        });
      }));
  }

  imageClick(index: number) {
    this.activeIndex = index;
    this.displayCustom = true;
  }

  ngAfterViewInit(): void {
    if (this.globals.selectedCustomer !== undefined) {
      this.setCustomerNameAndShowGrid();
    }

    let isBuyerObservableSubscription = this.globals.isBuyerObservable.subscribe(response => {
      if (this.globals.selectedCustomer !== undefined) {
        this.setCustomerNameAndShowGrid();
      }
    });
    this.subscriptions.push(isBuyerObservableSubscription);

    let selectCustomerObservableSubscription = this.globals.selectCustomerObservable.subscribe(response => {
      if (response !== null && response !== undefined) {
        this.setCustomerNameAndShowGrid();
      }
    });
    this.subscriptions.push(selectCustomerObservableSubscription);

  }

  setCustomerNameAndShowGrid() {
    this.setCustomerName();
    this.showReport = true;
  }

  expandRow(data: PortalWorkOrderPartsView) {
    // as example in case we need to fetch data to be shown in the grid.
  }

  addInstructions(coldata) {
    this.instructions = '';
    this.selectedColData = coldata;
    this.showInstructionDialog = true;
  }

  saveInstructions() {
    const request = new CreateWorkOrderMessageRequest({ id: this.selectedColData.workOrderId, message: this.instructions });
    this.workOrderService.message(env.apiVersion, request).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.selectedColData.messages.push(request);
        this.showInstructionDialog = false;
      }));
  }


  showReportInfo(reportType: EnumReport, row: any) {
    this.globals.showLoader(true);
    this.selectedReport = reportType;
    if (this.ncrWorkOrder?.workOrderId !== row.colData.workOrderId) {
      this.workOrderService.workOrder(row.colData.workOrderId, this.globals.selectedCustomer.id,
        null, null, null, null, env.apiVersion).pipe(take(1))
        .subscribe(responseHandler(response => {
          response.object[0].workOrderTasks.forEach(workOrderTask => {
            workOrderTask.workOrderTaskMonitors.forEach((workOrderTaskMonitor: any) => {

              workOrderTaskMonitor.pass = this.isMonitorPassing(workOrderTaskMonitor);


            });
          });

          this.ncrWorkOrder = response.object[0];
          this.showNcrModal = true;
        }));
    }
  }

  isMonitorPassing(workOrderTaskMonitor: WorkOrderTaskMonitorModel) {

    const monitorType = workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null
      ? workOrderTaskMonitor?.procedureStepMonitor?.monitorTypeId : workOrderTaskMonitor?.monitorTypeId;

    switch (monitorType) {
      case EnumMonitorType.Equipment:
        return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
      case EnumMonitorType.Number:

        if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Sensor || workOrderTaskMonitor?.inputTypeId === EnumMonitorInputType.Sensor) {
          return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null
            && workOrderTaskMonitor.textVal !== '' && workOrderTaskMonitor.textVal !== 'No Sensor Value Available');
        }

        if (workOrderTaskMonitor.procedureStepMonitor?.inputTypeId === EnumMonitorInputType.Manual) {

          if (workOrderTaskMonitor.numVal === null ||
            workOrderTaskMonitor.numVal === undefined) {
            return false;
          }

          let targetValue;
          if (workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null) {
            targetValue = Number(workOrderTaskMonitor.procedureStepMonitor?.targetValue);
          } else {
            targetValue = Number(workOrderTaskMonitor.targetValue);
          }


          switch (workOrderTaskMonitor.procedureStepMonitor.shouldBe) {
            case EnumMonitorShouldBe.EQUAL: {
              return (targetValue === workOrderTaskMonitor.numVal);
            }
            case EnumMonitorShouldBe.ABOVE: {
              return (targetValue <= workOrderTaskMonitor.numVal);
            }
            case EnumMonitorShouldBe.BELOW: {
              return (workOrderTaskMonitor.numVal <= targetValue);
            }
            case EnumMonitorShouldBe.BETWEEN: {

              if (workOrderTaskMonitor?.procedureMonitorId !== undefined && workOrderTaskMonitor?.procedureMonitorId !== null) {
                return (workOrderTaskMonitor.procedureStepMonitor.lowTarget <= workOrderTaskMonitor.numVal &&
                  workOrderTaskMonitor.procedureStepMonitor.highTarget >= workOrderTaskMonitor.numVal);
              } else {
                return (workOrderTaskMonitor.lowTarget <= workOrderTaskMonitor.numVal &&
                  workOrderTaskMonitor.highTarget >= workOrderTaskMonitor.numVal);

              }

            }
            default:
              return false;
          }

        }

        return workOrderTaskMonitor.numVal;
      case EnumMonitorType.YesOrNo:
        return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
      case EnumMonitorType.Text:
        return (workOrderTaskMonitor.textVal !== undefined && workOrderTaskMonitor.textVal !== null && workOrderTaskMonitor.textVal !== '');
      case EnumMonitorType.Select:
        return (workOrderTaskMonitor.multiVal !== undefined && workOrderTaskMonitor.multiVal !== null);
      case EnumMonitorType.PassOrFail:
        return workOrderTaskMonitor.numVal === EnumMonitorPassFailStatus.PassOrYes;
      default:
        break;
    }

    return false;
  }

  print() {
    window.print();
  }

  setCustomerName() {
    this.title = 'Work Orders';
    if (this.globals.selectedCustomer?.name !== undefined) {
      this.title += ' - ' + this.globals.selectedCustomer.name;
    }
  }

  setSubPartsProperties(subpart: any) {
    subpart.partNumber = subpart.partNumber || 'N/A';
    subpart.name = subpart.name || 'N/A';
  }

  setSubpartspropertiesToWoSubparts(workOrder: PortalWorkOrderPartsView) {
    workOrder.subParts.map(x => this.setSubPartsProperties(x));
  }

  getGridData(event: LazyLoadEvent) {
    debugger;
    this.globals.showLoader(true);
    setTimeout(() => {
      var a = this.globals.selectedCustomer;
      const pageFilters = { customerId: this.globals.selectedCustomer.id, fromDate: this.fromDate, toDate: this.toDate };
      callFunctionWithFilters(this.workOrderService, this.workOrderService.portal, pageFilters, this.gridSaved.columnsSaved, event)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          const retData = response.object.map((x: any) => {
            x.serialNumber = x.serialNumber === null ? 'N/A' : x.serialNumber;
            let ret = new PortalWorkOrderPartsView(x);
            this.setSubpartspropertiesToWoSubparts(ret);
            return ret;
          });
          emptyArray(this.data);
          this.data.push(...retData);
          this.totalRecords = response.totalNumberOfRecords;
        }));
    }, 10);
  }

  getEngineerColumns() {
    return [
      new ColumnsSaved({ id: 'workOrderId', label: 'Work Order Id', type: EnumColumnType.String, visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'companyPartNumber', label: 'Company Part #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: EnumColumnType.Number, styles: { 'width': '6rem' } }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'qty', label: 'Qty', visible: true, type: EnumColumnType.Number, styles: { 'width': '6rem' } }),
      new ColumnsSaved({ id: 'startDate', label: 'Start Date', visible: true, type: EnumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY', isRanged: true }),
      new ColumnsSaved({ id: 'dueDate', label: 'Due Date', visible: true, type: EnumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY', isRanged: true }),
      new ColumnsSaved({ id: 'partName', label: 'Part Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.Template, templateName: this.statusCol }),
      new ColumnsSaved({ id: 'supportingInfo', label: 'Supporting Info', visible: true, type: EnumColumnType.Template, templateName: this.ncrItem }),
      new ColumnsSaved({ id: 'disposition', label: 'Disposition', visible: true, type: EnumColumnType.Template, templateName: this.disposition }),
    ];
  }

  getBuyerColumns() {
    return [
      new ColumnsSaved({ id: 'workOrderId', label: 'Work Order Id', type: EnumColumnType.String, visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO #', type: EnumColumnType.Number, visible: true }),
      new ColumnsSaved({ id: 'qty', label: 'Qty', visible: true, type: EnumColumnType.Number, styles: { 'width': '6rem' } }),
      new ColumnsSaved({ id: 'startDate', label: 'Start Date', visible: true, type: EnumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY', isRanged: true }),
      new ColumnsSaved({ id: 'dueDate', label: 'Due Date', visible: true, type: EnumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY', isRanged: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'supportingInfo', label: 'Supporting Info', visible: true, type: EnumColumnType.Template, templateName: this.ncrItem }),
      new ColumnsSaved({ id: 'invoiceName', label: 'Invoice #', visible: true, type: EnumColumnType.String }),
      new ColumnsSaved({ id: 'price', label: 'Price', visible: true, type: EnumColumnType.Money }),
      new ColumnsSaved({ id: 'invoiceAmount', label: 'Amount', visible: true, type: EnumColumnType.Money }),
      new ColumnsSaved({ id: 'invoiceDate', label: 'Invoice Date', visible: true, type: EnumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY', isRanged: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true, type: EnumColumnType.Template, templateName: this.statusCol }),
    ];
  }
}
