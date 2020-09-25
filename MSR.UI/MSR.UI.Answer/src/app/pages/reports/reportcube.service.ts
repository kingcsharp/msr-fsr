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
        const cubeWorkinprocess = 'CubeWorkinprocess.';
        const cubeWorkorderparts = 'CubeWorkorderparts.';
        const cubePartsmonitors = 'CubePartsmonitors.';
        const cubeFinancial = 'CubeFinancial.'
        switch (reportInfo.name.replace(/ /g, '') + reportInfo.subtitle.replace(/ /g, '')) {
            case 'WorkOrderPartsHistorybyPartNumber':
                return [
                    new ColumnsSaved({ id: cubeWorkorderparts + 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'id', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'shipdate', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'WorkOrderPartsHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: cubeWorkorderparts + 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'id', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'shipdate', label: 'Date Completed', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'SerialNumberHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: cubeWorkorderparts + 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'id', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'wocreationdate', label: 'Created', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'shipdate', label: 'Ship Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'msrfsrfacility', label: 'Facility', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'customername', label: 'Customer', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'specno', label: 'Spec #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeWorkorderparts + 'cycleCount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number })
                ];
                break;
            case "WorkInProcessbyWorkOrder":
                return [
                    new ColumnsSaved({ id: cubeWorkinprocess + 'id', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeWorkinprocess + 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeWorkinprocess + 'details', label: 'Details', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case "MonitorsHistorybyWorkOrder":
                return [
                    new ColumnsSaved({ id: cubePartsmonitors + 'value', label: 'Monitor Value', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'customerid', label: 'Customer Id', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'locationname', label: 'Location', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'partnumber', label: 'Part #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'partname', label: 'Part Names', visible: true, type: this.enumColumnType.StringArray, dropdownHeader: true, multipleValues: true }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'workordername', label: 'WO Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubePartsmonitors + 'lastupdatedby', label: 'Updated By', visible: true, type: this.enumColumnType.String }),
                ];
                break;
            case "CombinedFinancialDatabyWorkOrder":
                return [
                    new ColumnsSaved({ id: cubeFinancial + 'wonumber', label: 'WO Item', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: cubeFinancial + 'wocreationdate', label: 'Creation Date', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeFinancial + 'shipdate', label: 'Ship Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeFinancial + 'msrfsrfacility', label: 'Facility', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'customername', label: 'Customer', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'customerpartnumber', label: 'Customer Part #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'specificationnumber', label: 'Specification', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'serial', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    // new ColumnsSaved({ id: cubeFinancial+'', label: 'Invoice #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'invoicedate', label: 'Invoice Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeFinancial + 'invoicedescription', label: 'Invoice Description', visible: true, type: this.enumColumnType.String }),
                    // new ColumnsSaved({ id: cubeFinancial+'', label: 'Qty', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'amount', label: 'Amount', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'subtotal', label: 'SubTotal', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'wtax', label: 'w/ Tax', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case "WorkOrdersNotInvoicedbyWorkOrder":
                return [
                    new ColumnsSaved({ id: cubeFinancial + 'ponumber', label: 'Customer Purchase', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'wonumber', label: 'Work Order Item', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'msrfsrfacility', label: 'Location', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'kitname', label: 'Product Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'shipdate', label: 'WO Complete Date', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: cubeFinancial + 'wtax', label: 'Total', visible: true, type: this.enumColumnType.String })
                ];
                break;
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
            case "MonitorsHistorybyWorkOrder":
                const workInProcessbyWorkOrder = data.map(elem => {
                    //we need to set this property as for multiple filters if the property name has a "." grid wont be filtered.
                    elem['partname'] = elem['CubePartsmonitors.partname'].map((x) => {
                        return {
                            name: x,
                            id: x
                        }
                    });
                    return elem;
                })
                return workInProcessbyWorkOrder;
                break;
            case "CombinedFinancialDatabyWorkOrder":
                break;
            case "WorkOrdersNotInvoicedbyWorkOrder":
                const workOrdersNotInvoicedbyWorkOrder = data.filter(x => x['CubeFinancial.invoicedate'] === undefined || x['CubeFinancial.invoicedate'] === null);
                return workOrdersNotInvoicedbyWorkOrder;
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