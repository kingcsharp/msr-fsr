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

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {

  /**
    for template type templates will have as avaiable this data
    col: col,
    colData:itemData,
    colsSaved:gridSaved.columnsSaved,
    gridData:gridData
  */

  @Input() gridSaved: GridSaved;
  @Input() showReport: boolean;
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
  // expanded: boolean = false;
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
    private reportCubeService: ReportCubeService) {

  }

  ngOnInit(): void {
    if (this.saveToLocalStorage === undefined) {
      this.saveToLocalStorage = true;
    }
    this.getReport(this.data, this.reportInfo);
  }

  expandRow(expanded, row) {
    if (!expanded) {
      this.expandRowClick.emit(row);
    }
  }

  handleFilter(ev, filteredData) {
    if (!this.hasChart) {
      return;
    }

    this.globals.showLoader(true);
    let objFiltered = {};
    if (Object.keys(filteredData.filters).length > 0) {
      filteredData.filteredValue.forEach(element => {
        objFiltered[element.elemKey] = element;
      });
    } else {
      filteredData.value.forEach(element => {
        objFiltered[element.elemKey] = element;
      });
    }
    this.chartInfo.chartData = objFiltered;

    this.showCharts = false;
    setTimeout(() => {
      const resp = this.reportCubeService.generateChart(this.chartInfo);
      this.chartOptions = resp.chartOptions;
      this.chartInfo = resp.chartInfo;
      this.showCharts = true;
      this.globals.showLoader(false);
    }, 300);
  }

  getReport(data: any, reportInfo?: ReportModel) {
    if (reportInfo === undefined || reportInfo.apiEndPointURL === undefined) {
      this.gridData = data;
    } else {
      this.globals.showLoader(true);
      this.reportCubeService.getReport(reportInfo).then((resp) => {
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

}
