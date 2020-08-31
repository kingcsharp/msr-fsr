
describe('User Functionality', () => {
    it('TC353_AddingUserRole', () => {
        cy.server();
        cy.route({
            method: 'GET',
            url: '/v1/Role',
            response: {
                "object": [{
                    "id": 1,
                    "name": "GM - General Manager",
                    "isCertificationRole": false,
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
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
                            "canRead": true,
                            "canCreate": true,
                            "canEdit": true,
                            "canActivate": true,
                            "canApprove": true,
                            "canDelete": true
                        },
                        "roles": [],
                        "enumMenuItem": 0
                    }],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 2,
                    "name": "CEO",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 3,
                    "name": "CFO",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 4,
                    "name": "CTO",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 5,
                    "name": "COO",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 6,
                    "name": "Office Manager",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 8,
                    "name": "MSR Shipper / Receiver",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 9,
                    "name": "Media Blast Certification",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 10,
                    "name": "Process Engineer",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 11,
                    "name": "Incoming Inspection IL",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 12,
                    "name": "Disassembly IL",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 13,
                    "name": "Soak DI IL",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 14,
                    "name": "Wipe down IPA IL",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 15,
                    "name": "Production Manager",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 16,
                    "name": "MSR Quality Controller",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 17,
                    "name": "Clean Room",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 23,
                    "name": "Client Engineer",
                    "isCertificationRole": false,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false
                }, {
                    "id": 24,
                    "name": "Client Buyer",
                    "isCertificationRole": false,
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
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": true
                }, {
                    "id": 27,
                    "name": "Administrator",
                    "isCertificationRole": false,
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
                        "info": "List of Procedure Step Templates",
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
                        "icon": "fa fa-cog",
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
                    "inheritedPermissions": null,
                    "parentRoles": [{
                        "id": 1,
                        "name": "GM - General Manager",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }],
                    "hasAssignedUsers": true
                }, {
                    "id": 28,
                    "name": "TestRole12",
                    "isCertificationRole": true,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [{
                        "id": 1,
                        "name": "GM - General Manager",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 27,
                        "name": "Administrator",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 30,
                        "name": "TestRole2",
                        "isCertificationRole": true,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 2,
                        "name": "CEO",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 3,
                        "name": "CFO",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }],
                    "hasAssignedUsers": false
                }, {
                    "id": 30,
                    "name": "TestRole2",
                    "isCertificationRole": true,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [{
                        "id": 1,
                        "name": "GM - General Manager",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 2,
                        "name": "CEO",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 5,
                        "name": "COO",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 8,
                        "name": "MSR Shipper / Receiver",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }],
                    "hasAssignedUsers": true
                }, {
                    "id": 31,
                    "name": "test433",
                    "isCertificationRole": true,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [{
                        "id": 1,
                        "name": "GM - General Manager",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }, {
                        "id": 2,
                        "name": "CEO",
                        "isCertificationRole": false,
                        "menus": [],
                        "permissions": null,
                        "inheritedPermissions": null,
                        "parentRoles": [],
                        "hasAssignedUsers": false
                    }],
                    "hasAssignedUsers": false
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
            url: '/v1/Role',
            response:{
                "object": {
                    "id": 32,
                    "name": "MegaSuperAdmin",
                    "isCertificationRole": true,
                    "menus": [],
                    "permissions": null,
                    "inheritedPermissions": null,
                    "parentRoles": [],
                    "hasAssignedUsers": false,
                    "lastUpdatedOn": "2020-08-31T22:43:06.5608974Z",
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
                        "timeZone": null,
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-06-05T19:13:53",
                        "createdBy": 68,
                        "roles": [],
                        "fileModel": null
                    },
                    "createdOn": "2020-08-31T22:43:06.5608974Z",
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
                        "timeZone": null,
                        "lastUpdatedBy": 134,
                        "createdOn": "2020-06-05T19:13:53",
                        "createdBy": 68,
                        "roles": [],
                        "fileModel": null
                    }
                },
                "successMessage": "Role successfully created",
                "errorMessages": [],
                "id": 0,
                "hasErrors": false,
                "hasValidationErrors": false
            }
        });

        cy.beginWebConsoleTracking();
        cy.login('admin');
        cy.navigateToPage('People', 'Roles');
        cy.showDropdownTrackableModel('usergrid-options');

        cy.get('[data-cy=add-button]').click();
        cy.get('[data-cy=isCertificationRole-checkbox]').click();
        

        cy.writeAndValidate('name-input','MegaSuperAdmin');
        cy.multiselectFormClick('parentroles-modal','GM - General Manager');
        cy.get('[data-cy=save-button]').click();
        cy.waitpost();

        cy.writeAndValidate('id','32');
        cy.get('[data-cy=row]').find('td:first').contains('32');

        cy.logout();
        cy.checkWebConsoleTracking();
    });
})

