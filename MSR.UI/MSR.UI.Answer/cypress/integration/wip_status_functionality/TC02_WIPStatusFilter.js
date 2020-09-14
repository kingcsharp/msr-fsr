describe('WIP Status Functionality', () => {
    it('TC02_WIPStatusFilter', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','Wip Status')
        
        cy.get('[data-cy=location-multiselect]').click()
        console.log(cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted'))
        cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted').contains('Hillsboro').click()
        cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted').contains('Chandler').click()
        cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted').contains('Naas').click()
        cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted').contains('Kiryat Gat').click()
        cy.get('p-multiselectitem').find('li').find('span.ng-star-inserted').contains('Hillsboro').click()

        cy.get('[data-cy="wipstatus-row"]').find('[data-cy=wipstatus-location]').contains('Hillsboro')

        cy.checkWebConsoleTracking();

    })
})