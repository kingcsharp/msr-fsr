import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
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
  allStatus: any[] = [
    {
      label: 'Requested',
      value: 1
    },
    {
      label: 'Assigned',
      value: 2
    },
    {
      label: 'Completed',
      value: 3
    },
    {
      label: 'Scheduled',
      value: 5
    }
  ];
  currentEM: EquipmentMaintenanceModel;
  locations: any[] = [];
  getLocationsFlag: boolean = false;
  users: any[] = [];
  getUsersFlag: boolean = false;

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
    this.getLocations();
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
    this.userService.userGet(null, null, null, null, null, null, null, null, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.users = [];
        response.object.map((x) => {
          this.users.push({ label: x.fullName, value: x.id });
        });
      }));
  }

  getLocations() {
    this.locationService.locationGet(null, null, null, env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      this.locations = [];
      response.object.map((x) => {
        this.locations.push({ label: x.name, value: x.id });
      });
      this.getLocationsFlag = true;
    }));
  }

  showEMModal(em: EquipmentMaintenanceModel) {
    if (em) {
      this.currentEM = em;
    } else {
      this.currentEM = new EquipmentMaintenanceModel();
      this.currentEM.troubleState = true;
    }

    this.displayEMDialog = true;
  }

  closeEMModal() {
    this.displayEMDialog = false;
    this.currentEM = null;
    jQuery('.parsleyjs').parsley().reset();
  }

  onTroubleStateToggle($event: boolean) {
    this.currentEM.troubleState = $event;
  }

  onEMFormSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      let requestData;
      this.globals.showLoader(true);
      if (this.currentEM.id === undefined) {
        requestData = new CreateEquipmentMaintenanceRequest();
        Object.assign(requestData, this.currentEM);
        this.equipmentMaintenanceService.equipmentMaintenancePost(env.apiVersion, requestData).pipe(take(1))
          .subscribe(responseHandler(response => {
            this.data.push(response.object);
            this.closeEMModal();
          }));
      } else {
        requestData = new UpdateEquipmentMaintenanceRequest();
        Object.assign(requestData, this.currentEM);
        this.equipmentMaintenanceService.equipmentMaintenancePatch(env.apiVersion, requestData).pipe(take(1))
          .subscribe(responseHandler(response => {
            const index = this.data.findIndex(x => x.id === this.currentEM.id);
            this.data.splice(index, 1);
            this.data.splice(index, 0, response.object);
            this.closeEMModal();
          }));
      }
    }
  }

}
