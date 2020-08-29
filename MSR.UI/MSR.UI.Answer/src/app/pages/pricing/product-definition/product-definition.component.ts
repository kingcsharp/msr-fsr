import { Component, OnInit } from '@angular/core';
import {
  PartService, PartModel, SubPartModel, EnumApprovalTables, AuditActionResultOfPartModel, CreatePartRequest, UpdatePartRequest, FileModel,
  ProcedureService, Procedure,
  ProcedureStepTemplateService, ProcedureStepTemplateModel,
  LocationService, LocationModel,
  ProductService, ProductModel, CreateProductRequest, UpdateProductRequest,
  CustomerService, Customer,
  QuoteService, QuotesProductsView,
  ProcedureStepModel
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { ToastrService } from 'ngx-toastr';
import { Observable } from 'rxjs';
import { Globals } from '../../../models/lib/globals';
import { ActivatedRoute } from '@angular/router';
import { EnumProductPageModes } from '../../../models/enums/ProductPageModes';
import { Router } from '@angular/router';

declare let jQuery: any;

@Component({
  selector: 'app-product-definition',
  templateUrl: './product-definition.component.html',
  styleUrls: ['./product-definition.component.scss'],
  providers: [
    CustomerService,
    ProcedureService,
    ProductService,
    ProcedureStepTemplateService,
    QuoteService,
  ]
})
export class ProductDefinitionComponent implements OnInit {
  productPageModes = EnumProductPageModes;
  mode: string = null;
  id: number = null;
  showAllStepColumns: boolean = false;

  customersData: any[] = [];
  getCustomersFlag: boolean = false;
  locationsData: any[] = [];
  getLocationsFlag: boolean = false;
  partsData: any[] = [];
  getPartsFlag: boolean = false;
  isRefreshingPartsData: boolean = false;
  proceduresData: any[] = [];
  getProceduresFlag: boolean = false;
  isRefreshingProceduresData: boolean = false;
  procedureStepsData: any[];
  getProcedureStepsFlag: boolean = false;
  newStepsCounts: number = 0;
  procedureStepTemplatesData: any[] = [];
  getProcedureStepTemplatesFlag: boolean = false;
  productData: ProductModel;
  getProductDataFlag: boolean = false;
  quoteData: QuotesProductsView;
  getQuoteDataFlag: boolean = false;

  constructor(
    public globals: Globals,
    private partsService: PartService,
    private locationService: LocationService,
    private customerService: CustomerService,
    private procedureService: ProcedureService,
    private procedureStepTemplateService: ProcedureStepTemplateService,
    private productService: ProductService,
    private quoteService: QuoteService,
    private route: ActivatedRoute,
    private router: Router,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = parseInt(params.get('id'), 10);
      this.mode = params.get('mode');
      switch(this.mode) {
        case this.productPageModes.Create:
          this.initPageCreateMode();
          break;
        case this.productPageModes.Edit:
          this.initPageEditMode();
          break;
        case this.productPageModes.View:
          this.initPageViewMode();
          break;
        default:
          break;
      }
    });
  }

  initPageCreateMode() {
    this.getQuoteData();
    this.getAllData();
  }

  initPageEditMode() {
    this.getProductData();
    this.getAllData();
  }

  initPageViewMode() {
    this.getProductData();
  }

  getAllData() {
    this.getCustomers();
    this.getParts();
    this.getProcedures();
    this.getProcedureStepTemplates();
  }

  getQuoteData() {
    this.getQuoteDataFlag = false;
    this.quoteService.quoteGet(this.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.quoteData = response.object[0];
        this.getQuoteDataFlag = true;
        this.productData = new ProductModel();
        this.productData.name = this.quoteData.productName;
        this.getProductDataFlag = true;
      }));
  }

  getProductData() {
    this.getProductDataFlag = false;
    this.productService.productGet(this.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.productData = response.object[0];
        this.getProductDataFlag = true;
        this.getProcedureStepsData(this.productData.procedureId);
      }));
  }

  getProcedureStepsData(procedureId: number) {
    this.getProcedureStepsFlag = false;
    this.procedureService.stepGet(procedureId, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.procedureStepsData = response.object;
        this.getProcedureStepsFlag = true;
      }));
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

  getCustomerLabel(customer: Customer): string {
    return `[MSR-FSR] ${customer.name} - [ID: ${customer.id}]`
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

  getLocationLabel(location: LocationModel): string {
    return location.name;
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

  getPartLabel(part: PartModel): string {
    return `${part.name} [${part.partNumber}] [ID: ${part.id}]`;
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

  getProcedureLabel(procedure: Procedure): string {
    return `${procedure.name} [ID: ${procedure.id}]`;
  }

  onSelectProcedureId($event) {
    if ($event.value) {
      this.getProcedureSteps($event.value);
    }
  }

  onSelectStepTemplate($event, index: number) {
    if ($event.value) {
      this.procedureStepsData[index].equipmentTime = $event.value.equipmentTime;
      this.procedureStepsData[index].laborTime = $event.value.laborTime;
      this.procedureStepsData[index].replacementCost = $event.value.replacementCost;
      this.procedureStepsData[index].usefulLife = $event.value.usefulLife;
      this.procedureStepsData[index].utilizationTime = $event.value.utilizationTime;
      this.procedureStepsData[index].printOrder = index + 1;
      this.procedureStepsData[index].stepText = $event.value.stepText;
      this.procedureStepsData[index].title = $event.value.title;
    }
  }

  getProcedureStepTemplates() {
    const ctrl = this;
    if (ctrl.getProcedureStepTemplatesFlag) {
      return ctrl.procedureStepTemplatesData;
    }
    this.procedureStepTemplateService.procedureStepTemplateGet(null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          ctrl.procedureStepTemplatesData.push({ label: x.name, value: x });
        });
        ctrl.getProceduresFlag = true;
      }));
  }


  getProcedureSteps(id: number) {
    const ctrl = this;

    ctrl.getProcedureStepsFlag = false;
    ctrl.procedureStepsData = [];
    this.procedureService.stepGet(id, null, env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      response.object.forEach(step => {
        ctrl.procedureStepsData.push({...step, ...ctrl.calculateStepValues(step)});
      });
      ctrl.getProcedureStepsFlag = true;
      ctrl.newStepsCounts = 0;
    }));
  }

  onSelectProcedure($event) {
    this.getProcedureSteps($event.target.value)
  }

  onAddProcedureStep() {
    this.procedureStepsData.push(new ProcedureStepModel());
    this.newStepsCounts++;
  }

  onRemoveProcedureStep() {
    if (this.newStepsCounts > 0) {
      this.procedureStepsData.pop();
      this.newStepsCounts--;
    }
  }

  getStepsValues() {
    const values = {
      totalLaborMins: 0,
      totalMachineMins: 0,
      totalLaborCharge: 0,
      totalEquipmentCharge: 0
    }
    this.procedureStepsData.forEach(step => {
      values.totalLaborMins += step.laborTime;
      values.totalMachineMins += step.equipmentTime;
      values.totalLaborCharge += step.laboar_chage ? step.laboar_chage : 0;
      values.totalEquipmentCharge += step.equipment_charge ? step.equipment_charge : 0;
    });

    this.productData.totalSalePrice = values.totalLaborCharge + values.totalEquipmentCharge + this.productData.materialCost ? this.productData.materialCost : 0;

    return values;
  }

  getEquipPerMin(procedureStep: ProcedureStepModel) {
    if (procedureStep.replacementCost && procedureStep.usefulLife && procedureStep.utilizationTime) {
      return procedureStep.replacementCost/procedureStep.usefulLife;
    } else {
      return 0.0;
    }
  }

  onToggle($event: boolean) {
    this.showAllStepColumns = $event;
  }

  openQuoteViewModal() {

  }


  onSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    const ctrl = this;
    if (jQuery('.parsleyjs').parsley().isValid()) {
      this.globals.showLoader(true);
      if (ctrl.mode === ctrl.productPageModes.Create) {
        const requestData = new CreateProductRequest();
        requestData.init(ctrl.productData);
        this.productService.productPost(env.apiVersion, requestData)
          .pipe(take(1))
          .subscribe(responseHandler((resp) => {
            this.globals.showLoader(true);
            if (!resp.hasErrors) {
              ctrl.router.navigate(['app/pricing/products']);
            }
          }));
      } else if (ctrl.mode === ctrl.productPageModes.Edit) {
        const updateData = new UpdateProductRequest();
        updateData.init(ctrl.productData);
        this.productService.productPatch(env.apiVersion, updateData)
          .pipe(take(1))
          .subscribe(responseHandler((resp) => {
            this.globals.showLoader(true);
            if (!resp.hasErrors) {
              ctrl.router.navigate(['app/pricing/products']);
            }
          }));
      }
    }
  }

  calculateStepValues(step: ProcedureStepModel) {

    const laboar_chage = step.laborTime * LABOR_RATE_PER_MIN;
     const annual_rm = step.replacementCost * RM_ANNUAL_RATE;
    const rm_per_min = annual_rm / (YEAR_HOURS * HOURS_MINUTES * step.utilizationTime);
    const ex_per_min = (step.replacementCost / step.usefulLife) /  (YEAR_HOURS * HOURS_MINUTES * step.utilizationTime);
    const equipment_charge = step.equipmentTime * ex_per_min + step.equipmentTime * rm_per_min;

    return {
      laboar_charge: laboar_chage,
      annual_rm: annual_rm,
      rm_per_min: rm_per_min,
      ex_per_min: ex_per_min,
      equipment_charge: equipment_charge
    }

  }

}

const RM_ANNUAL_RATE = 10;
const LABOR_RATE_PER_MIN = 2.92;
const YEAR_HOURS = 2080;
const HOURS_MINUTES = 60;

