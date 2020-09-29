
import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import {
    ReportService, ReportModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { ToastrService } from 'ngx-toastr';
import { Observable, forkJoin, of } from 'rxjs';
import { replaceArrayItems, pushIfNotExists, emptyArray, copyObj } from '../../../models/lib/Utils';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../../models/enums/EnumColumnType';
import { GridSaved } from '../../../models/lib/GridSaved';

declare let jQuery: any;

@Component({
    selector: 'app-operations',
    templateUrl: './operations.component.html',
    styleUrls: ['./operations.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class OperationsComponent implements OnInit {

    ngOnInit(): void {
        // throw new Error('Method not implemented.');
    }


}