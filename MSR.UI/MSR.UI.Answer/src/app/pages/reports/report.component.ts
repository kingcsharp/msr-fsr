import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../models/lib/globals';
import {
  WorkflowGroupService, WorkflowGroupModel, WorkflowGroupRoleMapModel, RoleService, UserService, WorkflowGroupUserMapModel,
  Role, AuditActionResultOfWorkflowGroupModel, CreateWorkflowGroupRequest, UpdateWorkflowGroupRequest, EnumMenuItem, ReportService
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../models/lib/Utils';
import { CubejsClient } from '@cubejs-client/ngx';
import { Subject } from "rxjs";
import { CSVConverterService } from '../../services/csvconverter.service';

declare let jQuery: any;

@Component({
  selector: 'app-reports',
  templateUrl: './report.component.html',
  styleUrls: ['./report.component.scss'],
  encapsulation: ViewEncapsulation.None
})
export class ReportComponent implements OnInit {
  data: any;
  query: any = [];
  querySubject: any;
  repData: any;
  constructor(public globals: Globals, public cg: CommonGrid, private toastr: ToastrService,
    private elem: ElementRef, private reportService: ReportService, private cubejs: CubejsClient, private cSVConverterService: CSVConverterService) {

  }

  ngOnInit(): void {
    this.reportService.report(env.apiVersion).subscribe(responseHandler(response => {
      this.data = response.object;
    }));
    // Get data of report.
    // this.querySubject = new Subject();
    // this.cubejs.watch(this.querySubject).subscribe(resultSet => {
    //   debugger;
    //   console.log(resultSet);
    // },
    //   err => console.log('HTTP Error', err)
    // );
    // this.querySubject.next(this.query);

    //csv demo.
    this.repData = this.getData();
  }

  printCsvReport(reportId) {
    console.log(reportId);
    this.cSVConverterService.downloadFile(this.repData,
      ['CubeWorkinprocess.id',
        'CubeWorkinprocess.duedate',
        'CubeWorkinprocess.details',
        'CubeWorkinprocess.status',
        'CubeWorkinprocess.name',
        'CubeWorkinprocess.mttn'], ['Id',
      'Due Date',
      'Details',
      'Status',
      'Name',
      'Mttn'], "reportFile");
  }

  getData() {
    return [
      {
        "CubeWorkinprocess.id": 552,
        "CubeWorkinprocess.duedate": "2019-03-28T11:07:00.000",
        "CubeWorkinprocess.details": "Specification: ASM Pulsar Valve Clean (04-SEA-002) |   ATTN: N/A  | P.O.: 45995754BA | Kit: Clean Seagate ASM Angle Valve | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Waiting to Start",
        "CubeWorkinprocess.name": "ASM Pulsar Valve Clean (04-SEA-002)",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 553,
        "CubeWorkinprocess.duedate": "2019-04-16T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: ASM Pulsar Valve Clean (04-SEA-002) |   ATTN: N/A  | P.O.: 45995754BA | Kit: Clean Seagate ASM Angle Valve | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Waiting to Start",
        "CubeWorkinprocess.name": "ASM Pulsar Valve Clean (04-SEA-002)",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 565,
        "CubeWorkinprocess.duedate": "1900-01-01T00:00:00.000",
        "CubeWorkinprocess.details": "Specification: Single Wafer Tray SWC (03-VWR-002) |   ATTN: N/A  | P.O.: INT-1505001-JB | Kit: VWR Wafer Tray | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Single Wafer Tray SWC (03-VWR-002)",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 688,
        "CubeWorkinprocess.duedate": "2020-04-05T08:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT504.2"
      },
      {
        "CubeWorkinprocess.id": 709,
        "CubeWorkinprocess.duedate": "2020-04-05T17:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT515.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT515.2"
      },
      {
        "CubeWorkinprocess.id": 711,
        "CubeWorkinprocess.duedate": "2020-04-06T03:42:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 307481/1 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 713,
        "CubeWorkinprocess.duedate": "2020-04-06T03:50:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 307481 / 2  | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 720,
        "CubeWorkinprocess.duedate": "2020-04-06T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: CAR261.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "CAR261.2"
      },
      {
        "CubeWorkinprocess.id": 721,
        "CubeWorkinprocess.duedate": "2020-04-06T07:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO837.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO837.1"
      },
      {
        "CubeWorkinprocess.id": 722,
        "CubeWorkinprocess.duedate": "2020-04-06T10:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXT515.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT515.2"
      },
      {
        "CubeWorkinprocess.id": 746,
        "CubeWorkinprocess.duedate": "2020-04-07T00:53:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 307481 / 1  | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 747,
        "CubeWorkinprocess.duedate": "2020-04-07T01:07:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 307481 / 2  | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 749,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 750,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 751,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 752,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 753,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 803,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 804,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 805,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 806,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 807,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 808,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 809,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 810,
        "CubeWorkinprocess.duedate": "2020-05-11T07:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 1820,
        "CubeWorkinprocess.duedate": "2019-12-11T08:02:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120219A | Kit: Non Copper RPS Standard Core | Tool:  | MTTN: ANT709.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT709.1"
      },
      {
        "CubeWorkinprocess.id": 1821,
        "CubeWorkinprocess.duedate": "2019-12-11T08:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120219A | Kit: Non Copper RPS Standard Core | Tool:  | MTTN: ONT705.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT705.2"
      },
      {
        "CubeWorkinprocess.id": 2095,
        "CubeWorkinprocess.duedate": "2019-12-25T15:27:00.000",
        "CubeWorkinprocess.details": "Specification: EPM Lens Assembly ( 03-AMT-014) |   ATTN: N/A  | P.O.: 501460 | Kit: Test Quote clean of EPM  | Tool:  | MTTN: 805702/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "EPM Lens Assembly ( 03-AMT-014)",
        "CubeWorkinprocess.mttn": "805702/1"
      },
      {
        "CubeWorkinprocess.id": 2381,
        "CubeWorkinprocess.duedate": "2020-01-23T11:37:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 292987 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 1304439935",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "1304439935"
      },
      {
        "CubeWorkinprocess.id": 2408,
        "CubeWorkinprocess.duedate": "2020-01-27T08:36:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS623.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS623.1"
      },
      {
        "CubeWorkinprocess.id": 2409,
        "CubeWorkinprocess.duedate": "2020-01-27T08:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO855.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO855.2"
      },
      {
        "CubeWorkinprocess.id": 2410,
        "CubeWorkinprocess.duedate": "2020-01-27T08:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO841.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO841.1"
      },
      {
        "CubeWorkinprocess.id": 2484,
        "CubeWorkinprocess.duedate": "2020-02-02T13:25:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX705.1"
      },
      {
        "CubeWorkinprocess.id": 2485,
        "CubeWorkinprocess.duedate": "2020-02-02T13:25:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX735.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX735.4"
      },
      {
        "CubeWorkinprocess.id": 2486,
        "CubeWorkinprocess.duedate": "2020-02-02T16:32:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS626.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS626.1"
      },
      {
        "CubeWorkinprocess.id": 2487,
        "CubeWorkinprocess.duedate": "2020-02-02T16:33:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 2509,
        "CubeWorkinprocess.duedate": "2020-02-03T09:03:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-481 - OXT Focus Ring |   ATTN: N/A  | P.O.: 602346 | Kit: 500249953 - X2,OXTON,BASE, FOCUS,RING,CLEANED | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-481 - OXT Focus Ring",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 2520,
        "CubeWorkinprocess.duedate": "2020-02-03T17:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT301.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT301.1"
      },
      {
        "CubeWorkinprocess.id": 2524,
        "CubeWorkinprocess.duedate": "2020-02-04T08:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO341.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO341.1"
      },
      {
        "CubeWorkinprocess.id": 2569,
        "CubeWorkinprocess.duedate": "2020-02-07T12:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO889.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO889.2"
      },
      {
        "CubeWorkinprocess.id": 2570,
        "CubeWorkinprocess.duedate": "2020-02-07T12:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO889.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO889.6"
      },
      {
        "CubeWorkinprocess.id": 2572,
        "CubeWorkinprocess.duedate": "2020-02-07T13:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX706.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX706.3"
      },
      {
        "CubeWorkinprocess.id": 2573,
        "CubeWorkinprocess.duedate": "2020-02-07T13:57:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT734.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT734.1"
      },
      {
        "CubeWorkinprocess.id": 2576,
        "CubeWorkinprocess.duedate": "2020-02-07T14:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX702.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX702.3"
      },
      {
        "CubeWorkinprocess.id": 2593,
        "CubeWorkinprocess.duedate": "2020-02-08T14:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX710.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX710.1"
      },
      {
        "CubeWorkinprocess.id": 2616,
        "CubeWorkinprocess.duedate": "2020-02-06T15:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 514150 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 818237/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "818237/1"
      },
      {
        "CubeWorkinprocess.id": 2617,
        "CubeWorkinprocess.duedate": "2020-02-06T15:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 514150 | Kit: 0247-02455 - Litmas RPS Cu | Tool:  | MTTN: 818237/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "818237/1"
      },
      {
        "CubeWorkinprocess.id": 2619,
        "CubeWorkinprocess.duedate": "2020-02-06T15:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 514150 | Kit: 0247-02455 - Litmas RPS Cu | Tool:  | MTTN: 818237/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "818237/1"
      },
      {
        "CubeWorkinprocess.id": 2628,
        "CubeWorkinprocess.duedate": "2020-02-12T07:24:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT503.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT503.2"
      },
      {
        "CubeWorkinprocess.id": 2655,
        "CubeWorkinprocess.duedate": "2020-02-13T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS258.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS258.2"
      },
      {
        "CubeWorkinprocess.id": 2688,
        "CubeWorkinprocess.duedate": "2020-02-17T06:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT512.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT512.1"
      },
      {
        "CubeWorkinprocess.id": 2691,
        "CubeWorkinprocess.duedate": "2020-02-17T07:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 2695,
        "CubeWorkinprocess.duedate": "2020-02-17T08:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 2721,
        "CubeWorkinprocess.duedate": "2020-02-15T17:40:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: Qual PO 21020 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "1"
      },
      {
        "CubeWorkinprocess.id": 2744,
        "CubeWorkinprocess.duedate": "2020-02-19T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS671.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS671.1"
      },
      {
        "CubeWorkinprocess.id": 2768,
        "CubeWorkinprocess.duedate": "2020-02-18T03:14:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 296727/2 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 2769,
        "CubeWorkinprocess.duedate": "2020-02-18T03:18:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 296727/3 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 2771,
        "CubeWorkinprocess.duedate": "2020-02-18T03:21:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 296727/5 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 2774,
        "CubeWorkinprocess.duedate": "2020-02-20T06:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE514.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE514.1"
      },
      {
        "CubeWorkinprocess.id": 2776,
        "CubeWorkinprocess.duedate": "2020-02-20T07:02:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT714.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT714.1"
      },
      {
        "CubeWorkinprocess.id": 2807,
        "CubeWorkinprocess.duedate": "2020-02-16T09:16:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 517358 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 821026/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "821026/1"
      },
      {
        "CubeWorkinprocess.id": 2808,
        "CubeWorkinprocess.duedate": "2020-02-16T09:16:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 517358 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: 821026/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": "821026/1"
      },
      {
        "CubeWorkinprocess.id": 2852,
        "CubeWorkinprocess.duedate": "2020-02-23T12:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS451.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS451.2"
      },
      {
        "CubeWorkinprocess.id": 2944,
        "CubeWorkinprocess.duedate": "2020-02-28T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO837.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO837.5"
      },
      {
        "CubeWorkinprocess.id": 2992,
        "CubeWorkinprocess.duedate": "2020-02-29T13:02:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: ANT304.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT304.6"
      },
      {
        "CubeWorkinprocess.id": 3017,
        "CubeWorkinprocess.duedate": "2020-03-01T17:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT706.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT706.5"
      },
      {
        "CubeWorkinprocess.id": 3045,
        "CubeWorkinprocess.duedate": "2020-03-02T16:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX720.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX720.1"
      },
      {
        "CubeWorkinprocess.id": 3098,
        "CubeWorkinprocess.duedate": "2020-03-04T16:24:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX757.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX757.2"
      },
      {
        "CubeWorkinprocess.id": 3123,
        "CubeWorkinprocess.duedate": "2020-03-06T09:15:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO889.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO889.5"
      },
      {
        "CubeWorkinprocess.id": 3124,
        "CubeWorkinprocess.duedate": "2020-03-06T09:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO889.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO889.1"
      },
      {
        "CubeWorkinprocess.id": 3180,
        "CubeWorkinprocess.duedate": "2020-03-07T12:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO830.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO830.2"
      },
      {
        "CubeWorkinprocess.id": 3281,
        "CubeWorkinprocess.duedate": "2020-03-09T07:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX711.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX711.1"
      },
      {
        "CubeWorkinprocess.id": 3296,
        "CubeWorkinprocess.duedate": "2020-03-10T06:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX737.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX737.4"
      },
      {
        "CubeWorkinprocess.id": 3297,
        "CubeWorkinprocess.duedate": "2020-03-10T06:15:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT521.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT521.1"
      },
      {
        "CubeWorkinprocess.id": 3302,
        "CubeWorkinprocess.duedate": "2020-03-10T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 3420,
        "CubeWorkinprocess.duedate": "2020-01-20T14:08:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 602346 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 1304437632",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "1304437632"
      },
      {
        "CubeWorkinprocess.id": 3421,
        "CubeWorkinprocess.duedate": "2020-01-20T14:10:00.000",
        "CubeWorkinprocess.details": "Specification: 03-INT-135 Subfab Exhaust (J, H, M pipes) |   ATTN: N/A  | P.O.: 602346 | Kit: 500407971 - CL-EXHAUST M-PIPE | Tool:  | MTTN: 1304437632",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "03-INT-135 Subfab Exhaust (J, H, M pipes)",
        "CubeWorkinprocess.mttn": "1304437632"
      },
      {
        "CubeWorkinprocess.id": 3466,
        "CubeWorkinprocess.duedate": "2020-01-25T06:24:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.2"
      },
      {
        "CubeWorkinprocess.id": 3467,
        "CubeWorkinprocess.duedate": "2020-01-25T06:25:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT735.5"
      },
      {
        "CubeWorkinprocess.id": 3510,
        "CubeWorkinprocess.duedate": "2020-01-25T05:12:00.000",
        "CubeWorkinprocess.details": "Specification: ASM VFN FLANGE - LSR GDR B  (01-INT-483)   |   ATTN: N/A  | P.O.: 500083406 | Kit: 500083406 LSR LINER SUS RING, IMP | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "ASM VFN FLANGE - LSR GDR B  (01-INT-483)  ",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 3537,
        "CubeWorkinprocess.duedate": "1900-01-01T00:00:00.000",
        "CubeWorkinprocess.details": "Specification: Single Wafer Tray SWC (03-VWR-002) |   ATTN: N/A  | P.O.: 8050-2019 WAFER TRAYS | Kit: VWR Wafer Tray | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Single Wafer Tray SWC (03-VWR-002)",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 3538,
        "CubeWorkinprocess.duedate": "1900-01-01T00:00:00.000",
        "CubeWorkinprocess.details": "Specification: Single Wafer Tray SWC (03-VWR-002) |   ATTN: N/A  | P.O.: 8050 - 2019 WAFER TRAYS | Kit: VWR Wafer Tray | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Single Wafer Tray SWC (03-VWR-002)",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 3793,
        "CubeWorkinprocess.duedate": "2020-02-05T06:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT708.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT708.5"
      },
      {
        "CubeWorkinprocess.id": 3799,
        "CubeWorkinprocess.duedate": "2020-02-05T08:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX806.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX806.3"
      },
      {
        "CubeWorkinprocess.id": 3800,
        "CubeWorkinprocess.duedate": "2020-02-05T08:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS428.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS428.2"
      },
      {
        "CubeWorkinprocess.id": 3811,
        "CubeWorkinprocess.duedate": "2020-01-31T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 512075 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 816351/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "816351/1"
      },
      {
        "CubeWorkinprocess.id": 3832,
        "CubeWorkinprocess.duedate": "2020-02-06T06:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT305.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT305.2"
      },
      {
        "CubeWorkinprocess.id": 3837,
        "CubeWorkinprocess.duedate": "2020-02-06T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 3838,
        "CubeWorkinprocess.duedate": "2020-02-06T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 3840,
        "CubeWorkinprocess.duedate": "2020-02-06T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 3841,
        "CubeWorkinprocess.duedate": "2020-02-06T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 3842,
        "CubeWorkinprocess.duedate": "2020-02-06T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 3985,
        "CubeWorkinprocess.duedate": "2019-12-17T23:58:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 121212 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 121212",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "121212"
      },
      {
        "CubeWorkinprocess.id": 4020,
        "CubeWorkinprocess.duedate": "2019-12-19T00:02:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 12344321 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: 12344321",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "12344321"
      },
      {
        "CubeWorkinprocess.id": 4106,
        "CubeWorkinprocess.duedate": "2020-02-09T06:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE339.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE339.1"
      },
      {
        "CubeWorkinprocess.id": 4107,
        "CubeWorkinprocess.duedate": "2020-02-09T06:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE328.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE328.2"
      },
      {
        "CubeWorkinprocess.id": 4129,
        "CubeWorkinprocess.duedate": "2020-02-05T09:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 513619 | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 817772/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "817772/1"
      },
      {
        "CubeWorkinprocess.id": 4140,
        "CubeWorkinprocess.duedate": "2020-02-10T10:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX722.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX722.3"
      },
      {
        "CubeWorkinprocess.id": 4160,
        "CubeWorkinprocess.duedate": "2020-02-11T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS421.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS421.1"
      },
      {
        "CubeWorkinprocess.id": 4163,
        "CubeWorkinprocess.duedate": "2020-02-11T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS421.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS421.2"
      },
      {
        "CubeWorkinprocess.id": 4323,
        "CubeWorkinprocess.duedate": "2019-10-19T19:19:00.000",
        "CubeWorkinprocess.details": "Specification: test 417 |   ATTN: N/A  | P.O.: 123454566 | Kit: test quote 417 | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test 417",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 4392,
        "CubeWorkinprocess.duedate": "2019-10-27T19:32:00.000",
        "CubeWorkinprocess.details": "Specification: test 417 |   ATTN: N/A  | P.O.: 4507434087 | Kit: test quote 417 | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test 417",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 4456,
        "CubeWorkinprocess.duedate": "2019-12-29T04:54:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 1234/1 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 4645,
        "CubeWorkinprocess.duedate": "2020-02-14T06:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: LAT734.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "LAT734.4"
      },
      {
        "CubeWorkinprocess.id": 4646,
        "CubeWorkinprocess.duedate": "2020-02-14T06:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL551.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL551.1"
      },
      {
        "CubeWorkinprocess.id": 4647,
        "CubeWorkinprocess.duedate": "2020-02-14T06:24:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX757.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX757.4"
      },
      {
        "CubeWorkinprocess.id": 4649,
        "CubeWorkinprocess.duedate": "2020-02-14T07:36:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO840.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO840.6"
      },
      {
        "CubeWorkinprocess.id": 5262,
        "CubeWorkinprocess.duedate": "2019-09-25T09:45:00.000",
        "CubeWorkinprocess.details": "Specification: test 417 |   ATTN: N/A  | P.O.: 475377/1 | Kit: test quote 417 | Tool:  | MTTN: 778734/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test 417",
        "CubeWorkinprocess.mttn": "778734/1"
      },
      {
        "CubeWorkinprocess.id": 5264,
        "CubeWorkinprocess.duedate": "2019-09-25T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: test 417 |   ATTN: N/A  | P.O.: 475433/1 | Kit: test quote 417 | Tool:  | MTTN: 778767/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test 417",
        "CubeWorkinprocess.mttn": "778767/1"
      },
      {
        "CubeWorkinprocess.id": 5266,
        "CubeWorkinprocess.duedate": "2019-09-25T10:35:00.000",
        "CubeWorkinprocess.details": "Specification: test 417 |   ATTN: N/A  | P.O.: 123454566 | Kit: test quote 417 | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test 417",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5322,
        "CubeWorkinprocess.duedate": "2020-03-12T07:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 5389,
        "CubeWorkinprocess.duedate": "2020-04-03T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT314.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT314.2"
      },
      {
        "CubeWorkinprocess.id": 5390,
        "CubeWorkinprocess.duedate": "2020-04-03T11:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX737.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX737.6"
      },
      {
        "CubeWorkinprocess.id": 5510,
        "CubeWorkinprocess.duedate": "2020-03-15T09:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT715.2"
      },
      {
        "CubeWorkinprocess.id": 5528,
        "CubeWorkinprocess.duedate": "2020-03-16T07:50:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT703.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT703.5"
      },
      {
        "CubeWorkinprocess.id": 5533,
        "CubeWorkinprocess.duedate": "2020-03-16T10:50:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 5554,
        "CubeWorkinprocess.duedate": "2020-03-16T18:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX721.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX721.4"
      },
      {
        "CubeWorkinprocess.id": 5555,
        "CubeWorkinprocess.duedate": "2020-03-17T07:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO828.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO828.6"
      },
      {
        "CubeWorkinprocess.id": 5605,
        "CubeWorkinprocess.duedate": "2020-03-19T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE508.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE508.2"
      },
      {
        "CubeWorkinprocess.id": 5606,
        "CubeWorkinprocess.duedate": "2020-03-19T07:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX706.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX706.2"
      },
      {
        "CubeWorkinprocess.id": 5632,
        "CubeWorkinprocess.duedate": "2020-03-20T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX722.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX722.3"
      },
      {
        "CubeWorkinprocess.id": 5669,
        "CubeWorkinprocess.duedate": "2020-03-21T11:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT317.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT317.2"
      },
      {
        "CubeWorkinprocess.id": 5670,
        "CubeWorkinprocess.duedate": "2020-03-21T11:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT522.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT522.1"
      },
      {
        "CubeWorkinprocess.id": 5671,
        "CubeWorkinprocess.duedate": "2020-03-21T11:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT734.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT734.5"
      },
      {
        "CubeWorkinprocess.id": 5695,
        "CubeWorkinprocess.duedate": "2020-03-21T04:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: RPS TEST | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5696,
        "CubeWorkinprocess.duedate": "2020-03-21T04:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: RPS TEST | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5718,
        "CubeWorkinprocess.duedate": "2020-03-24T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT305.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT305.1"
      },
      {
        "CubeWorkinprocess.id": 5719,
        "CubeWorkinprocess.duedate": "2020-03-24T08:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: LAT716.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "LAT716.2"
      },
      {
        "CubeWorkinprocess.id": 5748,
        "CubeWorkinprocess.duedate": "2020-03-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS247.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS247.2"
      },
      {
        "CubeWorkinprocess.id": 5750,
        "CubeWorkinprocess.duedate": "2020-03-25T07:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO854.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO854.1"
      },
      {
        "CubeWorkinprocess.id": 5762,
        "CubeWorkinprocess.duedate": "2020-03-25T08:36:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO854.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO854.2"
      },
      {
        "CubeWorkinprocess.id": 5763,
        "CubeWorkinprocess.duedate": "2020-03-25T08:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO854.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO854.6"
      },
      {
        "CubeWorkinprocess.id": 5765,
        "CubeWorkinprocess.duedate": "2020-03-25T08:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO854.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO854.5"
      },
      {
        "CubeWorkinprocess.id": 5791,
        "CubeWorkinprocess.duedate": "2020-03-26T08:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT704.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT704.5"
      },
      {
        "CubeWorkinprocess.id": 5843,
        "CubeWorkinprocess.duedate": "2020-03-25T14:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: RPS TEST | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5844,
        "CubeWorkinprocess.duedate": "2020-03-25T14:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: RPS TEST | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5846,
        "CubeWorkinprocess.duedate": "2020-03-25T14:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: RPS TEST | Kit: 0247-02456 - Litmas RPS Non Cu | Tool:  | MTTN: 0.00",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "0.00"
      },
      {
        "CubeWorkinprocess.id": 5870,
        "CubeWorkinprocess.duedate": "2020-03-29T08:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 5910,
        "CubeWorkinprocess.duedate": "2020-03-29T17:50:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT734.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT734.5"
      },
      {
        "CubeWorkinprocess.id": 5911,
        "CubeWorkinprocess.duedate": "2020-03-29T17:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 5933,
        "CubeWorkinprocess.duedate": "2020-03-30T07:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO843.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO843.6"
      },
      {
        "CubeWorkinprocess.id": 5934,
        "CubeWorkinprocess.duedate": "2020-03-30T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO843.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO843.2"
      },
      {
        "CubeWorkinprocess.id": 5951,
        "CubeWorkinprocess.duedate": "2020-03-31T07:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX702.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX702.4"
      },
      {
        "CubeWorkinprocess.id": 5984,
        "CubeWorkinprocess.duedate": "2020-04-01T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT706.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT706.6"
      },
      {
        "CubeWorkinprocess.id": 5985,
        "CubeWorkinprocess.duedate": "2020-04-01T07:32:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 6019,
        "CubeWorkinprocess.duedate": "2020-04-07T07:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO844.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO844.5"
      },
      {
        "CubeWorkinprocess.id": 6020,
        "CubeWorkinprocess.duedate": "2020-04-07T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO856.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO856.5"
      },
      {
        "CubeWorkinprocess.id": 6021,
        "CubeWorkinprocess.duedate": "2020-04-07T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 6022,
        "CubeWorkinprocess.duedate": "2020-04-07T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO844.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO844.6"
      },
      {
        "CubeWorkinprocess.id": 6023,
        "CubeWorkinprocess.duedate": "2020-04-07T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO844.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO844.2"
      },
      {
        "CubeWorkinprocess.id": 6061,
        "CubeWorkinprocess.duedate": "2020-04-08T09:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX806.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX806.2"
      },
      {
        "CubeWorkinprocess.id": 6062,
        "CubeWorkinprocess.duedate": "2020-04-08T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO834.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO834.6"
      },
      {
        "CubeWorkinprocess.id": 6072,
        "CubeWorkinprocess.duedate": "2020-04-08T11:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT706.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT706.2"
      },
      {
        "CubeWorkinprocess.id": 6086,
        "CubeWorkinprocess.duedate": "2020-04-09T08:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT512.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT512.2"
      },
      {
        "CubeWorkinprocess.id": 6141,
        "CubeWorkinprocess.duedate": "2020-04-11T09:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE328.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE328.1"
      },
      {
        "CubeWorkinprocess.id": 6142,
        "CubeWorkinprocess.duedate": "2020-04-11T09:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT710.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT710.5"
      },
      {
        "CubeWorkinprocess.id": 6190,
        "CubeWorkinprocess.duedate": "2020-04-12T18:03:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS411.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS411.2"
      },
      {
        "CubeWorkinprocess.id": 6192,
        "CubeWorkinprocess.duedate": "2020-04-12T18:04:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO856.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO856.6"
      },
      {
        "CubeWorkinprocess.id": 6200,
        "CubeWorkinprocess.duedate": "2020-04-12T18:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT713.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT713.6"
      },
      {
        "CubeWorkinprocess.id": 6241,
        "CubeWorkinprocess.duedate": "2020-04-13T18:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT519.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT519.1"
      },
      {
        "CubeWorkinprocess.id": 6296,
        "CubeWorkinprocess.duedate": "2020-04-15T17:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: BCL453.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL453.1"
      },
      {
        "CubeWorkinprocess.id": 6297,
        "CubeWorkinprocess.duedate": "2020-04-15T17:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXT514.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT514.1"
      },
      {
        "CubeWorkinprocess.id": 6298,
        "CubeWorkinprocess.duedate": "2020-04-15T17:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXT514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT514.2"
      },
      {
        "CubeWorkinprocess.id": 6299,
        "CubeWorkinprocess.duedate": "2020-04-15T17:59:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS628.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS628.1"
      },
      {
        "CubeWorkinprocess.id": 6345,
        "CubeWorkinprocess.duedate": "2020-04-17T08:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: BCL453.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL453.1"
      },
      {
        "CubeWorkinprocess.id": 6375,
        "CubeWorkinprocess.duedate": "2020-04-17T11:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT515.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT515.1"
      },
      {
        "CubeWorkinprocess.id": 6541,
        "CubeWorkinprocess.duedate": "2020-04-22T13:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX757.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX757.4"
      },
      {
        "CubeWorkinprocess.id": 6542,
        "CubeWorkinprocess.duedate": "2020-04-22T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT301.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT301.2"
      },
      {
        "CubeWorkinprocess.id": 6543,
        "CubeWorkinprocess.duedate": "2020-04-22T13:48:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX710.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX710.1"
      },
      {
        "CubeWorkinprocess.id": 6548,
        "CubeWorkinprocess.duedate": "2020-04-22T14:50:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX803.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX803.3"
      },
      {
        "CubeWorkinprocess.id": 6549,
        "CubeWorkinprocess.duedate": "2020-04-22T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.2"
      },
      {
        "CubeWorkinprocess.id": 6550,
        "CubeWorkinprocess.duedate": "2020-04-22T14:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO827.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO827.5"
      },
      {
        "CubeWorkinprocess.id": 6551,
        "CubeWorkinprocess.duedate": "2020-04-22T14:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 6568,
        "CubeWorkinprocess.duedate": "2020-04-25T07:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT733.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT733.1"
      },
      {
        "CubeWorkinprocess.id": 6569,
        "CubeWorkinprocess.duedate": "2020-04-25T07:59:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX702.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX702.3"
      },
      {
        "CubeWorkinprocess.id": 6570,
        "CubeWorkinprocess.duedate": "2020-04-25T09:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: BCL807.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL807.3"
      },
      {
        "CubeWorkinprocess.id": 6571,
        "CubeWorkinprocess.duedate": "2020-04-25T09:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX810.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX810.3"
      },
      {
        "CubeWorkinprocess.id": 6572,
        "CubeWorkinprocess.duedate": "2020-04-25T09:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.1"
      },
      {
        "CubeWorkinprocess.id": 6573,
        "CubeWorkinprocess.duedate": "2020-04-25T09:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX810.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX810.3"
      },
      {
        "CubeWorkinprocess.id": 6613,
        "CubeWorkinprocess.duedate": "2020-04-26T16:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.1"
      },
      {
        "CubeWorkinprocess.id": 6614,
        "CubeWorkinprocess.duedate": "2020-04-26T16:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.1"
      },
      {
        "CubeWorkinprocess.id": 6615,
        "CubeWorkinprocess.duedate": "2020-04-26T16:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 6616,
        "CubeWorkinprocess.duedate": "2020-04-26T16:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 6617,
        "CubeWorkinprocess.duedate": "2020-04-26T16:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX758.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX758.4"
      },
      {
        "CubeWorkinprocess.id": 6654,
        "CubeWorkinprocess.duedate": "2020-04-27T08:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX720.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX720.2"
      },
      {
        "CubeWorkinprocess.id": 6655,
        "CubeWorkinprocess.duedate": "2020-04-27T08:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX720.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX720.2"
      },
      {
        "CubeWorkinprocess.id": 6656,
        "CubeWorkinprocess.duedate": "2020-04-27T08:35:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX720.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX720.2"
      },
      {
        "CubeWorkinprocess.id": 6658,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6659,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6660,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6661,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6662,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6663,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6664,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6666,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6668,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6669,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6670,
        "CubeWorkinprocess.duedate": "2020-04-27T11:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX812.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX812.3"
      },
      {
        "CubeWorkinprocess.id": 6718,
        "CubeWorkinprocess.duedate": "2020-04-28T10:06:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.1"
      },
      {
        "CubeWorkinprocess.id": 6720,
        "CubeWorkinprocess.duedate": "2020-04-28T10:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT521.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT521.2"
      },
      {
        "CubeWorkinprocess.id": 6723,
        "CubeWorkinprocess.duedate": "2020-04-28T10:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.3"
      },
      {
        "CubeWorkinprocess.id": 6724,
        "CubeWorkinprocess.duedate": "2020-04-28T10:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT713.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT713.1"
      },
      {
        "CubeWorkinprocess.id": 6725,
        "CubeWorkinprocess.duedate": "2020-04-28T10:22:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT701.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT701.5"
      },
      {
        "CubeWorkinprocess.id": 6746,
        "CubeWorkinprocess.duedate": "2020-04-29T07:32:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 310068 / 1 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6749,
        "CubeWorkinprocess.duedate": "2020-04-29T07:49:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 310421 / 1 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6767,
        "CubeWorkinprocess.duedate": "2020-04-29T11:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR268.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR268.1"
      },
      {
        "CubeWorkinprocess.id": 6769,
        "CubeWorkinprocess.duedate": "2020-04-29T11:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 6770,
        "CubeWorkinprocess.duedate": "2020-04-29T11:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX806.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX806.4"
      },
      {
        "CubeWorkinprocess.id": 6771,
        "CubeWorkinprocess.duedate": "2020-04-29T11:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX806.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX806.4"
      },
      {
        "CubeWorkinprocess.id": 6772,
        "CubeWorkinprocess.duedate": "2020-04-29T11:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 6774,
        "CubeWorkinprocess.duedate": "2020-04-29T11:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 6775,
        "CubeWorkinprocess.duedate": "2020-04-29T11:50:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO816.1"
      },
      {
        "CubeWorkinprocess.id": 6777,
        "CubeWorkinprocess.duedate": "2020-04-29T11:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS451.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS451.2"
      },
      {
        "CubeWorkinprocess.id": 6778,
        "CubeWorkinprocess.duedate": "2020-04-29T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 6779,
        "CubeWorkinprocess.duedate": "2020-04-29T16:03:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.5"
      },
      {
        "CubeWorkinprocess.id": 6781,
        "CubeWorkinprocess.duedate": "2020-04-29T16:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 6782,
        "CubeWorkinprocess.duedate": "2020-04-29T16:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.2"
      },
      {
        "CubeWorkinprocess.id": 6805,
        "CubeWorkinprocess.duedate": "2020-04-30T09:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX721.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX721.1"
      },
      {
        "CubeWorkinprocess.id": 6806,
        "CubeWorkinprocess.duedate": "2020-04-30T09:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.1"
      },
      {
        "CubeWorkinprocess.id": 6807,
        "CubeWorkinprocess.duedate": "2020-04-30T09:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.1"
      },
      {
        "CubeWorkinprocess.id": 6808,
        "CubeWorkinprocess.duedate": "2020-04-30T09:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.1"
      },
      {
        "CubeWorkinprocess.id": 6809,
        "CubeWorkinprocess.duedate": "2020-04-30T09:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.1"
      },
      {
        "CubeWorkinprocess.id": 6832,
        "CubeWorkinprocess.duedate": "2020-04-30T14:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS629.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS629.2"
      },
      {
        "CubeWorkinprocess.id": 6833,
        "CubeWorkinprocess.duedate": "2020-04-30T14:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO828.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO828.2"
      },
      {
        "CubeWorkinprocess.id": 6834,
        "CubeWorkinprocess.duedate": "2020-04-30T15:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS626.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS626.1"
      },
      {
        "CubeWorkinprocess.id": 6850,
        "CubeWorkinprocess.duedate": "2020-05-01T10:57:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS253.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS253.2"
      },
      {
        "CubeWorkinprocess.id": 6851,
        "CubeWorkinprocess.duedate": "2020-05-01T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: BCL807.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL807.1"
      },
      {
        "CubeWorkinprocess.id": 6867,
        "CubeWorkinprocess.duedate": "2020-05-01T14:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL551.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL551.3"
      },
      {
        "CubeWorkinprocess.id": 6945,
        "CubeWorkinprocess.duedate": "2020-05-02T05:09:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 309429 / 1 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6946,
        "CubeWorkinprocess.duedate": "2020-05-02T05:23:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 309429 / 2 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6947,
        "CubeWorkinprocess.duedate": "2020-05-02T05:30:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 309429 / 3 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6948,
        "CubeWorkinprocess.duedate": "2020-05-02T05:34:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 309429 / 4 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 6949,
        "CubeWorkinprocess.duedate": "2020-05-04T08:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT716.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT716.2"
      },
      {
        "CubeWorkinprocess.id": 6950,
        "CubeWorkinprocess.duedate": "2020-05-04T08:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT712.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT712.5"
      },
      {
        "CubeWorkinprocess.id": 6951,
        "CubeWorkinprocess.duedate": "2020-05-04T08:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT503.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT503.1"
      },
      {
        "CubeWorkinprocess.id": 6952,
        "CubeWorkinprocess.duedate": "2020-05-04T08:31:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX710.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX710.4"
      },
      {
        "CubeWorkinprocess.id": 6953,
        "CubeWorkinprocess.duedate": "2020-05-04T08:33:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE508.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE508.2"
      },
      {
        "CubeWorkinprocess.id": 6954,
        "CubeWorkinprocess.duedate": "2020-05-04T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE508.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE508.1"
      },
      {
        "CubeWorkinprocess.id": 6955,
        "CubeWorkinprocess.duedate": "2020-05-04T08:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT703.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT703.6"
      },
      {
        "CubeWorkinprocess.id": 6956,
        "CubeWorkinprocess.duedate": "2020-05-04T08:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.5"
      },
      {
        "CubeWorkinprocess.id": 6957,
        "CubeWorkinprocess.duedate": "2020-05-04T08:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE339.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE339.1"
      },
      {
        "CubeWorkinprocess.id": 6958,
        "CubeWorkinprocess.duedate": "2020-05-04T08:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE339.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE339.2"
      },
      {
        "CubeWorkinprocess.id": 6959,
        "CubeWorkinprocess.duedate": "2020-05-04T08:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.1"
      },
      {
        "CubeWorkinprocess.id": 6991,
        "CubeWorkinprocess.duedate": "2020-05-05T08:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR267.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR267.1"
      },
      {
        "CubeWorkinprocess.id": 6993,
        "CubeWorkinprocess.duedate": "2020-05-05T09:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO835.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO835.1"
      },
      {
        "CubeWorkinprocess.id": 6994,
        "CubeWorkinprocess.duedate": "2020-05-05T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO820.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO820.2"
      },
      {
        "CubeWorkinprocess.id": 6995,
        "CubeWorkinprocess.duedate": "2020-05-05T09:03:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO830.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO830.5"
      },
      {
        "CubeWorkinprocess.id": 6996,
        "CubeWorkinprocess.duedate": "2020-05-05T09:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO820.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO820.6"
      },
      {
        "CubeWorkinprocess.id": 6997,
        "CubeWorkinprocess.duedate": "2020-05-05T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 6998,
        "CubeWorkinprocess.duedate": "2020-05-05T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.3"
      },
      {
        "CubeWorkinprocess.id": 6999,
        "CubeWorkinprocess.duedate": "2020-05-05T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.1"
      },
      {
        "CubeWorkinprocess.id": 7000,
        "CubeWorkinprocess.duedate": "2020-05-05T09:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS252.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS252.1"
      },
      {
        "CubeWorkinprocess.id": 7001,
        "CubeWorkinprocess.duedate": "2020-05-05T09:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR267.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR267.1"
      },
      {
        "CubeWorkinprocess.id": 7002,
        "CubeWorkinprocess.duedate": "2020-05-05T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR261.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR261.1"
      },
      {
        "CubeWorkinprocess.id": 7003,
        "CubeWorkinprocess.duedate": "2020-05-05T09:25:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX804.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX804.3"
      },
      {
        "CubeWorkinprocess.id": 7004,
        "CubeWorkinprocess.duedate": "2020-05-05T09:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS413.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS413.1"
      },
      {
        "CubeWorkinprocess.id": 7005,
        "CubeWorkinprocess.duedate": "2020-05-05T09:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR260.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR260.1"
      },
      {
        "CubeWorkinprocess.id": 7006,
        "CubeWorkinprocess.duedate": "2020-05-05T09:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS413.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS413.2"
      },
      {
        "CubeWorkinprocess.id": 7018,
        "CubeWorkinprocess.duedate": "2020-05-06T08:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL301.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL301.3"
      },
      {
        "CubeWorkinprocess.id": 7019,
        "CubeWorkinprocess.duedate": "2020-05-06T08:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.3"
      },
      {
        "CubeWorkinprocess.id": 7020,
        "CubeWorkinprocess.duedate": "2020-05-06T08:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.3"
      },
      {
        "CubeWorkinprocess.id": 7021,
        "CubeWorkinprocess.duedate": "2020-05-06T08:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL301.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL301.3"
      },
      {
        "CubeWorkinprocess.id": 7036,
        "CubeWorkinprocess.duedate": "2020-05-06T10:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS451.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS451.1"
      },
      {
        "CubeWorkinprocess.id": 7037,
        "CubeWorkinprocess.duedate": "2020-05-06T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO834.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO834.2"
      },
      {
        "CubeWorkinprocess.id": 7038,
        "CubeWorkinprocess.duedate": "2020-05-06T10:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.2"
      },
      {
        "CubeWorkinprocess.id": 7039,
        "CubeWorkinprocess.duedate": "2020-05-06T10:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.2"
      },
      {
        "CubeWorkinprocess.id": 7040,
        "CubeWorkinprocess.duedate": "2020-05-06T10:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 7041,
        "CubeWorkinprocess.duedate": "2020-05-06T10:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO874.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO874.6"
      },
      {
        "CubeWorkinprocess.id": 7042,
        "CubeWorkinprocess.duedate": "2020-05-06T10:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO874.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO874.6"
      },
      {
        "CubeWorkinprocess.id": 7069,
        "CubeWorkinprocess.duedate": "2020-05-07T08:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT735.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT735.2"
      },
      {
        "CubeWorkinprocess.id": 7070,
        "CubeWorkinprocess.duedate": "2020-05-07T08:44:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.2"
      },
      {
        "CubeWorkinprocess.id": 7074,
        "CubeWorkinprocess.duedate": "2020-05-07T10:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS247.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS247.2"
      },
      {
        "CubeWorkinprocess.id": 7075,
        "CubeWorkinprocess.duedate": "2020-05-07T10:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS247.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS247.2"
      },
      {
        "CubeWorkinprocess.id": 7076,
        "CubeWorkinprocess.duedate": "2020-05-07T10:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS247.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS247.2"
      },
      {
        "CubeWorkinprocess.id": 7077,
        "CubeWorkinprocess.duedate": "2020-05-07T10:45:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS247.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS247.2"
      },
      {
        "CubeWorkinprocess.id": 7078,
        "CubeWorkinprocess.duedate": "2020-05-07T16:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO839.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO839.5"
      },
      {
        "CubeWorkinprocess.id": 7079,
        "CubeWorkinprocess.duedate": "2020-05-07T16:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO839.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO839.5"
      },
      {
        "CubeWorkinprocess.id": 7081,
        "CubeWorkinprocess.duedate": "2020-05-07T16:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO839.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO839.5"
      },
      {
        "CubeWorkinprocess.id": 7082,
        "CubeWorkinprocess.duedate": "2020-05-07T16:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO839.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO839.5"
      },
      {
        "CubeWorkinprocess.id": 7113,
        "CubeWorkinprocess.duedate": "2020-05-08T09:56:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 7114,
        "CubeWorkinprocess.duedate": "2020-05-11T08:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT710.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT710.1"
      },
      {
        "CubeWorkinprocess.id": 7116,
        "CubeWorkinprocess.duedate": "2020-05-11T08:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.1"
      },
      {
        "CubeWorkinprocess.id": 7136,
        "CubeWorkinprocess.duedate": "2020-05-12T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX715.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX715.1"
      },
      {
        "CubeWorkinprocess.id": 7137,
        "CubeWorkinprocess.duedate": "2020-05-12T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX715.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX715.1"
      },
      {
        "CubeWorkinprocess.id": 7138,
        "CubeWorkinprocess.duedate": "2020-05-12T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.1"
      },
      {
        "CubeWorkinprocess.id": 7139,
        "CubeWorkinprocess.duedate": "2020-05-12T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.1"
      },
      {
        "CubeWorkinprocess.id": 7140,
        "CubeWorkinprocess.duedate": "2020-05-12T08:39:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.1"
      },
      {
        "CubeWorkinprocess.id": 7159,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7160,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7161,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7162,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7163,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7164,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7165,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7166,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7167,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7168,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7169,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7170,
        "CubeWorkinprocess.duedate": "2020-05-12T11:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.2"
      },
      {
        "CubeWorkinprocess.id": 7180,
        "CubeWorkinprocess.duedate": "2020-05-11T00:13:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 1 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7181,
        "CubeWorkinprocess.duedate": "2020-05-11T00:21:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 2 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7182,
        "CubeWorkinprocess.duedate": "2020-05-11T00:28:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 3 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7183,
        "CubeWorkinprocess.duedate": "2020-05-11T00:35:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 4 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7184,
        "CubeWorkinprocess.duedate": "2020-05-11T00:41:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 5 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7185,
        "CubeWorkinprocess.duedate": "2020-05-11T00:55:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 6 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7186,
        "CubeWorkinprocess.duedate": "2020-05-11T01:03:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 7 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7187,
        "CubeWorkinprocess.duedate": "2020-05-11T01:11:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 312897 / 8 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7190,
        "CubeWorkinprocess.duedate": "2020-05-13T10:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 7199,
        "CubeWorkinprocess.duedate": "2020-05-13T12:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS231.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS231.1"
      },
      {
        "CubeWorkinprocess.id": 7200,
        "CubeWorkinprocess.duedate": "2020-05-13T12:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS231.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS231.1"
      },
      {
        "CubeWorkinprocess.id": 7201,
        "CubeWorkinprocess.duedate": "2020-05-13T12:16:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS231.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS231.1"
      },
      {
        "CubeWorkinprocess.id": 7211,
        "CubeWorkinprocess.duedate": "2020-05-14T07:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.6"
      },
      {
        "CubeWorkinprocess.id": 7212,
        "CubeWorkinprocess.duedate": "2020-05-14T07:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.6"
      },
      {
        "CubeWorkinprocess.id": 7232,
        "CubeWorkinprocess.duedate": "2020-05-14T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.1"
      },
      {
        "CubeWorkinprocess.id": 7233,
        "CubeWorkinprocess.duedate": "2020-05-14T11:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX737.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX737.1"
      },
      {
        "CubeWorkinprocess.id": 7237,
        "CubeWorkinprocess.duedate": "2020-05-15T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT703.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT703.6"
      },
      {
        "CubeWorkinprocess.id": 7238,
        "CubeWorkinprocess.duedate": "2020-05-15T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT703.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT703.6"
      },
      {
        "CubeWorkinprocess.id": 7239,
        "CubeWorkinprocess.duedate": "2020-05-15T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT703.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT703.6"
      },
      {
        "CubeWorkinprocess.id": 7240,
        "CubeWorkinprocess.duedate": "2020-05-15T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT703.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT703.6"
      },
      {
        "CubeWorkinprocess.id": 7308,
        "CubeWorkinprocess.duedate": "2020-05-18T05:58:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 313948 / 3 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7309,
        "CubeWorkinprocess.duedate": "2020-05-18T06:07:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 313948 / 4 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 7310,
        "CubeWorkinprocess.duedate": "2020-05-18T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: LAT714.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "LAT714.2"
      },
      {
        "CubeWorkinprocess.id": 7311,
        "CubeWorkinprocess.duedate": "2020-05-18T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT714.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT714.2"
      },
      {
        "CubeWorkinprocess.id": 7312,
        "CubeWorkinprocess.duedate": "2020-05-18T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT714.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT714.2"
      },
      {
        "CubeWorkinprocess.id": 7313,
        "CubeWorkinprocess.duedate": "2020-05-18T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT714.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT714.2"
      },
      {
        "CubeWorkinprocess.id": 7314,
        "CubeWorkinprocess.duedate": "2020-05-18T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT714.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT714.2"
      },
      {
        "CubeWorkinprocess.id": 7315,
        "CubeWorkinprocess.duedate": "2020-05-18T09:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX737.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX737.5"
      },
      {
        "CubeWorkinprocess.id": 7333,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7334,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7335,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7336,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7337,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7338,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7339,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7340,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7341,
        "CubeWorkinprocess.duedate": "2020-05-18T11:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.2"
      },
      {
        "CubeWorkinprocess.id": 7360,
        "CubeWorkinprocess.duedate": "2020-05-19T06:57:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: BCL701.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL701.3"
      },
      {
        "CubeWorkinprocess.id": 7361,
        "CubeWorkinprocess.duedate": "2020-05-19T06:57:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: BCL701.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL701.3"
      },
      {
        "CubeWorkinprocess.id": 7372,
        "CubeWorkinprocess.duedate": "2020-05-19T11:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR610.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR610.1"
      },
      {
        "CubeWorkinprocess.id": 7398,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7399,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7400,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7401,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7402,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7403,
        "CubeWorkinprocess.duedate": "2020-05-20T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT518.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT518.1"
      },
      {
        "CubeWorkinprocess.id": 7415,
        "CubeWorkinprocess.duedate": "2020-05-20T11:24:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS624.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS624.1"
      },
      {
        "CubeWorkinprocess.id": 7438,
        "CubeWorkinprocess.duedate": "2020-05-21T07:04:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX721.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX721.2"
      },
      {
        "CubeWorkinprocess.id": 7439,
        "CubeWorkinprocess.duedate": "2020-05-21T07:04:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.2"
      },
      {
        "CubeWorkinprocess.id": 7440,
        "CubeWorkinprocess.duedate": "2020-05-21T07:04:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX721.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX721.2"
      },
      {
        "CubeWorkinprocess.id": 7454,
        "CubeWorkinprocess.duedate": "2020-05-21T09:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.2"
      },
      {
        "CubeWorkinprocess.id": 7457,
        "CubeWorkinprocess.duedate": "2020-05-22T07:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT705.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT705.6"
      },
      {
        "CubeWorkinprocess.id": 7458,
        "CubeWorkinprocess.duedate": "2020-05-22T07:17:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT705.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT705.6"
      },
      {
        "CubeWorkinprocess.id": 7487,
        "CubeWorkinprocess.duedate": "2020-05-22T08:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE550.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE550.2"
      },
      {
        "CubeWorkinprocess.id": 7488,
        "CubeWorkinprocess.duedate": "2020-05-22T08:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE550.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE550.2"
      },
      {
        "CubeWorkinprocess.id": 7490,
        "CubeWorkinprocess.duedate": "2020-05-22T09:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS629.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS629.2"
      },
      {
        "CubeWorkinprocess.id": 7539,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7540,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7541,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7542,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7543,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7544,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7545,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7546,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7547,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7548,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7549,
        "CubeWorkinprocess.duedate": "2020-05-25T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE346.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE346.2"
      },
      {
        "CubeWorkinprocess.id": 7567,
        "CubeWorkinprocess.duedate": "2020-05-25T15:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL551.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL551.1"
      },
      {
        "CubeWorkinprocess.id": 7568,
        "CubeWorkinprocess.duedate": "2020-05-25T15:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL551.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL551.1"
      },
      {
        "CubeWorkinprocess.id": 7579,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7580,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7581,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7582,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7583,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7584,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7585,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7586,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7587,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7588,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7589,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7590,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7591,
        "CubeWorkinprocess.duedate": "2020-05-26T09:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 7624,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7625,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7626,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7627,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7628,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7629,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7630,
        "CubeWorkinprocess.duedate": "2020-05-27T08:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 7670,
        "CubeWorkinprocess.duedate": "2020-05-28T08:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 7671,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7672,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7673,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7674,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7675,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7676,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7677,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7678,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7679,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7680,
        "CubeWorkinprocess.duedate": "2020-05-28T10:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO817.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO817.2"
      },
      {
        "CubeWorkinprocess.id": 7690,
        "CubeWorkinprocess.duedate": "2020-05-29T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT707.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT707.5"
      },
      {
        "CubeWorkinprocess.id": 7691,
        "CubeWorkinprocess.duedate": "2020-05-29T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT707.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT707.5"
      },
      {
        "CubeWorkinprocess.id": 7700,
        "CubeWorkinprocess.duedate": "2020-05-29T10:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS403.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS403.2"
      },
      {
        "CubeWorkinprocess.id": 7701,
        "CubeWorkinprocess.duedate": "2020-05-29T10:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS403.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS403.2"
      },
      {
        "CubeWorkinprocess.id": 7702,
        "CubeWorkinprocess.duedate": "2020-05-29T10:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS403.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS403.2"
      },
      {
        "CubeWorkinprocess.id": 7703,
        "CubeWorkinprocess.duedate": "2020-05-29T10:40:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS403.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS403.2"
      },
      {
        "CubeWorkinprocess.id": 7781,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7782,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7783,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7784,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7785,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7786,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7787,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7788,
        "CubeWorkinprocess.duedate": "2020-06-02T07:18:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT708.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT708.1"
      },
      {
        "CubeWorkinprocess.id": 7804,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7805,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7806,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7807,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7808,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7809,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7810,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7811,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7812,
        "CubeWorkinprocess.duedate": "2020-06-02T12:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX736.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX736.5"
      },
      {
        "CubeWorkinprocess.id": 7843,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7844,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7845,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7846,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7847,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7848,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7849,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7850,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7851,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7852,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7853,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7854,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7855,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7856,
        "CubeWorkinprocess.duedate": "2020-06-03T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 7904,
        "CubeWorkinprocess.duedate": "2020-06-04T13:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.6"
      },
      {
        "CubeWorkinprocess.id": 7905,
        "CubeWorkinprocess.duedate": "2020-06-04T13:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.6"
      },
      {
        "CubeWorkinprocess.id": 7906,
        "CubeWorkinprocess.duedate": "2020-06-04T13:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.6"
      },
      {
        "CubeWorkinprocess.id": 7907,
        "CubeWorkinprocess.duedate": "2020-06-04T13:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.6"
      },
      {
        "CubeWorkinprocess.id": 7908,
        "CubeWorkinprocess.duedate": "2020-06-04T13:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT735.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT735.6"
      },
      {
        "CubeWorkinprocess.id": 7916,
        "CubeWorkinprocess.duedate": "2020-06-05T08:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 7917,
        "CubeWorkinprocess.duedate": "2020-06-05T08:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 7918,
        "CubeWorkinprocess.duedate": "2020-06-05T08:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 7919,
        "CubeWorkinprocess.duedate": "2020-06-05T10:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS428.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS428.1"
      },
      {
        "CubeWorkinprocess.id": 7920,
        "CubeWorkinprocess.duedate": "2020-06-05T10:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS428.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS428.1"
      },
      {
        "CubeWorkinprocess.id": 7921,
        "CubeWorkinprocess.duedate": "2020-06-05T10:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS428.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS428.1"
      },
      {
        "CubeWorkinprocess.id": 7922,
        "CubeWorkinprocess.duedate": "2020-06-05T10:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS428.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS428.1"
      },
      {
        "CubeWorkinprocess.id": 8005,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8006,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8007,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8008,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8010,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8011,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8012,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8013,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8014,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8015,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8016,
        "CubeWorkinprocess.duedate": "2020-06-08T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT705.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT705.1"
      },
      {
        "CubeWorkinprocess.id": 8048,
        "CubeWorkinprocess.duedate": "2020-06-09T02:41:00.000",
        "CubeWorkinprocess.details": "Specification: Clean Test Parts 8-9-19 |   ATTN: N/A  | P.O.: 315770 / 1 | Kit: 500236801 - CLEANED,MSR,PIA,TERM,RING,INSULATOR,KI | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Clean Test Parts 8-9-19",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8049,
        "CubeWorkinprocess.duedate": "2020-06-09T02:56:00.000",
        "CubeWorkinprocess.details": "Specification: 04-INT-103-00  PIA Kit |   ATTN: N/A  | P.O.: 316307 / 1 | Kit: 500193599 - CLEANED,PIA,SOURCE,TUBE,KIT,MSR | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "04-INT-103-00  PIA Kit",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8050,
        "CubeWorkinprocess.duedate": "2020-06-09T06:51:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 317592 / 1 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8060,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8061,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8062,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8063,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8064,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8065,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8066,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8067,
        "CubeWorkinprocess.duedate": "2020-06-09T09:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR266.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR266.1"
      },
      {
        "CubeWorkinprocess.id": 8089,
        "CubeWorkinprocess.duedate": "2020-06-07T16:45:00.000",
        "CubeWorkinprocess.details": "Specification: ASM ALD Pulsar Clean( 04-SEA-001 )  |   ATTN: N/A  | P.O.: test060220 | Kit: ASM Pulsar for GF | Tool:  | MTTN: 34343",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "ASM ALD Pulsar Clean( 04-SEA-001 ) ",
        "CubeWorkinprocess.mttn": "34343"
      },
      {
        "CubeWorkinprocess.id": 8090,
        "CubeWorkinprocess.duedate": "2020-06-07T16:45:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-469-00  non edited upload from ESCAQA |   ATTN: N/A  | P.O.: test060220 | Kit: Test Clean new customer part 23232232 Widget | Tool:  | MTTN: 34343",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-469-00  non edited upload from ESCAQA",
        "CubeWorkinprocess.mttn": "34343"
      },
      {
        "CubeWorkinprocess.id": 8091,
        "CubeWorkinprocess.duedate": "2020-06-10T06:30:00.000",
        "CubeWorkinprocess.details": "Specification: Clean Test Parts 8-9-19 |   ATTN: N/A  | P.O.: 317592 / 3 | Kit: 500236801 - CLEANED,MSR,PIA,TERM,RING,INSULATOR,KI | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Clean Test Parts 8-9-19",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8104,
        "CubeWorkinprocess.duedate": "2020-06-10T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: BCL304.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "BCL304.3"
      },
      {
        "CubeWorkinprocess.id": 8105,
        "CubeWorkinprocess.duedate": "2020-06-10T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL304.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL304.3"
      },
      {
        "CubeWorkinprocess.id": 8106,
        "CubeWorkinprocess.duedate": "2020-06-10T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL304.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL304.3"
      },
      {
        "CubeWorkinprocess.id": 8107,
        "CubeWorkinprocess.duedate": "2020-06-10T08:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL304.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL304.3"
      },
      {
        "CubeWorkinprocess.id": 8120,
        "CubeWorkinprocess.duedate": "2020-06-10T11:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.2"
      },
      {
        "CubeWorkinprocess.id": 8121,
        "CubeWorkinprocess.duedate": "2020-06-10T11:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.2"
      },
      {
        "CubeWorkinprocess.id": 8122,
        "CubeWorkinprocess.duedate": "2020-06-10T11:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.2"
      },
      {
        "CubeWorkinprocess.id": 8130,
        "CubeWorkinprocess.duedate": "2020-06-11T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT714.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT714.5"
      },
      {
        "CubeWorkinprocess.id": 8131,
        "CubeWorkinprocess.duedate": "2020-06-11T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.5"
      },
      {
        "CubeWorkinprocess.id": 8132,
        "CubeWorkinprocess.duedate": "2020-06-11T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.5"
      },
      {
        "CubeWorkinprocess.id": 8133,
        "CubeWorkinprocess.duedate": "2020-06-11T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.5"
      },
      {
        "CubeWorkinprocess.id": 8134,
        "CubeWorkinprocess.duedate": "2020-06-11T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.5"
      },
      {
        "CubeWorkinprocess.id": 8141,
        "CubeWorkinprocess.duedate": "2020-06-11T10:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS252.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS252.2"
      },
      {
        "CubeWorkinprocess.id": 8142,
        "CubeWorkinprocess.duedate": "2020-06-11T10:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS252.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS252.2"
      },
      {
        "CubeWorkinprocess.id": 8143,
        "CubeWorkinprocess.duedate": "2020-06-11T10:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS252.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS252.2"
      },
      {
        "CubeWorkinprocess.id": 8144,
        "CubeWorkinprocess.duedate": "2020-06-11T10:28:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS252.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS252.2"
      },
      {
        "CubeWorkinprocess.id": 8164,
        "CubeWorkinprocess.duedate": "2020-06-12T07:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE502.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE502.1"
      },
      {
        "CubeWorkinprocess.id": 8165,
        "CubeWorkinprocess.duedate": "2020-06-12T07:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE502.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE502.1"
      },
      {
        "CubeWorkinprocess.id": 8168,
        "CubeWorkinprocess.duedate": "2020-06-12T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.1"
      },
      {
        "CubeWorkinprocess.id": 8169,
        "CubeWorkinprocess.duedate": "2020-06-12T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.1"
      },
      {
        "CubeWorkinprocess.id": 8170,
        "CubeWorkinprocess.duedate": "2020-06-12T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS248.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS248.1"
      },
      {
        "CubeWorkinprocess.id": 8233,
        "CubeWorkinprocess.duedate": "2020-06-14T01:16:00.000",
        "CubeWorkinprocess.details": "Specification: 04-INT-103-00  PIA Kit |   ATTN: N/A  | P.O.: 318117 / 1 | Kit: 500193599 - CLEANED,PIA,SOURCE,TUBE,KIT,MSR | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "04-INT-103-00  PIA Kit",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8248,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8249,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8250,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8251,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8252,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8253,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8254,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8255,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8256,
        "CubeWorkinprocess.duedate": "2020-06-15T07:42:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.5"
      },
      {
        "CubeWorkinprocess.id": 8284,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8285,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8286,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8287,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8288,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8289,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8290,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8291,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8292,
        "CubeWorkinprocess.duedate": "2020-06-15T13:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS671.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS671.2"
      },
      {
        "CubeWorkinprocess.id": 8334,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8335,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8336,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8337,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8338,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8339,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8340,
        "CubeWorkinprocess.duedate": "2020-06-16T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE552.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE552.2"
      },
      {
        "CubeWorkinprocess.id": 8342,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8343,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8344,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8345,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8346,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8347,
        "CubeWorkinprocess.duedate": "2020-06-16T14:52:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.3"
      },
      {
        "CubeWorkinprocess.id": 8380,
        "CubeWorkinprocess.duedate": "2020-06-18T05:37:00.000",
        "CubeWorkinprocess.details": "Specification: Clean Test Parts 8-9-19 |   ATTN: N/A  | P.O.: 319649 / 1 | Kit: 500236801 - CLEANED,MSR,PIA,TERM,RING,INSULATOR,KI | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "Clean Test Parts 8-9-19",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8381,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8382,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8383,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8384,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8385,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8386,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8387,
        "CubeWorkinprocess.duedate": "2020-06-18T07:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT702.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT702.6"
      },
      {
        "CubeWorkinprocess.id": 8388,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8389,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8390,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8391,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8392,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8393,
        "CubeWorkinprocess.duedate": "2020-06-18T10:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS455.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS455.1"
      },
      {
        "CubeWorkinprocess.id": 8416,
        "CubeWorkinprocess.duedate": "2020-06-19T07:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.3"
      },
      {
        "CubeWorkinprocess.id": 8417,
        "CubeWorkinprocess.duedate": "2020-06-19T07:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.3"
      },
      {
        "CubeWorkinprocess.id": 8425,
        "CubeWorkinprocess.duedate": "2020-06-19T08:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.1"
      },
      {
        "CubeWorkinprocess.id": 8426,
        "CubeWorkinprocess.duedate": "2020-06-19T08:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.1"
      },
      {
        "CubeWorkinprocess.id": 8427,
        "CubeWorkinprocess.duedate": "2020-06-19T08:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR650.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR650.1"
      },
      {
        "CubeWorkinprocess.id": 8496,
        "CubeWorkinprocess.duedate": "2020-06-22T05:34:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 320288 / 7 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8497,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8498,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8499,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8500,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8501,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8502,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8503,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8504,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8505,
        "CubeWorkinprocess.duedate": "2020-06-22T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE360.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE360.2"
      },
      {
        "CubeWorkinprocess.id": 8522,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8523,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8524,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8525,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8526,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8527,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8528,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8529,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8530,
        "CubeWorkinprocess.duedate": "2020-06-22T12:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ANT702.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ANT702.2"
      },
      {
        "CubeWorkinprocess.id": 8531,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8532,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8533,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8534,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8535,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8536,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8537,
        "CubeWorkinprocess.duedate": "2020-06-22T15:01:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.3"
      },
      {
        "CubeWorkinprocess.id": 8604,
        "CubeWorkinprocess.duedate": "2020-06-23T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX711.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX711.3"
      },
      {
        "CubeWorkinprocess.id": 8605,
        "CubeWorkinprocess.duedate": "2020-06-23T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.3"
      },
      {
        "CubeWorkinprocess.id": 8606,
        "CubeWorkinprocess.duedate": "2020-06-23T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.3"
      },
      {
        "CubeWorkinprocess.id": 8607,
        "CubeWorkinprocess.duedate": "2020-06-23T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.3"
      },
      {
        "CubeWorkinprocess.id": 8608,
        "CubeWorkinprocess.duedate": "2020-06-23T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.3"
      },
      {
        "CubeWorkinprocess.id": 8619,
        "CubeWorkinprocess.duedate": "2020-06-23T15:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 8620,
        "CubeWorkinprocess.duedate": "2020-06-23T15:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 8621,
        "CubeWorkinprocess.duedate": "2020-06-23T15:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 8622,
        "CubeWorkinprocess.duedate": "2020-06-23T15:20:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 8624,
        "CubeWorkinprocess.duedate": "2020-06-22T01:53:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 320289 / 7 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8625,
        "CubeWorkinprocess.duedate": "2020-06-22T01:53:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 320289 / 8 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8626,
        "CubeWorkinprocess.duedate": "2020-06-22T04:44:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 320672 / 1 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8627,
        "CubeWorkinprocess.duedate": "2020-06-22T04:53:00.000",
        "CubeWorkinprocess.details": "Specification: GTO Foreline Pipe Cleaning |   ATTN: N/A  | P.O.: 320672 / 2 | Kit: GTO cx Foreline Clean | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "GTO Foreline Pipe Cleaning",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8628,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8629,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8630,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8631,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8632,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8633,
        "CubeWorkinprocess.duedate": "2020-06-24T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: LAT713.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "LAT713.2"
      },
      {
        "CubeWorkinprocess.id": 8654,
        "CubeWorkinprocess.duedate": "2020-06-24T13:33:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX757.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX757.4"
      },
      {
        "CubeWorkinprocess.id": 8655,
        "CubeWorkinprocess.duedate": "2020-06-24T13:33:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX757.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX757.4"
      },
      {
        "CubeWorkinprocess.id": 8656,
        "CubeWorkinprocess.duedate": "2020-06-24T13:33:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX757.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX757.4"
      },
      {
        "CubeWorkinprocess.id": 8664,
        "CubeWorkinprocess.duedate": "2020-06-25T02:16:00.000",
        "CubeWorkinprocess.details": "Specification: test1119-Test |   ATTN: N/A  | P.O.: 315629 / 6 | Kit: 633018122 - ELECTODE COVER, 300MM, HME, MSR CLEANE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "test1119-Test",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 8665,
        "CubeWorkinprocess.duedate": "2020-06-25T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.1"
      },
      {
        "CubeWorkinprocess.id": 8666,
        "CubeWorkinprocess.duedate": "2020-06-25T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.1"
      },
      {
        "CubeWorkinprocess.id": 8667,
        "CubeWorkinprocess.duedate": "2020-06-25T07:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT712.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT712.1"
      },
      {
        "CubeWorkinprocess.id": 8669,
        "CubeWorkinprocess.duedate": "2020-06-25T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 8670,
        "CubeWorkinprocess.duedate": "2020-06-25T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 8671,
        "CubeWorkinprocess.duedate": "2020-06-25T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 8672,
        "CubeWorkinprocess.duedate": "2020-06-25T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 8703,
        "CubeWorkinprocess.duedate": "2020-06-26T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT304.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT304.2"
      },
      {
        "CubeWorkinprocess.id": 8704,
        "CubeWorkinprocess.duedate": "2020-06-26T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT304.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT304.2"
      },
      {
        "CubeWorkinprocess.id": 8705,
        "CubeWorkinprocess.duedate": "2020-06-26T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: OXT304.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXT304.2"
      },
      {
        "CubeWorkinprocess.id": 8706,
        "CubeWorkinprocess.duedate": "2020-06-26T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT304.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT304.2"
      },
      {
        "CubeWorkinprocess.id": 8707,
        "CubeWorkinprocess.duedate": "2020-06-26T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXT304.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXT304.2"
      },
      {
        "CubeWorkinprocess.id": 8708,
        "CubeWorkinprocess.duedate": "2020-06-26T09:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS411.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS411.2"
      },
      {
        "CubeWorkinprocess.id": 8794,
        "CubeWorkinprocess.duedate": "2020-06-29T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT704.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT704.6"
      },
      {
        "CubeWorkinprocess.id": 8795,
        "CubeWorkinprocess.duedate": "2020-06-29T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT704.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT704.6"
      },
      {
        "CubeWorkinprocess.id": 8796,
        "CubeWorkinprocess.duedate": "2020-06-29T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT704.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT704.6"
      },
      {
        "CubeWorkinprocess.id": 8797,
        "CubeWorkinprocess.duedate": "2020-06-29T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT704.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT704.6"
      },
      {
        "CubeWorkinprocess.id": 8798,
        "CubeWorkinprocess.duedate": "2020-06-29T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1372944",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1372944"
      },
      {
        "CubeWorkinprocess.id": 8799,
        "CubeWorkinprocess.duedate": "2020-06-29T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1372944",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1372944"
      },
      {
        "CubeWorkinprocess.id": 8832,
        "CubeWorkinprocess.duedate": "2020-06-30T07:21:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX702.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX702.1"
      },
      {
        "CubeWorkinprocess.id": 8833,
        "CubeWorkinprocess.duedate": "2020-06-30T07:21:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX702.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX702.1"
      },
      {
        "CubeWorkinprocess.id": 8834,
        "CubeWorkinprocess.duedate": "2020-06-30T07:21:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX702.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX702.1"
      },
      {
        "CubeWorkinprocess.id": 8835,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8836,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8837,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8838,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8839,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8840,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8841,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8842,
        "CubeWorkinprocess.duedate": "2020-06-30T09:10:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS811.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS811.1"
      },
      {
        "CubeWorkinprocess.id": 8873,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8874,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8875,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8876,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8877,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8878,
        "CubeWorkinprocess.duedate": "2020-06-29T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500315834 | Tool:  | MTTN: 1374771",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1374771"
      },
      {
        "CubeWorkinprocess.id": 8879,
        "CubeWorkinprocess.duedate": "2020-07-01T09:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.2"
      },
      {
        "CubeWorkinprocess.id": 8880,
        "CubeWorkinprocess.duedate": "2020-07-01T09:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.2"
      },
      {
        "CubeWorkinprocess.id": 8881,
        "CubeWorkinprocess.duedate": "2020-07-01T09:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.2"
      },
      {
        "CubeWorkinprocess.id": 8882,
        "CubeWorkinprocess.duedate": "2020-07-01T09:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX711.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX711.2"
      },
      {
        "CubeWorkinprocess.id": 8904,
        "CubeWorkinprocess.duedate": "2020-07-01T13:48:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 8905,
        "CubeWorkinprocess.duedate": "2020-07-01T13:48:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 8906,
        "CubeWorkinprocess.duedate": "2020-07-01T13:48:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 8907,
        "CubeWorkinprocess.duedate": "2020-07-01T13:48:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS676.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS676.1"
      },
      {
        "CubeWorkinprocess.id": 8923,
        "CubeWorkinprocess.duedate": "2020-07-02T08:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX720.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX720.1"
      },
      {
        "CubeWorkinprocess.id": 8924,
        "CubeWorkinprocess.duedate": "2020-07-02T08:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX720.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX720.1"
      },
      {
        "CubeWorkinprocess.id": 8956,
        "CubeWorkinprocess.duedate": "2020-07-03T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX735.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX735.4"
      },
      {
        "CubeWorkinprocess.id": 8957,
        "CubeWorkinprocess.duedate": "2020-07-03T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.4"
      },
      {
        "CubeWorkinprocess.id": 8959,
        "CubeWorkinprocess.duedate": "2020-07-03T08:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: BCL406.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "BCL406.3"
      },
      {
        "CubeWorkinprocess.id": 9045,
        "CubeWorkinprocess.duedate": "2020-07-06T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX706.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX706.2"
      },
      {
        "CubeWorkinprocess.id": 9046,
        "CubeWorkinprocess.duedate": "2020-07-06T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX706.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX706.2"
      },
      {
        "CubeWorkinprocess.id": 9047,
        "CubeWorkinprocess.duedate": "2020-07-06T08:31:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT702.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT702.1"
      },
      {
        "CubeWorkinprocess.id": 9070,
        "CubeWorkinprocess.duedate": "2020-07-07T03:59:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 323844 / 2 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9071,
        "CubeWorkinprocess.duedate": "2020-07-07T04:01:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 323844 / 3 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9072,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9073,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9074,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9075,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9076,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9077,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9078,
        "CubeWorkinprocess.duedate": "2020-07-07T07:29:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.2"
      },
      {
        "CubeWorkinprocess.id": 9082,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9083,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9084,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9085,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9086,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9087,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9088,
        "CubeWorkinprocess.duedate": "2020-07-07T09:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.2"
      },
      {
        "CubeWorkinprocess.id": 9089,
        "CubeWorkinprocess.duedate": "2020-07-07T09:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO804.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO804.1"
      },
      {
        "CubeWorkinprocess.id": 9112,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9113,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9114,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9115,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9116,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9117,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9118,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9119,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9120,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9121,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9122,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9123,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9124,
        "CubeWorkinprocess.duedate": "2020-07-07T15:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS622.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS622.1"
      },
      {
        "CubeWorkinprocess.id": 9148,
        "CubeWorkinprocess.duedate": "2020-07-09T02:01:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 323844 / 3 | Kit: 500060121 - CLEANED,RC1,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9153,
        "CubeWorkinprocess.duedate": "2020-07-09T02:59:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 324033 / 3 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9157,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9158,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9159,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9160,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9161,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9162,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9163,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9164,
        "CubeWorkinprocess.duedate": "2020-07-09T07:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT715.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT715.6"
      },
      {
        "CubeWorkinprocess.id": 9282,
        "CubeWorkinprocess.duedate": "2020-07-13T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX758.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX758.4"
      },
      {
        "CubeWorkinprocess.id": 9283,
        "CubeWorkinprocess.duedate": "2020-07-13T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX758.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX758.4"
      },
      {
        "CubeWorkinprocess.id": 9284,
        "CubeWorkinprocess.duedate": "2020-07-13T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX758.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX758.4"
      },
      {
        "CubeWorkinprocess.id": 9285,
        "CubeWorkinprocess.duedate": "2020-07-13T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX758.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX758.4"
      },
      {
        "CubeWorkinprocess.id": 9286,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9287,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9288,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9289,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9290,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9291,
        "CubeWorkinprocess.duedate": "2020-07-13T10:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX717.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX717.1"
      },
      {
        "CubeWorkinprocess.id": 9313,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9314,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9315,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9316,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9317,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9318,
        "CubeWorkinprocess.duedate": "2020-07-13T14:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX825.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX825.3"
      },
      {
        "CubeWorkinprocess.id": 9323,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9324,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9325,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9326,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9327,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9328,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9329,
        "CubeWorkinprocess.duedate": "2020-07-14T09:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.6"
      },
      {
        "CubeWorkinprocess.id": 9370,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9371,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9372,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9373,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9374,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9375,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9376,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9377,
        "CubeWorkinprocess.duedate": "2020-07-14T14:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.1"
      },
      {
        "CubeWorkinprocess.id": 9378,
        "CubeWorkinprocess.duedate": "2020-07-13T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1379520",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1379520"
      },
      {
        "CubeWorkinprocess.id": 9403,
        "CubeWorkinprocess.duedate": "2020-07-15T15:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS257.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS257.1"
      },
      {
        "CubeWorkinprocess.id": 9404,
        "CubeWorkinprocess.duedate": "2020-07-15T15:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.1"
      },
      {
        "CubeWorkinprocess.id": 9405,
        "CubeWorkinprocess.duedate": "2020-07-15T15:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.1"
      },
      {
        "CubeWorkinprocess.id": 9406,
        "CubeWorkinprocess.duedate": "2020-07-15T15:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.1"
      },
      {
        "CubeWorkinprocess.id": 9407,
        "CubeWorkinprocess.duedate": "2020-07-15T15:00:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS257.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS257.1"
      },
      {
        "CubeWorkinprocess.id": 9416,
        "CubeWorkinprocess.duedate": "2020-07-16T07:16:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1375485",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1375485"
      },
      {
        "CubeWorkinprocess.id": 9417,
        "CubeWorkinprocess.duedate": "2020-07-16T10:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX735.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX735.1"
      },
      {
        "CubeWorkinprocess.id": 9444,
        "CubeWorkinprocess.duedate": "2020-07-17T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE504.2"
      },
      {
        "CubeWorkinprocess.id": 9445,
        "CubeWorkinprocess.duedate": "2020-07-17T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE504.2"
      },
      {
        "CubeWorkinprocess.id": 9446,
        "CubeWorkinprocess.duedate": "2020-07-17T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE504.2"
      },
      {
        "CubeWorkinprocess.id": 9447,
        "CubeWorkinprocess.duedate": "2020-07-17T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE504.2"
      },
      {
        "CubeWorkinprocess.id": 9448,
        "CubeWorkinprocess.duedate": "2020-07-17T07:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE504.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE504.2"
      },
      {
        "CubeWorkinprocess.id": 9449,
        "CubeWorkinprocess.duedate": "2020-07-15T07:47:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1377887",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1377887"
      },
      {
        "CubeWorkinprocess.id": 9450,
        "CubeWorkinprocess.duedate": "2020-07-15T09:14:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1380685",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1380685"
      },
      {
        "CubeWorkinprocess.id": 9452,
        "CubeWorkinprocess.duedate": "2020-07-17T10:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: CAR610.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "CAR610.1"
      },
      {
        "CubeWorkinprocess.id": 9453,
        "CubeWorkinprocess.duedate": "2020-07-17T10:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR610.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR610.1"
      },
      {
        "CubeWorkinprocess.id": 9454,
        "CubeWorkinprocess.duedate": "2020-07-17T10:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR610.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR610.1"
      },
      {
        "CubeWorkinprocess.id": 9455,
        "CubeWorkinprocess.duedate": "2020-07-17T10:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: CAR610.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "CAR610.1"
      },
      {
        "CubeWorkinprocess.id": 9532,
        "CubeWorkinprocess.duedate": "2020-07-20T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 9533,
        "CubeWorkinprocess.duedate": "2020-07-20T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 9534,
        "CubeWorkinprocess.duedate": "2020-07-20T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 9535,
        "CubeWorkinprocess.duedate": "2020-07-20T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 9536,
        "CubeWorkinprocess.duedate": "2020-07-20T07:11:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE514.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE514.2"
      },
      {
        "CubeWorkinprocess.id": 9546,
        "CubeWorkinprocess.duedate": "2020-07-20T11:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO811.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO811.5"
      },
      {
        "CubeWorkinprocess.id": 9547,
        "CubeWorkinprocess.duedate": "2020-07-20T11:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO811.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO811.5"
      },
      {
        "CubeWorkinprocess.id": 9548,
        "CubeWorkinprocess.duedate": "2020-07-20T11:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO811.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO811.5"
      },
      {
        "CubeWorkinprocess.id": 9549,
        "CubeWorkinprocess.duedate": "2020-07-20T11:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO811.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO811.5"
      },
      {
        "CubeWorkinprocess.id": 9550,
        "CubeWorkinprocess.duedate": "2020-07-20T11:13:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO811.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO811.5"
      },
      {
        "CubeWorkinprocess.id": 9574,
        "CubeWorkinprocess.duedate": "2020-07-21T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX707.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX707.3"
      },
      {
        "CubeWorkinprocess.id": 9575,
        "CubeWorkinprocess.duedate": "2020-07-21T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX707.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX707.3"
      },
      {
        "CubeWorkinprocess.id": 9576,
        "CubeWorkinprocess.duedate": "2020-07-21T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX707.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX707.3"
      },
      {
        "CubeWorkinprocess.id": 9577,
        "CubeWorkinprocess.duedate": "2020-07-21T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX707.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX707.3"
      },
      {
        "CubeWorkinprocess.id": 9578,
        "CubeWorkinprocess.duedate": "2020-07-21T09:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX707.3",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX707.3"
      },
      {
        "CubeWorkinprocess.id": 9593,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9594,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9595,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9596,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9597,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9598,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9599,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9600,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9601,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9602,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9603,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9604,
        "CubeWorkinprocess.duedate": "2020-07-21T13:51:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS243.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS243.2"
      },
      {
        "CubeWorkinprocess.id": 9659,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9660,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9661,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9662,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9663,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9664,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9665,
        "CubeWorkinprocess.duedate": "2020-07-23T07:23:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX722.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX722.4"
      },
      {
        "CubeWorkinprocess.id": 9677,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9678,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9679,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9680,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9681,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9682,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9683,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9684,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9685,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9686,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9687,
        "CubeWorkinprocess.duedate": "2020-07-23T11:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS454.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS454.2"
      },
      {
        "CubeWorkinprocess.id": 9697,
        "CubeWorkinprocess.duedate": "2020-07-22T08:19:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1382127",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1382127"
      },
      {
        "CubeWorkinprocess.id": 9698,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9699,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9700,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9701,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9702,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9703,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9704,
        "CubeWorkinprocess.duedate": "2020-07-24T08:47:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX836.4",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX836.4"
      },
      {
        "CubeWorkinprocess.id": 9808,
        "CubeWorkinprocess.duedate": "2020-07-27T05:06:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-316 RC1/RC2 Longlife Coldtrap |   ATTN: N/A  | P.O.: 327050 / 3 | Kit: 500060122 - CLEANED,RC2,COLDTRAP,LONGLIFE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-316 RC1/RC2 Longlife Coldtrap",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9813,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9814,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9815,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9816,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9817,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9818,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9819,
        "CubeWorkinprocess.duedate": "2020-07-27T07:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT709.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT709.5"
      },
      {
        "CubeWorkinprocess.id": 9836,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9837,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9838,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9839,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9840,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9841,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9842,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9843,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9844,
        "CubeWorkinprocess.duedate": "2020-07-27T13:14:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS236.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS236.2"
      },
      {
        "CubeWorkinprocess.id": 9888,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9889,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9890,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9891,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9892,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9893,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9894,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9895,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9896,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9897,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9898,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9899,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9900,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9901,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9902,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9903,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9904,
        "CubeWorkinprocess.duedate": "2020-07-28T11:34:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: TAO895.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO895.6"
      },
      {
        "CubeWorkinprocess.id": 9920,
        "CubeWorkinprocess.duedate": "2020-07-24T03:17:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327370 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9921,
        "CubeWorkinprocess.duedate": "2020-07-24T05:10:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327489 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9922,
        "CubeWorkinprocess.duedate": "2020-07-24T05:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327501 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9923,
        "CubeWorkinprocess.duedate": "2020-07-24T05:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327505 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9928,
        "CubeWorkinprocess.duedate": "2020-07-27T05:30:00.000",
        "CubeWorkinprocess.details": "Specification: ASM VFN FLANGE - LSR GDR B  (01-INT-483)   |   ATTN: N/A  | P.O.: 327520 / 2 | Kit: 500083406 LSR LINER SUS RING, IMP | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "ASM VFN FLANGE - LSR GDR B  (01-INT-483)  ",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9929,
        "CubeWorkinprocess.duedate": "2020-07-27T10:48:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500315834 | Tool:  | MTTN: 1381425",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1381425"
      },
      {
        "CubeWorkinprocess.id": 9961,
        "CubeWorkinprocess.duedate": "2020-07-25T02:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327701 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9962,
        "CubeWorkinprocess.duedate": "2020-07-25T02:18:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327702 / 2 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9963,
        "CubeWorkinprocess.duedate": "2020-07-25T02:20:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327703 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9966,
        "CubeWorkinprocess.duedate": "2020-07-25T02:39:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327489 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9967,
        "CubeWorkinprocess.duedate": "2020-07-25T02:40:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327501 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9969,
        "CubeWorkinprocess.duedate": "2020-07-25T03:42:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327702 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9975,
        "CubeWorkinprocess.duedate": "2020-07-25T05:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327706 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9976,
        "CubeWorkinprocess.duedate": "2020-07-25T05:02:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327707 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9977,
        "CubeWorkinprocess.duedate": "2020-07-25T05:04:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327709 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9978,
        "CubeWorkinprocess.duedate": "2020-07-25T05:50:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327706 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9979,
        "CubeWorkinprocess.duedate": "2020-07-25T05:52:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327707 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9980,
        "CubeWorkinprocess.duedate": "2020-07-25T05:53:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 327709 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 9983,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9984,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9985,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9986,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9987,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9988,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9989,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9990,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9991,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9992,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 9993,
        "CubeWorkinprocess.duedate": "2020-07-30T07:43:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: OXS680.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS680.2"
      },
      {
        "CubeWorkinprocess.id": 10021,
        "CubeWorkinprocess.duedate": "2020-07-31T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: REX737.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX737.5"
      },
      {
        "CubeWorkinprocess.id": 10022,
        "CubeWorkinprocess.duedate": "2020-07-31T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX737.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX737.5"
      },
      {
        "CubeWorkinprocess.id": 10023,
        "CubeWorkinprocess.duedate": "2020-07-31T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX737.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX737.5"
      },
      {
        "CubeWorkinprocess.id": 10024,
        "CubeWorkinprocess.duedate": "2020-07-31T07:19:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX737.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX737.5"
      },
      {
        "CubeWorkinprocess.id": 10025,
        "CubeWorkinprocess.duedate": "2020-07-31T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 10026,
        "CubeWorkinprocess.duedate": "2020-07-31T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 10027,
        "CubeWorkinprocess.duedate": "2020-07-31T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 10028,
        "CubeWorkinprocess.duedate": "2020-07-31T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 10029,
        "CubeWorkinprocess.duedate": "2020-07-31T09:53:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX816.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX816.1"
      },
      {
        "CubeWorkinprocess.id": 10030,
        "CubeWorkinprocess.duedate": "2020-07-29T10:20:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1383859",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1383859"
      },
      {
        "CubeWorkinprocess.id": 10150,
        "CubeWorkinprocess.duedate": "2020-07-28T16:20:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 123456 | Kit: V2 Heated Valve Refurb | Tool:  | MTTN: 1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": "1"
      },
      {
        "CubeWorkinprocess.id": 10203,
        "CubeWorkinprocess.duedate": "2020-07-28T23:34:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328007 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10206,
        "CubeWorkinprocess.duedate": "2020-07-28T23:42:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328007 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10208,
        "CubeWorkinprocess.duedate": "2020-07-28T23:45:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328020 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10210,
        "CubeWorkinprocess.duedate": "2020-07-28T23:48:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328025 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10212,
        "CubeWorkinprocess.duedate": "2020-07-28T23:53:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328028 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10213,
        "CubeWorkinprocess.duedate": "2020-07-28T23:55:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328034 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10215,
        "CubeWorkinprocess.duedate": "2020-07-29T02:05:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328165 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10221,
        "CubeWorkinprocess.duedate": "2020-07-29T02:19:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328146 / 2 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10222,
        "CubeWorkinprocess.duedate": "2020-07-29T02:20:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328181 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10223,
        "CubeWorkinprocess.duedate": "2020-07-29T02:21:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328182 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10225,
        "CubeWorkinprocess.duedate": "2020-07-29T02:23:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328186 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10226,
        "CubeWorkinprocess.duedate": "2020-07-29T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328188 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10227,
        "CubeWorkinprocess.duedate": "2020-07-29T02:27:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328190 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10228,
        "CubeWorkinprocess.duedate": "2020-07-29T02:29:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328191 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10229,
        "CubeWorkinprocess.duedate": "2020-07-29T02:31:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328193 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10230,
        "CubeWorkinprocess.duedate": "2020-07-29T02:36:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328194 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10231,
        "CubeWorkinprocess.duedate": "2020-07-29T02:42:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328196 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10232,
        "CubeWorkinprocess.duedate": "2020-08-01T02:43:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328146 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10233,
        "CubeWorkinprocess.duedate": "2020-08-01T02:44:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10234,
        "CubeWorkinprocess.duedate": "2020-08-01T02:50:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10235,
        "CubeWorkinprocess.duedate": "2020-08-01T02:52:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10236,
        "CubeWorkinprocess.duedate": "2020-08-01T03:02:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10237,
        "CubeWorkinprocess.duedate": "2020-08-01T03:03:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 5 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10238,
        "CubeWorkinprocess.duedate": "2020-08-01T03:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 6 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10239,
        "CubeWorkinprocess.duedate": "2020-08-01T03:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328214 / 7 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10240,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10241,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10242,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10243,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10244,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10245,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10246,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10247,
        "CubeWorkinprocess.duedate": "2020-08-03T07:05:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: REX715.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX715.2"
      },
      {
        "CubeWorkinprocess.id": 10269,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10270,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10271,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10272,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10273,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10274,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10275,
        "CubeWorkinprocess.duedate": "2020-08-03T12:49:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS433.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS433.1"
      },
      {
        "CubeWorkinprocess.id": 10278,
        "CubeWorkinprocess.duedate": "2020-07-29T23:44:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328281 / 2 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10279,
        "CubeWorkinprocess.duedate": "2020-07-30T01:56:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328459 / 3 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10280,
        "CubeWorkinprocess.duedate": "2020-07-30T01:57:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328463 / 2 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10281,
        "CubeWorkinprocess.duedate": "2020-07-30T01:59:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328465 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10282,
        "CubeWorkinprocess.duedate": "2020-07-30T02:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328468 / 3 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10283,
        "CubeWorkinprocess.duedate": "2020-08-02T02:04:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328459 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10284,
        "CubeWorkinprocess.duedate": "2020-08-02T02:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328459 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10285,
        "CubeWorkinprocess.duedate": "2020-08-02T02:07:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328463 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10286,
        "CubeWorkinprocess.duedate": "2020-08-02T02:39:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328468 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10287,
        "CubeWorkinprocess.duedate": "2020-08-02T02:42:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328468 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10288,
        "CubeWorkinprocess.duedate": "2020-07-30T02:44:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328466 / 2 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10291,
        "CubeWorkinprocess.duedate": "2020-08-02T02:56:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10292,
        "CubeWorkinprocess.duedate": "2020-08-02T02:58:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10293,
        "CubeWorkinprocess.duedate": "2020-08-02T02:59:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10294,
        "CubeWorkinprocess.duedate": "2020-08-02T04:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 5 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10295,
        "CubeWorkinprocess.duedate": "2020-08-02T04:06:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 6 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10296,
        "CubeWorkinprocess.duedate": "2020-08-02T04:07:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328442 / 7 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10297,
        "CubeWorkinprocess.duedate": "2020-08-02T04:33:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328546 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10298,
        "CubeWorkinprocess.duedate": "2020-07-30T06:59:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328542 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10299,
        "CubeWorkinprocess.duedate": "2020-07-30T07:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328543 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10328,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10329,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10330,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10331,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10332,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10333,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10334,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10335,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10336,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10337,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10338,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10339,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10340,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10341,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10342,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10343,
        "CubeWorkinprocess.duedate": "2020-08-04T11:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT711.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT711.6"
      },
      {
        "CubeWorkinprocess.id": 10353,
        "CubeWorkinprocess.duedate": "2020-07-30T18:04:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577988 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872572/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872572/1"
      },
      {
        "CubeWorkinprocess.id": 10354,
        "CubeWorkinprocess.duedate": "2020-07-30T18:05:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577981 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872952/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872952/1"
      },
      {
        "CubeWorkinprocess.id": 10355,
        "CubeWorkinprocess.duedate": "2020-07-30T18:07:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577967 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872772/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872772/1"
      },
      {
        "CubeWorkinprocess.id": 10356,
        "CubeWorkinprocess.duedate": "2020-07-31T02:32:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328655 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10357,
        "CubeWorkinprocess.duedate": "2020-07-31T02:34:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328656 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10358,
        "CubeWorkinprocess.duedate": "2020-07-31T02:36:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328651 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10359,
        "CubeWorkinprocess.duedate": "2020-07-31T02:37:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328653 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10360,
        "CubeWorkinprocess.duedate": "2020-07-31T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328654 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10361,
        "CubeWorkinprocess.duedate": "2020-07-31T02:44:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 328663 / 1 | Kit: 500419028 - CL,ICE Small kit 1274 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10362,
        "CubeWorkinprocess.duedate": "2020-08-03T04:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328281 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10363,
        "CubeWorkinprocess.duedate": "2020-07-31T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577970 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872601/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872601/1"
      },
      {
        "CubeWorkinprocess.id": 10364,
        "CubeWorkinprocess.duedate": "2020-07-31T07:28:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577976 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872556/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872556/1"
      },
      {
        "CubeWorkinprocess.id": 10365,
        "CubeWorkinprocess.duedate": "2020-07-31T07:30:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577974 | Kit: Clean REX BT Kit | Tool:  | MTTN: 872554/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872554/1"
      },
      {
        "CubeWorkinprocess.id": 10366,
        "CubeWorkinprocess.duedate": "2020-07-31T07:32:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577993 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872573/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872573/1"
      },
      {
        "CubeWorkinprocess.id": 10367,
        "CubeWorkinprocess.duedate": "2020-07-31T07:35:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578009 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872637/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872637/1"
      },
      {
        "CubeWorkinprocess.id": 10368,
        "CubeWorkinprocess.duedate": "2020-07-31T07:36:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577982 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872953/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872953/1"
      },
      {
        "CubeWorkinprocess.id": 10369,
        "CubeWorkinprocess.duedate": "2020-07-31T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577985 | Kit: Clean REX SP Kit | Tool:  | MTTN: 872701/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872701/1"
      },
      {
        "CubeWorkinprocess.id": 10370,
        "CubeWorkinprocess.duedate": "2020-07-31T07:45:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577955 | Kit: Clean REX BT Kit | Tool:  | MTTN: 872770/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872770/1"
      },
      {
        "CubeWorkinprocess.id": 10371,
        "CubeWorkinprocess.duedate": "2020-07-31T07:46:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577956 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872790/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872790/1"
      },
      {
        "CubeWorkinprocess.id": 10372,
        "CubeWorkinprocess.duedate": "2020-07-31T07:47:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577957 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872625/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872625/1"
      },
      {
        "CubeWorkinprocess.id": 10373,
        "CubeWorkinprocess.duedate": "2020-07-31T07:48:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577959 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872771/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872771/1"
      },
      {
        "CubeWorkinprocess.id": 10374,
        "CubeWorkinprocess.duedate": "2020-07-31T07:49:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577960 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872787/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872787/1"
      },
      {
        "CubeWorkinprocess.id": 10375,
        "CubeWorkinprocess.duedate": "2020-07-31T07:57:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577838 | Kit: Clean Rex ME Kit | Tool:  | MTTN: 872910/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872910/1"
      },
      {
        "CubeWorkinprocess.id": 10376,
        "CubeWorkinprocess.duedate": "2020-07-31T07:58:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577867 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872773/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872773/1"
      },
      {
        "CubeWorkinprocess.id": 10377,
        "CubeWorkinprocess.duedate": "2020-07-31T07:59:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 577856 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 877868/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "877868/1"
      },
      {
        "CubeWorkinprocess.id": 10378,
        "CubeWorkinprocess.duedate": "2020-07-31T08:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578242 | Kit: Clean REX SS Smalls Kit | Tool:  | MTTN: 872602/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "872602/1"
      },
      {
        "CubeWorkinprocess.id": 10379,
        "CubeWorkinprocess.duedate": "2020-08-03T08:45:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500249975 | Tool:  | MTTN: 1385077",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1385077"
      },
      {
        "CubeWorkinprocess.id": 10380,
        "CubeWorkinprocess.duedate": "2020-07-31T11:12:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578348 | Kit: Rex cd Kit 633016422 | Tool:  | MTTN: 873318/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873318/1"
      },
      {
        "CubeWorkinprocess.id": 10382,
        "CubeWorkinprocess.duedate": "2020-07-31T11:23:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578298 | Kit: Rex cd Kit 633016422 | Tool:  | MTTN: 873418/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873418/1"
      },
      {
        "CubeWorkinprocess.id": 10383,
        "CubeWorkinprocess.duedate": "2020-07-31T11:25:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578315 | Kit: Rex CO  633014639 | Tool:  | MTTN: 873370/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873370/1"
      },
      {
        "CubeWorkinprocess.id": 10384,
        "CubeWorkinprocess.duedate": "2020-07-31T11:27:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578301 | Kit: 1272 REXcx SMALL SS KIT 633014638 | Tool:  | MTTN: 873419/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873419/1"
      },
      {
        "CubeWorkinprocess.id": 10385,
        "CubeWorkinprocess.duedate": "2020-07-31T11:28:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578319 | Kit: 1272 REXcx SMALL SS KIT 633014638 | Tool:  | MTTN: 873371/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873371/1"
      },
      {
        "CubeWorkinprocess.id": 10386,
        "CubeWorkinprocess.duedate": "2020-07-31T11:30:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1272 |   ATTN: N/A  | P.O.: 578357 | Kit: 1272 REXcx SMALL SS KIT 633014638 | Tool:  | MTTN: 873316/1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1272",
        "CubeWorkinprocess.mttn": "873316/1"
      },
      {
        "CubeWorkinprocess.id": 10432,
        "CubeWorkinprocess.duedate": "2020-08-01T02:16:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328780 / 4 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10433,
        "CubeWorkinprocess.duedate": "2020-08-01T02:17:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328788 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10434,
        "CubeWorkinprocess.duedate": "2020-08-04T02:18:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328780 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10435,
        "CubeWorkinprocess.duedate": "2020-08-04T02:24:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328780 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10436,
        "CubeWorkinprocess.duedate": "2020-08-04T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 328780 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10437,
        "CubeWorkinprocess.duedate": "2020-08-01T02:26:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328784 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10438,
        "CubeWorkinprocess.duedate": "2020-08-01T02:27:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328785 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10439,
        "CubeWorkinprocess.duedate": "2020-08-01T02:28:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 328786 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10440,
        "CubeWorkinprocess.duedate": "2020-08-06T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS451.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS451.2"
      },
      {
        "CubeWorkinprocess.id": 10441,
        "CubeWorkinprocess.duedate": "2020-08-06T07:09:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: OXS451.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "OXS451.2"
      },
      {
        "CubeWorkinprocess.id": 10450,
        "CubeWorkinprocess.duedate": "2020-08-06T11:05:00.000",
        "CubeWorkinprocess.details": "Specification: IMR Spin Chuck |   ATTN: N/A  | P.O.: 12456 | Kit: IMR Spin Chuck 500290511 | Tool:  | MTTN: 1383174",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "IMR Spin Chuck",
        "CubeWorkinprocess.mttn": "1383174"
      },
      {
        "CubeWorkinprocess.id": 10451,
        "CubeWorkinprocess.duedate": "2020-08-06T11:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.1"
      },
      {
        "CubeWorkinprocess.id": 10452,
        "CubeWorkinprocess.duedate": "2020-08-06T11:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.1"
      },
      {
        "CubeWorkinprocess.id": 10453,
        "CubeWorkinprocess.duedate": "2020-08-06T11:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT714.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT714.1"
      },
      {
        "CubeWorkinprocess.id": 10494,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10495,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10496,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10497,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10498,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10499,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10500,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10501,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10502,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10503,
        "CubeWorkinprocess.duedate": "2020-08-07T07:38:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT710.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT710.6"
      },
      {
        "CubeWorkinprocess.id": 10561,
        "CubeWorkinprocess.duedate": "2020-08-04T02:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329178 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10562,
        "CubeWorkinprocess.duedate": "2020-08-04T02:14:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329180 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10563,
        "CubeWorkinprocess.duedate": "2020-08-04T02:15:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329181 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10564,
        "CubeWorkinprocess.duedate": "2020-08-04T02:17:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329182 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10566,
        "CubeWorkinprocess.duedate": "2020-08-07T02:19:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329112 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10567,
        "CubeWorkinprocess.duedate": "2020-08-07T02:20:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329112 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10568,
        "CubeWorkinprocess.duedate": "2020-08-07T02:22:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329112 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10569,
        "CubeWorkinprocess.duedate": "2020-08-07T02:23:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329112 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10570,
        "CubeWorkinprocess.duedate": "2020-08-07T02:24:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329112 / 5 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10571,
        "CubeWorkinprocess.duedate": "2020-08-04T02:29:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329109 / 3 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10572,
        "CubeWorkinprocess.duedate": "2020-08-04T02:30:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329184 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10573,
        "CubeWorkinprocess.duedate": "2020-08-04T02:32:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329185 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10574,
        "CubeWorkinprocess.duedate": "2020-08-07T02:34:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329109 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10575,
        "CubeWorkinprocess.duedate": "2020-08-07T02:35:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329109 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10599,
        "CubeWorkinprocess.duedate": "2020-08-05T00:41:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329353 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10600,
        "CubeWorkinprocess.duedate": "2020-08-05T02:46:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329375 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10601,
        "CubeWorkinprocess.duedate": "2020-08-10T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT717.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT717.5"
      },
      {
        "CubeWorkinprocess.id": 10602,
        "CubeWorkinprocess.duedate": "2020-08-10T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT717.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT717.5"
      },
      {
        "CubeWorkinprocess.id": 10603,
        "CubeWorkinprocess.duedate": "2020-08-10T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT717.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT717.5"
      },
      {
        "CubeWorkinprocess.id": 10604,
        "CubeWorkinprocess.duedate": "2020-08-10T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT717.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT717.5"
      },
      {
        "CubeWorkinprocess.id": 10605,
        "CubeWorkinprocess.duedate": "2020-08-10T07:07:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT717.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT717.5"
      },
      {
        "CubeWorkinprocess.id": 10606,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10607,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10608,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10609,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10610,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10611,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10612,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10613,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10614,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10615,
        "CubeWorkinprocess.duedate": "2020-08-10T11:08:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: TAO804.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "TAO804.2"
      },
      {
        "CubeWorkinprocess.id": 10641,
        "CubeWorkinprocess.duedate": "2020-08-06T01:34:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329504 / 5 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10642,
        "CubeWorkinprocess.duedate": "2020-08-09T01:36:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329504 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10643,
        "CubeWorkinprocess.duedate": "2020-08-09T01:37:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329504 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10644,
        "CubeWorkinprocess.duedate": "2020-08-09T01:39:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329504 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10645,
        "CubeWorkinprocess.duedate": "2020-08-09T01:41:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329504 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10675,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10676,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10677,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10678,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10679,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10680,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10681,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10682,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10683,
        "CubeWorkinprocess.duedate": "2020-08-11T13:58:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ICE313.1",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ICE313.1"
      },
      {
        "CubeWorkinprocess.id": 10695,
        "CubeWorkinprocess.duedate": "2020-08-07T02:50:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329650 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10696,
        "CubeWorkinprocess.duedate": "2020-08-07T02:51:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329655 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10698,
        "CubeWorkinprocess.duedate": "2020-08-07T02:55:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329659 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10699,
        "CubeWorkinprocess.duedate": "2020-08-07T02:57:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329661 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10700,
        "CubeWorkinprocess.duedate": "2020-08-07T02:59:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329662 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10701,
        "CubeWorkinprocess.duedate": "2020-08-07T03:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329663 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10702,
        "CubeWorkinprocess.duedate": "2020-08-07T06:24:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329713 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10703,
        "CubeWorkinprocess.duedate": "2020-08-07T06:26:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329714 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10704,
        "CubeWorkinprocess.duedate": "2020-08-10T06:54:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: TEST 854258 | Kit: 0247-02455 - Litmas RPS Cu | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10708,
        "CubeWorkinprocess.duedate": "2020-08-12T11:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Non-Cu | Tool:  | MTTN: ONT708.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "ONT708.2"
      },
      {
        "CubeWorkinprocess.id": 10709,
        "CubeWorkinprocess.duedate": "2020-08-12T11:46:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.2",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.2"
      },
      {
        "CubeWorkinprocess.id": 10725,
        "CubeWorkinprocess.duedate": "2020-08-12T14:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.5"
      },
      {
        "CubeWorkinprocess.id": 10726,
        "CubeWorkinprocess.duedate": "2020-08-12T14:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.5"
      },
      {
        "CubeWorkinprocess.id": 10727,
        "CubeWorkinprocess.duedate": "2020-08-12T14:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.5"
      },
      {
        "CubeWorkinprocess.id": 10728,
        "CubeWorkinprocess.duedate": "2020-08-12T14:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.5"
      },
      {
        "CubeWorkinprocess.id": 10729,
        "CubeWorkinprocess.duedate": "2020-08-12T14:12:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX818.5",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX818.5"
      },
      {
        "CubeWorkinprocess.id": 10751,
        "CubeWorkinprocess.duedate": "2020-08-08T01:55:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329790 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10752,
        "CubeWorkinprocess.duedate": "2020-08-08T02:00:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329792 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10753,
        "CubeWorkinprocess.duedate": "2020-08-11T02:08:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329820 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10754,
        "CubeWorkinprocess.duedate": "2020-08-11T02:20:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329820 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10755,
        "CubeWorkinprocess.duedate": "2020-08-11T02:22:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329820 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10756,
        "CubeWorkinprocess.duedate": "2020-08-11T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329820 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10757,
        "CubeWorkinprocess.duedate": "2020-08-11T02:30:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 329820 / 5 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10759,
        "CubeWorkinprocess.duedate": "2020-08-08T02:37:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329841 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10760,
        "CubeWorkinprocess.duedate": "2020-08-08T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 329843 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10761,
        "CubeWorkinprocess.duedate": "2020-08-13T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: REX817.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "REX817.6"
      },
      {
        "CubeWorkinprocess.id": 10762,
        "CubeWorkinprocess.duedate": "2020-08-13T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX817.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX817.6"
      },
      {
        "CubeWorkinprocess.id": 10763,
        "CubeWorkinprocess.duedate": "2020-08-13T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX817.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX817.6"
      },
      {
        "CubeWorkinprocess.id": 10764,
        "CubeWorkinprocess.duedate": "2020-08-13T07:27:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: REX817.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "REX817.6"
      },
      {
        "CubeWorkinprocess.id": 10765,
        "CubeWorkinprocess.duedate": "2020-08-13T07:41:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Standard Core - Copper | Tool:  | MTTN: TAO875.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": "TAO875.6"
      },
      {
        "CubeWorkinprocess.id": 10790,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10791,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Copper | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10792,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10793,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10794,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10795,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10796,
        "CubeWorkinprocess.duedate": "2020-08-14T07:55:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Thick Core |   ATTN: N/A  | P.O.: 120519 | Kit: AZ RPS Thick Core -Non-Cu | Tool:  | MTTN: ONT708.6",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Thick Core",
        "CubeWorkinprocess.mttn": "ONT708.6"
      },
      {
        "CubeWorkinprocess.id": 10872,
        "CubeWorkinprocess.duedate": "2020-08-14T00:26:00.000",
        "CubeWorkinprocess.details": "Specification: RPS Standard Core |   ATTN: N/A  | P.O.: TAO204.2 1319192 | Kit: 0247-02455 - Litmas RPS Cu | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "RPS Standard Core",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10874,
        "CubeWorkinprocess.duedate": "2020-08-11T02:31:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330117 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10875,
        "CubeWorkinprocess.duedate": "2020-08-11T02:33:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330118 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10876,
        "CubeWorkinprocess.duedate": "2020-08-11T02:35:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330120 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10877,
        "CubeWorkinprocess.duedate": "2020-08-11T02:37:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330123 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10878,
        "CubeWorkinprocess.duedate": "2020-08-11T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330152 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10879,
        "CubeWorkinprocess.duedate": "2020-08-11T02:45:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330119 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10880,
        "CubeWorkinprocess.duedate": "2020-08-14T02:47:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10881,
        "CubeWorkinprocess.duedate": "2020-08-14T02:49:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10882,
        "CubeWorkinprocess.duedate": "2020-08-14T02:53:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10883,
        "CubeWorkinprocess.duedate": "2020-08-14T02:55:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10885,
        "CubeWorkinprocess.duedate": "2020-08-14T02:59:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 5 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10886,
        "CubeWorkinprocess.duedate": "2020-08-14T03:02:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330153 / 6 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10887,
        "CubeWorkinprocess.duedate": "2020-08-11T04:55:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330119 / 1 | Kit: 633018748 - CLND,CU,REX,TUBES-KIT  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10925,
        "CubeWorkinprocess.duedate": "2020-08-12T02:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330277 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10926,
        "CubeWorkinprocess.duedate": "2020-08-12T02:04:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330294 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10927,
        "CubeWorkinprocess.duedate": "2020-08-12T02:07:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330298 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10928,
        "CubeWorkinprocess.duedate": "2020-08-12T02:09:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330298 / 2 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10946,
        "CubeWorkinprocess.duedate": "2020-08-12T05:08:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330156 / 2 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 10947,
        "CubeWorkinprocess.duedate": "2020-08-12T05:29:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330156 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11002,
        "CubeWorkinprocess.duedate": "2020-08-12T23:56:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330363 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11003,
        "CubeWorkinprocess.duedate": "2020-08-12T23:57:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330364 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11004,
        "CubeWorkinprocess.duedate": "2020-08-12T23:58:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330365 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11005,
        "CubeWorkinprocess.duedate": "2020-08-13T00:00:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330366 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11006,
        "CubeWorkinprocess.duedate": "2020-08-13T00:01:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330367 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11007,
        "CubeWorkinprocess.duedate": "2020-08-13T00:02:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330368 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11008,
        "CubeWorkinprocess.duedate": "2020-08-13T00:03:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330369 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11009,
        "CubeWorkinprocess.duedate": "2020-08-13T00:04:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330370 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11010,
        "CubeWorkinprocess.duedate": "2020-08-13T00:05:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330371 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11011,
        "CubeWorkinprocess.duedate": "2020-08-13T00:06:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330372 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11012,
        "CubeWorkinprocess.duedate": "2020-08-13T00:07:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330373 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11013,
        "CubeWorkinprocess.duedate": "2020-08-13T00:32:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330465 / 13 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11015,
        "CubeWorkinprocess.duedate": "2020-08-16T02:04:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11016,
        "CubeWorkinprocess.duedate": "2020-08-16T02:05:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11017,
        "CubeWorkinprocess.duedate": "2020-08-16T02:06:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11018,
        "CubeWorkinprocess.duedate": "2020-08-16T02:08:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 5 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11019,
        "CubeWorkinprocess.duedate": "2020-08-16T02:10:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 6 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11020,
        "CubeWorkinprocess.duedate": "2020-08-16T02:13:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 7 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11021,
        "CubeWorkinprocess.duedate": "2020-08-16T02:14:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 8 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11022,
        "CubeWorkinprocess.duedate": "2020-08-16T02:16:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 9 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11023,
        "CubeWorkinprocess.duedate": "2020-08-16T02:18:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 10 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11024,
        "CubeWorkinprocess.duedate": "2020-08-16T02:21:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 13 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11025,
        "CubeWorkinprocess.duedate": "2020-08-16T02:22:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 14 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11026,
        "CubeWorkinprocess.duedate": "2020-08-16T02:24:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330330 / 15 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11027,
        "CubeWorkinprocess.duedate": "2020-08-16T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330510 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11028,
        "CubeWorkinprocess.duedate": "2020-08-16T02:30:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11029,
        "CubeWorkinprocess.duedate": "2020-08-16T02:32:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11030,
        "CubeWorkinprocess.duedate": "2020-08-16T02:35:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11031,
        "CubeWorkinprocess.duedate": "2020-08-16T02:36:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11032,
        "CubeWorkinprocess.duedate": "2020-08-16T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 5 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11033,
        "CubeWorkinprocess.duedate": "2020-08-16T02:39:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 6 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11034,
        "CubeWorkinprocess.duedate": "2020-08-16T02:41:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 7 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11035,
        "CubeWorkinprocess.duedate": "2020-08-16T02:42:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330465 / 8 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11038,
        "CubeWorkinprocess.duedate": "2020-08-13T07:25:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330551 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11039,
        "CubeWorkinprocess.duedate": "2020-08-13T07:26:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330558 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11087,
        "CubeWorkinprocess.duedate": "2020-08-17T02:47:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 330667 / 1 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11088,
        "CubeWorkinprocess.duedate": "2020-08-17T02:49:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 330667 / 2 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11089,
        "CubeWorkinprocess.duedate": "2020-08-17T02:51:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 330667 / 3 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11090,
        "CubeWorkinprocess.duedate": "2020-08-17T02:52:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 330667 / 4 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11091,
        "CubeWorkinprocess.duedate": "2020-08-14T04:27:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330656 / 5 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11092,
        "CubeWorkinprocess.duedate": "2020-08-14T04:28:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330690 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11095,
        "CubeWorkinprocess.duedate": "2020-08-14T05:10:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330712 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11096,
        "CubeWorkinprocess.duedate": "2020-08-14T05:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330731 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11097,
        "CubeWorkinprocess.duedate": "2020-08-14T05:14:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330732 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11098,
        "CubeWorkinprocess.duedate": "2020-08-14T05:16:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330733 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11099,
        "CubeWorkinprocess.duedate": "2020-08-14T05:18:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330713 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11139,
        "CubeWorkinprocess.duedate": "2020-08-18T00:10:00.000",
        "CubeWorkinprocess.details": "Specification: 01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE |   ATTN: N/A  | P.O.: 330667 / 4 | Kit: 633004779 - CU,CLEAN,PLATE,PURGE,GAS,TOP,PLATE | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "01-INT-239 - CLEANED,PLATE,PURGE,GAS,TOP,PLATE",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11140,
        "CubeWorkinprocess.duedate": "2020-08-15T01:33:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330656 / 5 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11141,
        "CubeWorkinprocess.duedate": "2020-08-15T01:35:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330690 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11143,
        "CubeWorkinprocess.duedate": "2020-08-15T02:14:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330819 / 2 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11144,
        "CubeWorkinprocess.duedate": "2020-08-15T02:16:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330823 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11145,
        "CubeWorkinprocess.duedate": "2020-08-15T02:19:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 330849 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11146,
        "CubeWorkinprocess.duedate": "2020-08-18T02:28:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330872 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11147,
        "CubeWorkinprocess.duedate": "2020-08-18T02:29:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 330872 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11261,
        "CubeWorkinprocess.duedate": "2020-08-18T02:22:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331099 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11262,
        "CubeWorkinprocess.duedate": "2020-08-18T02:24:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331155 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11263,
        "CubeWorkinprocess.duedate": "2020-08-18T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331162 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11264,
        "CubeWorkinprocess.duedate": "2020-08-18T02:26:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331188 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11265,
        "CubeWorkinprocess.duedate": "2020-08-18T02:28:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331195 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11266,
        "CubeWorkinprocess.duedate": "2020-08-18T02:29:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331201 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11282,
        "CubeWorkinprocess.duedate": "2020-08-18T04:11:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331241 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11283,
        "CubeWorkinprocess.duedate": "2020-08-18T04:13:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331242 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11284,
        "CubeWorkinprocess.duedate": "2020-08-18T04:14:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331243 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11285,
        "CubeWorkinprocess.duedate": "2020-08-18T04:15:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331244 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11286,
        "CubeWorkinprocess.duedate": "2020-08-18T04:16:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331246 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11287,
        "CubeWorkinprocess.duedate": "2020-08-18T04:17:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331247 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11288,
        "CubeWorkinprocess.duedate": "2020-08-18T04:18:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331248 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11345,
        "CubeWorkinprocess.duedate": "2020-08-19T02:22:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331329 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11346,
        "CubeWorkinprocess.duedate": "2020-08-19T02:25:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331333 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11347,
        "CubeWorkinprocess.duedate": "2020-08-22T02:26:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11349,
        "CubeWorkinprocess.duedate": "2020-08-22T02:31:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11350,
        "CubeWorkinprocess.duedate": "2020-08-22T02:34:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11351,
        "CubeWorkinprocess.duedate": "2020-08-22T02:35:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11352,
        "CubeWorkinprocess.duedate": "2020-08-22T02:36:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 5 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11353,
        "CubeWorkinprocess.duedate": "2020-08-22T02:37:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 6 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11354,
        "CubeWorkinprocess.duedate": "2020-08-22T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 7 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11355,
        "CubeWorkinprocess.duedate": "2020-08-22T02:40:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 8 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11356,
        "CubeWorkinprocess.duedate": "2020-08-22T02:41:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 9 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11357,
        "CubeWorkinprocess.duedate": "2020-08-22T02:42:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331383 / 10 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11358,
        "CubeWorkinprocess.duedate": "2020-08-22T02:50:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331392 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11359,
        "CubeWorkinprocess.duedate": "2020-08-22T02:51:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331392 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11360,
        "CubeWorkinprocess.duedate": "2020-08-22T02:52:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331392 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11361,
        "CubeWorkinprocess.duedate": "2020-08-22T02:56:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331392 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11362,
        "CubeWorkinprocess.duedate": "2020-08-19T07:37:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331436 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11363,
        "CubeWorkinprocess.duedate": "2020-08-19T07:39:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331442 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11364,
        "CubeWorkinprocess.duedate": "2020-08-19T07:40:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331443 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11403,
        "CubeWorkinprocess.duedate": "2020-08-20T02:56:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331541 / 1 | Kit: 500315854 - CL,CLN2,REX,PIPE,KIT,X4 | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11404,
        "CubeWorkinprocess.duedate": "2020-08-20T02:58:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331620 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11405,
        "CubeWorkinprocess.duedate": "2020-08-20T02:59:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331621 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11406,
        "CubeWorkinprocess.duedate": "2020-08-20T03:03:00.000",
        "CubeWorkinprocess.details": "Specification: REX SS KIT Cleaning 1274 |   ATTN: N/A  | P.O.: 331621 / 1 | Kit: 633018478 - CLND, CU, REX, TUBES, RED PM KIT | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "REX SS KIT Cleaning 1274",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11458,
        "CubeWorkinprocess.duedate": "2020-08-24T01:01:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331747 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11459,
        "CubeWorkinprocess.duedate": "2020-08-24T01:02:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331474 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11539,
        "CubeWorkinprocess.duedate": "2020-08-25T05:23:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331912 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11540,
        "CubeWorkinprocess.duedate": "2020-08-25T05:24:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331912 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11541,
        "CubeWorkinprocess.duedate": "2020-08-25T05:25:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 331912 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11691,
        "CubeWorkinprocess.duedate": "2020-08-29T02:19:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332438 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11692,
        "CubeWorkinprocess.duedate": "2020-08-29T02:21:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332438 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11693,
        "CubeWorkinprocess.duedate": "2020-08-29T02:33:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332438 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11694,
        "CubeWorkinprocess.duedate": "2020-08-29T02:38:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332439 / 1 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11695,
        "CubeWorkinprocess.duedate": "2020-08-29T02:40:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332439 / 2 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11696,
        "CubeWorkinprocess.duedate": "2020-08-29T02:47:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332439 / 3 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11699,
        "CubeWorkinprocess.duedate": "2020-08-29T02:51:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332437 / 1 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11700,
        "CubeWorkinprocess.duedate": "2020-08-29T02:53:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332437 / 2 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11701,
        "CubeWorkinprocess.duedate": "2020-08-29T02:57:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332437 / 4 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11702,
        "CubeWorkinprocess.duedate": "2020-08-29T03:00:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332437 / 3 | Kit: 633014239 Heated V2 Valve - Copper | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      },
      {
        "CubeWorkinprocess.id": 11703,
        "CubeWorkinprocess.duedate": "2020-08-29T05:15:00.000",
        "CubeWorkinprocess.details": "Specification: V2 Heated Valve Refurb |   ATTN: N/A  | P.O.: 332438 / 4 | Kit: 500213434  Heated V2 Valve  | Tool:  | MTTN: ",
        "CubeWorkinprocess.status": "Completed",
        "CubeWorkinprocess.name": "V2 Heated Valve Refurb",
        "CubeWorkinprocess.mttn": null
      }
    ];
  }

}