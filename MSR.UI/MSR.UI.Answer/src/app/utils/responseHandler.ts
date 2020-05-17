
import { Injectable } from '@angular/core';
import { ToastrService } from 'ngx-toastr';
import { throwError } from 'rxjs';

@Injectable()
export class ResponseHandler {

    constructor(private toastr: ToastrService) {

    }

    public subscribe(nextFn, errorFn) {
        const ctrl = this;
        let subscriberA = {
            next(value: any) {
                ctrl.nexthandler(value);
                if (errorFn !== undefined) {
                    nextFn(value);
                }
            },
            error(err: any) {
                if(err===undefined){
                    if (errorFn !== undefined) {
                        errorFn({});
                        return;
                    }
                }
                let reader = new FileReader();
                reader.onload = event => {
                    const errorParsed = JSON.parse(event.target.result.toString());
                    if (errorFn !== undefined) {
                        errorFn(errorParsed);
                    }
                };
                reader.readAsText(err);
            },
            complete() {
                console.log('Subscriber A - complete');
            }
        };
        return subscriberA;
    }

    private nexthandler(value: any) {
        if (value === undefined || value === null) {
            return;
        }

        if (!value.hasErrors && value.successMessage) {
            this.toastr.success(value.successMessage);
        } else if (value.hasErrors && value.errorMessages.length > 0) {
            this.toastr.error(value.errorMessages[0].message);
        }
    }
}