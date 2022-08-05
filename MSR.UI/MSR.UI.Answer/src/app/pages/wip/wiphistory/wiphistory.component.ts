import {
  Component,
  OnInit,
  ElementRef,
  ViewChild,
  AfterViewInit,
} from "@angular/core";
import { Globals } from "../../../models/lib/globals";
import { ColumnsSaved } from "../../../models/lib/ColumnsSaved";
import { CommonGrid } from "../../../models/lib/CommonGrid";
import { SelectItem } from "primeng/api";
import { EnumPrivilege } from "../../../models/enums/privileges";
import {
  EnumMenuItem,
  EnumApprovalTables,
  WorkOrderService,
  ProductService,
  WorkOrderGridSummary,
  EnumSegregationType,
  WorkOrderHistoryView,
  ReportModel,
  CreateWorkOrderMessageRequest,
} from "../../../services/api.client.generated";
import { Router } from "@angular/router";
import { environment as env } from "../../../../environments/environment";
import { responseHandler } from "../../../utils/responseHandler";
import { take } from "rxjs/operators";
import { callFunctionWithFilters } from "../../../models/lib/Utils";
import { LazyLoadEvent } from "primeng/api";
import { GridSaved } from "../../../models/lib/GridSaved";
import { EnumColumnType } from "../../../models/enums/EnumColumnType";
import * as moment from "moment";
import * as _ from "lodash";

@Component({
  selector: "app-wiphistory",
  templateUrl: "./wiphistory.component.html",
  styleUrls: ["./wiphistory.component.scss"],
  providers: [WorkOrderService],
})
export class WiphistoryComponent implements OnInit, AfterViewInit {
  gridSettings: Array<ColumnsSaved> = new Array<ColumnsSaved>();
  gridStorageId: string;
  data: Array<any> = new Array<any>();
  statusOptions: Array<SelectItem>;
  canRead: boolean = false;
  privileges = EnumPrivilege;
  locationOptions: Array<SelectItem>;
  gridVersion: string;
  EnumSegregationType = EnumSegregationType;
  totalRecords: number = 0;
  gridPartsSaved: GridSaved;
  emptyReportModel: ReportModel;
  showDispositionDialog: boolean = false;
  selectedItem: any = null;
  instructions: string;
  currentEvent: any;

  gridProductsSaved: GridSaved;
  gridProductPartsSaved: GridSaved;
  @ViewChild("expandedRowTemplate") expandedRowTemplate: ElementRef;

  constructor(
    public commonGrid: CommonGrid,
    private elementReference: ElementRef,
    public globals: Globals,
    private router: Router,
    private workOrderService: WorkOrderService,
    private productService: ProductService
  ) {}

