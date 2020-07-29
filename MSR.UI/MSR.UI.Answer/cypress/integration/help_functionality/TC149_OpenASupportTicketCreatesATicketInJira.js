describe('Help Functionality', () => {
    it('TC149_OpenASupportTicketCreatesATicketInJira', () => {
        
        const getIframeDocument = () => {
            return cy
            .get('iframe[data-cy="jirasupportticket"]')
            // Cypress yields jQuery element, which has the real
            // DOM element under property "0".
            // From the real DOM iframe element we can get
            // the "document" element, it is stored in "contentDocument" property
            // Cypress "its" command can access deep properties using dot notation
            // https://on.cypress.io/its
            .its('0.contentDocument').should('exist')
          }

          const getIframeBody = () => {
            // get the document
            return getIframeDocument()
            // automatically retries until body is loaded
            .its('body').should('not.be.undefined')
            // wraps "body" DOM element to allow
            // chaining more Cypress commands, like ".find(...)"
            .then(cy.wrap)
          }

        cy.server()

        var adminUsername = Cypress.env('admin-username')
        var adminPassword = Cypress.env('admin-password')
        var maxTimeout = parseInt(Cypress.env('maxTimeout'));

        cy.visit('/')

        cy.url().should('include', '/login')

        cy.window().then((win) => {

            cy.spy(win.console, 'error').as('errorMessage')
            cy.spy(win.console, 'warn').as('warningMessage')

        })

        cy.get('[data-cy=username-input]').type(adminUsername).should('have.value', adminUsername)
        cy.get('[data-cy=password-input]').type(adminPassword).should('have.value', adminPassword)
        
        cy.get('[data-cy=submit-button]').click()

        cy.get('[data-cy=logout-link]', {timeout: 20000}).url().should('include', '/people/people')

        var menuBaseItemName = "Help";
        var menuChildItemName = "Support Ticket";

        cy.get('#side-nav a.accordion-toggle>span', { timeout: maxTimeout }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuBaseItemName) > -1) {
                cy.wrap(elem.parent()).click();
            }
        });

        cy.get('#Help li a span', { timeout: 2000 }).each((elem) => {
            if (Cypress.$(elem).text().trim().indexOf(menuChildItemName) > -1) {
                cy.wrap(elem).click();
            }
        });

        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");

        cy.get('#ui-dialog-0-label').contains(menuChildItemName).click();

        getIframeBody().get('#i_summary');//.type('Sample Support Ticker').should('have.value', 'Sample Support Ticker');

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called") 
    })
})