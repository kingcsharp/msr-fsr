import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EnumColumnType } from '../../models/enums/EnumColumnType';
import { GridSaved } from '../../models/lib/GridSaved';
import { ViewSaved } from '../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../models/lib/CommonGrid';
import { Globals } from '../../models/lib/globals';
import {
  SearchService, ProcedureService
} from '../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../environments/environment';
import { EnumPrivilege } from '../../models/enums/privileges';
import { responseHandler } from '../../utils/responseHandler';


@Component({
  selector: 'app-search-component',
  templateUrl: './search.component.html',
  styleUrls: ['./search.component.scss']
})
export class SearchComponent implements OnInit {
  enumColumnType = EnumColumnType;
  defaultView: ViewSaved;
  data: any;
  gridSaved: GridSaved;
  selectedItem: any;
  display: boolean = false;

  constructor(private activatedRoute: ActivatedRoute, private elem: ElementRef, public cg: CommonGrid,
    private globals: Globals, private searchService: SearchService, private procedureService: ProcedureService) {
    this.activatedRoute.queryParams.subscribe(params => {
      this.getSearchData(params['search'])
    });
  }

  ngOnInit(): void {
    // let searchTerm = this.route.snapshot.queryParams["search"];
    // console.log(searchTerm);

    this.gridSaved = new GridSaved({
      columnsSaved: [new ColumnsSaved({ id: 'itemId', label: 'Id', visible: true, type: this.enumColumnType.Number }),
      new ColumnsSaved({ id: 'itemName', label: 'Name', visible: true, type: this.enumColumnType.String }),
      new ColumnsSaved({ id: 'itemType', label: 'Type', visible: true, type: this.enumColumnType.String, dropdownHeader: true }),
      new ColumnsSaved({ id: 'description', label: 'Description', visible: true, type: this.enumColumnType.String }),
      new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Updated On', visible: true, type: this.enumColumnType.Date }),
      new ColumnsSaved({ id: 'lastUpdatedBy', label: 'Updated By', visible: true, type: this.enumColumnType.String })
      ],
      storageId: 'search' + this.elem.nativeElement.tagName.toLowerCase(),
      version: '1.0.0'
    });
  }

  getSearchData(searchTerm) {
    this.globals.showLoader(true);
    this.searchService.search(searchTerm, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.globals.showLoader(false);
        this.data = response.object;
      }));
  }

  clseDialog() {
    this.display = false;
  }

  viewFunction(rowData) {
    
    switch (rowData.itemType) {
      case 'Procedure':
        this.procedureService.procedureGet(rowData.itemId, env.apiVersion).pipe(take(1))
          .subscribe(responseHandler(response => {
            this.selectedItem = response?.object[0];
            this.selectedItem.itemType = rowData.itemType;
          }));
        break;

      default:
        break;
    }
    this.display = true;
  }
}
