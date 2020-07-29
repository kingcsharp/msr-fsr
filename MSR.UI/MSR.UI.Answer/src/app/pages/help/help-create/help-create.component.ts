import { Component, OnInit } from '@angular/core';
import { HelpService, CreateHelpPageRequest, RoleService, Role, HelpPage, UpdateHelpPageRequest } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Location } from '@angular/common';
import { ActivatedRoute, Router } from '@angular/router';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-help-create',
  templateUrl: './help-create.component.html',
  styleUrls: ['./help-create.component.scss'],
  providers: [HelpService, RoleService]
})
export class HelpCreateComponent implements OnInit {

  availableRoles: Role[] = new Array<Role>();
  selectedRoles: Role[] = new Array<Role>();
  helpPageToEditId: number = 0;
  helpPageToEdit: HelpPage;

  constructor(private helpService: HelpService, private roleService: RoleService, private location: Location, 
    private route: ActivatedRoute, public globals: Globals, private router: Router) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.helpPageToEditId = params['id'] == null ? 0 : Number(params['id']);

      if (this.helpPageToEditId !== 0) {

          this.helpService.helpGet(this.helpPageToEditId,env.apiVersion).subscribe(responseHandler((response) => {
            
            this.helpPageToEdit = response.object[0] as HelpPage;
            this.helpPageToEdit.roles.forEach(role => {
              this.selectedRoles.push(role);

            });
          }));

      }else{

        this.helpPageToEdit = new HelpPage();
        this.helpPageToEdit.content = 'Sample Content';
      }

    });

    this.roleService.role(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.object);
    });

  }

  saveHelpPage() {

    let createHelpPageRequest = new CreateHelpPageRequest();
    createHelpPageRequest.title = this.helpPageToEdit.title;
    createHelpPageRequest.friendlyURL = this.helpPageToEdit.friendlyURL;
    createHelpPageRequest.helpContent = this.helpPageToEdit.content;

    if(this.selectedRoles.length === 0){

      createHelpPageRequest.roleIds = new Array<number>();

    }else{

      createHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);

    }

    this.globals.showLoader(true);
    this.helpService.helpPost(env.apiVersion,createHelpPageRequest).subscribe(responseHandler((response) => {
      if (!response.hasErrors) {
        console.log(response);
        this.helpPageToEditId = response.object.id;
      }
    }, (error) => {
      console.log(error);
    }));
    

    
  }

  updateHelpPage() {

    let updateHelpPageRequest = new UpdateHelpPageRequest();
    updateHelpPageRequest.helpPageId = this.helpPageToEditId;
    updateHelpPageRequest.title = this.helpPageToEdit.title;
    updateHelpPageRequest.friendlyURL = this.helpPageToEdit.friendlyURL;
    updateHelpPageRequest.helpContent = this.helpPageToEdit.content;
    
    if(this.selectedRoles.length === 0){

      updateHelpPageRequest.roleIds = new Array<number>();

    }else{

      updateHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);

    }


    this.helpService.helpPatch(env.apiVersion,updateHelpPageRequest).subscribe(responseHandler((response) => {
      if (!response.hasErrors) {
        console.log(response);
        this.helpPageToEditId = response.object.id;
      }
    }, (error) => {
      console.log(error);
    }));
    

    
  }

}
