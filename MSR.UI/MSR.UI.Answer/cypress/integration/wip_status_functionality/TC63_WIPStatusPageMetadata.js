describe('WIP Status Functionality', () => {
    it('TC63_WIPStatusPageMetadata', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','Wip Status')
        
        

        cy.checkWebConsoleTracking();

    })
})