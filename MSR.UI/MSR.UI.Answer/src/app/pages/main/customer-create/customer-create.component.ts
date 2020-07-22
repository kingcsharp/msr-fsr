import { Component, OnInit } from '@angular/core';
import { CustomerService } from '../../../services/api.client.generated';
import { ActivatedRoute } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-customer-create',
  templateUrl: './customer-create.component.html',
  styleUrls: ['./customer-create.component.scss']
})
export class CustomerCreateComponent implements OnInit {

  customer:any;
  constructor(private customerService: CustomerService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.customer.id = params['id'] == null ? 0 : params['id'];

      if (this.customer.id !== 0) {

          this.customerService.customerGet(this.customer.id,null,null,null,null,null,null,null,env.apiVersion).subscribe(responseHandler((response) => {
            
            console.log(response);

          }));

      }

    });

  }

}
