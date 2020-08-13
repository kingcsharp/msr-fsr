import { Component, OnInit, ElementRef, AbstractType } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  InvoiceService, InvoiceView, InvoiceItemView, CustomerService, LocationService,
  CreateInvoiceRequest, EnumApprovalTables, Customer, LocationModel, WorkOrderService, WorkOrderModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { replaceArrayItems, pushIfNotExists } from '../../../models/lib/Utils';
import { UrlHandlingStrategy } from '@angular/router';

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
  showWorkOrders: boolean = false;
  showInvoiceItems: boolean = true;
  workorders: Array<WorkOrderModel> = new Array<WorkOrderModel>();
  constructor(private globals: Globals, private invoiceService: InvoiceService, public cg: CommonGrid,
    private elem: ElementRef, private toastr: ToastrService, private customerService: CustomerService,
    private locationService: LocationService, private workOrderService: WorkOrderService) {

  }

  ngOnInit(): void {
    // this.currentInvoice = this.getInvoice(undefined);
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
      new ColumnsSaved({ id: 'customerLine', label: 'Customer Line', visible: true }),
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

    // this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.Invoices);
    var a = {
      canRead: true,
      canActivate: true,
      canCreate: true,
      canEdit: true,
      canDelete: true
    } as AllowedActions;

    this.userPrivileges = new AllowedActions(a);

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
          const workOrdersUpdated = response.object.map((wo) => {
            const firstPartWithNullParent = wo.workOrderParts.find(x => x.parentId === undefined || x.parentId === null);
            wo.serialNumber = firstPartWithNullParent.serialNumber;
            return wo;
          });
          replaceArrayItems(this.workorders, workOrdersUpdated);
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

  downloadAllInvoices() {
    this.invoiceService.download(null, null, null, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.downloadItem(response.data);
      }));
  }

  downloadFilteredInvoices() {
    if (localStorage[this.gridStorageId] !== undefined) {
      var filters = JSON.parse(localStorage[this.gridStorageId]).filters;
      this.invoiceService.download(this.getFilterVal(filters, "id"), this.getFilterVal(filters, "customerName"),
        this.getFilterVal(filters, "description"),
        this.getFilterVal(filters, "invoiceNumber"), this.getFilterVal(filters, "invoiceDate"),
        this.getFilterVal(filters, "createdOn"), this.getFilterVal(filters, "createdByName"),
        this.getFilterVal(filters, "lastUpdatedOn"), this.getFilterVal(filters, "lastUpdatedByName"),
        this.getFilterVal(filters, "total"), this.getFilterVal(filters, "statusId"), env.apiVersion)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          this.downloadItem(response.data);
        }));
    }
  }

  downloadItem(data) {
    var URL = window.URL;
    var downloadURL = URL.createObjectURL(data);
    window.open(downloadURL);
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
      var addInvoiceItem = new InvoiceItemView({
        purchaseOrderId: workorder.purchase.purchaseOrderId,
        purchaseNumber: workorder.purchase.customerPurchaseNumber,
        workOrderId: workorder.id,
      });

      pushIfNotExists(addInvoiceItem, this.invoiceItemOptions, "purchaseOrderId");
      pushIfNotExists(addInvoiceItem, this.currentInvoice.invoiceItems, "purchaseOrderId");

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
    if (invoice === undefined) {
      let ret = new InvoiceView();
      ret.invoiceItems = [];
      return ret;
    }

    invoice.location = this.locations.find(x => x.id === invoice.locationId);
    invoice.customer = this.customers.find(x => x.id === invoice.customerId);
    return invoice;
  }
}
