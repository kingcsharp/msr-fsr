import { ColumnsSaved } from '../../../src/app/models/lib/ColumnsSaved';

describe('Procedure Step Templates Functionality', () => {
    it('TC345_ProcedureTemplatesHelp', () => {

        cy.server()
        cy.route({
            method: 'POST',
            url: '/v1/ProcedureTemplate',
            response: { "object": { "id": 53, "title": "asdasd", "text": "", "systemTaskId": null, "laborTime": null, "referenceProcedures": null, "referenceDocuments": null, "equipmentTime": null, "replacementCost": null, "utilization": null, "usefulLife": null, "roles": [], "comments": "", "revision": null }, "successMessage": "Procedure Template Created Successfully", "errorMessages": [], "id": 0, "hasErrors": false, "hasValidationErrors": false }
        });

        cy.beginWebConsoleTracking()

        cy.login('admin')

        cy.navigateToPage('Procedures', 'Templates')

        cy.get('[data-cy=add-button]').click()
        cy.get(".ui-blockui-document", { timeout: 1200000 }).should("not.be.visible");


        cy.writeAndValidate('title-input', 'Loreum Ipsum Titlum')
        cy.writeAndValidate('replacementcost-input', '1')
        cy.writeAndValidate('utilization-input', '2')
        cy.writeAndValidate('usefulLife-input', '3')
        cy.multiselectClick('roles-multiselect', 'Administrator');

        cy.get("[aria-label='Rich Text Editor, main']").each(($el, index, $list) => {

            if (index === 0) {
                cy.wrap($el).type('Sample Text')
            }

            if (index === 1) {
                cy.wrap($el).type('Sample Comments')
            }

        })

        cy.get('[data-cy=save-button]').click()

        cy.checkWebConsoleTracking();

    })
})