import { ColumnsSaved } from './ColumnsSaved';

export interface IViewSaved {
    viewName?: string | undefined;
    controllerName?: string | undefined;
    columns?: ColumnsSaved[] | undefined;
    isDefault?: boolean | undefined;
    pagingTotal?: number | undefined;
    version?: string | undefined;
}

export class ViewSaved implements IViewSaved {
    viewName?: string | undefined;
    controllerName?: string | undefined;
    columns?: ColumnsSaved[] | undefined;
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
            this.isDefault = _data["isDefault"];
            this.controllerName = _data["controllerName"];
            this.pagingTotal = _data["pagingTotal"];
            this.version = _data["version"];
            this.viewName = _data["viewName"];
            if (Array.isArray(_data["columns"])) {
                this.columns = [] as any;
                for (let item of _data["columns"])
                    this.columns!.push(ColumnsSaved.fromJS(item));
            }
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
        data["controllerName"] = this.controllerName;
        data["isDefault"] = this.isDefault;
        data["pagingTotal"] = this.pagingTotal;
        data["version"] = this.version;
        data["viewName"] = this.viewName;
        if (Array.isArray(this.columns)) {
            data["columns"] = [];
            for (let item of this.columns)
                data["columns"].push(item.toJSON());
        }
        return data;
    }
}