import {
  Component,
  OnInit,
  ElementRef,
  ViewEncapsulation,
} from "@angular/core";
import { Globals } from "../../../models/lib/globals";
import { ColumnsSaved } from "../../../models/lib/ColumnsSaved";
import { CommonGrid } from "../../../models/lib/CommonGrid";
import { SelectItem } from "primeng/api";
import {
  WorkOrderService,
  EnumSegregationType,
  ReportModel,
  UpdateWorkOrderPriceRequest,
  CreateWorkOrderMessageRequest,
  WorkOrderGridSummary,
  WorkOrderMessageModel,
} from "../../../services/api.client.generated";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { take } from "rxjs/operators";
import { LazyLoadEvent } from "primeng/api";
import { callFunctionWithFiltersViews } from "../../../models/lib/Utils";
import { EnumColumnType } from "../../../../app/models/enums/EnumColumnType";
import { GridSaved } from "../../../models/lib/GridSaved";
import * as moment from "moment";
import * as _ from "lodash";
import { clone, cloneDeep } from "lodash";
import { MergeScanOperator } from "rxjs/internal/operators/mergeScan";

interface SelectedItem extends WorkOrderGridSummary {
  initialValues: Omit<SelectedItem, 'initialValues'>
  scheduledEndDateChangeReason: string,
  selectedIndex: number,
  scheduledEndDateChanged: boolean,
  // workOrderMessages: Array<any>
}

@Component({
  selector: "app-wip",
  templateUrl: "./wip.component.html",
  styleUrls: ["./wip.component.scss"],
  providers: [WorkOrderService],
  encapsulation: ViewEncapsulation.Emulated,
})
export class WipComponent implements OnInit {
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  locationOptions: Array<SelectItem>;
  gridVersion: string;
  EnumSegregationType = EnumSegregationType;
  totalRecords: number = 0;
  gridPartsSaved: GridSaved;
  reportPartsModel: ReportModel;
  prices: Array<number> = [];
  currentRowIndex: number = -1;
  invalidPriceError: boolean = false;
  adminOrManager: boolean = false;
  showDispositionDialog: boolean = false;
  selectedItem: SelectedItem = null;
  instructions: string;
  currentEvent: any;
  showScheduledEndDateDialog: boolean = false;

  constructor(
    public commonGrid: CommonGrid,
    private elementReference: ElementRef,
    public globals: Globals,
    private workOrderService: WorkOrderService
  ) {}

  ngOnInit(): void {
    this.adminOrManager = this.globals
      .getCurrentUser()
      .roles.some(
        (role) =>
          role.name === "Administrator" || role.name === "Production Manager"
      );

    this.gridPartsSaved = new GridSaved({
      columnsSaved: [
        new ColumnsSaved({
          id: "id",
          label: "Id",
          visible: false,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.Number,
        }),
        new ColumnsSaved({
          id: "serialNumber",
          label: "Serial #",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.String,
          styles: { "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "partNumber",
          label: "Company Part #",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.String,
          styles: { "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "cycleCount",
          label: "Cycle Count",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.Number,
          styles: { width: "10rem", "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "qty",
          label: "Qty",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.Number,
          styles: { width: "6rem", "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "name",
          label: "Part Name",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.String,
          styles: { width: "30rem", "text-align": "center" },
        }),
      ],
      showMyViewsFeature: false,
      paginator: false,
      storageId:
        "wiphistory_parts" +
        this.elementReference.nativeElement.tagName.toLowerCase(),
      version: "1.0.0",
    });

    this.reportPartsModel = new ReportModel({
      name: "",
    });

    this.gridStorageId =
      "wogrid" + this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({
        id: "purchaseId",
        label: "Purchase Id",
        visible: false,
        type: EnumColumnType.Number,
      }),
      new ColumnsSaved({
        id: "workOrderItemNumber",
        label: "WorkOrder Item Number",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "customerName",
        label: "Customer",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "locationName",
        label: "Location",
        visible: true,
        type: EnumColumnType.StringArray,
      }),
      new ColumnsSaved({
        id: "serialNumber",
        label: "Serial Number",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "referencePO",
        label: "PO #",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "quantity",
        label: "Quantity",
        visible: true,
        type: EnumColumnType.Number,
      }),
      new ColumnsSaved({
        id: "scheduledStartDate",
        label: "Scheduled Start Date",
        visible: true,
        type: EnumColumnType.Date,
      }),
      new ColumnsSaved({
        id: "scheduledEndDate",
        label: "Scheduled End Date",
        visible: true,
        type: EnumColumnType.Date,
      }),
      new ColumnsSaved({
        id: "actualStartDate",
        label: "Actual Start Date",
        visible: true,
        type: EnumColumnType.Date,
      }),
      new ColumnsSaved({
        id: "productName",
        label: "Product",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "procedureName",
        label: "Procedure",
        visible: true,
        type: EnumColumnType.String,
      }),
      new ColumnsSaved({
        id: "status",
        label: "Status",
        visible: true,
        type: EnumColumnType.StringArray,
      }),
      new ColumnsSaved({
        id: "disposition",
        label: "Disposition",
        visible: true,
        type: EnumColumnType.String,
        styles: { width: "10rem", "text-align": "center" },
      }),
    ];

    if (this.adminOrManager) {
      this.gridSettings.push(
        new ColumnsSaved({
          id: "price",
          label: "Price",
          visible: true,
          type: EnumColumnType.Number,
        })
      );
    }

    this.statusOptions = [
      { label: "In Progress", value: "In Progress" },
      { label: "Waiting to Start", value: "Waiting to Start" },
    ];
    this.locationOptions = this.globals.getTopLevelLocations();
  }


  editSelectedEndDate(model: SelectedItem, index: number){
    this.selectedItem = model;
    this.selectedItem.selectedIndex = index;
    this.selectedItem.initialValues = cloneDeep(model);
    this.showScheduledEndDateDialog = true;
  }

  saveScheduledEndDate(){
    alert('saved');
    this.selectedItem.scheduledEndDateChanged = true;
    this.showScheduledEndDateDialog = false;
  }
  closeScheduledEndDateDialog(){
    const {initialValues}  = this.selectedItem;
    this.data[this.selectedItem.selectedIndex] = initialValues;
    this.showScheduledEndDateDialog = false;
  }


  getScheduledEndDateChangeReason(model: SelectedItem){
    return model.scheduledEndDateChangeReason ? `Reason: ${model.scheduledEndDateChangeReason}`: null;
  }

  getWorkOrders(event: LazyLoadEvent) {
    if (event !== undefined) {
      this.currentEvent = event;
    }
    this.globals.showLoader(true);
    setTimeout(() => {
      // this.workOrderService.menu(0, 100, null, null, env.apiVersion)
      callFunctionWithFiltersViews(
        this.workOrderService,
        this.workOrderService.menu,
        {},
        this.gridSettings,
        this.currentEvent,
        this.globals.functionDic
      )
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.totalRecords = response.totalNumberOfRecords;
            this.data = response.object;
            this.prices = this.data.map((workorderMenu) => workorderMenu.price);
            this.currentRowIndex = -1;
            this.invalidPriceError = false;
            this.data.map((elem) => this.setElementStyle(elem));
          })
        );
    }, 10);
  }

