import { Component, OnInit } from '@angular/core';
import { WorkOrderModel, WorkOrderService} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Router } from '@angular/router';
import { Globals } from '../../models/lib/globals';
import { WorkOrderItem } from '../../models/work-order-item';

@Component({
  selector: 'selectworkorderdropdown-wrapper',
  templateUrl: './select-work-order-drop-down-wrapper.component.html',
  styleUrls: ['./select-work-order-drop-down-wrapper.component.scss'],
  providers: [WorkOrderService]
})
export class SelectWorkOrderDropDownWrapperComponent implements OnInit {

  hideCompleted: boolean = false;
  workOrdersAvailable: Array<WorkOrderItem>;
  orignalworkOrdersOptions: Array<WorkOrderItem>;
  selectedWorkOrder: string;

  constructor(private workOrderService: WorkOrderService, private router: Router, public globals: Globals) { }

  ngOnInit(): void {

     this.workOrderService.workOrder(null, null, null, null,
      this.globals.getCurrentUser().id, env.apiVersion).subscribe(responseHandler(response => {

      let workOrders = <Array<WorkOrderModel>>response.object;
      this.workOrdersAvailable = new Array<WorkOrderItem>();
      workOrders.map(workOrder => {

        let workOrderItem = new WorkOrderItem();
        workOrderItem.WorkOrderId = workOrder.id;
        workOrderItem.CustomerPurchaseNumber = workOrder.purchase?.customerPurchaseNumber === undefined ? '' :
        workOrder.purchase?.customerPurchaseNumber ;
        workOrderItem.ProcedureName = workOrder.product?.procedure?.name;
        workOrderItem.SerialNumber = workOrder.purchase?.serialNumber;

        if (workOrder.workOrderTasks.map(s => s.status.name).find(s => s === 'Waiting to Start' || s === 'Requested')) {
          workOrderItem.Status = 'Requested';
        } else if (workOrder.workOrderTasks.map(s => s.status.name).find(s => s === 'Finished' || s === 'Complete')) {
          workOrderItem.Status = 'Finished';
        } else if (workOrder.workOrderTasks.map(s => s.status.name).find(s => s === 'Accepted' || s === 'Waiting to Start')) {
          workOrderItem.Status = 'Accepted';
        } else {
          workOrderItem.Status = 'Closed';
        }

        this.workOrdersAvailable.push(workOrderItem);

      });

      this.orignalworkOrdersOptions = this.workOrdersAvailable;

     }));

  }

  workOrderSelected($event) {
    this.selectedWorkOrder = '';
    this.router.navigate(['app/wip/details', $event.value.WorkOrderId]);
  }

  updateWorkOrders() {

    this.hideCompleted = !this.hideCompleted;

    if (this.hideCompleted) {
      this.workOrdersAvailable = this.orignalworkOrdersOptions.filter(s => s.Status !== 'Finished');
    } else {
      this.workOrdersAvailable = this.orignalworkOrdersOptions;
    }

  }

}
