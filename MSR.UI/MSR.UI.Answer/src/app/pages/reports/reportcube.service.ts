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
import * as moment from 'moment';
import * as Highcharts from 'highcharts';
import { ChartInfo } from '../../../app/models/lib/ChartInfo';

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
                    new ColumnsSaved({ id: cubeFinancial + 'wocreationdate', label: 'Creation Date', visible: true, type: this.enumColumnType.Date }),
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
                    new ColumnsSaved({ id: cubeFinancial + 'amount', label: 'Amount', visible: true, type: this.enumColumnType.Money }),
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
                    new ColumnsSaved({ id: cubeFinancial + 'shipdate', label: 'WO Complete Date', visible: true, type: this.enumColumnType.Date }),
                    new ColumnsSaved({ id: cubeFinancial + 'wtax', label: 'Total', visible: true, type: this.enumColumnType.String })
                ];
                break;
            case "RevenuebyCustomerbyTimePeriod":
                return [new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, formatting: 'MM-yyyy' }),
                new ColumnsSaved({ id: 'customername', label: 'Customer Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money })
                ];
                break;
            case "RevenuebyKitbyPart/Kit":
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, formatting: 'MM-yyyy' }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money })
                ];

                break;
            case "CountofKitsbyPart/Kit":
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.Date, formatting: 'MM-yyyy' }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'count', label: 'Count', visible: true, type: this.enumColumnType.Number })
                ];
                break;
            default:
                break;
        }

        return [new ColumnsSaved({ id: 'workOrderNumber', label: 'Id', visible: true, type: this.enumColumnType.Number })];;
    }


    public fromToDate(amount: number, unit: moment.DurationInputArg2, format: string) {
        const fromToValues = [];
        while (amount--) {
            fromToValues.push(moment().subtract(amount, unit).format(format));
        }

        return fromToValues;
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
                let dataDic = {};
                // const resultData = [];
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('MM-YYYY') + '_' + elem['CubeFinancial.customername'].replace(/ /g, '') + '_' + elem['CubeFinancial.msrfsrfacility'].replace(/ /g, '');
                    if (dataDic[elemKey] === undefined) {
                        dataDic[elemKey] = {
                            elemKey: elemKey,
                            yearMonth: moment(elem['CubeFinancial.duedate']),
                            site: elem['CubeFinancial.msrfsrfacility'],
                            customername: elem['CubeFinancial.customername'],
                            total: parseFloat(elem['CubeFinancial.wtax'].substring(1))
                        };
                    } else {
                        dataDic[elemKey].total += parseFloat(elem['CubeFinancial.wtax'].substring(1));
                    }
                });

                const chartInfo = new ChartInfo({
                    chartData: dataDic,
                    stackBy: 'total',
                    chartTitle: 'Revenue by Customer',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfo);
            case "RevenuebyKitbyPart/Kit":
                let dataDic3 = {};
                const resultData2 = [];
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('MM-YYYY') + '_' + elem['CubeFinancial.kitname'].replace(/ /g, '') + '_' + elem['CubeFinancial.msrfsrfacility'].replace(/ /g, '');
                    if (dataDic3[elemKey] === undefined) {
                        dataDic3[elemKey] = {
                            elemKey: elemKey,
                            duedate: elem['CubeFinancial.duedate'],
                            yearMonth: moment(elem['CubeFinancial.duedate']),
                            kitname: elem['CubeFinancial.kitname'],
                            site: elem['CubeFinancial.msrfsrfacility'],
                            total: parseFloat(elem['CubeFinancial.wtax'].substring(1))
                        };
                    } else {
                        dataDic3[elemKey].total += parseFloat(elem['CubeFinancial.wtax'].substring(1));
                    }
                });
                Object.keys(dataDic3).forEach(x => resultData2.push(dataDic3[x]));

                const chartInfo2 = new ChartInfo({
                    chartData: dataDic3,
                    stackBy: 'total',
                    chartTitle: 'Revenue by Kit',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfo2);
            case "CountofKitsbyPart/Kit":
                let dataDic2 = {};
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('MM-YYYY') + '_' + elem['CubeFinancial.kitname'].replace(/ /g, '') + '_' + elem['CubeFinancial.msrfsrfacility'].replace(/ /g, '');
                    if (dataDic2[elemKey] === undefined) {
                        dataDic2[elemKey] = {
                            elemKey: elemKey,
                            duedate: elem['CubeFinancial.duedate'],
                            yearMonth: moment(elem['CubeFinancial.duedate']),
                            kitname: elem['CubeFinancial.kitname'],
                            site: elem['CubeFinancial.msrfsrfacility'],
                            count: 1
                        };
                    } else {
                        dataDic2[elemKey].count += 1;
                    }
                });

                const chartInfo3 = new ChartInfo({
                    chartData: dataDic2,
                    stackBy: 'count',
                    chartTitle: 'Count of Kits',
                    xAxisTitle: 'Month (Previous 12 Months Rolling)',
                    yAxisTitle: 'Revenue Per Month',
                    tooltipFormat: 'Revenue: <b>{point.y:.1f}</b>'
                });

                return this.getResultDataAndChart(chartInfo3);

            default:
                break;
        }
        return data;
    }

    public generateChart(chartInfo3) {
        return this.getResultDataAndChart(chartInfo3);
    }

    public getResultDataAndChart(chartInfo: ChartInfo) {

        const months = this.fromToDate(chartInfo.amount, chartInfo.unit, chartInfo.format);
        const dataSeries = [];
        const resultData = [];

        Object.keys(chartInfo.chartData).forEach(x => {
            const index = months.indexOf(x.split('_')[0]);
            const name = x.split('_')[1];
            if (index !== -1) {

                const nameIndex = dataSeries.findIndex(x => x.name === name);
                if (nameIndex !== -1) {
                    dataSeries[nameIndex].data[index] += chartInfo.chartData[x][chartInfo.stackBy];
                    if (isNaN(dataSeries[nameIndex].data[index])) {
                        debugger;
                    }
                } else {
                    const dataArr = [];
                    months.forEach(element => {
                        dataArr.push(0);
                    });
                    dataArr[index] += chartInfo.chartData[x][chartInfo.stackBy];
                    dataArr.forEach(element => {
                        if (isNaN(element)) {
                            debugger;
                        }
                    });
                    dataSeries.push({
                        name: name,
                        data: dataArr
                    });
                }
            }
            resultData.push(chartInfo.chartData[x]);
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
                type: 'column'
            },
            title: {
                text: chartInfo.chartTitle,
                style: {
                    color: '#fff',
                    fontWeight: 'bold',
                    fontFamily: "Open Sans"
                }
            },
            // colors: ['#56616f'],
            xAxis: {
                type: 'category',

                labels: {
                    style: {
                        color: '#fff',
                        fontSize: '13px',
                        fontFamily: "Open Sans"
                    }
                },
                categories: months,
                title: {
                    text: chartInfo.xAxisTitle,
                    style: {
                        color: '#fff',
                        fontFamily: "Open Sans"
                    }
                }
            },
            yAxis: {
                min: 0,
                title: {
                    text: chartInfo.yAxisTitle,
                    style: {
                        color: '#fff',
                        fontFamily: "Open Sans"
                    }
                },
                labels: {
                    style: {
                        color: '#fff',
                        fontSize: '13px',
                        fontFamily: "Open Sans"
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
            legend: {
                enabled: false
            },
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
            tooltip: {
                headerFormat: '<b>Month:</b> {point.x}<br/>',
                pointFormat: '<b>{series.name}</b>: {point.y:,.2f}<br/> <b>Total</b>: {point.stackTotal:,.2f}'
            },
            plotOptions: {
                column: {
                    stacking: 'normal',
                    // dataLabels: {
                    //     enabled: true
                    // }
                }
            },
            series: dataSeries
        }

        return { resultData: resultData, chartOptions: chartOptions, chartInfo: chartInfo };
    }
}