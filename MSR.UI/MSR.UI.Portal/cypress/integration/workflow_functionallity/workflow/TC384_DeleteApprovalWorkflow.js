describe('Workflow Group Functionality', () => {
    it('TC378_ApprovalWorkflowGrid', () => {
        cy.server();
        cy.route({
            method: 'GET',
            url: '/v1/Workflow',
            response:{
                "object": [{
                    "name": "Reference Document Approvals",
                    "lastUpdatedByName": "Diamond Brown",
                    "createdByName": "Mike Harvey",
                    "memberStages": [{
                        "workflowStageName": "Reference Document Approvals",
                        "workflowId": 1,
                        "workflowStageId": 4
                    }, {
                        "workflowStageName": "Admin Stage",
                        "workflowId": 1,
                        "workflowStageId": 5
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Documents",
                        "workflowId": 1,
                        "workflowActivityId": 14
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-04T02:47:17.543",
                    "lastUpdatedBy": 78,
                    "lastUpdated": {
                        "id": 78,
                        "isActive": true,
                        "userRoleId": "f6b01ff6-0568-4feb-9afb-29525cad8430",
                        "userName": "dbrown",
                        "firstName": "Diamond",
                        "lastName": "Brown",
                        "fullName": "Diamond Brown",
                        "title": "Administrators",
                        "email": "diamond.brown@cmhworks.com",
                        "securityStamp": "ed548a82-4da5-4825-9d4a-fcffcfb8bde6",
                        "phone": null,
                        "supervisorId": 45,
                        "supervisorName": "Mike Harvey",
                        "locationId": 0,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 11,
                        "lastUpdatedOn": "2020-05-31T13:17:16.807",
                        "lastUpdatedBy": 78,
                        "createdOn": "2019-07-05T14:47:32.4",
                        "createdBy": 71,
                        "roles": []
                    },
                    "createdOn": "2019-05-31T16:13:49.733",
                    "createdBy": 45,
                    "created": {
                        "id": 45,
                        "isActive": true,
                        "userRoleId": "9c096045-0b58-4a8d-98d8-2cd5d7661f1e",
                        "userName": "mharvey",
                        "firstName": "Mike",
                        "lastName": "Harvey",
                        "fullName": "Mike Harvey",
                        "title": "Dev Administrator",
                        "email": "mike.harvey@cmhworks.com",
                        "securityStamp": "57a57397-f709-43f8-a5bd-e9a3d4ed060b",
                        "phone": "7032145629",
                        "supervisorId": 71,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 11,
                        "lastUpdatedOn": "2020-06-11T19:24:28.78",
                        "lastUpdatedBy": 45,
                        "createdOn": "2018-07-01T00:58:16.707",
                        "createdBy": 71,
                        "roles": []
                    },
                    "id": 1
                }, {
                    "name": "Admin Workflow",
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "createdByName": "Mike Harvey",
                    "memberStages": [{
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 26
                    }, {
                        "workflowStageName": "Approval Stage Test 1",
                        "workflowId": 2,
                        "workflowStageId": 24
                    }, {
                        "workflowStageName": "Test Approval Stage",
                        "workflowId": 2,
                        "workflowStageId": 23
                    }, {
                        "workflowStageName": "Workflow Stage Test",
                        "workflowId": 2,
                        "workflowStageId": 22
                    }, {
                        "workflowStageName": "Workflow Stage Test",
                        "workflowId": 2,
                        "workflowStageId": 21
                    }, {
                        "workflowStageName": "Workflow Stage Test",
                        "workflowId": 2,
                        "workflowStageId": 20
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 18
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 16
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 15
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 14
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 13
                    }, {
                        "workflowStageName": "wewe2",
                        "workflowId": 2,
                        "workflowStageId": 12
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 11
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 10
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 9
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 8
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 7
                    }, {
                        "workflowStageName": "wwwww23",
                        "workflowId": 2,
                        "workflowStageId": 6
                    }, {
                        "workflowStageName": "Admin Stage",
                        "workflowId": 2,
                        "workflowStageId": 5
                    }, {
                        "workflowStageName": "Reference Document Approvals",
                        "workflowId": 2,
                        "workflowStageId": 4
                    }, {
                        "workflowStageName": "d8f75230c904a614",
                        "workflowId": 2,
                        "workflowStageId": 17
                    }, {
                        "workflowStageName": "OP and WI Document Approvals",
                        "workflowId": 2,
                        "workflowStageId": 3
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Parts",
                        "workflowId": 2,
                        "workflowActivityId": 12
                    }, {
                        "workflowActivityName": "Products",
                        "workflowId": 2,
                        "workflowActivityId": 11
                    }, {
                        "workflowActivityName": "Purchase Orders",
                        "workflowId": 2,
                        "workflowActivityId": 10
                    }, {
                        "workflowActivityName": "Procedures",
                        "workflowId": 2,
                        "workflowActivityId": 8
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 2,
                        "workflowActivityId": 5
                    }, {
                        "workflowActivityName": "Locations",
                        "workflowId": 2,
                        "workflowActivityId": 4
                    }, {
                        "workflowActivityName": "Customers",
                        "workflowId": 2,
                        "workflowActivityId": 1
                    }, {
                        "workflowActivityName": "Documents",
                        "workflowId": 2,
                        "workflowActivityId": 14
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-24T15:35:16.27",
                    "lastUpdatedBy": 73,
                    "lastUpdated": {
                        "id": 73,
                        "isActive": true,
                        "userRoleId": "e9a4c980-d2fb-443e-9af8-0458858316cd",
                        "userName": "alec",
                        "firstName": "Alexanderrrrrgg",
                        "lastName": "Elliss",
                        "fullName": "Alexanderrrrrgg Elliss",
                        "title": "Monkeyy",
                        "email": "alec.ellis@cmhworks.com",
                        "securityStamp": "8d5b9f07-af77-4dcd-b247-b047e67da3c9",
                        "phone": "9863592333",
                        "supervisorId": 35,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 22,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-07-24T02:24:24.37",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-07-10T01:46:56.043",
                        "createdBy": 73,
                        "roles": []
                    },
                    "createdOn": "2019-05-31T16:13:49.733",
                    "createdBy": 45,
                    "created": {
                        "id": 45,
                        "isActive": true,
                        "userRoleId": "9c096045-0b58-4a8d-98d8-2cd5d7661f1e",
                        "userName": "mharvey",
                        "firstName": "Mike",
                        "lastName": "Harvey",
                        "fullName": "Mike Harvey",
                        "title": "Dev Administrator",
                        "email": "mike.harvey@cmhworks.com",
                        "securityStamp": "57a57397-f709-43f8-a5bd-e9a3d4ed060b",
                        "phone": "7032145629",
                        "supervisorId": 71,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 11,
                        "lastUpdatedOn": "2020-06-11T19:24:28.78",
                        "lastUpdatedBy": 45,
                        "createdOn": "2018-07-01T00:58:16.707",
                        "createdBy": 71,
                        "roles": []
                    },
                    "id": 2
                }, {
                    "name": "apWorkflow23",
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "createdByName": "Alexanderrrrrgg Elliss",
                    "memberStages": [{
                        "workflowStageName": "OP and WI Document Approvals",
                        "workflowId": 6,
                        "workflowStageId": 3
                    }, {
                        "workflowStageName": "Reference Document Approvals",
                        "workflowId": 6,
                        "workflowStageId": 4
                    }, {
                        "workflowStageName": "wwwww23",
                        "workflowId": 6,
                        "workflowStageId": 6
                    }, {
                        "workflowStageName": "Admin Stage",
                        "workflowId": 6,
                        "workflowStageId": 5
                    }, {
                        "workflowStageName": "wewe2",
                        "workflowId": 6,
                        "workflowStageId": 12
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Customers",
                        "workflowId": 6,
                        "workflowActivityId": 1
                    }, {
                        "workflowActivityName": "Locations",
                        "workflowId": 6,
                        "workflowActivityId": 4
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 6,
                        "workflowActivityId": 5
                    }, {
                        "workflowActivityName": "Procedures",
                        "workflowId": 6,
                        "workflowActivityId": 8
                    }, {
                        "workflowActivityName": "Purchase Orders",
                        "workflowId": 6,
                        "workflowActivityId": 10
                    }, {
                        "workflowActivityName": "Products",
                        "workflowId": 6,
                        "workflowActivityId": 11
                    }, {
                        "workflowActivityName": "Parts",
                        "workflowId": 6,
                        "workflowActivityId": 12
                    }, {
                        "workflowActivityName": "Documents",
                        "workflowId": 6,
                        "workflowActivityId": 14
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-24T15:34:37.68",
                    "lastUpdatedBy": 73,
                    "lastUpdated": {
                        "id": 73,
                        "isActive": true,
                        "userRoleId": "e9a4c980-d2fb-443e-9af8-0458858316cd",
                        "userName": "alec",
                        "firstName": "Alexanderrrrrgg",
                        "lastName": "Elliss",
                        "fullName": "Alexanderrrrrgg Elliss",
                        "title": "Monkeyy",
                        "email": "alec.ellis@cmhworks.com",
                        "securityStamp": "8d5b9f07-af77-4dcd-b247-b047e67da3c9",
                        "phone": "9863592333",
                        "supervisorId": 35,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 22,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-07-24T02:24:24.37",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-07-10T01:46:56.043",
                        "createdBy": 73,
                        "roles": []
                    },
                    "createdOn": "2020-07-07T02:19:27.89",
                    "createdBy": 73,
                    "created": {
                        "id": 73,
                        "isActive": true,
                        "userRoleId": "e9a4c980-d2fb-443e-9af8-0458858316cd",
                        "userName": "alec",
                        "firstName": "Alexanderrrrrgg",
                        "lastName": "Elliss",
                        "fullName": "Alexanderrrrrgg Elliss",
                        "title": "Monkeyy",
                        "email": "alec.ellis@cmhworks.com",
                        "securityStamp": "8d5b9f07-af77-4dcd-b247-b047e67da3c9",
                        "phone": "9863592333",
                        "supervisorId": 35,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 22,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-07-24T02:24:24.37",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-07-10T01:46:56.043",
                        "createdBy": 73,
                        "roles": []
                    },
                    "id": 6
                }, {
                    "name": "Test Approval Workflow 8.3.20",
                    "lastUpdatedByName": "Diamond Brown",
                    "createdByName": "Diamond Brown",
                    "memberStages": [{
                        "workflowStageName": "Reference Document Approvals",
                        "workflowId": 11,
                        "workflowStageId": 4
                    }, {
                        "workflowStageName": "Approval Stage Test 1",
                        "workflowId": 11,
                        "workflowStageId": 24
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Customers",
                        "workflowId": 11,
                        "workflowActivityId": 1
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 11,
                        "workflowActivityId": 5
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 11,
                        "workflowActivityId": 5
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 11,
                        "workflowActivityId": 5
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-10T23:38:13.257",
                    "lastUpdatedBy": 78,
                    "lastUpdated": {
                        "id": 78,
                        "isActive": true,
                        "userRoleId": "f6b01ff6-0568-4feb-9afb-29525cad8430",
                        "userName": "dbrown",
                        "firstName": "Diamond",
                        "lastName": "Brown",
                        "fullName": "Diamond Brown",
                        "title": "Administrators",
                        "email": "diamond.brown@cmhworks.com",
                        "securityStamp": "ed548a82-4da5-4825-9d4a-fcffcfb8bde6",
                        "phone": null,
                        "supervisorId": 45,
                        "supervisorName": "Mike Harvey",
                        "locationId": 0,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 11,
                        "lastUpdatedOn": "2020-05-31T13:17:16.807",
                        "lastUpdatedBy": 78,
                        "createdOn": "2019-07-05T14:47:32.4",
                        "createdBy": 71,
                        "roles": []
                    },
                    "createdOn": "2020-08-04T02:54:40.277",
                    "createdBy": 78,
                    "created": {
                        "id": 78,
                        "isActive": true,
                        "userRoleId": "f6b01ff6-0568-4feb-9afb-29525cad8430",
                        "userName": "dbrown",
                        "firstName": "Diamond",
                        "lastName": "Brown",
                        "fullName": "Diamond Brown",
                        "title": "Administrators",
                        "email": "diamond.brown@cmhworks.com",
                        "securityStamp": "ed548a82-4da5-4825-9d4a-fcffcfb8bde6",
                        "phone": null,
                        "supervisorId": 45,
                        "supervisorName": "Mike Harvey",
                        "locationId": 0,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 11,
                        "lastUpdatedOn": "2020-05-31T13:17:16.807",
                        "lastUpdatedBy": 78,
                        "createdOn": "2019-07-05T14:47:32.4",
                        "createdBy": 71,
                        "roles": []
                    },
                    "id": 11
                }, {
                    "name": "weee2",
                    "lastUpdatedByName": "Alexanderrrrrgg Elliss",
                    "createdByName": "Alexanderrrrrgg Elliss",
                    "memberStages": [{
                        "workflowStageName": "OP and WI Document Approvals",
                        "workflowId": 14,
                        "workflowStageId": 3
                    }, {
                        "workflowStageName": "Reference Document Approvals",
                        "workflowId": 14,
                        "workflowStageId": 4
                    }, {
                        "workflowStageName": "Admin Stage",
                        "workflowId": 14,
                        "workflowStageId": 5
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Customers",
                        "workflowId": 14,
                        "workflowActivityId": 1
                    }, {
                        "workflowActivityName": "Locations",
                        "workflowId": 14,
                        "workflowActivityId": 4
                    }, {
                        "workflowActivityName": "Users",
                        "workflowId": 14,
                        "workflowActivityId": 5
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-25T01:39:55.17",
                    "lastUpdatedBy": 73,
                    "lastUpdated": {
                        "id": 73,
                        "isActive": true,
                        "userRoleId": "e9a4c980-d2fb-443e-9af8-0458858316cd",
                        "userName": "alec",
                        "firstName": "Alexanderrrrrgg",
                        "lastName": "Elliss",
                        "fullName": "Alexanderrrrrgg Elliss",
                        "title": "Monkeyy",
                        "email": "alec.ellis@cmhworks.com",
                        "securityStamp": "8d5b9f07-af77-4dcd-b247-b047e67da3c9",
                        "phone": "9863592333",
                        "supervisorId": 35,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 22,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-07-24T02:24:24.37",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-07-10T01:46:56.043",
                        "createdBy": 73,
                        "roles": []
                    },
                    "createdOn": "2020-08-25T00:41:57.53",
                    "createdBy": 73,
                    "created": {
                        "id": 73,
                        "isActive": true,
                        "userRoleId": "e9a4c980-d2fb-443e-9af8-0458858316cd",
                        "userName": "alec",
                        "firstName": "Alexanderrrrrgg",
                        "lastName": "Elliss",
                        "fullName": "Alexanderrrrrgg Elliss",
                        "title": "Monkeyy",
                        "email": "alec.ellis@cmhworks.com",
                        "securityStamp": "8d5b9f07-af77-4dcd-b247-b047e67da3c9",
                        "phone": "9863592333",
                        "supervisorId": 35,
                        "supervisorName": null,
                        "locationId": 208,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 22,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-07-24T02:24:24.37",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-07-10T01:46:56.043",
                        "createdBy": 73,
                        "roles": []
                    },
                    "id": 14
                }, {
                    "name": "ApprovalTest",
                    "lastUpdatedByName": "Admin Msrfsr",
                    "createdByName": "Admin Msrfsr",
                    "memberStages": [{
                        "workflowStageName": "OP and WI Document Approvals",
                        "workflowId": 16,
                        "workflowStageId": 3
                    }],
                    "activityMaps": [{
                        "workflowActivityName": "Customers",
                        "workflowId": 16,
                        "workflowActivityId": 1
                    }],
                    "isActive": true,
                    "lastUpdatedOn": "2020-08-25T04:05:47.097",
                    "lastUpdatedBy": 134,
                    "lastUpdated": {
                        "id": 134,
                        "isActive": true,
                        "userRoleId": null,
                        "userName": "admin",
                        "firstName": "Admin",
                        "lastName": "Msrfsr",
                        "fullName": "Admin Msrfsr",
                        "title": "Admin",
                        "email": "admin-msrfsr@cmhworks.com",
                        "securityStamp": null,
                        "phone": "5552221212",
                        "supervisorId": 45,
                        "supervisorName": "Mike Harvey",
                        "locationId": 0,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-06-05T23:16:32.637",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-06-05T19:13:53",
                        "createdBy": 68,
                        "roles": []
                    },
                    "createdOn": "2020-08-25T04:05:47.097",
                    "createdBy": 134,
                    "created": {
                        "id": 134,
                        "isActive": true,
                        "userRoleId": null,
                        "userName": "admin",
                        "firstName": "Admin",
                        "lastName": "Msrfsr",
                        "fullName": "Admin Msrfsr",
                        "title": "Admin",
                        "email": "admin-msrfsr@cmhworks.com",
                        "securityStamp": null,
                        "phone": "5552221212",
                        "supervisorId": 45,
                        "supervisorName": "Mike Harvey",
                        "locationId": 0,
                        "locationName": null,
                        "isAnswerUser": true,
                        "customerId": 0,
                        "lockoutEndDateUtc": null,
                        "lockoutEnabled": false,
                        "accessFailedCount": 0,
                        "timeZoneId": 0,
                        "lastUpdatedOn": "2020-06-05T23:16:32.637",
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-06-05T19:13:53",
                        "createdBy": 68,
                        "roles": []
                    },
                    "id": 16
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
            url: '/v1/Workflow/16',
            response:{
                "successMessage": "Workflow was successfully removed.",
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });

        cy.beginWebConsoleTracking();
        cy.login('admin');
        cy.navigateToPage('Workflow', 'Approval Workflows');

        cy.showDropdownTrackableModel('usergrid-options');
        cy.get(".ui-blockui-document", { timeout: 8000 }).should("not.be.visible");

        //delete item
        cy.get('[data-cy=searchbyname-grid]').type('ApprovalTest').should('have.value', 'ApprovalTest');
        cy.get('tbody').find('tr:first-child td').contains('ApprovalTest');
        cy.get('tbody').find('tr:first-child td').get('[data-cy=deleteRow').click();
        //delete item
        
        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
