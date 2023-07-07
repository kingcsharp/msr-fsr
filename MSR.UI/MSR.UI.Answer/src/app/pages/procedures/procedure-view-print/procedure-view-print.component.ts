import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import {
  Procedure,
  ProcedureService,
  ProcedureStepMonitorService,
  ProcedureStepModel,
  ProcedureStepMonitor,
} from "../../../services/api.client.generated";
import { responseHandler } from "../../../utils/responseHandler";
import { environment as env } from "../../../../environments/environment";

@Component({
  selector: "app-procedure-view-print",
  templateUrl: "./procedure-view-print.component.html",
  styleUrls: ["./procedure-view-print.component.scss"],
  providers: [ProcedureService, ProcedureStepMonitorService],
})
export class ProcedureViewPrintComponent implements OnInit {
  procedure: Procedure = new Procedure();
  procedureSteps: Array<ProcedureStepModel>;
  procedureStepMonitors: Record<string, ProcedureStepMonitor[]> = {};

  constructor(
    private procedureService: ProcedureService,
    private procedureStepMonitorService: ProcedureStepMonitorService,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.route.queryParams.subscribe((params) => {
      this.procedure.id = params["id"] == null ? 0 : Number(params["id"]);

      if (this.procedure.id !== 0) {
        this.procedureService
          .procedureGet(
            this.procedure.id,
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
              this.procedure = response.object[0];

              let resolveAllLoaded;

              const allLoaded = new Promise((resolve) => {
                resolveAllLoaded = resolve;
              });

              this.procedureService
                .stepGet(this.procedure.id, null, env.apiVersion)
                .subscribe(
                  responseHandler((stepGetResponse) => {
                    this.procedureSteps = stepGetResponse.object;

                    let remainingSteps = this.procedureSteps.length;
                    const steps = this.procedureSteps.forEach((step) => {
                      this.procedureStepMonitorService
                        .procedurestep(step.id, null, env.apiVersion)
                        .subscribe(
                          responseHandler((response) => {
                            this.procedureStepMonitors[step.id] =
                              response.object.length === 0
                                ? []
                                : response.object;

                            remainingSteps--;

                            if (remainingSteps === 0) {
                              resolveAllLoaded();
                            }
                          })
                        );
                    });
                  })
                );

              allLoaded.then(() => {
                setTimeout(() => {
                  window.print();
                }, 1000);
              });
            })
          );
      }
    });
  }
}
