export interface IColumnsSaved {
    id?: string | undefined;
    label?: string | undefined;
    visible?: boolean | undefined;
}
export class ColumnsSaved implements IColumnsSaved {
    id?: string | undefined;
    label?: string | undefined;
    visible?: boolean | undefined;

    constructor(data?: IColumnsSaved) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): ColumnsSaved {
        data = typeof data === 'object' ? data : {};
        let result = new ColumnsSaved();
        result.init(data);
        return result;
    }

    init(_data?: any) {
        if (_data) {
            this.id = _data['id'];
            this.label = _data['label'];
            this.visible = _data['visible'];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['label'] = this.label;
        data['visible'] = this.visible;
        return data;
    }
}

