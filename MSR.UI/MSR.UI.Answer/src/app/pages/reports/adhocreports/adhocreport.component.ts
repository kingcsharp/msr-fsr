import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    ReportService, ReportModel, CustomerService, PartService
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
export class AdhocComponent implements OnInit {
    gridSaved: GridSaved;
    reportId: string;
    reportInfo: ReportModel;
    showReport: boolean;
    hasChart: boolean = false;
    subscriptions: Subscription[] = [];
    staticOptions: any;
    customerNamesAndIds: Array<any>;

    constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
        private elem: ElementRef, private reportService: ReportService, private route: ActivatedRoute,
        private reportCubeService: ReportCubeService, private customerService: CustomerService, private partService: PartService) {
    }

    ngOnInit(): void {
        this.subscriptions.push(this.route.params.subscribe(routeParams => {
            this.reportId = routeParams.id;
        }));

        this.getReportData();

        this.staticOptions = [
            {
                msrfsrfacility: 'Chandler'
            },
            {
                msrfsrfacility: 'Naas'
            },
            {
                msrfsrfacility: 'Hillsboro'
            },
            {
                msrfsrfacility: 'Kiryat Gat'
            },
            {
                locationname: 'Chandler'
            },
            {
                locationname: 'Naas'
            },
            {
                locationname: 'Hillsboro'
            },
            {
                locationname: 'Kiryat Gat'
            },
            {
                site: 'Chandler'
            },
            {
                site: 'Naas'
            },
            {
                site: 'Hillsboro'
            },
            {
                site: 'Kiryat Gat'
            }
        ];

        this.customerService.customerGet(null, null, null, null, null, null, null, null, null, null,
            null, null, null, null, null, null, null, null, null, env.apiVersion).pipe(take(1)).subscribe(customers => {
                this.customerNamesAndIds = customers.object.map(s =>  {

                    return {
                        id: s.id,
                        customername: s.name
                    };

                });

                this.customerNamesAndIds.map(s => {

                    this.staticOptions.push({ customername: s.customername});

                });

                this.customerNamesAndIds.map(s => {

                    this.staticOptions.push({ customerid: s.id});

                });
            });


    }

    getReportData() {
        this.globals.showLoader(true);
        this.reportService.report(false, env.apiVersion).pipe(take(1))
            .subscribe(responseHandler(response => {
                this.reportInfo = response.object.filter(x => x.id === parseInt(this.reportId, 10))[0];
                this.gridSaved = new GridSaved({
                    columnsSaved: this.reportCubeService.getReportColumns(this.reportInfo),
                    storageId: this.reportInfo.name.replace(/\s/g, '') + this.reportInfo.subtitle.replace(/\s/g, '') + this.elem.nativeElement.tagName.toLowerCase(),
                    version: '1.0.0',
                    archivedFolder: this.reportCubeService.getArchivedEnum(this.reportInfo)
                });

                this.showReport = true;
            }));
    }
}
