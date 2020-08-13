import { Component, OnInit, ElementRef } from '@angular/core';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ColumnsSaved } from '../../../models/lib/ColumnsSaved';
import { CommonGrid } from '../../../models/lib/CommonGrid';
import { EnumPrivilege, EnumMenuItem, EnumApprovalTables } from '../../../models/enums/privileges';
import { Globals } from '../../../models/lib/globals';
import { UserService, TrainingCertificationView } from '../../../services/api.client.generated';

@Component({
  selector: 'app-certifications',
  templateUrl: './certifications.component.html',
  styleUrls: ['./certifications.component.scss']
})
export class CertificationsComponent implements OnInit {

  data: any;
  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  loading: boolean = false;
  gridStorageId: string;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;
  statusOptions: any[];

  constructor(private userService: UserService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {
    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'employeeName', label: 'Employee Name', visible: true }),
      new ColumnsSaved({ id: 'certificationName', label: 'Certification', visible: true}),
      new ColumnsSaved({ id: 'certificationFromDate', label: 'Begin Date', visible: true }),
      new ColumnsSaved({ id: 'certificationToDate', label: 'End Date', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true })
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);

    this.globals.showLoader(true);
    this.userService.trainingCertification(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data = response.object;
      this.statusOptions = this.data.filter(
        (thing, i, arr) => arr.findIndex(t => t.status === thing.status) === i
      ).map(x => ({ label: x.status, value: x.status }));

    }));
 
  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege('HelpPages', privilegeName);
  }

}
