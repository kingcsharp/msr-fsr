export interface IAllowedActions {
    canCreate: boolean | undefined;
    canActivate: boolean | undefined;
    canDelete: boolean | undefined;
    canEdit: boolean | undefined;
    canRead: boolean | undefined;
}

export class AllowedActions {
    canCreate: boolean | undefined;
    canActivate: boolean | undefined;
    canDelete: boolean | undefined;
    canEdit: boolean | undefined;
    canRead: boolean | undefined;

    constructor(data?: IAllowedActions) {
        if (data) {
            for (let property in data) {
                if (data.hasOwnProperty(property)) {
                    (<any>this)[property] = (<any>data)[property];
                }
            }
        }
    }

    static fromJS(data: any): AllowedActions {
        data = typeof data === 'object' ? data : {};
        let result = new AllowedActions();
        result.init(data);
        return result;
    }

    init(_data?: any) {
        if (_data) {
            this.canCreate = _data['canCreate'];
            this.canActivate = _data['canActivate'];
            this.canDelete = _data['canDelete'];
            this.canEdit = _data['canEdit'];
            this.canRead = _data['canRead'];
        }
    }

    toJSON(data?: any) {
        data = typeof data === 'object' ? data : {};
        data['canCreate'] = this.canCreate;
        data['canActivate'] = this.canActivate;
        data['canDelete'] = this.canDelete;
        data['canEdit'] = this.canEdit;
        data['canRead'] = this.canRead;
        return data;
    }
}