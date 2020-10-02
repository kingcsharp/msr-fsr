import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ConfirmationService } from 'primeng/api';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { EnumMenuItem, PurchaseOrderService, CustomerService, ProductService, UpdatePurchaseOrderRequest, CreatePurchaseOrderRequest, PurchaseOrderModel } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../../models/lib/Utils';
import { Utils } from 'ngx-bootstrap/utils';

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
  currentPO: PurchaseOrderModel;
  customersData: any[] = [];
  getCustomersFlag: boolean = false;
  productsData: any[] = [];
  getProductsFlag: boolean = false;
  showProductsSelect:boolean = true;
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
  ];

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
      new ColumnsSaved({ id: 'customerReferencePO', label: 'PO#', visible: true }),
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
    this.currentPO = new PurchaseOrderModel();
  }

  getPurchaseOrders() {
    this.globals.showLoader(true);
    this.purchaseOrderService.purchaseOrderGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  showPurchaseOrderModal(purchaseOrder: PurchaseOrderModel) {
    this.currentPO = this.getPuchaseOrder(purchaseOrder === undefined ? new PurchaseOrderModel() : purchaseOrder);
    this.display = true;
  }

  setCurrentCustomer(currentPO, purchaseOrder) {
    currentPO.customer = this.customersData.find(x => x.id === purchaseOrder.customerId);
  }

  onClickDelete(purchaseOrder: PurchaseOrderModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to Delete this record?',
      accept: () => {
        this.deletePurchaseOrder(purchaseOrder);
      }
    });
  }

  deletePurchaseOrder(purchaseOrder: PurchaseOrderModel) {
    this.globals.showLoader(true);
    this.purchaseOrderService.purchaseOrderDelete(purchaseOrder.id, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        const index = this.data.findIndex(x => x.id === purchaseOrder.id);
        this.data.splice(index, 1);
      }));
  }

  onClickClose(purchaseOrder: PurchaseOrderModel) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to Close this record?',
      accept: () => {
        this.closePurchaseOrder(purchaseOrder);
      }
    });
  }

  closePurchaseOrder(purchaseOrder: any) {
    this.globals.showLoader(true);
    let purchaseOrderRequest = new UpdatePurchaseOrderRequest();
    Object.assign(purchaseOrderRequest, purchaseOrder);
    purchaseOrderRequest.products = purchaseOrder.products.map((elem) => elem.id);
    purchaseOrderRequest.closeDate = new Date();
    // BACKEND ENDPOINT TBD
    this.purchaseOrderService.purchaseOrderPatch(env.apiVersion, purchaseOrderRequest).pipe(take(1))
      .subscribe(responseHandler(response => {
        const index = this.data.findIndex(x => x.id === purchaseOrder.id);
        this.data.splice(index, 1);
        this.data.splice(index, 0, response.object);
      }));
  }

  setCustomerId(purchaseOrderRequest: any, purchaseOrder: any) {
    purchaseOrderRequest.customerId = purchaseOrder.customer.id;
  }


  getPuchaseOrder(purchaseOrder) {
    if (purchaseOrder.id === undefined) {
      purchaseOrder.selectedProducts = [];
      return purchaseOrder;
    }
    let ret = copyObj(purchaseOrder);
    ret.selectedProducts = ret.products;
    this.setCurrentCustomer(ret, purchaseOrder);
    return ret;
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
    this.globals.showLoader(true);
    this.productService.productGet(null, this.currentPO.customer.id, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        ctrl.showProductsSelect = false;
        replaceArrayItems(ctrl.productsData, response.object);
        setTimeout(() => {
          ctrl.showProductsSelect = true;
        }, 10);
      }));
  }

  setSelectedProductIds(to, from) {
    to.products = from.selectedProducts.map((elem) => elem.id);
  }

  onPurchaseSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      let purchaseUpdateOrderRequest = new UpdatePurchaseOrderRequest();
      this.setCustomerId(purchaseUpdateOrderRequest, this.currentPO);
      Object.assign(purchaseUpdateOrderRequest, this.currentPO);
      this.setSelectedProductIds(purchaseUpdateOrderRequest, this.currentPO);
      if (this.currentPO.id === undefined) {
        let purchaseOrderRequest = new CreatePurchaseOrderRequest();
        Object.assign(purchaseOrderRequest, purchaseUpdateOrderRequest);
        this.purchaseOrderService.purchaseOrderPost(env.apiVersion, purchaseOrderRequest).pipe(take(1))
          .subscribe(responseHandler(response => {
            this.data.push(response.object);
            ctrl.clseDialog();
          }));
      } else {
        this.globals.showLoader(true);
        this.purchaseOrderService.purchaseOrderPatch(env.apiVersion, purchaseUpdateOrderRequest).pipe(take(1))
          .subscribe(responseHandler(response => {
            console.log(response);
            const index = this.data.findIndex(x => x.id === this.currentPO.id);
            this.data.splice(index, 1);
            this.data.splice(index, 0, response.object);
            ctrl.clseDialog();
          }));
      }
    }
  }
}

