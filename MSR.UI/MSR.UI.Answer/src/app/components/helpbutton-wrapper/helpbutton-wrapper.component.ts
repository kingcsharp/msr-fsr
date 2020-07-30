import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Globals } from '../../models/lib/globals';

@Component({
  selector: 'helpbutton-wrapper',
  templateUrl: './helpbutton-wrapper.component.html',
  styleUrls: ['./helpbutton-wrapper.component.scss']
})
export class HelpbuttonWrapperComponent implements OnInit {
  canViewHelpPage = true;
  helpMenuUrl:string;
  constructor(private router: Router, private globals: Globals) { }

  ngOnInit(): void {
    console.log(this.router.url.replace('/app',''));
    this.helpMenuUrl = this.router.url.replace('/app','');

  }

}
