import { Component, OnInit, Output, Input, EventEmitter, ElementRef } from '@angular/core';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { Globals } from '../../models/lib/globals';

@Component({
  host: {
    '(document:click)': 'onClick($event)',
  },
  selector: 'grid-options',
  templateUrl: './grid-options.component.html',
  // template: `<div>HIasfasf</div>`,
  styleUrls: ['./grid-options.component.scss']
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

  @Input() defaultColumns: Array<ColumnsSaved>;
  @Output() defaultColumnsChange: EventEmitter<Array<ColumnsSaved>> = new EventEmitter<Array<ColumnsSaved>>();
  @Input() gridStorageId: string;
  @Input() gridVersion: string;
  constructor(public globals: Globals, private _eref: ElementRef) {
  }

  ngOnInit(): void {
    this.updateDefaultColumns(this.globals.getDefaultView(this.gridStorageId));

    this.columnPicker = this.defaultColumns.map((elem) => {
      return { label: elem.label, value: { id: elem.id, name: elem.label, visible: elem.visible } }
    });
    this.selectedColumns = this.defaultColumns.filter(x=>x.visible).map((elem) => {
      return { id: elem.id, name: elem.label, visible: elem.visible }
    });

    this.viewToSave = new ViewSaved({
      version: this.gridVersion, isDefault: false, gridId: this.gridStorageId
    });

    this.viewsSaved = this.globals.getViews(this.gridStorageId);
  }

  onClick(event) {
    if (!this._eref.nativeElement.contains(event.target)) // or some similar check
    {
      this.columnDropdown = false;
    }
  }

  public columnDropdownFn() {
    this.columnDropdown = !this.columnDropdown;
    this.gridOptionsRotate = true;
    setTimeout(() => { this.gridOptionsRotate = false }, 900);
  }

  public changeColVisibility(value) {
    if (value === undefined) {
      return;
    }
    this.defaultColumns.forEach((elem) => {
      elem.visible = value.findIndex(x => x.id == elem.id) > -1;
    });
    this.defaultColumnsChange.emit(this.defaultColumns);
  }

  public updateDefaultColumns(view: ViewSaved) {
    if (view===null || view.columns === undefined) {
      return;
    }
    this.defaultColumns.forEach((elem) => {
      elem.visible = view.columns.find(x => x.id == elem.id).visible;
    });
    this.defaultColumnsChange.emit(this.defaultColumns);
  }

  public savedViewChange(view: ViewSaved) {
    view.isDefault = !view.isDefault;
    this.globals.setAsDefault(view);
    this.defaultView = view;
  }

  public deleteView(view: ViewSaved) {
    this.globals.deleteView(view);
    this.viewsSaved = this.globals.getViews(this.gridStorageId);
  }

  public showSaveViewDiv() {
    this.showSaveView = !this.showSaveView;
    if (this.showSaveView) {
      this.viewToSave = new ViewSaved({ version: this.gridVersion, isDefault: false, gridId: this.gridStorageId, columns: this.defaultColumns });
    }
  }

  public saveView() {
    const savedView = new ViewSaved({ version: this.gridVersion, isDefault: false });
    Object.assign(savedView, this.viewToSave);
    this.globals.addView(savedView);
    this.viewsSaved = this.globals.getViews(this.gridStorageId);
  }

  public stopEvent(event) {
    this.columnDropdown = false;
    event.preventDefault();
    event.stopPropagation();
  }

}
