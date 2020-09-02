import { Component, OnInit, ElementRef } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege } from '../../../models/enums/privileges';
import { EnumApprovalTables, MonitorService, MonitorModel } from '../../../services/api.client.generated';

@Component({
  selector: 'app-monitors',
  templateUrl: './monitors.component.html',
  styleUrls: ['./monitors.component.scss'],
  providers: [MonitorService]
})
export class MonitorsComponent implements OnInit {

  data: any[];
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;

  constructor(private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals, public monitorsService: MonitorService) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
      new ColumnsSaved({ id: 'monitorType', label: 'Monitor Type', visible: true }),
      new ColumnsSaved({ id: 'result', label: 'Result', visible: true }),
      new ColumnsSaved({ id: 'passing', label: 'Passing', visible: true }),
      new ColumnsSaved({ id: 'workerName.fullName', label: 'Worker Name', visible: true }),
      new ColumnsSaved({ id: 'taskCompleted', label: 'Task Completed', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial', visible: true })
    ];

    this.getMonitors();
  }

  getMonitors() {

    this.globals.showLoader(true);

    this.monitorsService.monitor(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data = response.object;
      this.loading = false;
    }, () => {

      this.data = [];
      this.loading = false;

    }));

  }

}
