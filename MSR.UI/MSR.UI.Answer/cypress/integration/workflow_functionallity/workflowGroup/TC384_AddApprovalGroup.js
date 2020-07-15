describe('Workflow Group Functionality', () => {
    it('TC384_AddApprovalWorkflow', () => {
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
        var menuChildItemName = "Approval Groups";

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

        cy.get(".ui-blockui-document", { timeout: 12000 }).should("not.be.visible");
        cy.get('[data-cy=page-title]').contains('Approval Groups').click();
        //create 
        var wfsageName = 'Workflow Test';
        cy.get('[data-cy=add-button]').click();
        cy.get('[data-cy=user-header]').contains('Create Workflow Group');
        cy.get('[data-cy=name-input]').type(wfsageName).should('have.value',wfsageName);
        cy.get('[data-cy=roles-multiselect]').click();
        cy.get('.ui-multiselect-panel .ui-multiselect-items-wrapper li:first-child:first').click();
        cy.get('.ui-multiselect-panel .ui-multiselect-close').click();
        cy.get('[data-cy=users-multiselect]').click();
        cy.get('.ui-multiselect-items-wrapper li:first-child:first').click();
        cy.get('.ui-multiselect-panel .ui-multiselect-close').click();
        cy.get('[data-cy=save-button]').click();
        cy.get(".ui-blockui-document", { timeout: 6000 }).should("not.be.visible");
        //end creation

        //validate creation in grid
        cy.get('[data-cy=searchbyname-grid]').type(wfsageName).should('have.value', wfsageName);
        cy.get('tbody').find('tr:first-child td').contains(wfsageName);

        //validate creation in approval workflow
        var menuBaseItemName2 = "Workflow";
        var menuChildItemName2 = "Approval Stages";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName2) > -1) {
                cy.wrap(elem.parent().parent()).click();
            }
        });
       
        cy.get('#Workflow li a span', { timeout: 2300 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName2) > -1) {
                cy.wrap(elem).click();
            }
        });

        cy.get(".ui-blockui-document", { timeout: 12000 }).should("not.be.visible");
        
        cy.get('[data-cy=page-title]').contains(menuChildItemName2).click();

        cy.get('[data-cy=add-button]').click();
        cy.get('[data-cy=user-header]').contains('Create Workflow Stage');
        cy.get('[data-cy=workflowgroups-multiselect]').click();

        cy.get('.ui-multiselect-panel .ui-inputtext').type(wfsageName).should('have.value',wfsageName);
        cy.get('.ui-multiselect-panel .ui-multiselect-items-wrapper li:visible>span').contains(wfsageName);
        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
