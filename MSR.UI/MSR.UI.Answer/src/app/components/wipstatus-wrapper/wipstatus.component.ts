import { Component, OnInit } from '@angular/core';
import { SelectItem } from 'primeng/api';
import { Router } from '@angular/router';
import { WorkOrderService, WorkOrderStatus } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';

@Component({
  selector: 'wipstatus',
  templateUrl: './wipstatus.component.html',
  styleUrls: ['./wipstatus.component.scss'],
  providers: [WorkOrderService]
})
export class WipstatusWrapperComponent implements OnInit {

  workOrderStatuses: Array<WorkOrderStatus>;// = new Array<WorkOrderStatus>();
  displayWorkOrderStatuses: Array<WorkOrderStatus> = new Array<WorkOrderStatus>();
  locationOptions: Array<SelectItem> = new Array<SelectItem>();
  selectedLocations: Array<string>;
  showTakeOverAsUserConfirmationDialog: boolean = false;
  workOrderToTakeOver: number;
  constructor(private router: Router, private workOrderService: WorkOrderService, public globals: Globals) { }

  
  ngOnInit(): void {

    this.globals.showLoader(true);
    this.workOrderService.status(env.apiVersion).subscribe(responseHandler(response => {

      this.workOrderStatuses = response.object;
      this.displayWorkOrderStatuses = response.object;

      this.locationOptions = this.workOrderStatuses?.map(s => s.locationName).filter((v, i, a) => a.indexOf(v) === i).map( s => ({ label: s, value: s}));
    
      if(localStorage.getItem('wipstatus') === undefined || localStorage.getItem('wipstatus') === null){
        this.selectedLocations = this.locationOptions.map(s => s.value);
        localStorage.setItem('wipstatus',this.selectedLocations.toString());
      }else{
        this.selectedLocations = localStorage.getItem('wipstatus').split(',');
      }

    }));

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

}