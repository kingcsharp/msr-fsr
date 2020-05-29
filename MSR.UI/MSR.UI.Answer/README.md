# MSR Answer 3.0

## Setting-up environment for development

**WARNING: This project uses yarn, do not use NPM for any commnands during development**

After cloning the project, run the following command to install required packages

    yarn install

Run the following command to compile the solution

    yarn build

The following options allow the application to run for different purposes

Option 1) Run with dev server as the backend

    yarn start

Option 2) Run with local backend

    yarn run start:backend

Option 3) Run with Cypress Test Runner (Note: runs with local backend)

    yarn run cypress:test:local

Option 4) Run with Cypress Test Runner (Note: runs with local backend)

    yarn run cypress:test:dev

Navigate to [http://localhost:3000/](http://localhost:3000/). The app will automatically reload if you change any of the source files.

**Note: If Cypress is running, it will automatically reload tests**

## Other commands worth noting ##

**Code scaffolding**

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

**Build**

Run `yarn build` to build the project. The build artifacts will be stored in the `dist/` directory. Use the `-prod` flag for a production build.

**Further help with Angular**

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).

**package.json has all the scripts you can run or add to be compiled before actions.**
eg. `npm run buildstage` this will build the solution with the environment file environment.stage

# Must Read Developer Notes #
**Note: All developers should read this**

## GLOBALS

The `hashasPrivilege` method handles the privileges of the user. Every view will need to handle the specific privileges in the following way:

This method will tell globals what views can be accessed based on the user's privileges

    hasPrivilege(privName) {
        return this.globals.hasPrivilege('users', privName);
    }

Then in the initialization of the component we need to set the privilege as follows:

    this.canAddUsers = this.hasPrivilege(this.privileges.CanCreate);
    this.canEditUsers = this.hasPrivilege(this.privileges.CanEdit);

Setting that up in the view with `<button *ngIf="canAddUsers".../>` we'll know if the user can or can not see a button.


## Environments:
Environments defined in the folder with .dev/.prod/stage or the localhost which is environment.ts

##  Services:
    imports needed:
    import { AccountService, ForgotPasswordRequest, ForgotUserNameRequest } from '../../services/api.client.generated';
    import { environment as env } from '../../../environments/environment';
    import { take } from 'rxjs/operators';
    import { responseHandler } from '../../utils/responseHandler';

`responseHandler` will act as handler to show any success message or cases we will want to automatically handle.

`app.interceptor.ts` will handle all main loaders shown in the site, and handle all errors to show a toaster

## How to show the loader

    this.globals.showLoader(false);

**Note: Loader will stop automatically via the interceptor.**

Example

    this.accountService.forgotpassword(new ForgotPasswordRequest({ userName: this.username }), env.apiVersion)
    .pipe(take(1))
    .subscribe(responseHandler((result) => {
        //use result as we wish

    }, (err) => {

        //use error as we wish

    }));


# Third-party documentation #

## Ngx Admin Template ##

Light Blue Angular Dashboard - Ngx Admin Template (4.3.0 Full version) with Angular 8.0 Final Release support
- [Documentation](https://demo.flatlogic.com/sing-app/documentation/angular_components.html)
- [Demo](https://flatlogic.com/templates/light-blue-angular/demo)

## Primefaces Grid ##

- [Documentation](https://www.primefaces.org/primeng/showcase/#/filterutils)
- [Table Grid Component](https://www.primefaces.org/primeng/showcase/#/table)

## Cypress

- [Documentation](https://docs.cypress.io/guides/overview/why-cypress.html)
- [Writing your first test](https://docs.cypress.io/guides/getting-started/writing-your-first-test.html#Add-a-test-file)
- [Core Concepts](https://docs.cypress.io/guides/core-concepts/introduction-to-cypress.html#Cypress-Can-Be-Simple-Sometimes)

## NSwag

- [Repository and Documentation](https://github.com/RicoSuter/NSwag)
- [Nswag Studio to generate the nswag config file](https://github.com/RicoSuter/NSwag/wiki/NSwagStudio)

## Bootstrap
[Documentation](https://ng-bootstrap.github.io/#/components/tooltip/examples)
[Documentation](https://getbootstrap.com/docs/4.0/utilities/colors/)