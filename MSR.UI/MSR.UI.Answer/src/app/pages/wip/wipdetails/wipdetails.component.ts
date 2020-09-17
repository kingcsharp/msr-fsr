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
  stepTimer;
  stepSeconds: number = 0;
  stepMinutes: number = 0;
  stepHours: number = 0;

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

  selectTask(workOrderTask: WorkOrderTaskModel){
    
  }

  startTask(){
    this.workOrderTaskInProgress.statusId = 2;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 2,
      name: 'In Progress'
    } as IStatusModel);
    this.workOrderTaskInProgress.taskIsRunning = true;
    this.stepTimer = setInterval( () => {

      this.stepSeconds += 1;
      this.stepMinutes = Math.floor(this.stepSeconds / 60);
      this.stepHours= Math.floor(this.stepMinutes / 60);

    },1000);
  }

  pauseTask(){
    this.workOrderTaskInProgress.taskIsRunning = false;
    clearInterval(this.stepTimer);
  }

  completeTask(){
    this.workOrderTaskInProgress.statusId = 3;
    this.workOrderTaskInProgress.status = new StatusModel({
      id: 3,
      name: 'Completed'
    } as IStatusModel);

    this.stepSeconds = 0;
    this.stepMinutes = 0;
    this.stepHours = 0;
    clearInterval(this.stepTimer);

    let indexOfNextTask = this.workOrderTasks.findIndex(s => s.id === this.workOrderTaskInProgress.id);
    if((indexOfNextTask + 1) > this.workOrderTasks.length){
      this.workOrderTaskInProgress = undefined;
    }else{
      this.workOrderTaskInProgress = this.workOrderTasks[indexOfNextTask + 1];
      this.workOrderTaskInProgress.status = new StatusModel({
        id: 2,
        name: 'In Progress'
      } as IStatusModel);
    }
    
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
