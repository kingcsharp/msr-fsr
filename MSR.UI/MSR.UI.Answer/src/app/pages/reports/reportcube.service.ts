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
                    new ColumnsSaved({ id: 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'dateCompleted', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'WorkOrderPartsHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'dateCompleted', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'SerialNumberHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'wocreationdate', label: 'Created', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'dateCompleted', label: 'Ship Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'msrfsrfacility', label: 'Facility', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'customername', label: 'Customer', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'specno', label: 'Spec #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number })
                ];
                break;
            case "WorkInProcessbyWorkOrder":
                return [
                    new ColumnsSaved({ id: 'workOrderNumber', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'details', label: 'Details', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case "MonitorsHistorybyWorkOrder":
                break;
            case "CombinedFinancialDatabyWorkOrder":
                break;
            case "WorkOrdersNotInvoicedbyWorkOrder":
                break;
            case "RevenuebyCustomerbyTimePeriod":
                break;
            case "RevenuebyKitbyPart/Kit":
                break;
            case "CountofKitsbyPart/Kit":
                break;
            default:
                break;
        }

        return [new ColumnsSaved({ id: 'workOrderNumber', label: 'Id', visible: true, type: this.enumColumnType.Number })];;
    }

    public filterReportData(data: any, reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/ /g, '') + reportInfo.subtitle.replace(/ /g, '')) {
            case 'WorkOrderPartsHistorybyPartNumber':
            case 'WorkOrderPartsHistorybySerialNumber':
            case 'SerialNumberHistorybySerialNumber':
                const woPartInfo = data.map(elem => {
                    return {
                        workOrderNumber: elem['CubeWorkorderparts.id'],
                        ponumber: elem['CubeWorkorderparts.ponumber'],
                        serialnumber: elem['CubeWorkorderparts.serialnumber'],
                        dateCompleted: elem['CubeWorkorderparts.shipdate'],
                        ncdisposition: elem['CubeWorkorderparts.ncdisposition'],
                        cycleCount: elem['CubeWorkorderparts.cyclecount'],
                        duedate: elem['CubeWorkorderparts.duedate'],
                        mttn: elem['CubeWorkorderparts.mttn'],
                        kitname: elem['CubeWorkorderparts.kitname'],
                        specno: elem['CubeWorkorderparts.specno'],
                        customername: elem['CubeWorkorderparts.customername'],
                        msrfsrfacility: elem['CubeWorkorderparts.msrfsrfacility'],
                        wocreationdate: elem['CubeWorkorderparts.wocreationdate']
                    }
                })
                return woPartInfo;
            case "WorkInProcessbyWorkOrder":
                const workInProcessbyWorkOrder = data.map(elem => {
                    return {
                        workOrderNumber: elem['CubeWorkinprocess.id'],
                        duedate: elem['CubeWorkinprocess.duedate'],
                        details: elem['CubeWorkinprocess.details'],
                    }
                })
                return workInProcessbyWorkOrder;
                break;
            case "MonitorsHistorybyWorkOrder":
                break;
            case "CombinedFinancialDatabyWorkOrder":
                break;
            case "WorkOrdersNotInvoicedbyWorkOrder":
                break;
            case "RevenuebyCustomerbyTimePeriod":
                break;
            case "RevenuebyKitbyPart/Kit":
                break;
            case "CountofKitsbyPart/Kit":
                break;

            default:
                break;
        }
        return data;
    }
}