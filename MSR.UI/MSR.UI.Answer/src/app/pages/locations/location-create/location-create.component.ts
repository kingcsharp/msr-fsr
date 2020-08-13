import { Component, OnInit } from '@angular/core';
import { LocationService, LocationModel, CreateLocationRequest, ICreateLocationRequest, UpdateLocationRequest, ILocationModel } from '../../../services/api.client.generated';
import { ActivatedRoute, Router } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { SelectItem } from 'primeng/api';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-location-create',
  templateUrl: './location-create.component.html',
  styleUrls: ['./location-create.component.scss']
})
export class LocationCreateComponent implements OnInit {

  locationToEdit: LocationModel;
  locationToEditId: number;
  parentLocationOptions: Array<LocationModel>;
  countryOptions: SelectItem[];
  selectedParentLocation: LocationModel;
  constructor(private locationService: LocationService, private route: ActivatedRoute, public globals: Globals, private router: Router) { }

  ngOnInit(): void {

    this.countryOptions = new LookUpItems().Countries();

    this.locationService.locationGet(null, null, env.apiVersion).subscribe(responseHandler((response) => {

      this.parentLocationOptions = response.object;

    }));

    this.route.queryParams.subscribe(params => {
      this.locationToEditId = params['id'] == null ? 0 : Number(params['id']);

      if (this.locationToEditId !== 0) {

        this.locationService.locationGet(null, this.locationToEditId, env.apiVersion).subscribe(responseHandler((response) => {

          this.locationToEdit = response.object[0];
          if (this.locationToEdit.parentId !== null) {

            this.selectedParentLocation = this.parentLocationOptions.find(s => s.id === this.locationToEdit.id);

          }

        }));

      } else {
        this.locationToEdit = new LocationModel();

      }

    });

  }

  updateLocation() {
    if (this.selectedParentLocation !== undefined) {
      this.locationToEdit.parentId = this.selectedParentLocation.id;
    }

    let updateLocationRequest = new UpdateLocationRequest();
    updateLocationRequest.locationId = this.locationToEdit.id;
    updateLocationRequest.address1 = this.locationToEdit.address1;
    updateLocationRequest.address2 = this.locationToEdit.address2;
    updateLocationRequest.city = this.locationToEdit.city;
    updateLocationRequest.country = this.locationToEdit.country;
    updateLocationRequest.internalAddress = this.locationToEdit.internalAddress;
    updateLocationRequest.invoiceClass = this.locationToEdit.invoiceClass;
    updateLocationRequest.name = this.locationToEdit.name;
    updateLocationRequest.parentId = this.locationToEdit.parentId;
    updateLocationRequest.phone = this.locationToEdit.phone;
    updateLocationRequest.postalCode = this.locationToEdit.postalCode;
    updateLocationRequest.state = this.locationToEdit.state;

    this.globals.showLoader(true);
    this.locationService.locationPatch(env.apiVersion, updateLocationRequest).subscribe(responseHandler((response) => {

    }));
  }

  saveLocation() {

    if (this.selectedParentLocation !== undefined) {
      this.locationToEdit.parentId = this.selectedParentLocation.id;
    }

    let createLocationRequest = new CreateLocationRequest();
    createLocationRequest.address1 = this.locationToEdit.address1;
    createLocationRequest.address2 = this.locationToEdit.address2;
    createLocationRequest.city = this.locationToEdit.city;
    createLocationRequest.country = this.locationToEdit.country;
    createLocationRequest.internalAddress = this.locationToEdit.internalAddress;
    createLocationRequest.invoiceClass = this.locationToEdit.invoiceClass;
    createLocationRequest.name = this.locationToEdit.name;
    createLocationRequest.parentId = this.locationToEdit.parentId;
    createLocationRequest.phone = this.locationToEdit.phone;
    createLocationRequest.postalCode = this.locationToEdit.postalCode;
    createLocationRequest.state = this.locationToEdit.state;

    this.globals.showLoader(true);
    this.locationService.locationPost(env.apiVersion, createLocationRequest).subscribe(responseHandler((response) => {

      this.locationToEdit.id = response.object.id;

    }));

  }

}
