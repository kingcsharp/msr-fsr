describe('WIP Menu Functionality', () => {
    it('TC67_WIPMenuPageHelpButton', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/WorkOrder/Menu',
            response: {
                "object": [
                    {
                        "locationName": "Location1",
                        "percentageOfExpectedDurationTimeLogged": 0.4579305804349092,
                        "percentageOfTasksCompleted": 0.6958095125356485,
                        "actualEndDate": "2020-09-14T11:30:08.999Z",
                        "actualStartDate": "2020-09-14T11:30:08.999Z",
                        "currentActiveTaskName": "Active Task Name1",
                        "customerName": "INTEL 32-F",
                        "disposition": "This is some random disposition text",
                        "id": 1,
                        "procedureName": "Procedure1",
                        "productName": "Product Name1",
                        "purchaseId": 1,
                        "purchaseOrderNumber": 1,
                        "quantity": 1,
                        "scheduledEndDate": "2020-09-14T11:30:08.999Z",
                        "scheduledStartDate": "2020-09-14T11:30:08.999Z",
                        "serialNumber": "2342321",
                        "status": "Completed",
                        "workOrderItemNumber": "2324231",
                        "hasNcr": true
                    },
                    {
                        "locationName": "Location2",
                        "percentageOfExpectedDurationTimeLogged": 0.8060215547193104,
                        "percentageOfTasksCompleted": 0.5609747027289826,
                        "actualEndDate": "2020-09-14T11:30:08.999Z",
                        "actualStartDate": "2020-09-14T11:30:08.999Z",
                        "currentActiveTaskName": "Active Task Name2",
                        "customerName": "INTEL 32-F",
                        "disposition": "This is some random disposition text",
                        "id": 2,
                        "procedureName": "Procedure2",
                        "productName": "Product Name2",
                        "purchaseId": 2,
                        "purchaseOrderNumber": 2,
                        "quantity": 2,
                        "scheduledEndDate": "2020-09-14T11:30:08.999Z",
                        "scheduledStartDate": "2020-09-14T11:30:08.999Z",
                        "serialNumber": "2342322",
                        "status": "Completed",
                        "workOrderItemNumber": "2324232",
                        "hasNcr": true
                    },
                    {
                        "locationName": "Location3",
                        "percentageOfExpectedDurationTimeLogged": 0.7384193954189378,
                        "percentageOfTasksCompleted": 0.729146709757418,
                        "actualEndDate": "2020-09-14T11:30:08.999Z",
                        "actualStartDate": "2020-09-14T11:30:08.999Z",
                        "currentActiveTaskName": "Active Task Name3",
                        "customerName": "INTEL 32-F",
                        "disposition": "This is some random disposition text",
                        "id": 3,
                        "procedureName": "Procedure3",
                        "productName": "Product Name3",
                        "purchaseId": 3,
                        "purchaseOrderNumber": 3,
                        "quantity": 3,
                        "scheduledEndDate": "2020-09-14T11:30:08.999Z",
                        "scheduledStartDate": "2020-09-14T11:30:08.999Z",
                        "serialNumber": "2342323",
                        "status": "Completed",
                        "workOrderItemNumber": "2324233",
                        "hasNcr": false
                    }
                ],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });

        cy.route({
            method: 'GET',
            url: '/v1/Help?FriendlyURL=/wip/wip',
            response:  {
                "object": [
                    {
                        "id": 55,
                        "title": "WIP Menu Help Page",
                        "friendlyURL": "/wip/wip",
                        "content": "Sample Help Page for WIP Menu",
                        "roles": [
                            { 
                                "id": 27,
                                "name": "Administrator",
                                "isCertificationRole": null,
                                "menus": [],
                                "permissions": null,
                                "inheritedPermissions": null
                            }
                        ]
                    }
                ],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        })

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','WIP Menu')
        
        cy.get('[data-cy=help-button]').click()

        cy.get('.ui-dialog-title').contains('WIP Menu Help Page')

        cy.get('[data-cy=helpmodalclose-button]').click()
        
        cy.checkWebConsoleTracking();

    })
})