  ngOnInit(): void {
    const partColumnsSaved = [
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
    ];

    this.gridPartsSaved = new GridSaved({
      columnsSaved: partColumnsSaved,
      showMyViewsFeature: false,
      paginator: false,
      storageId:
        "wiphistory_parts" +
        this.elementReference.nativeElement.tagName.toLowerCase(),
      version: "1.0.0",
    });

    this.emptyReportModel = new ReportModel({
      name: "",
    });

    this.gridProductsSaved = new GridSaved({
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
          label: "Serial Number",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.String,
          styles: { width: "4rem", "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "qty",
          label: "Qty",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.Number,
          styles: { width: "2rem", "text-align": "center" },
        }),
        new ColumnsSaved({
          id: "name",
          label: "Product Name",
          visible: true,
          disableSort: true,
          disableFilter: true,
          type: EnumColumnType.String,
          styles: { "text-align": "center" },
        }),
      ],
      showMyViewsFeature: false,
      paginator: false,
      storageId:
        "wipmenu_products" +
        this.elementReference.nativeElement.tagName.toLowerCase(),
      version: "1.0.0",
      visibleColumns: 3,
      expandRows: true,
      expandRowProperty: "workOrderParts",
    });

    this.gridProductPartsSaved = new GridSaved({
      columnsSaved: partColumnsSaved,
      showMyViewsFeature: false,
      paginator: false,
      storageId:
        "wiphistory_product_parts" +
        this.elementReference.nativeElement.tagName.toLowerCase(),
      version: "1.0.0",
    });

    this.gridStorageId =
      "wiphistory_grid" +
      this.elementReference.nativeElement.tagName.toLowerCase();

    this.gridSettings = [
      new ColumnsSaved({
        id: "purchaseId",
        label: "Purchase Id",
        visible: false,
      }),
      new ColumnsSaved({
        id: "workOrderItemNumber",
        label: "WorkOrder Item Number",
        visible: true,
      }),
      new ColumnsSaved({ id: "customer", label: "Customer", visible: true }),
      new ColumnsSaved({ id: "location", label: "Location", visible: true }),
      new ColumnsSaved({
        id: "serialNumber",
        label: "Serial Number",
        visible: true,
      }),
      new ColumnsSaved({
        id: "purchaseOrderNumber",
        label: "PO #",
        visible: true,
      }),
      new ColumnsSaved({ id: "qty", label: "Quantity", visible: true }),
      new ColumnsSaved({
        id: "scheduledStartDate",
        label: "Scheduled Start Date",
        visible: true,
      }),
      new ColumnsSaved({
        id: "scheduledEndDate",
        label: "Scheduled End Date",
        visible: true,
      }),
      new ColumnsSaved({
        id: "actualStartDate",
        label: "Actual Start Date",
        visible: true,
      }),
      new ColumnsSaved({
        id: "actualEndDate",
        label: "Actual End Date",
        visible: true,
      }),
      new ColumnsSaved({ id: "product", label: "Product", visible: true }),
      new ColumnsSaved({ id: "procedure", label: "Procedure", visible: true }),
      new ColumnsSaved({ id: "status", label: "Status", visible: true }),
      new ColumnsSaved({
        id: "disposition",
        label: "Disposition",
        visible: true,
      }),
    ];

    this.locationOptions = this.globals.getTopLevelLocations();
    this.statusOptions = [
      { label: "Complete", value: "Complete" },
      { label: "Cancelled", value: "Cancelled" },
    ];

    this.canRead = this.globals.hasPrivilege(
      EnumMenuItem.WIPHistory,
      this.privileges.CanRead
    );

    if (this.canRead === false) {
      this.router.navigate(["app/wip/wipstatus"]);
    }
  }

  ngAfterViewInit(): void {
    this.gridProductsSaved.expandRowsTemplate = this.expandedRowTemplate;
  }

  getWorkOrdersHistory(event: LazyLoadEvent) {
    if (event !== undefined) {
      this.currentEvent = event;
    }
    this.globals.showLoader(true);
    setTimeout(() => {
      callFunctionWithFilters(
        this.workOrderService,
        this.workOrderService.history,
        this.currentEvent,
        this.globals.functionDic
      )
        .pipe(take(1))
        .subscribe(
          responseHandler((response) => {
            this.totalRecords = response.totalNumberOfRecords;
            this.data = response.object;
          })
        );
    }, 10);
  }

  getVisibleColumns() {
    return this.gridSettings.filter((x) => x.visible).length;
  }

  viewDispositionHistory(model: any) {
    this.instructions = "";
    this.selectedItem = model;
    this.selectedItem.workOrderMessages = _.sortBy(
      model.workOrderMessages,
      (message) => moment(message.date).valueOf()
    )
      .reverse()
      .map((message) => {
        return {
          ...message,
          date: moment(message.date).format("MMM DD, YYYY HH:mm"),
        };
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
            date: moment().format("MMM DD, YYYY HH:mm"),
          });
          this.instructions = "";
          this.getWorkOrdersHistory(undefined);
        })
      );
  }

  expandRow(expanded, row) {
    this.clearExpandedRow();
    if (!expanded && !row.workOrderProducts) {
      const index = this.data.findIndex(
        (v) => v.workOrderId === row.workOrderId
      );
      if (index > -1) {
        this.getProducts(index);
      }
    }
  }

  getProducts(index) {
    this.globals.showLoader(true);
    this.productService
      .byWorkOrder(this.data[index].workOrderId, env.apiVersion)
      .pipe(take(1))
      .subscribe(
        responseHandler((response) => {
          this.data[index].workOrderProducts = response.object;
        })
      );
  }

  clearExpandedRow() {
    const gridState = localStorage.getItem(this.gridStorageId);

    if (gridState) {
      const jsonData = JSON.parse(gridState);
      delete jsonData.expandedRowKeys;
      localStorage.setItem(this.gridStorageId, JSON.stringify(jsonData));
    }
  }
}
