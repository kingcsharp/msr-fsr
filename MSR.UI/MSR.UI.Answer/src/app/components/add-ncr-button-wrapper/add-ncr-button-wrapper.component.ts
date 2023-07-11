import { Component, EventEmitter, Input, OnInit, Output } from "@angular/core";
import {
  ProcedureService,
  WorkOrderTaskService,
  LocationService,
  UserService,
  Procedure,
  WorkOrderPartService,
  WorkOrderModel,
  WorkOrderService,
  ProcedureStepMonitorService,
  WorkOrderTaskModel,
  AddNCRWorkOrderTaskRequest,
  IAddNCRWorkOrderTaskRequest,
} from "../../services/api.client.generated";
import { environment as env } from "../../../environments/environment";
import { responseHandler } from "../../utils/responseHandler";
import { Globals } from "../../models/lib/globals";
import { take } from "rxjs/operators";

@Component({
  selector: "addncrbutton-wrapper",
  templateUrl: "./add-ncr-button-wrapper.component.html",
  styleUrls: ["./add-ncr-button-wrapper.component.scss"],
  providers: [
    WorkOrderService,
    ProcedureStepMonitorService,
    WorkOrderPartService,
    ProcedureService,
    WorkOrderTaskService,
    LocationService,
    UserService,
  ],
})
export class AddNcrButtonWrapperComponent implements OnInit {
  @Input() workOrderModel: WorkOrderModel;
  @Input() workOrderTaskInProgress: WorkOrderTaskModel;
  @Output() workOrderModelChange = new EventEmitter<any>();
  @Output() workOrderTaskInProgressChange = new EventEmitter<any>();
  @Output() addNcrTasks = new EventEmitter<any>();
  showAddNcrDialog: boolean = false;
  ncrProceduresAvailable: Array<Procedure>;
  workOrderTasks: Array<any>;

  constructor(
    public globals: Globals,
    private procedureService: ProcedureService,
    private workOrderService: WorkOrderService
  ) {}

  ngOnInit(): void {}

  addNcr() {
    this.globals.showLoader(true);
    this.procedureService
      .procedureGet(
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        null,
        env.apiVersion
      )
      .subscribe(
        responseHandler((response) => {
          this.ncrProceduresAvailable = response.object.filter(
            (s) => s.procedureType.name === "Conformance Action (NCR)"
          );
          this.showAddNcrDialog = !this.showAddNcrDialog;
        })
      );
  }

  insertNCR(ncrProcedure: Procedure) {
    this.showAddNcrDialog = !this.showAddNcrDialog;
    this.globals.showLoader(true);

    let addNCRWorkOrderTaskRequest = new AddNCRWorkOrderTaskRequest({
      workOrderId: this.workOrderModel.id,
      userId: this.globals.getCurrentUser().id,
      procedureId: ncrProcedure.id,
    } as IAddNCRWorkOrderTaskRequest);
    this.workOrderService
      .addNCRWorkOrderTask(env.apiVersion, addNCRWorkOrderTaskRequest)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.workOrderTasks = response.object;
          this.workOrderModel.hasNCR = true;
          this.addNcrTasks.emit(this.workOrderTasks);
        })
      );
  }
}
