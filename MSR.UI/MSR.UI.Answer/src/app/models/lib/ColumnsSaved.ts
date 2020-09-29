import { EnumColumnType } from '../enums/EnumColumnType';

export interface IColumnsSaved {
    id?: string | undefined;
    label?: string | undefined;
    visible?: boolean | undefined;
    type?: EnumColumnType | undefined;
    dropdownHeader?: boolean | undefined;
    multipleValues?: boolean | undefined;
    formatting?: string | undefined;
}
export class ColumnsSaved implements IColumnsSaved {
    id?: string | undefined;
    label?: string | undefined;
    visible?: boolean | undefined;
    type?: EnumColumnType | undefined;
    dropdownHeader?: boolean | undefined;
    multipleValues?: boolean | undefined;
    //used only for date values at the time but can be extended if wanted
    formatting?: string | undefined;

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
            this.type = _data['type'];
            this.dropdownHeader = _data['dropdownHeader'];
            this.multipleValues = _data['multipleValues'];
            this.formatting = _data['formatting'];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['id'] = this.id;
        data['label'] = this.label;
        data['visible'] = this.visible;
        data['type'] = this.type;
        data['dropdownHeader'] = this.dropdownHeader;
        data['multipleValues'] = this.multipleValues;
        data['formatting'] = this.formatting;

        return data;
    }
}

