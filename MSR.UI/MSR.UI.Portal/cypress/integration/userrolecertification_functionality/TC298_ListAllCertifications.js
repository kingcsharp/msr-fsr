describe('User Role Certification Functionality', () => {
    it('TC298_ListAllCertifications', () => {
        
        cy.server()
        
        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('People','Training/Certifications')

        let headerNames = new Array();
        headerNames.push('Employee Name');
        headerNames.push('Certification');
        headerNames.push('Begin Date');
        headerNames.push('End Date');
        headerNames.push('Status');

        
        cy.get('[data-cy=certifications-header]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })
        
        cy.logout()

        cy.checkWebConsoleTracking();

    })
})