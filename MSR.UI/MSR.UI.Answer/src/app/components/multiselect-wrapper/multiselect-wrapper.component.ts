import { Component, OnInit, Input, Output, EventEmitter, ElementRef } from '@angular/core';
// import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
// import { CommonGrid } from '../../models/lib/CommonGrid'
// import { ViewSaved } from '../../models/lib/ViewSaved';
// import { Globals } from '../../models/lib/globals';
// import { TableState } from 'primeng/api';

@Component({
  selector: 'multiselect-wrapper',
  templateUrl: './multiselect-wrapper.component.html',
  styleUrls: ['./multiselect-wrapper.component.scss']
})
export class MultiselectWrapperComponent implements OnInit {
  selectedColumns: Array<any>;

  @Input() gridStorageId: string;
  @Input() options: any;
  @Input() filterId: string;
  @Input() defaultText: string;
  @Input() defaultTextTooltip: string;
  @Input() datatable: any;
  @Input() reset: any;
  constructor() {

  }

  ngOnInit(): void {
    this.selectedColumns = [];
    this.setSelectedColumns(this.options, this.datatable.filters[this.filterId]);
    this.datatable.onFilter.subscribe((elem) => {
      if (elem.filters[this.filterId] === undefined) {
        this.selectedColumns = [];
      }
    });
    this.datatable.onStateRestore.subscribe((elem) => {
      if (elem.filters[this.filterId] === undefined) {
        this.selectedColumns = [];
      }
      this.selectedColumns = elem.filters[this.filterId].value;
    })
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
