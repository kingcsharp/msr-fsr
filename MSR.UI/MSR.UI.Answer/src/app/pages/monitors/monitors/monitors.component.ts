import { Component, OnInit,  ElementRef  } from '@angular/core';
import { Globals } from '../../../models/lib/globals';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { MockServices } from '../../../services/mocks/services/mockservices';

@Component({
  selector: 'app-monitors',
  templateUrl: './monitors.component.html',
  styleUrls: ['./monitors.component.scss'],
  providers: [MockServices]
})
export class MonitorsComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  approvalTables = EnumApprovalTables;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = true;
  gridStorageId: string;

  constructor(private mockService: MockServices, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {

    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();
    
    this.gridSettings = [
      new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
      new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
      new ColumnsSaved({ id: 'monitorType', label: 'Monitor Type', visible: true }),
      new ColumnsSaved({ id: 'result', label: 'Result', visible: true }),
      new ColumnsSaved({ id: 'passing', label: 'Passing', visible: true }),
      new ColumnsSaved({ id: 'workerName', label: 'Worker Name', visible: true }),
      new ColumnsSaved({ id: 'taskCompleted', label: 'Task Completed', visible: true }),
      new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true })
      ];

      this.getMonitors()
  }

  getMonitors(){

    this.data = this.mockService.monitorsGet(null);
    this.loading = false;
  }

}
