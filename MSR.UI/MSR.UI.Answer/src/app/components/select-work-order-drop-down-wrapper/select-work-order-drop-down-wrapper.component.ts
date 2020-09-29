import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { WorkOrderService} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Router } from '@angular/router';

@Component({
  selector: 'selectworkorderdropdown-wrapper',
  templateUrl: './select-work-order-drop-down-wrapper.component.html',
  styleUrls: ['./select-work-order-drop-down-wrapper.component.scss'],
  providers: [WorkOrderService]
})
export class SelectWorkOrderDropDownWrapperComponent implements OnInit {

  workOrdersAvailable: Array<WorkOrderItem>;
  selectedWorkOrder: string;

  constructor(private workOrderService: WorkOrderService, private router: Router) { }

  ngOnInit(): void {

    this.addMockData();
    
  }

  workOrderSelected($event){
    this.selectedWorkOrder = '';
    this.router.navigate(['app/wip/details', $event.value.WorkOrderId]);
  }

  addMockData(){

    this.workOrdersAvailable = new Array<WorkOrderItem>();
    for(let index = 0; index < 10; index++){

      this.workOrdersAvailable.push({
        WorkOrderId: 552,
        ProcedureName: 'Sample Procedure' + index,
        SerialNumber: '79879879271231' + index,
        Status: ['Requested', 'Finished', 'Accepted'][Math.floor(Math.random() * 3)],
        CustomerPurchaseNumber: '87979879' + index
      });

    }


  }

}

export class WorkOrderItem{
  WorkOrderId: number;
  ProcedureName: string;
  SerialNumber: string;
  Status: string;
  CustomerPurchaseNumber: string;
}
