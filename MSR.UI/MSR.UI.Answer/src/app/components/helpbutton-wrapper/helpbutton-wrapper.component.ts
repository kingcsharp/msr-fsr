import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Globals } from '../../models/lib/globals';
import { HelpService, HelpPage } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';

@Component({
  selector: 'helpbutton-wrapper',
  templateUrl: './helpbutton-wrapper.component.html',
  styleUrls: ['./helpbutton-wrapper.component.scss']
})
export class HelpbuttonWrapperComponent implements OnInit {
  canViewHelpPage = true;
  helpMenuUrl:string;
  display: boolean = false;
  helpContent: string;

  constructor(private router: Router, private globals: Globals, private helpService: HelpService) { }

  ngOnInit(): void {
    console.log(this.router.url.replace('/app',''));
    this.helpMenuUrl = this.router.url.replace('/app','');

    this.helpService.helpGet(null, this.helpMenuUrl, env.apiVersion).subscribe(responseHandler(response => {

      this.helpContent = response.object[0].content;

    }));

  }

}
