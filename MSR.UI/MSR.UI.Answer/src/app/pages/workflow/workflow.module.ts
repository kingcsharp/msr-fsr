import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ApprovalWorkflowComponent } from './approval-workflow/approval-workflow.component';
import { RouterModule } from '@angular/router';
import { WidgetModule } from '../../layout/widget/widget.module';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { TrendModule } from 'ngx-trend';
import { UtilsModule } from '../../layout/utils/utils.module';
import { RickshawChartModule } from '../../components/rickshaw/rickshaw.module';
import { LiveTileModule } from '../../components/tile/tile.module';
import { FlotChartModule } from '../../components/flot/flot.module';
import { JqSparklineModule } from '../../components/sparkline/sparkline.module';
import { MapaelLayersMapModule } from '../../components/mapael/mapael.module';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { FormsModule } from '@angular/forms';
import { TextMaskModule } from 'angular2-text-mask';
import { InputSwitchModule } from 'primeng/inputswitch';
import { DialogModule } from 'primeng/dialog';
import { TableModule } from 'primeng/table';
import { DropdownModule } from 'primeng/dropdown';
import { CheckboxModule } from 'primeng/checkbox';
import { CalendarModule } from 'primeng/calendar';
import { MultiSelectModule } from 'primeng/multiselect';
import { PendingApprovalsComponent } from './pending-approvals/pending-approvals.component';
import { ApprovalGroupsComponent } from './approval-groups/approval-groups.component';
import { ApprovalStagesComponent } from './approval-stages/approval-stages.component';
import { PopoverModule } from 'ngx-bootstrap/popover';

export const routes = [
  { path: '', redirectTo: 'workflow', pathMatch: 'full' },
  { path: 'pendingapproval', redirectTo: 'pendingapproval/9', pathMatch: 'full' },
  { path: 'approvalworkflows', component: ApprovalWorkflowComponent, pathMatch: 'full' },
  { path: 'pendingapproval/:table', component: PendingApprovalsComponent, pathMatch: 'full' },
  { path: 'approvalgroups', component: ApprovalGroupsComponent, pathMatch: 'full' },
  { path: 'approvalstages', component: ApprovalStagesComponent, pathMatch: 'full' }
];

@NgModule({
  declarations: [
    PendingApprovalsComponent,
    ApprovalGroupsComponent,
    ApprovalStagesComponent,
    ApprovalWorkflowComponent
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    WidgetModule,
    ProgressbarModule.forRoot(),
    TrendModule,
    CheckboxModule,
    MultiSelectModule,
    BsDropdownModule.forRoot(),
    DropdownModule,
    TooltipModule.forRoot(),
    FormsModule,
    InputSwitchModule,
    TextMaskModule,
    DialogModule,
    TableModule,
    CalendarModule,
    UtilsModule,
    RickshawChartModule,
    LiveTileModule,
    WidgetModule,
    FlotChartModule,
    RickshawChartModule,
    JqSparklineModule,
    MapaelLayersMapModule,
    NewWidgetModule,
    PopoverModule.forRoot()
  ]
})
export class WorkflowModule {
  static routes = routes;
}
