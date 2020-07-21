import { Component, OnInit, ElementRef } from '@angular/core';
import { LocationService, LocationModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements OnInit {

  data: Array<LocationModel>;

  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;

  constructor(private locationService: LocationService,private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'name', label: 'name', visible: true }),
      new ColumnsSaved({ id: 'address1', label: 'address1', visible: true }),
      new ColumnsSaved({ id: 'city', label: 'city', visible: true }),
      new ColumnsSaved({ id: 'state', label: 'state', visible: true }),
      new ColumnsSaved({ id: 'postalcode', label: 'postalcode', visible: true }),
      new ColumnsSaved({ id: 'country', label: 'country', visible: true }),
      new ColumnsSaved({ id: 'phone', label: 'phone', visible: true }),
      new ColumnsSaved({ id: 'parentId', label: 'parentid', visible: true }),
      new ColumnsSaved({ id: 'internaladdress', label: 'internaladdress', visible: true }),
      new ColumnsSaved({ id: 'createdon', label: 'createdon', visible: true }),
      new ColumnsSaved({ id: 'createdby', label: 'createdby', visible: true }),
      new ColumnsSaved({ id: 'timezone', label: 'timezone', visible: false }),
      new ColumnsSaved({ id: 'address2', label: 'address1', visible: false }),
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);
    this.getLocations();

    

  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege('HelpPages', privilegeName);
  }

  getLocations(){
    this.locationService.locationGet(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data = response.object;
      //console.log(this.data);
    }));
  }

}
