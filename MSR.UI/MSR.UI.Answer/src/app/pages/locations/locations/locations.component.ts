import { Component, OnInit, ElementRef } from '@angular/core';
import { LocationService, LocationModel, EnumMenuItem, EnumApprovalTables } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import { take } from 'rxjs/operators';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-locations',
  templateUrl: './locations.component.html',
  styleUrls: ['./locations.component.scss']
})
export class LocationsComponent implements OnInit {

  data: Array<any>;
  approvalTables = EnumApprovalTables;
  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridVersion: string;
  gridStorageId: string;
  locationToDelete: LocationModel;
  showConfirmDeleteDialog: boolean = false;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;
  canApproveLocation: boolean = false;
  menuItems = EnumMenuItem;
  statusOptions: any[];
  totalRecords: number = 0;
  currentEvent: LazyLoadEvent;

  constructor(private locationService: LocationService, public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'internalAddress', label: 'Internal Address', visible: true }),
      new ColumnsSaved({ id: 'created.fullName', label: 'Created By', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'address1', label: 'Address 1', visible: false }),
      new ColumnsSaved({ id: 'city', label: 'City', visible: false }),
      new ColumnsSaved({ id: 'state', label: 'State/Province', visible: false }),
      new ColumnsSaved({ id: 'postalcode', label: 'Postalcode', visible: false }),
      new ColumnsSaved({ id: 'country', label: 'Country', visible: false }),
      new ColumnsSaved({ id: 'phone', label: 'Phone', visible: false }),
      new ColumnsSaved({ id: 'parent.name', label: 'Parent', visible: false }),
      new ColumnsSaved({ id: 'timezone.description', label: 'Timezone', visible: false }),
      new ColumnsSaved({ id: 'address2', label: 'Address 2', visible: false }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanDelete);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);
    this.canApproveLocation = this.hasPrivilege(this.privileges.CanApprove);

    this.statusOptions = this.globals.getTopLevelStatus();

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  getLocations(event: LazyLoadEvent) {
    this.currentEvent = event;
    this.globals.showLoader(true);
    setTimeout(() => {
        callFunctionWithFilters(this.locationService, this.locationService.locationGet, event, this.globals.functionDic).pipe(take(1)).subscribe(responseHandler((response) => {
          this.totalRecords = response.totalNumberOfRecords;
          this.data = response.object;
          this.data.map((elem) => {
            elem.show = elem.status !== null;
          });

          this.data.map((elem) => {
            if (elem.status === null) {
              elem.status = 'Approved';
            }
          });

        }));
    }, 10);
  }

  getParentName(parentId): string {
    return this.data.filter(s => s.id === parentId)[0].name;
  }

  openConfirmDeleteDialog(location: LocationModel) {
    this.locationToDelete = location;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.locationToDelete = null;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  deleteLocation() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    this.globals.showLoader(true);
    this.locationService.locationDelete(this.locationToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      this.getLocations(this.currentEvent);
    }));

  }

  uploadLocationsData($event) {
    // TODO: locations csv file upload
  }

}
