import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
  QuoteService,
  CreateQuoteRequest,
  CreateQuoteItemRequest,
  CustomerService,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Router } from '@angular/router';

declare let jQuery: any;

@Component({
  selector: 'app-quote-create',
  templateUrl: './quote-create.component.html',
  styleUrls: ['./quote-create.component.scss'],
  providers: [QuoteService],
})
export class QuoteCreateComponent implements OnInit {
  data: CreateQuoteRequest;
  customersData: any[] = [];
  getCustomersFlag: boolean = false

  constructor(
    public globals: Globals,
    private quoteService: QuoteService,
    private customerService: CustomerService,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.initCreateQuoteRequestData();
    this.getCustomers();
  }

  initCreateQuoteRequestData() {
    this.data = new CreateQuoteRequest();
    this.data.quoteItems = [new CreateQuoteItemRequest()];
  }

  addQuoteItem() {
    this.data.quoteItems.push(new CreateQuoteItemRequest())
  }

  removeQuoteItem() {
    if (this.data.quoteItems.length > 1) {
      this.data.quoteItems.pop();
    }
  }

  submit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      this.data.quoteJson=JSON.stringify(this.data);
      this.quoteService.quotePost(env.apiVersion, this.data)
        .pipe(take(1))
        .subscribe(responseHandler((resp) => {
          this.globals.showLoader(true);
          if (!resp.hasErrors) {
            ctrl.router.navigate(['app/pricing/products']);
          }
      }));
    }
  }

  getCustomers() {
    const ctrl = this;
    if (ctrl.getCustomersFlag) {
      return ctrl.customersData;
    }
    this.globals.showLoader(true);
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      response.object.map((x) => {
        ctrl.customersData.push({ label: `[MSR-FSR] ${x.name} - [ID: ${x.id}]`, value: x.id });
      });
      ctrl.getCustomersFlag = true;
      this.globals.showLoader(false);
    }));
  }
}
