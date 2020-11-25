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

export function formatBytes(bytes, decimals = 2) {
    if (bytes === 0) return '0 Bytes';

    const k = 1024;
    let dm = decimals < 0 ? 0 : decimals;
    const sizes = ['Bytes', 'KB', 'MB', 'GB', 'TB', 'PB', 'EB', 'ZB', 'YB'];

    const i = Math.floor(Math.log(bytes) / Math.log(k));

    if (sizes.indexOf(sizes[i]) < 2) {
        dm = 0;
    }

    return parseFloat((bytes / Math.pow(k, i)).toFixed(dm)) + ' ' + sizes[i];
}


