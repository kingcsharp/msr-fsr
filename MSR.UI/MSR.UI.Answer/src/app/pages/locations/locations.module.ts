import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsComponent } from './locations/locations.component';
import { RouterModule } from '@angular/router';

export const routes = [
  { path: '', redirectTo: 'locations', pathMatch: 'full' },
  { path: 'locations', component: LocationsComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [LocationsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class LocationsModule {
  static routes = routes;
 }
