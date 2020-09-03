import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { EnumProductPageModes } from '../../../models/enums/ProductPageModes';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { environment as env } from '../../../../environments/environment';
import { Observable } from 'rxjs';
import { QuoteService, QuotesProductsView, EnumMenuItem } from '../../../services/api.client.generated';

@Component({
  selector: 'app-quotes-products',
  templateUrl: './quotes-products.component.html',
  styleUrls: ['./quotes-products.component.scss'],
  providers: [QuoteService]
})
export class QuotesProductsComponent implements OnInit {
  productPageModes = EnumProductPageModes;
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  gridVersion: string;
  data: QuotesProductsView[] = [];
  userPrivileges: AllowedActions;
  showConfirmDeleteDialog: boolean;
  quoteToDelete: QuotesProductsView;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private quoteService: QuoteService,
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
    this.showConfirmDeleteDialog = false;
    this.data = [];
    this.getQuotesProducts();
  }

  getQuotesProducts() {
    this.globals.showLoader(true);
    this.quoteService.product(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  onClickImportQuote($event) {
    // TODO: Import CSV process;
    console.log('Click Import Quote');
  }

  openConfirmDeleteDialog(quote: QuotesProductsView) {
    this.quoteToDelete = quote;
    this.showConfirmDeleteDialog = true;
  }

  closeConfirmDeleteDialog() {
    this.quoteToDelete = null;
    this.showConfirmDeleteDialog = false;
  }

  deleteQuote() {
    this.showConfirmDeleteDialog = false;
    this.globals.showLoader(true);
    this.quoteService.quoteDelete(this.quoteToDelete.id, env.apiVersion)
      .subscribe(responseHandler((response) => {
        this.quoteToDelete = null;
        this.getQuotesProducts();
      }));
  }

}
