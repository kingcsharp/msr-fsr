import { Component, OnInit } from '@angular/core';
import { HelpService, CreateHelpPageRequest, RoleService, Role, HelpPage, UpdateHelpPageRequest } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Location } from '@angular/common';
import { ActivatedRoute, Router, Route } from '@angular/router';
import { Globals } from '../../../models/lib/globals';
import { SelectItem } from 'primeng/api';
import {LoadedRouterConfig} from '@angular/router/bundles/router.umd.js';
import {LocationsModule} from '../../locations/locations.module';
import {MainModule} from '../../main/main.module';
import {PartsModule} from '../../parts/parts.module';
import { WorkflowModule} from '../../workflow/workflow.module';

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
  helpPageToEdit: any;

  menuItems: any;
  urls: Array<SelectItem> = new Array<SelectItem>();
  friendlyUrlOptions: Array<string> = new Array<string>();
  constructor(private helpService: HelpService, private roleService: RoleService, private location: Location,
    private activatedRoute: ActivatedRoute, public globals: Globals, private router: Router) { }

  ngOnInit(): void {

    this.globals.showLoader(true);
    this.roleService.role(env.apiVersion).subscribe(response => {
      this.availableRoles = this.availableRoles.concat(response.object);

      this.loadHelpPage();

    });

  }

  loadFriendlyUrls() {

    this.globals.showLoader(true);
    this.helpService.helpGet(null, null, env.apiVersion).subscribe(responseHandler((response) => {

      let friendlyUrlsUsed = response.object.map(s => s.friendlyURL) as Array<string>;

      this.generateAllUrlPathsRegistered();
      this.friendlyUrlOptions.forEach( friendlyUrlOption => {

        if ( friendlyUrlsUsed.find(s => s === friendlyUrlOption) === undefined) {

          this.urls.push({ label: friendlyUrlOption, value: friendlyUrlOption });

        }

      });

      if(this.helpPageToEditId !== 0){
        this.urls.push({ label: this.helpPageToEdit.friendlyURL, value: this.helpPageToEdit.friendlyURL });
      }

  }));

  }

  loadHelpPage() {

    this.activatedRoute.queryParams.subscribe(params => {
      this.helpPageToEditId = params['id'] == null ? 0 : Number(params['id']);
      if (this.helpPageToEditId !== 0) {

        this.globals.showLoader(true);
        this.helpService.helpGet(this.helpPageToEditId, null, env.apiVersion).subscribe(responseHandler((response) => {

          this.helpPageToEdit = response.object[0] as HelpPage;
          this.helpPageToEdit.roles.forEach(role => {

            let selectedRole = this.availableRoles.find(s => s.id === role.id);

            this.selectedRoles.push(selectedRole);
          });
        }));

      } else {

        this.helpPageToEdit = new HelpPage();
        this.helpPageToEdit.content = '';
      }

      this.loadFriendlyUrls();
    });

  }

  generateAllUrlPathsRegistered() {

    let helpPaths = this.getHelpPaths();
    this.friendlyUrlOptions = this.friendlyUrlOptions.concat(helpPaths);
    let locationPaths = LocationsModule.routes.filter(s => s.path !== '').map(m => '/locations/' + m.path.toLowerCase());
    this.friendlyUrlOptions = this.friendlyUrlOptions.concat(locationPaths);
    let mainPaths = MainModule.routes.filter(s => s.path !== '').map(m => '/people/' + m.path.toLowerCase());
    this.friendlyUrlOptions = this.friendlyUrlOptions.concat(mainPaths);
    let partsPaths = PartsModule.routes.filter(s => s.path !== '').map(m => '/parts/' + m.path.toLowerCase());
    this.friendlyUrlOptions = this.friendlyUrlOptions.concat(partsPaths);
    let workflowPaths = PartsModule.routes.filter(s => s.path !== '').map(m => '/workflow/' + m.path.toLowerCase());
    this.friendlyUrlOptions = this.friendlyUrlOptions.concat(workflowPaths);
  }

  getHelpPaths(): Array<string> {
    let routerConfig = <LoadedRouterConfig>(<any>this.router.config.find(s => s.path === 'app'))['_loadedConfig'];
    let helpConfig = <LoadedRouterConfig>(<any>routerConfig.routes[0].children.find(s => s.path === 'help'))['_loadedConfig'];
    let helpPaths = helpConfig.routes.filter(s => s.path !== '').map(m => '/help/' + m.path);
    return helpPaths;
  }

  saveHelpPage() {
    let createHelpPageRequest = new CreateHelpPageRequest();
    createHelpPageRequest.title = this.helpPageToEdit.title;
    createHelpPageRequest.friendlyURL = this.helpPageToEdit.friendlyURL;
    createHelpPageRequest.helpContent = this.helpPageToEdit.content;

    if (this.selectedRoles.length === 0) {
      createHelpPageRequest.roleIds = new Array<number>();
    } else {
      createHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);
    }

    this.globals.showLoader(true);
    this.helpService.helpPost(env.apiVersion, createHelpPageRequest).subscribe(responseHandler((response) => {
      if (!response.hasErrors) {
        this.helpPageToEditId = response.object.id;
      }
    }, (error) => {
    }));
  }

  updateHelpPage() {
    let updateHelpPageRequest = new UpdateHelpPageRequest();
    updateHelpPageRequest.helpPageId = this.helpPageToEditId;
    updateHelpPageRequest.title = this.helpPageToEdit.title;
    updateHelpPageRequest.friendlyURL = this.helpPageToEdit.friendlyURL;
    updateHelpPageRequest.helpContent = this.helpPageToEdit.content;

    if (this.selectedRoles.length === 0) {
      updateHelpPageRequest.roleIds = new Array<number>();
    } else {
      updateHelpPageRequest.roleIds = this.selectedRoles.filter((selectedRole) => this.availableRoles.find(role => role.name === selectedRole.name)).map(s => s.id);
    }


    this.helpService.helpPatch(env.apiVersion, updateHelpPageRequest).subscribe(responseHandler((response) => {
      if (!response.hasErrors) {
        this.helpPageToEditId = response.object.id;
      }
    }, (error) => {
    }));
  }

}
