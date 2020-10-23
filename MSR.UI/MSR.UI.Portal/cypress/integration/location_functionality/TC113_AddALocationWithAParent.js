describe('Location Functionality', () => {
    it('TC113_AddALocationWithAParent', () => {
        
        cy.server()

        cy.route({
            method: 'GET',
            url: '/v1/Location',
            response: {
                "object": [
                    {
                        "oldId": 2,
                        "name": "Cypress Test Location ",
                        "address1": "asdasd",
                        "address2": "asdas",
                        "city": "asda",
                        "state": "asd",
                        "postalCode": null,
                        "country": "Afganistan",
                        "phone": "1231231212",
                        "parentId": null,
                        "parent": null,
                        "internalAddress": "1234",
                        "invoiceClass": null,
                        "timeZone": null,
                        "status": "Pending",
                        "isActive": true,
                        "lastUpdatedOn": "2020-08-01T03:47:25.3",
                        "lastUpdatedBy": 107,
                        "lastUpdated": null,
                        "createdOn": "2019-01-29T21:41:25.317",
                        "createdBy": 4,
                        "created": null,
                        "id": 1
                    }
                ],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        })

        cy.route({
            method: 'POST',
            url: '/v1/Location',
            response: {
                "object": [
                    {
                        "oldId": 2,
                        "name": "Cypress Test Location ",
                        "address1": "asdasd",
                        "address2": "asdas",
                        "city": "asda",
                        "state": "asd",
                        "postalCode": null,
                        "country": "Afganistan",
                        "phone": "1231231212",
                        "parentId": null,
                        "parent": null,
                        "internalAddress": "1234",
                        "invoiceClass": null,
                        "timeZone": null,
                        "status": "Pending",
                        "isActive": true,
                        "lastUpdatedOn": "2020-08-01T03:47:25.3",
                        "lastUpdatedBy": 107,
                        "lastUpdated": null,
                        "createdOn": "2019-01-29T21:41:25.317",
                        "createdBy": 4,
                        "created": null,
                        "id": 1
                    }
                ],
                "successMessage":"Location Create Pending Approval",
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        })

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('Locations','Locations')

        cy.get('[data-cy=add-button]').click()

        cy.get('[data-cy=parentlocation-dropdown]').click()
        cy.get('.ui-dropdown-item').click()
        cy.get('[data-cy=name-input]').type("Cypress Sample Test Name").should('have.value', "Cypress Sample Test Name")
        cy.get('[data-cy=internaladdress-input]').type("CCBB3").should('have.value', "CCBB3")
        cy.get('[data-cy=save-button]').click()
        
        cy.checkWebConsoleTracking();

    })
})