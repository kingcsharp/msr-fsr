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
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'friendlyURL', label: 'Friendly URL', visible: true }),
      new ColumnsSaved({ id: 'roles', label: 'Roles', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
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
      console.log(this.data);
    }));
  }

}
