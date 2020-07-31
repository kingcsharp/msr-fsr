import { Component, OnInit } from '@angular/core';
import { CustomerService, Customer, CreateCustomerRequest, ICustomer } from '../../../services/api.client.generated';
import { ActivatedRoute } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.scss'],
  providers: [CustomerService]
})
export class CustomerCreateComponent implements OnInit {

  customer: Customer = null;
  customerToEditId: number = 0;
  constructor(private customerService: CustomerService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.customerToEditId = params['id'] == null ? 0 : params['id'];

      if (this.customerToEditId !== 0) {

        this.customerService.customerGet(this.customerToEditId, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {

          console.log(response);

        }));

      } else {

        this.customer = new Customer();
        console.log(this.customer);

      }

    });

  }

}
