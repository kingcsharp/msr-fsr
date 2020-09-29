import { Injectable } from '@angular/core';
import * as moment from 'moment';
import { EnumColumnType } from '../models/enums/EnumColumnType';
import { ColumnsSaved } from '../models/lib/ColumnsSaved';

@Injectable()
export class CSVConverterService {
    // downloadFile(data: any, columns: string[], headerTitles: string[], filename = 'data') {
    downloadFile(data: any, columns: ColumnsSaved[], filename = 'data') {
        let csvData = this.ConvertToCSV(data, columns);
        let blob = new Blob(['\ufeff' + csvData], { type: 'text/csv;charset=utf-8;' });
        let dwldLink = document.createElement("a");
        let url = URL.createObjectURL(blob);
        let isSafariBrowser = navigator.userAgent.indexOf('Safari') != -1 && navigator.userAgent.indexOf('Chrome') == -1;
        if (isSafariBrowser) {  //if Safari open in new window to save file with random filename.
            dwldLink.setAttribute("target", "_blank");
        }
        dwldLink.setAttribute("href", url);
        dwldLink.setAttribute("download", `${filename}${moment().format('MM_DD_YYYY')}.csv`);
        dwldLink.style.visibility = "hidden";
        document.body.appendChild(dwldLink);
        dwldLink.click();
        document.body.removeChild(dwldLink);
    }

    ConvertToCSV(objArray, columns: ColumnsSaved[]) {
        let array = typeof objArray != 'object' ? JSON.parse(objArray) : objArray;
        let str = '';
        let row = '';
        for (let index in columns) {
            row += columns[index].label + ',';
        }
        row = row.slice(0, -1);
        str += row + '\r\n';
        for (let i = 0; i < array.length; i++) {
            let line = '';
            for (let index in columns) {
                let propName = columns[index].id;
                const value = array[i][propName];
                const item = "\"" + this.getDefaultValue(value, columns[index]) + "\"";
                if (line.length > 0) {
                    line += ',';
                }

                line += item;
            }
            str += line + '\r\n';
        }
        return str;
    }

    getDefaultValue(value, column: ColumnsSaved) {
        if (value === null || value === undefined) {
            if (column.type === EnumColumnType.Number) {
                return 0;
            }
            return "";
        }
        if (column.type === EnumColumnType.Date) {
            return moment(value).format(column.formattingMoment);
        }
        if (column.type === EnumColumnType.Boolean) {
            return value === true ? 'Yes' : 'No';
        }

        return value;
    }
}