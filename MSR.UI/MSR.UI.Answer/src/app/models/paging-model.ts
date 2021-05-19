import { Options } from "highcharts";
import { ChartInfo } from "./lib/ChartInfo";

export interface IPagingModel {
    pageNumber: number;
    pageSize: number;
    totalRows: number;
    data: Array<any>;
    HighChartsOptions: Options;
    ChartInformation: ChartInfo;
    queryString: string;
    partsdata: Array<any>;
}


export class PagingModel {

    public pageNumber: number = 0;
    public pageSize: number = 10;
    public totalRows: number = 0;
    public data: Array<any> = new Array<any>();
    public HighChartsOptions: Options;
    public ChartInformation: ChartInfo;
    public queryString: string = '';
    public sortTerm: string = null;
    public sortAscending: boolean = null;
    partsdata: Array<any> = new Array<any>();

    constructor(pagingModel: IPagingModel) {
        this.pageNumber = pagingModel.pageNumber === undefined ? 0 : pagingModel.pageNumber;
        this.pageSize = pagingModel.pageSize === undefined ? 10 : pagingModel.pageSize;
        this.totalRows = pagingModel.totalRows === undefined ? 0 : pagingModel.totalRows;
        this.data = pagingModel.data;
        this.HighChartsOptions = pagingModel.HighChartsOptions;
        this.ChartInformation = pagingModel.ChartInformation;
        this.partsdata = pagingModel.partsdata;
    }

    get HasChart(){

        if(this.HighChartsOptions === undefined || this.ChartInformation === undefined){
            return false;
        } else {
            return true;
        }

    }

    public getReportingQueryString(apiEndPointUrl, forReportDownload = true): string {

        if(forReportDownload){

            apiEndPointUrl = `${apiEndPointUrl}?pagesize=${this.pageSize}&pagenumber=${this.pageNumber}`;

            if(this.queryString !== ''){
                apiEndPointUrl += `&${this.queryString}`;
            }

            if(this.sortTerm !== null && this.sortAscending !== null){

                apiEndPointUrl += `&term=${this.sortTerm}&sortascending=${this.sortAscending}`;
    
            }
            
        } else {

            if(this.queryString !== ''){
                apiEndPointUrl += `?${this.queryString}`;
            }

            
        }

        

        return apiEndPointUrl;
        
    }
}