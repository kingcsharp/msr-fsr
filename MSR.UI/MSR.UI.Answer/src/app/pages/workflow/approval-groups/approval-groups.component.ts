import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-approval-groups',
  templateUrl: './approval-groups.component.html',
  styleUrls: ['./approval-groups.component.scss']
})
export class ApprovalGroupsComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {
  }

}
