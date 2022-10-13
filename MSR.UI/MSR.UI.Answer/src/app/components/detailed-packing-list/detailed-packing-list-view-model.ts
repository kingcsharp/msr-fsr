import {
  WorkOrderModel,
  WorkOrderPartModel,
  PurchaseModel,
  WorkOrderTaskMonitorModel,
  ProductModel,
} from "../../services/api.client.generated";
import { MonitorStatusPipe } from "../../pipes/monitorstatus";
import * as _ from "lodash";
export class NumberHashMap<T> {
  [key: number]: T;
}

interface PackingListPartViewModel {
  part: WorkOrderPartModel;
  subparts?: Array<WorkOrderPartModel>;
  poLineLabel: string;
}

export class PackingListViewModel {
  valid: boolean;
  sortedParts: Array<PackingListPartViewModel>;
  parentParts: Array<PackingListPartViewModel>;
  allMonitors: Array<WorkOrderTaskMonitorModel>;

  constructor() {
    this.valid = false;
    this.sortedParts = undefined;
  }

  // The purpose of this is to colate the list of parts, in order,
  // with groupings by subparts.  This way the view can just iterate
  // through and display them without any additional logic.
  populate(workOrder: WorkOrderModel, purchase: PurchaseModel) {
    let index: number;
    let partCount: number = workOrder.workOrderParts.length;
    let parents: NumberHashMap<Array<WorkOrderPartModel>> = new NumberHashMap<
      Array<WorkOrderPartModel>
    >();
    let referencePO: string = purchase?.purchaseOrder?.referencePO;

    // scan for parents
    for (index = 0; index < partCount; index++) {
      let part: WorkOrderPartModel = workOrder.workOrderParts[index];
      if (typeof part.parentId === "undefined" || part.parentId === null) {
        let woProduct: ProductModel = _.find(
          workOrder.workOrderProducts,
          (p) => p.partId && part.partId && p.partId === part.partId
        );
        part.parentId = null;
        part.workOrder = new WorkOrderModel({ product: woProduct });
        parents[part.id] = [part];
      }
    }

    // scan for children
    for (index = 0; index < partCount; index++) {
      let part: WorkOrderPartModel = workOrder.workOrderParts[index];

      if (typeof part.parentId === "undefined" || part.parentId === null) {
        continue;
      }

      if (parents.hasOwnProperty(part.parentId)) {
        parents[part.parentId].push(part);
      } else {
        // orphan, display anyway
        parents[part.id] = [part];
      }
    }

    this.sortedParts = new Array();
    Object.keys(parents)
      .sort()
      .forEach((partid) => {
        let parts: Array<WorkOrderPartModel> = parents[partid];
        let subpartIndex: number;
        let parentPart: PackingListPartViewModel;

        for (subpartIndex = 0; subpartIndex < parts.length; subpartIndex++) {
          let part: WorkOrderPartModel = parts[subpartIndex];
          let partViewModel: PackingListPartViewModel = {
            part,
            poLineLabel: "",
            subparts: null,
          };

          // parent is index 0, children are the rest
          if (subpartIndex === 0) {
            partViewModel.poLineLabel = `${referencePO}/${part.customerLineNumber}`;
            partViewModel.subparts = new Array<WorkOrderPartModel>();
            parentPart = partViewModel;
          } else {
            partViewModel.poLineLabel = "";
            parentPart.subparts.push(partViewModel.part);
          }

          this.sortedParts.push(partViewModel);
        }
      });
    this.parentParts = this.sortedParts.filter((e) => e.part.parentId == null);

    // prepare monitor result data
    this.allMonitors = new Array<WorkOrderTaskMonitorModel>();
    workOrder.workOrderTasks.forEach((workOrderTask) => {
      if (
        workOrderTask.workOrderTaskMonitors &&
        workOrderTask.workOrderTaskMonitors.length > 0
      ) {
        this.allMonitors = this.allMonitors.concat(
          workOrderTask.workOrderTaskMonitors
        );
      }
    });
  }
}
