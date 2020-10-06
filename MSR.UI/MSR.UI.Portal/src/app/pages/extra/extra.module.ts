import 'fullcalendar/dist/fullcalendar.js';
import 'jquery-ui/ui/draggable.js';
import 'magnific-popup/dist/jquery.magnific-popup.min.js';
import 'shufflejs/dist/shuffle.js';
import 'moment/moment.js';

import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule, CUSTOM_ELEMENTS_SCHEMA } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ModalModule } from 'ngx-bootstrap/modal';

import { CalendarComponent } from './calendar/calendar.component';
import { SearchResultsComponent } from './search-results/search-results.component';
import { TimeLineComponent } from './time-line/time-line.component';
import { GalleryComponent } from './gallery/gallery.component';

export const routes = [
  { path: '', redirectTo: 'calendar', pathMatch: 'full' },
  { path: 'calendar', component: CalendarComponent },
  { path: 'search', component: SearchResultsComponent },
  { path: 'timeline', component: TimeLineComponent },
  { path: 'gallery', component: GalleryComponent }
];

@NgModule({
  declarations: [
    // Components / Directives/ Pipes
    CalendarComponent,
    SearchResultsComponent,
    TimeLineComponent,
    GalleryComponent
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
    AlertModule.forRoot(),
    ModalModule,
    ButtonsModule.forRoot(),
    BsDropdownModule.forRoot()
  ],
  schemas: [CUSTOM_ELEMENTS_SCHEMA]
})
export class ExtraModule {
  static routes = routes;
}
