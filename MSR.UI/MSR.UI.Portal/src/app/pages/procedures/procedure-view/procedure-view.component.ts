import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Procedure, ProcedureService, ProcedureStepModel } from '../../../services/api.client.generated';
import { responseHandler } from '../../../utils/responseHandler';
import { environment as env } from '../../../../environments/environment';

@Component({
  selector: 'app-procedure-view',
  templateUrl: './procedure-view.component.html',
  styleUrls: ['./procedure-view.component.scss'],
  providers: [ProcedureService]
})
export class ProcedureViewComponent implements OnInit {

  procedure: Procedure = new Procedure();
  procedureSteps: Array<ProcedureStepModel>;

  constructor(private procedureService: ProcedureService, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      this.procedure.id = params['id'] == null ? 0 : Number(params['id']);

      if (this.procedure.id !== 0) {

        this.procedureService.procedureGet(this.procedure.id, env.apiVersion).subscribe(responseHandler((response) => {
            this.procedure = response.object[0];

            this.procedureService.stepGet(this.procedure.id, null, env.apiVersion).subscribe(responseHandler((stepGetResponse) => {
              this.procedureSteps = stepGetResponse.object;
            }));

        }));

      }

    });

  }

}
