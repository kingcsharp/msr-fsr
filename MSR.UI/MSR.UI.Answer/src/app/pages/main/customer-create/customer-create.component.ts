import { Component, OnInit } from '@angular/core';
import { CustomerService, Customer, CreateCustomerRequest, UpdateCustomerRequest, ICustomer, UserService, User, LocationService, LocationModel } from '../../../services/api.client.generated';
import { ActivatedRoute, Router } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { SelectItem } from 'primeng/api';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.scss'],
  providers: [CustomerService]
})
export class CustomerCreateComponent implements OnInit {

  customer: Customer = null;
  customerToEditId: number = null;
  allUsers: Array<SelectItem>;
  locationOptions: Array<LocationModel>;
  selectedLocation: LocationModel;
  selectedPrimaryContactId: number = null;
  selectedSecondaryContactId: number = null;

  constructor(private customerService: CustomerService, public globals: Globals, private userService: UserService,
    private locationService: LocationService, private route: ActivatedRoute, private router: Router) { }

  ngOnInit(): void {

    this.globals.showLoader(true);
    this.userService.userGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {

      this.allUsers = response.object.map(s => ({ label: s.fullName, value: s.id }));

      this.globals.showLoader(true);
      this.locationService.locationGet(null, null, env.apiVersion).subscribe(responseHandler((locationResponse) => {

        this.locationOptions = locationResponse.object;

        this.getCustomer();

      }));

    }));

  }

  getCustomer() {

    this.route.queryParams.subscribe(params => {
      this.customerToEditId = params['id'] == null ? 0 : Number(params['id']);

      if (this.customerToEditId !== 0) {

        this.globals.showLoader(true);
        this.customerService.customerGet(this.customerToEditId, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {

          this.customer = response.object[0];
          if (this.customer.location !== undefined && this.customer.location.id !== 0) {
            this.selectedLocation = this.locationOptions.find(s => s.id === this.customer.location.id);
          }

          if (this.customer.primaryContactUser !== undefined && this.customer.primaryContactUser.id !== 0) {
            this.selectedPrimaryContactId = this.customer.primaryContactUser.id;
          }

          if (this.customer.secondaryContactUser !== undefined && this.customer.secondaryContactUser.id !== 0) {
            this.selectedSecondaryContactId = this.customer.secondaryContactUser.id;
          }

        }));

      } else {

        this.customer = new Customer();
        this.customer.id = 0;

      }

    });

  }

  saveCustomer() {

    let createCustomerRequest = new CreateCustomerRequest();
    createCustomerRequest.address = this.customer.address;
    createCustomerRequest.locationId = this.selectedLocation == null ? null : this.selectedLocation.id;
    createCustomerRequest.name = this.customer.name;
    createCustomerRequest.phone = this.customer.phone;
    createCustomerRequest.primaryContactUserId = this.selectedPrimaryContactId;
    createCustomerRequest.secondaryContactUserId = this.selectedSecondaryContactId;
    createCustomerRequest.customerNumber = this.customer.customerNumber;

    this.globals.showLoader(true);
    this.customerService.customerPost(env.apiVersion, createCustomerRequest).subscribe(responseHandler((response) => {
      this.customer.id = response.object.id;
      this.router.navigate(['app/people/customers']);
    }));

  }

  updateCustomer() {

    let updateCustomerRequest = new UpdateCustomerRequest();
    updateCustomerRequest.customerId = this.customer.id;
    updateCustomerRequest.address = this.customer.address;
    updateCustomerRequest.locationId = this.selectedLocation == null ? null : this.selectedLocation.id;
    updateCustomerRequest.name = this.customer.name;
    updateCustomerRequest.phone = this.customer.phone;
    updateCustomerRequest.primaryContactUserId = this.selectedPrimaryContactId;
    updateCustomerRequest.secondaryContactUserId = this.selectedSecondaryContactId;
    updateCustomerRequest.customerNumber = this.customer.customerNumber;

    this.globals.showLoader(true);
    this.customerService.customerPatch(env.apiVersion, updateCustomerRequest).subscribe(responseHandler((response) => {
      this.router.navigate(['app/people/customers']);
    }));

  }

}
