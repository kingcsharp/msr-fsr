describe('Workflow Group Functionality', () => {
    it('TC378_ApprovalWorkflowGrid', () => {

        var adminUsername = Cypress.env('admin-username');
        var adminPassword = Cypress.env('admin-password');
        var maxTimeout = parseInt(Cypress.env('admin-password'));

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
        var menuChildItemName = "Approval Workflows";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
                cy.wrap(elem.parent()).click();
            }
        });

        cy.get('#Workflow li a span', { timeout: 1000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
                cy.wrap(elem).click();
            }
        });

        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");
        cy.get('[data-cy=page-title]').contains('Pending Approvals').click();

        //edit 
        cy.get('[data-cy=searchbyname-grid]').type('ApprovalTest').should('have.value', 'ApprovalTest');
        cy.get('tbody').find('tr:first-child td').contains('ApprovalTest');
        cy.get('tbody').find('tr:first-child td').get('[data-cy=editRow').click();
        
        cy.get('[data-cy=user-header]').contains('Edit Workflow');
        cy.get('[data-cy=name-input]').clear();
        cy.get('[data-cy=name-input]').type('ApprovalTest2').should('have.value', 'ApprovalTest2');
        cy.get('[data-cy=roles-multiselect]').click();
        cy.get('.ui-multiselect-panel .ui-multiselect-items-wrapper li:first-child:first').click();
        cy.get('[data-cy=roles-multiselect]').click();
        cy.get('[data-cy=allactivities-multiselect]').click();
        cy.get('.ui-multiselect-items-wrapper li:first-child:first').click();
        cy.get('[data-cy=allactivities-multiselect]').click();
        cy.get('[data-cy=save-button]').click();
        //end edit
        cy.get(".ui-blockui-document", { timeout: 4000 }).should("not.be.visible");
        cy.get('[data-cy=searchbyname-grid]').clear();
        cy.get('[data-cy=searchbyname-grid]').type('ApprovalTest2').should('have.value', 'ApprovalTest2');
        cy.get('tbody').find('tr:first-child td').contains('ApprovalTest2');
        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
