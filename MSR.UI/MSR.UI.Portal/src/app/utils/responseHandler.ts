function responseHandler(nextFn, errorFn?) {
    let subscriberA = {
        next(value: any) {
            if (nextFn !== undefined) {
                nextFn(value);
            }
        },
        error(err: any) {
            if (err === undefined) {
                if (errorFn !== undefined) {
                    errorFn({});
                    return;
                }
            }
            let reader = new FileReader();
            reader.onload = event => {
                if (event.target.result === '') {
                    return;
                }
                const errorParsed = JSON.parse(event.target.result.toString());
                if (errorFn !== undefined) {
                    errorFn(errorParsed);
                }
            };
            if (err !== undefined) {
                reader.readAsText(err);
            }
        },
        complete() {
            // console.log('Subscriber A - complete');
        }
    };
    return subscriberA;
}

export {
    responseHandler
};
