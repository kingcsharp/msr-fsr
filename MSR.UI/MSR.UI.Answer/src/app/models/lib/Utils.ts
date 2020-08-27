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



