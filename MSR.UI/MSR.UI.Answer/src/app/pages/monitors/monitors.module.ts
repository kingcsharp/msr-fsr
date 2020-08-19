import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MonitorsComponent } from './monitors/monitors.component';
import { RouterModule } from '@angular/router';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'monitors', component: MonitorsComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class MonitorsModule { static routes = routes; }
