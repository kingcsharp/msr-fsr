import { Component, OnInit, ViewChild, ElementRef, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  ReportModel, DocumentService, AuditActionResultOfICollectionOfArchiveDocumentView, ArchiveDocumentView
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { formatBytes } from '../../models/lib/Utils';
import { EnumColumnType } from '../../models/enums/EnumColumnType';
import { GridSaved } from '../../models/lib/GridSaved';
import { ReportCubeService } from '../../pages/reports/reportcube.service';
import * as Highcharts from 'highcharts';
import { ChartInfo } from '../../../app/models/lib/ChartInfo';
import { CSVConverterService } from '../../services/csvconverter.service';
import { LocaleSettings } from 'primeng/calendar';
import { IPagingModel, PagingModel } from '../../models/paging-model';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-grid',
  templateUrl: './grid.component.html',
  styleUrls: ['./grid.component.scss']
})
export class GridComponent implements OnInit {
  @Input() gridSaved: GridSaved;
  @Input() showReport: boolean;
  @Input() showExportGrid: boolean;
  @Input() saveToLocalStorage: boolean;
  @Input() data;
  @Input() reportInfo: ReportModel;
  @Input() calanderIsRange: boolean = false;
  @Output() expandRowClick = new EventEmitter<any>();
  @ViewChild('downlodInfo') downlodInfo: ElementRef;
  @Input() staticOptions: any;
  @Input() partOptions: any;

  showCharts: boolean = false;
  hasChart: boolean = false;
  Highcharts: typeof Highcharts = Highcharts;
  chartOptions: Highcharts.Options;
  chartInfo: ChartInfo;
  gridData: any = [];
  privileges = EnumPrivilege;
  enumColumnType = EnumColumnType;
  calendarLocalSettings: LocaleSettings;
  filteredData: any;
  showArchiveDialogue: boolean = false;
  archivedGridData: ArchiveDocumentView[] = [];
  archiveDocsGrid: GridSaved;
  reportArchiveModel: ReportModel;

  pagingModel: PagingModel = new PagingModel({} as IPagingModel);
  totalRows: number = 0;
  completedOrCancelledStatuses: Array<any>;
  waitingToStartOrInProgressStatuses: Array<any>;
  constructor(public globals: Globals, public cg: CommonGrid, private reportCubeService: ReportCubeService, 
    private cSVConverterService: CSVConverterService,
    private documentService: DocumentService) {

  }

  ngOnInit(): void {
    this.calendarLocalSettings = this.globals.getCalendarDefault();
    if (this.saveToLocalStorage === undefined) {
      this.saveToLocalStorage = true;
    }

    this.completedOrCancelledStatuses = [
      { status: 'Completed' },
      { status: 'Cancelled' }
    ];

    this.waitingToStartOrInProgressStatuses = [
      { status: 'Waiting to Start' },
      { status: 'In Progress' }
    ];

    if(this.reportInfo.name === 'Work In Process'){
      this.waitingToStartOrInProgressStatuses.map(status => {
        this.staticOptions.push(status);
      })
    } else if(this.reportInfo.name === 'Combined Financial Data'){
      this.completedOrCancelledStatuses.map(status => {
        this.staticOptions.push(status);
      });
    }
  }

  expandRow(expanded, row) {
    if (!expanded) {
      this.expandRowClick.emit(row);
    }
  }

  getData(lazyLoadEvent: LazyLoadEvent){

    if(lazyLoadEvent.first !== undefined && lazyLoadEvent.rows !== undefined && lazyLoadEvent.rows !== 0){
      this.pagingModel.pageNumber = lazyLoadEvent.first / lazyLoadEvent.rows;
    }

    if(lazyLoadEvent.sortField !== undefined){
      this.pagingModel.sortTerm = lazyLoadEvent.sortField;
      this.pagingModel.sortAscending = lazyLoadEvent.sortOrder === 1 ? true : false;
    }

    this.pagingModel.pageSize = lazyLoadEvent.rows;
    this.pagingModel.queryString = this.reportCubeService.primeNgFilterToQueryStringConverter(lazyLoadEvent.filters);
    this.getReport(this.data, this.reportInfo);

  }

  updateChartWhenGridFiltersChange(){

    this.globals.showLoader(true);
    this.chartInfo.gridData = this.filteredData;

    this.showCharts = false;
    setTimeout(() => {
      const pagingModel = this.reportCubeService.getResultDataAndChart(this.chartInfo,this.pagingModel);
      if (this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
        this.regnerateCharOptions(pagingModel.HighChartsOptions);
      }
      this.chartOptions = pagingModel.HighChartsOptions;
      this.chartInfo = pagingModel.ChartInformation;
      this.showCharts = true;
      this.globals.showLoader(false);
    }, 300);

  }

  getReport(data: any, reportInfo?: ReportModel) {
    this.showCharts = false;
    if (reportInfo === undefined || reportInfo.apiEndPointURL === undefined) {
      this.gridData = data;
      this.filteredData = this.gridData;
    } else {
      this.globals.showLoader(true);

      this.reportCubeService.getReport(reportInfo, this.pagingModel).then(<PagingModel>(pagingModel) => {
        if (pagingModel.HasChart) {
          this.hasChart = true;
          this.gridData = pagingModel.data;
          if (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '') === 'PartsCycleCountsbyWorkOrderDate') {
            this.regnerateCharOptions(pagingModel.HighChartsOptions);
          }
          this.chartOptions = pagingModel.HighChartsOptions;
          this.chartInfo = pagingModel.ChartInformation;
          this.showCharts = true;
        } else {
          this.gridData = pagingModel.data;
        }
        this.filteredData = pagingModel.data;
        this.totalRows = pagingModel.totalRows;
        this.pagingModel.pageNumber = pagingModel.pageNumber;
        this.pagingModel.pageSize = pagingModel.pageSize;
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

    this.reportCubeService.getReport(this.reportInfo, this.pagingModel, false).then(<PagingModel>(pagingModel) => {
      
      this.cSVConverterService.downloadFile(pagingModel.data, this.gridSaved.columnsSaved, this.reportInfo.name);
    });

    
  }

  viewArchives() {
    this.globals.showLoader(true);

    this.archiveDocsGrid = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({ id: 'fileName', label: 'Archive File', type: EnumColumnType.String, visible: true, styles: { 'width': '23rem' } }),
        new ColumnsSaved({ id: 'fileSize', label: 'File Size', visible: true, type: EnumColumnType.String, styles: { 'width': '10rem' } }),
        new ColumnsSaved({ id: 'createDate', label: 'Created On', visible: true, type: EnumColumnType.Date, isRanged: true, styles: { 'width': '8rem' }, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
        new ColumnsSaved({ id: 'downloadUrl', label: 'Actions', visible: true, type: EnumColumnType.DownloadLink, styles: { 'width': '4rem' } })
      ],
      gridClass: 'formTbl',
      showMyViewsFeature: false,
      paginator: false,
      storageId: 'archivedDocPopupGrid',
      version: '1.0.0'
    });

    this.reportArchiveModel = new ReportModel({
      name: ''
    });

    this.documentService.archive(this.gridSaved.archivedFolder, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler((resp: AuditActionResultOfICollectionOfArchiveDocumentView) => {
        this.archivedGridData = resp.object.map(x => this.formatSize(x));
        this.showArchiveDialogue = true;
      }));
  }

  formatSize(archiveDocmentView: any) {
    archiveDocmentView.fileSize = formatBytes(archiveDocmentView.fileSize);
    return archiveDocmentView;
  }

}
