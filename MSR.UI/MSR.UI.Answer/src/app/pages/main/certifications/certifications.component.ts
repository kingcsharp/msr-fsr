import { Component, OnInit, ElementRef } from "@angular/core";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { ColumnsSaved } from "../../../models/lib/ColumnsSaved";
import { CommonGrid } from "../../../models/lib/CommonGrid";
import { EnumPrivilege } from "../../../models/enums/privileges";
import { Globals } from "../../../models/lib/globals";
import { UserService } from "../../../services/api.client.generated";
import { callFunctionWithFilters } from "../../../models/lib/Utils";
import { take } from "rxjs/operators";
import { LazyLoadEvent } from "primeng/api";

@Component({
  selector: "app-certifications",
  templateUrl: "./certifications.component.html",
  styleUrls: ["./certifications.component.scss"],
})
export class CertificationsComponent implements OnInit {
  data: any;
  privileges = EnumPrivilege;
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  canAddLocation: boolean = false;
  canEditLocation: boolean = false;
  canDeleteLocation: boolean = false;
  statusOptions: any[];
  gridVersion: string;
  totalRecords: number = 0;

  constructor(
    private userService: UserService,
    private commonGrid: CommonGrid,
    private elementReference: ElementRef,
    public globals: Globals
  ) {}

  ngOnInit(): void {
    this.gridStorageId =
      "userGrid" + this.elementReference.nativeElement.tagName.toLowerCase();
    this.gridVersion = "1.0.0";
    this.gridSettings = [
      new ColumnsSaved({ id: "userId", label: "User Id", visible: false }),
      new ColumnsSaved({
        id: "employeeName",
        label: "Employee Name",
        visible: true,
      }),
      new ColumnsSaved({
        id: "certificationName",
        label: "Certification",
        visible: true,
      }),
      new ColumnsSaved({
        id: "certificationFromDate",
        label: "From Date",
        visible: true,
      }),
      new ColumnsSaved({
        id: "certificationToDate",
        label: "To Date",
        visible: true,
      }),
      new ColumnsSaved({ id: "status", label: "Status", visible: true }),
    ];

    this.canAddLocation = this.hasPrivilege(this.privileges.CanCreate);
    this.canDeleteLocation = this.hasPrivilege(this.privileges.CanActivate);
    this.canEditLocation = this.hasPrivilege(this.privileges.CanEdit);
    this.statusOptions = [
      { label: "Active", value: true },
      { label: "InActive", value: false },
    ];
  }

  getTrainingCertification(event: LazyLoadEvent) {
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(
        this.userService,
        this.userService.trainingCertification,
        event,
        this.globals.functionDic
      )
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.totalRecords = response.totalNumberOfRecords;
            this.data = response.object;
          })
        );
    }, 10);
  }

  hasPrivilege(privilegeName) {
    return this.globals.hasPrivilege("HelpPages", privilegeName);
  }
}
