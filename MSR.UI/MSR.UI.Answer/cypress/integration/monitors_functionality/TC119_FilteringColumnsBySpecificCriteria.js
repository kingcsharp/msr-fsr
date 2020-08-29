import { ColumnsSaved } from '../../../src/app/models/lib/ColumnsSaved';

describe('Procedure Types Functionality', () => {
    it('TC119_FilteringColumnsBySpecificCriteria', () => {
        
        cy.server()

        cy.route({
            method: 'GET',
            url: '/v1/Monitors',
            response: {
                "object": [{
                    "id":1,
                    "description":"Loreum Ipsum desca1",
                    "monitorType":"Monitor Type1",
                    "passing":true,
                    "result":"Sample Result text",
                    "serialNumber":"11321",
                    "taskCompleted":"2020-08-27T01:11:19.817Z",
                    "workerName":"John Doe",
                    "inputType":"Text",
                    "shouldBe":"Yes",
                    "targetValue":"Yes",
                    "faultHandling":"Loreum",
                    "sendEmailNotification":true
                },
                {
                    "id":2,
                    "description":"Loreum Ipsum desca1",
                    "monitorType":"Monitor Type1",
                    "passing":true,
                    "result":"Sample Result text",
                    "serialNumber":"11321",
                    "taskCompleted":"2020-08-27T01:11:19.817Z",
                    "workerName":"John Doe",
                    "inputType":"Text",
                    "shouldBe":"Yes",
                    "targetValue":"Yes",
                    "faultHandling":"Loreum",
                    "sendEmailNotification":true
                },
                {
                    "id":11,
                    "description":"Loreum Ipsum desca1",
                    "monitorType":"Monitor Type1",
                    "passing":true,
                    "result":"Sample Result text",
                    "serialNumber":"11321",
                    "taskCompleted":"2020-08-27T01:11:19.817Z",
                    "workerName":"John Doe",
                    "inputType":"Text",
                    "shouldBe":"Yes",
                    "targetValue":"Yes",
                    "faultHandling":"Loreum",
                    "sendEmailNotification":true
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
        
        cy.navigateToPage('Monitors','Monitors')

        const columns = [
            new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
            new ColumnsSaved({ id: 'description', label: 'Description', visible: true }),
            new ColumnsSaved({ id: 'monitorType', label: 'Monitor Type', visible: true }),
            new ColumnsSaved({ id: 'result', label: 'Result', visible: true }),
            new ColumnsSaved({ id: 'passing', label: 'Passing', visible: true }),
            new ColumnsSaved({ id: 'workerName', label: 'Worker Name', visible: true }),
            new ColumnsSaved({ id: 'taskCompleted', label: 'Task Completed', visible: true }),
            new ColumnsSaved({ id: 'serialNumber', label: 'Serial Number', visible: true })
            ];

        cy.get('body').click('top')
        columns.forEach(element => {
            cy.get('[data-cy=monitors-header]').contains(element.label).click();
        });

        cy.writeAndValidate('id','11');
        cy.writeAndValidate('description','Loreum Ipsum desca1');
        cy.writeAndValidate('monitorType','Monitor Type1');
        cy.writeAndValidate('result','Sample Result text');
        cy.writeAndValidate('passing','true');
        cy.writeAndValidate('workerName','John Doe');
        cy.writeAndValidate('serialNumber','11321');
        cy.get('body').click('top')
        cy.get('[data-cy=row]').find('td:first').contains('11')


        cy.checkWebConsoleTracking();

    })
})