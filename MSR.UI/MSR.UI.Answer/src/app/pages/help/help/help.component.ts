import { Component, OnInit, ElementRef } from '@angular/core';
import { HelpService, RoleService, Role, HelpPage } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';

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
  roleFilter:string;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;
  canAddHelpPage: boolean = true;
  canEditHelpPage: boolean = true;
  canDeleteHelpPage: boolean = true;
  showConfirmDeleteDialog: boolean = false;
  helpPageToDelete: HelpPage;

  constructor(private helpService: HelpService,private roleService: RoleService, 
    private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'friendlyURL', label: 'Friendly URL', visible: true }),
      new ColumnsSaved({ id: 'roles', label: 'Roles', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    //this.canAddHelpPage = this.hasPrivilege(this.privileges.CanCreate);
    //this.canDeleteHelpPage = this.hasPrivilege(this.privileges.CanActivate);
    //this.canEditHelpPage = this.hasPrivilege(this.privileges.CanEdit);
    this.getHelpPages();
  }

  getHelpPages(){

    this.globals.showLoader(true);
    this.helpService.helpGet(null, env.apiVersion).subscribe(responseHandler(response => {
      this.data = new Array<HelpPage>();
      console.log(response)
      
      response.object.forEach(helpPage => {
        this.data.push(helpPage);
      });
      this.allRoles = new Array<SelectItem>();
      let distinctRolesFromReturnedResults = response.object.map(s => s.roles).flat().map(role => ({ label: role.name, value: role.name }) ).filter((value, index, self) => self.findIndex(role => role.label === value.label) === index);
      this.allRoles = this.allRoles.concat(distinctRolesFromReturnedResults);
      this.loading = false;
      
    }));

  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege('HelpPages', privilegeName);
  }

  openConfirmDeleteDialog(helpPage: HelpPage){

    this.helpPageToDelete = helpPage;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  closeConfirmDeleteDialog(helpPage: HelpPage){
    this.helpPageToDelete = null;
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
  }

  deleteHelpPage(){
    this.showConfirmDeleteDialog = !this.showConfirmDeleteDialog;
    this.globals.showLoader(true);
    this.helpService.helpDelete(this.helpPageToDelete.id, env.apiVersion).subscribe(responseHandler((response) => {

      console.log(response);

    }));
  }

}