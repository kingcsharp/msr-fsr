import { Component, OnInit, ɵCompiler_compileModuleSync__POST_R3__ } from '@angular/core';
import { HelpService, CreateHelpPageRequest, RoleService, Role } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import * as ClassicEditor from '@ckeditor/ckeditor5-build-classic';
import { responseHandler } from '../../../utils/responseHandler';
import { Location } from '@angular/common';
import { ActivatedRoute } from '@angular/router';
import { HelpPageModule } from '../help/help.component';
import { take } from 'rxjs/operators';
import { THIS_EXPR } from '@angular/compiler/src/output/output_ast';

@Component({
  selector: 'app-help-create',
  templateUrl: './help-create.component.html',
  styleUrls: ['./help-create.component.scss'],
  providers: [HelpService, RoleService]
})
export class HelpCreateComponent implements OnInit {

  availableRoles: Role[] = new Array<Role>();
  selectedRoles: Role[] = new Array<Role>();
  public Editor = ClassicEditor;
  helpPageToEditId: number = 0;
  helpPageToEdit: HelpPageModule;

  constructor(private helpService: HelpService, private roleService: RoleService, private location: Location, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {
      this.helpPageToEditId = params['id'] == null ? 0 : params['id'];

      if (this.helpPageToEditId !== 0) {

          this.helpService.helpGet(this.helpPageToEditId,env.apiVersion).subscribe(responseHandler((response) => {
            
            this.helpPageToEdit = response.returnedObject[0] as HelpPageModule;
            this.helpPageToEdit.roles.forEach(role => {
              this.selectedRoles.push(role);

            });
          }));

      }else{

        this.helpPageToEdit = new HelpPageModule();

      }

    });

    this.roleService.roleGet(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.returnedObject);
    });

  }

  saveNewHelpPage() {

    
    //this.createHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);

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
