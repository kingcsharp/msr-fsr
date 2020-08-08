import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  InvoiceService, InvoiceModel, InvoiceItemModel, CustomerService, LocationService,
  CreateInvoiceRequest, EnumApprovalTables, Customer, LocationModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { AllowedActions } from '../../../models/lib/AllowedActions';

declare let jQuery: any;

@Component({
  selector: 'invoice',
  templateUrl: './invoice.component.html',
  styleUrls: ['./invoice.component.scss']
})
export class InvoiceComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  approvalTables = EnumApprovalTables;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  roles: any[];
  allRoles: any[] = [];
  canCreate: boolean = false;
  canActivateStages: boolean = false;
  canEditStages: boolean = false;
  display: boolean = false;
  currentInvoice: any;
  data: any;
  isKitStatus: any[];
  workflowGroups: any[] = [];
  getWorkflowGroupsDone: boolean = false;
  allParts: any[] = [];
  isActive: any[];
  uploadedFinished: boolean = false;
  showApproveButtons: boolean = true;
  userPrivileges: AllowedActions;
  customers: Array<Customer>;
  locations: Array<LocationModel>;

  constructor(private globals: Globals, private invoiceService: InvoiceService, public cg: CommonGrid,
    private elem: ElementRef, private toastr: ToastrService, private customerService: CustomerService, private locationService: LocationService) {

  }

  ngOnInit(): void {
    this.currentInvoice = this.getInvoice(undefined);
    this.gridStorageId = 'invoiceGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
    new ColumnsSaved({ id: 'customerName', label: 'Customer Name', visible: true }),
    new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
    new ColumnsSaved({ id: 'invoiceNumber', label: 'Invoice Number', visible: true }),
    new ColumnsSaved({ id: 'total', label: 'Amount', visible: true }),
    new ColumnsSaved({ id: 'invoiceDate', label: 'Due Date', visible: true }),
    new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
    new ColumnsSaved({ id: 'created.fullName', label: 'Created By', visible: false }),
    new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: false }),
    new ColumnsSaved({ id: 'lastUpdated.fullName', label: 'Updated By', visible: false })
    ];

    this.isKitStatus = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];
    this.isActive = [{ label: 'Yes', value: true },
    { label: 'No', value: false }];

    // this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.Invoices);
    var a = {
      canActivate: true,
      canCreate: true,
      canEdit: true,
      canDelete: true
    } as AllowedActions;

    this.userPrivileges = new AllowedActions(a);

    this.getInvoices();
    this.getLocations();
    this.getCustomers();
    this.data = [];
  }


  getCustomers() {
    this.customerService.customerGet(null, null, null, null, null, null, null, true, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.customers = response.object;
      }));
  }

  getLocations() {
    this.locationService.locationGet(null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.locations = response.object;
      }));
  }

  showDialog(invoice: InvoiceModel) {
    this.display = true;
    this.currentInvoice = this.getInvoice(invoice);
  }

  clseDialog() {
    this.display = false;
    jQuery('.parsleyjs').parsley().reset();
  }

  getInvoices() {
    this.globals.showLoader(true);
    this.invoiceService.invoiceGet(null, null, null, null, null, null, null, null, null, null, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  getInvoice(invoice: InvoiceModel) {
    if (invoice === undefined) {
      let ret = new CreateInvoiceRequest();
      ret.invoiceItems = [];
      return ret;
    }
    return invoice;

  }
}
