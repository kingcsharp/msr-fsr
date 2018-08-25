--Seed Menus data
--TODO UPDATE

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Billing-1')
BEGIN
	INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
	VALUES (N'Billing-1', N'Invoices', N'Aug  24 2018 10:18PM', N'SP', 1, N'Invoices', N'Invoices', N'Billing', N'fa-exchange', N'fa-envelope-square', 8, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Billing-2')
BEGIN
	INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Billing-2', N'PurchaseOrder/Purchases', N'Aug  24 2018 10:18PM', N'SP', 2, N'Purchases', N'Purchases', N'Billing', N'fa-usd', N'fa-envelope-square', 8, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Dashboards-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Dashboards-1', N'#', N'Aug  24 2018 10:18PM', N'SP', 1, N'Dashboard A', N'Dashboard A', N'Dashboards', N'fa-area-chart', N'fa-dashboard', 11, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Dashboards-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Dashboards-2', N'#', N'Aug  24 2018 10:18PM', N'SP', 2, N'Dashboard B', N'Dashboard B', N'Dashboards', N'fa-line-chart', N'fa-dashboard', 11, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Dashboards-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Dashboards-3', N'#', N'Aug  24 2018 10:18PM', N'SP', 3, N'Dashboard C', N'Dashboard C', N'Dashboards', N'fa-pie-chart', N'fa-dashboard', 11, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Documents-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Documents-1', N'Documents', N'Aug  24 2018 10:18PM', N'SP', 1, N'Documents', N'Documents', N'Documents', N'fa-file-text', NULL, 15, 1)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Help-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Help-1', N'#', N'Aug  24 2018 10:18PM', N'SP', 1, N'Open a Support Ticket', N'Open a Support Ticket', N'Help', N'fa-question-circle', N'fa-life-bouy', 16, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Help-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Help-2', N'Help', N'Aug  24 2018 10:18PM', N'SP', 2, N'Pages', N'Pages', N'Help', N'fa-list', N'fa-life-bouy', 16, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Locations-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Locations-1', N'Locations', N'Aug  24 2018 10:18PM', N'SP', 1, N'Locations', N'Locations', N'Locations', N'fa-map', N'fa-map-marker', 5, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Locations-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Locations-2', N'Regions', N'Aug  24 2018 10:18PM', N'SP', 2, N'Regions', N'Regions', N'Locations', N'fa-globe', N'fa-map-marker', 5, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Monitors-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Monitors-1', N'Monitors', N'Aug  24 2018 10:18PM', N'SP', 1, N'Monitors', N'Monitors', N'Monitors', N'fa-desktop', N'fa-desktop', 6, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Parts-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Parts-1', N'parts', N'Aug  24 2018 10:18PM', N'SP', 1, N'Parts', N'Parts List', N'Parts', N'fa-cog', N'fa-cogs', 2, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Parts-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Parts-2', N'PartTypes', N'Aug  24 2018 10:18PM', N'SP', 2, N'Part Types', N'Part Types', N'Parts', N'fa-list', N'fa-cogs', 2, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Parts-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Parts-3', N'ActualParts', N'Aug  24 2018 10:18PM', N'SP', 3, N'Actual Parts/Assets', N'Actual Parts/Assets', N'Parts', N'fa-clone', N'fa-cogs', 2, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='People-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'People-1', N'People', N'Aug  24 2018 10:18PM', N'SP', 1, N'Users', N'Users', N'People', N'fa-user', N'fa-users', 4, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='People-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'People-2', N'Roles', N'Aug  24 2018 10:18PM', N'SP', 2, N'User Roles', N'User Roles', N'People', N'fa-group', N'fa-users', 4, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='People-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'People-3', N'Companies', N'Aug  24 2018 10:18PM', N'SP', 3, N'Companies/Departments', N'Companies/Departments', N'People', N'fa-building', N'fa-users', 4, 0)
END
GO

IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Pricing-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Pricing-1', N'ProductionPlanning', N'Aug  24 2018 10:18PM', N'SP', 1, N'Quotes', N'Quotes', N'Pricing', N'fa-paper-plane-o', N'fa-money', 7, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Pricing-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Pricing-2', N'Quote/Create', N'Aug  24 2018 10:18PM', N'SP', 2, N'Freeform Quote', N'Freeform Quote', N'Pricing', N'fa-usd', N'fa-money', 7, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Pricing-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Pricing-3', N'PurchaseOrder', N'Aug  24 2018 10:18PM', N'SP', 3, N'PurchaseOrders', N'PurchaseOrders', N'Pricing', N'fa-list-ul', N'fa-money', 7, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Procedures-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Procedures-1', N'Procedures', N'Aug  24 2018 10:18PM', N'SP', 1, N'Runnable Procedures', N'Runnable Procedures', N'Procedures', N'fa-step-forward', N'fa-puzzle-piece', 3, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Procedures-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Procedures-2', N'ProcedureVerbs', N'Aug  24 2018 10:18PM', N'SP', 2, N'Proecedure Types', N'Proecedure Types', N'Procedures', N'fa-list', N'fa-puzzle-piece', 3, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Procedures-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Procedures-3', N'PreProSearch', N'Aug  24 2018 10:18PM', N'SP', 3, N'Templates', N'Templates', N'Procedures', N'fa-file-text', N'fa-puzzle-piece', 3, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Reporting-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Reporting-1', N'#', N'Aug  24 2018 10:18PM', N'SP', 1, N'Excel Reports', N'Excel Reports', N'Reporting', N' fa-file-excel-o', N'fa-newspaper-o', 10, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Reporting-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Reporting-2', N'#', N'Aug  24 2018 10:18PM', N'SP', 2, N'Purchases', N'Purchases', N'Reporting', N'fa fa-usd', N'fa-newspaper-o', 10, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Utilities-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Utilities-1', N'administration/AssignRoleToJob', N'Aug  24 2018 10:18PM', N'SP', 1, N'Answer Admin', N'Answer Admin', N'Utilities', N'fa-wrench', N'fa-newspaper-o', 12, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Utilities-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Utilities-2', N'EquipmentMaintenance', N'Aug  24 2018 10:18PM', N'SP', 2, N'Equipment Maintenance', N'Equipment Maintenance', N'Utilities', N'fa-plus-square', N'fa-newspaper-o', 12, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Utilities-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Utilities-3', N'delivery', N'Aug  24 2018 10:18PM', N'SP', 3, N'Parts Recieved by Cust', N'Parts Recieved by Cust', N'Utilities', N'fa-truck', N'fa-newspaper-o', 12, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Utilities-4')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Utilities-4', N'AdminCostSettings', N'Aug  24 2018 10:18PM', N'SP', 4, N'Admin Cost Settings', N'Admin Cost Settings', N'Utilities', N'fa fa-cogs', N'fa-newspaper-o', 12, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Wip-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Wip-1', N'wip/statusview', N'Aug  24 2018 10:18PM', N'SP', 1, N'Wip Status', N'WIP Status', N'WIP', N'fa-tachometer', N'fa-list-alt', 1, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Wip-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Wip-2', N'wip', N'Aug  24 2018 10:18PM', N'SP', 2, N'WIP Menu', N'WIP Menu', N'WIP', N'fa-list', N'fa-list-alt', 1, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Workflow-1')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Workflow-1', N'ApprovalWorkflows', N'Aug  24 2018 10:18PM', N'SP', 1, N'Approval Workflows', N'Approval Workflows', N'Workflow', N'fa-align-center', N'fa-align-center', 9, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Workflow-2')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent]) 
VALUES (N'Workflow-2', N'ApprovalStages', N'Aug  24 2018 10:18PM', N'SP', 2, N'Approval Stages', N'Approval Stages', N'Workflow', N'fa-list-ol', N'fa-align-center', 9, 0)
END
GO


IF NOT EXISTS(SELECT * FROM [dbo].[A_MENUS] WHERE ID ='Workflow-3')
BEGIN
INSERT [dbo].[A_MENUS] ([ID], [URL], [DRCM], [MODBY], [NUM], [NAME], [INFO], [MENU_GROUP], [ICON], [GroupIcon], [OrderNumber], [IsParent])
VALUES (N'Workflow-3', N'ApprovalGroups', N'Aug  24 2018 10:18PM', N'SP', 3, N'Approval Groups', N'Approval Groups', N'Workflow', N'fa-group', N'fa-align-center', 9, 0)
END
GO
