import * as moment from "moment";
import { LazyLoadEvent } from "primeng/api";
import { Exception, Filter, Sort } from "../../../app/services/api.client.generated";
import { environment as env } from '../../../environments/environment';
import { EnumColumnType } from "../enums/EnumColumnType";
import { ColumnsSaved } from "./ColumnsSaved";

export function emptyArray(array) {
    let length = array.length;
    while (length--) {
        array.pop();
    }
}

export function removeDotAndCamelCaseFromObj(elem: any) {
    let objToReturn = {};
    Object.keys(elem).forEach((key) => {
        const keysplitted = key.split('.');
        //capitalizeFirstLetter(string: any)
        const keysCapitalized = keysplitted.map((x: string, index: number) => index < 1 ? lowerCaseFirstLetter(x) : capitalizeFirstLetter(x)).join('');
        // const keyVal = keysplitted.length > 1 ? 1 : 0;
        objToReturn[keysCapitalized] = elem[key];
    });
    return objToReturn;
}

export function removeDotAndCamelCaseFromStr(elem: string) {
    if (elem === undefined) {
        return null;
    }
    const keysplitted = elem.split('.');
    const keysCapitalized = keysplitted.map((x: string, index: number) => index < 1 ? lowerCaseFirstLetter(x) : capitalizeFirstLetter(x)).join('');

    return keysCapitalized;
}

export function replaceArrayItems(arrayToBeReplaced, newArray) {
    emptyArray(arrayToBeReplaced);
    arrayToBeReplaced.push(...newArray);
}

export function pushIfNotExists(item, array, id) {
    const index = array.findIndex(x => x[id] === item[id]);
    if (index === -1) {
        array.push(item);
    }
}

export function copyObj(objToCopy: any): any {
    return Object.assign({}, objToCopy);
}

export function formatBytes(bytes, decimals = 2) {
    if (bytes === 0) { return '0 Bytes'; }

    const k = 1024;
    let dm = decimals < 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    const i = Math.floor(Math.log(bytes) / Math.log(k));

    if (sizes.indexOf(sizes[i]) < 2) {
        dm = 0;
    }

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}

export function capitalizeFirstLetter(string: any) {
    if (string === undefined || string === null) {
        return null;
    }
    return string.charAt(0).toUpperCase() + string.slice(1);
}

export function lowerCaseFirstLetter(string: any) {
    if (string === undefined || string === null) {
        return null;
    }
    return string.charAt(0).toLowerCase() + string.slice(1);
}

export function getArguments(func) {
    const ARROW = true;
    const FUNC_ARGS = ARROW ? /^(function)?\s*[^\(]*\(\s*([^\)]*)\)/m : /^(function)\s*[^\(]*\(\s*([^\)]*)\)/m;
    const FUNC_ARG_SPLIT = /,/;
    const FUNC_ARG = /^\s*(_?)(.+?)\1\s*$/;
    const STRIP_COMMENTS = /((\/\/.*$)|(\/\*[\s\S]*?\*\/))/mg;

    return ((func || '').toString().replace(STRIP_COMMENTS, '').match(FUNC_ARGS) || ['', '', ''])[2]
        .split(FUNC_ARG_SPLIT)
        .map(function (arg) {
            return arg.replace(FUNC_ARG, function (all, underscore, name) {
                return name.split('=')[0].trim();
            });
        })
        .filter(String);
}

