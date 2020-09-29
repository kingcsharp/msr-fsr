import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  Role, EnumMenuItem, ReportService, ReportModel
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../models/lib/Utils';
import { Subject } from "rxjs";
import { CSVConverterService } from '../../services/csvconverter.service';
import { ReportCubeService } from './reportcube.service';

declare let jQuery: any;

@Component({
  selector: 'app-reports',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ReportComponent implements OnInit {
  data: any;
  query: any = [];
  querySubject: any;
  repData: any;
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private reportService: ReportService, private cSVConverterService: CSVConverterService,
    private reportCubeService: ReportCubeService) {

  }

  ngOnInit(): void {
    this.reportService.report(env.apiVersion).subscribe(responseHandler(response => {
      this.data = response.object;
    }));

  }

  printCsvReport(reportId) {
    let reportInfo = this.data.filter(x=>x.id === reportId)[0];
    const reportColumns = this.reportCubeService.getReportColumns(reportInfo);
    this.globals.showLoader(true);
    this.reportCubeService.getReport(reportInfo).then((resp) => {
      this.globals.showLoader(false);
      this.cSVConverterService.downloadFile(resp,
        reportColumns.map(x=>x.id), reportColumns.map(x=>x.label), reportInfo.name);
    });
    
  }

}