import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterUtils } from 'primeng/utils';
import * as moment from 'moment';
import { CommonGrid } from '../../models/lib/CommonGrid';
// import {  } from 'moment';

@Component({
  selector: 'pcalendar-wrapper',
  templateUrl: './pcalendar-wrapper.component.html'
})
export class PcalendarWrapperComponent implements OnInit {
  selectedDate: Date;
  subscriptions: Subscription[] = [];
  en: any;
  @Input() gridStorageId: string;
  @Input() filterId: string;
  @Input() datatable: any;
  @Input() reset: any;
  constructor(public cg: CommonGrid) { }

  ngOnInit(): void {
    // this.selectedColumns = [];
    // this.setSelectedColumns(this.options, this.datatable.filters[this.filterId]);
    this.en = {
      firstDayOfWeek: 0,
      dayNames: ['Sunday', 'Monday', 'Tuesday', 'Wednesday', 'Thursday', 'Friday', 'Saturday'],
      dayNamesShort: ['Sun', 'Mon', 'Tue', 'Wed', 'Thu', 'Fri', 'Sat'],
      dayNamesMin: ['Su', 'Mo', 'Tu', 'We', 'Th', 'Fr', 'Sa'],
      monthNames: ['January', 'February', 'March', 'April', 'May', 'June', 'July', 'August', 'September', 'October', 'November', 'December'],
      monthNamesShort: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
      today: 'Today',
      clear: 'Clear',
      dateFormat: 'yyy-mm-dd'
    };
    const ctrl = this;
    
    FilterUtils['dateRangeFilter'] = (value, filter): boolean => {
      // IF WE USE RANGE AS FILTER THEN it would just be setting range in the pcalendar-wrapper selectionMode='range' and here filter would be an array.
      return moment(filter).startOf('day').isBefore(value) && moment(filter).endOf('day').isAfter(value);
    };

    this.setSelectedDate(this.datatable.filters[this.filterId]);

    const sub1 = this.datatable.onFilter.subscribe((elem) => {
      if (Object.keys(elem.filters).length === 0) {
        this.selectedDate = undefined;
      }
    });

    const sub2 = this.datatable.onStateRestore.subscribe((elem) => {
      if (elem.filters[ctrl.filterId] === undefined) {
        this.selectedDate = undefined;
      }
      this.selectedDate = moment(elem.filters[ctrl.filterId].value).toDate();
    });

    this.subscriptions.push(sub1);
    this.subscriptions.push(sub2);
  }

  clearFilter() {
    this.selectedDate = undefined;
    this.cg.filter(this.datatable, this.filterId);
  }

  setSelectedDate(filters) {
    if (filters === undefined || filters.value.length === 0) {
      return;
    }
    this.selectedDate = moment(filters.value).toDate();
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  filterGrid() {
    this.datatable.filter(this.selectedDate, this.filterId, 'dateRangeFilter');
  }
}
