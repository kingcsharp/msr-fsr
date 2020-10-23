describe('Procedure Types Functionality', () => {
    it('TC343_ProcedureTypesGridColumns', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureType',
            response: {"object":[{"id":1,"name":"Standard Operation","type":null,"revision":null,"status":null},{"id":4,"name":"Corrective Operation","type":null,"revision":null,"status":null},{"id":6,"name":"Non-Conformation Operation","type":null,"revision":null,"status":null},{"id":7,"name":"asdasd","type":null,"revision":null,"status":null},{"id":8,"name":"PROCTYPE1598416546","type":null,"revision":null,"status":null},{"id":9,"name":"asdasd","type":null,"revision":null,"status":null},{"id":10,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":11,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":12,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":13,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":14,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":15,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":16,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":17,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":18,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":19,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":20,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":21,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null}],"successMessage":null,"errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        });

        cy.route({
            method: 'POST',
            url: '/v1/ProcedureType',
            response: {"object":null,"successMessage":"Procedure Type Created Successfully","errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        });

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Procedure Types')

        cy.get('[data-cy=add-button]').click()

        cy.writeAndValidate('name-input', 'Loreum Ipsum Namum')
        cy.get('[data-cy=majorgroup-dropdown]').click()
        cy.get("[aria-label='Operate']").click()
        cy.get('[data-cy=save-button]').click()
        cy.checkWebConsoleTracking();

    })
})