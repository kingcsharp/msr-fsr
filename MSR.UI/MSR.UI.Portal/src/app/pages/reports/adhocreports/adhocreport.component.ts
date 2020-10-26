import { Component, OnInit, ViewEncapsulation, ElementRef, AfterViewInit, OnDestroy } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    ReportService, ReportModel, CustomerService
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { ActivatedRoute } from '@angular/router';
import { GridSaved } from '../../../models/lib/GridSaved';
import { ReportCubeService } from '../reportcube.service';
import { Subscription } from 'rxjs';

declare let jQuery: any;

@Component({
    selector: 'app-adhocreport',
    templateUrl: './adhocreport.component.html',
    styleUrls: ['./adhocreport.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class AdhocComponent implements OnInit, AfterViewInit, OnDestroy {
    gridSaved: GridSaved;
    reportId: string;
    reportInfo: ReportModel;
    showReport: boolean;
    hasChart: boolean = false;
    subscriptions: Subscription[] = [];
    isGettingPortal: boolean = false;

    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
        private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
        private reportCubeService: ReportCubeService) {
    }

    ngOnInit(): void {
        const sub1 = this.route.params.subscribe(routeParams => {
            this.reportId = routeParams.id;
        });

        this.subscriptions.push(sub1);

        if (this.globals.selectedCustomer !== undefined) {
            this.getReportData();
        }
    }

    ngOnDestroy() {
        this.subscriptions.forEach((subscription) => subscription.unsubscribe());
    }

    ngAfterViewInit(): void {
        if (this.globals.selectedCustomer !== undefined) {
            this.getReportData();
        }

        const sub2 = this.globals.isBuyerObservable.subscribe(response => {
            if (this.globals.selectedCustomer !== undefined) {
                this.getReportData();
            }
        });

        const sub3 = this.globals.selectCustomerObservable.subscribe(response => {
            if (response !== null && response !== undefined) {
                this.getReportData();
            }
        });
        this.subscriptions.push(sub2);
        this.subscriptions.push(sub3);
    }

    getReportData() {
        this.globals.showLoader(true);
        this.showReport = false;
        if (!this.isGettingPortal) {
            this.isGettingPortal = true;
            this.reportService.report(true, env.apiVersion).pipe(take(1))
                .subscribe(responseHandler(response => {
                    this.isGettingPortal = false;
                    this.reportInfo = response.object.filter(x => x.id === parseInt(this.reportId, 10))[0];
                    this.gridSaved = new GridSaved({
                        columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo),
                        storageId: this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                        version: '1.0.0'
                    });

                    this.showReport = true;
                }));
        }
    }
}
