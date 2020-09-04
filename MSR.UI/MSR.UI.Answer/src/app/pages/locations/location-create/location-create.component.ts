import { Component, OnInit } from '@angular/core';
import {
  LocationService, LocationModel, CreateLocationRequest, ICreateLocationRequest,
  UpdateLocationRequest, ILocationModel, SensorModel, SensorService, TimeZoneModel, TimezoneService
} from '../../../services/api.client.generated';
import { ActivatedRoute, Router } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { SelectItem } from 'primeng/api';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-location-create',
  templateUrl: './location-create.component.html',
  styleUrls: ['./location-create.component.scss'],
  providers: [SensorService, TimezoneService]
})
export class LocationCreateComponent implements OnInit {

  locationToEdit: LocationModel;
  locationToEditId: number;
  parentLocationOptions: Array<SelectItem>;
  countryOptions: SelectItem[];
  selectedParentLocation: number;
  sensorOptions: SensorModel[] = new Array<SensorModel>();
  selectedSensors: SensorModel[] = new Array<SensorModel>();
  originalSensorsSelected: SensorModel[] = new Array<SensorModel>();
  timezonesAvailable: Array<SelectItem>;
  selectedTimezone: number;
  constructor(private locationService: LocationService, private sensorService: SensorService, private route: ActivatedRoute,
    public globals: Globals, private router: Router, private timezoneService: TimezoneService) { }

  ngOnInit(): void {

    this.countryOptions = new LookUpItems().Countries();

    this.getLocationForEditorCreate();

  }

  getAvailableParentLocations() {

    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, env.apiVersion).subscribe(responseHandler((response) => {

      if (this.locationToEdit !== undefined) {
        this.parentLocationOptions = response.object.filter(s => s.id !== this.locationToEdit.id).map(m => ({ label: m.name, value: m.id }))
          .sort((a, b) => a.label < b.label ? -1 : a.label > b.label ? 1 : 0);
      }

      this.timezoneService.timezone(env.apiVersion).subscribe(responseHandler((timezoneResponse) => {

        this.timezonesAvailable = timezoneResponse.object.map(s => ({ label: s.description, value: s.id }));

        if(this.locationToEdit?.timeZone !== undefined){
          this.selectedTimezone = this.locationToEdit.timeZone.id;
        }
      }));

    }));

  }

  getLocationForEditorCreate() {

    this.route.queryParams.subscribe(params => {
      this.locationToEditId = params['id'] == null ? 0 : Number(params['id']);


      if (this.locationToEditId !== 0) {

        this.globals.showLoader(true);
        this.locationService.locationGet(null, this.locationToEditId, env.apiVersion).subscribe(responseHandler((locationGetResponse) => {

          this.locationToEdit = locationGetResponse.object[0];

          if (this.locationToEdit.parentId !== this.locationToEdit.id) {

            this.selectedParentLocation = this.locationToEdit.parentId;
          }

          this.globals.showLoader(true);
          this.sensorService.sensor(null, this.locationToEdit.site, env.apiVersion).subscribe(responseHandler((locationResponse) => {

            this.selectedSensors = locationResponse.object;
            this.originalSensorsSelected = locationResponse.object;
          }));

        }));

      } else {
        this.locationToEdit = new LocationModel();

      }

      this.getAvailableParentLocations();

    });

  }

  updateLocation() {

    let updateLocationRequest = new UpdateLocationRequest();
    updateLocationRequest.locationId = this.locationToEditId;
    updateLocationRequest.address1 = this.locationToEdit.address1;
    updateLocationRequest.address2 = this.locationToEdit.address2;
    updateLocationRequest.city = this.locationToEdit.city;
    updateLocationRequest.country = this.locationToEdit.country;
    updateLocationRequest.internalAddress = this.locationToEdit.internalAddress;
    updateLocationRequest.invoiceClass = this.locationToEdit.invoiceClass;
    updateLocationRequest.name = this.locationToEdit.name;
    updateLocationRequest.parentId = this.selectedParentLocation;
    updateLocationRequest.phone = this.locationToEdit.phone;
    updateLocationRequest.postalCode = this.locationToEdit.postalCode;
    updateLocationRequest.state = this.locationToEdit.state;
    updateLocationRequest.timeZoneId = this.selectedTimezone === undefined || this.selectedTimezone === null ? null : this.selectedTimezone;
    this.globals.showLoader(true);
    this.locationService.locationPatch(env.apiVersion, updateLocationRequest).subscribe(responseHandler((response) => {

      let sensorsToAdd = new Array<SensorModel>();
      let sensorsToDelete = new Array<SensorModel>();

      if (this.selectedSensors.length !== 0) {

        this.originalSensorsSelected.forEach(originalSensor => {

          if (this.selectedSensors.find(s => s.id === originalSensor.id) === undefined) {
            sensorsToDelete.push(originalSensor);
          }

        });

        this.selectedSensors.forEach(selectedSensor => {

          if (this.originalSensorsSelected.find(s => s.id === selectedSensor.id) === undefined) {
            sensorsToAdd.push(selectedSensor);
          }

        });


        sensorsToDelete.forEach(sensorToDelete => {
          this.globals.showLoader(true);
          this.locationService.sensorDelete(this.locationToEditId, sensorToDelete.id, env.apiVersion).subscribe(responseHandler((sensorResponse) => {



          }));

        });

        sensorsToAdd.forEach(sensorToAdd => {
          this.globals.showLoader(true);
          this.locationService.sensorPost(this.locationToEditId, sensorToAdd.id, env.apiVersion).subscribe(responseHandler((sensorResponse) => {

          }));

        });



      }

      this.router.navigate(['app/locations/locations']);

    }));
  }

  saveLocation() {


    let createLocationRequest = new CreateLocationRequest();
    createLocationRequest.address1 = this.locationToEdit.address1;
    createLocationRequest.address2 = this.locationToEdit.address2;
    createLocationRequest.city = this.locationToEdit.city;
    createLocationRequest.country = this.locationToEdit.country;
    createLocationRequest.internalAddress = this.locationToEdit.internalAddress;
    createLocationRequest.invoiceClass = this.locationToEdit.invoiceClass;
    createLocationRequest.name = this.locationToEdit.name;
    createLocationRequest.parentId = this.selectedParentLocation;
    createLocationRequest.phone = this.locationToEdit.phone;
    createLocationRequest.postalCode = this.locationToEdit.postalCode;
    createLocationRequest.state = this.locationToEdit.state;
    createLocationRequest.timeZoneId = this.selectedTimezone === undefined || this.selectedTimezone === null ? null : this.selectedTimezone;
    this.globals.showLoader(true);
    this.locationService.locationPost(env.apiVersion, createLocationRequest).subscribe(responseHandler((response) => {

      this.locationToEditId = response.object.id;

      if (this.selectedSensors.length !== 0) {

        this.selectedSensors.forEach(sensor => {

          this.globals.showLoader(true);
          this.locationService.sensorPost(Number(this.locationToEditId), Number(sensor.id), env.apiVersion).subscribe(responseHandler((sensorPostResponse) => {

          }));

        });

      }


      this.router.navigate(['app/locations/locations']);

    }));

  }


  parentSelected($event) {

    if ($event.value === null) {
      this.selectedParentLocation = undefined;
    } else {
      this.sensorService.sensor(null, $event.value.site, env.apiVersion).subscribe(responseHandler((response) => {

        this.sensorOptions.length = 0;
        this.sensorOptions.push(...response.object);

      }));
    }
  }

}
