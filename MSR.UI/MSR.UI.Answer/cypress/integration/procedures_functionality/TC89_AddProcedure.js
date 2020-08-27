describe('Procedure Functionality', () => {
    it('TC315_ProcedureGridColumns', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureType',
            response: {"object":[{"id":1,"name":"Standard Operation","type":null,"revision":null,"status":null},{"id":4,"name":"Corrective Operation","type":null,"revision":null,"status":null},{"id":6,"name":"Non-Conformation Operation","type":null,"revision":null,"status":null},{"id":7,"name":"asdasd","type":null,"revision":null,"status":null},{"id":8,"name":"PROCTYPE1598416546","type":null,"revision":null,"status":null},{"id":9,"name":"asdasd","type":null,"revision":null,"status":null},{"id":10,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":11,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":12,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":13,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":14,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":15,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":16,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":17,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":18,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":19,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":20,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":21,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":22,"name":"asdas","type":null,"revision":null,"status":null},{"id":23,"name":"asdas","type":null,"revision":null,"status":null},{"id":24,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null},{"id":25,"name":"PROCTYPE1598416853","type":null,"revision":null,"status":null}],"successMessage":null,"errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        });
        cy.route({
            method: 'POST',
            url: '/v1/Procedure',
            response: {"object":{"id":8212,"name":"asdas","procedureTypeId":4,"isRelatedToAProduct":null,"creatorCompany":null,"createdByDepartmentName":null,"revision":0,"comment":null,"duration":2,"durationType":"SYS_HOURS","procedureType":{"id":4,"name":"Corrective Operation","type":null,"revision":null,"status":null},"referenceFiles":null,"roles":null},"successMessage":"Procedure was Created Successfully","errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        })

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Procedures')
        cy.get('[data-cy=add-button]').click()
        cy.get(".ui-blockui-document", { timeout: 30000 }).should("not.be.visible");

        cy.get('[data-cy=proceduretype-dropdown]').click()
        cy.get("[aria-label='Standard Operation']").click()
        cy.writeAndValidate('name-input', 'Loreum Ipsum Namum')
        cy.get("[aria-label='Rich Text Editor, main']").type('Loreum Ipsum')
        cy.get("[data-cy=roles-multiselect]").click()
        cy.get('span').contains('Production Manager').click();
        cy.get('span').contains('Administrator').click();
        cy.get('body').click('top')
        cy.writeAndValidate('duration-input', '10')
        cy.get("[data-cy=durationype-dropdown]").click()
        cy.get('span').contains('SYS_MINUTES').click();

        cy.get('[data-cy=save-button]').click()
        cy.checkWebConsoleTracking();

    })
})