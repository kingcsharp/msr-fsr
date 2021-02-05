import { Component, OnInit } from '@angular/core';
import { EnumStatusSteps, WorkOrderModel, WorkOrderService} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Router } from '@angular/router';
import { Globals } from '../../models/lib/globals';
import { WorkOrderItem } from '../../models/work-order-item';
import { take } from 'rxjs/operators';

@Component({
  selector: 'selectworkorderdropdown-wrapper',
  templateUrl: './select-work-order-drop-down-wrapper.component.html',
  styleUrls: ['./select-work-order-drop-down-wrapper.component.scss'],
  providers: [WorkOrderService]
})
export class SelectWorkOrderDropDownWrapperComponent implements OnInit {

  workOrdersAvailable: Array<WorkOrderItem>;
  selectedWorkOrder: string;
  disableDropDown: boolean = true;
  placeHolder: string = 'Loading Available WorkOrders...';

  constructor(private workOrderService: WorkOrderService, private router: Router, public globals: Globals) { }

  ngOnInit(): void {

    this.globals.addRequestToIgnore('v1/WorkOrder?assignedToId');
     this.workOrderService.workOrder(null, null, null, null,
      this.globals.getCurrentUser().id, true,env.apiVersion).pipe(take(1)).subscribe(responseHandler(response => {

      let workOrders = <Array<WorkOrderModel>>response.object;
      this.workOrdersAvailable = new Array<WorkOrderItem>();
      workOrders.map(workOrder => {

        let workOrderItem = new WorkOrderItem();
        workOrderItem.WorkOrderId = workOrder.id;
        workOrderItem.CustomerPurchaseNumber = workOrder.purchase?.customerPurchaseNumber === undefined ? '' :
        workOrder.purchase?.customerPurchaseNumber ;
        workOrderItem.ProcedureName = workOrder.product?.procedure?.name;
        workOrderItem.SerialNumber = workOrder.purchase?.serialNumber;

        if(workOrder.workOrderTasks.map(s => s.statusId).find(s => s === EnumStatusSteps.WaitingtoStart)){

          workOrderItem.Status = 'Requested';
          this.workOrdersAvailable.push(workOrderItem);

        } else if(workOrder.workOrderTasks.map(s => s.statusId).find(s => s === EnumStatusSteps.InProgress)){

          workOrderItem.Status = 'Accepted';
          this.workOrdersAvailable.push(workOrderItem);

        }

      });

      this.placeHolder = 'Select a WorkOrder';
      this.disableDropDown = false;
     }));

  }

  workOrderSelected($event) {
    this.selectedWorkOrder = '';
    this.router.navigate(['app/wip/details', $event.value.WorkOrderId]);
  }

}
