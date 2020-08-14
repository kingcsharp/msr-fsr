import { Component, OnInit, Input } from '@angular/core';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { Globals } from '../../models/lib/globals';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';

@Component({
  selector: '[grid-input-filter]',
  templateUrl: './grid-input-filter.component.html',
  styleUrls: ['./grid-input-filter.component.scss']
})
export class GridInputFilterComponent implements OnInit {
  generatedId: string = Math.random().toString(36).substring(2, 15) + Math.random().toString(36).substring(2, 15);
  filterTooltipLabel: string;
  show = false;
  @Input() searchField: string;
  @Input() dt: any;
  @Input() gridSettings: ColumnsSaved[];
  constructor(public globals: Globals, public cg: CommonGrid) {

  }

  ngOnInit(): void {
    this.filterTooltipLabel = this.gridSettings.find(x => x.id === this.searchField).label;
    this.show = true;
  }
}
