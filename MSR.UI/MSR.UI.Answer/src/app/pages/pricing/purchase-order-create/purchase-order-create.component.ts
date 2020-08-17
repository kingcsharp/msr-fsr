import { Component, OnInit } from '@angular/core';
import {
  CustomerService
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-purchase-order-create',
  templateUrl: './purchase-order-create.component.html',
  styleUrls: ['./purchase-order-create.component.scss'],
  providers: [CustomerService]
})
export class PurchaseOrderCreateComponent implements OnInit {
  customersData: any[] = [];
  getCustomersFlag: boolean = false;

  constructor(
    private customerService: CustomerService,
  ) { }

  ngOnInit(): void {
    this.getCustomers();
  }

  getCustomers() {
    const ctrl = this;
    if (ctrl.getCustomersFlag) {
      return ctrl.customersData;
    }
    // this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
    //   response.object.map((x) => {
    //     ctrl.customersData.push({ label: `[MSR-FSR] ${x.name} - [ID: ${x.id}]`, value: x.id });
    //   });
    //   ctrl.getCustomersFlag = true;
    // }));
    ctrl.customersData = [
      {
        label: '[MSR-FSR] APPLIED MATERIALS - [ID:1566]',
        value: 1566
      },
      {
        label: '[MSR-FSR]ATA - [ID:1574]',
        value: 1574
      }
    ];
    ctrl.getCustomersFlag = true;
  }

}
