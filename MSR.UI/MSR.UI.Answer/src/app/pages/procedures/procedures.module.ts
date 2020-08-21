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
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CkeditorWrapperComponent } from '../../components/ckeditor-wrapper/ckeditor-wrapper.component';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'procedures', component: ProceduresComponent, pathMatch: 'full' },
  { path: 'proceduretypes', component: ProceduretypesComponent, pathMatch: 'full' },
  { path: 'proceduretemplates', component: TemplatesComponent, pathMatch: 'full' },
  { path: 'procedure-create', component: ProcedureComponent, pathMatch: 'full' },
  { path: 'procedure-edit', component: ProcedureComponent, pathMatch: 'full' },
  { path: 'proceduretype-create', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretype-edit', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretemplate-create', component: TemplateComponent, pathMatch: 'full' },
  { path: 'proceduretemplate-edit', component: TemplateComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [ProceduresComponent, TemplatesComponent, ProceduretypesComponent, ProcedureComponent, 
    ProceduretypeComponent, TemplateComponent, CkeditorWrapperComponent],
  imports: [
    CommonModule,
    RouterModule.forChild(routes),
    UtilsModule,
    DropdownModule,
    FormsModule,
    TableModule,
    NewWidgetModule,
    MultiSelectModule,
    DialogModule,
    CKEditorModule
  ]
})
export class ProceduresModule { static routes = routes;}
