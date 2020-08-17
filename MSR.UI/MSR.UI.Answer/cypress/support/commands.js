// ***********************************************
// This example commands.js shows you how to
// create various custom commands and overwrite
// existing commands.
//
// For more comprehensive examples of custom
// commands please read more here:
// https://on.cypress.io/custom-commands
// ***********************************************
//
//
// -- This is a parent command --
// Cypress.Commands.add("login", (email, password) => { ... })
//
//
// -- This is a child command --
// Cypress.Commands.add("drag", { prevSubject: 'element'}, (subject, options) => { ... })
//
//
// -- This is a dual command --
// Cypress.Commands.add("dismiss", { prevSubject: 'optional'}, (subject, options) => { ... })
//
//
// -- This will overwrite an existing command --
// Cypress.Commands.overwrite("visit", (originalFn, url, options) => { ... })
// -- Login Command --
Cypress.Commands.add("login", (role) => {

    let adminUsername;
    let adminPassword;
    if (role === 'admin') {
        adminUsername = Cypress.env('admin-username')
        adminPassword = Cypress.env('admin-password')
    }

    cy.visit('/')

    cy.url().should('include', '/login')

    cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
    cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)

    cy.get('[data-cy=submit-button]').click()

    cy.get('[data-cy=logout-link]', { timeout: 20000 })

})

Cypress.Commands.add("logout", () => {

    cy.get('[data-cy=logout-link]', {timeout: 20000}).click();

    cy.get('[data-cy=username-input]').url().should('include', '/login')

})

Cypress.Commands.add("navigateToPage", (menuBaseItemName,menuChildItemName) => {

    var maxTimeout = parseInt(Cypress.env('maxTimeout'));
    cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
        if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
            cy.wrap(elem.parent()).click();
        }
    });

    cy.get('#' + menuBaseItemName + ' li a span', { timeout: 2000 }).each((elem) => {
        if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
            cy.wrap(elem).click();
        }
    });

    cy.get(".ui-blockui-document", { timeout: 20000 }).should("not.be.visible");


})

Cypress.Commands.add("beginWebConsoleTracking", () => {

    cy.window().then((win) => {

        cy.spy(win.console, 'error').as('errorMessage')
        cy.spy(win.console, 'warn').as('warningMessage')

    })

})

Cypress.Commands.add("checkWebConsoleTracking", () => {
    cy.get('@warningMessage').should("not.called")
    cy.get('@errorMessage').should("not.called") 
})