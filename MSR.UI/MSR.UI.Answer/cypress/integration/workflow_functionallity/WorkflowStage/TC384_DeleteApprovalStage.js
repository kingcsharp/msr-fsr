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
        var menuChildItemName = "Approval Stages";

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

        //delete item
        var wfstageNewName = 'Workflow Stage Test2';
        cy.get(".ui-blockui-document", { timeout: 4000 }).should("not.be.visible");
        cy.get('[data-cy=searchbyname-grid]').clear();
        cy.get('[data-cy=searchbyname-grid]').type(wfstageNewName).should('have.value', wfstageNewName);
        cy.get('tbody').find('tr:first-child td').contains(wfstageNewName);
        cy.get('tbody').find('tr:first-child td').get('[data-cy=deleteRow').click();
        //delete item
        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
