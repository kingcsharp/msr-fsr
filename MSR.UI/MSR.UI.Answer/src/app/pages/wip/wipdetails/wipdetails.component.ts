import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PartModel, ProductModel, PurchaseModel, WorkOrderModel, WorkOrderPartModel } from '../../../services/api.client.generated';

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss']
})
export class WipdetailsComponent implements OnInit {

  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModelExtension = new WorkOrderPartModelExtension();

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrderModel = this.getMockData(workOrderId);

    });

  }

  getMockData(workOrderId: number): WorkOrderModel{
    
    let workOrderModel = new WorkOrderModel();
    workOrderModel.id = workOrderId;
    workOrderModel.workOrderParts = new Array<WorkOrderPartModel>();

    let workOrderPartsModel = new WorkOrderPartModelExtension();
    workOrderPartsModel.serialNumber = '123456789876';
    workOrderPartsModel.part = new WorkOrderPartModel();
    workOrderPartsModel.part.partNumber = '123123123123';
    workOrderPartsModel.cycleCount = 3;
    workOrderPartsModel.quantity = 8;
    workOrderPartsModel.part.name = 'Heated V2 Valve';
    workOrderModel.workOrderParts.push(workOrderPartsModel);
    
    this.parentPart = workOrderPartsModel;

    return workOrderModel;
  }

}

export class WorkOrderPartModelExtension extends WorkOrderPartModel{
  cycleCount:number;
  quantity: number;
}
