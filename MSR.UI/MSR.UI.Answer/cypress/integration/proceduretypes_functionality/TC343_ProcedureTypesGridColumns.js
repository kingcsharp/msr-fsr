describe('Procedure Types Functionality', () => {
    it('TC343_ProcedureTypesGridColumns', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureType',
            response: {"object":[{"id":1,"name":"Standard Operation","type":null,"revision":null,"status":null},{"id":4,"name":"Corrective Operation","type":null,"revision":null,"status":null},{"id":6,"name":"Non-Conformation Operation","type":null,"revision":null,"status":null},{"id":7,"name":"asdasd","type":null,"revision":null,"status":null},{"id":8,"name":"PROCTYPE1598416546","type":null,"revision":null,"status":null},{"id":9,"name":"asdasd","type":null,"revision":null,"status":null},{"id":10,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":11,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":12,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":13,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":14,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":15,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":16,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":17,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":18,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":19,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":20,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":21,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null}],"successMessage":null,"errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        });

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Procedure Types')

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Name');
        headerNames.push('Type');
        headerNames.push('Revision');
        headerNames.push('Status');
        headerNames.push('Actions');

        cy.get('body').click('top')

        cy.get('[data-cy="grid-header"]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })

        cy.checkWebConsoleTracking();

    })
})