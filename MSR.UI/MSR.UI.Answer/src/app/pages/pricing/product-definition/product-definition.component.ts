import { Component, OnInit } from '@angular/core';
import {
  PartService, PartModel, SubPartModel, EnumApprovalTables, AuditActionResultOfPartModel, CreatePartRequest, UpdatePartRequest, FileModel,
  ProcedureService, Procedure,
  ProcedureStepTemplateService, ProcedureStepTemplate,
  LocationService, LocationModel,
  CustomerService,
  ProcedureStep
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Globals } from '../../../models/lib/globals';

@Component({
  selector: 'app-product-definition',
  templateUrl: './product-definition.component.html',
  styleUrls: ['./product-definition.component.scss'],
  providers: [CustomerService, ProcedureService]
})
export class ProductDefinitionComponent implements OnInit {
  showAllStepColumns: boolean = false;
  partsData: any[] = [];
  getPartsFlag: boolean = false;
  proceduresData: any[] = [];
  getProceduresFlag: boolean = false;
  locationsData: any[] = [];
  getLocationsFlag: boolean = false;
  customersData: any[] = [];
  getCustomersFlag: boolean = false;
  productData: ProductModel;
  isRefreshingPartsData: boolean = false;
  isRefreshingProceduresData: boolean = false;
  getProcedureStepsFlag: boolean = true;
  newStepsCounts: number = 0;

  constructor(
    public globals: Globals,
    private partsService: PartService,
    private locationService: LocationService,
    private customerService: CustomerService,
    private procedureService: ProcedureService,
  ) { }

  ngOnInit(): void {
    this.productData = new ProductModel;
    this.productData.procedureSteps = [];
    this.getLocations();
    this.getCustomers();
    this.getParts();
    this.getProcedures();
  }

  getCustomers() {
    const ctrl = this;
    if (ctrl.getCustomersFlag) {
      return ctrl.customersData;
    }
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      response.object.map((x) => {
        ctrl.customersData.push({ label: `[MSR-FSR] ${x.name} - [ID: ${x.id}]`, value: x.id });
      });
      ctrl.getCustomersFlag = true;
    }));
  }

  getLocations() {
    const ctrl = this;
    if (ctrl.getLocationsFlag) {
      return ctrl.locationsData;
    }
    this.globals.showLoader(true);
    this.locationService.locationGet(null, null, env.apiVersion).subscribe(responseHandler((response) => {
      response.object.map((x) => {
        ctrl.locationsData.push({ label: x.name, value: x.id });
      });
      ctrl.getLocationsFlag = true;
    }));
  }

  getParts(isRefresh: boolean = false) {
    const ctrl = this;

    if (isRefresh) {
      ctrl.isRefreshingPartsData = true;
      ctrl.partsData = [];
    }

    if (ctrl.getPartsFlag && !isRefresh) {
      return ctrl.partsData;
    }

    this.partsService.partGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.partsData.push({ label: `${x.name} [${x.partNumber}] [ID: ${x.id}]`, value: x.id });
        });
        ctrl.getPartsFlag = true;
        if (isRefresh) {
          ctrl.isRefreshingPartsData = false;
        }
      }));
  }

  getProcedures(isRefresh: boolean = false) {
    const ctrl = this;

    if (isRefresh) {
      ctrl.isRefreshingProceduresData = true;
      ctrl.proceduresData = [];
    }
    if (ctrl.getProceduresFlag && !isRefresh) {
      return ctrl.proceduresData;
    }
    this.procedureService.procedureGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.proceduresData.push({ label: `${x.name} [ID: ${x.id}]`, value: x.id });
        });
        ctrl.getProceduresFlag = true;
        if (isRefresh) {
          ctrl.isRefreshingProceduresData = false;
        }
      }));
  }

  getProcedureSteps(id: number) {
    const ctrl = this;

    ctrl.getProcedureStepsFlag = false;
    ctrl.productData.procedureSteps = [];
    this.procedureService.stepGet(id, null, env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      ctrl.productData.procedureSteps = response.object;
      ctrl.getProcedureStepsFlag = true;
      ctrl.newStepsCounts = 0;
    }));
  }

  onSelectProcedure($event) {
    this.getProcedureSteps($event.target.value)
  }

  onAddProcedureStep() {
    this.productData.procedureSteps.push(new ProcedureStep);
    this.newStepsCounts++;
  }

  onRemoveProcedureStep() {
    if (this.newStepsCounts > 0) {
      this.productData.procedureSteps.pop();
      this.newStepsCounts--;
    }
  }

  onToggle($event: boolean) {
    this.showAllStepColumns = $event;
  }


  onSubmit() {
    // TODO: Submit Functionality
    console.log('_______', this.productData)
  }

}

class ProductModel {
  locationId: number;
  customerId: number;
  partId: number;
  procedureId: number;
  procedureSteps: ProcedureStep[];
}


