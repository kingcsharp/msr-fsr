import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege} from '../../../models/enums/privileges';
import { Procedure, ProcedureService, EnumApprovalTables, EnumMenuItem} from '../../../services/api.client.generated';

@Component({
  selector: 'app-procedures',
  templateUrl: './procedures.component.html',
  styleUrls: ['./procedures.component.scss'],
  providers: [ProcedureService]
})
export class ProceduresComponent implements OnInit {

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
  procedureToDelete: Procedure;

  constructor(private procedureService: ProcedureService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'creatorCompany', label: 'Creator Company', visible: true }),
      new ColumnsSaved({ id: 'createdByDepartmentName', label: 'Created By Department', visible: true }),
      new ColumnsSaved({ id: 'procedureType.name', label: 'Type', visible: true }),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
      new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
      ];

    this.canAdd = this.hasPrivilege(this.privileges.CanCreate);
    this.canDelete = this.hasPrivilege(this.privileges.CanActivate);
    this.canEdit = this.hasPrivilege(this.privileges.CanEdit);

    this.getProcedures();
  }

  getProcedures() {

    this.globals.showLoader(true);
    this.procedureService.procedureGet(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data  = response.object;
      this.loading = false;
    }));

  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.Locations, privName);
  }

  openConfirmDeleteDialog(procedure) {

    this.procedureToDelete = procedure;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog(procedure) {
    this.procedureToDelete = procedure;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  delete() {

    this.globals.showLoader(true);
    this.procedureService.procedureDelete(this.procedureToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    }, () => {
      this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    }));

  }

  copyProcedure(procedure) {

    this.data.length = 0;
    this.loading = true;
    this.getProcedures();


  }

}
