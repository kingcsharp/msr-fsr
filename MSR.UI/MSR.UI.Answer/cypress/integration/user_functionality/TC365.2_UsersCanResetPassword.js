describe('User Functionality', () => {
    it('TC365.2_UsersCanResetPassword', () => {

        cy.server()

        cy.route({
            method: 'POST',      // Route all GET requests
            url: '/v1/Account/forgotpassword',    // that have a URL that matches '/users/*'
            response: []        // and force the response to be: []
          })

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=forgotpassword-link]').click()

        cy.get('[data-cy=username-input]').type('admin').should('have.value','admin')

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=alert-message] > div').contains(' Reset password email has been sent. ')

    })
})