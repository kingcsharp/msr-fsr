import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterUtils } from 'primeng/utils';

@Component({
  selector: 'multiselect-wrapper',
  templateUrl: './multiselect-wrapper.component.html',
  styleUrls: ['./multiselect-wrapper.component.scss']
})
export class MultiselectWrapperComponent implements OnInit {
  selectedColumns: Array<any>;
  subscriptions: Subscription[] = [];

  @Input() gridStorageId: string;
  @Input() options: any;
  @Input() filterId: string;
  @Input() defaultText: string;
  @Input() defaultTextTooltip: string;
  @Input() datatable: any;
  @Input() reset: any;
  @Input() filterProp: string;
  @Input() multipleValues: boolean;
  constructor() {

  }

  ngOnInit(): void {
    const ctrl = this;
    FilterUtils['multipleValuesFilter'] = (value, filter): boolean => {
      let found = false;
      filter.forEach(fElement => {
        value.forEach(vElement => {
          if (fElement === vElement[ctrl.filterProp]) {
            found = true;
            return;
          }
        });
      });
      return found;
    };
    this.selectedColumns = [];
    this.setSelectedColumns(this.options, this.datatable.filters[this.filterId]);
    const sub1 = this.datatable.onFilter.subscribe((elem) => {
      if (elem.filters[this.filterId] === undefined) {
        this.selectedColumns = [];
      }
    });
    const sub2 = this.datatable.onStateRestore.subscribe((elem) => {
      if (elem.filters[this.filterId] === undefined) {
        this.selectedColumns = [];
      }
      this.selectedColumns = elem.filters[this.filterId].value;
    });
    this.subscriptions.push(sub1);
    this.subscriptions.push(sub2);
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  filterGrid() {
    if (this.multipleValues) {
      this.datatable.filter(this.selectedColumns, this.filterId, 'multipleValuesFilter');
    } else {
      this.datatable.filter(this.selectedColumns, this.filterId, 'in');
    }
  }

  setSelectedColumns(options, filters) {
    if (filters === undefined || filters.value.length === 0) {
      return;
    }
    options.forEach(element => {
      if (filters.value.indexOf(element.value) > -1) {
        this.selectedColumns.push(element.value);
      }
    });
  }

}
