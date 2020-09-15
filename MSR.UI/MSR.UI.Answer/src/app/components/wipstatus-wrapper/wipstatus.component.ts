import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Router } from '@angular/router';

@Component({
  selector: 'wipstatus',
  templateUrl: './wipstatus.component.html',
  styleUrls: ['./wipstatus.component.scss']
})
export class WipstatusWrapperComponent implements OnInit {

  workOrderStatuses: Array<WorkOrderStatus> = new Array<WorkOrderStatus>();
  displayWorkOrderStatuses: Array<WorkOrderStatus> = new Array<WorkOrderStatus>();
  locationOptions: Array<SelectItem> = new Array<SelectItem>();
  selectedLocations: Array<string>;
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOver: number;
  constructor(private router: Router) { }

  
  ngOnInit(): void {

    this.getMockData();
    this.locationOptions = this.workOrderStatuses?.map(s => s.locationName).filter((v, i, a) => a.indexOf(v) === i).map( s => ({ label: s, value: s}));
    
    if(localStorage.getItem('wipstatus') === undefined || localStorage.getItem('wipstatus') === null){
      this.selectedLocations = this.locationOptions.map(s => s.value);
      localStorage.setItem('wipstatus',this.selectedLocations.toString());
    }else{
      this.selectedLocations = localStorage.getItem('wipstatus').split(',');
    }

  }

  openTakeOverAsUserConfirmationDialog(workOrderId: number){
    this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
    this.workOrderToTakeOver = workOrderId;
  }

  closeTakeOverAsUserConfirmationDialog(){
    this.showTakeOverAsUserConfirmationDialog = !this.showTakeOverAsUserConfirmationDialog;
  }

  takeOverAsUserConfirmationDialog(){
    this.router.navigate(['app/wip/details',this.workOrderToTakeOver]);
  }

  locationsSelectedUpdated(){
    localStorage.setItem('wipstatus',this.selectedLocations.toString());
    this.displayWorkOrderStatuses = this.workOrderStatuses.filter(s => this.selectedLocations.includes(s.locationName));
  }

  getMockData(){

    for(let index = 1; index < 14; index++){

      let workOrderStatus = new WorkOrderStatus();
      workOrderStatus.locationName = ['Chandler', 'Hillsboro', 'Kiryat Gat', 'Naas'][Math.floor(Math.random() * Math.floor(4))];
      workOrderStatus.partNumber = '1321231' + index;
      workOrderStatus.procedureName = 'Procedure' + index;
      workOrderStatus.productName = 'Product' + index;
      workOrderStatus.workOrderSummary = new WorkOrderSummary();
      workOrderStatus.workOrderSummary.purchaseOrderLineNumber = '131231231' + index;
      workOrderStatus.workOrderSummary.workOrderAssignedTo = 'Robert Lara';
      workOrderStatus.workOrderSummary.workOrderHasNcr = Math.random() >= 0.5;
      workOrderStatus.workOrderSummary.workOrderId = index;
      workOrderStatus.workOrderSummary.workOrderItemNumber = '123123132' + index;
      workOrderStatus.workOrderSummary.workOrderPartSerialNumber = '2998723982' + index;
      workOrderStatus.workOrderSummary.workOrderScheduledEndDate = new Date();
      workOrderStatus.workOrderSummary.workOrderStatus = ['Waiting to Start', 'In Progress'][Math.floor(Math.random() * Math.floor(2))]
      this.displayWorkOrderStatuses.push(workOrderStatus);
      this.workOrderStatuses.push(workOrderStatus);

    }

  }

}

export class WorkOrderStatus{
  productName: string;
  partNumber: string;
  procedureName: string;
  locationName: string;
  workOrderSummary: WorkOrderSummary;
}

export class WorkOrderSummary{
  workOrderId: number;
  workOrderItemNumber: string;
  purchaseOrderLineNumber: string;
  workOrderPartSerialNumber: string;
  workOrderStatus: string;
  workOrderAssignedTo: string;
  workOrderHasNcr: boolean;
  workOrderScheduledEndDate: Date;
}
