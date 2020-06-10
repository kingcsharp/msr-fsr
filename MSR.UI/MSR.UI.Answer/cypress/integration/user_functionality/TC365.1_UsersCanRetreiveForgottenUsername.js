describe('User Functionality', () => {
    it('TC365.1_UsersCanRetreiveForgottenUsername', () => {

        cy.server()

        cy.route({
            method: 'POST',      // Route all GET requests
            url: '/v1/Account/forgotusername',    // that have a URL that matches '/users/*'
            response: []        // and force the response to be: []
          })

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=forgotusername-link]').click()

        cy.get('[data-cy=email-input]').type('admin-msrfsr@cmhworks.com').should('have.value','admin-msrfsr@cmhworks.com')

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=alert-message] > div').contains(' An email has been sent with your information. ')

    })
})