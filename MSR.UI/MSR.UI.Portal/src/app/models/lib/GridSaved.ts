import { ElementRef } from '@angular/core';
import { ColumnsSaved } from './ColumnsSaved';

export interface IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    storageId?: string | undefined;
    version?: string | undefined;
    visibleColumns?: number | undefined;
    expandRows?: boolean | undefined;
    expandRowsTemplate?: ElementRef | undefined;
    showMyViewsFeature?: boolean | undefined;
    paginator?: boolean | undefined;
}
export class GridSaved implements IGridSaved {
    columnsSaved?: ColumnsSaved[] | undefined;
    version?: string | undefined;
    storageId?: string | undefined;
    visibleColumns?: number | undefined;
    expandRows?: boolean | undefined;
    expandRowsTemplate?: ElementRef | undefined;
    showMyViewsFeature?: boolean = true;
    paginator?: boolean = true;
    

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
            this.expandRowsTemplate = _data['expandRowsTemplate'];
            this.showMyViewsFeature = _data['showMyViewsFeature'];
            this.paginator = _data['paginator'];
            

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
        data['expandRowsTemplate'] = this.expandRowsTemplate;
        data['showMyViewsFeature'] = this.showMyViewsFeature;
        data['paginator'] = this.paginator;

        if (Array.isArray(this.columnsSaved)) {
            data['columnsSaved'] = [];
            for (let item of this.columnsSaved) {
                data['columnsSaved'].push(item.toJSON());
            }
        }
        return data;
    }
}

