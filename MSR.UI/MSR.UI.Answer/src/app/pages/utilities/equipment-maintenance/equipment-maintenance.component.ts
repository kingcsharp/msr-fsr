import { Component, OnInit, ViewEncapsulation, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { EnumPrivilege } from '../../../models/enums/privileges';
import {
  EquipmentMaintenanceService,
  EquipmentMaintenanceModel,
  EnumMenuItem,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Observable } from 'rxjs';
import { ViewSaved } from '../../../models/lib/ViewSaved';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { AllowedActions } from '../../../models/lib/AllowedActions';

declare let jQuery: any;

@Component({
  selector: 'app-equipment-maintenance',
  templateUrl: './equipment-maintenance.component.html',
  styleUrls: ['./equipment-maintenance.component.scss'],
  providers: [EquipmentMaintenanceService],
  encapsulation: ViewEncapsulation.None,
  preserveWhitespaces: true
})
export class EquipmentMaintenanceComponent implements OnInit {
  privileges = EnumPrivilege;
  menuItems = EnumMenuItem;
  data: any;
  getEMDataFlag: boolean = false;
  displayEMDialog: boolean = true;
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
  currentEM: EquipmentMaintenanceModel;

  constructor(
    public globals: Globals,
    public cg: CommonGrid,
    private elem: ElementRef,
    private equipmentMaintenanceService: EquipmentMaintenanceService,
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

  showEMModal() {
    this.displayEMDialog = true;
  }

}
