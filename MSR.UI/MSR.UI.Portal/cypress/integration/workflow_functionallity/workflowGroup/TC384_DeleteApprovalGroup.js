describe('Workflow Group Functionality', () => {
    it('TC378_ApprovalWorkflowGrid', () => {

        cy.server();
        cy.route({
            method: 'GET',
            url: '/v1/WorkflowGroup',
            response: {
                "object": [{
                    "name": "Workflow Test",
                    "lastUpdatedOn": "2020-08-25T05:30:46.4622405Z",
                    "lastUpdatedBy": 134,
                    "lastUpdatedByName": "Admin Msrfsr",
                    "createdByName": "Admin Msrfsr",
                    "createdOn": "2020-08-25T05:30:46.4622405Z",
                    "createdBy": 134,
                    "id": 45,
                    "isActive": true,
                    "groupRoles": [{
                        "roleId": 1,
                        "name": null,
                        "workflowGroupId": 45
                    }],
                    "groupUsers": [{
                        "userId": 2,
                        "workflowGroupId": 45
                    }]
                },{
                    "name": "ab2",
                    "lastUpdatedOn": "2020-08-24T22:41:19.83",
                    "lastUpdatedBy": 73,
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "createdByName": "Alexanderrrrrgg Elliss",
                    "createdOn": "2020-06-28T13:29:22.29",
                    "createdBy": 73,
                    "id": 10,
                    "isActive": true,
                    "groupRoles": [{
                        "roleId": 27,
                        "name": null,
                        "workflowGroupId": 10
                    }, {
                        "roleId": 2,
                        "name": null,
                        "workflowGroupId": 10
                    }, {
                        "roleId": 3,
                        "name": null,
                        "workflowGroupId": 10
                    }, {
                        "roleId": 17,
                        "name": null,
                        "workflowGroupId": 10
                    }],
                    "groupUsers": [{
                        "userId": 73,
                        "workflowGroupId": 10
                    }]
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-07-11T19:24:10.507",
                    "lastUpdatedBy": 45,
                    "lastUpdatedByName": "Mike Harvey",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-06-30T15:03:04.1",
                    "createdBy": 43,
                    "id": 24,
                    "isActive": false,
                    "groupRoles": [],
                    "groupUsers": []
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-06-30T20:52:04.997",
                    "lastUpdatedBy": 43,
                    "lastUpdatedByName": "David  Dombrowsky",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-06-30T20:52:04.997",
                    "createdBy": 43,
                    "id": 27,
                    "isActive": true,
                    "groupRoles": [],
                    "groupUsers": []
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-08-06T23:36:04.983",
                    "lastUpdatedBy": 78,
                    "lastUpdatedByName": "Diamond Brown",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-07-08T18:33:57.48",
                    "createdBy": 43,
                    "id": 31,
                    "isActive": true,
                    "groupRoles": [{
                        "roleId": 6,
                        "name": null,
                        "workflowGroupId": 31
                    }],
                    "groupUsers": [{
                        "userId": 78,
                        "workflowGroupId": 31
                    }]
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-07-08T18:58:43.093",
                    "lastUpdatedBy": 43,
                    "lastUpdatedByName": "David  Dombrowsky",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-07-08T18:58:43.093",
                    "createdBy": 43,
                    "id": 32,
                    "isActive": true,
                    "groupRoles": [],
                    "groupUsers": []
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-07-11T03:45:21.97",
                    "lastUpdatedBy": 43,
                    "lastUpdatedByName": "David  Dombrowsky",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-07-11T03:45:21.97",
                    "createdBy": 43,
                    "id": 33,
                    "isActive": true,
                    "groupRoles": [],
                    "groupUsers": []
                }, {
                    "name": "Approval Group Add Test 2 8.6.2020",
                    "lastUpdatedOn": "2020-08-24T22:42:38.453",
                    "lastUpdatedBy": 73,
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "createdByName": "Diamond Brown",
                    "createdOn": "2020-08-06T21:18:09.647",
                    "createdBy": 78,
                    "id": 40,
                    "isActive": false,
                    "groupRoles": [{
                        "roleId": 1,
                        "name": null,
                        "workflowGroupId": 40
                    }, {
                        "roleId": 27,
                        "name": null,
                        "workflowGroupId": 40
                    }],
                    "groupUsers": [{
                        "userId": 121,
                        "workflowGroupId": 40
                    }, {
                        "userId": 78,
                        "workflowGroupId": 40
                    }, {
                        "userId": 119,
                        "workflowGroupId": 40
                    }, {
                        "userId": 78,
                        "workflowGroupId": 40
                    }]
                }, {
                    "name": "Test Approval Group 8.6.20",
                    "lastUpdatedOn": "2020-08-06T23:57:18.633",
                    "lastUpdatedBy": 78,
                    "lastUpdatedByName": "Diamond Brown",
                    "createdByName": "Diamond Brown",
                    "createdOn": "2020-08-06T23:57:18.633",
                    "createdBy": 78,
                    "id": 41,
                    "isActive": true,
                    "groupRoles": [{
                        "roleId": 16,
                        "name": null,
                        "workflowGroupId": 41
                    }],
                    "groupUsers": [{
                        "userId": 78,
                        "workflowGroupId": 41
                    }]
                }, {
                    "name": "CreateWorkflowGroupRequest-8faf6491cf88817e",
                    "lastUpdatedOn": "2020-08-10T17:58:17.91",
                    "lastUpdatedBy": 43,
                    "lastUpdatedByName": "David  Dombrowsky",
                    "createdByName": "David  Dombrowsky",
                    "createdOn": "2020-08-10T17:58:17.91",
                    "createdBy": 43,
                    "id": 42,
                    "isActive": true,
                    "groupRoles": [],
                    "groupUsers": []
                }],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });
        cy.route({
            method:'Delete',
            url: '/v1/WorkflowGroup/45',
            response:{
                "successMessage": "WorkflowGroup was successfully removed.",
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });

        cy.beginWebConsoleTracking();
        cy.login('admin');
        cy.navigateToPage('Workflow', 'Approval Groups');
        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");
        cy.showDropdownTrackableModel('usergrid-options');

        //delete item
        cy.get('[data-cy=searchbyname-grid]').type('Workflow Test').should('have.value', 'Workflow Test');
        cy.get('tbody').find('tr:first-child td').contains('Workflow Test');
        cy.get('tbody').find('tr:first-child td').get('[data-cy=deleteRow').click();
        //delete item
        cy.waitpost();
        cy.get('[data-cy=searchbyname-grid]').clear();
        cy.writeAndValidate('searchbyname-grid','3');
        cy.get('[data-cy=deleteRow]').should('not.exist');
        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
