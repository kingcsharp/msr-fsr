import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { WipComponent } from './wip/wip.component';
import { WiphistoryComponent } from './wiphistory/wiphistory.component';
import { WipstatusComponent } from './wipstatus/wipstatus.component';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'wip', component: WipComponent, pathMatch: 'full' },
  { path: 'wiphistory', component: WiphistoryComponent, pathMatch: 'full' },
  { path: 'wipstatus', component: WipstatusComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [WipComponent, WiphistoryComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
  ]
})
export class WipModule { static routes = routes; }
