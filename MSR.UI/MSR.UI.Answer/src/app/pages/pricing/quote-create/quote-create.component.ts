import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';

@Component({
  selector: 'app-quote-create',
  templateUrl: './quote-create.component.html',
  styleUrls: ['./quote-create.component.scss']
})
export class QuoteCreateComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  data: QuoteModel;

  constructor(
    public globals: Globals
  ) { }

  ngOnInit(): void {
    this.data = new QuoteModel();
  }

}

interface IQuoteItemModel {
  itemNo: number;
  quantity: number;
  description: string;
  leadTime: number;
  customerPartNo: number;
  price: number;
  extension?: number;
}

class QuoteItemModel implements IQuoteItemModel {
  itemNo: number;
  quantity!: number;
  description!: string;
  leadTime!: number;
  customerPartNo!: number;
  price!: number;
  extension?: number | undefined;

  constructor(data?: IQuoteItemModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}

interface IQuoteModel {
  submittedDate: Date;
  customerId: number;
  contact?: string;
  delivery?: string;
  title: string;
  phone?: string;
  existingProcess: string;
  processDescription?: string;
  createdByName: string;
  titleSubmit: string;
  address?: string;
  quoteItems?: QuoteItemModel[];
}

class QuoteModel implements IQuoteModel {
  submittedDate!: Date;
  customerId!: number;
  contact?: string | undefined;
  delivery?: string | undefined;
  title!: string;
  phone?: string | undefined;
  existingProcess!: string;
  processDescription?: string | undefined;
  createdByName!: string | undefined;
  titleSubmit!: string | undefined;
  address?: string | undefined;
  quoteItems?: QuoteItemModel[] | undefined;

  constructor(data?: IQuoteModel) {
    if (data) {
      for (var property in data) {
        if (data.hasOwnProperty(property))
          (<any>this)[property] = (<any>data)[property];
      }
    }
  }
}
