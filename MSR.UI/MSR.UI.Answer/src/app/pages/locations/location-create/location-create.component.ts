import { Component, OnInit } from '@angular/core';
import { LocationService, LocationModel } from '../../../services/api.client.generated';
import { ActivatedRoute } from '@angular/router';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { LookUpItems } from '../../../utils/lookup-items';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-location-create',
  templateUrl: './location-create.component.html',
  styleUrls: ['./location-create.component.scss']
})
export class LocationCreateComponent implements OnInit {

  locationToEdit: LocationModel = new LocationModel();
  parentLocationOptions: Array<LocationModel>;
  countryOptions: SelectItem[];
  constructor(private locationService: LocationService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.countryOptions = new LookUpItems().Countries();

    this.locationService.locationGet(null,env.apiVersion).subscribe(responseHandler((response) => {

      this.parentLocationOptions = response.object;

    }));

    this.route.queryParams.subscribe(params => {
      this.locationToEdit.id = params['id'] == null ? 0 : params['id'];

      if (this.locationToEdit.id !== 0) {

          this.locationService.locationGet(this.locationToEdit.id,env.apiVersion).subscribe(responseHandler((response) => {
            
            console.log(response);

          }));

      }

    });

  }

  updateLocation(){
    console.log(this.locationToEdit);
  }

  saveLocation(){
    console.log(this.locationToEdit);
  }

}
