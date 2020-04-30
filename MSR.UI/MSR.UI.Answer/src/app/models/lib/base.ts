import { isObject, isArray } from 'util';
export abstract class Base<E> {
    public createdBy: number;
    public createdAt: string;
    public modifiedBy: number;
    public modifiedAt: string;
    public isActive: boolean;

    protected initialValue<T>(value: T, def?: T) {
        return value === undefined ? def === undefined ? null : def : value
    }

    serialize() {
        const data = {};
        Object.keys(this).forEach((k) => {

            return (data[k] = this[k]);
        });
        return data;
    }

    serializeJson() {
        const result = JSON.parse(JSON.stringify(this))
        if (this.createdAt != undefined) {
            result.createdAt = this.createdAt
        }
        if (this.modifiedAt != undefined) {
            result.modifiedAt = this.modifiedAt
        }
        return result;
    }
}