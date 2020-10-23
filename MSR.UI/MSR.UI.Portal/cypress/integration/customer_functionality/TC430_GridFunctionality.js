describe('Customer Functionality', () => {
    it('TC430_GridFunctionality', () => {
        
        cy.server()
        
        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('People','Customers/Departments')

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Name');
        headerNames.push('Customer Number');
        headerNames.push('Address');
        headerNames.push('Phone');
        headerNames.push('Location');
        headerNames.push('Primary Contact');
        headerNames.push('Secondary Contact');
        headerNames.push('Is Active');
        headerNames.push('Created By');
        headerNames.push('Created On');
        headerNames.push('Status');
        
        cy.get('[data-cy=customers-header]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined && $el.text().trim() !== 'Actions'){
                throw new Error("Invalid Column in Grid" + $el.text().trim())
            }
        })
        
        cy.logout()

        cy.checkWebConsoleTracking();

    })
})