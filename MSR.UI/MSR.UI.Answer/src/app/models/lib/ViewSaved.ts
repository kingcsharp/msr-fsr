export interface IViewSaved {
    gridId?: string | undefined;
    viewName?: string | undefined;
    controllerName?: string | undefined;
    gridPagingData?: string | undefined;
    isDefault?: boolean | undefined;
    pagingTotal?: number | undefined;
    version?: string | undefined;
}

export class ViewSaved implements IViewSaved {
    gridId?: string | undefined;
    viewName?: string | undefined;
    controllerName?: string | undefined;
    gridPagingData?: string | undefined;
    isDefault?: boolean | undefined;
    pagingTotal?: number | undefined;
    version?: string | undefined;

    constructor(data?: IViewSaved) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.gridId = _data["gridId"];
            this.isDefault = _data["isDefault"];
            this.controllerName = _data["controllerName"];
            this.pagingTotal = _data["pagingTotal"];
            this.version = _data["version"];
            this.viewName = _data["viewName"];
            this.gridPagingData = _data["gridPagingData"];
        }
    }

    static fromJS(data: any): ViewSaved {
        data = typeof data === 'object' ? data : {};
        let result = new ViewSaved();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["gridId"] = this.gridId;
        data["controllerName"] = this.controllerName;
        data["isDefault"] = this.isDefault;
        data["pagingTotal"] = this.pagingTotal;
        data["version"] = this.version;
        data["viewName"] = this.viewName;
        data["gridPagingData"] = this.gridPagingData;

        return data;
    }
}