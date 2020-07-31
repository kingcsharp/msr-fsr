import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsComponent } from './locations/locations.component';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { LocationCreateComponent } from './location-create/location-create.component';
import { DropdownModule } from 'primeng/dropdown';
import { UtilsModule } from '../../../app/layout/utils/utils.module';

export const routes = [
  { path: '', redirectTo: 'locations', pathMatch: 'full' },
  { path: 'locations', component: LocationsComponent, pathMatch: 'full' },
  { path: 'location-create', component: LocationCreateComponent, pathMatch: 'full'}
];


@NgModule({
  declarations: [LocationsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    TableModule,
    NewWidgetModule,
    DialogModule,
    MultiSelectModule,
    FormsModule,
    DropdownModule,
    UtilsModule
  ]
})
export class LocationsModule {
  static routes = routes;
 }
