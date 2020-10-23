describe('Procedure Functionality', () => {
    it('TC315_ProcedureGridColumns', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/Procedure',
            response: {
                "object": [
                    {
                        "id": 7031,
                        "name": "01-150-002-00-test",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 105,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7632,
                        "name": "01-ASM-002-00  non edited upload from ESCAQA",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 0,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7140,
                        "name": "01-INT-001-  ESCAQA Upload 6-1-20 ",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 40,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7141,
                        "name": "01-INT-002-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 65,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7142,
                        "name": "01-INT-003-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 30,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7143,
                        "name": "01-INT-004-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 60,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7144,
                        "name": "01-INT-005-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 35,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7145,
                        "name": "01-INT-006-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 50,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7146,
                        "name": "01-INT-007-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 35,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7147,
                        "name": "01-INT-008-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 80,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7148,
                        "name": "01-INT-009-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 55,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
                        "referenceFiles": null,
                        "roles": null
                    },
                    {
                        "id": 7149,
                        "name": "01-INT-010-  ESCAQA Upload 6-1-20",
                        "procedureTypeId": 1,
                        "isRelatedToAProduct": null,
                        "creatorCompany": null,
                        "createdByDepartmentName": null,
                        "revision": 1,
                        "comment": null,
                        "duration": 25,
                        "durationType": "TIME_SYS_MINUTES",
                        "procedureType": {
                            "id": 1,
                            "name": "Standard Operation",
                            "type": null,
                            "revision": null,
                            "status": null
                        },
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

        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Name');
        headerNames.push('Creator Company');
        headerNames.push('Created By Department');
        headerNames.push('Type');
        headerNames.push('Revision');
        headerNames.push('Reference Files');
        headerNames.push('Actions');

        cy.get('body').click('top')

        cy.get('[data-cy="grid-header"]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })

        cy.checkWebConsoleTracking();

    })
})