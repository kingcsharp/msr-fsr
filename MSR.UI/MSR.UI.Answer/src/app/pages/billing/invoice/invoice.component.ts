import { Component, OnInit, ElementRef, AbstractType } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  InvoiceService, InvoiceView, InvoiceItemView, CustomerService, LocationService, EnumMenuItem,
  UpdateInvoiceRequest, CreateInvoiceRequest, EnumApprovalTables, Customer, LocationModel, WorkOrderService, WorkOrderModel, AuditActionResultOfInvoiceView, CreateInvoiceItemRequest, UpdateInvoiceItemRequest
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
  workorders: Array<WorkOrderModel> = new Array<WorkOrderModel>();
  calendarEn: any;
  constructor(private globals: Globals, private invoiceService: InvoiceService, public cg: CommonGrid,
    private elem: ElementRef, private toastr: ToastrService, private customerService: CustomerService,
    private locationService: LocationService, private workOrderService: WorkOrderService) {

  }

  ngOnInit(): void {
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
      new ColumnsSaved({ id: 'purchase.customerPurchaseNumber', label: 'Customer Puchase Number', visible: true }),
      new ColumnsSaved({ id: 'purchase.customerLineNumber', label: 'Customer Line', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Work Order Item', visible: true }),
      new ColumnsSaved({ id: 'location.name', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'product.name', label: 'Product Name', visible: true }),
      new ColumnsSaved({ id: 'actualEndDate', label: 'Work Order Complete Date', visible: true }),
      new ColumnsSaved({ id: 'product.totalSalePrice', label: 'Total', visible: true })
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
    if (this.currentInvoice.customerId !== undefined && this.currentInvoice.locationId !== undefined) {
      this.workOrderService.workOrderGet(null, this.currentInvoice.customerId,
        this.currentInvoice.locationId, null, env.apiVersion).pipe(take(1))
        .subscribe(responseHandler(response => {
          response.object.forEach((wo) => {
            const firstPartWithNullParent = wo.workOrderParts.find(x => x.parentId === undefined || x.parentId === null);
            if (firstPartWithNullParent !== undefined) {
              wo.serialNumber = firstPartWithNullParent.serialNumber;
            }
          });
          replaceArrayItems(this.workorders, response.object);
        }));
    }
  }

  locationChanged() {
    this.currentInvoice.locationId = this.currentInvoice.location.id;
    this.getWorkOrders();
  }

  customerChanged() {
    this.currentInvoice.customerId = this.currentInvoice.customer.id;
    this.getWorkOrders();
  }

  getCustomers() {
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
    this.locationService.locationGet(null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.locations = response.object.filter(x => x.parentId === null);
      }));
  }

  showDialog(invoice: InvoiceView) {
    this.currentInvoice = this.getInvoice(invoice);
    this.invoiceItemOptions = JSON.parse(JSON.stringify(this.currentInvoice.invoiceItems));
    this.getWorkOrders();
    this.display = true;
  }

  selectWorkOrder(ev, workorder: WorkOrderModel) {
    if (ev.checked) {
      this.showInvoiceItems = false;
      let addInvoiceItem = new InvoiceItemView({
        purchaseOrderId: workorder.purchase.purchaseOrderId,
        purchaseNumber: workorder.purchase.customerPurchaseNumber,
        workOrderId: workorder.id,
      });

      pushIfNotExists(addInvoiceItem, this.invoiceItemOptions, 'purchaseOrderId');
      pushIfNotExists(addInvoiceItem, this.currentInvoice.invoiceItems, 'purchaseOrderId');

      setTimeout(() => {
        this.showInvoiceItems = true;
      }, 10);
    }
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getInvoices() {
    this.globals.showLoader(true);
    this.invoiceService.invoiceGet(null, null, null, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  onInvoiceSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
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
          return new UpdateInvoiceItemRequest(item);
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
              ctrl.data.push(...resp.object);
              ctrl.clseDialog();
            }
          }, () => {
          }));
        }
      }
      method.pipe(take(1)).subscribe(responseHandler((resp) => {
        if (!resp.hasErrors) {
          if (ctrl.currentInvoice.id === undefined) {
            ctrl.data.push(resp.object);
          } else {
            const index = ctrl.data.findIndex(x => x.id === ctrl.currentInvoice.id);
            ctrl.data.splice(index, 1);
            ctrl.data.splice(index, 0, resp.object);
          }
          ctrl.clseDialog();
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
