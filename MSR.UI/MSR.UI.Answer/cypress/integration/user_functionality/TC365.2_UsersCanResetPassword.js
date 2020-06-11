describe('User Functionality', () => {
    it('TC365.2_UsersCanResetPassword', () => {

        cy.server()

        cy.route({
            method: 'POST', 
            url: '/v1/Account/forgotpassword',
            response: []
        })

        var adminUsername = Cypress.env('admin-username')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {

            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')

        })
        
        cy.get('[data-cy=forgotpassword-link]').click()

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=alert-message] > div').contains(' Reset password email has been sent. ')

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called") 
    })
})