import { Component, Input, OnInit } from '@angular/core';
import {
  WorkOrderModel,
  WorkOrderPartModel,
  WorkOrderPartService,
} from '../../services/api.client.generated';
import * as _ from 'lodash';

@Component({
  selector: 'ncr-label',
  templateUrl: './ncr-label.component.html',
  styleUrls: ['./ncr-label.component.scss'],
  providers: [WorkOrderPartService]
})
export class NcrLabelComponent implements OnInit {


  todayDate : Date = new Date();
  @Input() WorkOrder: WorkOrderModel;
  workOrderParts: Array<WorkOrderPartModel>;
  workOrderTasks: Array<any>;

  constructor() { }

  ngOnInit(): void {
    this.workOrderParts = this.WorkOrder.workOrderParts || [];
    this.workOrderTasks = [];
    _.map(this.workOrderParts, workOrderPart => {
      const associatedFirstNCRTask =
        _.orderBy (
          _.filter(this.WorkOrder.workOrderTasks || [], task =>
            !!task.ncNumber &&
            _.some(task.mappedWorkOrderParts, part => part.id === workOrderPart.id)
          ),
          ['ncNumber'],
          ['desc']
        )[0]
      this.workOrderTasks.push(
        associatedFirstNCRTask
          ? _.filter(this.WorkOrder.workOrderTasks, task => task.ncNumber === associatedFirstNCRTask.ncNumber)
          : null
      )
    });
  }

}
