describe('Workflow Group Functionality', () => {
    it('TC382_ApprovalStageGrid', () => {

        var adminUsername = Cypress.env('admin-username');
        var adminPassword = Cypress.env('admin-password');
        var maxTimeout = parseInt(Cypress.env('admin-password'));

        cy.visit('/');

        cy.url().should('include', '/login');

        cy.window().then((win) => {
            cy.spy(win.console, 'error').as('errorMessage');
            cy.spy(win.console, 'warn').as('warningMessage');
        })
        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername);
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword);

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

        cy.get(".ui-blockui-document", { timeout: 10000 }).should("not.be.visible");
        cy.get('[data-cy=page-title]').contains(menuChildItemName).click();

        cy.get('[data-cy=usertable-header]').contains('Id').click();
        cy.get('[data-cy=usertable-header]').contains('Active').click();
        cy.get('[data-cy=usertable-header]').contains('Approval Stage Name').click();

        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').click()

        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom')
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created By').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated On').click()
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated By').click()

        cy.get('[data-cy=usertable-header]').contains('Created On').click();
        cy.get('[data-cy=usertable-header]').contains('Created By').click();
        cy.get('[data-cy=usertable-header]').contains('Updated On').click();
        cy.get('[data-cy=usertable-header]').contains('Created By').click();

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
