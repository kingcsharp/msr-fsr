import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { EnumMenuItem } from '../../../services/api.client.generated';

@Component({
  selector: 'app-quotes',
  templateUrl: './quotes.component.html',
  styleUrls: ['./quotes.component.scss']
})
export class QuotesComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: any;
  userPrivileges: AllowedActions;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'quotesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'submittedDate', label: 'Submitted Date', visible: true }),
      new ColumnsSaved({ id: 'company', label: 'Company', visible: true }),
      new ColumnsSaved({ id: 'submittedBy', label: 'Submitted By', visible: true }),
      new ColumnsSaved({ id: 'partKitNo', label: 'Part/Kit Number', visible: true }),
      new ColumnsSaved({ id: 'procedureName', label: 'Procedure Name', visible: true }),
      new ColumnsSaved({ id: 'productName', label: 'Product Name', visible: true }),
      new ColumnsSaved({ id: 'representative', label: 'Representative', visible: false }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'equipmentCost', label: 'Equipment Cost', visible: false }),
      new ColumnsSaved({ id: 'materialCost', label: 'Material Cost', visible: false }),
      new ColumnsSaved({ id: 'salesTax', label: 'Sales Tax', visible: false }),
      new ColumnsSaved({ id: 'totalPrice', label: 'Total Price', visible: false }),
      new ColumnsSaved({ id: 'cycleTime', label: 'Cycle Time', visible: false }),
      new ColumnsSaved({ id: 'lastUpdateOn', label: 'LastUpdateOn', visible: false }),
      new ColumnsSaved({ id: 'lastUpdatedBy', label: 'LastUpdated By', visible: false })
    ];
    this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.QuotesProducts);

    this.data = []
    this.getQuotesProducts();
  }

  getQuotesProducts() {
    this.globals.showLoader(false);
    this.data = demoData;
  }

  onClickImportQuote($event) {
    //TODO: Import CSV process;
    console.log('Click Import Quote');
  }

  onClickStartQuote(quoteId: number) {
    //TODO: Start Quote;
    console.log('Click Start Quote - ', quoteId);
  }

  onClickDeleteQuote(quoteId: number) {
    //TODO: Delete Quote;
    console.log('Click Delete Quote - ', quoteId);
  }

  onClickEditProduct(productId: number) {
    //TODO: Edit Product();
    console.log('Click Edit product - ', productId);
  }

  onClickViewProduct(productId: number) {
    //TODO: View Product
    console.log('Click Viwe product - ', productId);
  }

}

// TODO: This is temp code, will remove when back-end APIs have done.
interface IQuoteProduct {
  id: number;
  isProduct: boolean;
  submittedDate: Date;
  company?: string;
  submittedBy: string;
  partKitNo?: number | string;
  procedureName?: string;
  productName?: string;
  representative?: string
  revision: number;
  equipmentCost?: number;
  materialCost?: number;
  salesTax?: number;
  totalPrice?: number;
  cycleTime?: number;
  lastUpdateOn: Date;
  lastUpdatedBy: string;
 }

class QuoteProductModel implements IQuoteProduct {
  id: number;
  isProduct: boolean;
  submittedDate: Date;
  company?: string | undefined;
  submittedBy: string;
  partKitNo?: number | string | undefined;
  procedureName?: string | undefined;
  productName?: string | undefined;
  representative?: string | undefined
  revision: number;
  equipmentCost?: number | undefined;
  materialCost?: number | undefined;
  salesTax?: number | undefined;
  totalPrice?: number | undefined;
  cycleTime?: number | undefined;
  lastUpdateOn: Date;
  lastUpdatedBy: string;

  constructor(data?: IQuoteProduct) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }

  init(_data?: any) {
    if (_data) {
      this.id = _data["id"];
      this.isProduct = _data["isProduct"]
      this.submittedDate = _data["submittedDate"];
      this.company = _data["company"];
      this.submittedBy = _data["submittedBy"];
      this.partKitNo = _data["partKitNo"];
      this.procedureName = _data["procedureName"];
      this.productName = _data["productName"];
      this.representative = _data["representative"];
      this.revision = _data["revision"];
      this.equipmentCost = _data["equipmentCost"];
      this.materialCost = _data["equipmentCost"];
      this.salesTax = _data["salesTax"];
      this.totalPrice = _data["totalPrice"];
      this.cycleTime = _data["cycleTime"];
      this.lastUpdateOn = _data["lastUpdateOn"];
      this.lastUpdatedBy = _data["lastUpdatedBy"]
    }
  }

  static fromJS(data: any): QuoteProductModel {
      data = typeof data === 'object' ? data : {};
      let result = new QuoteProductModel();
      result.init(data);
      return result;
  }

  toJSON(data?: any) {
    data = typeof data === 'object' ? data : {};
    data["id"] = this.id;
    data["isProduct"] = this.isProduct;
    data["submittedDate"] = this.submittedDate;
    data["company"] = this.company;
    data["submittedBy"] = this.submittedBy;
    data["partKitNo"] = this.partKitNo;
    data["procedureName"] = this.procedureName;
    data["productName"] = this.productName;
    data["representative"] = this.representative;
    data["revision"] = this.revision;
    data["equipmentCost"] = this.equipmentCost;
    data["materialCost"] = this.materialCost;
    data["salesTax"] = this.salesTax;
    data["totalPrice"] = this.totalPrice;
    data["cycleTime"] = this.cycleTime;
    data["lastUpdateOn"] = this.lastUpdateOn;
    data["lastUpdatedBy"] = this.lastUpdatedBy;
    return data;
  }
}

const demoData = [
  new QuoteProductModel({
    id: 0,
    isProduct: false,
    submittedDate: new Date('05/12/2020'),
    company: null,
    submittedBy: 'Derek',
    partKitNo: null,
    procedureName: null,
    productName: 'Test_Part_Alpha',
    representative:'Derek',
    revision: 0,
    equipmentCost: null,
    materialCost: null,
    totalPrice: null,
    cycleTime: null,
    lastUpdateOn: new Date(),
    lastUpdatedBy: 'Derek'
  }),
  new QuoteProductModel({
    id: 0,
    isProduct: true,
    submittedDate: new Date('04/27/2020'),
    company: '[MSR-FSR]INTEL F28 - [ID:1586]',
    submittedBy: 'Derek',
    partKitNo: 633014638,
    procedureName: 'REX SS KIT Cleaning',
    productName: '633014638 - REX Cu Small kit, 1272',
    representative:'Derek',
    revision: 1,
    equipmentCost: 415.25,
    materialCost: 500.12,
    totalPrice: 415.09,
    cycleTime: 2,
    lastUpdateOn: new Date(),
    lastUpdatedBy: 'Derek'
  }),
]
