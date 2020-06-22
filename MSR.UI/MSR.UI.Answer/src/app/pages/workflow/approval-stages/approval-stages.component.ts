import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-approval-stages',
  templateUrl: './approval-stages.component.html',
  styleUrls: ['./approval-stages.component.scss']
})
export class ApprovalStagesComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {
  }

}
