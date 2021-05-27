import { WorkOrderModel, WorkOrderPartModel, PurchaseModel }
    from '../../services/api.client.generated';

interface NumberHashMap<T> {
    [key: number]: T
}

interface PackingListPartViewModel {
    part: WorkOrderPartModel;
    poLineLabel: string;
}

export class PackingListViewModel {
    valid: boolean;
    sortedParts: Array<PackingListPartViewModel>;

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
        let parents: NumberHashMap<Array<WorkOrderPartModel>> = {};
        let referencePO: string = purchase?.purchaseOrder?.referencePO;

        // scan for parents
        for (index = 0; index < partCount ; index++) {
            let part: WorkOrderPartModel = workOrder.workOrderParts[index];
            if (typeof(part.parentId) === 'undefined' ||
                part.parentId === null) {
                parents[part.id] = [part];
            }
        }

        // scan for children
        for (index = 0; index < partCount ; index++) {
            let part: WorkOrderPartModel = workOrder.workOrderParts[index];

            if (typeof(part.parentId) === 'undefined' ||
                part.parentId === null) {
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
        Object.keys(parents).sort().forEach(partid => {
            let parts: Array<WorkOrderPartModel> = parents[partid];
            let subpartIndex: number;
            for (subpartIndex = 0;
                 subpartIndex < parts.length;
                 subpartIndex++) {
                let part: WorkOrderPartModel = parts[subpartIndex];
                let partViewModel: PackingListPartViewModel = {
                    part,
                    poLineLabel: ""
                };

                // parent is index 0, children are the rest
                if (subpartIndex == 0) {
                    partViewModel.poLineLabel =
                        `${referencePO}/${part.customerLineNumber}`;
                } else {
                    partViewModel.poLineLabel = "";
                }

                this.sortedParts.push(partViewModel);
            }
        });
    }
}
