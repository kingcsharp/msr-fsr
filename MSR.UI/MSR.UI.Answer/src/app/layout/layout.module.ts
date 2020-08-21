import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { TooltipModule } from 'ngx-bootstrap/tooltip';
import { ROUTES } from './layout.routes';

import { Layout } from './layout.component';
import { Sidebar } from './sidebar/sidebar.component';
import { Navbar } from './navbar/navbar.component';
import { BlockUIModule } from 'primeng/blockui';
import { DialogModule } from 'primeng/dialog';
import { ApprovalCommentComponent } from '../components/approval-comment/approval-comment.component';

@NgModule({
  imports: [
    CommonModule,
    ROUTES,
    FormsModule,
    ButtonsModule.forRoot(),
    BsDropdownModule.forRoot(),
    AlertModule.forRoot(),
    ProgressbarModule.forRoot(),
    TooltipModule.forRoot(),
    BlockUIModule,
    DialogModule
  ],
  declarations: [Layout, Sidebar, Navbar, ApprovalCommentComponent]
})
export class LayoutModule {
}
