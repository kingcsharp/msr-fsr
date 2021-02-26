import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {
  PartService,
  PartModel,
  ProcedureService, Procedure,
  ProcedureStepTemplateService,
  ProductService, CreateProductRequest, UpdateProductRequest,
  CustomerService, Customer,
  QuoteService, QuoteModel,
  ProcedureStepModel,
  AdminCostSettingsService, AdminCostSettingsModel,
  ProductStepModel,
  ProductStep,
} from '../../../services/api.client.generated';
import { take } from 'rxjs/operators';
import { environment as env } from '../../../../environments/environment';
import { responseHandler } from '../../../utils/responseHandler';
import { Globals } from '../../../models/lib/globals';
import { ActivatedRoute } from '@angular/router';
import { EnumProductPageModes } from '../../../models/enums/ProductPageModes';
import { Router } from '@angular/router';
import { CSRJsonModel } from '../../../models/csr-json-model';

declare let jQuery: any;

const RM_ANNUAL_RATE = 10;
const LABOR_RATE_PER_MIN = 2.92;
const YEAR_HOURS = 2080;
const HOURS_MINUTES = 60;

@Component({
  selector: 'app-product-definition',
  templateUrl: './product-definition.component.html',
  styleUrls: ['./product-definition.component.scss'],
  encapsulation: ViewEncapsulation.None,
  providers: [
    CustomerService,
    ProcedureService,
    ProductService,
    ProcedureStepTemplateService,
    QuoteService,
    AdminCostSettingsService,
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
  customerRequirementJson: CSRJsonModel;
  showRequirementViewModal: boolean = false;
  adminCostSettings: AdminCostSettingsModel;
  getAdminCostSettingsFlag: boolean = false;
  productSteps: any[] = [];
  isEditableHiddenColumns: boolean = false;

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
    private adminCostSettingsService: AdminCostSettingsService,
  ) { }

  ngOnInit(): void {
    this.route.paramMap.subscribe(params => {
      this.id = parseInt(params.get('id'), 10);
      this.mode = params.get('mode');
      this.isEditableHiddenColumns = this.globals.hasRole('CFO');
      this.globals.showLoader(true);
      this.getAdminCostSettings();
    });
  }
  getAdminCostSettings() {
    this.adminCostSettingsService.adminCostSettingsGet(env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.adminCostSettings = response.object;
        this.getAdminCostSettingsFlag = true;
        this.initPage();
      }));
  }

  initPage() {
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

  getQuoteData(id: number) {
    this.getQuoteDataFlag = false;
    const isCreateMode = this.mode === this.productPageModes.Create;
    this.quoteService.quoteGet(id, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.quoteData = response.object[0];
        if (this.quoteData.quoteJson) {
          this.quoteJson = JSON.parse(this.quoteData.quoteJson);
        }
        if (this.quoteData.customerRequirementJson) {
          this.customerRequirementJson = JSON.parse(this.quoteData.customerRequirementJson);
          if (!this.quoteData.quoteJson && !isCreateMode) {
            this.quoteJson = {
              customerId: this.quoteData.customerId,
              contact: this.customerRequirementJson.CommercialName,
              title: this.customerRequirementJson.CommercialTitle,
              phone: this.customerRequirementJson.CommercialPhone,
              email: this.customerRequirementJson.CommercialEmail,
              representative: this.quoteData.submittedBy.fullName,
              representativeTitle: this.quoteData.submittedBy.title,
              representativeAddress: this.customerRequirementJson.StreetAddress,
            };

            this.quoteJson.quoteItems = [{
              qty: 1,
              unit: 'Unit',
              description: this.productData.procedure?.name,
              price: this.productData.totalSalePrice,
              extension: this.productData.totalSalePrice,
            }];
          }
        }

        this.getQuoteDataFlag = true;

        if (isCreateMode) {
          this.productData = new CreateProductRequest();
          if (this.quoteData.customerRequirementJson) {
            this.productData.name =  this.customerRequirementJson.RequirementName;
          }
          this.productData.quoteId = this.quoteData.id;
          this.productData.customerId = this.quoteData.customerId;
          this.productData.partId = null;
          this.productData.procedureId = null;
          this.productData.revision = 0;
          this.productData.laborCost = 0;
          this.productData.equipmentCost = 0;
          this.productData.totalLaborMins = 0;
          this.productData.totalMachineMins = 0;
          this.productData.materialCost = 0;
          this.getProductDataFlag = true;
          this.getQuotePartKitNo();
        }
      }));
  }

  getProductData() {
    this.getProductDataFlag = false;
    this.productSteps = [];
    this.productService.productGet(this.id, null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.productData = response.object[0];
        this.getProductDataFlag = true;
        if (this.productData.productSteps && this.productData.productSteps.length > 0) {
          this.productData.productSteps.forEach((value) => {
            const productStepValue = this.calculateProductStepValues(value);
            this.productSteps.push(productStepValue);
          });
          this.getStepsValues(this.mode === this.productPageModes.Create);
          this.getProcedureStepsFlag = true;
        } else {
          this.getProcedureSteps(this.productData.procedureId);
        }
        this.getQuoteData(this.productData.quoteId);
      }));
  }

  getCustomers() {
    if (this.getCustomersFlag) {
      return this.customersData;
    }
    this.customerService.customerGet(null, null, null, null, null, null, null, null, env.apiVersion).subscribe(responseHandler((response) => {
      response.object.map((x) => {
        this.customersData.push({ label: `[MSR-FSR] ${x.name} - [ID: ${x.id}]`, value: x.id });
      });
      this.getCustomersFlag = true;
    }));
  }

  getCustomerLabel(customer: Customer): string {
    return `[MSR-FSR] ${customer.name} - [ID: ${customer.id}]`;
  }

  getParts(isRefresh: boolean = false) {
    if (isRefresh) {
      this.isRefreshingPartsData = true;
      this.globals.showLoader(true);
      this.partsData = [];
    }

    if (this.getPartsFlag && !isRefresh) {
      return this.partsData;
    }

    this.partsService.partGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          this.partsData.push({ label: `${x.name} [${x.partNumber}] [ID: ${x.id}]`, partNumber: x.partNumber, value: x.id });
        });
        this.getPartsFlag = true;
        if (isRefresh) {
          this.isRefreshingPartsData = false;
        }
        if (this.mode === this.productPageModes.Create) {
          this.getQuotePartKitNo();
        }
      }));
  }

  getQuotePartKitNo() {
    if (this.productData.partId === null && this.getQuoteDataFlag && this.getPartsFlag) {
      const index = this.partsData.findIndex(x => x.partNumber === this.quoteData.partKitNo);
      if (index > -1) {
        this.productData.partId = this.partsData[index].value;
      }
    }
  }

  getPartLabel(part: PartModel): string {
    return `${part.name} [${part.partNumber}] [ID: ${part.id}]`;
  }

  getProcedures(isRefresh: boolean = false) {
    if (isRefresh) {
      this.isRefreshingProceduresData = true;
      this.globals.showLoader(true);
      this.proceduresData = [];
    }
    if (this.getProceduresFlag && !isRefresh) {
      return this.proceduresData;
    }
    this.procedureService.procedureGet(null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.map((x) => {
          this.proceduresData.push({ label: `${x.name} [ID: ${x.id}]`, value: x.id });
        });
        this.getProceduresFlag = true;
        if (isRefresh) {
          this.isRefreshingProceduresData = false;
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
      const step = new ProductStepModel({
        laborMinutes: $event.value.laborTime,
        equipmentMinutes: $event.value.equipmentTime,
        replacementCost: $event.value.replacementCost,
        utilization:  $event.value.utilization,
        usefulLife: $event.value.usefulLife,
        printOrder: index + 1,
        title: $event.value.title,
      });
      const productStepValue = this.calculateProductStepValues(step);
      const stepTemplate = this.productSteps[index].stepTemplate;
      this.productSteps[index] = {stepTemplate, ...productStepValue};
      this.getStepsValues(true);
    }
  }

  getProcedureStepTemplates() {
    if (this.getProcedureStepTemplatesFlag) {
      return this.procedureStepTemplatesData;
    }
    this.procedureStepTemplateService.procedureStepTemplateGet(null, env.apiVersion)
      .pipe(take(1))
      .subscribe(responseHandler(response => {
        this.procedureStepTemplatesData = response.object;
        this.getProcedureStepTemplatesFlag = true;
      }));
  }

  /**
   * Get ProcedureSteps by procedure Id
   */
  getProcedureSteps(id: number) {
    this.getProcedureStepsFlag = false;
    this.globals.showLoader(true);
    this.productSteps = [];
    this.procedureService.stepGet(id, null, env.apiVersion).pipe(take(1))
      .subscribe(responseHandler(response => {
        response.object.forEach((value) => {
          // Calculate each step values
          const step = new ProductStepModel({
            procedureStepId: value.id,
            laborMinutes: value.laborTime,
            equipmentMinutes: value.equipmentTime,
            replacementCost: value.replacementCost,
            utilization:  value.utilization,
            usefulLife: value.usefulLife,
            printOrder: value.printOrder,
            title: value.title,
          });
          const productStepValue = this.calculateProductStepValues(step);
          this.productSteps.push(productStepValue);
        });
        this.getProcedureStepsFlag = true;

        // Calcuate total step values
        this.getStepsValues(this.mode === this.productPageModes.Create);
        this.newStepsCounts = 0;
      }));
  }

  onSelectProcedure($event) {
    this.getProcedureSteps($event.target.value);
  }

  onAddProductStep() {
    this.productSteps.push({});
    this.newStepsCounts++;
  }

  onRemoveProductStep() {
    if (this.newStepsCounts > 0) {
      this.productSteps.pop();
      this.newStepsCounts--;
      this.getStepsValues(true);
    }
  }

  getStepsValues(isRefresh: boolean = false) {
    let totalLaborMins = 0;
    let totalMachineMins = 0;
    let totalLaborCharge = 0;
    let totalEquipmentCharge = 0;

    this.productSteps.forEach(step => {
      totalLaborMins += (step.laborMinutes ? step.laborMinutes : 0);
      totalMachineMins += (step.equipmentMinutes ? step.equipmentMinutes : 0);
      totalLaborCharge += step.laborCharge;
      totalEquipmentCharge += step.equipmentCharge;
    });
    this.productData.totalLaborMins = totalLaborMins;
    this.productData.totalMachineMins = totalMachineMins;

    if (isRefresh) {
      this.productData.laborCost = parseFloat(totalLaborCharge.toFixed(2));
      this.productData.equipmentCost = parseFloat(totalEquipmentCharge.toFixed(2));
      this.productData.totalSalePrice = parseFloat((this.productData.laborCost + this.productData.equipmentCost + (this.productData.materialCost ? this.productData.materialCost : 0)).toFixed(2));
    }
  }

  onChangeLaborTime($event, index: number) {
    jQuery(`#laborMinutes_${index}`).parsley().validate();
    if (jQuery(`#laborMinutes_${index}`).parsley().isValid()) {
      let laborMinutes = 0;
      if ($event.target.value) {
        laborMinutes = parseInt($event.target.value, 10);
      }

      this.productSteps[index].laborMinutes = laborMinutes;
      this.productSteps[index].laborCharge = laborMinutes * this.adminCostSettings.laborRateMinute;

      this.getStepsValues(true);
    }
  }

  onChangeEquipmentTime($event, index: number) {
    jQuery(`#equipmentMinutes_${index}`).parsley().validate();
    if (jQuery(`#equipmentMinutes_${index}`).parsley().isValid()) {
      let equipmentMinutes = 0;
      if ($event.target.value) {
        equipmentMinutes = parseInt($event.target.value, 10);
      }

      this.productSteps[index].equipmentMinutes = equipmentMinutes;
      this.productSteps[index].equipmentCharge = equipmentMinutes * this.productSteps[index].equipmentExpensePerMinute + equipmentMinutes * this.productSteps[index].rmPerMinuteRate;

      this.getStepsValues(true);
    }
  }

  onChangeMaterialCost($event) {
    jQuery('#materialCost').parsley().validate();
    if (jQuery('#materialCost').parsley().isValid()) {
      let materialCost = 0.0;
      if ($event.target.value) {
        materialCost = parseFloat(parseFloat($event.target.value).toFixed(2));
        this.productData.materialCost = materialCost;
      } else {
        this.productData.materialCost = null;
      }
      this.productData.totalSalePrice = parseFloat((this.productData.laborCost + this.productData.equipmentCost + materialCost).toFixed(2));
    }
  }

  onChangeTotalSalePrice($event) {
    jQuery('#totalSalePrice').parsley().validate();
    if (jQuery('#totalSalePrice').parsley().isValid()) {
      if ($event.target.value) {
        const totalSalePrice = parseFloat(parseFloat($event.target.value).toFixed(2));
        this.productData.totalSalePrice = totalSalePrice;
      } else {
        this.productData.totalSalePrice = null;
      }
    }
  }

  onChangeCycleTime($event) {
    jQuery('#cycleTime').parsley().validate();
    if (jQuery('#cycleTime').parsley().isValid()) {
      if ($event.target.value) {
        const cycleTime = parseInt($event.target.value, 10);
        this.productData.cycleTime = cycleTime;
      } else {
        this.productData.cycleTime = null;
      }
    }
  }

  onChangePrintOrder($event, index: number) {
    jQuery(`#printOrder_${index}`).parsley().validate();
    if (jQuery(`#printOrder_${index}`).parsley().isValid()) {
      let printOrder = index + 1;
      if ($event.target.value) {
        printOrder = parseInt($event.target.value, 10);
      }
      this.productSteps[index].printOrder = printOrder;
    }
  }

  onChangeReplacementCost($event, index: number) {
    jQuery(`#replacementcost_${index}`).parsley().validate();
    if (jQuery(`#replacementcost_${index}`).parsley().isValid()) {
      let replacementCost = null;

      if ($event.target.value) {
        replacementCost = parseInt($event.target.value, 10);
      }

      this.productSteps[index].replacementCost = replacementCost;

      this.productSteps[index].rmAnnualRate = replacementCost ? replacementCost * this.adminCostSettings.rmAnnualRate : 0;
      this.calculateEquipPerMinute(index);
    }
  }

  onChangeUtilization($event, index: number) {
    jQuery(`#utilization_${index}`).parsley().validate();
    if (jQuery(`#utilization_${index}`).parsley().isValid()) {
      let utilization = null;

      if ($event.target.value) {
        utilization = parseFloat($event.target.value);
      }

      this.productSteps[index].utilization = utilization;

      this.productSteps[index].rmPerMinuteRate = utilization ? (this.adminCostSettings.rmAnnualRate / (this.adminCostSettings.yearsHours * this.adminCostSettings.hourMinutes * utilization)) : 0;
      this.calculateEquipPerMinute(index);
    }
  }

  onChangeUsefulLife($event, index: number) {
    jQuery(`#usefulLife_${index}`).parsley().validate();
    if (jQuery(`#usefulLife_${index}`).parsley().isValid()) {
      let usefulLife = null;

      if ($event.target.value) {
        usefulLife = parseInt($event.target.value, 10);
      }

      this.productSteps[index].usefulLife = usefulLife;
      this.calculateEquipPerMinute(index);
    }
  }

  onChangeEquipPerMin($event, index: number) {
    jQuery(`#equipPerMin_${index}`).parsley().validate();
    if (jQuery(`#equipPerMin_${index}`).parsley().isValid()) {
      let equipmentExpensePerMinute = null;

      if ($event.target.value) {
        equipmentExpensePerMinute = parseFloat($event.target.value);
      }

      this.productSteps[index].equipmentExpensePerMinute = equipmentExpensePerMinute;
      this.calculateCharge(index);
    }
  }

  onChangeAnualRM($event, index: number) {
    jQuery(`#anualRM_${index}`).parsley().validate();
    if (jQuery(`#anualRM_${index}`).parsley().isValid()) {
      let rmAnnualRate = null;

      if ($event.target.value) {
        rmAnnualRate = parseFloat($event.target.value);
      }

      this.productSteps[index].rmAnnualRate = rmAnnualRate;
      this.calculateCharge(index);
    }
  }

  onChangeRMPerMin($event, index: number) {
    jQuery(`#rmPerMin_${index}`).parsley().validate();
    if (jQuery(`#rmPerMin_${index}`).parsley().isValid()) {
      let rmPerMinuteRate = null;

      if ($event.target.value) {
        rmPerMinuteRate = parseFloat($event.target.value);
      }

      this.productSteps[index].rmPerMinuteRate = rmPerMinuteRate;
      this.calculateCharge(index);
    }
  }

  calculateEquipPerMinute(index: number) {
    this.productSteps[index].equipmentExpensePerMinute =
        (this.productSteps[index].replacementCost && this.productSteps[index].utilization && this.productSteps[index].usefulLife)
          ? (this.productSteps[index].replacementCost / this.productSteps[index].usefulLife) / (this.adminCostSettings.yearsHours * this.adminCostSettings.hourMinutes * this.productSteps[index].utilization)
          : 0;
    this.calculateCharge(index);
  }

  calculateCharge(index: number) {
    this.productSteps[index].laborCharge = this.productSteps[index].laborMinutes ? this.productSteps[index].laborMinutes * this.adminCostSettings.laborRateMinute : 0;

    this.productSteps[index].equipmentCharge = this.productSteps[index].equipmentMinutes
      ? (this.productSteps[index].equipmentMinutes * this.productSteps[index].equipmentExpensePerMinute +  this.productSteps[index].equipmentMinutes * this.productSteps[index].rmPerMinuteRate)
      : 0;

    this.getStepsValues(true);
  }

  calculateProductStepValues(step: ProductStepModel) {
    const rmAnnualRate = step.rmAnnualRate ? step.rmAnnualRate : (step.replacementCost ? step.replacementCost * this.adminCostSettings.rmAnnualRate : 0);

    const rmPerMinuteRate = step.rmPerMinuteRate ? step.rmPerMinuteRate : (step.utilization ? (this.adminCostSettings.rmAnnualRate / (this.adminCostSettings.yearsHours * this.adminCostSettings.hourMinutes * step.utilization)) : 0);

    const equipmentExpensePerMinute = step.equipmentExpensePerMinute
      ? step.equipmentExpensePerMinute
      : ((step.replacementCost && step.utilization && step.usefulLife)
        ? (step.replacementCost / step.usefulLife) / (this.adminCostSettings.yearsHours * this.adminCostSettings.hourMinutes * step.utilization)
        : 0);

    const laborCharge = step.laborMinutes ? step.laborMinutes * this.adminCostSettings.laborRateMinute : 0;

    const equipmentCharge = step.equipmentMinutes ? (step.equipmentMinutes * equipmentExpensePerMinute + step.equipmentMinutes * rmPerMinuteRate) : 0;

    return {
      ...step,
      laborMinutes: step.laborMinutes || 0,
      equipmentMinutes: step.equipmentMinutes || 0,
      rmAnnualRate,
      rmPerMinuteRate,
      equipmentExpensePerMinute,
      laborCharge,
      equipmentCharge,
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

  openRequirementViewModal() {
    this.showRequirementViewModal = true;
  }

  closeRequirementViewModal() {
    this.showRequirementViewModal = false;
  }

  openUrlWithNewTab(urlTree: string) {
    const url = this.router.serializeUrl(
      this.router.createUrlTree([urlTree])
    );
    window.open('#/' + url, '_blank');
  }

  onSubmit() {
    jQuery('.parsleyjs').parsley().validate();
    if (jQuery('.parsleyjs').parsley().isValid()) {
      const productData = this.productData;
      productData.productSteps = [];
      this.productSteps.forEach((step) => {
        const productStep = new ProductStep();
        productStep.init(step);
        productStep.productId = this.mode === this.productPageModes.Create ? null : this.id;
        productData.productSteps.push(productStep);
      });
      this.globals.showLoader(true);
      if (this.mode === this.productPageModes.Create) {
        const requestData = new CreateProductRequest();
        requestData.init(productData);
        this.productService.productPost(env.apiVersion, requestData)
          .pipe(take(1))
          .subscribe(responseHandler((resp) => {
            if (!resp.hasErrors) {
              this.router.navigate(['app/pricing/products']);
            }
          }));
      } else if (this.mode === this.productPageModes.Edit) {
        const updateData = new UpdateProductRequest();
        updateData.init(productData);
        this.productService.productPatch(env.apiVersion, updateData)
          .pipe(take(1))
          .subscribe(responseHandler((resp) => {
            if (!resp.hasErrors) {
              this.router.navigate(['app/pricing/products']);
            }
          }));
      }
    }
  }

  print() {
    window.print();
  }
}

