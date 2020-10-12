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
import {
  QuoteService,
  QuotesProductsView,
  EnumMenuItem,
  CustomerService,
  Customer,
  CreateQuoteRequest,
} from '../../../services/api.client.generated';
import { CSRJsonModel, ProcessModel, PartModel } from '../../../models/csr-json-model';

declare let jQuery: any;

@Component({
  selector: 'app-quotes-products',
  templateUrl: './quotes-products.component.html',
  styleUrls: ['./quotes-products.component.scss'],
  providers: [QuoteService, CustomerService]
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
  showCSRDialog: boolean = false;
  CSRToCreate: CSRJsonModel;
  CSRShippingMethods: any[] = [
    {
      value: 'UPS',
      label: 'UPS',
    },
    {
      value: 'FEDEX',
      label: 'FEDEX',
    },
    {
      value: 'USPS',
      label: 'USPS',
    },
    {
      value: 'Freight',
      label: 'Freight',
    }
  ];
  customersData: Customer[] = [];
  getCustomersFlag: boolean = false;
  CSRCustomer: Customer;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private quoteService: QuoteService,
    private customerService: CustomerService,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'quotesGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'submittedDate', label: 'Submitted Date', visible: true }),
      new ColumnsSaved({ id: 'company', label: 'Company', visible: true }),
      new ColumnsSaved({ id: 'submittedBy.fullName', label: 'Submitted By', visible: true }),
      new ColumnsSaved({ id: 'divisionFab', label: 'Division/Fab #', visible: true }),
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
    this.getCustomers();
  }

  getQuotesProducts() {
    this.globals.showLoader(true);
    this.quoteService.product(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
      }));
  }

  getCustomers() {
    if (this.getCustomersFlag) {
      return this.customersData;
    }
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      this.customersData = response.object;
      this.getCustomersFlag = true;
    }));
  }

  onClickImportQuote($event) {
    // TODO: Import CSV process;
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

  addCSRProcess() {
    this.CSRToCreate.Process.push(new ProcessModel());
  }

  removeCSRProcess() {
    if (this.CSRToCreate.Process.length > 1) {
      this.CSRToCreate.Process.pop();
    }
  }

  addCSRPart() {
    this.CSRToCreate.Parts.push(new PartModel());
  }

  removeCSRPart() {
    if (this.CSRToCreate.Parts.length > 1) {
      this.CSRToCreate.Parts.pop();
    }
  }

  openCSRDialog() {
    this.CSRToCreate = new CSRJsonModel();
    this.CSRToCreate.SubmittedBy = this.globals.user.fullName;
    this.CSRToCreate.Process = [new ProcessModel()];
    this.CSRToCreate.Parts = [new PartModel()];
    this.CSRCustomer = null;
    this.showCSRDialog = true;
  }

  closeCSRDialog() {
    this.showCSRDialog = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  setCustomerId() {
    this.CSRToCreate.customerId = this.CSRCustomer.id;
  }

  onCSRSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      const requestData = new CreateQuoteRequest();
      requestData.customerId = this.CSRToCreate.customerId;
      requestData.customerRequirementJson = JSON.stringify(this.CSRToCreate);
      this.quoteService.quotePost(env.apiVersion, requestData)
        .pipe(take(1))
        .subscribe(responseHandler((resp) => {
          this.closeCSRDialog();
          this.getQuotesProducts();
      }));
    }
  }

}
