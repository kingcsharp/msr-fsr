import { Injectable, OnDestroy } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { EnumAwsFolders, ReportModel } from '../../services/api.client.generated';
import { ColumnsSaved } from '../../../app/models/lib/ColumnsSaved';
import { EnumColumnType } from '../../../app/models/enums/EnumColumnType';
import * as moment from 'moment';
import * as Highcharts from 'highcharts';
import { ChartInfo } from '../../../app/models/lib/ChartInfo';
import { Globals } from '../../models/lib/globals';
import { EnumChartType } from '../../../app/models/enums/ChartType';
import { EnumChartStackType } from '../../../app/models/enums/EnumChartStackType';

@Injectable({
    providedIn: 'root'
})
export class ReportCubeService {
    cubeKey = '9nyEf9X3gjVQqryBAYKcMSefrkCZ7m8bCHJSXeXCYsfhCqcRJt';
    enumColumnType = EnumColumnType;
    splitChars = '_axy_';
    constructor(private http: HttpClient, private toastr: ToastrService, private globals: Globals) {

    }

    public getReport = async (reportInfo: ReportModel) => {
        const headers = new HttpHeaders().set('key', this.cubeKey);
        return this.http.get(reportInfo.apiEndPointURL, { headers: headers }).toPromise().then(response => {
            return this.filterReportData(response, reportInfo);
        });
    }

