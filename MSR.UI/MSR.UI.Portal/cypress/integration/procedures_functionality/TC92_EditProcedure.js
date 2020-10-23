const { first } = require("rxjs-compat/operator/first")

describe('Procedure Functionality', () => {
    it('TC315_ProcedureGridColumns', () => {
        
        cy.server()
        
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureStepMonitor/procedurestep/**',
            response: {"object":[
                {
                    "id": 1,
                    "monitorType": "Text",
                    "inputType": "Sensor",
                    "shouldBe": null,
                    "targetValue": "32",
                    "faultHandling": "RECORD AND CONTINUE",
                    "description": "Loreum Ipsum Descriptionum I",
                    "sendEmailNotification": true
                  },
                  {
                    "id": 2,
                    "monitorType": "Text",
                    "inputType": "Sensor",
                    "shouldBe": null,
                    "targetValue": "32",
                    "faultHandling": "RECORD AND CONTINUE",
                    "description": "Loreum Ipsum Descriptionum II",
                    "sendEmailNotification": true
                  },
                  {
                    "id": 3,
                    "monitorType": "Text",
                    "inputType": "Sensor",
                    "shouldBe": null,
                    "targetValue": "32",
                    "faultHandling": "RECORD AND CONTINUE",
                    "description": "Loreum Ipsum Descriptionum III",
                    "sendEmailNotification": true
                  }
            ],"successMessage":null,"errorMessages":[],"id":0,"hasErrors":false,"hasValidationErrors":false}
        });

        cy.route({
            method: 'GET',
            url: '/v1/Procedure/**/step',
            response: {
                "object": [
                    {
                        "id": 40802,
                        "procedureId": 7031,
                        "title": "Planar Polish",
                        "stepText": "Loreipsumis simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        "duration": null,
                        "durationType": null,
                        "procedureStepType": "Standard",
                        "procedureStepTypeId": "1",
                        "printOrder": 1,
                        "predecessorStepId": null,
                        "laborTime": null,
                        "equipmentTime": 15,
                        "replacementCost": 200000,
                        "utilizationTime": 1,
                        "usefulLife": 1,
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 32723,
                        "procedureId": 7031,
                        "title": "Serialize",
                        "stepText": "Lorem Ipsum is simply dummy text of the printing and typesetting industry. Lorem Ipsum has been the industry's standard dummy text ever since the 1500s, when an unknown printer took a galley of type and scrambled it to make a type specimen book. It has survived not only five centuries, but also the leap into electronic typesetting, remaining essentially unchanged. It was popularised in the 1960s with the release of Letraset sheets containing Lorem Ipsum passages, and more recently with desktop publishing software like Aldus PageMaker including versions of Lorem Ipsum.",
                        "duration": null,
                        "durationType": null,
                        "procedureStepType": "Standard",
                        "procedureStepTypeId": "1",
                        "printOrder": 2,
                        "predecessorStepId": 1,
                        "laborTime": null,
                        "equipmentTime": 15,
                        "replacementCost": 200000,
                        "utilizationTime": 2,
                        "usefulLife": 1,
                        "referenceFiles": null,
                        "roles": null
                    }
                ],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });


        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Procedures')
        cy.get('body').click('top')

        cy.get('[data-cy=edit-button]').first().click()
        
        cy.get('[data-cy=proceduretype-dropdown]').click()
        cy.get("[aria-label='Standard Operation']").click()
        cy.writeAndValidate('name-input', 'Loreum Ipsum Namum')
        cy.get("[aria-label='Rich Text Editor, main']").first().type('Loreum Ipsum')
        cy.get("[data-cy=roles-multiselect]").first().click()
        cy.get('span').contains('Production Manager').click();
        cy.get('span').contains('Administrator').click();
        cy.get('body').click('top')
        cy.writeAndValidate('duration-input', '10')
        cy.get("[data-cy=durationtype-dropdown]").click()
        cy.get('span').contains('SYS_MINUTES').click();


        cy.checkWebConsoleTracking();

    })
})