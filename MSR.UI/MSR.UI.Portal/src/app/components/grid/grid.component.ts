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
  @Input() showReport: boolean;
  @Input() totalRecords: number;
  @Input() isLazyLoad: boolean;
  @Input() showExportGrid: boolean;
  @Input() saveToLocalStorage: boolean;
  @Input() data;
  @Input() reportInfo: ReportModel;
  @Output() expandRowClick = new EventEmitter<any>();
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

  @Output() getData = new EventEmitter<any>();


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
    if (!this.isLazyLoad) {
      this.getReport(this.data, this.reportInfo);
    }
  }

  expandRow(expanded, row) {
    if (!expanded) {
      this.expandRowClick.emit(row);
    }
  }

  handleFilter(ev, filteredData) {
    if(this.isLazyLoad){
      return;
    }

    this.filteredData = filteredData.filteredValue === null ? filteredData.value : filteredData.filteredValue;
    if (!this.hasChart) {
      return;
    }

    this.globals.showLoader(true);
    this.chartInfo.gridData = this.filteredData;

    this.showCharts = false;
    setTimeout(() => {
      const resp = this.reportCubeService.generateChart(this.chartInfo);
      if (this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
        this.regnerateCharOptions(resp.chartOptions);
      }
      this.chartOptions = resp.chartOptions;
      this.chartInfo = resp.chartInfo;
      this.showCharts = true;
      this.globals.showLoader(false);
    }, 300);
  }

  getLazyLoadReport(event: LazyLoadEvent) {
    this.gridData = this.data;
    this.filteredData = this.data;
    this.getData.emit(event);
  }

  getReport(data: any, reportInfo?: ReportModel) {
    if (reportInfo === undefined || reportInfo.apiEndPointURL === undefined) {
      this.gridData = data;
      this.filteredData = this.gridData;
    } else {
      this.globals.showLoader(true);
      this.reportCubeService.getReport(reportInfo).then((resp) => {
        if (resp.chartOptions !== undefined) {
          this.hasChart = true;
          this.gridData = resp.resultData;
          if (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
            this.regnerateCharOptions(resp.chartOptions);
          }
          this.chartOptions = resp.chartOptions;
          this.chartInfo = resp.chartInfo;
          this.showCharts = true;
        } else {
          this.gridData = resp;
        }
        this.filteredData = this.gridData;
        this.globals.showLoader(false);
      });
    }
  }

  regnerateCharOptions(chartOptions) {
    chartOptions.series.map(data => {
      let index, value;
      for (let i=data['data'].length-1; i >= 0; i--) {
        if (data['data'][i] !== 0) {
         index = i;
         value = data['data'][i];
         break;
        }
      }
      if (value) {
        for (let i = index; i < data['data'].length; i++) data['data'][i] = value;
      }
      return data;
    });
  }

  printCsvReport() {
    this.cSVConverterService.downloadFile(this.filteredData, this.gridSaved.columnsSaved, this.reportInfo.name);
  }

}
