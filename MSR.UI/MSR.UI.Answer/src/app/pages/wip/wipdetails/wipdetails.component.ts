import { Component, OnInit, ViewEncapsulation, ElementRef} from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Customer, IStatusModel, PartModel, Procedure, ProcedureStepMonitor, ProductModel, PurchaseModel, StatusModel, 
  WorkOrderModel, WorkOrderPartModel, WorkOrderService, WorkOrderTaskModel, ProcedureStepMonitorService, WorkOrderTaskMonitorModel } from '../../../services/api.client.generated';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Product } from '../../ecommerce/products.service';
import {forkJoin} from "rxjs";
import {tap} from "rxjs/operators";
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'app-wipdetails',
  templateUrl: './wipdetails.component.html',
  styleUrls: ['./wipdetails.component.scss'],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true,
  providers: [WorkOrderService, ProcedureStepMonitorService]
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
  workOrderTaskToView: WorkOrderTaskModel;
  

  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService, private procedureStepMonitorService: ProcedureStepMonitorService) { }

  ngOnInit(): void {

    this.route.params.subscribe(params => {

      let workOrderId = params['id'] == null ? 0 : Number(params['id']);
      this.workOrdersService.workOrder(workOrderId,null,null,null,env.apiVersion).subscribe(responseHandler(response => {
        this.workOrderModel = response.object[0];
        this.workOrderParts = this.workOrderModel.workOrderParts;
        this.workOrderTasks = this.workOrderModel.workOrderTasks;
        this.parentPart = this.workOrderModel.workOrderParts[0];
        this.procedure = this.workOrderModel.product.procedure;
        this.customer = this.workOrderModel.product.customer;
        this.product = this.workOrderModel.product;
        this.purchase = this.workOrderModel.purchase;

        this.cleanData();
        this.workOrderTaskInProgress = this.workOrderTasks[0];
        this.workOrderTaskToView = this.workOrderTasks[0];
        

      }));

    });

  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel){
    this.workOrderTaskToView = workOrderTask;
  }

  saveMonitorsAndCloseTask(){

  }

  cleanData(){

    this.workOrderTasks.map(s => {

      s.status = new StatusModel({
        id: 11,
        name: 'Waiting to Start' 
      });

    })

    this.workOrderTasks.map(s => {
      s.taskIsRunning = false;
    });

  }


}
