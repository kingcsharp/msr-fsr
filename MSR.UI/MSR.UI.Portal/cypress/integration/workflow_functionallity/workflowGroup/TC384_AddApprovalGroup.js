describe('Workflow Group Functionality', () => {
    it('TC384_AddApprovalWorkflow', () => {
        cy.server();
        cy.route({
            method: 'GET',
            url: '/v1/WorkflowGroup',
            response: {
                "object": [{
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
            method: 'GET',
            url: '/v1/Role',
            response: {
                "object": [{
                    "id": 1,
                    "name": "GM - General Manager",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 108,
                        "url": "Roles",
                        "name": "Roles",
                        "info": "List of User Roles",
                        "icon": "fa-group",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 2,
                    "name": "CEO",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 3,
                    "name": "CFO",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 4,
                    "name": "CTO",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 5,
                    "name": "COO",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 6,
                    "name": "Office Manager",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 109,
                        "url": "Customers",
                        "name": "Customers/Departments",
                        "info": "List of  Customers and Departments",
                        "icon": "fa-building",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 7,
                    "name": "Technician",
                    "isCertificationRole": true,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 8,
                    "name": "MSR Shipper / Receiver",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 9,
                    "name": "Media Blast Certification",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 10,
                    "name": "Process Engineer",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": false,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 11,
                    "name": "Incoming Inspection IL",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 12,
                    "name": "Disassembly IL",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 13,
                    "name": "Soak DI IL",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 14,
                    "name": "Wipe down IPA IL",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 15,
                    "name": "Production Manager",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 108,
                        "url": "Roles",
                        "name": "Roles",
                        "info": "List of User Roles",
                        "icon": "fa-group",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 16,
                    "name": "MSR Quality Controller",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 17,
                    "name": "Clean Room",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 21,
                    "name": "Mainteance Technician",
                    "isCertificationRole": true,
                    "menus": [{
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": null,
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 23,
                    "name": "Client Engineer",
                    "isCertificationRole": null,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 24,
                    "name": "Client Buyer",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }, {
                    "id": 27,
                    "name": "Administrator",
                    "isCertificationRole": null,
                    "menus": [{
                        "id": 122,
                        "url": "WIPStatus",
                        "name": "Wip Status",
                        "info": "Work Orders In Progress Monitor View",
                        "icon": "fa-tachometer",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 123,
                        "url": "WIP",
                        "name": "WIP Menu",
                        "info": "List of Work Orders In Progress",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 104,
                        "url": "parts",
                        "name": "Parts",
                        "info": "List of Customer Parts",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 116,
                        "url": "ProcedureTemplates",
                        "name": "Templates",
                        "info": "List of Procedure Templates",
                        "icon": "fa-file-text",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 107,
                        "url": "People",
                        "name": "Users",
                        "info": "List of Users",
                        "icon": "fa-user",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 108,
                        "url": "Roles",
                        "name": "Roles",
                        "info": "List of User Roles",
                        "icon": "fa-group",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 109,
                        "url": "Customers",
                        "name": "Customers/Departments",
                        "info": "List of  Customers and Departments",
                        "icon": "fa-building",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 110,
                        "url": "Training",
                        "name": "Training/Certifications",
                        "info": "List of Training/Certifications",
                        "icon": "fa-certificate",
                        "orderNumber": 4,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 111,
                        "url": "Products",
                        "name": "Quotes/Products",
                        "info": "List of Products and Quotes",
                        "icon": "fa-paper-plane-o",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 112,
                        "url": "Quote/Create",
                        "name": "Freeform Quote",
                        "info": "Create a Quote for a Customer",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 113,
                        "url": "PurchaseOrder",
                        "name": "Purchase Orders",
                        "info": "List of POs",
                        "icon": "fa-list-ul",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Pricing",
                            "info": "Quotes, Products, and POs",
                            "icon": "fa-money",
                            "orderNumber": 7
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 98,
                        "url": "PurchaseOrder/Purchases",
                        "name": "Purchases",
                        "info": "List of Purchases",
                        "icon": "fa-usd",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": false,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 124,
                        "url": "ApprovalWorkflows",
                        "name": "Approval Workflows",
                        "info": "List of Approval Workflows",
                        "icon": "fa-align-center",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Workflow",
                            "info": "Workflows, Stages, Groups, and Pending Approvals",
                            "icon": "fa-align-center",
                            "orderNumber": 9
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 125,
                        "url": "ApprovalStages",
                        "name": "Approval Stages",
                        "info": "List of Approval Stages",
                        "icon": "fa-list-ol",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Workflow",
                            "info": "Workflows, Stages, Groups, and Pending Approvals",
                            "icon": "fa-align-center",
                            "orderNumber": 9
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 126,
                        "url": "ApprovalGroups",
                        "name": "Approval Groups",
                        "info": "List of Approval Groups",
                        "icon": "fa-group",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Workflow",
                            "info": "Workflows, Stages, Groups, and Pending Approvals",
                            "icon": "fa-align-center",
                            "orderNumber": 9
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 127,
                        "url": "PendingApproval",
                        "name": "Pending Approvals",
                        "info": "List of Pending Approvals and CSRs",
                        "icon": "fa-clock-o",
                        "orderNumber": 4,
                        "menuGroup": {
                            "url": "#",
                            "name": "Workflow",
                            "info": "Workflows, Stages, Groups, and Pending Approvals",
                            "icon": "fa-align-center",
                            "orderNumber": 9
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 117,
                        "url": "Report/AdHocReports/",
                        "name": "Reports",
                        "info": "Available Ad-Hoc Reports",
                        "icon": "fa-bar-chart",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Reporting",
                            "info": "All Adhoc Reports",
                            "icon": "fa-bar-chart",
                            "orderNumber": 10
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 99,
                        "url": "Report/FinancialDashboard",
                        "name": "Financial",
                        "info": "Financial Dashboard of Reports",
                        "icon": "fa-line-chart",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 100,
                        "url": "Report/OperationsDashboard",
                        "name": "Operational",
                        "info": "Operational Dashboard of Reports",
                        "icon": "fa-pie-chart",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Dashboards",
                            "info": "Dashboard Reporting",
                            "icon": "fa-dashboard",
                            "orderNumber": 11
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 119,
                        "url": "EquipmentMaintenance",
                        "name": "Equipment Maintenance",
                        "info": "List of EM/PM Requests",
                        "icon": "fa-plus-square",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 121,
                        "url": "AdminCostSettings",
                        "name": "Admin Cost Settings",
                        "info": "System Wide Cost Settings",
                        "icon": "far fa-edit",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Utilities",
                            "info": "Administrative Tools",
                            "icon": "fa-cogs",
                            "orderNumber": 12
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 101,
                        "url": "#",
                        "name": "Support Ticket",
                        "info": "Create a new Support Ticket",
                        "icon": "fa-question-circle",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 102,
                        "url": "Help",
                        "name": "Help Pages",
                        "info": "List of Pages to View and Edit",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Help",
                            "info": "Support and Help Pages",
                            "icon": "fa-life-bouy",
                            "orderNumber": 14
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 115,
                        "url": "ProcedureTypes",
                        "name": "Procedure Types",
                        "info": "List of Procedure Types",
                        "icon": "fa-list",
                        "orderNumber": 2,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 132,
                        "url": "Monitors",
                        "name": "Monitors",
                        "info": "Completed Monitors",
                        "icon": "fa-desktop",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Monitors",
                            "info": "Procedure Monitors",
                            "icon": "fa-desktop",
                            "orderNumber": 6
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 129,
                        "url": "Parts",
                        "name": "Parts",
                        "info": "Part Definitions",
                        "icon": "fa-cog",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Parts",
                            "info": "Part Definitions",
                            "icon": "fa-cogs",
                            "orderNumber": 2
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 135,
                        "url": "Documents",
                        "name": "Documents",
                        "info": "Approved Documents",
                        "icon": "fa-file",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Documents",
                            "info": "List of Site-wide Documents",
                            "icon": "fa-files-o",
                            "orderNumber": 13
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 133,
                        "url": "Locations",
                        "name": "Locations",
                        "info": "Sites, Rooms, and Equipment",
                        "icon": "fa-map",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Locations",
                            "info": "Sites, Rooms, and Equipment",
                            "icon": "fa-map-marker",
                            "orderNumber": 5
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 128,
                        "url": "RoleAssignments",
                        "name": "Role Module Permission",
                        "info": "Module to Role Settings and Permissions",
                        "icon": "fa-key",
                        "orderNumber": 5,
                        "menuGroup": {
                            "url": "#",
                            "name": "People",
                            "info": "Users, Roles, Companies and Training",
                            "icon": "fa-users",
                            "orderNumber": 4
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 114,
                        "url": "Procedures",
                        "name": "Runnable Procedures",
                        "info": "List of Customer Procedures",
                        "icon": "fa-step-forward",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Procedures",
                            "info": "Procedures, Types, and Templates",
                            "icon": "fa-puzzle-piece",
                            "orderNumber": 3
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 137,
                        "url": "WIPHistory",
                        "name": "WIP History",
                        "info": "List of Completed and Cancelled WorkOrders",
                        "icon": "fa-archive",
                        "orderNumber": 3,
                        "menuGroup": {
                            "url": "#",
                            "name": "WIP",
                            "info": "Work Orders In Progress",
                            "icon": "fa-list-alt",
                            "orderNumber": 1
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }, {
                        "id": 97,
                        "url": "Invoices",
                        "name": "Invoices",
                        "info": "Create and Edit Invoices",
                        "icon": "fa-exchange",
                        "orderNumber": 1,
                        "menuGroup": {
                            "url": "#",
                            "name": "Billing",
                            "info": "Invoices and POs",
                            "icon": "fa-envelope-square",
                            "orderNumber": 8
                        },
                        "permissions": {
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "inheritedPermissions": {
                            "canRead": false,
                            "canCreate": false,
                            "canEdit": false,
                            "canActivate": false,
                            "canApprove": false,
                            "canDelete": false
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null
                }],
                "successMessage": null,
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });
        cy.route({
            method:'POST',
            url: '/v1/WorkflowGroup',
            response:{
                "object": {
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
                },
                "successMessage": "Workflow Group has been successfully created.",
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
        //create 
        var wfsageName = 'Workflow Test';
        cy.get('[data-cy=add-button]').click();
        cy.get('[data-cy=user-header]').contains('Create Workflow Group');
        cy.get('[data-cy=name-input]').type(wfsageName).should('have.value', wfsageName);
        
        cy.multiselectFormClick('form-groupRoles','GM - General Manager');
        cy.multiselectFormClick('form-groupUsers','Nati Ben Shushan');

        cy.get('[data-cy=save-button]').click();
        cy.get(".ui-blockui-document", { timeout: 6000 }).should("not.be.visible");
        //end creation

        //validate creation in grid
        cy.get('[data-cy=searchbyname-grid]').type(wfsageName).should('have.value', wfsageName);
        cy.get('tbody').find('tr:first-child td').contains(wfsageName);

        cy.get('@warningMessage').should("not.called")
        cy.get('@errorMessage').should("not.called")
    })
})
