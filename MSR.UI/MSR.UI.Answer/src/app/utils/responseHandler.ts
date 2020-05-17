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
                ctrl.errorHandler(err);
                if (errorFn !== undefined) {
                    errorFn(err);
                }
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

    private errorHandler(err: any) {
        if (err.error) {
            if (err.status === 401) {
                return throwError(err);
            }
            if (err.status === 404) {
                return throwError(err);
            }
            if (err.error.errorMessages && err.error.errorMessages.length > 0) {
                this.toastr.error(err.error.errorMessages[0].message);
            } else {
                this.toastr.error(err.statusText);
            }

            return throwError(err.error);
        }
        if (err.error === 'Invalid token.') {
            this.toastr.error('Invalid token.');
            return throwError(err);
        }
    }

}