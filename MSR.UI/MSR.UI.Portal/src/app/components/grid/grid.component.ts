import { Component, OnInit, ViewEncapsulation, ElementRef, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  ReportService, ReportModel
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable, forkJoin, of } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../models/lib/Utils';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../models/enums/EnumColumnType';
import { GridSaved } from '../../models/lib/GridSaved';
import { ReportCubeService } from '../../pages/reports/reportcube.service';
import * as Highcharts from 'highcharts';
import { ChartInfo } from '../../../app/models/lib/ChartInfo';
import { CSVConverterService } from '../../services/csvconverter.service';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  @Input() gridSaved: GridSaved;
  @Input() totalRecords: number;
  @Input() showReport: boolean;
  @Input() showExportGrid: boolean;
  @Input() saveToLocalStorage: boolean;
  @Input() data;
  @Input() reportInfo: ReportModel;
  @Output() expandRowClick = new EventEmitter<any>();
  @Output() getData = new EventEmitter<any>();
  showCharts: boolean = false;
  hasChart: boolean = false;
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options;
  chartInfo: ChartInfo;
  gridData: any = [];
  privileges = EnumPrivilege;
  enumColumnType = EnumColumnType;
  calendarEn;
  filteredData: any;

  // expanded: boolean = false;
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
    private reportCubeService: ReportCubeService, private cSVConverterService: CSVConverterService) {

  }

  ngOnInit(): void {
    this.calendarEn = this.globals.getCalendarDefault();
    if (this.saveToLocalStorage === undefined) {
      this.saveToLocalStorage = true;
    }
    // this.getReport();
  }

  expandRow(expanded, row) {
    if (!expanded) {
      this.expandRowClick.emit(row);
    }
  }

  getGridData(event: LazyLoadEvent) {
    debugger;
    if (this.reportInfo === undefined || this.reportInfo.apiEndPointURL === undefined) {
      this.getData.emit(event);
      this.gridData = this.data;
    } else {
      this.globals.showLoader(true);
      this.reportCubeService.getReport(this.reportInfo).then((resp: any) => {
        if (resp.chartOptions !== undefined) {
          this.hasChart = true;
          this.gridData = resp.resultData;
          this.chartOptions = resp.chartOptions;
          this.chartInfo = resp.chartInfo;
          this.showCharts = true;
        } else {
          this.gridData = resp;
        }
        this.globals.showLoader(false);
      });
    }

  }

  // handleFilter(ev, filteredData) {
  //   this.filteredData = filteredData.filteredValue === null ? filteredData.value : filteredData.filteredValue;
  //   if (!this.hasChart) {
  //     return;
  //   }

  //   this.globals.showLoader(true);
  //   this.chartInfo.gridData = this.filteredData;

  //   this.showCharts = false;
  //   setTimeout(() => {
  //     const resp = this.reportCubeService.generateChart(this.chartInfo);
  //     this.chartOptions = resp.chartOptions;
  //     this.chartInfo = resp.chartInfo;
  //     this.showCharts = true;
  //     this.globals.showLoader(false);
  //   }, 300);
  // }

  printCsvReport() {
    this.cSVConverterService.downloadFile(this.gridData, this.gridSaved.columnsSaved, this.reportInfo.name);
  }

}
