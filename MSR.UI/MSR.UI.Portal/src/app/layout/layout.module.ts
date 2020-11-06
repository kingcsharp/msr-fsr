import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ButtonsModule } from 'ngx-bootstrap/buttons';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { DropdownModule } from 'primeng/dropdown';
import { AlertModule } from 'ngx-bootstrap/alert';
import { ProgressbarModule } from 'ngx-bootstrap/progressbar';
import { UtilsModule } from '../layout/utils/utils.module';
import { ROUTES } from './layout.routes';
import { Layout } from './layout.component';
import { Sidebar } from './sidebar/sidebar.component';
import { Navbar } from './navbar/navbar.component';
import { BlockUIModule } from 'primeng/blockui';
import { DialogModule } from 'primeng/dialog';
import { ApprovalCommentComponent } from '../components/approval-comment/approval-comment.component';
import { PanelModule } from 'primeng/panel';
import { HelpbuttonWrapperComponent } from '../components/helpbutton-wrapper/helpbutton-wrapper.component';
import { InputSwitchModule } from 'primeng/inputswitch';

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
    DialogModule,
    DropdownModule,
    PanelModule,
    InputSwitchModule
  ],
  declarations: [Layout, Sidebar, Navbar, ApprovalCommentComponent, HelpbuttonWrapperComponent],
  exports: [HelpbuttonWrapperComponent]
})
export class LayoutModule {
}
