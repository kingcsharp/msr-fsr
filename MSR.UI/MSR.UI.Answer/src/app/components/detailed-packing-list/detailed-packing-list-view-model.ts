import { WorkOrderModel, WorkOrderPartModel, PurchaseModel, WorkOrderTaskMonitorModel} from '../../services/api.client.generated';

export class NumberHashMap<T> {
    [key: number]: T
}

interface PackingListPartViewModel {
    part: WorkOrderPartModel;
    subparts?: Array<WorkOrderPartModel>;
    poLineLabel: string;
}

/* Monitor data returned by the API is raw.  It performs
   no logic to tell the UI what to display, and
   if it is a pass or fail.  This is up to the front-end
   code to determine.  TODO: this should probably
   be factored out into something reusable */
interface MonitorResult {
    monitorDescription: string;
    valueAsString: string;
    isPassing: boolean;
}

export class PackingListViewModel {
    valid: boolean;
    sortedParts: Array<PackingListPartViewModel>;
    parentParts: Array<PackingListPartViewModel>;
    allMonitors: Array<MonitorResult>;

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
        let parents: NumberHashMap<Array<WorkOrderPartModel>> =
            new NumberHashMap<Array<WorkOrderPartModel>>();
        let referencePO: string = purchase?.purchaseOrder?.referencePO;

        // scan for parents
        for (index = 0; index < partCount; index++) {
            let part: WorkOrderPartModel = workOrder.workOrderParts[index];
            if (typeof (part.parentId) === 'undefined' ||
                part.parentId === null) {
                part.parentId = null;
                parents[part.id] = [part];
            }
        }

        // scan for children
        for (index = 0; index < partCount; index++) {
            let part: WorkOrderPartModel = workOrder.workOrderParts[index];

            if (typeof (part.parentId) === 'undefined' ||
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
            let parentPart: PackingListPartViewModel;

            for (subpartIndex = 0;
                subpartIndex < parts.length;
                subpartIndex++) {
                let part: WorkOrderPartModel = parts[subpartIndex];
                let partViewModel: PackingListPartViewModel = {
                    part,
                    poLineLabel: '',
                    subparts: null,
                };

                // parent is index 0, children are the rest
                if (subpartIndex === 0) {
                    partViewModel.poLineLabel =
                        `${referencePO}/${part.customerLineNumber}`;
                    partViewModel.subparts = new Array<WorkOrderPartModel>();
                    parentPart = partViewModel;
                } else {
                    partViewModel.poLineLabel = '';
                    parentPart.subparts.push(partViewModel.part);
                }

                this.sortedParts.push(partViewModel);
            }
        });
        this.parentParts = this.sortedParts.filter(e => e.part.parentId == null);

        // prepare monitor result data
        this.allMonitors = new Array<MonitorResult>();
        workOrder.workOrderTasks.forEach(workOrderTask => {
            if (workOrderTask.workOrderTaskMonitors &&
                workOrderTask.workOrderTaskMonitors.length > 0) {
                this.allMonitors = this.allMonitors.concat(
                    workOrderTask.workOrderTaskMonitors.map(
                        workOrderTaskMonitor =>
                            this.getMonitorResult(workOrderTaskMonitor)
                    )
                );
            }
        });
    }

    // Check a monitor's data to determine status of pass/fail
    getMonitorResult(monitorModel: WorkOrderTaskMonitorModel): MonitorResult {

        let result: MonitorResult = {
            monitorDescription: '',
            valueAsString: '',
            isPassing: false
        };
        result.monitorDescription = monitorModel.description;

        /* monitor type table in database:
            1 Equipment
            2 Number
            3 Yes or No
            4 Text
            5 Pass or Fail
            6 Select
        */
        switch(monitorModel.monitorTypeId) {
            case 1:
                result.valueAsString = monitorModel.textVal; // not sure?
                result.isPassing = (
                    monitorModel.textVal &&
                    monitorModel.textVal.length > 0
                );
                break;
            case 2:
                if (typeof monitorModel.numVal === 'undefined' ||
                    monitorModel.numVal === null) {
                    result.valueAsString = null;
                } else {
                    result.valueAsString =
                        monitorModel.numVal.toString();
                }
                let targetValue = parseFloat(monitorModel.targetValue);
                let highValue = monitorModel.highTarget;
                let lowValue = monitorModel.lowTarget;
                let parsedValue = monitorModel.numVal;
                if (monitorModel.shouldBe === 'ABOVE') {
                    result.isPassing =
                        parsedValue != null &&
                        parsedValue > targetValue;
                } else if (monitorModel.shouldBe === 'BELOW') {
                    result.isPassing =
                        parsedValue != null &&
                        parsedValue < targetValue;
                } else if (monitorModel.shouldBe === 'BETWEEN') {
                    result.isPassing =
                        parsedValue != null &&
                        lowValue < parsedValue &&
                        parsedValue < highValue;
                } else if (monitorModel.shouldBe === 'EQUAL') {
                    result.isPassing =
                        parsedValue != null &&
                        parsedValue == targetValue;
                } else if (monitorModel.shouldBe === 'NULL') {
                    result.isPassing = (
                        result.valueAsString == null ||
                        result.valueAsString == undefined
                    );
                }
                break;
            case 3:
                result.valueAsString = monitorModel.numVal ? 'YES' : 'NO';
                if (monitorModel.targetValue) {
                    result.isPassing = result.valueAsString == 'YES';
                } else {
                    result.isPassing = result.valueAsString == 'NO';
                }
                break;
            case 4:
                result.valueAsString = monitorModel.textVal;
                result.isPassing = (
                    monitorModel.textVal &&
                    monitorModel.textVal.length > 0
                );
                break;
            case 5:
                result.valueAsString = monitorModel.numVal ? 'PASS' : 'FAIL';
                result.isPassing = result.valueAsString === 'PASS';
                break;
            case 6:
                result.valueAsString = monitorModel.textVal;
                result.isPassing = (
                    monitorModel.textVal &&
                    monitorModel.textVal.length > 0
                );
                break;
        }
        return result;
    }
}
