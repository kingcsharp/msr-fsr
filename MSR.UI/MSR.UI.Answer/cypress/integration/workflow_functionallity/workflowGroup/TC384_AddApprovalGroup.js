describe('User Functionality', () => {
    it('TC378_ApprovalWorkflowGrid', () => {

        cy.server()

        cy.route({
            method: 'POST',
            url: '/v1/WorkflowGroup',
            response: []
          })

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {

            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')

        })

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        var menuBaseItemName = "Workflow";
        var menuChildItemName = "Approval Groups";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: 53000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
                console.log('a')
                cy.wrap(elem.parent()).click();
            }
        })

        cy.get('#Workflow li a span', { timeout: 1000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
                cy.wrap(elem.parent()).click();
            }
        })

        cy.get('[data-cy=add-button]').click()

        cy.get('[data-cy=user-header]').contains('Create Workflow Group')

        cy.get('[data-cy=name-input]').type('WF1A').should('have.value','WF1A')

        cy.get('[data-cy=lastname-input]').type('Doe').should('have.value','Doe')

        cy.get('[data-cy=email-input]').type('jdoe@cmhworks.com').should('have.value','jdoe@cmhworks.com')
        
        cy.get('[data-cy=username-input]').type('jdoe').should('have.value','jdoe')
        
        cy.get('[data-cy=title-input]').type('Mr.').should('have.value','Mr.')

        cy.get('[data-cy=phone-input]').type('2143326762').should('have.value','(214) 332-6762')

        cy.get('[data-cy=isactive-checkbox]').click();

        cy.get('[data-cy=isansweruser-checkbox]').click();

        cy.get('[data-cy=save-button]').click();

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called") 
    })
})