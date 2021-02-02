import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { Globals } from '../../models/lib/globals';
import { TableState } from 'primeng/api';
import cloneDeep from 'lodash.cloneDeep';
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
  viewsSaved: Array<ViewSaved> = new Array<ViewSaved>();
  viewToSave: ViewSaved;
  showSaveView: boolean = false;
  savedViewsOptions: any;
  defaultView: ViewSaved;
  showRemove: boolean = false;
  toDeleteView: ViewSaved;

  @Input() defaultColumns: Array<ColumnsSaved>;
  @Output() defaultColumnsChange: EventEmitter<Array<ColumnsSaved>> = new EventEmitter<Array<ColumnsSaved>>();
  @Input() visibleColumnsCount: number = 0;
  @Output() visibleColumnsCountChange: EventEmitter<number> = new EventEmitter<number>();
  @Input() gridStorageId: string;
  @Input() gridVersion: string;
  @Input() ptable: any;
  constructor(public globals: Globals, private _eref: ElementRef, public cg: CommonGrid) {
  }

  ngOnInit(): void {
    this.gridSettings = cloneDeep(this.defaultColumns);
    this.updateDefaultColumns(this.cg.getDefaultView(this.gridStorageId, this.gridVersion));

    this.columnPicker = this.defaultColumns.map((elem) => {
      return { label: elem.label, value: { id: elem.id, name: elem.label, visible: elem.visible } };
    });

    this.selectedColumns = this.defaultColumns.filter(x => x.visible).map((elem) => {
      return { id: elem.id, name: elem.label, visible: elem.visible };
    });

    this.visibleColumnsCount = this.selectedColumns.length;

    this.viewToSave = this.getNewView();

    this.viewsSaved = this.cg.getViews(this.gridStorageId);
  }

  getNewView() {
    return new ViewSaved({
      version: this.gridVersion, isDefault: false,
      gridId: this.gridStorageId, viewName: ''
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
    this.visibleColumnsCount = this.selectedColumns.length;
    this.visibleColumnsCountChange.emit(this.visibleColumnsCount);
    this.defaultColumnsChange.emit(this.defaultColumns);
  }

  public updateDefaultColumns(view: ViewSaved) {
    let visibleCols = 0;
    this.defaultColumns.forEach((elem) => {
      if (view === null || view === undefined || view.columns === undefined) {
        elem.visible = this.gridSettings.find(x => x.id === elem.id).visible;
      } else {
        elem.visible = view.columns.find(x => x.id === elem.id).visible;
      }
      if (elem.visible) {
        visibleCols++;
      }
    });
    this.visibleColumnsCount = visibleCols;
    this.visibleColumnsCountChange.emit(this.visibleColumnsCount);
    this.defaultColumnsChange.emit(this.defaultColumns);

    this.selectedColumns = this.defaultColumns.filter(x => x.visible).map((elem) => {
      return { id: elem.id, name: elem.label, visible: elem.visible };
    });
  }

  public savedViewChange(view: ViewSaved) {
    view.isDefault = !view.isDefault;
    this.cg.setAsDefault(view);
    this.defaultView = view;
  }

  public resetgrid(view) {
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

    localStorage.setItem(this.gridStorageId, JSON.stringify({ 'first': 0, 'rows': 10 }));
    this.updateDefaultColumns(view);
  }

  restoreState(view: ViewSaved) {
    let state: TableState = JSON.parse(view.gridPagingData);

    if (state === null || state.filters === undefined) {
      this.resetgrid(view);
      return;
    }

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

    this.updateDefaultColumns(view);
    this.visibleColumnsCount = this.selectedColumns.length;
    this.visibleColumnsCountChange.emit(this.visibleColumnsCount);
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

  public updateTemplateWithCurrentView(tplView: ViewSaved) {
    const columns = cloneDeep(this.defaultColumns).map(e => new ColumnsSaved(e));
    const savedView = new ViewSaved({ version: this.gridVersion, isDefault: false, columns: columns });
    Object.assign(savedView, this.viewToSave);
    this.cg.updateView(tplView, savedView);
  }

  public saveView() {
    const columns = cloneDeep(this.defaultColumns).map(e => new ColumnsSaved(e));
    const savedView = new ViewSaved({ version: this.gridVersion, isDefault: false, columns: columns });
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
