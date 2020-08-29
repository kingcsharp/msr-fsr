import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AlertModule } from 'ngx-bootstrap/alert';
import { UtilsModule } from '../../layout/utils/utils.module';
import { TabsModule } from 'ngx-bootstrap/tabs';
import { AccordionModule } from 'ngx-bootstrap/accordion';
import { ModalModule } from 'ngx-bootstrap/modal';

import { WidgetModule } from '../../layout/widget/widget.module';
import { TypographyComponent } from './typography/typography.component';
import { ColorsComponent } from './colors/colors.component';
import { GridComponent } from './grid/grid.component';

export const routes = [
  { path: '', redirectTo: 'typography', pathMatch: 'full' },
  { path: 'typography', component: TypographyComponent },
  { path: 'colors', component: ColorsComponent },
  { path: 'grid', component: GridComponent },
];

@NgModule({
  declarations: [
    // Components / Directives/ Pipes
    TypographyComponent,
    ColorsComponent,
    GridComponent,
  ],
  imports: [
    CommonModule,
    FormsModule,
    RouterModule.forChild(routes),
    AlertModule.forRoot(),
    WidgetModule,
    UtilsModule,
    ModalModule,
    ButtonsModule.forRoot(),
    BsDropdownModule.forRoot(),
    TabsModule.forRoot(),
    AccordionModule.forRoot()
  ]
})
export class CoreElementsModule {
  static routes = routes;
}
