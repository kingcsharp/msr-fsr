describe('User Functionality', () => {
    it('TC101_AdministratorCanAddUser', () => {

        cy.server()

        cy.route({
            method: 'POST',
            url: '/v1/User',
            response: []
          })

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        cy.get('[data-cy=add-button]').click()

        cy.get('[data-cy=user-header]').contains('Create User')

        cy.get('[data-cy=firstname-input]').type('John').should('have.value','John')

        cy.get('[data-cy=lastname-input]').type('Doe').should('have.value','Doe')

        cy.get('[data-cy=email-input]').type('jdoe@cmhworks.com').should('have.value','jdoe@cmhworks.com')
        
        cy.get('[data-cy=username-input]').type('jdoe').should('have.value','jdoe')
        
        cy.get('[data-cy=title-input]').type('Mr.').should('have.value','Mr.')

        cy.get('[data-cy=phone-input]').type('2143326762').should('have.value','(214) 332-6762')

        cy.get('[data-cy=isactive-checkbox]').click();

        cy.get('[data-cy=isansweruser-checkbox]').click();

        cy.get('[data-cy=save-button]').click();
    })
})