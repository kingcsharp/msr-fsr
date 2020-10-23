import { ColumnsSaved } from './ColumnsSaved';

export interface IViewSaved {
    gridId?: string | undefined;
    viewName?: string | undefined;
    gridPagingData?: string | undefined;
    isDefault?: boolean | undefined;
    pagingTotal?: number | undefined;
    version?: string | undefined;
    columns?: ColumnsSaved[] | undefined;
}

export class ViewSaved implements IViewSaved {
    gridId?: string | undefined;
    viewName?: string | undefined;
    gridPagingData?: string | undefined;
    isDefault?: boolean | undefined;
    pagingTotal?: number | undefined;
    version?: string | undefined;
    columns?: ColumnsSaved[] | undefined;

    constructor(data?: IViewSaved) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ViewSaved {
        data = typeof data === 'object' ? data : {};
        let result = new ViewSaved();
        result.init(data);
        return result;
    }

    init(_data?: any) {
        if (_data) {
            this.gridId = _data['gridId'];
            this.isDefault = _data['isDefault'];
            this.pagingTotal = _data['pagingTotal'];
            this.version = _data['version'];
            this.viewName = _data['viewName'];
            this.gridPagingData = _data['gridPagingData'];
            if (Array.isArray(_data['columns'])) {
                this.columns = [] as any;
                for (let item of _data['columns']) {
                    if (this.columns !== undefined) {
                        this.columns.push(ColumnsSaved.fromJS(item));
                    }
                }
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['gridId'] = this.gridId;
        data['isDefault'] = this.isDefault;
        data['pagingTotal'] = this.pagingTotal;
        data['version'] = this.version;
        data['viewName'] = this.viewName;
        data['gridPagingData'] = this.gridPagingData;
        if (Array.isArray(this.columns)) {
            data['columns'] = [];
            for (let item of this.columns) {
                data['columns'].push(item.toJSON());
            }
        }

        return data;
    }
}
