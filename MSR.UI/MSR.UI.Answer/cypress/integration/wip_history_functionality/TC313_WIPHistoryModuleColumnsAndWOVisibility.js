describe('WIP History Functionality', () => {
    it('TC313_WIPHistoryModuleColumnsAndWOVisibility', () => {
        
        cy.server()
        cy.route({
            method: 'GET',
            url: '/v1/WorkOrder/History',
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

        cy.beginWebConsoleTracking()

        cy.login('admin')
        
        cy.navigateToPage('WIP','WIP History')

        
        let headerNames = new Array();
        headerNames.push('Id');
        headerNames.push('Purchase Id');
        headerNames.push('WorkOrder Item Number');
        headerNames.push('Customer');
        headerNames.push('Location');
        headerNames.push('Serial Number');
        headerNames.push('Purchase Order Number');
        headerNames.push('Quantity');
        headerNames.push('Scheduled Start Date');
        headerNames.push('Scheduled End Date');
        headerNames.push('Actual Start Date');
        headerNames.push('Actual End Date');
        headerNames.push('Product');
        headerNames.push('Procedure');
        headerNames.push('Status');
        headerNames.push('Disposition');

        cy.get('body').click('top')

        cy.get('[data-cy="grid-header"]').each(($el, index, list$) => {

            if(headerNames.find( name => name === $el.text().trim()) === undefined){
                throw new Error("Invalid Column in Grid")
            }
        })
        
        cy.checkWebConsoleTracking();

    })
})