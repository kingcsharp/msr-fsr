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
    this.step++;
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
