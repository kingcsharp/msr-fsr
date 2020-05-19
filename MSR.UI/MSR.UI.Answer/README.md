# Light Blue Angular Dashboard - Ngx Admin Template (4.3.0 Full version) with Angular 8.0 Final Release support
Documentation:https://demo.flatlogic.com/sing-app/documentation/angular_components.html#f_validation
Demo: https://flatlogic.com/templates/light-blue-angular/demo
##Install yarn
##https://classic.yarnpkg.com/en/docs/install#windows-stable

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 1.7.3.

**For upgrade instruction please refer to [https://update.angular.io/](https://update.angular.io/).**

## Install dependencies

Run `yarn install`.
**Do NOT use NPM!**

## Development server
Backend server needs to be up before we run this commands.
RUN `npm run build` so it compiles the solution.
Run `npm run start` for a dev server. Navigate to `http://localhost:3000/`. The app will automatically reload if you change any of 
the source files.

#package.json has all the scripts you can run or add to be compiled before actions.
eg. `npm run buildstage` this will build the solution with the environment file environment.stage



## If you use app with backend support, please use
```
yarn run start:backend
```

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `yarn build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

Grid:
https://www.primefaces.org/primeng/showcase/#/filterutils

## DEV HELP

# GLOBALS
here is where we handle the privileges of the user with hasPrivilege
in every view we'll need to handle the specific privileges in the following way:

this method will tell globals what view it needs to be linked to the privileges the user has
hasPrivilege(privName) {
    return this.globals.hasPrivilege('users', privName);
}
then in the init of the component we need to set this privileged like:
this.canAddUsers = this.hasPrivilege(this.privileges.CanCreate);
this.canEditUsers = this.hasPrivilege(this.privileges.CanEdit);

setting that up in the view with <button *ngIf="canAddUsers" we'll know if the user can or can not see a button.


## Environments:
environments defined in the folder with .dev/.prod/stage or the localhost wich is environment.ts

##  Services:
imports needed:
import { AccountService, ForgotPasswordRequest, ForgotUserNameRequest } from '../../services/api.client.generated';
import { environment as env } from '../../../environments/environment';
import { take } from 'rxjs/operators';
import { responseHandler } from '../../utils/responseHandler';

responseHandler will act as handler to show any success message or case we'll want to automatically handle.
## app.interceptor.ts
will handle all main loaders shown in the site, and handle all errors to show a toaster

## loader: to show just call: this.globals.showLoader(false);
## loader stop will be automatically called within the interceptor.


this.accountService.forgotpassword(new ForgotPasswordRequest({ userName: this.username }), env.apiVersion)
.pipe(take(1))
.subscribe(responseHandler((result) => {
//use result as we wish

}, (err) => {

//use error as we wish

}));