  updateWorkOrderPrice(rowIndex: number) {
    this.globals.showLoader(true);
    const currentWorkOrder = this.data[rowIndex];
    const updatePriceRequest = new UpdateWorkOrderPriceRequest({
      workOrderId: currentWorkOrder.id,
      price: currentWorkOrder.price,
    });
    this.workOrderService
      .updatePrice(env.apiVersion, updatePriceRequest)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.data[rowIndex].price = response.object?.price;
          this.prices[rowIndex] = response.object?.price;
        })
      );
  }

  setElementStyle(elem) {
    elem.timeLoggedType = "danger";
    if (elem.percentageOfExpectedDurationTimeLogged > 0.25) {
      elem.timeLoggedType = "warning";
    }
    if (elem.percentageOfExpectedDurationTimeLogged > 0.5) {
      elem.timeLoggedType = "info";
    }
    if (elem.percentageOfExpectedDurationTimeLogged > 0.75) {
      elem.timeLoggedType = "success";
    }
    elem.tasksCompletedType = "danger";
    if (elem.percentageOfTasksCompleted > 0.25) {
      elem.tasksCompletedType = "warning";
    }
    if (elem.percentageOfTasksCompleted > 0.5) {
      elem.tasksCompletedType = "info";
    }
    if (elem.percentageOfTasksCompleted > 0.75) {
      elem.tasksCompletedType = "success";
    }
  }

  getVisibleColumns() {
    return this.gridSettings.filter((x) => x.visible).length;
  }

  onEditInit(): void {
    this.currentRowIndex = -1;
    this.invalidPriceError = false;
  }

  onEditCancel(): void {
    if (this.currentRowIndex !== -1) {
      this.data[this.currentRowIndex].price = this.prices[this.currentRowIndex];
    }
  }

  onEditComplete(): void {
    if (this.currentRowIndex !== -1) {
      if (this.invalidPriceError) {
        this.data[this.currentRowIndex].price =
          this.prices[this.currentRowIndex];
      } else {
        const newPrice = parseFloat(
          Number(this.data[this.currentRowIndex].price).toFixed(2)
        );
        this.data[this.currentRowIndex].price = newPrice;
        if (newPrice !== this.prices[this.currentRowIndex]) {
          this.updateWorkOrderPrice(this.currentRowIndex);
        }
      }
    }
  }

  onChangePrice(rowIndex: number) {
    this.currentRowIndex = rowIndex;
    let invalidPriceError = false;
    if (!Number(this.data[this.currentRowIndex].price)) {
      invalidPriceError = true;
    }
    this.invalidPriceError = invalidPriceError;
  }

  viewDispositionHistory(model: SelectedItem) {
    this.instructions = "";
    this.selectedItem = model;
    this.selectedItem.workOrderMessages = _.sortBy(
      model.workOrderMessages,
      (message) => moment(message.date).valueOf()
    )
      .reverse()
      .map<WorkOrderMessageModel>((message) => {
        return {
          ...message,
          date: moment(message.date).format("MMM DD, YYYY HH:mm"),
        } as unknown as WorkOrderMessageModel;
      });
    this.showDispositionDialog = true;
  }

  closeDispositionDialog() {
    this.showDispositionDialog = false;
    this.selectedItem = null;
  }

  saveInstructions() {
    this.globals.showLoader(true);
    const request = new CreateWorkOrderMessageRequest({
      id: this.selectedItem.id,
      message: this.instructions,
    });
    this.workOrderService
      .message(env.apiVersion, request)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.selectedItem.workOrderMessages.splice(0, 0, {
            message: request.message,
            name: this.globals.getCurrentUser().fullName,
            date: moment().format("MMM DD, YYYY HH:mm") as unknown as Date,
          } as WorkOrderMessageModel);
          this.instructions = "";
          this.getWorkOrders(undefined);
        })
      );
  }
}
