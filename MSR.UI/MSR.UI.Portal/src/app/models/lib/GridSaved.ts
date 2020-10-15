import { ColumnsSaved } from './ColumnsSaved';

export interface IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    storageId?: string | undefined;
    version?: string | undefined;
    visibleColumns?: number | undefined;
    expandRows?: boolean | undefined;
}
export class GridSaved implements IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    version?: string | undefined;
    storageId?: string | undefined;
    visibleColumns?: number | undefined;
    expandRows?: boolean | undefined;

    constructor(data?: IGridSaved) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
            this.visibleColumns = this.columnsSaved.filter(x => x.visible).length;
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
            this.expandRows = _data['expandRows'];
            

            if (Array.isArray(_data['columnsSaved'])) {
                this.columnsSaved = [] as any;
                for (let item of _data['columnsSaved']) {
                    /* tslint:disable */
                    this.columnsSaved!.push(ColumnsSaved.fromJS(item));
                    /* tslint:enable */
                }
            }
            this.visibleColumns = this.columnsSaved.filter(x => x.visible).length;
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['storageId'] = this.storageId;
        data['version'] = this.version;
        data['visibleColumns'] = this.visibleColumns;
        data['expandRows'] = this.expandRows;

        if (Array.isArray(this.columnsSaved)) {
            data['columnsSaved'] = [];
            for (let item of this.columnsSaved) {
                data['columnsSaved'].push(item.toJSON());
            }
        }
        return data;
    }
}

