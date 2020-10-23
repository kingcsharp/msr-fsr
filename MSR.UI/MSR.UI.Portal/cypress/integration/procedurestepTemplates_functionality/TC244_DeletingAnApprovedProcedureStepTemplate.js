describe('Procedure Step Templates Functionality', () => {
    it('TC244_DeletingAnApprovedProcedureStepTemplate', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureTemplate',
            response: {
                "object": [
                    {
                        "id": 1,
                        "title": "Some Random Title 32",
                        "text": "Alex Step",
                        "revision": null,
                        "status": null,
                        "isRelatedToAProduct": false,
                        "referenceFiles": [
                            {
                                "fileId": 15,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3380-test.txt"
                            },
                            {
                                "fileId": 16,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3383-test.txt"
                            }
                        ]
                    },
                    {
                        "id": 2,
                        "title": "Some Random Title A",
                        "text": "Alex Step",
                        "revision": null,
                        "status": null,
                        "isRelatedToAProduct": true,
                        "referenceFiles": [
                            {
                                "fileId": 15,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3380-test.txt"
                            },
                            {
                                "fileId": 16,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3383-test.txt"
                            }
                        ]
                    },
                    {
                        "id": 3,
                        "title": "Some Random Title",
                        "text": "Alex Step",
                        "revision": null,
                        "status": null,
                        "isRelatedToAProduct": false,
                        "referenceFiles": [
                            {
                                "fileId": 15,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3380-test.txt"
                            },
                            {
                                "fileId": 16,
                                "entityId": null,
                                "name": "test.txt",
                                "base64String": null,
                                "fileContents": null,
                                "contentType": "text/html",
                                "fileURL": "Part-3383-test.txt"
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
        });

        cy.route({
            method: 'DELETE',
            url: '/v1/ProcedureTemplate/**',
            response: { "successMessage": "Procedure Template Deleted successfully", "errorMessages": [], "id": 0, "hasErrors": false, "hasValidationErrors": false }
        })

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Templates')

        cy.get('[data-cy=delete-button]').then((deleteButtons) => {
            let firstDeleteButton = deleteButtons[0]
            cy.wrap(firstDeleteButton).click();
        })

        cy.get('[data-cy=confirmdelete-button]').click()

        cy.checkWebConsoleTracking();

    })
})