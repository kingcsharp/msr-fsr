import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { environment as env } from '../../../../environments/environment';
import {
  PurchaseOrderService,
  PurchaseOrderView,
  ProductService,
  ProductModel,
  LocationService,
  CustomerService,
  CustomerModel,
  PurchaseService,
  CreatePurchaseRequest
} from '../../../services/api.client.generated';
import { Router, ActivatedRoute } from '@angular/router';
import * as moment from 'moment';

declare let jQuery: any;

@Component({
  selector: 'app-purchase-create',
  templateUrl: './purchase-create.component.html',
  styleUrls: ['./purchase-create.component.scss'],
  providers: [
    PurchaseOrderService,
    CustomerService,
    PurchaseService,
    LocationService,
  ],
})
export class PurchaseCreateComponent implements OnInit {
  poId: number;
  purchaseOrderData: PurchaseOrderView;
  getPurchaseOrderFlag: boolean = false;
  customerData: CustomerModel;
  getCustomerFlag: boolean = false;
  locationsData: any[] = [];
  getLocationsFlag: boolean = false;
  purchaseProducts: any[] = [];
  purchaseItems: any[] = [];
  purchaseSerializeItems: any[] = [];
  serialNumberModal: boolean = false;
  globalDueDate: Date;
  step: number;
  custLineElem: any;
  selectButtonOptions = [
    {
      label: 'ON',
      value: true
    },
    {
      label: 'OFF',
      value: false
    }
  ];

  constructor(
    public globals: Globals,
    private route: ActivatedRoute,
    private purchaseOrderService: PurchaseOrderService,
    private customerService: CustomerService,
    private purchaseService: PurchaseService,
    private locationService: LocationService,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.globalDueDate = moment().add(7, 'days').toDate();
    this.route.paramMap.subscribe(params => {
      this.poId = parseInt(params.get('id'), 10);
      this.step = 0;
      this.getPurchaseOrderData(this.poId);
    });
    this.getLocationsData();
  }

  getPurchaseOrderData(id: number) {
    this.globals.showLoader(true);
    this.purchaseOrderService.purchaseOrderGet(id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.purchaseOrderData = response.object[0];
        this.purchaseProducts = [];
        this.purchaseOrderData.products.map(product => {
          this.purchaseProducts.push({
            id: product.id,
            name: product.name,
            partName: product.partName,
            partNumber: product.partNumber,
            procedureName: product.procedureName,
            price: product.totalSalePrice,
            qty: 0,
            groupWO: false,
            serializeIndividually: false,
            dueDate: product.cycleTime ? moment().add(product.cycleTime, 'days').toDate() : this.globalDueDate,
          });
        });
        this.getPurchaseOrderFlag = true;
        this.getCustomerData(this.purchaseOrderData.customerId);
      }));
  }

  getCustomerData(id: number) {
    this.customerService.customerGet(id, null, null, null, null, null, null, true, null, null, null, null, null,null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.customerData = response.object[0] ? response.object[0] : {};
        this.getCustomerFlag = true;
      }));
  }

  getCustomerLabel(customer: CustomerModel): string {
    return `[MSR-FSR] ${customer.name} - [ID: ${customer.id}]`;
  }

  getLocationsData() {
    this.locationService.locationGet(null, null, null, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          if (x.parentId === null) {
            this.locationsData.push({ label: x.name, value: x.id });
          }
        });
        this.getLocationsFlag = true;
      }));
  }

  applyGlobalDueDate() {
    this.purchaseItems.forEach((item, index) => {
      this.purchaseItems[index].dueDate = this.globalDueDate;
    });
  }

  goToNextStep() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      if (this.step === 0) {
        this.purchaseItems = [];
        let valid = true;
        let sum: number = this.purchaseProducts.map(a => a.qty).reduce(function(a, b) {
          return a + b;
        });

        this.purchaseProducts.map(product => {
          if (sum < 1) { valid = false; }
          if (sum > 0 && product.qty > 0) {
            if (product.groupWO) {
              this.purchaseItems.push({
                id: product.id,
                productName: product.name,
                customerLineNumber: null,
                mttn: null,
                dueDate: product.dueDate,
                qty: product.qty,
                unitPrice: product.price,
                extPrice: product.qty * product.price,
                groupWO: product.groupWO,
                serializeIndividually: product.serializeIndividually,
                partName: product.partName,
                partNumber: product.partNumber,
                procedureName: product.procedureName,
              });
            } else {
              for (let i = 0; i < product.qty; i++) {
                this.purchaseItems.push({
                  id: product.id,
                  productName: product.name,
                  customerLineNumber: null,
                  mttn: null,
                  dueDate: product.dueDate,
                  qty: 1,
                  unitPrice: product.price,
                  extPrice: product.price,
                  groupWO: product.groupWO,
                  serializeIndividually: product.serializeIndividually,
                  partName: product.partName,
                  partNumber: product.partNumber,
                  procedureName: product.procedureName,
                });
              }
            }
          }
        });
        if (valid) { this.step = 1; }
      } else if (this.step === 1) {
        this.purchaseSerializeItems = [];
        this.purchaseItems.map(item => {
          if (!item.serializeIndividually) {
            this.purchaseSerializeItems.push({
              serialKitNo: null,
              locationId: null,
              ...item,
            });
          } else {
            for (let i = 0; i < item.qty; i++) {
              this.purchaseSerializeItems.push({
                serialKitNo: null,
                locationId:  null,
                ...item,
                qty: 1,
              });
            }
          }
        });
        this.step = 2;
      } else {

        length = this.purchaseSerializeItems.length;
        let rootItemIndex = 0;
        this.globals.showLoader(true);

        this.purchaseItems.forEach((purchaseItem) => {

          const rootItem = this.purchaseSerializeItems[rootItemIndex];

          const serialNumbers: string[] = [];
          let purchaseSerializeItemsCount = purchaseItem.serializeIndividually ? purchaseItem.qty : 1;

          for (let i = rootItemIndex; i < rootItemIndex + purchaseSerializeItemsCount; i++) {
            serialNumbers.push(this.purchaseSerializeItems[i].serialKitNo ? this.purchaseSerializeItems[i].serialKitNo : '');
          }

          const requestData = new CreatePurchaseRequest(
            {
              purchaseOrderId: this.purchaseOrderData.id,
              statusId: 1, // Approved
              purchaseOrderProductId: rootItem.id,
              serialNumbers: serialNumbers,
              locationId: rootItem.locationId,
              qty: purchaseItem.qty,
              customerLineNumber: rootItem.customerLineNumber ? parseInt(rootItem.customerLineNumber, 10) : null,
              mttn: rootItem.mttn,
              dueDate: rootItem.dueDate,
              purchasePrice: rootItem.unitPrice,
              serializeIndividually: rootItem.serializeIndividually,
            }
          );

          rootItemIndex += purchaseSerializeItemsCount;

          this.purchaseService.purchasePost(env.apiVersion, requestData)
          .pipe(take(1))
          .subscribe(responseHandler((resp) => {
            length -= purchaseItem.serializeIndividually ? purchaseItem.qty : 1;
            if (length === 0) {
              this.router.navigate(['app/pricing/purchaseorder']);
            }
          }));
        });
      }
    }
  }

  showSerialNumberModal() {
    this.serialNumberModal = true;
  }

  closeSerialNumberModal() {
    this.serialNumberModal = false;
  }

  generateArray(n: number): number[] {
    return Array(n);
  }

}
