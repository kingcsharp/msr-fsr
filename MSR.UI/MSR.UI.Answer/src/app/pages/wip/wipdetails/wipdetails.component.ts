import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PartModel, ProductModel, PurchaseModel, WorkOrderModel, WorkOrderPartModel, WorkOrderService } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss'],
  providers: [WorkOrderService]
})
export class WipdetailsComponent implements OnInit {

  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModel = new WorkOrderPartModel();

  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrdersService.workOrder(552,null,null,null,env.apiVersion).subscribe(responseHandler(response => {
        this.workOrderModel = response.object[0];
      }));

    });

  }

}
