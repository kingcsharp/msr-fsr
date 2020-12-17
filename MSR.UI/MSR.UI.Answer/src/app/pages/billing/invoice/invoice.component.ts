import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  InvoiceService, InvoiceView, InvoiceItemView, CustomerService, LocationService, EnumMenuItem,
  UpdateInvoiceRequest, CreateInvoiceRequest, EnumApprovalTables, Customer, LocationModel, WorkOrderService, AuditActionResultOfInvoiceView, CreateInvoiceItemRequest, UpdateInvoiceItemRequest, InvoiceableWorkOrderView
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import * as moment from 'moment';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../../models/lib/Utils';
import { Observable } from 'rxjs';

declare let jQuery: any;

@Component({
  selector: 'invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  approvalTables = EnumApprovalTables;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridWoStorageId: string;
  gridSettings: ColumnsSaved[];
  gridWoSettings: ColumnsSaved[];
  gridVersion: string;
  roles: any[];
  allRoles: any[] = [];
  canCreate: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currentInvoice: any;
  data: any;
  isKitStatus: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;
  allParts: any[] = [];
  isActive: any[];
  uploadedFinished: boolean = false;
  showApproveButtons: boolean = true;
  userPrivileges: AllowedActions;
  customers: Array<Customer>;
  locations: Array<LocationModel>;
  invoiceItemOptions: any;
  combineSelected: boolean = true;
  showWorkOrders: boolean = false;
  showInvoiceItems: boolean = true;
  allWorkorders: Array<InvoiceableWorkOrderView> = new Array <InvoiceableWorkOrderView>();
  workorders: Array<InvoiceableWorkOrderView> = new Array<InvoiceableWorkOrderView>();
  calendarEn: any;
  isSelectAllWorkOrders: boolean = false;

  constructor(public globals: Globals, private invoiceService: InvoiceService, public cg: CommonGrid,
    private elem: ElementRef, private toastr: ToastrService, private customerService: CustomerService,
    private locationService: LocationService, private workOrderService: WorkOrderService) {

  }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.calendarEn = this.globals.getCalendarDefault();
    this.gridStorageId = 'invoiceGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'customerName', label: 'Customer Name', visible: true }),
    new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
    new ColumnsSaved({ id: 'invoiceNumber', label: 'Invoice Number', visible: true }),
    new ColumnsSaved({ id: 'amount', label: 'Amount', visible: true }),
    new ColumnsSaved({ id: 'dueDate', label: 'Due Date', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'createdByName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedByName', label: 'Updated By', visible: false })
    ];

    this.gridWoStorageId = 'invoiceWorkorderGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridWoSettings = [
      new ColumnsSaved({ id: 'customerName', label: 'Customer Name', visible: true }),
      new ColumnsSaved({ id: 'id', label: 'WorkOrder #', visible: true }),
      new ColumnsSaved({ id: 'referencePO', label: 'PO #', visible: true }),
      new ColumnsSaved({ id: 'customerPurchaseNumber', label: 'Customer Puchase Number', visible: true }),
      new ColumnsSaved({ id: 'customerLineNumber', label: 'Customer Line', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Work Order Item', visible: true }),
      new ColumnsSaved({ id: 'locationName', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true }),
      new ColumnsSaved({ id: 'actualEndDate', label: 'Work Order Complete Date', visible: true }),
      new ColumnsSaved({ id: 'totalSalePrice', label: 'Total', visible: true })
    ];

    this.isKitStatus = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.isActive = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];

    this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.Invoices);
    this.getInvoices();
    this.getLocations();
    this.getCustomers();
    this.data = [];
  }

  getWorkOrders() {
    this.globals.showLoader(true);
    this.showWorkOrders = false;
    this.workOrderService.invoiceable(env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        replaceArrayItems(this.allWorkorders, response.object);
        replaceArrayItems(this.workorders, response.object);
        if (this.currentInvoice.customerId) {
          this.workorders = this.workorders.filter(x => x.customerId === this.currentInvoice.customerId);
        }
        if (this.currentInvoice.locationId) {
          this.workorders = this.workorders.filter(x => x.locationId === this.currentInvoice.locationId);
        }
        this.isSelectAllWorkOrders = false;
        this.showWorkOrders = true;
      }));
  }

  locationChanged() {
    this.currentInvoice.locationId = this.currentInvoice.location.id;
    this.workorders = this.allWorkorders.filter(x => x.locationId === this.currentInvoice.locationId);
    this.isSelectAllWorkOrders = false;
  }

  customerChanged() {
    this.currentInvoice.customerId = this.currentInvoice.customer.id;
    this.workorders = this.allWorkorders.filter(x => x.customerId === this.currentInvoice.customerId);
    this.isSelectAllWorkOrders = false;
  }

  getCustomers() {
    this.globals.showLoader(true);
    this.customerService.customerGet(null, null, null, null, null, null, null, true, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.customers = response.object;
      }));
  }

  downloadAllInvoices(invoiceId) {
    this.globals.showLoader(true);
    this.invoiceService.download(invoiceId ? invoiceId : null, null, null, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.downloadItem(response.data);
      }));
  }

  downloadFilteredInvoices() {
    this.globals.showLoader(true);
    if (localStorage[this.gridStorageId] !== undefined) {
      let filters = JSON.parse(localStorage[this.gridStorageId]).filters;
      this.invoiceService.download(this.getFilterVal(filters, 'id'), this.getFilterVal(filters, 'customerName'),
        this.getFilterVal(filters, 'description'),
        this.getFilterVal(filters, 'invoiceNumber'), this.getFilterVal(filters, 'invoiceDate'),
        this.getFilterVal(filters, 'createdOn'), this.getFilterVal(filters, 'createdByName'),
        this.getFilterVal(filters, 'lastUpdatedOn'), this.getFilterVal(filters, 'lastUpdatedByName'),
        this.getFilterVal(filters, 'total'), this.getFilterVal(filters, 'statusId'), env.apiVersion)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          this.downloadItem(response.data);
        }));
    } else {
      this.downloadAllInvoices(null);
    }
  }

  downloadItem(data) {
    let a = document.createElement('a');
    document.body.appendChild(a);
    a.style.display = 'none';
    const url = window.URL.createObjectURL(data);
    a.href = url;
    a.download = `invoices_${moment().format('MM_DD_YYYY')}.zip`;
    a.click();
    window.URL.revokeObjectURL(url);
    a.remove();
  }

  getLocations() {
    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.locations = response.object.filter(x => x.parentId === null);
      }));
  }

  showDialog(invoice: InvoiceView) {
    this.currentInvoice = this.getInvoice(invoice);
    this.invoiceItemOptions = JSON.parse(JSON.stringify(this.currentInvoice.invoiceItems));
    this.getWorkOrders();
    this.isSelectAllWorkOrders = false;
    this.display = true;
  }

  selectWorkOrder(ev, workorder: InvoiceableWorkOrderView) {
    if (ev.checked) {
      this.showInvoiceItems = false;
      let addInvoiceItem = new InvoiceItemView({
        purchaseOrderId: workorder.purchaseOrderId,
        purchaseNumber: workorder.customerPurchaseNumber,
        workOrderId: workorder.id,
      });

      pushIfNotExists(addInvoiceItem, this.invoiceItemOptions, 'workOrderId');
      pushIfNotExists(addInvoiceItem, this.currentInvoice.invoiceItems, 'workOrderId');

      setTimeout(() => {
        this.showInvoiceItems = true;
      }, 10);
    } else {
      this.isSelectAllWorkOrders = false;
    }
  }

  selectAllWorkOrders(event) {
    if (event.checked) {
      this.showInvoiceItems = false;

      this.workorders.forEach((workorder, index) => {
        this.workorders[index]['checked'] = true;
        let addInvoiceItem = new InvoiceItemView({
          purchaseOrderId: workorder.purchaseOrderId,
          purchaseNumber: workorder.customerPurchaseNumber,
          workOrderId: workorder.id,
        });

        pushIfNotExists(addInvoiceItem, this.invoiceItemOptions, 'workOrderId');
        pushIfNotExists(addInvoiceItem, this.currentInvoice.invoiceItems, 'workOrderId');
      });

      setTimeout(() => {
        this.showInvoiceItems = true;
      }, 10);
    } else {
      this.workorders.forEach((_, index) => {
        this.workorders[index]['checked'] = false;
      });
    }
  }

  closeDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getInvoices() {
    this.globals.showLoader(true);
    this.invoiceService.invoiceGet(null, null, null, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
      }));
  }

  onInvoiceSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const isValid = jQuery('.parsleyjs').parsley().isValid();
    if (isValid) {
      this.globals.showLoader(true);
      let method: Observable<AuditActionResultOfInvoiceView> = null;
      let basicReqData: any = {
        description: this.currentInvoice.description,
        invoiceDate: moment(this.currentInvoice.dueDate, 'MM/DD/YYYY').toDate(),
        taxPercentage: this.currentInvoice.taxPercentage
      };

      if (this.currentInvoice.id !== undefined) {
        basicReqData.id = this.currentInvoice.id;
        basicReqData.invoiceItems = this.currentInvoice.invoiceItems.map((item) => {
          return new UpdateInvoiceItemRequest({ id: item.workOrderId });
        });
        method = this.invoiceService.invoicePatch(env.apiVersion, new UpdateInvoiceRequest(basicReqData));
      } else {
        basicReqData.invoiceItems = this.currentInvoice.invoiceItems.map((item) => {
          return new CreateInvoiceItemRequest(item);
        });
        basicReqData.invoiceClass = this.currentInvoice.location.invoiceClass;
        basicReqData.customerId = this.currentInvoice.customerId;
        if (this.combineSelected) {
          method = this.invoiceService.createOneInvoice(env.apiVersion, new CreateInvoiceRequest(basicReqData));
        } else {
          this.invoiceService.createIndividualInvoices(env.apiVersion, new CreateInvoiceRequest(basicReqData)).pipe(take(1)).subscribe(responseHandler((resp) => {
            if (!resp.hasErrors) {
              this.data.push(...resp.object);
              this.data = this.data.slice(0);
              this.closeDialog();
            }
          }, () => {
          }));
        }
      }
      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (this.currentInvoice.id === undefined) {
            this.data.push(resp.object);
            this.data = this.data.slice(0);
          } else {
            const index = this.data.findIndex(x => x.id === this.currentInvoice.id);
            this.data.splice(index, 1);
            this.data.splice(index, 0, resp.object);
            this.data = this.data.slice(0);
          }
          this.closeDialog();
        }
      }, () => {

      }));
    }
  }

  getFilterVal(sotorageGridFilters, id) {
    if (sotorageGridFilters === undefined) {
      return null;
    }
    const item = sotorageGridFilters[id];
    if (item === undefined) {
      return null;
    }
    return sotorageGridFilters[id].value;
  }

  getInvoice(invoice) {
    this.combineSelected = true;
    if (invoice === undefined) {
      let ret = new InvoiceView();
      ret.dueDate = new Date();
      ret.invoiceItems = [];
      emptyArray(this.workorders);
      return ret;
    }

    let invoiceCopy = copyObj(invoice);
    invoiceCopy.dueDate = moment(invoiceCopy.dueDate).toDate();
    invoiceCopy.location = this.locations.find(x => x.id === invoice.locationId);
    invoiceCopy.customer = this.customers.find(x => x.id === invoice.customerId);
    return invoiceCopy;
  }
}
