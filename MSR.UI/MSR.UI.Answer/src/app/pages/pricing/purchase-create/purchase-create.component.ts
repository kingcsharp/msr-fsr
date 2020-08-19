import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-purchase-create',
  templateUrl: './purchase-create.component.html',
  styleUrls: ['./purchase-create.component.scss']
})
export class PurchaseCreateComponent implements OnInit {
  purchaseOrderData: PurchaseOrder;
  getPurchaseOrderFlag: boolean = false;
  purchaseProducts: any[] = [];
  purchaseItems: PurchaseItem[] = [];
  purchaseSerializeItems: any[] = [];
  siteData = [
    {
      label: 'Site 1',
      value: 1566
    },
    {
      label: 'Site 2',
      value: 1574
    }
  ];

  step: number;
  selectButtonOptions = [
    {
      label: 'ON',
      value: true
    },
    {
      label: 'OFF',
      value: false
    }
  ]

  constructor() { }

  ngOnInit(): void {
    this.step = 0;
    this.getPurchaseOrderData();
  }

  getPurchaseOrderData() {
    //TODO: API integration
    this.purchaseOrderData = mockPO;
    mockPO.products.map(item => {
      this.purchaseProducts.push({
        id: item.id,
        name: item.name,
        qty: 0,
        groupWO: false,
        serializeIndividually: false
      })
    });
    this.getPurchaseOrderFlag = true;
  }

  goToNextStep() {
    if (this.step === 0) {
      this.purchaseItems = [];
      this.purchaseProducts.map(product => {
        if (product.groupWO) {
          this.purchaseItems.push(new PurchaseItem({
            productName: product.name,
            custLineItem: null,
            materialTransferTicketNo: null,
            dueDate: null,
            qty: product.qty,
            unitPrice: product.price,
            extPrice: product.qty * product.price,
            groupWO: product.groupWO,
            serializeIndividually: product.serializeIndividually
          }));
        } else {
          for (let i=0; i<product.qty; i++) {
            this.purchaseItems.push(new PurchaseItem({
              productName: product.name,
              custLineItem: null,
              materialTransferTicketNo: null,
              dueDate: null,
              qty: 1,
              unitPrice: product.price,
              extPrice: product.price,
              groupWO: product.groupWO,
              serializeIndividually: product.serializeIndividually
            }));
          }
        }
      });
      this.step = 1;
    } else if (this.step === 1) {
      this.purchaseSerializeItems = [];
      this.purchaseItems.map(item => {
        if (item.serializeIndividually) {
          this.purchaseSerializeItems.push({
            custLineItem: item.custLineItem,
            serialKitNo: null,
            qty: item.qty,
            site: null,
            part: null,
            procedure: null
          });
        } else {
          for (let i=0; i<item.qty; i++) {
            this.purchaseSerializeItems.push({
              custLineItem: item.custLineItem,
              serialKitNo: null,
              qty: 1,
              site: null,
              part: null,
              procedure: null
            })
          }
        }
      });
      this.step = 2;
    }
  }

  generateArray(n: number): number[] {
    return Array(n);
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
  customerRef?: string;
  openDate: Date;
  closeDate?: Date;
  totalPurchaseLimit: number;
  unusedAmount: number;
  revision: number;
  status: 'Open' | 'Closed' | 'All';
  products: any[]
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
  customerRef?: string;
  openDate: Date;
  closeDate: Date;
  totalPurchaseLimit: number;
  unusedAmount: number;
  revision: number;
  status: 'Open' | 'Closed' | 'All';
  products: any[]


  constructor(data?: IPurchaseOrder) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}

interface IPurchaseItem {
  productName: string;
  custLineItem: string;
  materialTransferTicketNo: string;
  dueDate: Date;
  qty: number;
  unitPrice: number;
  extPrice: number;
  groupWO: boolean;
  serializeIndividually: boolean;
}

class PurchaseItem implements IPurchaseItem {
  productName: string;
  custLineItem: string;
  materialTransferTicketNo: string;
  dueDate: Date;
  qty: number;
  unitPrice: number;
  extPrice: number;
  groupWO: boolean;
  serializeIndividually: boolean;

  constructor(data?: IPurchaseItem) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}


const mockPO = new PurchaseOrder({
  id: 516481,
  name: 'PO1590779276',
  referencePo: 'refcustponum1223334444',
  invoicedBalance: 0,
  uninvoicedBalance: 0,
  balance: 34775,
  customerName: '[MSR-FSR] APPLIED MATERIALS - [ID:1566]',
  customerId: 1566,
  customerRef: null,
  openDate: new Date(),
  closeDate: null,
  totalPurchaseLimit: 1234567,
  unusedAmount: 1234567,
  revision: 2,
  status: 'All',
  products: [
    {
      id: 1272,
      name: '(INTEL F32 1272) Rex cd Kit 633016411 (R-1) [supplier: MSR-FSR]'
    },
    {
      id: 2127,
      name: '(INTEL F32 2127) Rex cd Kit 633016422 (R-1) [supplier: MSR-FSR]'
    }
  ]
})
