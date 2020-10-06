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
// Technician - technician@cmhworks.com - tech:cmhworks.2020
// Shipping and Receiving - shipping@cmhworks.com - ship:cmhworks.2020
// Process Engineer - process@cmhworks.com - process:cmhworks.2020
// Office Manager - manager-msrfsr@cmhworks.com - manager:cmhworks.2020
// Admin - admin-msrfsr@cmhworks.com - admin:cmhworks.2020
// Maintenance Technician - maintenance@cmhworks.com - maintenance:cmhworks.2020
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

    cy.get('[data-cy=logout-link]', { timeout: 20000 }).click();

    cy.get('[data-cy=username-input]').url().should('include', '/login')

})

Cypress.Commands.add("navigateToPage", (menuBaseItemName, menuChildItemName) => {

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

Cypress.Commands.add("showDropdownTrackableModel", (cyGridName) => {
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').click();
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom');
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated On').click();
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated By').click();
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click();
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created By').click();
    
    cy.get(`[data-cy=${cyGridName}]`).find('.gridDropdown').click();
});

Cypress.Commands.add("multiselectClick", (cyproperty, containsName) => {
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').click();
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').find('input[type=text]:last').clear();
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').find('input[type=text]:last').type(containsName);
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').find('.ui-multiselect-items-wrapper').contains(containsName).click();
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').click();
});

Cypress.Commands.add("multiselectFormClick", (cyproperty, containsName) => {
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').click();
    cy.get(`.ui-multiselect-filter-container`).find('input').type(containsName);
    cy.get(`.ui-multiselect-items-wrapper`).find('.ui-multiselect-item').find('span').contains(containsName).click();
    cy.get(`[data-cy=${cyproperty}]`).find('.ui-multiselect').click();
});

Cypress.Commands.add("dropdownClick", (cyproperty, containsName) => {
    cy.get(`[data-cy=${cyproperty}]`).click();
    cy.get(`input.ui-dropdown-filter`).type(containsName);
    cy.get(`.ui-dropdown-items-wrapper li`).find('span').contains(containsName).click();
    cy.get(".ui-dropdown-items-wrapper", { timeout: 20000 }).should("not.be.visible");
});

Cypress.Commands.add("writeAndValidate", (cyproperty, text) => {
    cy.get(`[data-cy=${cyproperty}]`).clear();
    cy.get(`[data-cy=${cyproperty}]`).type(text).should('have.value', text);
});

Cypress.Commands.add("writeAndValidateDate", (cyproperty, textDate) => {
    //format: "MM/DD/YYYY"
    cy.get(`[data-cy=${cyproperty}]`).find('input').type(textDate).should('have.value', textDate);
    cy.get('.ui-datepicker-current-day').click();
});

Cypress.Commands.add("waitpost", () => {
    cy.get(".ui-blockui-document", { timeout: 5000 }).should("be.visible");
    cy.get(".ui-blockui-document", { timeout: 40000 }).should("not.be.visible");
});

Cypress.Commands.add("writeCommentAndClose", (comment) => {
    cy.get("[data-cy=approval-comment-input]").type(comment);
    cy.get('[data-cy=approval-save-button]').click();
});

