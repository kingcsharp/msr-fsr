describe('Workflow Group Functionality', () => {
    it('TC307_PendingApprovalGrid', () => {

        var adminUsername = Cypress.env('admin-username');
        var adminPassword = Cypress.env('admin-password');
        var maxTimeout = parseInt(Cypress.env('admin-password'));

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {
            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')
        })
        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)

        cy.get('[data-cy=submit-button]').click()

        var menuBaseItemName = "Workflow";
        var menuChildItemName = "Pending Approvals";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
                cy.wrap(elem.parent()).click();
            }
        });

        cy.get('#Workflow li a span', { timeout: 2000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
                cy.wrap(elem).click();
            }
        });

        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");

        cy.get('[data-cy=page-title]').contains('Pending Approvals').click();

        cy.get('[data-cy=pendingapproval-header]').contains('Id').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Activity Type').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Name').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Comments').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Workflow Name').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Workflow Group').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Initiatior').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Status').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Created On').click();
        cy.get('[data-cy=pendingapproval-header]').contains('Created By').click();

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
