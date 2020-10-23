
describe('Invoice Functionality', () => {
    it('TC369_DownloadAll', () => {
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
        cy.get('[data-cy=id]').type("21").should('have.value', "21");
       
        cy.get('[data-cy=downloadAll-button').click();
        // cy.logout();
        cy.checkWebConsoleTracking();
    })
});

