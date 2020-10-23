describe('WIP Status Functionality', () => {
    it('TC63_WIPStatusPageMetadata', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','Wip Status')

        let headerNames = new Array();
        headerNames.push('Product');
        headerNames.push('Part Number');
        headerNames.push('Procedure');
        headerNames.push('Open WorkOrders');

        cy.get('[data-cy=wipstatus-header]').find('th').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){

                if(!$el.text().includes('Location')){
                    throw new Error("Invalid Column in Grid")
                }
                
            }
        })

        cy.checkWebConsoleTracking();

    })
})