import { Component, OnInit, ElementRef } from '@angular/core';
import { LocationService, LocationModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem } from '../../../models/enums/privileges';
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
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'internalAddress', label: 'Internal Address', visible: true }),
      new ColumnsSaved({ id: 'createdBy', label: 'Created By', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'address1', label: 'Address 1', visible: false }),
      new ColumnsSaved({ id: 'city', label: 'City', visible: false }),
      new ColumnsSaved({ id: 'state', label: 'State/Province', visible: false }),
      new ColumnsSaved({ id: 'postalcode', label: 'Postalcode', visible: false }),
      new ColumnsSaved({ id: 'country', label: 'Country', visible: false }),
      new ColumnsSaved({ id: 'phone', label: 'Phone', visible: false }),
      new ColumnsSaved({ id: 'parentId', label: 'Parent', visible: false }),
      new ColumnsSaved({ id: 'timezone', label: 'Timezone', visible: false }),
      new ColumnsSaved({ id: 'address2', label: 'Address 2', visible: false }),
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);
    this.getLocations();

    

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  getLocations(){
    this.globals.showLoader(true);
    this.locationService.locationGet(null,null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data = response.object;
      this.loading = false;
    }));
  }

  getParentName(parentId):string{

    return this.data.filter(s => s.id == parentId)[0].name;

  }

  

}
