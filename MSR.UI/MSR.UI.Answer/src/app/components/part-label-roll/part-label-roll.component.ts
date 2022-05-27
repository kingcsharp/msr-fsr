import { Component, Input, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderPartModel, WorkOrderPartService } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { take } from 'rxjs/operators';

@Component({
  selector: 'part-label-roll',
  templateUrl: './part-label-roll.component.html',
  styleUrls: ['./part-label-roll.component.scss'],
  providers: [WorkOrderPartService]
})
export class PartLabelRollComponent implements OnInit {
  todayDate: Date = new Date();

  @Input() WorkOrder: WorkOrderModel;
  workOrderParts: Array<WorkOrderPartModel>;

  constructor(private workOrderPartService: WorkOrderPartService) { }

  ngOnInit(): void {

    this.workOrderPartService.workOrderPartGet(null, this.WorkOrder.id, env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {
      this.workOrderParts = response.object;
    }));

  }

}
