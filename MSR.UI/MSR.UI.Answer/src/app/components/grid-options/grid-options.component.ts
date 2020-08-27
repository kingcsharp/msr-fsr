import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { Globals } from '../../models/lib/globals';
import { TableState } from 'primeng/api';

@Component({
  host: {
    '(document:click)': 'onClick($event)',
  },
  selector: 'grid-options',
  templateUrl: './grid-options.component.html'

})
export class GridOptionsComponent implements OnInit {
  gridSettings: ColumnsSaved[];
  selectedColumns: any;
  columnPicker: any;
  columnDropdown: boolean = false;
  gridOptionsRotate: boolean = false;
  viewsSaved: Array<ViewSaved>;
  viewToSave: ViewSaved;
  showSaveView: boolean = false;
  savedViewsOptions: any;
  defaultView: ViewSaved;
  showRemove: boolean = false;
  toDeleteView: ViewSaved;

  @Input() defaultColumns: Array<ColumnsSaved>;
  @Output() defaultColumnsChange: EventEmitter<Array<ColumnsSaved>> = new EventEmitter<Array<ColumnsSaved>>();
  @Input() gridStorageId: string;
  @Input() gridVersion: string;
  @Input() ptable: any;
  constructor(public globals: Globals, private _eref: ElementRef, public cg: CommonGrid) {
  }

  ngOnInit(): void {
    this.updateDefaultColumns(this.cg.getDefaultView(this.gridStorageId));

    this.columnPicker = this.defaultColumns.map((elem) => {
      return { label: elem.label, value: { id: elem.id, name: elem.label, visible: elem.visible } };
    });
    this.selectedColumns = this.defaultColumns.filter(x => x.visible).map((elem) => {
      return { id: elem.id, name: elem.label, visible: elem.visible };
    });

    this.viewToSave = this.getNewView();

    this.viewsSaved = this.cg.getViews(this.gridStorageId);
  }

  getNewView() {
    return new ViewSaved({
      version: this.gridVersion, isDefault: false, gridId: this.gridStorageId, viewName: ''
    });
  }

  onClick(event) {
    if (!this._eref.nativeElement.contains(event.target)) {
      this.columnDropdown = false;
      this.viewToSave = this.getNewView();
    }
  }

  public columnDropdownFn() {
    this.columnDropdown = !this.columnDropdown;
    this.gridOptionsRotate = true;
    setTimeout(() => { this.gridOptionsRotate = false; }, 900);
  }

  public changeColVisibility(value) {
    if (value === undefined) {
      return;
    }
    this.defaultColumns.forEach((elem) => {
      elem.visible = value.findIndex(x => x.id === elem.id) > -1;
    });
    this.defaultColumnsChange.emit(this.defaultColumns);
  }

  public updateDefaultColumns(view: ViewSaved) {
    if (view === null || view.columns === undefined) {
      return;
    }
    this.defaultColumns.forEach((elem) => {
      elem.visible = view.columns.find(x => x.id === elem.id).visible;
    });
    this.defaultColumnsChange.emit(this.defaultColumns);
  }

  public savedViewChange(view: ViewSaved) {
    view.isDefault = !view.isDefault;
    this.cg.setAsDefault(view);
    this.defaultView = view;
  }

  public resetgr() {
    this.ptable.onFilter.emit({
      filters: {},
      filteredValue: null
    });

    this.ptable._sortField = null;
    this.ptable._sortOrder = this.ptable.defaultSortOrder;
    this.ptable._multiSortMeta = null;
    this.ptable.tableService.onSort(null);

    this.ptable.filteredValue = null;
    this.ptable.filters = {};

    this.ptable.first = 0;
    this.ptable.firstChange.emit(this.ptable.first);

    if (this.ptable.lazy) {
      this.ptable.onLazyLoad.emit(this.ptable.createLazyLoadMetadata());
    } else {
      this.ptable.totalRecords = (this.ptable._value ? this.ptable._value.length : 0);
    }
  }

  restoreState(view: ViewSaved) {
    let state: TableState = JSON.parse(view.gridPagingData);

    if (this.ptable.paginator) {
      this.ptable.first = state.first;
      this.ptable.rows = state.rows;
      this.ptable.firstChange.emit(this.ptable.first);
      this.ptable.rowsChange.emit(this.ptable.rows);
    }

    if (state.sortField) {
      this.ptable.restoringSort = true;
      this.ptable._sortField = state.sortField;
      this.ptable._sortOrder = state.sortOrder;
    }

    if (state.multiSortMeta) {
      this.ptable.restoringSort = true;
      this.ptable._multiSortMeta = state.multiSortMeta;
    }

    if (state.filters) {
      this.ptable.restoringFilter = true;
      this.ptable.filters = state.filters;
    }

    if (this.ptable.resizableColumns) {
      this.ptable.columnWidthsState = state.columnWidths;
      this.ptable.tableWidthState = state.tableWidth;
    }

    if (state.expandedRowKeys) {
      this.ptable.expandedRowKeys = state.expandedRowKeys;
    }

    if (state.selection) {
      Promise.resolve(null).then(() => this.ptable.selectionChange.emit(state.selection));
    }

    this.ptable.stateRestored = true;
    this.ptable.onStateRestore.emit(state);

    if (this.ptable.filterTimeout) {
      clearTimeout(this.ptable.filterTimeout);
    }
    this.ptable.filterTimeout = setTimeout(() => {
      this.ptable._filter();
      this.ptable.filterTimeout = null;
    }, this.ptable.filterDelay);
  }

  public deleteView(view: ViewSaved) {
    this.toDeleteView = view;
    this.showRemove = true;
  }
  public removeView() {
    this.cg.deleteView(this.toDeleteView);
    this.viewsSaved = this.cg.getViews(this.gridStorageId);
    this.showRemove = false;
  }

  public showSaveViewDiv() {
    try {
      if (!this.showSaveView) {
        const viewData = { version: this.gridVersion, isDefault: false, gridId: this.gridStorageId, columns: this.defaultColumns };
        this.viewToSave = new ViewSaved(viewData);
      }
      this.showSaveView = !this.showSaveView;
    } catch (err) {
      console.log(err);
    }
  }

  public updateTemplateWithCurrentView(tplView: ViewSaved) {
    this.cg.updateView(tplView, this.viewToSave);
  }

  public saveView() {
    const savedView = new ViewSaved({ version: this.gridVersion, isDefault: false });
    Object.assign(savedView, this.viewToSave);
    this.cg.addView(savedView);
    this.viewsSaved = this.cg.getViews(this.gridStorageId);
    this.viewToSave = this.getNewView();
  }

  public stopEvent(event) {
    this.columnDropdown = false;
    event.preventDefault();
    event.stopPropagation();
  }

}
