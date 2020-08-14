import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationsComponent } from './locations/locations.component';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { GridOptionsComponent } from '../../components/grid-options/grid-options.component';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { LocationCreateComponent } from './location-create/location-create.component';
import { DropdownModule } from 'primeng/dropdown';
import { ApproveEntityComponent } from '../../components/aproove-entity/approve-entity.component';
import { CsvImportComponent } from '../../components/csv-import/csv-import.component';
import { CmhFileUploaderComponent } from '../../components/cmh-file-uploader/cmh-file-uploader.component';
import { HelpbuttonWrapperComponent } from '../../components/helpbutton-wrapper/helpbutton-wrapper.component';
export const routes = [
  { path: '', redirectTo: 'locations', pathMatch: 'full' },
  { path: 'locations', component: LocationsComponent, pathMatch: 'full' },
  { path: 'location-create', component: LocationCreateComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [
    LocationsComponent, 
    GridOptionsComponent, 
    LocationCreateComponent, 
    ApproveEntityComponent,
    CsvImportComponent, 
    CmhFileUploaderComponent,
    HelpbuttonWrapperComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    TableModule,
    NewWidgetModule,
    DialogModule,
    MultiSelectModule,
    FormsModule,
    DropdownModule
  ]
})
export class LocationsModule {
  static routes = routes;
}
