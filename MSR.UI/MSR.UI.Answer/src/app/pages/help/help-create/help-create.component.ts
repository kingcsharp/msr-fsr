import { Component, OnInit } from '@angular/core';
import { HelpService, CreateHelpPageRequest, RoleService, Role } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../../utils/responseHandler';
import { Location } from '@angular/common';

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
  public Editor = ClassicEditor;

  constructor(private helpService:HelpService, private roleService:RoleService, private location: Location) { }

  ngOnInit(): void {

    this.roleService.roleGet(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.returnedObject);
    });

  }

  saveNewHelpPage(){
    this.createHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);
    
    /* TODO fix when you merge with Alec's Workflow branch to fix the bug coming up here
    this.helpService.helpPost(env.apiVersion,this.createHelpPageRequest).subscribe(responseHandler((resp) => {
      if (!resp.hasErrors) {
        console.log(resp);
      }
    }, (error) => {
      console.log(error);
    }));
    */

   this.location.go('help/help');
  }

}
