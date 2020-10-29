import { EnumChartType } from '../enums/ChartType';
import { EnumChartStackType } from '../enums/EnumChartStackType';

export interface IChartInfo {
    amount?: number;
    unit?: moment.DurationInputArg2;
    format?: string;
    chartData?: any | undefined;
    gridData?: any | undefined;
    stackBy?: string | undefined;
    stackByType?: EnumChartStackType | undefined;
    chartTitle?: string | undefined;
    xAxisTitle?: string | undefined;
    yAxisTitle?: string | undefined;
    tooltipFormat?: string | undefined;
    chartType?: EnumChartType | undefined;
    chartTOptions?: Highcharts.Options | undefined;
}

export class ChartInfo implements IChartInfo {
    amount: number = 12;
    unit: moment.DurationInputArg2 = 'month';
    format: string = 'MM-YYYY';
    chartData?: any | undefined;
    gridData?: any | undefined;
    stackBy?: string | undefined;
    stackByType?: EnumChartStackType = EnumChartStackType.Sum;
    chartTitle?: string | undefined;
    xAxisTitle?: string | undefined;
    yAxisTitle?: string | undefined;
    tooltipFormat?: string | undefined;
    chartType?: EnumChartType | undefined;
    chartTOptions?: Highcharts.Options | undefined;

    constructor(data?: IChartInfo) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ChartInfo {
        data = typeof data === 'object' ? data : {};
        let result = new ChartInfo();
        result.init(data);
        return result;
    }

    init(_data?: any) {
        if (_data) {
            this.amount = _data['amount'];
            this.unit = _data['unit'];
            this.format = _data['format'];
            this.chartData = _data['chartData'];
            this.stackBy = _data['stackBy'];
            this.chartTitle = _data['chartTitle'];
            this.xAxisTitle = _data['xAxisTitle'];
            this.yAxisTitle = _data['yAxisTitle'];
            this.tooltipFormat = _data['tooltipFormat'];
            this.chartType = _data['chartType'];
            this.chartTOptions = _data['chartTOptions'];
            this.gridData = _data['gridData'];
            this.stackByType = _data['stackByType'];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['amount'] = this.amount;
        data['unit'] = this.unit;
        data['format'] = this.format;
        data['chartData'] = this.chartData;
        data['stackBy'] = this.stackBy;
        data['chartTitle'] = this.chartTitle;
        data['xAxisTitle'] = this.xAxisTitle;
        data['yAxisTitle'] = this.yAxisTitle;
        data['tooltipFormat'] = this.tooltipFormat;
        data['chartType'] = this.chartType;
        data['chartTOptions'] = this.chartTOptions;
        data['gridData'] = this.gridData;
        data['stackByType'] = this.stackByType;

        return data;
    }
}
