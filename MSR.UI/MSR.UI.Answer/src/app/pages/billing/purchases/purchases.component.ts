import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';

@Component({
  selector: 'app-purchases',
  templateUrl: './purchases.component.html',
  styleUrls: ['./purchases.component.scss']
})
export class PurchasesComponent implements OnInit {

  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: any;
  currentPurchase: PurchaseModel;
  display: boolean =  false;
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
  canCreate: boolean = false;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'quotesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'purchaseNumber', label: 'Purchase Number', visible: true }),
      new ColumnsSaved({ id: 'custRefNumber', label: 'Cust Ref Number', visible: true }),
      new ColumnsSaved({ id: 'orderDescription', label: 'Order Description', visible: true }),
      new ColumnsSaved({ id: 'purchaseStatus', label: 'Purchase Status', visible: true }),
      new ColumnsSaved({ id: 'createdDate', label: 'Created Date', visible: true })
    ];
    this.getPurchases();
    this.currentPurchase = null;
    this.canCreate = this.hasPrivilege(this.privileges.CanCreate);
  }

  getPurchases() {
    this.data = demoData;
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.QuotesProducts, privName);
  }

  onClickViewPurchase(purchase: PurchaseModel) {
    //TODO: view purchase detail

    this.currentPurchase = purchase;
    console.log('____', this.currentPurchase);
    this.display = true;
  }

  onClickCloseDialog() {
    this.display = false;
    this.currentPurchase = null;
  }
}

interface PurchaseItemModel {
  id: number;
  description: string;
  account: string;
  custLine: string;
  materialTransferTicketNo: string;
  dueDate: Date;
  qty: number;
  unitPrice: number;
  extPrice: number;
}

interface IPurchaseModel {
  id: number;
  custRefNo: string;
  orderDescription: string;
  purchaseStatus: string;
  createdDate: Date;
  approvalStatus: string;
  purchaseItems: PurchaseItemModel[];
}

class PurchaseModel implements IPurchaseModel {
  id: number;
  custRefNo: string;
  orderDescription: string;
  purchaseStatus: string;
  createdDate: Date;
  approvalStatus: string;
  purchaseItems: PurchaseItemModel[];


  constructor(data?: IPurchaseModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}


const demoData = [
  new PurchaseModel({
    id: 516481,
    custRefNo: 'PO1590779276',
    orderDescription: 'refcustponum1223334444',
    createdDate: new Date(),
    purchaseStatus: 'All',
    approvalStatus: 'approved',
    purchaseItems: [
      {
        id: 12301,
        description: 'description',
        account: 'account',
        custLine: 'line',
        materialTransferTicketNo: '12301',
        dueDate: new Date(),
        qty: 1,
        unitPrice: 123,
        extPrice: 123,
      }
    ]
  }),
  new PurchaseModel({
    id: 516482,
    custRefNo: 'PO1590779276',
    orderDescription: 'refcustponum1223334444',
    createdDate: new Date(),
    purchaseStatus: 'All',
    approvalStatus: 'approved',
    purchaseItems: [
      {
        id: 12301,
        description: 'description',
        account: 'account',
        custLine: 'line',
        materialTransferTicketNo: '12301',
        dueDate: new Date(),
        qty: 1,
        unitPrice: 123,
        extPrice: 123,
      }
    ]
  })
]
