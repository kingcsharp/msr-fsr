import { Component, OnInit, ElementRef } from '@angular/core';
import { CustomerService, Customer, UserService, UserModel, EnumMenuItem, EnumApprovalTables, UpdateCustomerRequest } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-customers',
  templateUrl: './customers.component.html',
  styleUrls: ['./customers.component.scss'],
  providers: [CustomerService]
})
export class CustomersComponent implements OnInit {
  users: Array<UserModel>;
  data: Array<Customer>;
  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAddCustomer: boolean = false;
  canEditCustomer: boolean = false;
  canDeleteCustomer: boolean = false;
  canActivateCustomer: boolean = false;
  customerToDelete: Customer;
  showConfirmDeleteDialog: boolean = false;
  approvalTables = EnumApprovalTables;
  menuItems = EnumMenuItem;
  statusOptions: any[];

  constructor(private customerService: CustomerService, private userService: UserService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'customerNumber', label: 'Customer Number', visible: true }),
      new ColumnsSaved({ id: 'address', label: 'Address', visible: true }),
      new ColumnsSaved({ id: 'phone', label: 'Phone', visible: true }),
      new ColumnsSaved({ id: 'location.name', label: 'Location', visible: true }),
      new ColumnsSaved({ id: 'primaryContactUser.fullName', label: 'Primary Contact', visible: true }),
      new ColumnsSaved({ id: 'secondaryContactUser.fullName', label: 'Secondary Contact', visible: true }),
      new ColumnsSaved({ id: 'isActive', label: 'Is Active', visible: true }),
      new ColumnsSaved({ id: 'created.fullName', label: 'Created By', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    this.canAddCustomer = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteCustomer = this.hasPrivilege(this.privileges.CanDelete);
    this.canEditCustomer = this.hasPrivilege(this.privileges.CanEdit);
    this.canActivateCustomer = this.hasPrivilege(this.privileges.CanActivate);
    this.getCustomers();


  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.CustomersDepartments, privName);
  }

  getCustomers() {

    this.globals.showLoader(true);

    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {

      this.data = response.object;

      this.data.map((elem) => {
        if (elem.status === null) {
          elem.status = 'Approved';
        }

      });

      this.statusOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
      ).map(x => ({ label: x.status, value: x.status }));

      this.loading = false;

    }));

  }

  openConfirmDeleteDialog(customer: Customer) {

    this.customerToDelete = customer;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
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

  changeCustomerStatus(customer: Customer) {

    let updateCustomerRequest = new UpdateCustomerRequest();
    updateCustomerRequest.customerId = customer.id;
    updateCustomerRequest.address = customer.address;
    updateCustomerRequest.locationId = customer.location?.id;
    updateCustomerRequest.name = customer.name;
    updateCustomerRequest.phone = customer.phone;
    updateCustomerRequest.primaryContactUserId = customer.primaryContactUser?.id;
    updateCustomerRequest.secondaryContactUserId = customer.secondaryContactUser?.id;
    updateCustomerRequest.customerNumber = customer.customerNumber;
    updateCustomerRequest.isActive = customer.isActive;

    this.globals.showLoader(true);
    this.customerService.customerPatch(env.apiVersion, updateCustomerRequest).subscribe(responseHandler((response) => {

    }));

  }
}
