import { Component, OnInit,  ElementRef  } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { ProcedureTypeMock } from '../../../services/mocks/models/ProcedureTypeMock';

@Component({
  selector: 'app-proceduretypes',
  templateUrl: './proceduretypes.component.html',
  styleUrls: ['./proceduretypes.component.scss'],
  providers: [MockServices]
})
export class ProceduretypesComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAdd: boolean = false;
  canEdit: boolean = false;
  canDelete: boolean = false;
  menuItems = EnumMenuItem;
  statusOptions: any[];
  showConfirmDeleteDialog: boolean = false;
  procedureTypeToDelete: ProcedureTypeMock;
  constructor(private mockService: MockServices, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'type', label: 'Type', visible: true }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
      ];

    this.canAdd = this.hasPrivilege(this.privileges.CanCreate);
    this.canDelete = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

    this.getProcedureTypes()
  }

  getProcedureTypes(){

    this.data = this.mockService.procedureTypesGet(null);
    this.statusOptions = this.data.filter(
      (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
    ).map(x => ({ label: x.status, value: x.status }));
    this.loading = false;
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  openConfirmDeleteDialog(procedureType) {

    this.procedureTypeToDelete = procedureType;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog(procedureType) {
    this.procedureTypeToDelete = procedureType;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete(){

    this.mockService.procedureTypesDelete(this.procedureTypeToDelete.id);
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }
}
