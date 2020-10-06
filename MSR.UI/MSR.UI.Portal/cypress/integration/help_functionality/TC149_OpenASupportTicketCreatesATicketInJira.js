describe('Help Functionality', () => {
    it('TC149_OpenASupportTicketCreatesATicketInJira', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Help','Support Ticket')

        cy.get('[data-cy=close-btn]').click()

        cy.logout()

        cy.checkWebConsoleTracking();

    })
})