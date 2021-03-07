import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { ProcedureType, ProcedureTypeService, EnumApprovalTables, EnumMenuItem } from '../../../services/api.client.generated';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import { LazyLoadEvent } from 'primeng/api';
import { take } from 'rxjs/operators';
@Component({
  selector: 'app-proceduretypes',
  templateUrl: './proceduretypes.component.html',
  styleUrls: ['./proceduretypes.component.scss'],
  providers: [ProcedureTypeService]
})
export class ProceduretypesComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  canAdd: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;
  menuItems = EnumMenuItem;
  statusOptions: any[];
  showConfirmDeleteDialog: boolean = false;
  procedureTypeToDelete: ProcedureType;
  gridVersion: string;
  totalRecords: number = 0;
  currentEvent: LazyLoadEvent;

  constructor(private procedureTypeService: ProcedureTypeService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridVersion = '1.0.1';
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    this.canAdd = this.hasPrivilege(this.privileges.CanCreate);
    this.canDelete = this.hasPrivilege(this.privileges.CanDelete);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);
    this.statusOptions = this.globals.getTopLevelStatus();
  }

  getProcedureTypes(event: LazyLoadEvent) {
    callFunctionWithFilters(this.procedureTypeService, this.procedureTypeService.procedureTypeGet, event)
      .pipe(take(1))
      .subscribe(responseHandler((response) => {
        this.totalRecords = response.totalNumberOfRecords;
        this.currentEvent = event;
        this.data = response.object;
      }));

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.ProcedureTypes, privName);
  }

  openConfirmDeleteDialog(procedureType) {
    this.procedureTypeToDelete = procedureType;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {
    this.procedureTypeService.procedureTypeDelete(this.procedureTypeToDelete.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler((response) => {
        this.getProcedureTypes(this.currentEvent);
        this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
      }));

  }
}
