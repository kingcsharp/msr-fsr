import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    ReportService, ReportModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../../models/enums/EnumColumnType';
import { GridSaved } from '../../../models/lib/GridSaved';
import { ReportCubeService } from '../reportcube.service';
import { ChartInfo } from '../../../../app/models/lib/ChartInfo';

declare let jQuery: any;

@Component({
    selector: 'app-adhocreport',
    templateUrl: './adhocreport.component.html',
    styleUrls: ['./adhocreport.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AdhocComponent implements OnInit {
    privileges = EnumPrivilege;
    enumColumnType = EnumColumnType;
    data: any;
    gridSaved: GridSaved;
    reportId: string;
    reportInfo: ReportModel;
    showReport: boolean;

    showCharts: boolean = false;
    chartInfo: ChartInfo;
    hasChart: boolean = false;

    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
        private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
        private reportCubeService: ReportCubeService) {
    }

    ngOnInit(): void {
        this.route.params.subscribe(routeParams => {
            this.reportId = routeParams.id
        });

        this.getReportData();
    }

    getReportData() {
        this.globals.showLoader(true);
        this.reportService.report(env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.reportInfo = response.object.filter(x => x.id === parseInt(this.reportId))[0];
                this.gridSaved = new GridSaved({
                    columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo),
                    storageId: this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                    version: '1.0.0'
                });

                this.showReport = true;
            }));
    }
}
