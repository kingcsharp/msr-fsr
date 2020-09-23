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
import { Globals } from './models/lib/globals';
import { NotificationService } from './layout/navbar/notification.service';
import { CommonGrid } from './models/lib/CommonGrid';
import { CSVConverterService } from '../app/services/csvconverter.service';
import { environment } from '../environments/environment';
import { CubejsClientModule } from '@cubejs-client/ngx';

// const cubejsOptions = {
//   token: environment.CUBEJS_API_TOKEN,
//   options: { apiUrl: environment.CUBEJS_API_URL }
// };
const cubejsOptions = {
  token: '9nyEf9X3gjVQqryBAYKcMSefrkCZ7m8bCHJSXeXCYsfhCqcRJt',
  options: { apiUrl: 'https://report-api.cmhworks.com/answer/dev/v1/workinprocess' }
  // options: { apiUrl: 'https://report-api.cmhworks.com/answer/dev/cubejs-api/v1' }
};

import * as $ from 'jquery';
import {
  UserService, AccountService, API_BASE_URL, WorkflowService, WorkflowGroupService, CustomerService,
  WorkflowStageService, LocationService, RoleService, WorkflowPendingApprovalService, PartService,
  FileService, InvoiceService, WorkOrderService, TimezoneService, PurchaseOrderService, ProductService, ReportService,
  SearchService, ProcedureService 
} from './services/api.client.generated';

const APP_PROVIDERS = [
  CheckAllService,
  LoginService,
  AppGuard,
  AppConfig,
  ResetpasswordService,
  Globals,
  CommonGrid,
  CSVConverterService,
  NotificationService
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
    }),
    CubejsClientModule.forRoot(cubejsOptions)
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
    WorkflowService,
    WorkflowGroupService,
    WorkflowStageService,
    WorkflowPendingApprovalService,
    PartService,
    FileService,
    UserService,
    InvoiceService,
    CustomerService,
    TimezoneService,
    WorkOrderService,
    {
      provide: API_BASE_URL,
      useValue: environment.url
    },
    LocationService,
    RoleService,
    PurchaseOrderService, CustomerService, ProductService, ReportService, SearchService,ProcedureService
  ]
})
// { //we have this bse url set in the app.config that's why we define as ''
//   provide: API_BASE_URL,
//   useValue: environment.url
// }
export class AppModule { }
