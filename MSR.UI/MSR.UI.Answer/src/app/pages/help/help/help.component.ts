import { Component, OnInit, ElementRef } from '@angular/core';
import { HelpService, RoleService, Role } from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { RoleModule } from '../../main/roleassignments/roleassignments.component';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-help',
  templateUrl: './help.component.html',
  styleUrls: ['./help.component.scss'],
  providers: [HelpService]
})
export class HelpComponent implements OnInit {

  data: Array<HelpPageModule>;
  roles: SelectItem[] = new Array<SelectItem>();
  roleFilter:string;
  gridSettings: ColumnsSaved[] = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;

  constructor(private helpService: HelpService,private roleService: RoleService, private commonGrid: CommonGrid, private elementReference: ElementRef) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
      new ColumnsSaved({ id: 'friendlyURL', label: 'Friendly URL', visible: true }),
      new ColumnsSaved({ id: 'roles', label: 'Roles', visible: true }),
      new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
    ];

    this.getHelpPages();
  }

  getHelpPages(){

    this.helpService.helpGet(null, env.apiVersion).subscribe(response => {
      this.data = new Array<HelpPageModule>();
      let mockId = 1; //TODO Remove mock Id for real ones from API
      response.returnedObject.forEach(element => {
        element.id = mockId++;
        this.data.push(element);
      });
      this.roles = this.roles.concat(response.returnedObject.map( s => s.roles).flat(1).map( m => { label: m.name; value: m.name; }).filter((value, index, self) => self.indexOf(value) === index));
      this.loading = false;
    });

  }



}

export class HelpPageModule {
  id?: number;
  friendlyUrl: string;
  helpContent: string;
  title: string;
  roles: HelpRoleModule;
}

export class HelpRoleModule {
  id?: number;
  isCertificationRole?: boolean;
  name: string;
  menus?: any[];
}
