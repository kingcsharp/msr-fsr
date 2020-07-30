describe('Help Functionality', () => {
    it('TC295_ANewHelpPageCanBeAdded', () => {
        
        cy.server()

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')
        var maxTimeout = parseInt(Cypress.env('maxTimeout'));

        cy.visit('/')

        cy.url().should('include', '/login')


        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        var menuBaseItemName = "Help";
        var menuChildItemName = "Help Pages";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
                cy.wrap(elem.parent()).click();
            }
        });

        cy.get('#Help li a span', { timeout: 2000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
                cy.wrap(elem).click();
            }
        });

        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");

        cy.get('.page-title').contains(menuChildItemName).click();

        cy.get('[data-cy=add-button]').click()

        cy.get('[data-cy=page-title]').contains('Create Help Page')

    })
})