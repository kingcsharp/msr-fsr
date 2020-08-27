import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Globals } from '../models/lib/globals';

@Pipe({
    name: 'userTimezone'
})
export class TimeZonePipe implements PipeTransform {

    currentUserGmtTimezone: string;
    constructor(private globals: Globals, private datePipe: DatePipe) {
        this.currentUserGmtTimezone = globals.user.timezonePipe;
    }

    transform(value: Date, format: string = 'short'): string {
        if (value) {
            var convert = this.datePipe.transform(value, format, this.currentUserGmtTimezone);
            return convert;
        }
    }
}