import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';

import { ResetpasswordComponent } from './resetpassword.component';
import { NewWidgetModule } from '../../layout/new-widget/widget.module';
import { AlertModule } from 'ngx-bootstrap/alert';

export const routes = [
    { path: '', component: ResetpasswordComponent, pathMatch: 'full' }
];

@NgModule({
    declarations: [
        ResetpasswordComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild(routes),
        NewWidgetModule,
        AlertModule.forRoot()
    ]
})
export class ResetPasswordModule {
    static routes = routes;
}
