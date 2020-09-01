import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ConfirmationService } from 'primeng/api';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { EnumMenuItem, PurchaseOrderService, CustomerService, ProductService, UpdatePurchaseOrderRequest } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';

declare let jQuery: any;

@Component({
  selector: 'app-purchase-orders',
  templateUrl: './purchase-orders.component.html',
  styleUrls: ['./purchase-orders.component.scss'],
  providers: [ConfirmationService],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class PurchaseOrdersComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: any;
  purchasePrivileges: AllowedActions;
  purchaseOrderPrivileges: AllowedActions;
  display: boolean = false;
  currentPO: PurchaseOrder;
  customersData: any[] = [];
  getCustomersFlag: boolean = false;
  productsData: any[] = [];
  getProductsFlag: boolean = false;
  purchaseOrderStatus: any[] = [
    {
      label: 'All',
      value: 'All'
    },
    {
      label: 'Open',
      value: 'Open'
    },
    {
      label: 'Closed',
      value: 'Closed'
    }
  ]

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private confirmationService: ConfirmationService,
    private purchaseOrderService: PurchaseOrderService,
    private customerService: CustomerService,
    private productService: ProductService
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'quotesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'ID', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'referencePo', label: 'PO#', visible: true }),
      new ColumnsSaved({ id: 'invoicedBalance', label: 'Invoiced', visible: true }),
      new ColumnsSaved({ id: 'uninvoicedBalance', label: 'Not Invoiced', visible: true }),
      new ColumnsSaved({ id: 'balance', label: 'Balance', visible: true }),
      new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true }),
      new ColumnsSaved({ id: 'openDate', label: 'Open Date', visible: true }),
      new ColumnsSaved({ id: 'closeDate', label: 'Close Date', visible: true }),
      new ColumnsSaved({ id: 'totalPurchaseLimit', label: 'Total', visible: true }),
      new ColumnsSaved({ id: 'unusedAmount', label: 'Unused', visible: true }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true })
    ];
    this.purchasePrivileges = this.globals.getEnumPrivileges(this.menuItems.Purchases);
    this.purchaseOrderPrivileges = this.globals.getEnumPrivileges(this.menuItems.PurchaseOrders);
    this.data = [];
    this.getPurchaseOrders();
    this.getCustomers();
    this.currentPO = new PurchaseOrder();
    this.getProducts();
  }

  getPurchaseOrders() {
    this.globals.showLoader(true);
    this.purchaseOrderService.purchaseOrderGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  onClickEdit(purchaseOrder: PurchaseOrder) {
    //TODO: Edit function
    this.currentPO = purchaseOrder;
    this.display = true;
  }

  onClickDelete(purchaseOrder: PurchaseOrder) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to Delete this record?',
      accept: () => {
        this.deletePurchaseOrder(purchaseOrder);
      }
    });
  }

  deletePurchaseOrder(purchaseOrder: PurchaseOrder) {
    this.globals.showLoader(true);
    this.purchaseOrderService.purchaseOrderDelete(purchaseOrder.id, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        const index = this.data.findIndex(x => x.id === purchaseOrder.id);
        this.data.splice(index, 1);
      }));
  }

  onClickPurchase(purchaseOrder: PurchaseOrder) {
    //TODO: Purchase function
  }

  onClickClose(purchaseOrder: PurchaseOrder) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to Close this record?',
      accept: () => {
        this.closePurchaseOrder(purchaseOrder);
      }
    });
  }

  closePurchaseOrder(purchaseOrder: PurchaseOrder) {
    let purchaseOrderRequest = new UpdatePurchaseOrderRequest();
    Object.assign(purchaseOrderRequest, purchaseOrder);
    this.globals.showLoader(true);
    // BACKEND ENDPOINT TBD
    // this.purchaseOrderService.purchaseOrderPatch(env.apiVersion,purchaseOrderRequest).pipe(take(1))
    // .subscribe(responseHandler(response => {
    //   purchaseOrder.status = 'Closed';
    // }));
  }

  onClickAdd() {
    //TODO: Add function
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getCustomers() {
    const ctrl = this;
    if (ctrl.getCustomersFlag) {
      return ctrl.customersData;
    }
    this.customerService.customerGet(null, null, null, null, null, null, null
      , true, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.customersData = response.object;
        ctrl.getCustomersFlag = true;
      }));
  }

  getProducts() {
    const ctrl = this;
    this.productService.productGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.productsData = response.object;
      }));
  }

  onEditSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      const index = this.data.findIndex(x => x.id === this.currentPO.id);
      this.data[index] = this.currentPO;
      this.currentPO = new PurchaseOrder();
      ctrl.clseDialog();
    }
  }
}


// Temp model
interface IPurchaseOrder {
  id: number;
  name: string;
  referencePo: string;
  invoicedBalance?: number;
  uninvoicedBalance?: number;
  balance: number;
  customerName: string;
  customerId: number,
  openDate: Date;
  closeDate?: Date;
  totalPurchaseLimit: number;
  unusedAmount: number;
  revision: number;
  status: 'Open' | 'Closed' | 'All';
}

class PurchaseOrder implements IPurchaseOrder {
  id: number;
  name: string;
  referencePo: string;
  invoicedBalance: number;
  uninvoicedBalance: number;
  balance: number;
  customerName: string;
  customerId: number;
  openDate: Date;
  closeDate: Date;
  totalPurchaseLimit: number;
  unusedAmount: number;
  revision: number;
  status: 'Open' | 'Closed' | 'All';


  constructor(data?: IPurchaseOrder) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}

const demoData = [
  new PurchaseOrder({
    id: 516481,
    name: 'PO1590779276',
    referencePo: 'refcustponum1223334444',
    invoicedBalance: 0,
    uninvoicedBalance: 0,
    balance: 34775,
    customerName: '[MSR-FSR] APPLIED MATERIALS - [ID:1566]',
    customerId: 1566,
    openDate: new Date(),
    closeDate: null,
    totalPurchaseLimit: 1234567,
    unusedAmount: 1234567,
    revision: 2,
    status: 'All'
  }),
  new PurchaseOrder({
    id: 516481,
    name: 'PO1590779276',
    referencePo: 'CPO1590779276',
    invoicedBalance: 34775,
    uninvoicedBalance: 0,
    balance: 34775,
    customerName: '[MSR-FSR]ATA - [ID:1574]',
    customerId: 1574,
    openDate: new Date(),
    closeDate: new Date(),
    totalPurchaseLimit: 1234567,
    unusedAmount: 1199792,
    revision: 1,
    status: 'Closed'
  })
]


