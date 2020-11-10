import { Pipe, PipeTransform } from '@angular/core';
import { DatePipe } from '@angular/common';
import { Globals } from '../models/lib/globals';
import * as moment from 'moment';

@Pipe({
  name: 'userTimezone'
})
export class TimeZonePipe implements PipeTransform {

  currentUserSTDTimezone: string;
  currentUserDSTTimezone: string;
  constructor(private globals: Globals, private datePipe: DatePipe) {
    this.currentUserSTDTimezone = globals.user.timezoneSTDPipe;
    this.currentUserDSTTimezone = globals.user.timezoneDSTPipe;
  }

  transform(value: Date, format: string = 'short'): string {
    if (value) {
      let convert = this.datePipe.transform(value, format, moment(value).isDST() ? this.currentUserDSTTimezone : this.currentUserSTDTimezone);
      return convert;
    }
  }
}
