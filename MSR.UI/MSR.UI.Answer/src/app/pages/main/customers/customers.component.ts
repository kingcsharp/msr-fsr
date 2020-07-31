import { Component, OnInit, ElementRef } from '@angular/core';
import { CustomerService } from '../../../services/api.client.generated';
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

  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;
  data: any[] = [];
  constructor(private customerService: CustomerService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'name', visible: true }),
      new ColumnsSaved({ id: 'customernumber', label: 'customernumber', visible: true }),
      new ColumnsSaved({ id: 'address', label: 'address', visible: true }),
      new ColumnsSaved({ id: 'phone', label: 'phone', visible: true }),
      new ColumnsSaved({ id: 'location', label: 'location', visible: true }),
      new ColumnsSaved({ id: 'primarycontact', label: 'primarycontact', visible: true }),
      new ColumnsSaved({ id: 'secondarycontact', label: 'secondarycontact', visible: true }),
      new ColumnsSaved({ id: 'internaladdress', label: 'internaladdress', visible: true }),
      new ColumnsSaved({ id: 'isactive', label: 'isactive', visible: true }),
      new ColumnsSaved({ id: 'createdby', label: 'createdby', visible: true }),
      new ColumnsSaved({ id: 'createdon', label: 'createdon', visible: true })
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);
    this.getCustomers();


  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege('HelpPages', privilegeName);
  }

  getCustomers() {
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      console.log(response);
    }));
  }

}
