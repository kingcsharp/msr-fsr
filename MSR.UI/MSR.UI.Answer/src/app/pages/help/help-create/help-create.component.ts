import { Component, OnInit } from '@angular/core';
import { HelpService, CreateHelpPageRequest, RoleService, Role } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';

@Component({
  selector: 'app-help-create',
  templateUrl: './help-create.component.html',
  styleUrls: ['./help-create.component.scss'],
  providers:[HelpService, RoleService]
})
export class HelpCreateComponent implements OnInit {

  createHelpPageRequest: CreateHelpPageRequest = new CreateHelpPageRequest();
  availableRoles: Role[] = new Array<Role>();
  selectedRoles: Role[] = new Array<Role>();
  constructor(private helpService:HelpService, private roleService:RoleService) { }

  ngOnInit(): void {

    this.roleService.roleGet(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.returnedObject);
    });

  }

  saveNewHelpPage(){
    this.createHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);
  }

}
