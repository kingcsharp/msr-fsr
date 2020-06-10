describe('User Functionality', () => {
    it('TC361_UsersGridFunctionality', () => {

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        cy.get('[data-cy=usertable-header]').contains('Id').click();
        cy.get('[data-cy=usertable-header]').contains('Active').click();
        cy.get('[data-cy=usertable-header]').contains('User Type').click();
        cy.get('[data-cy=usertable-header]').contains('First Name').click();
        cy.get('[data-cy=usertable-header]').contains('Last Name').click();
        cy.get('[data-cy=usertable-header]').contains('Username').click();
        cy.get('[data-cy=usertable-header]').contains('Email').click();

        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').click()
        
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom')
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Roles').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Location Id').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Supervisor Name').click()
        
        cy.get('[data-cy=usertable-header]').contains('Created On').click();
        cy.get('[data-cy=usertable-header]').contains('Roles').click();
        cy.get('[data-cy=usertable-header]').contains('Location Id').click();
        cy.get('[data-cy=usertable-header]').contains('Supervisor Name').click();
    })
})