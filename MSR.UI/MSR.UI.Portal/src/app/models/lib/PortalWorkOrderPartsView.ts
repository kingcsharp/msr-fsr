import { IPortalWorkOrderView, PortalWorkOrderView, WorkOrderPartModel } from "../../services/api.client.generated";


export interface IPortalWorkOrderPartsView extends IPortalWorkOrderView{
    workOrderParts: WorkOrderPartModel[];
}

export class PortalWorkOrderPartsView extends PortalWorkOrderView {
    workOrderParts: WorkOrderPartModel[];

    constructor(data?: IPortalWorkOrderPartsView) {
        super(data);
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    init(_data?: any) {
        super.init(_data);
        if (_data) {
            this.workOrderParts = _data['workOrderParts'];
            if (Array.isArray(_data["workOrderParts"])) {
                this.workOrderParts = [] as WorkOrderPartModel[];
                for (let item of _data["workOrderParts"])
                    this.workOrderParts!.push(WorkOrderPartModel.fromJS(item));
            }
        }
    }

    static fromJS(data: any): PortalWorkOrderPartsView {
        data = typeof data === 'object' ? data : {};
        let result = new PortalWorkOrderPartsView();
        result.init(data);
        return result;
    }

    

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};

        if (Array.isArray(this.workOrderParts)) {
            data["workOrderParts"] = [];
            for (let item of this.workOrderParts)
                data["workOrderParts"].push(item.toJSON());
        }

        super.toJSON(data);

        return data;
    }


}

