import { Injectable, OnDestroy } from '@angular/core';
import { NotificationService } from '../../layout/navbar/notification.service';
import { environment as env } from '../../../environments/environment';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';
import { ReportModel } from '../../services/api.client.generated';
import { ColumnsSaved } from '../../../app/models/lib/ColumnsSaved';
import { EnumColumnType } from '../../../app/models/enums/EnumColumnType';

@Injectable({
    providedIn: 'root'
})
export class ReportCubeService {
    cubeKey = '9nyEf9X3gjVQqryBAYKcMSefrkCZ7m8bCHJSXeXCYsfhCqcRJt';
    enumColumnType = EnumColumnType;
    constructor(private http: HttpClient, private toastr: ToastrService) {

    }

    public getReport = async (reportInfo: ReportModel) => {
        const headers = new HttpHeaders().set("key", this.cubeKey);
        return this.http.get(reportInfo.apiEndPointURL, { headers: headers }).toPromise().then(response => {
            return this.filterReportData(response, reportInfo);
        });
    }
    /*
    Available Reports
    Financial
  PartsMonitors
  WorkInProcess
  WorkOrderParts
    */

    public getReportColumns(reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/ /g, '') + reportInfo.subtitle.replace(/ /g, '')) {
            case 'WorkOrderPartsHistorybyPartNumber':
                return [
                    new ColumnsSaved({ id: 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'specno', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'dateCompleted', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'SerialNumberHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: 'specno', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'dateCompleted', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'WorkInProcess':
                break;
            case 'MonitorsHistory':
                break;
            case 'WorkOrdersNotInvoiced':
                break;
            case 'RevenuebyCustomer':
                break;
            case 'RevenuebyKit':
                break;
            case 'CountofKits':
                break;

            default:
                break;
        }

        return [new ColumnsSaved({ id: 'workOrderNumber', label: 'Id', visible: true, type: this.enumColumnType.Number })];;
    }

    public filterReportData(data: any, reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/ /g, '') + reportInfo.subtitle.replace(/ /g, '')) {
            case 'WorkOrderPartsHistorybyPartNumber':
            case 'SerialNumberHistorybySerialNumber':
                const retData = data.map(elem => {
                    return {
                        workOrderNumber: elem['CubeWorkorderparts.id'],
                        ponumber: elem['CubeWorkorderparts.ponumber'],
                        specno: elem['CubeWorkorderparts.specno'],
                        dateCompleted: elem['CubeWorkorderparts.shipdate'],
                        ncdisposition: elem['CubeWorkorderparts.ncdisposition'],
                        cycleCount: elem['CubeWorkorderparts.cyclecount'],
                    }
                })
                return retData;
                break;
            case 'WorkInProcess':
                break;
            case 'MonitorsHistory':
                break;
            case 'WorkOrdersNotInvoiced':
                break;
            case 'RevenuebyCustomer':
                break;
            case 'RevenuebyKit':
                break;
            case 'CountofKits':
                break;

            default:
                break;
        }
        return data;
    }
}