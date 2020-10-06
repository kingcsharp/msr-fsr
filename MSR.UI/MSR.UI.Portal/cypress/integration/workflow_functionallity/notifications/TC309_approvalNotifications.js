describe('Workflow Group Functionality', () => {
    it('TC309_approvalNotifications', () => {

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

        cy.get('[data-cy=submit-button]').click();

        //page-notificationcount
        var notificationCount;
        cy.get('[page-notificationcount]').should(($div) => {
            notificationCount = $div.text();
        });

        // var menuBaseItemName = "Workflow";
        // var menuChildItemName = "Pending Approvals";

        // cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
        //     if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
        //         cy.wrap(elem.parent()).click();
        //     }
        // });

        // cy.get('#Workflow li a span', { timeout: 1000 }).each((elem) => {
        //     if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
        //         cy.wrap(elem).click();
        //     }
        // });

        // cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");

        
        // cy.get('[table-container]').find('p-paginator').find('ui-paginator-current').should(($div) => {
        //     expect(notificationCount).to.match($div.text())
        // });

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
