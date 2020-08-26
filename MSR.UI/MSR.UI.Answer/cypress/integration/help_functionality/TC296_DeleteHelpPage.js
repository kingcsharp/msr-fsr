describe('Help Functionality', () => {
    it('TC296_DeleteHelpPage', () => {

        cy.server()

        cy.route({
            method: 'DELETE',
            url: '/v1/Help/**',
            response: { "successMessage": "HelpPage Deleted successfully", "errorMessages": [], "id": 0, "hasErrors": false, "hasValidationErrors": false }
        })

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')
        var maxTimeout = parseInt(Cypress.env('maxTimeout'));

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {

            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')

        })

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', { timeout: 20000 }).url().should('include', '/people/people')

        cy.navigateToPage('Help','Help Pages')

        const idToDelete;
        cy.get('[data-cy=helppage-id]').first().invoke('text').then((text) => {
            idToDelete = text;
   
        });

        cy.get('[data-cy=delete-button]').first().click();


        cy.get('[data-cy=confirmdelete-button]').click();
        
        cy.get('[aria-live="polite"]').should("be.visible");
        cy.get('[aria-live="polite"]').should("not.be.visible");

        cy.get('[data-cy=helppage-id]').first().invoke('text').then((text) => {
            expect(text).not.to.include(idToDelete)
        });

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})