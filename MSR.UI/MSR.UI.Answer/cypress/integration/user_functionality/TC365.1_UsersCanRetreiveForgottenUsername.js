describe('User Functionality', () => {
    it('TC365.1_UsersCanRetreiveForgottenUsername', () => {

        cy.server()

        cy.route({
            method: 'POST',
            url: '/v1/Account/forgotusername',
            response: []
          })

        var adminEmail = Cypress.env('admin-email')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=forgotusername-link]').click()

        cy.get('[data-cy=email-input]').type(adminEmail).should('have.value',adminEmail)

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=alert-message] > div').contains(' An email has been sent with your information. ')

    })
})