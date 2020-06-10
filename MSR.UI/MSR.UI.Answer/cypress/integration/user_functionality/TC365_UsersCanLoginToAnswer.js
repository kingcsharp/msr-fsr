describe('User Functionality', () => {
    it('TC365_UsersCanLoginToAnswer', () => {

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        cy.get('[data-cy=logout-link]').click()

        cy.get('[data-cy=username-input]').url().should('include', '/login')

    })
})