import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { UtilsModule } from '../layout/utils/utils.module';
import { ROUTES } from './layout.routes';
import { HelpbuttonWrapperComponent } from '../components/helpbutton-wrapper/helpbutton-wrapper.component';

import { Layout } from './layout.component';
import { Sidebar } from './sidebar/sidebar.component';
import { Navbar } from './navbar/navbar.component';
import { BlockUIModule } from 'primeng/blockui';
import { DialogModule } from 'primeng/dialog';
import { ApprovalCommentComponent } from '../components/approval-comment/approval-comment.component';
import { ProductSegregationService } from '../services/product-segregation.service';

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    ButtonsModule.forRoot(),
    BsDropdownModule.forRoot(),
    AlertModule.forRoot(),
    ProgressbarModule.forRoot(),
    BlockUIModule,
    UtilsModule,
    DialogModule
  ],
  declarations: [Layout, Sidebar, Navbar, ApprovalCommentComponent, HelpbuttonWrapperComponent],
  exports: [
    HelpbuttonWrapperComponent
  ],
  providers: [
    ProductSegregationService
  ]
})
export class LayoutModule {
}
