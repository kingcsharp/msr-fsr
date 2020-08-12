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
  loading: boolean = true;
  gridStorageId: string;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;

  constructor(private userService: UserService, private commonGrid: CommonGrid, private elementReference: ElementRef, public globals: Globals) { }

  ngOnInit(): void {
    this.gridStorageId = 'userGrid' + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({ id: 'employeeName', label: 'Employee Name', visible: true }),
      new ColumnsSaved({ id: 'certificationFromDate', label: 'Begin Date', visible: true }),
      new ColumnsSaved({ id: 'certificationToDate', label: 'End Date', visible: true }),
      new ColumnsSaved({ id: 'status', label: 'Status', visible: true })
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);

    this.data = new Array<TrainingCertificationView>();


    let mockDataA = new TrainingCertificationView();
    mockDataA.employeeName = "MSR-FSR";
    mockDataA.status = "Active";
    mockDataA.certificationFromDate = new Date("2020-01-01"); 
    mockDataA.certificationToDate = new Date("2020-05-5");  

    this.data.push(mockDataA)
    this.loading = false;
    // TODO: Added API when Get All is fixed
    /*
    this.userService.trainingCertification(null, env.apiVersion).subscribe(responseHandler((response) => {
      this.data = response.object;
      console.log(response);
    }));
    */
  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege('HelpPages', privilegeName);
  }

}
