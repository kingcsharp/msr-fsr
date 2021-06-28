import { Component, OnInit, ViewEncapsulation, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  Role, EnumMenuItem, ReportService, ReportModel, CustomerService
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable, Subscription } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../models/lib/Utils';
import { Subject } from 'rxjs';
import { CSVConverterService } from '../../services/csvconverter.service';
import { ReportCubeService } from './reportcube.service';

declare let jQuery: any;

@Component({
  selector: 'app-reports',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ReportComponent implements OnInit, OnDestroy {
  data: any;
  query: any = [];
  querySubject: any;
  repData: any;
  subscriptions: Subscription[] = [];
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService, private customerService: CustomerService,
    private elem: ElementRef, private reportService: ReportService, private cSVConverterService: CSVConverterService,
    private reportCubeService: ReportCubeService) {

  }

  ngOnInit(): void {
    const sub1 = this.reportService.report(true, env.apiVersion).subscribe(responseHandler(response => {
      this.data = response.object;
    }));
    this.subscriptions.push(sub1);
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  printCsvReport(reportId) {
    let reportInfo = this.data.filter(x => x.id === reportId)[0];
    const reportColumns = this.reportCubeService.getReportColumns(reportInfo);
    this.globals.showLoader(true);
    this.reportCubeService.getAllReportData(reportInfo).then((resp) => {
      
      this.cSVConverterService.downloadFile(resp, reportColumns, reportInfo.name);
      this.globals.showLoader(false);
    });
  }
}
