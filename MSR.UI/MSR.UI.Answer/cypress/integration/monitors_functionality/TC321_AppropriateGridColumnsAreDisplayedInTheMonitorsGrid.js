describe('Procedure Types Functionality', () => {
    it('TC343_ProcedureTypesGridColumns', () => {
        
        cy.server()

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Monitors','Monitors')

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Description');
        headerNames.push('Monitor Type');
        headerNames.push('Result');
        headerNames.push('Passing');
        headerNames.push('Worker Name');
        headerNames.push('Task Completed');
        headerNames.push('Serial Number');

        cy.get('body').click('top')

        cy.get('[data-cy="monitors-header"]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })

        cy.checkWebConsoleTracking();

    })
})