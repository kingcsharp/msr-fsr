import { ColumnsSaved } from '../../../src/app/models/lib/ColumnsSaved';

describe('Procedure Step Templates Functionality', () => {
    it('TC345_ProcedureTemplatesHelp', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/ProcedureTemplate',
            response: {
                "object": [
                    {
                        "id": 1,
                        "title": "Some Random Title 32",
                        "text": "Random Text A",
                        "revision": 1,
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
                        "text": "Random Text A",
                        "revision": 2,
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
                        "text": "Random Text B",
                        "revision": 1,
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
            method: 'GET',
            url: '/v1/Help?FriendlyURL=/procedures/proceduretemplates',
            response:  {
                "object": [
                    {
                        "id": 55,
                        "title": "Procedure Template Help Page",
                        "friendlyURL": "/procedures/proceduretemplates",
                        "content": "Sample Help Page for Procedure Template",
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
        
        cy.navigateToPage('Procedures','Templates')

        cy.get('[data-cy=help-button]').click()

        cy.get('[data-cy=user-header]').contains('Procedure Template Help Page')

        cy.get('[data-cy=helpmodalclose-button]').click()

        cy.checkWebConsoleTracking();

    })
})