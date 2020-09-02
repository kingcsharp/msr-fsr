// TODO: This is temp code, will remove when back-end APIs have done.
import {
  Customer,
  Procedure,
  PartModel,
  WorkOrderModel,
  ITrackableModel,

} from '../services/api.client.generated';

export interface IQuotesProductsView extends ITrackableModel {
    id?: number;
    isProduct?: boolean;
    isDeletable?: boolean;
    submittedDate?: Date;
    company?: string | undefined;
    submittedBy?: string | undefined;
    partKitNo?: string | undefined;
    procedureName?: string | undefined;
    productName?: string | undefined;
    revision?: number;
    equipmentCost?: number;
    materialCost?: number;
    salesTax?: number;
    totalPrice?: number;
    cycleTime?: number;
    representative: string;
}

export class QuotesProductsView implements IQuotesProductsView {
  id?: number;
  isProduct?: boolean;
  isDeletable?: boolean;
  submittedDate?: Date;
  company?: string | undefined;
  submittedBy?: string | undefined;
  partKitNo?: string | undefined;
  procedureName?: string | undefined;
  productName?: string | undefined;
  revision?: number;
  equipmentCost?: number;
  materialCost?: number;
  salesTax?: number;
  totalPrice?: number;
  cycleTime?: number;
  representative: string;

  constructor(data?: IQuotesProductsView) {
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
          this.isProduct = _data["isProduct"];
          this.isDeletable = _data["isDeletable"];
          this.submittedDate = _data["submittedDate"] ? new Date(_data["submittedDate"].toString()) : <any>undefined;
          this.company = _data["company"];
          this.submittedBy = _data["submittedBy"];
          this.partKitNo = _data["partKitNo"];
          this.procedureName = _data["procedureName"];
          this.productName = _data["productName"];
          this.revision = _data["revision"];
          this.equipmentCost = _data["equipmentCost"];
          this.materialCost = _data["materialCost"];
          this.salesTax = _data["salesTax"];
          this.totalPrice = _data["totalPrice"];
          this.cycleTime = _data["cycleTime"];
      }
  }

  static fromJS(data: any): QuotesProductsView {
      data = typeof data === 'object' ? data : {};
      let result = new QuotesProductsView();
      result.init(data);
      return result;
  }

  toJSON(data?: any) {
      data = typeof data === 'object' ? data : {};
      data["id"] = this.id;
      data["isProduct"] = this.isProduct;
      data["isDeletable"] = this.isDeletable;
      data["submittedDate"] = this.submittedDate ? this.submittedDate.toISOString() : <any>undefined;
      data["company"] = this.company;
      data["submittedBy"] = this.submittedBy;
      data["partKitNo"] = this.partKitNo;
      data["procedureName"] = this.procedureName;
      data["productName"] = this.productName;
      data["revision"] = this.revision;
      data["equipmentCost"] = this.equipmentCost;
      data["materialCost"] = this.materialCost;
      data["salesTax"] = this.salesTax;
      data["totalPrice"] = this.totalPrice;
      data["cycleTime"] = this.cycleTime;
      return data;
  }
}

export const quotesproductsData = [
  new QuotesProductsView({
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
    lastUpdatedOn: new Date(),
    lastUpdated: null,
    isDeletable: true
  }),
  new QuotesProductsView({
    id: 0,
    isProduct: true,
    submittedDate: new Date('04/27/2020'),
    company: '[MSR-FSR]INTEL F28 - [ID:1586]',
    submittedBy: 'Derek',
    partKitNo: '633014638',
    procedureName: 'REX SS KIT Cleaning',
    productName: '633014638 - REX Cu Small kit, 1272',
    representative:'Derek',
    revision: 1,
    equipmentCost: 415.25,
    materialCost: 500.12,
    totalPrice: 415.09,
    cycleTime: 2,
    lastUpdatedOn: new Date(),
  }),
]
