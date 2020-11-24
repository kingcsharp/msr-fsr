import { Component, OnInit, ViewChild, ElementRef, Input, Output, EventEmitter } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  ReportService, ReportModel, EnumAwsFolders, DocumentService, AuditActionResultOfICollectionOfArchiveDocumentView, ArchiveDocumentView
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
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj, formatBytes } from '../../models/lib/Utils';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../models/enums/EnumColumnType';
import { GridSaved } from '../../models/lib/GridSaved';
import { ReportCubeService } from '../../pages/reports/reportcube.service';
import * as Highcharts from 'highcharts';
import { ChartInfo } from '../../../app/models/lib/ChartInfo';
import { CSVConverterService } from '../../services/csvconverter.service';

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
  @Output() expandRowClick = new EventEmitter<any>();
  @ViewChild('downlodInfo') downlodInfo: ElementRef;

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
  showArchiveDialogue: boolean = false;
  archivedGridData: ArchiveDocumentView[] = [];
  archiveDocsGrid: GridSaved;
  reportArchiveModel: ReportModel;

  // expanded: boolean = false;
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
    private reportCubeService: ReportCubeService, private cSVConverterService: CSVConverterService,
    private documentService: DocumentService) {

  }

  ngOnInit(): void {
    this.calendarEn = this.globals.getCalendarDefault();
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
    this.filteredData = filteredData.filteredValue === null ? filteredData.value : filteredData.filteredValue;
    if (!this.hasChart) {
      return;
    }

    this.globals.showLoader(true);
    this.chartInfo.gridData = this.filteredData;

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
      this.filteredData = this.gridData;
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
        this.filteredData = this.gridData;
        this.globals.showLoader(false);
      });
    }
  }

  printCsvReport() {
    this.cSVConverterService.downloadFile(this.filteredData, this.gridSaved.columnsSaved, this.reportInfo.name);
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
