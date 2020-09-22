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

declare let jQuery: any;

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
  workOrderMonitorsToView: Array<WorkOrderTaskMonitorExtended>;
  workOrderMonitorYesOrNoOptions: Array<SelectItem>;

  constructor(private route: ActivatedRoute, private workOrdersService: WorkOrderService, private procedureStepMonitorService: ProcedureStepMonitorService) { }

  ngOnInit(): void {

    this.workOrderMonitorYesOrNoOptions = [
      { label: 'Yes', value: '1' },
      { label: 'No', value: '0' }
    ]

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
        this.getMockMonitors();
        this.workOrderTaskInProgress = this.workOrderTasks[0];
        this.workOrderTaskToView = this.workOrderTasks[0];
        

      }));

    });

  }

  selectTaskForViewing(workOrderTask: WorkOrderTaskModel){
    this.workOrderTaskToView = workOrderTask;
  }

  saveMonitors(){
    jQuery('.parsleyjs').parsley().validate();

    if (!jQuery('.parsleyjs').parsley().isValid()) {
      alert('invalid');
    }else{
      alert('valid');
    }
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

  getMockMonitors(){

    if(this.workOrderMonitorsToView === undefined){
      this.workOrderMonitorsToView = new Array<WorkOrderTaskMonitorExtended>();
    }else{
      this.workOrderMonitorsToView.length = 0;
    }

    //Equipment, Pass or Fail, Select, Text, Yes or No, Number
    let equipmentMonitor = new WorkOrderTaskMonitorExtended();
    equipmentMonitor.id = 1;
    equipmentMonitor.monitorOrderNumber = 1;
    equipmentMonitor.procedureMonitorId = 1;
    equipmentMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    equipmentMonitor.procedureStepMonitor.id = 1;
    equipmentMonitor.procedureStepMonitor.inputType = 'Manual';
    equipmentMonitor.procedureStepMonitor.monitorType = 'Equipment';
    equipmentMonitor.procedureStepMonitor.targetValue = '10';
    equipmentMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    equipmentMonitor.procedureStepMonitor.description = "This is a sample Equipment Monitor"
    this.workOrderMonitorsToView.push(equipmentMonitor);

    let passOrdFailMonitor = new WorkOrderTaskMonitorExtended();
    passOrdFailMonitor.id = 2;
    passOrdFailMonitor.monitorOrderNumber = 2;
    passOrdFailMonitor.procedureMonitorId = 2;
    passOrdFailMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    passOrdFailMonitor.procedureStepMonitor.id = 2;
    passOrdFailMonitor.procedureStepMonitor.inputType = 'Manual';
    passOrdFailMonitor.procedureStepMonitor.monitorType = 'Pass or Fail';
    passOrdFailMonitor.procedureStepMonitor.targetValue = '1';
    passOrdFailMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    passOrdFailMonitor.procedureStepMonitor.description = "This is a sample Pass or Fail Monitor"
    this.workOrderMonitorsToView.push(passOrdFailMonitor);

    let textMonitor = new WorkOrderTaskMonitorExtended();
    textMonitor.id = 3;
    textMonitor.monitorOrderNumber = 3;
    textMonitor.procedureMonitorId = 3;
    textMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    textMonitor.procedureStepMonitor.id = 3;
    textMonitor.procedureStepMonitor.inputType = 'Manual';
    textMonitor.procedureStepMonitor.monitorType = 'Text';
    textMonitor.procedureStepMonitor.targetValue = '33';
    textMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    textMonitor.procedureStepMonitor.description = "This is a sample Text Monitor"
    this.workOrderMonitorsToView.push(textMonitor);

    let yesOrNotMonitor = new WorkOrderTaskMonitorExtended();
    yesOrNotMonitor.id = 4;
    yesOrNotMonitor.monitorOrderNumber = 4;
    yesOrNotMonitor.procedureMonitorId = 4;
    yesOrNotMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    yesOrNotMonitor.procedureStepMonitor.id = 4;
    yesOrNotMonitor.procedureStepMonitor.inputType = 'Manual';
    yesOrNotMonitor.procedureStepMonitor.monitorType = 'Yes or No';
    yesOrNotMonitor.procedureStepMonitor.targetValue = '1';
    yesOrNotMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    yesOrNotMonitor.procedureStepMonitor.description = "This is a sample Yes or No Monitor"
    this.workOrderMonitorsToView.push(yesOrNotMonitor);

    let numberEqualMonitor = new WorkOrderTaskMonitorExtended();
    numberEqualMonitor.id = 5;
    numberEqualMonitor.monitorOrderNumber = 5;
    numberEqualMonitor.procedureMonitorId = 5;
    numberEqualMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberEqualMonitor.procedureStepMonitor.id = 5;
    numberEqualMonitor.procedureStepMonitor.inputType = 'Manual';
    numberEqualMonitor.procedureStepMonitor.monitorType = 'Number';
    numberEqualMonitor.procedureStepMonitor.shouldBe = 'EQUAL';
    numberEqualMonitor.procedureStepMonitor.targetValue = '10';
    numberEqualMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberEqualMonitor.procedureStepMonitor.description = "This is a sample Equal Number Monitor"
    this.workOrderMonitorsToView.push(numberEqualMonitor);

    let numberAboveMonitor = new WorkOrderTaskMonitorExtended();
    numberAboveMonitor.id = 6;
    numberAboveMonitor.monitorOrderNumber = 6;
    numberAboveMonitor.procedureMonitorId = 6;
    numberAboveMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberAboveMonitor.procedureStepMonitor.id = 6;
    numberAboveMonitor.procedureStepMonitor.inputType = 'Manual';
    numberAboveMonitor.procedureStepMonitor.monitorType = 'Number';
    numberAboveMonitor.procedureStepMonitor.shouldBe = 'ABOVE';
    numberAboveMonitor.procedureStepMonitor.targetValue = '10';
    numberAboveMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberAboveMonitor.procedureStepMonitor.description = "This is a sample Above Number Monitor"
    this.workOrderMonitorsToView.push(numberAboveMonitor);

    let numberBelowMonitor = new WorkOrderTaskMonitorExtended();
    numberBelowMonitor.id = 7;
    numberBelowMonitor.monitorOrderNumber = 7;
    numberBelowMonitor.procedureMonitorId = 7;
    numberBelowMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBelowMonitor.procedureStepMonitor.id = 7;
    numberBelowMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBelowMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBelowMonitor.procedureStepMonitor.shouldBe = 'BELOW';
    numberBelowMonitor.procedureStepMonitor.targetValue = '10';
    numberBelowMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberBelowMonitor.procedureStepMonitor.description = "This is a sample Below Number Monitor"
    this.workOrderMonitorsToView.push(numberBelowMonitor);

    let numberBetweenMonitor = new WorkOrderTaskMonitorExtended();
    numberBetweenMonitor.id = 8;
    numberBetweenMonitor.monitorOrderNumber = 8;
    numberBetweenMonitor.procedureMonitorId = 8;
    numberBetweenMonitor.procedureStepMonitor = new ProcedureStepMonitor();
    numberBetweenMonitor.procedureStepMonitor.id = 8;
    numberBetweenMonitor.procedureStepMonitor.inputType = 'Manual';
    numberBetweenMonitor.procedureStepMonitor.monitorType = 'Number';
    numberBetweenMonitor.procedureStepMonitor.shouldBe = 'BETWEEN';
    numberBetweenMonitor.procedureStepMonitor.lowTarget = 10;
    numberBetweenMonitor.procedureStepMonitor.highTarget = 20;
    numberBetweenMonitor.procedureStepMonitor.faultHandling = 'RECORD AND CONTINUE';
    numberBetweenMonitor.procedureStepMonitor.description = "This is a sample Between Number Monitor"
    this.workOrderMonitorsToView.push(numberBetweenMonitor);
    

  }
}

export class WorkOrderTaskMonitorExtended extends WorkOrderTaskMonitorModel {
  procedureStepMonitor: ProcedureStepMonitor;
  monitorOrderNumber: number;
}
