describe('Location Functionality', () => {
    it('TC208_LocationGridFunctionality', () => {
        
        cy.server()
        
        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Locations','Locations')

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Name');
        headerNames.push('Internal Address');
        headerNames.push('Created By');
        headerNames.push('Created On');
        headerNames.push('Status');
        headerNames.push('Address 1');
        headerNames.push('Address 2');
        headerNames.push('City');
        headerNames.push('State/Province');
        headerNames.push('Postalcode');
        headerNames.push('Country');
        headerNames.push('Phone');
        headerNames.push('Parent');
        headerNames.push('Timezone');
        
        cy.get('[data-cy=locations-header]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined && $el.text().trim() !== 'Actions'){
                throw new Error("Invalid Column in Grid" + $el.text().trim())
            }
        })
        
        cy.logout()

        cy.checkWebConsoleTracking();

    })
})