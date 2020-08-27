import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from 'rxjs';
import { FilterUtils } from 'primeng/utils';

@Component({
  selector: 'multiselect-wrapper',
  templateUrl: './multiselect-wrapper.component.html'
})
export class MultiselectWrapperComponent implements OnInit {
  selectedColumns: Array<any>;
  subscriptions: Subscription[] = [];
  multiselectName: string = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);

  @Input() gridStorageId: string;
  @Input() options: any;
  @Input() filterId: string;
  @Input() defaultText: string;
  @Input() defaultTextTooltip: string;
  @Input() datatable: any;
  @Input() defaultId: string;
  @Input() reset: any;
  @Input() filterProp: string;
  @Input() multipleValues: boolean;
  currentOptions: any = [];
  basicOptions: any;
  isOldFilter: boolean = false;
  constructor() {
  }

  ngOnInit(): void {
    const ctrl = this;
    this.selectedColumns = [];
    this.basicOptions = {
      name: this.filterProp || 'name',
      id: this.defaultId || 'id'
    };
    FilterUtils['multipleValuesFilter' + this.multiselectName] = (value, filter): boolean => {
      let found = false;
      filter.forEach(fElement => {
        if (value !== undefined) {
          value.forEach(vElement => {
            if (ctrl.isOldFilter) {
              if (fElement === vElement[ctrl.filterProp]) {
                found = true;
                return;
              }
            } else if (fElement.id === vElement[ctrl.basicOptions.id]) {
              found = true;
              return;
            }
          });
        }
      });
      return found;
    };
    this.pushOptions();
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

  ngDoCheck() {
    if (this.options === undefined) {
      return;
    }
    const lengthChange = this.options.length !== this.currentOptions.length;
    if (lengthChange) {
      this.pushOptions();
      this.currentOptions.sort((a, b) => (a.label > b.label) ? 1 : -1);
    }
  }

  dataAlreadyParsed(data) {
    if (data[0].label !== undefined && data[0].value !== undefined) {
      this.isOldFilter = true;
      return true;
    }
    return false;
  }

  pushOptions() {
    const ctrl = this;
    if (this.options === undefined) {
      return;
    }

    this.options.map((item) => {
      if (this.multipleValues && !this.dataAlreadyParsed(ctrl.options)) {
        let filterItem = item[this.filterId];
        if (filterItem == undefined) {
          filterItem = item;
        }
        if (filterItem.length > 0) {
          filterItem.forEach(element => {
            let length = this.currentOptions.length;
            let found = false;
            while (length--) {
              const existingItem = this.currentOptions[length];
              if (existingItem.value.id === element[ctrl.basicOptions.id]) {
                found = true;
                length = 0;
              }
            }
            if (!found) {
              this.currentOptions.push({
                label: element[ctrl.basicOptions.name],
                value: {
                  id: element[ctrl.basicOptions.id],
                  name: element[ctrl.basicOptions.name]
                }
              });
            }
          });
        }
      }
      else {
        this.currentOptions.push({
          label: this.getLabel(item),
          value: this.getValue(item)
        });
      }
    });

    // this.setSelectedColumns(this.options, this.datatable.filters[this.filterId]);
  }

  getLabel(item) {
    if (item.label !== undefined) {
      return item.label;
    }
    return item[this.basicOptions.name]
  }

  getValue(item) {
    if (this.defaultId === undefined) {
      if (item.value !== undefined) {
        return item.value;
      }
    }
    return item[this.basicOptions.id];
  }

  ngOnDestroy() {
    this.subscriptions.forEach((subscription) => subscription.unsubscribe());
  }

  filterGrid() {
    if (this.multipleValues) {
      this.datatable.filter(this.selectedColumns, this.filterId, 'multipleValuesFilter' + this.multiselectName);
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
