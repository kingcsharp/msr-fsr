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
  }

}

interface IPurchaseModel {
  id: number;
  custRefNo: string;
  orderDescription: string;
  purchaseStatus: string;
  createdDate: Date;
  approvalStatus: string;
}

class PurchaseModel implements IPurchaseModel {
  id: number;
  custRefNo: string;
  orderDescription: string;
  purchaseStatus: string;
  createdDate: Date;
  approvalStatus: string;


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
    approvalStatus: 'approved'
  }),
  new PurchaseModel({
    id: 516482,
    custRefNo: 'PO1590779276',
    orderDescription: 'refcustponum1223334444',
    createdDate: new Date(),
    purchaseStatus: 'All',
    approvalStatus: 'approved'
  })
]
