import { ColumnsSaved } from '../../../src/app/models/lib/ColumnsSaved';

describe('Procedure Step Templates Functionality', () => {
    it('TC344_ProcedureTemplatesColumnFiltering', () => {
        
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

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Templates')

        const columns = [
            new ColumnsSaved({ id: 'id', label: 'Id', visible: true }),
            new ColumnsSaved({ id: 'title', label: 'Title', visible: true }),
            new ColumnsSaved({ id: 'text', label: 'Text', visible: true }),
            new ColumnsSaved({ id: 'revision', label: 'Revision', visible: true }),
            new ColumnsSaved({ id: 'status', label: 'Status', visible: true }),
            new ColumnsSaved({ id: 'referenceFiles', label: 'Reference Files', visible: true }),
            new ColumnsSaved({ id: 'actions', label: 'Actions', visible: true })
            ];

        cy.get('body').click('top')
        columns.forEach(element => {
            cy.get('[data-cy=templates-header]').contains(element.label).click();
        });

        cy.writeAndValidate('id','1');
        cy.writeAndValidate('title','Some Random Title 32');
        cy.writeAndValidate('text','Random Text A');
        cy.writeAndValidate('revision','1');
        cy.multiselectClick('status-multiselect','Approved');
        cy.get('body').click('top')
        cy.get('[data-cy=row]').find('td:first').contains('1');

        cy.checkWebConsoleTracking();

    })
})