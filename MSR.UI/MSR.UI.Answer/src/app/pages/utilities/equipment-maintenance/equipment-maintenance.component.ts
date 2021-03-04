import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumEMStatus } from '../../../models/enums/EMStatus';
import {
  EquipmentMaintenanceService,
  EquipmentMaintenanceModel,
  EnumMenuItem,
  LocationService,
  UserService,
  UpdateEquipmentMaintenanceRequest,
  CreateEquipmentMaintenanceRequest,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';

declare let jQuery: any;

@Component({
  selector: 'app-equipment-maintenance',
  templateUrl: './equipment-maintenance.component.html',
  styleUrls: ['./equipment-maintenance.component.scss'],
  providers: [
    EquipmentMaintenanceService,
    LocationService,
    UserService,
  ],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class EquipmentMaintenanceComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  data: any;
  getEMDataFlag: boolean = false;
  displayEMDialog: boolean = false;
  canEdit: boolean = false;
  showSaveView: boolean = false;
  savedViewsOptions: any;
  viewsSaved: Array<ViewSaved>;
  viewToSave: ViewSaved;
  gridVersion: string;
  defaultView: ViewSaved;
  gridStorageId: string;
  gridSettings: ColumnsSaved[];
  emPrivileges: AllowedActions;
  emStatus: any[];
  troubleStates: any[] = [
    {
      label: 'ON',
      value: true
    },
    {
      label: 'OFF',
      value: false
    }
  ];
  maintenanceTasks: any[] = [
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
  enumEMStatus = EnumEMStatus;
  allStatus: any[] = [
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
  selectedEquipmentMaintenance: EquipmentMaintenanceModel;
  locations: any[] = [];
  getLocationsFlag: boolean = false;
  users: any[] = [];
  getUsersFlag: boolean = false;
  internalAddress: string;
  displayConfirmModal: boolean  = false;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private equipmentMaintenanceService: EquipmentMaintenanceService,
    private locationService: LocationService,
    private userService: UserService,
  ) { }

  ngOnInit(): void {
    this.gridVersion = '1.0.0';
    this.gridStorageId = 'emGrid' + this.elem.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'ID', visible: true }),
      new ColumnsSaved({ id: 'location.name', label: 'Room/Equipment', visible: true }),
      new ColumnsSaved({ id: 'createdOn', label: 'Created On', visible: true }),
      new ColumnsSaved({ id: 'created.fullName', label: 'Request By', visible: true }),
      new ColumnsSaved({ id: 'assignedTo.fullName', label: 'Assigned To', visible: true }),
      new ColumnsSaved({ id: 'troubleState', label: 'Trouble state', visible: true }),
      new ColumnsSaved({ id: 'maintenanceTask', label: 'Maintenance Task', visible: true }),
      new ColumnsSaved({ id: 'comments', label: 'Comments', visible: true }),
      new ColumnsSaved({ id: 'pemLastCompletedDate', label: 'Last Completed', visible: true }),
      new ColumnsSaved({ id: 'frequencyField', label: 'Frequency', visible: true }),
      new ColumnsSaved({ id: 'status.name', label: 'Status', visible: true })
    ];
    this.emPrivileges = this.globals.getEnumPrivileges(this.menuItems.EquipmentMaintenance);
    this.data = [];
    this.globals.showLoader(true);
    this.getEMData();
    this.getUsers();
  }

  getEMData() {
    this.equipmentMaintenanceService.equipmentMaintenanceGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        this.data = response.object;
        this.emStatus = this.data.filter(
          (thing, i, arr) => arr.findIndex(t => t.statusId === thing.statusId) === i
        ).map(x => ({ label: x.status.name, value: x.statusId }));
        this.getEMDataFlag = true;
      }));
  }

  getUsers() {
    this.userService.userGet(null, null, null, null, null, null, null, null, [21], null, null, null, null, null, null, null, null,env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.users = [];
        response.object.map((x) => {
          this.users.push({ label: x.fullName, value: x.id });
        });
      }));
  }

  searchLocations() {
    if (this.getLocationsFlag === false || !this.internalAddress) {
      return;
    }
    if (this.internalAddress) {
      this.getLocations(this.internalAddress);
    }
  }

  getLocations(internalAddress: string) {
    this.globals.showLoader(true);
    this.getLocationsFlag = false;
    this.locationService.locationGet(null, null, internalAddress, null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,null,env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      this.locations = [];
      response.object.map((x) => {
        this.locations.push({ label: x.name, value: x.id });
      });
      if (this.locations.length === 1) {
        this.selectedEquipmentMaintenance.locationId = this.locations[0].value;
      }
      this.getLocationsFlag = true;
    }));
  }

  showEMModal(equipmentMaintenance: EquipmentMaintenanceModel) {
    if (equipmentMaintenance) {
      this.selectedEquipmentMaintenance = equipmentMaintenance;
      this.locations = [{
        label: equipmentMaintenance.location.name,
        value: equipmentMaintenance.location.id
      }];
      this.getLocationsFlag = true;
    } else {
      this.selectedEquipmentMaintenance = new EquipmentMaintenanceModel({
        troubleState: true,
        statusId: this.enumEMStatus.Requested,
        locationId: null,
        pemLastCompletedDate: null,
        maintenanceTask: null,
        assignedToId: null,
        frequencyField: null,
        comments: null,
      });
      this.getLocations(null);
    }

    this.displayEMDialog = true;
  }

  closeEMModal() {
    this.displayEMDialog = false;
    this.selectedEquipmentMaintenance = null;
    this.internalAddress = null;
    jQuery('.parsleyjs').parsley().reset();
  }

  onTroubleStateToggle($event: boolean) {
    this.selectedEquipmentMaintenance.troubleState = $event;
  }

  onChangeFrequencyField($event) {
    jQuery('#frequencyField').parsley().validate();
    if (jQuery('#frequencyField').parsley().isValid()) {
      let frequencyField = null;

      if ($event.target.value) {
        frequencyField = parseInt($event.target.value, 10);
      }
      this.selectedEquipmentMaintenance.frequencyField = frequencyField;
    }
  }

  onEMFormSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);

      const requestData = {
        locationId: this.selectedEquipmentMaintenance.locationId,
        troubleState: this.selectedEquipmentMaintenance.troubleState,
        statusId: this.selectedEquipmentMaintenance.troubleState ? this.enumEMStatus.Requested : this.selectedEquipmentMaintenance.statusId,
        maintenanceTask: this.selectedEquipmentMaintenance.troubleState ? null : this.selectedEquipmentMaintenance.maintenanceTask,
        pemLastCompletedDate:  this.selectedEquipmentMaintenance.troubleState ? null : this.selectedEquipmentMaintenance.pemLastCompletedDate,
        frequencyField:  this.selectedEquipmentMaintenance.troubleState ? null : this.selectedEquipmentMaintenance.frequencyField,
        assignedToId: !this.selectedEquipmentMaintenance.troubleState && this.selectedEquipmentMaintenance.statusId === this.enumEMStatus.Assigned ? this.selectedEquipmentMaintenance.assignedToId : null,
        comments: this.selectedEquipmentMaintenance.comments,
      };

      if (this.selectedEquipmentMaintenance.id === undefined) {
        this.equipmentMaintenanceService.equipmentMaintenancePost(env.apiVersion, new CreateEquipmentMaintenanceRequest(requestData)).pipe(take(1))
          .subscribe(responseHandler(response => {
            this.data.push(response.object);
            this.data = this.data.slice(0);
            this.closeEMModal();
          }));
      } else {
        this.equipmentMaintenanceService.equipmentMaintenancePatch(env.apiVersion, new UpdateEquipmentMaintenanceRequest({...requestData, id: this.selectedEquipmentMaintenance.id})).pipe(take(1))
          .subscribe(responseHandler(response => {
            const index = this.data.findIndex(x => x.id === this.selectedEquipmentMaintenance.id);
            this.data.splice(index, 1);
            this.data.splice(index, 0, response.object);
            this.data = this.data.slice(0);
            this.closeEMModal();
          }));
      }
    }
  }

  openConfirmModal(equipmentMaintenance: EquipmentMaintenanceModel) {
    if (equipmentMaintenance) {
      this.selectedEquipmentMaintenance = equipmentMaintenance;
      this.displayConfirmModal = true;
    }
  }

  closeConfirmModal() {
    this.displayConfirmModal = false;
    this.selectedEquipmentMaintenance = null;
  }

  deleteEquipmentMaintenance() {
    this.globals.showLoader(true);
    this.equipmentMaintenanceService.equipmentMaintenanceDelete(this.selectedEquipmentMaintenance.id, env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      const index = this.data.findIndex(x => x.id === this.selectedEquipmentMaintenance.id);
      this.data.splice(index, 1);
      this.data = this.data.slice(0);
      this.closeConfirmModal();
    }));
  }
}
