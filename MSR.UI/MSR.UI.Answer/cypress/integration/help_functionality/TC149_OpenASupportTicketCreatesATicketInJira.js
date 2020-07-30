describe('Help Functionality', () => {
    it('TC149_OpenASupportTicketCreatesATicketInJira', () => {

        cy.server()

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {

            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')

        })

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called") 
    })
})