import { Component, OnInit } from '@angular/core';
import {
  PartService, PartModel, SubPartModel, EnumApprovalTables, AuditActionResultOfPartModel, CreatePartRequest, UpdatePartRequest, FileModel,
  ProcedureService, Procedure,
  ProcedureStepTemplateService, ProcedureStepTemplateModel,
  ProductService, ProductModel, CreateProductRequest, UpdateProductRequest,
  CustomerService, Customer,
  QuoteService, QuoteModel,
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
  productData: any;
  getProductDataFlag: boolean = false;
  quoteData: QuoteModel;
  getQuoteDataFlag: boolean = false;
  showQuoteViewModal: boolean = false;
  quoteJson: any;

  constructor(
    public globals: Globals,
    private partsService: PartService,
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
      this.globals.showLoader(true);

      switch (this.mode) {
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
    this.getQuoteData(this.id);
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

  isLoading() {
    switch (this.mode) {
      case this.productPageModes.Create:
        this.globals.showLoader(!(
          this.getQuoteDataFlag &&
          this.getCustomersFlag &&
          this.getPartsFlag &&
          this.getProceduresFlag &&
          this.getProcedureStepTemplatesFlag
        ));
        break;
      case this.productPageModes.Edit:
        this.globals.showLoader(!(
          this.getProductDataFlag &&
          this.getQuoteDataFlag &&
          this.getCustomersFlag &&
          this.getPartsFlag &&
          this.getProceduresFlag &&
          this.getProcedureStepsFlag &&
          this.getProcedureStepTemplatesFlag
        ));
        break;
      case this.productPageModes.View:
        this.globals.showLoader(!(
          this.getProductDataFlag &&
          this.getQuoteDataFlag &&
          this.getProcedureStepsFlag
        ));
        break;
      default:
        break;
    }
  } 

  getQuoteData(id: number) {
    this.getQuoteDataFlag = false;
    const isCreateMode = this.mode === this.productPageModes.Create;
    this.quoteService.quoteGet(id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.quoteData = response.object[0];
        this.quoteJson = JSON.parse(this.quoteData.quoteJson);
        this.getQuoteDataFlag = true;

        if (isCreateMode) {
          this.productData = new CreateProductRequest();
          // this.productData.name = this.quoteData.productName;
          this.productData.quoteId = this.quoteData.id;
          this.productData.revision = 0;
          this.productData.laborCost = 0;
          this.productData.equipmentCost = 0;
        }

        this.productData.totalLaborMins = 0;
        this.productData.totalMachineMins = 0;

        this.getProductDataFlag = true;
        this.isLoading();
      }));
  }

  getProductData() {
    this.getProductDataFlag = false;
    this.productService.productGet(this.id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.productData = response.object[0];
        this.getProductDataFlag = true;
        this.isLoading();
        this.getProcedureSteps(this.productData.procedureId);
        this.getQuoteData(this.productData.quoteId);
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
      ctrl.isLoading();
    }));
  }

  getCustomerLabel(customer: Customer): string {
    return `[MSR-FSR] ${customer.name} - [ID: ${customer.id}]`;
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
        ctrl.isLoading();
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
        this.isLoading();
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
      const step = new ProcedureStepModel({
        equipmentTime: $event.value.equipmentTime,
        laborTime: $event.value.laborTime,
        replacementCost: $event.value.replacementCost,
        usefulLife: $event.value.usefulLife,
        utilizationTime: $event.value.utilizationTime,
        printOrder: index + 1,
        stepText: $event.value.stepText,
        title: $event.value.title
      });

      const stepValues = this.calculateStepValues(step);
      this.procedureStepsData[index] = {...step, ...stepValues};
      this.getStepsValues(true);
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
          ctrl.procedureStepTemplatesData.push({ label: x.title, value: x });
        });
        ctrl.getProcedureStepTemplatesFlag = true;
        this.isLoading();
      }));
  }

 /**
  * Get ProcedureSteps by procedure Id
  */
  getProcedureSteps(id: number) {
    const ctrl = this;
    ctrl.globals.showLoader(true);

    ctrl.getProcedureStepsFlag = false;
    ctrl.procedureStepsData = [];
    this.procedureService.stepGet(id, null, env.apiVersion).pipe(take(1))
    .subscribe(responseHandler(response => {
      response.object.forEach(step => {

        // Calculate each step values
        const stepValues = ctrl.calculateStepValues(step);
        ctrl.procedureStepsData.push({...step, ...stepValues});

      });
      ctrl.getProcedureStepsFlag = true;

      // Calcuate total step values
      ctrl.getStepsValues();
      ctrl.newStepsCounts = 0;
      this.isLoading();
    }));
  }

  onSelectProcedure($event) {
    this.getProcedureSteps($event.target.value);
  }

  onAddProcedureStep() {
    this.procedureStepsData.push(new ProcedureStepModel());
    this.newStepsCounts++;
  }

  onRemoveProcedureStep() {
    if (this.newStepsCounts > 0) {
      this.procedureStepsData.pop();
      this.newStepsCounts--;
      this.getStepsValues(true);
    }
  }

  getStepsValues(isRefresh: boolean = false) {
    const values = {
      totalLaborMins: 0,
      totalMachineMins: 0,
      totalLaborCharge: 0,
      totalEquipmentCharge: 0
    };
    this.procedureStepsData.forEach(step => {
      values.totalLaborMins += step.laborTime ? step.laborTime : 0;
      values.totalMachineMins += step.equipmentTime ? step.equipmentTime : 0;
      values.totalLaborCharge += step.laboar_charge ? step.laboar_charge : 0;
      values.totalEquipmentCharge += step.equipment_charge ? step.equipment_charge : 0;
    });
    this.productData.totalLaborMins = values.totalLaborMins;
    this.productData.totalMachineMins = values.totalMachineMins;

    if (isRefresh) {
      this.productData.laborCost = values.totalLaborCharge;
      this.productData.equipmentCost = values.totalEquipmentCharge;
      this.productData.totalSalePrice = this.productData.laborCost + this.productData.equipmentCost + (this.productData.materialCost || 0);
    }
  }

  onChangeLaborTime(index: number) {
    this.procedureStepsData[index].laboar_charge = this.procedureStepsData[index].laborTime * LABOR_RATE_PER_MIN;

    this.getStepsValues(true);
  }

  onChangeEquipmentTime(index: number) {
    this.procedureStepsData[index].equipment_charge = this.procedureStepsData[index].equipmentTime * this.procedureStepsData[index].ex_per_min + this.procedureStepsData[index].equipmentTime * this.procedureStepsData[index].rm_per_min;

    this.getStepsValues(true);
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
    };
  }

  onToggle($event: boolean) {
    this.showAllStepColumns = $event;
  }

  openQuoteViewModal() {
    this.showQuoteViewModal = true;
  }

  closeQuoteViewModal() {
    this.showQuoteViewModal = false;
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
}

const RM_ANNUAL_RATE = 10;
const LABOR_RATE_PER_MIN = 2.92;
const YEAR_HOURS = 2080;
const HOURS_MINUTES = 60;

