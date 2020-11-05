export function emptyArray(array) {
    let length = array.length;
    while (length--) {
        array.pop();
    }
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

export function deepCopy(data: any) {
    //WARNING it does not copy the type of the object.
    let node;
    if (Array.isArray(data)) {
        node = data.length > 0 ? data.slice(0) : [];
        node.forEach((e, i) => {
            if (
                (typeof e === 'object' && e !== {}) ||
                (Array.isArray(e) && e.length > 0)
            ) {
                node[i] = deepCopy(e);
            }
        });
    } else if (data && typeof data === 'object') {
        node = data instanceof Date ? data : Object.assign({}, data);
        Object.keys(node).forEach((key) => {
            if (
                (typeof node[key] === 'object' && node[key] !== {}) ||
                (Array.isArray(node[key]) && node[key].length > 0)
            ) {
                node[key] = deepCopy(node[key]);
            }
        });
    } else {
        node = data;
    }
    return node;
}


