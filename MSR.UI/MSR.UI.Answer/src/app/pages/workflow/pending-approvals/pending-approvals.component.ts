import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-pending-approvals',
  templateUrl: './pending-approvals.component.html',
  styleUrls: ['./pending-approvals.component.scss']
})
export class PendingApprovalsComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {
  }

}
