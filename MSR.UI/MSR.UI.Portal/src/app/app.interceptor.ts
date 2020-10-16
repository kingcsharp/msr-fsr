import { Injectable } from '@angular/core';
import { Observable, throwError } from 'rxjs';
import { ToastrService } from 'ngx-toastr';
import { catchError, map } from 'rxjs/operators';
import { HttpEvent, HttpInterceptor, HttpHandler, HttpRequest, HttpResponse, HttpHeaders } from '@angular/common/http';
import { AppConfig } from './app.config';
import { Globals } from './models/lib/globals';
import { LoginService } from './pages/login/login.service';
import { Router } from '@angular/router';


@Injectable()
export class AppInterceptor implements HttpInterceptor {
  config;
  requests: number = 0;
  constructor(
    appConfig: AppConfig, private toastr: ToastrService, private globals: Globals
    , private loginService: LoginService, private router: Router) {
    this.config = appConfig.getConfig();
  }

  turnOffLoader() {
    if (this.requests === 0) {
      this.globals.showLoader(false);
    }
  }

  nextSuccessHandler(blob) {
    let reader = new FileReader();
    reader.onload = event => {
      const value = JSON.parse(event.target.result.toString());
      if (value === undefined || value === null) {
        return;
      }
      if (!value.hasErrors && value.successMessage) {
        this.toastr.success(value.successMessage);
      } else if (value.hasErrors && value.errorMessages.length > 0) {
        this.toastr.error(value.errorMessages[0].message);
      }
    };
    reader.readAsText(blob);
  }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    req = req.clone({ url: this.config.baseURLApi + req.url });
    this.requests++;
    const method = req.method;
    const token: string = localStorage.getItem('token');

    if (this.loginService.isAuthenticated() !== undefined && !this.loginService.isAuthenticated()) {
      this.toastr.error("Your token has expired, please log in again.");
      this.router.navigate(['/login']);
      return throwError(undefined);
    }

    req = req.clone({
      headers: req.headers.set('Authorization', 'Bearer ' + token)
    });

    return next.handle(req).pipe(
      catchError(err => {
        this.requests--;
        this.turnOffLoader();
        if (err.error) {
          let reader = new FileReader();
          reader.onload = event => {
            if (event.target.result === '') {
              this.toastr.error('Internal Server Error, please try again later.');
              return throwError(undefined);
            }
            const errorParsed = JSON.parse(event.target.result.toString());
            if (err.status === 401) {
              this.toastr.error('You are not authorized to do this action.');
              return throwError(err.error);
            }
            if (errorParsed.errorMessages && errorParsed.errorMessages.length > 0) {
              this.toastr.error(errorParsed.errorMessages[0].message);
            } else {
              this.toastr.error(err.statusText);
            }

            return throwError(err.error);
          };
          if (err.statusText === 'Unknown Error') {
            this.toastr.error('Internal Server Error, please try again later.');
            return throwError(undefined);
          } else {
            reader.readAsText(err.error);
            return throwError(err.error);
          }
        }
        if (err.error === 'Invalid token.') {
          this.toastr.error('Invalid token.');
          return throwError(err);
        }
      }), map((event: HttpEvent<any>) => {
        if (event instanceof HttpResponse) {
          if (method === 'PATCH' || method === 'POST' || method === 'DELETE') {
            this.nextSuccessHandler(event.body);
          }
          this.requests--;
          this.turnOffLoader();
        }
        return event;
      }));
  }

  validateToken(token) {
    console.log(token);

  }
}
