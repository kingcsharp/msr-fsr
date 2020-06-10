describe('User Functionality', () => {
    it('TC365.2_UsersCanResetPassword', () => {

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=forgotpassword-link]').click()

        cy.get('[data-cy=username-input]').type('rlara').should('have.value','rlara')

        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=alert-message] > div').contains(' Reset password email has been sent. ')

    })
})