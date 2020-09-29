import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    ReportService, ReportModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { GridSaved } from '../../../models/lib/GridSaved';
import { ReportCubeService } from '../../../pages/reports/reportcube.service';
import { EnumReport } from '../../../../app/models/enums/ReportType';
declare let jQuery: any;

@Component({
    selector: 'app-operations',
    templateUrl: './operations.component.html',
    styleUrls: ['./operations.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class OperationsComponent implements OnInit {
    gridSaved: GridSaved;
    reportId: string;
    reportInfo: ReportModel;
    showReport: boolean;
    hasChart: boolean = false;

    gridSaved2: GridSaved;
    reportId2: string;
    reportInfo2: ReportModel;
    showReport2: boolean;
    hasChart2: boolean = false;

    gridSaved3: GridSaved;
    reportId3: string;
    reportInfo3: ReportModel;
    showReport3: boolean;
    hasChart3: boolean = false;

    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
        private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
        private reportCubeService: ReportCubeService) {


    }

    ngOnInit(): void {
        this.getReportData();
    }

    getReportData() {
        this.globals.showLoader(true);

        this.reportService.report(env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.reportInfo = response.object.filter(x => x.id === EnumReport.RevenuebyCustomerbyTimePeriod)[0];
                this.gridSaved = new GridSaved({
                    columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo),
                    storageId: this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                    version: '1.0.0'
                });

                this.showReport = true;
            }));

        this.reportService.report(env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.reportInfo2 = response.object.filter(x => x.id === EnumReport.RevenuebyKitbyPartKit)[0];
                this.gridSaved2 = new GridSaved({
                    columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo2),
                    storageId: this.reportInfo2.name.replace(/\s/g, '') + this.reportInfo2.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                    version: '1.0.0'
                });

                this.showReport2 = true;
            }));

        this.reportService.report(env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.reportInfo3 = response.object.filter(x => x.id === EnumReport.CountofKitsbyPartKit)[0];
                this.gridSaved3 = new GridSaved({
                    columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo3),
                    storageId: this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                    version: '1.0.0'
                });

                this.showReport3 = true;
            }));
    }


}