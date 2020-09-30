import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel } from '../../services/api.client.generated';

@Component({
  selector: 'part-label-roll',
  templateUrl: './part-label-roll.component.html',
  styleUrls: ['./part-label-roll.component.scss']
})
export class PartLabelRollComponent implements OnInit {

  @Input() WorkOrder: WorkOrderModel;
  workOrderParentPart: WorkOrderPartModel;
  
  constructor() { }

  ngOnInit(): void {
    this.workOrderParentPart = this.WorkOrder.workOrderParts[0];
  }

}
