describe('User Functionality', () => {
    it('TC365_UsersCanLoginToAnswer', () => {

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=username-input]').type('admin').should('have.value', 'admin')
        cy.get('[data-cy=password-input]').type('cmhworks.2020').should('have.value', 'cmhworks.2020')
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        cy.get('[data-cy=logout-link]').click()

        cy.get('[data-cy=username-input]').url().should('include', '/login')

    })
})