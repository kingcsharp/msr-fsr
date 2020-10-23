describe('Customer Functionality', () => {
    it('TC347_AddCustomer', () => {

        cy.server()

        cy.route({
            method: 'POST',
            url: '/v1/Customer',
            response: { "object": { "oldId": 0, "name": "asdasd", "address": null, "phone": null, "primaryContactUser": null, "secondaryContactUser": null, "location": null, "status": "Pending", "isActive": true, "customerNumber": null, "lastUpdatedOn": "2020-08-14T06:42:14.8643376Z", "lastUpdatedBy": 119, "lastUpdated": { "id": 119, "isActive": true, "userRoleId": null, "userName": "rlara", "firstName": "Robert", "lastName": "Lara", "fullName": "Robert Lara", "title": "Dev Administrator", "email": "robert.lara@cmhworks.com", "securityStamp": null, "phone": null, "supervisorId": 45, "supervisorName": null, "locationId": 0, "locationName": null, "isAnswerUser": true, "customerId": 0, "lockoutEndDateUtc": null, "lockoutEnabled": false, "accessFailedCount": 0, "timeZoneId": 0, "lastUpdatedOn": "2020-06-10T13:23:22.517", "lastUpdatedBy": 119, "createdOn": "2020-05-19T00:00:00", "createdBy": 45, "roles": [] }, "createdOn": "2020-08-14T06:42:14.8643376Z", "createdBy": 119, "created": { "id": 119, "isActive": true, "userRoleId": null, "userName": "rlara", "firstName": "Robert", "lastName": "Lara", "fullName": "Robert Lara", "title": "Dev Administrator", "email": "robert.lara@cmhworks.com", "securityStamp": null, "phone": null, "supervisorId": 45, "supervisorName": null, "locationId": 0, "locationName": null, "isAnswerUser": true, "customerId": 0, "lockoutEndDateUtc": null, "lockoutEnabled": false, "accessFailedCount": 0, "timeZoneId": 0, "lastUpdatedOn": "2020-06-10T13:23:22.517", "lastUpdatedBy": 119, "createdOn": "2020-05-19T00:00:00", "createdBy": 45, "roles": [] }, "id": 23 }, "successMessage": "Customer Creation Pending Approval", "errorMessages": [], "id": 0, "hasErrors": false, "hasValidationErrors": false }
        })

        cy.route({
            method: 'GET',
            url: '/v1/Location',
            response: {
                "object": [
                    {
                        "oldId": 143521,
                        "name": "Location A",
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
                        "invoiceClass": "06",
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
                    },
                    {
                        "oldId": 143521,
                        "name": "Location B ",
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
                        "invoiceClass": "06",
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
                    },
                    {
                        "oldId": 143521,
                        "name": "Location C ",
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
                        "invoiceClass": "06",
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
            method: 'GET',
            url: '/v1/User',
            response: {
                "object": [
                    {
                        "id": 2,
                        "isActive": true,
                        "userRoleId": "08f41694-1546-4492-b934-c9ce180e6657",
                        "userName": "nbenshushan",
                        "firstName": "Nati",
                        "lastName": "Ben Shushan",
                        "fullName": "Rand Doe",
                        "title": "Production Manager",
                        "email": "test@test.com",
                        "securityStamp": "091025fd-dfbd-43bd-9ae7-7725b84a9234",
                        "phone": "1238765444",
                        "supervisorId": 121,
                        "supervisorName": "Marc Jones",
                        "locationId": 829,
                        "locationName": "Naas",
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-06-30T18:55:22.853",
                        "lastUpdatedBy": 121,
                        "createdOn": "2019-10-30T17:20:58.537",
                        "createdBy": 71,
                        "roles": [
                            {
                                "id": 2,
                                "name": "CEO",
                                "isCertificationRole": null,
                                "menus": [],
                                "permissions": null,
                                "inheritedPermissions": null
                            }
                        ]
                    },
                    {
                        "id": 2,
                        "isActive": true,
                        "userRoleId": "08f41694-1546-4492-b934-c9ce180e6657",
                        "userName": "nbenshushan",
                        "firstName": "Nati",
                        "lastName": "Ben Shushan",
                        "fullName": "Jane Doe",
                        "title": "Production Manager",
                        "email": "test@test.com",
                        "securityStamp": "091025fd-dfbd-43bd-9ae7-7725b84a9234",
                        "phone": "1238765444",
                        "supervisorId": 121,
                        "supervisorName": "Marc Jones",
                        "locationId": 829,
                        "locationName": "Naas",
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-06-30T18:55:22.853",
                        "lastUpdatedBy": 121,
                        "createdOn": "2019-10-30T17:20:58.537",
                        "createdBy": 71,
                        "roles": [
                            {
                                "id": 2,
                                "name": "CEO",
                                "isCertificationRole": null,
                                "menus": [],
                                "permissions": null,
                                "inheritedPermissions": null
                            }
                        ]
                    },
                    {
                        "id": 2,
                        "isActive": true,
                        "userRoleId": "08f41694-1546-4492-b934-c9ce180e6657",
                        "userName": "nbenshushan",
                        "firstName": "Nati",
                        "lastName": "Ben Shushan",
                        "fullName": "John Doe",
                        "title": "Production Manager",
                        "email": "test@test.com",
                        "securityStamp": "091025fd-dfbd-43bd-9ae7-7725b84a9234",
                        "phone": "1238765444",
                        "supervisorId": 121,
                        "supervisorName": "Marc Jones",
                        "locationId": 829,
                        "locationName": "Naas",
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-06-30T18:55:22.853",
                        "lastUpdatedBy": 121,
                        "createdOn": "2019-10-30T17:20:58.537",
                        "createdBy": 71,
                        "roles": [
                            {
                                "id": 2,
                                "name": "CEO",
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

        cy.navigateToPage('People', 'Customers/Departments')

        cy.get('[data-cy=add-button]').click()

        cy.get('[data-cy=name-input]').type('Loreum Ipsum').should('have.value', 'Loreum Ipsum')
        cy.get('[data-cy=customernumber-input]').type('Loreum Ipsum').should('have.value', 'Loreum Ipsum')
        cy.get('[data-cy=address-input]').type('Loreum Ipsum').should('have.value', 'Loreum Ipsum')
        cy.get('[data-cy=phone-input]').type('2222222222').should('have.value', '2222222222')

        cy.get('[data-cy=location-dropdown]').click()

        cy.get("[aria-label='Location A']").click()

        cy.get('[data-cy=primarycontact-dropdown]').click()

        cy.get("[aria-label='John Doe']").click()

        cy.get('[data-cy=secondarycontact-dropdown]').click()

        cy.get("[aria-label='Jane Doe']").click()

        cy.get('[data-cy=save-button]').click();
        //cy.logout()

        cy.checkWebConsoleTracking();

    })
})