    public getArchivedEnum(reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '')) {
            case 'CombinedFinancialDatabyWorkOrder':
            case 'WorkOrdersNotInvoicedbyWorkOrder':
                return EnumAwsFolders.Combinedfinancialdata;
            default:
                return null;
        }
    }

    public getReportColumns(reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '')) {
            case 'PartsCycleCountsbyWorkOrderDate':
                return [
                    new ColumnsSaved({ id: 'id', label: 'Id', visible: true, type: this.enumColumnType.Number, styles: { 'width': '6rem' } }),
                    new ColumnsSaved({ id: 'partnumber', label: 'Part #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'woitem', label: 'WO Item', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'msrfsrfacility', label: 'MSRFSRfacility', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'customername', label: 'Customer Name', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'specno', label: 'Spec No', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number, styles: { 'width': '4rem' } }),
                    new ColumnsSaved({ id: 'startdate', label: 'Start Date', visible: true, type: this.enumColumnType.Date, styles: { 'width': '8rem' }, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' })
                ];
                break;
            case 'WorkOrderPartsHistorybyPartNumber':
                return [
                    new ColumnsSaved({ id: 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'woitem', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'shipdate', label: 'Date Completed', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case 'WorkOrderPartsHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: 'serialnumber', label: 'SN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'woitem', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'shipdate', label: 'Date Completed', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'ncdisposition', label: 'NC Disposition', visible: true, type: this.enumColumnType.String })
                ];
            case 'SerialNumberHistorybySerialNumber':
                return [
                    new ColumnsSaved({ id: 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'woitem', label: 'WO#', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'wocreationdate', label: 'Created', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'shipdate', label: 'Ship Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'msrfsrfacility', label: 'Facility', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'customername', label: 'Customer', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'specno', label: 'Spec #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number })
                ];
            case 'WorkInProcessbyWorkOrder':
                return [
                    new ColumnsSaved({ id: 'id', label: 'WO#', visible: true, type: this.enumColumnType.Number, styles: { 'width': '7rem' } }),
                    new ColumnsSaved({ id: 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date, styles: { 'width': '8rem' }, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'customerName', label: 'Customer', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'purchaseOrderNumber', label: 'PO#', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'quantity', label: 'Quantity', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'scheduledStartDate', label: 'Scheduled Start Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'scheduledEndDate', label: 'Scheduled End Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: 'productName', label: 'Product', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'procedureName', label: 'Procedure', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'price', label: 'Price', visible: true, type: this.enumColumnType.Money })
                ];
            case 'MonitorsHistorybyWorkOrder':
                return [
                    new ColumnsSaved({ id: 'value', label: 'Monitor Value', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'customerid', label: 'Customer Id', visible: false, type: this.enumColumnType.Number, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: true, type: this.enumColumnType.Number, styles: { 'width': '4rem' } }),
                    new ColumnsSaved({ id: 'locationname', label: 'Location', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'partnumber', label: 'Part #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'partname', label: 'Part Names', visible: true, type: this.enumColumnType.StringArray, dropdownHeader: true, multipleValues: true }),
                    new ColumnsSaved({ id: 'workordername', label: 'WO Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'lastupdatedon', label: 'Updated On', visible: true, type: this.enumColumnType.Date, isRanged: true, styles: { 'width': '8rem' }, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'lastupdatedby', label: 'Updated By', visible: true, type: this.enumColumnType.String })
                ];
            case 'MonitorsbyWorkOrder':
                return [
                    new ColumnsSaved({ id: 'description', label: 'Description', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'monitortype', label: 'Monitor Type', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'customerid', label: 'Customer Id', visible: false, type: this.enumColumnType.Number, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'cyclecount', label: 'Cycle Count', visible: false, type: this.enumColumnType.Number, styles: { 'width': '4rem' } }),
                    new ColumnsSaved({ id: 'locationname', label: 'Location', visible: false, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'partnumber', label: 'Part #', visible: false, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'partname', label: 'Part Names', visible: false, type: this.enumColumnType.StringArray, dropdownHeader: true, multipleValues: true }),
                    new ColumnsSaved({ id: 'lastupdatedby', label: 'Updated By', visible: false, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'value', label: 'Result', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'result', label: 'Passing', visible: true, type: this.enumColumnType.String, styles: { 'width': '6rem' } }),
                    new ColumnsSaved({ id: 'workordername', label: 'WO Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'lastupdatedon', label: 'Task Completed', visible: true, type: this.enumColumnType.Date, isRanged: true, styles: { 'width': '8rem' }, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'serialnumber', label: 'Serial #', visible: true, type: this.enumColumnType.String })
                ];
            case 'CombinedFinancialDatabyWorkOrder':
                return [
                    new ColumnsSaved({ id: 'wonumber', label: 'WO Item', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'ponumber', label: 'PO #', visible: true, type: this.enumColumnType.Number }),
                    new ColumnsSaved({ id: 'wocreationdate', label: 'Creation Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'duedate', label: 'Due Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'shipdate', label: 'Ship Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'msrfsrfacility', label: 'Facility', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'customername', label: 'Customer', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'customerpartnumber', label: 'Customer Part #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'specificationnumber', label: 'Specification', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'serial', label: 'Serial #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'mttn', label: 'MTTN', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'invoicenumber', label: 'Invoice #', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'invoicedate', label: 'Invoice Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'invoicedescription', label: 'Invoice Description', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'fillqty', label: 'Qty', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'amount', label: 'Amount', visible: true, type: this.enumColumnType.Money }),
                    new ColumnsSaved({ id: 'subtotal', label: 'SubTotal', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'wtax', label: 'w/ Tax', visible: true, type: this.enumColumnType.String })
                ];
            case 'WorkOrdersNotInvoicedbyWorkOrder':
                return [
                    new ColumnsSaved({ id: 'ponumber', label: 'Customer Purchase', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'wonumber', label: 'Work Order Item', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'msrfsrfacility', label: 'Location', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                    new ColumnsSaved({ id: 'kitname', label: 'Product Name', visible: true, type: this.enumColumnType.String }),
                    new ColumnsSaved({ id: 'shipdate', label: 'WO Complete Date', visible: true, type: this.enumColumnType.Date, formattingAngular: 'MM-dd-yyyy', formattingMoment: 'MM-DD-YYYY' }),
                    new ColumnsSaved({ id: 'wtax', label: 'Total', visible: true, type: this.enumColumnType.String })
                ];
            case 'RevenuebyCustomerbyTimePeriod':
                return [new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, isRanged: true, styles: { 'width': '8rem' } }),
                new ColumnsSaved({ id: 'customername', label: 'Customer Name', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money, styles: { 'width': '7rem' } })
                ];
            case 'RevenuebyKitbyPart/Kit':
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, isRanged: true, styles: { 'width': '8rem' } }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money, styles: { 'width': '7rem' } })
                ];
            case 'CountofKitsbyPart/Kit':
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, isRanged: true, styles: { 'width': '8rem' } }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
                new ColumnsSaved({ id: 'count', label: 'Count', visible: true, type: this.enumColumnType.Number, styles: { 'width': '6rem' } })
                ];
            default:
                break;
        }

        return [new ColumnsSaved({ id: 'workOrderNumber', label: 'Id', visible: true, type: this.enumColumnType.Number })];
    }


    public fromToDate(amount: number, unit: moment.DurationInputArg2, format: string) {
        const fromToValues = [];
        while (amount--) {
            fromToValues.push(moment().subtract(amount, unit).format(format));
        }

        return fromToValues;
    }

    private setName(row, prop1, prop2, separator) {
        let name = '';
        if (row[prop1] !== undefined && row[prop1] !== null) {
            name += row[prop1].replace(/\s/g, '');
        }
        if (row[prop2] !== undefined && row[prop2] !== null) {
            if (name.length > 0) {
                name += separator;
            }
            name += row[prop2].replace(/\s/g, '');
        }

        return name;
    }

    private isValidRowForChart(row, prop1, prop2) {
        const isValid = row[prop2] !== undefined && row[prop2] !== null && row[prop2].length > 0
            && row[prop1] !== undefined && row[prop1] !== null && row[prop1].length > 0;
        return isValid;
    }

    getCycleCountNr(row, prop) {
        const cycleCount = row[prop];
        if (isNaN(cycleCount)) {
            return 0;
        }
        return cycleCount;
    }

    public filterReportData(data: any, reportInfo: ReportModel) {
        switch (reportInfo.name.replace(/\s/g, '') + reportInfo.subtitle.replace(/\s/g, '')) {
            case 'PartsCycleCountsbyWorkOrderDate':
                const gridData = data.map(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    elem.elemKey = elem['startdate'] + this.splitChars + this.setName(elem, 'partnumber', 'serialnumber', '-');
                    elem.isValidForChart = this.isValidRowForChart(elem, 'partnumber', 'serialnumber');
                    elem.cyclecount = this.getCycleCountNr(elem, 'cyclecount');
                    elem.startdate = moment(elem['startdate']);
                    return elem;
                });
                const chartInfoPartsCycleCounts = new ChartInfo({
                    gridData: gridData,
                    chartType: EnumChartType.Line,
                    stackByType: EnumChartStackType.MaxStackDateValue,
                    stackBy: 'cyclecount',
                    chartTitle: 'Parts Cycle Counts',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Cycle Count',
                    tooltipFormat: 'Cycle Count: <b>{point.y:.1f}</b>',
                    chartTOptions: {
                        legend: {
                            align: 'center',
                            verticalAlign: 'bottom',
                            itemHoverStyle: {
                                color: '#bdbdbd'
                            },
                            itemStyle: {
                                color: '#fff',
                                fontFamily: 'Open Sans'
                            },
                            x: 0,
                            y: 0
                        },

                        tooltip: {
                            formatter: function () {
                                const date = moment(this.point.category, 'MM-YYYY').format('MMM-YY');
                                return `<div>
                                <b>${this.series.name}</b><br>
                                ${date}: Cycle Count ${this.point.y}
                                <div>`;
                            }
                        },
                        plotOptions: {
                            series: {
                                marker: {
                                    enabled: true
                                },
                                showInLegend: true
                            }
                        }
                    }
                });

                return this.getResultDataAndChart(chartInfoPartsCycleCounts);
            case 'MonitorsHistorybyWorkOrder':
                const gridDataRet = data.map(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    elem.elemKey = elem['lastupdatedon'] + this.splitChars + this.setName(elem, 'partnumber', 'serialnumber', '-');
                    elem.isValidForChart = this.isValidRowForChart(elem, 'partnumber', 'serialnumber');
                    elem.cyclecount = this.getCycleCountNr(elem, 'cyclecount');
                    elem.lastupdatedon = moment(elem['lastupdatedon']);
                    elem.partname = [{ name: elem['partname'], id: elem['partname'] }];
                    return elem;
                });
                const chartInfoDicworkInProcessbyWorkOrder = new ChartInfo({
                    gridData: gridDataRet,
                    chartType: EnumChartType.Bar,
                    stackBy: 'cyclecount',
                    chartTitle: 'Parts Cycle Counts',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Cycle Count',
                    tooltipFormat: 'Cycle Count: <b>{point.y:.1f}</b>',
                    chartTOptions: {
                        tooltip: {
                            formatter: function () {
                                const date = moment(this.point.category, 'MM-YYYY').format('MMM-YY');
                                return `<div>
                                <b>${this.series.name}</b><br>
                                ${date}: Cycle Count ${this.point.y}
                                <div>`;
                            }
                        }
                    }
                });
                return this.getResultDataAndChart(chartInfoDicworkInProcessbyWorkOrder);
                break;
            case 'MonitorsbyWorkOrder':
                const gridDataMonitorsbyWorkOrder = data.map(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    elem.elemKey = elem['lastupdatedon'] + this.splitChars + this.setName(elem, 'partnumber', 'serialnumber', '-');
                    elem.isValidForChart = this.isValidRowForChart(elem, 'partnumber', 'serialnumber');
                    elem.cyclecount = this.getCycleCountNr(elem, 'cyclecount');
                    elem.lastupdatedon = moment(elem['lastupdatedon']);
                    elem.partname = [{ name: elem['partname'], id: elem['partname'] }];
                    return elem;
                });
                return gridDataMonitorsbyWorkOrder;
                break;
            case 'WorkOrdersNotInvoicedbyWorkOrder':
                const workOrdersNotInvoicedbyWorkOrder =
                    data.filter(x => x['CubeFinancial.status'] === 'Completed' && x['CubeFinancial.shipdate'] !== undefined && x['CubeFinancial.shipdate'] !== null
                        && (x['CubeFinancial.invoicedate'] === undefined || x['CubeFinancial.invoicedate'] === null));
                return workOrdersNotInvoicedbyWorkOrder.map((elem) => this.removeObjectsPropertyPrefix(elem));
            case 'RevenuebyCustomerbyTimePeriod':
                const resultDataRevenuebyCustomerbyTimePeriod = data.map(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    elem.elemKey = elem['duedate'] + this.splitChars + elem['customername'].replace(/\s/g, '') + this.splitChars + elem['msrfsrfacility'].replace(/\s/g, '');
                    elem.yearMonth = moment(elem['duedate']);
                    elem.isValidForChart = true;
                    elem.site = elem['msrfsrfacility'];
                    elem.total = parseFloat(elem['wtax'].substring(1));
                    return elem;
                });

                const chartInfoRevenuebyCustomerbyTimePeriod = new ChartInfo({
                    gridData: resultDataRevenuebyCustomerbyTimePeriod,
                    stackBy: 'total',
                    chartTitle: 'Revenue by Customer',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfoRevenuebyCustomerbyTimePeriod);
            case 'RevenuebyKitbyPart/Kit':
                const resultData2 = data.map(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    elem.elemKey = elem['duedate'] + this.splitChars + elem['kitname'].replace(/\s/g, '') + this.splitChars + elem['msrfsrfacility'].replace(/\s/g, '');
                    elem.yearMonth = moment(elem['duedate']);
                    elem.isValidForChart = true;
                    elem.total = parseFloat(elem['wtax'].substring(1));
                    elem.site = elem['msrfsrfacility'];
                    return elem;
                });

                const chartInfo2 = new ChartInfo({
                    gridData: resultData2,
                    stackBy: 'total',
                    chartTitle: 'Revenue by Kit',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfo2);
            case 'CountofKitsbyPart/Kit':
                let countOfKitsGridDataDic = {};
                data.forEach(elem => {
                    elem = this.removeObjectsPropertyPrefix(elem);
                    const keyCombinedName = this.setName(elem, 'kitname', 'msrfsrfacility', '-');
                    const key = moment(elem['duedate']).format('YYYY-MM') + this.splitChars + keyCombinedName;
                    elem.elemKey = elem['duedate'] + this.splitChars + keyCombinedName;
                    elem.isValidForChart = this.isValidRowForChart(elem, 'kitname', 'msrfsrfacility');
                    elem.yearMonth = moment(elem['duedate']);
                    elem.site = elem['msrfsrfacility'];
                    elem.count = 1;
                    if (elem.isValidForChart) {
                        if (countOfKitsGridDataDic[key] === undefined) {
                            countOfKitsGridDataDic[key] = elem;
                        } else {
                            countOfKitsGridDataDic[key].count++;
                        }
                    }
                });

                const countOfKitsGridData = [];

                Object.keys(countOfKitsGridDataDic).forEach(chartDataKey => {
                    countOfKitsGridData.push(countOfKitsGridDataDic[chartDataKey]);
                });

                const chartInfo3 = new ChartInfo({
                    gridData: countOfKitsGridData,
                    stackBy: 'count',
                    chartTitle: 'Count of Kits',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfo3);
            default:
                if (data.length > 0) {
                    const resultDataArr = data.map((elem) => this.removeObjectsPropertyPrefix(elem));
                    return resultDataArr;
                }
                break;
        }
        return data;
    }

    removeObjectsPropertyPrefix(elem: any) {
        let objToReturn = {};
        Object.keys(elem).forEach((key) => {
            const keysplitted = key.split('.');
            const keyVal = keysplitted.length > 1 ? 1 : 0;
            objToReturn[keysplitted[keyVal]] = elem[key];
        });
        return objToReturn;
    }

    public generateChart(chartInfo3) {
        return this.getResultDataAndChart(chartInfo3);
    }

    private groupChartDataFromGridRows(chartInfo: ChartInfo) {
        let chartData = {};
        const gridData = chartInfo.gridData;
        let length = chartInfo.gridData.length;
        while (length--) {
            const gridDataRow = gridData[length];
            if (gridDataRow.isValidForChart !== false) {
                if (chartData[gridDataRow.elemKey] === undefined) {
                    chartData[gridDataRow.elemKey] = gridDataRow;
                } else {
                    chartData[gridDataRow.elemKey][chartInfo.stackBy] += gridDataRow[chartInfo.stackBy];
                }
            }
        }
        return chartData;
    }

    updateMaxDateValueSelected(dataSeriesMaxDateStackValueFromTo, stackBy, row, date) {
        const index = dataSeriesMaxDateStackValueFromTo.findIndex(x => x.monthYear === moment(date).format('MM-YYYY'));
        const savedElement = dataSeriesMaxDateStackValueFromTo[index];
        if (savedElement.row[stackBy] === undefined) {
            savedElement.row = row;
            savedElement.maxDate = date;
        } else if (moment(savedElement.maxDate).isBefore(moment(date))) {
            savedElement.row = row;
            savedElement.maxDate = date;
        }

        return savedElement;
    }

    public getResultDataAndChart(chartInfo: ChartInfo) {
        chartInfo.chartData = this.groupChartDataFromGridRows(chartInfo);
        const monthsFromTo = this.fromToDate(chartInfo.amount, chartInfo.unit, chartInfo.format);
        const dataSeries = [];
        const dataSeriesMaxDateStackValueFromTo = {};

        Object.keys(chartInfo.chartData).forEach(chartDataKey => {
            const rowData = chartInfo.chartData[chartDataKey];
            const xMonth = chartDataKey.split(this.splitChars)[0];
            const monthIndex = monthsFromTo.indexOf(moment(xMonth).format('MM-YYYY'));
            const keyName = chartDataKey.split(this.splitChars)[1];
            if (monthIndex !== -1) {
                const nameIndex = dataSeries.findIndex(z => z.name === keyName);
                if (nameIndex !== -1) {
                    if (chartInfo.stackByType === EnumChartStackType.MaxStackDateValue) {
                        const savedElem = this.updateMaxDateValueSelected(dataSeriesMaxDateStackValueFromTo[keyName], chartInfo.stackBy, rowData, xMonth);
                        dataSeries[nameIndex].data[monthIndex] = savedElem.row[chartInfo.stackBy];
                    } else {
                        dataSeries[nameIndex].data[monthIndex] += rowData[chartInfo.stackBy];
                    }
                } else {
                    const seriesDataArray = [];
                    dataSeriesMaxDateStackValueFromTo[keyName] = [];
                    // just to initialize an array with a 0 in all it's positions.
                    monthsFromTo.forEach(element => {
                        seriesDataArray.push(0);
                        dataSeriesMaxDateStackValueFromTo[keyName].push({ monthYear: element, maxDate: undefined, row: {} });
                    });

                    if (chartInfo.stackByType === EnumChartStackType.MaxStackDateValue) {
                        const savedElem = this.updateMaxDateValueSelected(dataSeriesMaxDateStackValueFromTo[keyName], chartInfo.stackBy, rowData, xMonth);
                        seriesDataArray[monthIndex] = savedElem.row[chartInfo.stackBy];
                    } else {
                        seriesDataArray[monthIndex] += rowData[chartInfo.stackBy];
                    }

                    dataSeries.push({
                        name: keyName,
                        data: seriesDataArray
                    });
                }
            }
        });

        Highcharts.setOptions({
            lang: {
                decimalPoint: '.',
                thousandsSep: ','
            },
        });
        let chartOptions: Highcharts.Options = {
            chart: {
                backgroundColor: '#222d3c',
                borderColor: 'none',
                type: chartInfo.chartType === EnumChartType.Line ? 'spline' : 'column',
            },
            title: {
                text: chartInfo.chartTitle,
                style: {
                    color: '#fff',
                    fontWeight: 'bold',
                    fontFamily: 'Open Sans'
                }
            },
            xAxis: {
                type: 'category',

                labels: {
                    style: {
                        color: '#fff',
                        fontSize: '13px',
                        fontFamily: 'Open Sans'
                    }
                },
                categories: monthsFromTo,
                title: {
                    text: chartInfo.xAxisTitle,
                    style: {
                        color: '#fff',
                        fontFamily: 'Open Sans'
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: chartInfo.yAxisTitle,
                    style: {
                        color: '#fff',
                        fontFamily: 'Open Sans'
                    }
                },
                labels: {
                    style: {
                        color: '#fff',
                        fontSize: '13px',
                        fontFamily: 'Open Sans'
                    }
                },
                stackLabels: {
                    enabled: true,
                    style: {
                        fontWeight: 'bold',
                        color: '#fff'
                    }
                }
            },
            legend: chartInfo.chartTOptions?.legend === undefined ? {
                enabled: false
            } : chartInfo.chartTOptions.legend,
            // legend: {
            //     align: 'right',
            //     x: -5,
            //     verticalAlign: 'top',
            //     y: 5,
            //     floating: true,
            //     backgroundColor:'white',
            //     borderColor: '#CCC',
            //     borderWidth: 1,
            //     shadow: false,
            //     maxHeight:100,
            //     width:200,
            //     itemHoverStyle:{
            //         opacity:1
            //     }
            // },
            tooltip: chartInfo.chartTOptions?.tooltip === undefined ? {
                headerFormat: '<b>Month:</b> {point.x}<br/>',
                pointFormat: '<b>{series.name}</b>: {point.y:,.2f}<br/> <b>Total</b>: {point.stackTotal:,.2f}'
            } : chartInfo.chartTOptions.tooltip,
            plotOptions: chartInfo.chartTOptions?.plotOptions === undefined ? {
                column: {
                    stacking: 'normal',
                    // dataLabels: {
                    //     enabled: true
                    // }
                }
            } : chartInfo.chartTOptions.plotOptions,
            series: dataSeries
        };


        return { resultData: chartInfo.gridData, chartOptions: chartOptions, chartInfo: chartInfo };
    }
}
