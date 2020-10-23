
describe('Invoice Functionality', () => {
    it('TC366_InvoiceFilteringColumns', () => {
        cy.server();
        cy.route({
            method:'GET',
            url: '/v1/Invoice',
            response:{
                "object": [{
                    "id": 21,
                    "customerId": 2,
                    "customerName": "SWAGELOK",
                    "description": "Updated 3",
                    "invoiceNumber": "05-20-21",
                    "amount": 550.0000,
                    "taxPercentage": 10.00,
                    "dueDate": "2020-07-18T06:46:08.577",
                    "createdOn": "2020-08-13T22:15:37.797",
                    "createdByName": "Alex San",
                    "lastUpdatedOn": "2020-08-14T03:07:22.45",
                    "lastUpdatedByName": "Alex San",
                    "statusId": 5,
                    "locationId": 829,
                    "invoiceItems": [{
                        "id": 20,
                        "purchaseOrderId": 4,
                        "purchaseNumber": "999",
                        "workOrderId": 14
                    }, {
                        "id": 27,
                        "purchaseOrderId": 1,
                        "purchaseNumber": "999",
                        "workOrderId": 12
                    }]
                }, {
                    "id": 28,
                    "customerId": 2,
                    "customerName": "SWAGELOK2",
                    "description": "Alex description",
                    "invoiceNumber": "05-20-28",
                    "amount": 796.2790,
                    "taxPercentage": 10.00,
                    "dueDate": "2020-07-20T04:20:59.89",
                    "createdOn": "2020-08-14T03:07:41.907",
                    "createdByName": "Alex San",
                    "lastUpdatedOn": "2020-08-14T03:07:42.297",
                    "lastUpdatedByName": "Alex San",
                    "statusId": 5,
                    "locationId": 829,
                    "invoiceItems": [{
                        "id": 35,
                        "purchaseOrderId": 6,
                        "purchaseNumber": "999",
                        "workOrderId": 100
                    }]
                }, {
                    "id": 35,
                    "customerId": 12,
                    "customerName": "INTEL VF",
                    "description": "test3AAAlec",
                    "invoiceNumber": "05-20-35",
                    "amount": 103.0000,
                    "taxPercentage": 3.00,
                    "dueDate": "2020-08-21T03:00:00",
                    "createdOn": "2020-08-14T19:15:36.793",
                    "createdByName": "Alexanderrrrrgg Elliss",
                    "lastUpdatedOn": "2020-08-14T19:15:38.5",
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "statusId": 5,
                    "locationId": 829,
                    "invoiceItems": [{
                        "id": 36,
                        "purchaseOrderId": 1,
                        "purchaseNumber": "999",
                        "workOrderId": 8
                    }]
                }],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });

        cy.beginWebConsoleTracking();
        cy.login('admin');        
        cy.navigateToPage('Billing','Invoices');
        
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').scrollTo('bottom');
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created On').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Created By').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated On').click();
        cy.get('[data-cy=usergrid-options]').find('.gridDropdown').find('.ui-multiselect-items-wrapper').contains('Updated By').click();

        cy.get('[data-cy=customerName]').type("SWAGELOK").should('have.value', "SWAGELOK");
        cy.get('[data-cy=id]').type("21").should('have.value', "21");
        cy.get('[data-cy=description]').type("Updated 3").should('have.value', "Updated 3");
        cy.get('[data-cy=invoiceNumber]').type("05-20-21").should('have.value', "05-20-21");
        cy.get('[data-cy=amount]').type("550").should('have.value', "550");        
        cy.get('[data-cy=createdByName]').type("Alex San").should('have.value', "Alex San");        
        cy.get('[data-cy=lastUpdatedByName]').type("Alex San").should('have.value', "Alex San");

        cy.get('[data-cy=row]').find('td:first').contains('21');

        cy.logout();
        cy.checkWebConsoleTracking();

    })
})

