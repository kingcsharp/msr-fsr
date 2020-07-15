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
  roleSuggestions: string[] = new Array<string>();
  selectedRoles: string[] = new Array<string>();
  roles: Role[] = new Array<Role>();
  constructor(private helpService:HelpService, private roleService:RoleService) { }

  ngOnInit(): void {

    this.roleService.roleGet(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.returnedObject);
    });

  }

  search(event) {
    this.roleSuggestions.length = 0;
    let recommendedNames = this.availableRoles.filter(s => s.name.includes(event.query)).map(s => s.name);
    this.roleSuggestions = this.roleSuggestions.concat(recommendedNames);
  }

  saveNewHelpPage(){
    this.createHelpPageRequest.roleIds = this.roles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);
    console.log(this.createHelpPageRequest.roleIds);
  }

}
