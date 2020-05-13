import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { FormsModule } from '@angular/forms';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { RouterModule, PreloadAllModules } from '@angular/router';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { ToastrModule } from 'ngx-toastr';


import { ROUTES } from './app.routes';
import { CheckAllService } from './layout/utils/directives/check-all.service';
import { AppComponent } from './app.component';
import { ErrorComponent } from './pages/error/error.component';
import { LoginService } from './pages/login/login.service';
import { ResetpasswordService } from './pages/resetpassword/resetpassword.service';
import { AppGuard } from './app.guard';
import { AppInterceptor } from './app.interceptor';
import { AppConfig } from './app.config';
import { CommonService } from './services/common';
import { Globals } from './models/lib/globals';
import { EnumPrivilege } from './models/enums/privileges';

import * as $ from 'jquery';
import { UserService, AccountService } from './services/api.client.generated';

const APP_PROVIDERS = [
  CheckAllService,
  LoginService,
  AppGuard,
  AppConfig,
  ResetpasswordService,
  CommonService,
  Globals
];

@NgModule({
  bootstrap: [AppComponent],
  declarations: [
    AppComponent,
    ErrorComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    FormsModule,
    HttpClientModule,
    ToastrModule.forRoot(),
    RouterModule.forRoot(ROUTES, {
      useHash: true,
      preloadingStrategy: PreloadAllModules
    })
  ],
  providers: [
    APP_PROVIDERS,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AppInterceptor,
      multi: true
    },
    Globals,
    AccountService,
    UserService
  ]
})
export class AppModule { }
