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
                return [new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'customername', label: 'Customer Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money })
                ];
                break;
            case "RevenuebyKitbyPart/Kit":
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'site', label: 'Site', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'total', label: 'Total', visible: true, type: this.enumColumnType.Money })
                ];

                break;
            case "CountofKitsbyPart/Kit":
                return [new ColumnsSaved({ id: 'kitname', label: 'Kit Name', visible: true, type: this.enumColumnType.String }),
                new ColumnsSaved({ id: 'yearMonth', label: 'Year-Month', visible: true, type: this.enumColumnType.String }),
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
        amount++;
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
                const resultData = [];
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('YYYY-MM') + '_' + elem['CubeFinancial.customername'] + '_' + elem['CubeFinancial.msrfsrfacility'];
                    if (dataDic[elemKey] === undefined) {
                        dataDic[elemKey] = {
                            duedate: elem['CubeFinancial.duedate'],
                            yearMonth: moment(elem['CubeFinancial.duedate']).format('YYYY-MM'),
                            site: elem['CubeFinancial.msrfsrfacility'],
                            customername: elem['CubeFinancial.customername'],
                            total: parseFloat(elem['CubeFinancial.wtax'].substring(1))
                        };
                    } else {
                        dataDic[elemKey].total += parseFloat(elem['CubeFinancial.wtax'].substring(1));
                    }
                });
                const months = this.fromToDate(12, 'month', 'YYYY-MM');
                const dataSeries = []
                months.forEach(element => {
                    dataSeries.push([element, 0]);
                });


                Object.keys(dataDic).forEach(x => {
                    const index = months.indexOf(x.split('_')[0]);
                    if (index !== -1) {
                        dataSeries[index][1] += dataDic[x].total;
                    }
                    resultData.push(dataDic[x]);
                });

                let chartOptions: Highcharts.Options = {
                    chart: {
                        backgroundColor: '#222d3c',
                        borderColor: 'none'
                    },
                    title: {
                        text: 'Revenue by Customer',
                        style: {
                            color: '#fff',
                            fontWeight:'bold',
                            fontFamily: "Open Sans"
                        }
                    },
                    colors: ['#56616f'],//,'#005378'
                    xAxis: {
                        type: 'category',
                        labels: {
                            // rotation: -45,
                            style: {
                                color: '#fff',
                                fontSize: '13px',
                                fontFamily: "Open Sans"
                            }
                        },
                        title: {
                            text: 'Month (Previous 12 Months Rolling)',
                            style: {
                                color: '#fff',
                                fontFamily: "Open Sans"
                            }
                        }
                    },
                    yAxis: {
                        min: 0,
                        title: {
                            text: 'Revenue Per Month',
                            style: {
                                color: '#fff',
                                fontFamily: "Open Sans"
                            }
                        },
                        labels:{
                            style: {
                                color: '#fff'
                            }
                        }
                    },
                    legend: {
                        enabled: false
                    },
                    tooltip: {
                        pointFormat: 'Revenue: <b>{point.y:.1f}</b>'
                    },
                    series: [{
                        type: 'column',
                        name: 'Revenue Per Month',
                        data: dataSeries,
                        dataLabels: {
                            enabled: true,
                            rotation: -90,
                            color: '#FFFFFF',
                            align: 'right',
                            format: '{point.y:.1f}', // one decimal
                            y: 10, // 10 pixels down from the top
                            style: {
                                fontSize: '13px',
                                fontFamily: "Open Sans"
                                // fontFamily: 'Verdana, sans-serif'
                            }
                        }
                    }]
                }


                return { resultData: resultData, chartOptions: chartOptions };
                break;
            case "RevenuebyKitbyPart/Kit":
                let dataDic3 = {};
                const resultData2 = [];
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('YYYY-MM') + '_' + elem['CubeFinancial.kitname'] + '_' + elem['CubeFinancial.msrfsrfacility'];
                    if (dataDic3[elemKey] === undefined) {
                        dataDic3[elemKey] = {
                            duedate: elem['CubeFinancial.duedate'],
                            yearMonth: moment(elem['CubeFinancial.duedate']).format('YYYY-MM'),
                            kitname: elem['CubeFinancial.kitname'],
                            site: elem['CubeFinancial.msrfsrfacility'],
                            total: parseFloat(elem['CubeFinancial.wtax'].substring(1))
                        };
                    } else {
                        dataDic3[elemKey].total += parseFloat(elem['CubeFinancial.wtax'].substring(1));
                    }
                });
                Object.keys(dataDic3).forEach(x => resultData2.push(dataDic3[x]));
                return resultData2;
                break;
            case "CountofKitsbyPart/Kit":
                let dataDic2 = {};
                const countOfKits = [];
                data.map(elem => {
                    const elemKey = moment(elem['CubeFinancial.duedate']).format('YYYY-MM') + '_' + elem['CubeFinancial.kitname'] + '_' + elem['CubeFinancial.msrfsrfacility'];
                    if (dataDic2[elemKey] === undefined) {
                        dataDic2[elemKey] = {
                            duedate: elem['CubeFinancial.duedate'],
                            yearMonth: moment(elem['CubeFinancial.duedate']).format('YYYY-MM'),
                            kitname: elem['CubeFinancial.kitname'],
                            site: elem['CubeFinancial.msrfsrfacility'],
                            count: 1
                        };
                    } else {
                        dataDic2[elemKey].count += 1;
                    }
                });
                Object.keys(dataDic2).forEach(x => countOfKits.push(dataDic2[x]));
                return countOfKits;
                break;

            default:
                break;
        }
        return data;
    }
}