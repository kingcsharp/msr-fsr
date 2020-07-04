import { Component, OnInit } from '@angular/core';
import { RoleService, Role, MenuService } from '../../../../services/api.client.generated';
import { ListboxModule } from 'primeng/listbox';
import { SelectItem } from 'primeng/api/selectitem';
import {ButtonModule} from 'primeng/button';

@Component({
  selector: 'app-roleassignments',
  templateUrl: './roleassignments.template.html',
  styleUrls: ['./roleassignments.style.scss'],
  providers: [MenuService]
})
export class RoleassignmentsComponent implements OnInit {

  cities1: SelectItem[];

  cities2: City[];

  selectedCity1: City;

  selectedCity2: City;

  cities: SelectItem[];

  selectedCities: string[];

  constructor(private roleService:RoleService, private menuService:MenuService) {

    this.roleService.roleGet("1").subscribe(response => {
      console.log(response);

      this.menuService.menu("1").subscribe(response => {
        console.log(response);
      });
    });
    //SelectItem API with label-value pairs
    this.cities1 = [
      { label: 'Select City', value: null },
      { label: 'New York', value: { id: 1, name: 'New York', code: 'NY' } },
      { label: 'Rome', value: { id: 2, name: 'Rome', code: 'RM' } },
      { label: 'London', value: { id: 3, name: 'London', code: 'LDN' } },
      { label: 'Istanbul', value: { id: 4, name: 'Istanbul', code: 'IST' } },
      { label: 'Paris', value: { id: 5, name: 'Paris', code: 'PRS' } }
    ];

    //An array of cities
    this.cities2 = [
      { name: 'New York', code: 'NY' },
      { name: 'Rome', code: 'RM' },
      { name: 'London', code: 'LDN' },
      { name: 'Istanbul', code: 'IST' },
      { name: 'Paris', code: 'PRS' }
    ];

    this.cities = [
      { label: 'Select City', value: null },
      { label: 'New York', value: { id: 1, name: 'New York', code: 'NY' } },
      { label: 'Rome', value: { id: 2, name: 'Rome', code: 'RM' } },
      { label: 'London', value: { id: 3, name: 'London', code: 'LDN' } },
      { label: 'Istanbul', value: { id: 4, name: 'Istanbul', code: 'IST' } },
      { label: 'Paris', value: { id: 5, name: 'Paris', code: 'PRS' } }
    ];

  }

  ngOnInit(): void {
  }


}

export class City {
  id?: number;
  name: string;
  code: string;

}
