import { Component, OnInit } from '@angular/core';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  WorkOrderPartService,
  WorkOrderService, ProcedureStepMonitorService, LocationModel
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { EquipmentMaintainanceTaskModel } from '../../models/equipmen-maintainance-task-model';

@Component({
  selector: 'empmbutton-wrapper',
  templateUrl: './em-pm-button-wrapper.component.html',
  styleUrls: ['./em-pm-button-wrapper.component.scss'],
  providers: [WorkOrderService, ProcedureStepMonitorService, WorkOrderPartService, ProcedureService,
    WorkOrderTaskService, LocationService, UserService]
})
export class EmPmButtonWrapperComponent implements OnInit {

  showEmPmDialog: boolean = false;
  equipmentMaintainanceTask: EquipmentMaintainanceTaskModel;

  constructor(public globals: Globals, private locationService: LocationService, private userService: UserService) { }

  ngOnInit(): void {

    this.equipmentMaintainanceTask = new EquipmentMaintainanceTaskModel();

    this.equipmentMaintainanceTask.statusOptions = [
      { label: 'Requested', value: 'Requested'},
      { label: 'Assigned', value: 'Assigned'},
      { label: 'Completed', value: 'Completed'},
      { label: 'Scheduled', value: 'Scheduled'}
    ];

    this.equipmentMaintainanceTask.maintainanceTaskOptions = [
      { label: 'Add/Replace Media', value: 'Add/Replace Media'},
      { label: 'Cleaning', value: 'Cleaning'},
      { label: 'PM', value: 'PM'},
      { label: 'Repair', value: 'Repair'}
    ];

  }

  addEmPm() {

    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, null, env.apiVersion).subscribe(responseHandler(locationGetResponse => {

      this.equipmentMaintainanceTask.locationOptions = locationGetResponse.object.map(s => ({ label: s.name , value: s.id}));
      this.equipmentMaintainanceTask.troubleState = true;

      this.globals.showLoader(true);
      this.userService.userGet(null, null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler(response => {

        this.equipmentMaintainanceTask.userOptions = response.object.map(s => ({ label: s.fullName, value: s.id}));

      }));

      this.showEmPmDialog = !this.showEmPmDialog;

    }));


  }

  submitEmPm() {

    this.showEmPmDialog = !this.showEmPmDialog;

  }

  lookUp() {

    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, null, env.apiVersion).subscribe(responseHandler(response => {

      this.equipmentMaintainanceTask.selectedLocation = response.object.find(s => s.internalAddress === this.equipmentMaintainanceTask.barcode).id;

    }));

  }

}
