import { Component, OnInit } from "@angular/core";
import { Globals } from "../../../models/lib/globals";
import { take } from "rxjs/operators";
import { responseHandler } from "../../../utils/responseHandler";
import { environment as env } from "../../../../environments/environment";
import {
  WorkOrderPartService,
  CycleCountUpdateRequest,
  ICycleCountUpdateRequest,
} from "../../../services/api.client.generated";
declare let jQuery: any;

@Component({
  selector: "app-admin-cycle-count-update",
  templateUrl: "./admin-cycle-count-update.component.html",
  styleUrls: ["./admin-cycle-count-update.component.scss"],
  providers: [WorkOrderPartService],
})
export class AdminCycleCountUpdateComponent implements OnInit {
  partNumber: string;
  serialNumber: string;
  cycleCount: string;

  constructor(
    public globals: Globals,
    private workOrderPartService: WorkOrderPartService
  ) {}

  ngOnInit(): void {
    this.initFormData();
  }

  initFormData() {
    this.partNumber = "";
    this.serialNumber = "";
    this.cycleCount = "";
  }

  onSubmit() {
    jQuery(".parsleyjs").parsley().validate();
    if (jQuery(".parsleyjs").parsley().isValid()) {
      const RequestData = new CycleCountUpdateRequest({
        partNumber: this.partNumber,
        serialNumber: this.serialNumber,
        cycleCount: parseInt(this.cycleCount, 10),
      } as ICycleCountUpdateRequest);
      this.globals.showLoader(true);
      this.workOrderPartService
        .cycleCount(env.apiVersion, RequestData)
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.initFormData();
          })
        );
    }
  }
}
