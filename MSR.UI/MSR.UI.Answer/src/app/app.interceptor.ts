import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { catchError, map } from 'rxjs/operators';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse } from '@angular/common/http';
import { AppConfig } from './app.config';


@Injectable()
export class AppInterceptor implements HttpInterceptor {
  config;

  constructor(
    appConfig: AppConfig,
    private toastr: ToastrService
  ) {
    this.config = appConfig.getConfig();
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({ url: this.config.baseURLApi + req.url });

    const token: string = localStorage.getItem('token');
    if (token) {
      req = req.clone({
        headers: req.headers.set('Authorization', 'Bearer ' + token)
      });

    }

    return next.handle(req).pipe(
      catchError(err => {
        debugger;
        if (err.error) {
          if (err.status === 401) {
            return throwError(err);
          }
          if (err.status === 404) {
            return throwError(err);
          }
          if (err.error.errorMessages.length > 0) {
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
      }),
      map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          if (event.body === undefined || event.body === null) {
            return event;
          }
          if (!event.body.hasErrors && event.body.successMessage) {
            this.toastr.success(event.body.successMessage);
          } else if (event.body.hasErrors && event.body.errorMessages.length > 0) {
            this.toastr.error(event.body.errorMessages[0].message);
          }
        }
        return event;
      }));

    // return next.handle(req);
  }
}
