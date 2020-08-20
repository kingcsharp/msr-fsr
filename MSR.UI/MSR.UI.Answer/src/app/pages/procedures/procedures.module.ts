import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { TableModule } from 'primeng/table';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { DialogModule } from 'primeng/dialog';
import { MultiSelectModule } from 'primeng/multiselect';
import { FormsModule } from '@angular/forms';
import { DropdownModule } from 'primeng/dropdown';
import { UtilsModule } from '../../layout/utils/utils.module';
import { ProceduresComponent } from './procedures/procedures.component';
import { TemplatesComponent } from './templates/templates.component';
import { ProceduretypesComponent } from './proceduretypes/proceduretypes.component';
import { ProcedureComponent } from './procedure/procedure.component';
import { ProceduretypeComponent } from './proceduretype/proceduretype.component';
import { TemplateComponent } from './template/template.component';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'procedures', component: ProceduresComponent, pathMatch: 'full' },
  { path: 'proceduretypes', component: ProceduretypesComponent, pathMatch: 'full' },
  { path: 'proceduretemplates', component: TemplatesComponent, pathMatch: 'full' },
  { path: 'procedure-create', component: ProcedureComponent, pathMatch: 'full' },
  { path: 'procedure-edit', component: ProcedureComponent, pathMatch: 'full' },
  { path: 'proceduretype-create', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretype-edit', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretemplates-create', component: TemplateComponent, pathMatch: 'full' },
  { path: 'proceduretemplates-edit', component: TemplateComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [ProceduresComponent, TemplatesComponent, ProceduretypesComponent, ProcedureComponent, ProceduretypeComponent, TemplateComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    UtilsModule,
    DropdownModule,
    FormsModule,
    TableModule,
    NewWidgetModule,
    MultiSelectModule,
    DialogModule
  ]
})
export class ProceduresModule { static routes = routes;}
