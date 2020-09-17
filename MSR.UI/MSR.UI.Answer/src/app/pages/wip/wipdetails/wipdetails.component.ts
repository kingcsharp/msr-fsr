import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Customer, IStatusModel, PartModel, Procedure, ProductModel, PurchaseModel, StatusModel, WorkOrderModel, WorkOrderPartModel, WorkOrderService, WorkOrderTaskModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Product } from '../../ecommerce/products.service';

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss'],
  providers: [WorkOrderService]
})
export class WipdetailsComponent implements OnInit {

  workOrderModel: WorkOrderModel = new WorkOrderModel();
  parentPart: WorkOrderPartModel = new WorkOrderPartModel();
  procedure: Procedure = new Procedure();
  customer: Customer = new Customer();
  product: Product = new Product();
  purchase: PurchaseModel = new PurchaseModel();
  workOrderParts: Array<WorkOrderPartModel> = new Array<WorkOrderPartModel>()
  workOrderTasks: Array<WorkOrderTaskModel>;
  workOrderTaskInProgress: WorkOrderTaskModel;

  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrdersService.workOrder(552,null,null,null,env.apiVersion).subscribe(responseHandler(response => {
        this.workOrderModel = response.object[0];
        this.dataCleanup();
        this.workOrderParts = this.workOrderModel.workOrderParts;
        this.workOrderTasks = this.workOrderModel.workOrderTasks;
        this.parentPart = this.workOrderModel.workOrderParts[0];
        this.procedure = this.workOrderModel.product.procedure;
        this.customer = this.workOrderModel.product.customer;
        this.product = this.workOrderModel.product;
        this.purchase = this.workOrderModel.purchase;
        this.workOrderTaskInProgress = this.workOrderTasks[0];
      }));

    });

  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel){
    
  }

  dataCleanup(){
    this.workOrderModel.workOrderTasks.map((elem) => {
      elem.statusId = 11;
      elem.status = new StatusModel({
        id: 11,
        name: 'Waiting to Start'
      } as IStatusModel);

    });

    this.workOrderModel.workOrderTasks.map((elem) => {
      elem.taskIsRunning = false;

    });
  }

}
