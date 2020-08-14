import { Component, OnInit, ElementRef } from '@angular/core';
import { CustomerService, Customer, UserService, User } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  providers: [CustomerService]
})
export class CustomersComponent implements OnInit {
  users: Array<User>;
  data: Array<Customer>;
  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAddCustomer: boolean = false;
  canEditCustomer: boolean = false;
  canDeleteCustomer: boolean = false;
  customerToDelete: Customer;
  showConfirmDeleteDialog: boolean = false;
  approvalTables = EnumApprovalTables;
  menuItems = EnumMenuItem;


  constructor(private customerService: CustomerService, private userService: UserService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: false }),
      new ColumnsSaved({ id: 'customerNumber', label: 'Customer Number', visible: true }),
      new ColumnsSaved({ id: 'address', label: 'Address', visible: true }),
      new ColumnsSaved({ id: 'phone', label: 'Phone', visible: true }),
      new ColumnsSaved({ id: 'location', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'primaryContact', label: 'Primary Contact', visible: true }),
      new ColumnsSaved({ id: 'secondaryContact', label: 'Secondary Contact', visible: true }),
      new ColumnsSaved({ id: 'isActive', label: 'Is Active', visible: true }),
      new ColumnsSaved({ id: 'createdBy', label: 'Created By', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    this.canAddCustomer = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteCustomer = this.hasPrivilege(this.privileges.CanDelete);
    this.canEditCustomer = this.hasPrivilege(this.privileges.CanEdit);
    this.getCustomers();


  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.CustomersDepartments, privName);
  }

  getCustomers() {

    this.globals.showLoader(true);

    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {

      this.data = response.object;

      this.loading = false;

    }));

  }

  openConfirmDeleteDialog(customer: Customer) {

    this.customerToDelete = customer;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog(customer: Customer) {
    this.customerToDelete = null;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  deleteCustomer() {

    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    this.globals.showLoader(true);
    this.customerService.customerDelete(this.customerToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {

      this.data.length = 0;
      this.getCustomers();

    }));

  }

}