export function callFunctionWithFilters(service, func, event: LazyLoadEvent, globalDic, extraParams?: any) {
    const sortField = event.sortField === null ? undefined : event.sortField;
    let filterEvObj: any = {
        term: capitalizeFirstLetter(removeDotAndCamelCaseFromStr(sortField)),
        pageNumber: event.first / event.rows,
        pageSize: event.rows,
        sortAscending: event.sortOrder === 1
    }
    Object.assign(filterEvObj, event);
    Object.assign(filterEvObj, extraParams);

    //to generate dic.
    // const args = getArguments(func);
    // globalDic[func.toString().split('(')[0]] = args;

    const args = globalDic[func.toString().split('(')[0]];

    const argsToCallFn = [];
    filterEvObj.filters = removeDotAndCamelCaseFromObj(filterEvObj.filters);
    args.forEach((arg: string) => {
        const filterObj = filterEvObj.filters[arg];
        if (filterObj !== undefined) {
            if (Array.isArray(filterObj.value)) {
                if (typeof (filterObj.value[0]) === "boolean") {
                    if (filterObj.value.length === 1) {
                        argsToCallFn.push(filterObj.value[0]);
                    } else {
                        argsToCallFn.push(null);
                    }
                } else {
                    if (typeof (filterObj.value[0]) === "object") {
                        const parentRoles = filterObj.value.map(x => x.id);
                        argsToCallFn.push(parentRoles);
                    } else {
                        argsToCallFn.push(filterObj.value);
                    }
                }
            } else {
                argsToCallFn.push(filterObj.value);
            }
        } else if (filterEvObj[arg] !== undefined) {
            argsToCallFn.push(filterEvObj[arg]);
        } else {
            argsToCallFn.push(null);
        }
    });
    argsToCallFn[argsToCallFn.length - 1] = env.apiVersion;
    return func.apply(service, argsToCallFn);
}

export function callFunctionWithFiltersViews(service, func, extraParams: any, columnsSaved: ColumnsSaved[], event: LazyLoadEvent, globalDic: any) {
    let filterEvObj: any = {
        pageNumber: event.first / event.rows,
        pageSize: event.rows,
        sort: new Array<Sort>()
    }

    filterEvObj.sort.push(
        new Sort({
            dir: event.sortOrder === 1 ? 'asc' : 'desc',
            field: capitalizeFirstLetter(removeDotAndCamelCaseFromStr(event.sortField ? event.sortField : columnsSaved[0].id))
        })
    );



    Object.assign(filterEvObj, event);
    filterEvObj.filters = new Array<Filter>();

    const uiFilters = removeDotAndCamelCaseFromObj(event.filters);
    Object.keys(uiFilters).forEach((filter: any) => {
        const filterObj = uiFilters[filter];
        filterEvObj.filters.push(new Filter({
            field: filter,
            value: getValueByType(filterObj.value, columnsSaved.find(x => x.id === filter)),
            operator: getOperatorByColumn(filter, columnsSaved),
            logic: 'and'
        }))
    });

    Object.assign(filterEvObj, extraParams);

    // const args = getArguments(func);
    const args = globalDic[func.toString().split('(')[0]];

    const argsToCallFn = [];
    args.forEach((arg: string) => {
        if (filterEvObj[arg] !== undefined) {
            argsToCallFn.push(filterEvObj[arg]);
        } else {
            argsToCallFn.push(null);
        }
    });
    argsToCallFn[argsToCallFn.length - 1] = env.apiVersion;
    return func.apply(service, argsToCallFn);
}

export function getValueByType(filterValue: any, columnsSaved: ColumnsSaved) {
    switch (columnsSaved.type) {
        case EnumColumnType.Date:
            return moment(filterValue).format('yyyy-MM-DD');
        case EnumColumnType.StringArray:
            return JSON.stringify(filterValue).toString();

        default:
            return filterValue;
    }
}

export function getOperatorByColumn(filtername: any, columnsSaved: ColumnsSaved[]) {
    /*possible operators":
       // Date: eq, lte, gte
       // Number: like
       // String: contains, StartsWith, EndsWith
       // Money: eq, lte, gte
       // Boolean: eq
       // stringArray: contains
       */
    const col = columnsSaved.find(x => x.id === filtername);
    switch (col.type) {
        case EnumColumnType.Boolean:
            return 'eq';
        case EnumColumnType.Date:
            return 'gte';
        case EnumColumnType.Number:
            return 'like';
        case EnumColumnType.String:
            return 'contains';
        case EnumColumnType.Money:
            return 'eq';
        case EnumColumnType.StringArray:
            return 'list';
        default:
            throw new Exception({ message: 'invalid col type' });
    }
}


