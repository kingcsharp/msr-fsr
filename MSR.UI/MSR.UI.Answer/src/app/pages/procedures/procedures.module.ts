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
import { ProcedureCreateComponent } from './procedure-create/procedure-create.component';
import { ProceduretypeComponent } from './proceduretype/proceduretype.component';
import { TemplateComponent } from './template/template.component';
import { CKEditorModule } from '@ckeditor/ckeditor5-angular';
import { CkeditorWrapperComponent } from '../../components/ckeditor-wrapper/ckeditor-wrapper.component';
import { ProcedureViewComponent } from './procedure-view/procedure-view.component';
import { ProcedureEditComponent } from './procedure-edit/procedure-edit.component';
import { SortableModule } from 'ngx-bootstrap/sortable';

export const routes = [
  { path: '', redirectTo: 'people', pathMatch: 'full' },
  { path: 'procedures', component: ProceduresComponent, pathMatch: 'full' },
  { path: 'proceduretypes', component: ProceduretypesComponent, pathMatch: 'full' },
  { path: 'proceduretemplates', component: TemplatesComponent, pathMatch: 'full' },
  { path: 'procedure-create', component: ProcedureCreateComponent, pathMatch: 'full' },
  { path: 'procedure-edit', component: ProcedureEditComponent, pathMatch: 'full' },
  { path: 'procedure-view', component: ProcedureViewComponent, pathMatch: 'full' },
  { path: 'proceduretype-create', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretype-edit', component: ProceduretypeComponent, pathMatch: 'full' },
  { path: 'proceduretemplate-create', component: TemplateComponent, pathMatch: 'full' },
  { path: 'proceduretemplate-edit', component: TemplateComponent, pathMatch: 'full' }
];


@NgModule({
  declarations: [ProceduresComponent, TemplatesComponent, ProceduretypesComponent, ProcedureCreateComponent,
    ProceduretypeComponent, TemplateComponent, CkeditorWrapperComponent, ProcedureViewComponent, ProcedureEditComponent],
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
    CKEditorModule,
    SortableModule
  ]
})
export class ProceduresModule { static routes = routes; }
