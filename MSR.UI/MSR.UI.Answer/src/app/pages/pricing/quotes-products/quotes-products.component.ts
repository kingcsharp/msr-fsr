import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { EnumMenuItem } from '../../../services/api.client.generated';
import { QuotesProductsView, quotesproductsData } from '../../../temp/mock-data';
import {ConfirmationService} from 'primeng/api';

@Component({
  selector: 'app-quotes-products',
  templateUrl: './quotes-products.component.html',
  styleUrls: ['./quotes-products.component.scss'],
  providers: [ConfirmationService]
})
export class QuotesProductsComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: QuotesProductsView[];
  userPrivileges: AllowedActions;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private confirmationService: ConfirmationService
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
    this.data = quotesproductsData;
  }

  onClickImportQuote($event) {
    //TODO: Import CSV process;
    console.log('Click Import Quote');
  }

  onClickDeleteQuote(quoteId: number) {
    this.confirmationService.confirm({
      message: 'Are you sure you want to Delete this Quote?',
      accept: () => {
        this.deleteQuote(quoteId);
      }
    });
  }

  deleteQuote(quoteId: number) {
    //TODO: Delete Quote API;
    const index = this.data.findIndex(x => x.id === quoteId);
    this.data.splice(index, 1);
  }

}
