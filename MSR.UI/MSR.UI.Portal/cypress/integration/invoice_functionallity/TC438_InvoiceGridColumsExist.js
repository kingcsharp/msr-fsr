
describe('Invoice Functionality', () => {
    it('TC438_InvoiceGridColumsExist', () => {
        cy.server();
        cy.beginWebConsoleTracking();
        cy.login('admin');        
        cy.navigateToPage('Billing','Invoices');
        // cy.get('[data-cy=close-btn]').click();

        cy.get('[data-cy=usertable-header]').contains('Due Date').click();
        cy.get('[data-cy=usertable-header]').contains('Id').click();
        cy.get('[data-cy=usertable-header]').contains('Customer Name').click();
        cy.get('[data-cy=usertable-header]').contains('Description').click();
        cy.get('[data-cy=usertable-header]').contains('Invoice Number').click();
        cy.get('[data-cy=usertable-header]').contains('Amount').click();
        

        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom');
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created By').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated On').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated By').click();

        cy.get('[data-cy=usertable-header]').contains('Created On').click();
        cy.get('[data-cy=usertable-header]').contains('Created By').click();
        cy.get('[data-cy=usertable-header]').contains('Updated On').click();
        cy.get('[data-cy=usertable-header]').contains('Updated By').click();


        cy.logout();
        cy.checkWebConsoleTracking();

    })
})

