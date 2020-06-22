import { Component, OnInit } from '@angular/core';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-approval-workflow',
  templateUrl: './approval-workflow.component.html',
  styleUrls: ['./approval-workflow.component.scss']
})
export class ApprovalWorkflowComponent implements OnInit {

  constructor(public globals: Globals) { }

  ngOnInit(): void {
    
  }

}
