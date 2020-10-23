describe('Procedure Step Templates Functionality', () => {
    it('TC318_ProcedureTemplatesGridColumns', () => {
        
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

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Procedures','Templates')

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Title');
        headerNames.push('Text');
        headerNames.push('Revision');
        headerNames.push('Status');
        headerNames.push('Reference Files');
        headerNames.push('Actions');

        
        cy.get('[data-cy=templates-header]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })

        cy.checkWebConsoleTracking();

    })
})