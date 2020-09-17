import { ColumnsSaved } from './ColumnsSaved';

export interface IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    storageId?: string | undefined;
    version?: string | undefined;
}
export class GridSaved implements IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    version?: string | undefined;
    storageId?: string | undefined;

    constructor(data?: IGridSaved) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): GridSaved {
        data = typeof data === 'object' ? data : {};
        let result = new GridSaved();
        result.init(data);
        return result;
    }

    init(_data?: any) {
        if (_data) {
            this.storageId = _data['storageId'];
            this.version = _data['version'];
            if (Array.isArray(_data["columnsSaved"])) {
                this.columnsSaved = [] as any;
                for (let item of _data["columnsSaved"])
                    this.columnsSaved!.push(ColumnsSaved.fromJS(item));
            }
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['storageId'] = this.storageId;
        data['version'] = this.version;
        if (Array.isArray(this.columnsSaved)) {
            data["columnsSaved"] = [];
            for (let item of this.columnsSaved)
                data["columnsSaved"].push(item.toJSON());
        }
        return data;
    }
}

