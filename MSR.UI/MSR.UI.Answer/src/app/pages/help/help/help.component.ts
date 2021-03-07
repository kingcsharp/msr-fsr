import { Component, OnInit, ElementRef } from '@angular/core';
import { HelpService, RoleService, Role, HelpPage, EnumMenuItem } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';
import { AllowedActions } from '../../../models/lib/AllowedActions';
import { callFunctionWithFilters } from '../../../models/lib/Utils';
import { LazyLoadEvent } from 'primeng/api';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss'],
  providers: [HelpService]
})
export class HelpComponent implements OnInit {
  privileges = EnumPrivilege;
  data: Array<HelpPage>;
  allRoles: Array<SelectItem>;
  roleFilter: string;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  showConfirmDeleteDialog: boolean = false;
  helpPageToDelete: HelpPage;
  gridVersion: string;
  menuItems = EnumMenuItem;
  userPrivileges: AllowedActions;
  helpContent: string;
  modalTitle: string;
  showPreviewDialog: boolean = false;
  totalRecords: number = 0;
  currentEvent: LazyLoadEvent;

  constructor(private helpService: HelpService, private roleService: RoleService,
    public commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'friendlyURL', label: 'Friendly URL', visible: true }),
      new ColumnsSaved({ id: 'roles', label: 'Roles', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];
    this.userPrivileges = this.globals.getEnumPrivileges(this.menuItems.HelpPages);
  }

  getHelpPages(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(this.helpService, this.helpService.helpGet, event)
        .pipe(take(1))
        .subscribe(responseHandler(response => {
          this.currentEvent = event;
          this.data = new Array<HelpPage>();
          this.totalRecords = response.totalNumberOfRecords;
          response.object.forEach(helpPage => {
            this.data.push(helpPage);
          });
          this.allRoles = new Array<SelectItem>();
          let distinctRolesFromReturnedResults = response.object.map(s => s.roles).flat().map(role => ({ label: role.name, value: role.name })).filter((value, index, self) => self.findIndex(role => role.label === value.label) === index);
          this.allRoles = this.allRoles.concat(distinctRolesFromReturnedResults);
        }));
    }, 10);
  }

  openHelpPage(helpage: HelpPage) {
    this.helpContent = helpage.content;
    this.modalTitle = helpage.title;
    this.showPreviewDialog = true;
  }

  openConfirmDeleteDialog(helpPage: HelpPage) {
    this.helpPageToDelete = helpPage;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog() {
    this.helpPageToDelete = null;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  deleteHelpPage() {
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    this.globals.showLoader(true);
    this.helpService.helpDelete(this.helpPageToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {
      this.getHelpPages(this.currentEvent);
    }));
  }
}
