import { Component, OnInit } from '@angular/core';
import { Procedure } from '../../../services/mocks/models/procedure';
import { ProcedureStep } from '../../../services/mocks/models/procedureStep';
import { MockServices } from '../../../services/mocks/services/mockservices';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-procedure-view',
  templateUrl: './procedure-view.component.html',
  styleUrls: ['./procedure-view.component.scss'],
  providers: [MockServices]
})
export class ProcedureViewComponent implements OnInit {

  procedure: Procedure = new Procedure();
  procedureSteps: Array<ProcedureStep>;

  constructor(private mockServices: MockServices, private route: ActivatedRoute) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      this.procedure.id = params['id'] == null ? 0 : Number(params['id']);
      
      if(this.procedure.id !== 0){

        let procedures = this.mockServices.procedureGet(this.procedure.id);

        this.procedure = procedures[0];

        this.procedureSteps = this.mockServices.procedureStepGet(null).slice(1,4);
      }

    });

    
  }

}
