describe('User Functionality', () => {
    it('TC365_UsersCanLoginToAnswer', () => {

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=username-input]').type('rlara').should('have.value', 'rlara')
        cy.get('[data-cy=password-input]').type('Password123.').should('have.value', 'Password123.')
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]').url().should('include', '/people/people')

        cy.get('[data-cy=logout-link]').click()

        cy.get('[data-cy=username-input]').url().should('include', '/login')

    })
})