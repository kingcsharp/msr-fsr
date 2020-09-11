import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MultiSelectModule } from 'primeng/multiselect';
import { UtilsModule } from '../../layout/utils/utils.module';
import { WipComponent } from './wip/wip.component';
import { WiphistoryComponent } from './wiphistory/wiphistory.component';
import { WipstatusComponent } from './wipstatus/wipstatus.component';
import {TooltipModule} from 'primeng/tooltip';
import {WipstatusWrapperComponent} from '../../components/wipstatus-wrapper/wipstatus.component'


export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'wip', component: WipComponent, pathMatch: 'full' },
  { path: 'wiphistory', component: WiphistoryComponent, pathMatch: 'full' },
  { path: 'wipstatus', component: WipstatusComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [WipComponent, WiphistoryComponent, WipstatusComponent, WipstatusWrapperComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    MultiSelectModule,
    UtilsModule
  ]
})
export class WipModule { static routes = routes; }
