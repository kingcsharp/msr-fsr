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
import { IPagingModel, PagingModel } from '../../../app/models/paging-model';
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
  pagingModel: PagingModel = new PagingModel({} as IPagingModel);
  @Input() calanderIsRange: boolean = false;

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
    if (this.isLazyLoad) {
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
      const resp = this.reportCubeService.generateChart(this.pagingModel.ChartInformation, this.pagingModel);
      if (this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
        this.regnerateCharOptions(resp.HighChartsOptions);
      }
      this.chartOptions = resp.HighChartsOptions;
      this.chartInfo = resp.ChartInformation;
      this.showCharts = true;
      this.globals.showLoader(false);
    }, 300);
  }

  getLazyLoadReport(lazyLoadEvent: LazyLoadEvent) {

    if (this.reportInfo.name !== '' && this.reportInfo.apiEndPointURL !== undefined) {

      if (lazyLoadEvent.first !== undefined && lazyLoadEvent.rows !== undefined && lazyLoadEvent.rows !== 0) {
        this.pagingModel.pageNumber = lazyLoadEvent.first / lazyLoadEvent.rows;
      }

      if (lazyLoadEvent.sortField !== undefined) {
        this.pagingModel.sortTerm = lazyLoadEvent.sortField;
        this.pagingModel.sortAscending = lazyLoadEvent.sortOrder === 1 ? true : false;
      }

      this.pagingModel.pageSize = lazyLoadEvent.rows;
      this.pagingModel.queryString = this.reportCubeService.primeNgFilterToQueryStringConverter(lazyLoadEvent.filters);

      this.getReport(this.data, this.reportInfo);
    } else {

      this.gridData = this.data;
      this.filteredData = this.data;
      this.getData.emit(lazyLoadEvent);

    }

  }

  getReport(data: any, reportInfo?: ReportModel) {
    if (reportInfo === undefined || reportInfo.apiEndPointURL === undefined) {
      this.gridData = data;
      this.filteredData = this.gridData;
    } else {
      this.globals.showLoader(true);
      this.reportCubeService.getReport(reportInfo, this.pagingModel).then((resp) => {
        if (resp.HighChartsOptions !== undefined) {
          this.hasChart = true;
          this.gridData = resp.data;
          if (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
            this.regnerateCharOptions(resp.HighChartsOptions);
          }
          this.chartOptions = resp.HighChartsOptions;
          this.chartInfo = resp.ChartInformation;
          this.showCharts = true;
        } else {
          this.gridData = resp.data;
        }
        this.filteredData = this.gridData;
        this.totalRecords = resp.totalRows;
        this.globals.showLoader(false);
      });
    }
  }

  regnerateCharOptions(chartOptions) {
    chartOptions.series.map(data => {
      for (let i = 1; i < data['data'].length; i++) {
        if (data['data'][i] === 0) {
          data['data'][i] = data['data'][i - 1];
        }
      }
      return data;
    });
  }

  printCsvReport() {

    this.globals.showLoader(true);
    this.reportCubeService.getReport(this.reportInfo, this.pagingModel, false).then((responsePagingModel) => {

      this.cSVConverterService.downloadFile(responsePagingModel.data, this.gridSaved.columnsSaved, this.reportInfo.name);
    });

  }

}
