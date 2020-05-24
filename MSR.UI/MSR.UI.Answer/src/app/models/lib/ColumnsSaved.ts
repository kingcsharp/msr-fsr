export interface IColumnsSaved {
    sortAsc?: boolean | undefined;
    sortValues: Array<string> | undefined;
}
export class ColumnsSaved implements IColumnsSaved {
    sortAsc?: boolean | undefined;
    sortValues: Array<string> | undefined;

    constructor(data?: IColumnsSaved) {
        if (data) {
            for (var property in data) {
                if (data.hasOwnProperty(property))
                    (<any>this)[property] = (<any>data)[property];
            }
        }
    }

    init(_data?: any) {
        if (_data) {
            this.sortAsc = _data["sortAsc"];
            this.sortValues = _data["sortValues"];
        }
    }

    static fromJS(data: any): ColumnsSaved {
        data = typeof data === 'object' ? data : {};
        let result = new ColumnsSaved();
        result.init(data);
        return result;
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data["sortAsc"] = this.sortAsc;
        data["sortValues"] = this.sortValues;
        return data;
    }
}