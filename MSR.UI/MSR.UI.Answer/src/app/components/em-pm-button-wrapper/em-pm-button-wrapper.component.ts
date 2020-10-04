import { Component, OnInit } from '@angular/core';
import {
  ProcedureService, WorkOrderTaskService, LocationService, UserService,
  WorkOrderPartService, EquipmentMaintenanceService,
  WorkOrderService, ProcedureStepMonitorService, LocationModel, EquipmentMaintenanceModel, CreateEquipmentMaintenanceRequest, ICreateEquipmentMaintenanceRequest
} from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { responseHandler } from '../../utils/responseHandler';
import { Globals } from '../../models/lib/globals';
import { EnumPrivilege } from '../../models/enums/privileges';
import { EnumEMStatus } from '../../models/enums/EMStatus';
import { EquipmentMaintainanceTaskModel } from '../../models/equipmen-maintainance-task-model';
import { SelectItem } from 'primeng/api';

@Component({
  selector: 'empmbutton-wrapper',
  templateUrl: './em-pm-button-wrapper.component.html',
  styleUrls: ['./em-pm-button-wrapper.component.scss'],
  providers: [WorkOrderService, ProcedureStepMonitorService, WorkOrderPartService, ProcedureService,
    WorkOrderTaskService, LocationService, UserService, EquipmentMaintenanceService]
})
export class EmPmButtonWrapperComponent implements OnInit {

  showEmPmDialog: boolean = false;
  equipmentMaintenanceModel: EquipmentMaintenanceModel;
  internalAddress: string;
  statusOptions: Array<SelectItem> = new Array<SelectItem>();
  maintainanceTaskOptions: Array<SelectItem> = new Array<SelectItem>();
  locationOptions: Array<SelectItem> = new Array<SelectItem>();
  userOptions: Array<SelectItem> = new Array<SelectItem>();
  troubleState: boolean = true;
  selectedUserId: number;


  constructor(public globals: Globals, private locationService: LocationService, private userService: UserService, private equipmentMaintenanceService: EquipmentMaintenanceService) { }

  ngOnInit(): void {

    this.equipmentMaintenanceModel = new EquipmentMaintenanceModel();
    this.equipmentMaintenanceModel.troubleState = true;
    this.statusOptions = [
      {
        label: 'Requested',
        value: EnumEMStatus.Requested
      },
      {
        label: 'Assigned',
        value: EnumEMStatus.Assigned
      },
      {
        label: 'Complete',
        value: EnumEMStatus.Complete
      },
      {
        label: 'Scheduled',
        value: EnumEMStatus.Scheduled
      }
    ];

    this.maintainanceTaskOptions = [
      {
        label: 'Add /Replace Media',
        value: 'Add /Replace Media'
      },
      {
        label: 'Cleaning',
        value: 'Cleaning'
      },
      {
        label: 'PM',
        value: 'PM'
      },
      {
        label: 'Repair',
        value: 'Repair'
      }
    ];

  }

  addEmPm() {

    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, null, env.apiVersion).subscribe(responseHandler(locationGetResponse => {

      this.locationOptions = locationGetResponse.object.map(s => ({ label: s.name , value: s.id}));
      this.troubleState = true;

      this.globals.showLoader(true);
      this.userService.userGet(null, null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler(response => {

        this.userOptions = response.object.map(s => ({ label: s.fullName, value: s.id}));

      }));

      this.showEmPmDialog = !this.showEmPmDialog;

    }));


  }

  submitEmPm() {

    this.showEmPmDialog = !this.showEmPmDialog;

    let createEqupmentMaintainaceRequest = new CreateEquipmentMaintenanceRequest({ 
      locationId: this.equipmentMaintenanceModel.locationId,
      statusId: this.equipmentMaintenanceModel.statusId,
      troubleState: this.equipmentMaintenanceModel.troubleState,
      assignedToId: this.selectedUserId,
      comments: this.equipmentMaintenanceModel.comments,
      frequencyField: this.equipmentMaintenanceModel.frequencyField,
      maintenanceTask: this.equipmentMaintenanceModel.maintenanceTask,
      pemLastCompletedDate: this.equipmentMaintenanceModel.pemLastCompletedDate
    } as ICreateEquipmentMaintenanceRequest);


    this.globals.showLoader(true);
    this.equipmentMaintenanceService.equipmentMaintenancePost(env.apiVersion, createEqupmentMaintainaceRequest).subscribe(() => { });
  }

  lookUp() {

    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, null, env.apiVersion).subscribe(responseHandler(response => {

      this.equipmentMaintenanceModel.locationId = response.object.find(s => s.internalAddress === this.internalAddress).id;

    }));

  }

}
