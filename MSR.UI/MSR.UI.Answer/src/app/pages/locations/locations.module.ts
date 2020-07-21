import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsComponent } from './locations/locations.component';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { GridOptionsComponent } from '../../components/grid-options/grid-options.component';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';

export const routes = [
  { path: '', redirectTo: 'locations', pathMatch: 'full' },
  { path: 'locations', component: LocationsComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [LocationsComponent, GridOptionsComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    TableModule,
    NewWidgetModule,
    DialogModule,
    MultiSelectModule
  ]
})
export class LocationsModule {
  static routes = routes;
 }
