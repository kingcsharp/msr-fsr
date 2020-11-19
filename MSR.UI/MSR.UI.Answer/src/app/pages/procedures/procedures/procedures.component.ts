import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege} from '../../../models/enums/privileges';
import { Procedure, ProcedureService, EnumApprovalTables, EnumMenuItem} from '../../../services/api.client.generated';
import { AllowedActions } from '../../../models/lib/AllowedActions';

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
  gridVersion: string;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  menuItems = EnumMenuItem;
  statusOptions: any[];
  showConfirmDeleteDialog: boolean = false;
  procedureToDelete: Procedure;
  procedurePrivileges: AllowedActions;

  constructor(
    private procedureService: ProcedureService,
    public commonGrid: CommonGrid,
    private elementReference: ElementRef,
    public globals: Globals
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true}),
      new ColumnsSaved({ id: 'name', label: 'Name', visible: true }),
      new ColumnsSaved({ id: 'procedureType.name', label: 'Procedure Type', visible: true}),
      new ColumnsSaved({ id: 'duration', label: 'Duration', visible: false}),
      new ColumnsSaved({ id: 'durationType', label: 'Duration Type', visible: false}),
      new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true}),
      new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true}),
      new ColumnsSaved({ id: 'created.fullName', label: 'Created By', visible: false}),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: false }),
      new ColumnsSaved({ id: 'lastUpdated.fullName', label: 'Last Updated By', visible: true}),
      new ColumnsSaved({ id: 'lastUpdatedOn', label: 'Last Updated On', visible: true}),
      new ColumnsSaved({ id: 'Actions', label: 'Actions', visible: true})
    ];
    this.procedurePrivileges = this.globals.getEnumPrivileges(this.menuItems.RunnableProcedures);
    this.getProcedures();
  }

  getProcedures() {
    this.globals.showLoader(true);
    this.procedureService.procedureGet(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data  = response.object;
    }));
  }

  hasPrivilege(privName) {
    return this.globals.hasPrivilege(EnumMenuItem.RunnableProcedures, privName);
  }

  openConfirmDeleteDialog(procedure) {
    this.procedureToDelete = procedure;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
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
    this.getProcedures();
  }
}
