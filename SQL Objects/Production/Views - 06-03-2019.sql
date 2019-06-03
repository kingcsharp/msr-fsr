USE [Answer2_Prod]
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Portal_TrainingView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Portal_TrainingView'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_COMPANIES_DROP_SEARCH'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_COMPANIES_DROP_SEARCH'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPaneCount' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane2' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

EXEC sys.sp_dropextendedproperty @name=N'MS_DiagramPane1' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

/****** Object:  View [dbo].[A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS]
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_SPECIAL_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUP_SPECIAL_MEMBERS]
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_REFERENCE_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_HEADER_REFERENCE_FILES]
GO

/****** Object:  View [dbo].[A_V_SURVEY_GET_FILE_ATTACHMENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_GET_FILE_ATTACHMENTS]
GO

/****** Object:  View [dbo].[A_V_FAVORITE_MENU_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FAVORITE_MENU_ITEMS]
GO

/****** Object:  View [dbo].[A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO]
GO

/****** Object:  View [dbo].[A_V_THEORY_PARAGRAPH_DOCUMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_PARAGRAPH_DOCUMENT_DATA]
GO

/****** Object:  View [dbo].[A_V_DOCUMENTS_LINKED]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DOCUMENTS_LINKED]
GO

/****** Object:  View [dbo].[A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO]
GO

/****** Object:  View [dbo].[A_V_APPROVED_PROCEDURE_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_PROCEDURE_STEPS]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_ATTACHMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_ATTACHMENT_DATA]
GO

/****** Object:  View [dbo].[A_O_PREPOP_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PREPOP_HISTORY]
GO

/****** Object:  View [dbo].[A_V_PROJECT_ITEMS_AND_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_ITEMS_AND_NAMES]
GO

/****** Object:  View [dbo].[A_O_DOCUMENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_DOCUMENTS]
GO

/****** Object:  View [dbo].[A_O_COUNTERS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_COUNTERS_HISTORY]
GO

/****** Object:  View [dbo].[A_LOCATIONS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_LOCATIONS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_ORDERS_WITH_QUOTE_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_ORDERS_WITH_QUOTE_STATUS]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_RECENT_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_RECENT_TASKS]
GO

/****** Object:  View [dbo].[A_V_COMPANY_CONTACTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANY_CONTACTS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_OBJECT_LINK_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_OBJECT_LINK_DATA]
GO

/****** Object:  View [dbo].[A_V_FAVORITES_COMPLETE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FAVORITES_COMPLETE]
GO

/****** Object:  View [dbo].[A_V_SURVEY_GET_QUESTIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_GET_QUESTIONS]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_DISCUSSIONS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_DISCUSSIONS_ITEMS]
GO

/****** Object:  View [dbo].[A_V_FILL_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILL_TASKS]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MEETINGS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_MEETINGS_ITEMS]
GO

/****** Object:  View [dbo].[A_V_FILLS_WITH_PRECEDENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILLS_WITH_PRECEDENTS]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MENU_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_MENU_ITEMS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_MONITORS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_MONITORS]
GO

/****** Object:  View [dbo].[A_V_QUOTE_ITEM_PARENTS_USING_ORDER_ITEM_FOR_RELATIONSHIP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_ITEM_PARENTS_USING_ORDER_ITEM_FOR_RELATIONSHIP]
GO

/****** Object:  View [dbo].[A_V_PART_TYPES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PART_TYPES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_DROP_DOWN_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATIONS_DROP_DOWN_DATA]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MESSAGES_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_MESSAGES_ITEMS]
GO

/****** Object:  View [dbo].[A_V_MENUS_WITH_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MENUS_WITH_ROLES]
GO

/****** Object:  View [dbo].[A_V_ORDERS_WITH_QUOTE_STATUS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDERS_WITH_QUOTE_STATUS_INFO]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_HEADER_REFERENCE_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_HEADER_REFERENCE_FILES]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_WITH_EMAILS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_WITH_EMAILS]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COST_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COST_DATA]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_SERVICE_CALLS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_SERVICE_CALLS_ITEMS]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_SURVEYS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_SURVEYS_ITEMS]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_TASK_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_TASK_ITEMS]
GO

/****** Object:  View [dbo].[A_V_APPROVED_PEOPLE_SIMPLE_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_PEOPLE_SIMPLE_SEARCH]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_NEEDS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_NEEDS_ITEMS]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEM_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_ITEM_DATA]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_QUOTE_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_QUOTE_STATUS]
GO

/****** Object:  View [dbo].[A_V_PREPOP_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PREPOP_QUICK]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_OT_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_OT_HOURS]
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS]
GO

/****** Object:  View [dbo].[A_V_TASK_PART_PURCH_ITEM_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_PART_PURCH_ITEM_QUICK]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_PREVIOUS_STEP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_PREVIOUS_STEP]
GO

/****** Object:  View [dbo].[A_V_TASK_QUOTE_ACCEPT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_QUOTE_ACCEPT_INFORMATION]
GO

/****** Object:  View [dbo].[A_V_TASK_REF_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_REF_FILES]
GO

/****** Object:  View [dbo].[A_ACCOUNT_INVOICE_ITEMS_WITH_ACCT_AND_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_ACCOUNT_INVOICE_ITEMS_WITH_ACCT_AND_STATUS]
GO

/****** Object:  View [dbo].[A_V_NEEDS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_NEEDS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER]
GO

/****** Object:  View [dbo].[A_V_THEORY_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_REF_THEORIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_HEADER_REF_THEORIES]
GO

/****** Object:  View [dbo].[A_V_TT_VERBS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TT_VERBS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_WF_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_STAGES]
GO

/****** Object:  View [dbo].[A_V_EMAILS_FOR_APPROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_EMAILS_FOR_APPROVED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PERFERENCES_GET_DATA_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_PERFERENCES_GET_DATA_BY_NTLOGIN]
GO

/****** Object:  View [dbo].[A_V_COUNTERS_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COUNTERS_BY_APPROVED_ID]
GO

/****** Object:  View [dbo].[A_V_QUOTE_ITEMS_WITH_QUOTE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_ITEMS_WITH_QUOTE_DATA]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PERFERENCES_GET_HISTORY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_PERFERENCES_GET_HISTORY_ID]
GO

/****** Object:  View [dbo].[A_V_TASK_WITH_QUOTE_ORDER_LINK_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_WITH_QUOTE_ORDER_LINK_INFO]
GO

/****** Object:  View [dbo].[Portal_Languages]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_Languages]
GO

/****** Object:  View [dbo].[A_O_NOUN_HIERARCHIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_NOUN_HIERARCHIES]
GO

/****** Object:  View [dbo].[A_V_INVOICE_TASK_STOP_DATES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_INVOICE_TASK_STOP_DATES]
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER_II]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER_II]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_PROC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_PROC_DATA]
GO

/****** Object:  View [dbo].[Portal_RolesApprovedDataQuick]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_RolesApprovedDataQuick]
GO

/****** Object:  View [dbo].[Portal_TimeZones]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_TimeZones]
GO

/****** Object:  View [dbo].[Portal_TimeZoneView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_TimeZoneView]
GO

/****** Object:  View [dbo].[A_V_MEETING_AGENDA_ITEM_GET_DOC_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_AGENDA_ITEM_GET_DOC_INFO]
GO

/****** Object:  View [dbo].[Portal_UserView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_UserView]
GO

/****** Object:  View [dbo].[Portal_ViewHistryDetails]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ViewHistryDetails]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP]
GO

/****** Object:  View [dbo].[Portal_WorkflowStages]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_WorkflowStages]
GO

/****** Object:  View [dbo].[Portal_MenuView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_MenuView]
GO

/****** Object:  View [dbo].[A_V_OBJECTS_WITH_LOCKED_BY_BOSS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_OBJECTS_WITH_LOCKED_BY_BOSS]
GO

/****** Object:  View [dbo].[A_V_WORKFLOWS_WITH_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WORKFLOWS_WITH_STAGES]
GO

/****** Object:  View [dbo].[A_APPROVED_ROLE_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_ROLE_ASSIGNEES]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_FILE_ATTACHMENT_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_GET_FILE_ATTACHMENT_INFO]
GO

/****** Object:  View [dbo].[A_V_TASK_IS_QUICK_PRICE_CHECK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_IS_QUICK_PRICE_CHECK]
GO

/****** Object:  View [dbo].[A_O_FORECASTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_FORECASTS]
GO

/****** Object:  View [dbo].[A_APPROVED_REGIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_REGIONS]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_PARENT_ITEM_NAM]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_PARENT_ITEM_NAM]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_TASKS]
GO

/****** Object:  View [dbo].[A_V_FORECASTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECASTS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_WF_PENDING_APPROVALS_WITH_STARTER_AND_INITER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_PENDING_APPROVALS_WITH_STARTER_AND_INITER]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_HISTORY_WITH_ITEMS_AND_FILLS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_HISTORY_WITH_ITEMS_AND_FILLS]
GO

/****** Object:  View [dbo].[Portal_ActivitiesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ActivitiesView]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_ROLES_TO_VIEW]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_ROLES_TO_VIEW]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_ROLES]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_ROLE_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_ROLE_NAMES]
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_ROLES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_ALLOWED_ROLES_DATA]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_ROLES]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_ROLES]
GO

/****** Object:  View [dbo].[A_V_NAV_HISTORY_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_NAV_HISTORY_WITH_NAMES]
GO

/****** Object:  View [dbo].[A_V_ENGINEER_REPORTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ENGINEER_REPORTS]
GO

/****** Object:  View [dbo].[A_O_PROPOSALS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PROPOSALS]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_COMPANY]
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_COMPANIES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_ALLOWED_COMPANIES_DATA]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_COMPANY]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_COMPANY]
GO

/****** Object:  View [dbo].[Portal_ApprovedCompaniesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ApprovedCompaniesView]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_PAYMENT_LOCATION_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_PAYMENT_LOCATION_NAMES]
GO

/****** Object:  View [dbo].[A_V_APPROVED_COMPANIES_WITH_ADDRESS_AND_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_COMPANIES_WITH_ADDRESS_AND_LOGOS]
GO

/****** Object:  View [dbo].[A_V_APPROVALS_PENDING]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVALS_PENDING]
GO

/****** Object:  View [dbo].[A_V_SURVEY_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_BY_ID]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_BOSS_SUB_LOOKUP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_BOSS_SUB_LOOKUP]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_WITH_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_WITH_COMPANIES]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_SEARCH]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_TIME_ZONE_WITH_PERSON_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TIME_ZONE_WITH_PERSON_ID]
GO

/****** Object:  View [dbo].[A_V_WF_STARTED_WITH_DENIAL_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_STARTED_WITH_DENIAL_INFO]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_ORG_CHART_TREE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_ORG_CHART_TREE_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_ROLES_WITH_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_ROLES_WITH_ASSIGNEES]
GO

/****** Object:  View [dbo].[A_V_SURVEY_WITH_INITIAL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_WITH_INITIAL_DATA]
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUP_MEMBERS]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_SURVEY_SEARCH_ALL_INVITED]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_SEARCH_ALL_INVITED]
GO

/****** Object:  View [dbo].[Protal_PendingApprovals]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Protal_PendingApprovals]
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLE_PEOPLE_2]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUP_ROLE_PEOPLE_2]
GO

/****** Object:  View [dbo].[Portal_PurchaseOrders]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PurchaseOrders]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_PRODUCTS]
GO

/****** Object:  View [dbo].[A_V_ORDERS_SHARED_SUPPLIER_LIST]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDERS_SHARED_SUPPLIER_LIST]
GO

/****** Object:  View [dbo].[A_O_NEEDS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_NEEDS]
GO

/****** Object:  View [dbo].[A_V_PURCHASES_WITH_SUPPLIER_QUOTES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASES_WITH_SUPPLIER_QUOTES]
GO

/****** Object:  View [dbo].[A_V_PRODUCT_WITH_SUPPLIER_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCT_WITH_SUPPLIER_INFO]
GO

/****** Object:  View [dbo].[A_O_ACCOUNTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_ACCOUNTS]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PAGE_HITS_RAW_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_PAGE_HITS_RAW_DATA]
GO

/****** Object:  View [dbo].[A_V_THEORY_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_HISTORY_SEARCH]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_ROLES]
GO

/****** Object:  View [dbo].[A_V_PARTS_SAFETY_STOCK_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_SAFETY_STOCK_ROLES]
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES]
GO

/****** Object:  View [dbo].[Portal_ApprovedPeople]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ApprovedPeople]
GO

/****** Object:  View [dbo].[Portal_ProductsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProductsView]
GO

/****** Object:  View [dbo].[Portal_ProcedureListView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProcedureListView]
GO

/****** Object:  View [dbo].[Portal_ApprovalStagesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ApprovalStagesView]
GO

/****** Object:  View [dbo].[A_V_WF_STAGES_WITH_GROUPS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_STAGES_WITH_GROUPS]
GO

/****** Object:  View [dbo].[A_O_WF_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_WF_STAGES]
GO

/****** Object:  View [dbo].[Portal_ProductsActualPart]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProductsActualPart]
GO

/****** Object:  View [dbo].[Portal_PeopleObjectSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PeopleObjectSearchView]
GO

/****** Object:  View [dbo].[A_V_MEETING_LOC_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_LOC_WITH_NAMES]
GO

/****** Object:  View [dbo].[A_V_PARTS_GET_SUB_PART_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_GET_SUB_PART_DATA]
GO

/****** Object:  View [dbo].[A_V_PARTS_SAFETY_STOCK_LEVELS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_SAFETY_STOCK_LEVELS]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_ROLES_SENT_TO_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_ROLES_SENT_TO_DATA]
GO

/****** Object:  View [dbo].[A_V_PERSON_ROLES_WITH_SECURITY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PERSON_ROLES_WITH_SECURITY]
GO

/****** Object:  View [dbo].[A_V_TASK_QUOTE_ORDER_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_QUOTE_ORDER_INFORMATION]
GO

/****** Object:  View [dbo].[A_V_PROPOSAL_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROPOSAL_ORDERS]
GO

/****** Object:  View [dbo].[A_V_TASK_WITH_ORDER_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_WITH_ORDER_INFORMATION]
GO

/****** Object:  View [dbo].[A_V_INVOICES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_INVOICES]
GO

/****** Object:  View [dbo].[A_V_EXTERNAL_PARTS_WITH_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_EXTERNAL_PARTS_WITH_COMPANY]
GO

/****** Object:  View [dbo].[A_V_PARTS_EXTERNAL_PART_LOOKUP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_EXTERNAL_PART_LOOKUP]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_WITH_REF_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_WITH_REF_PROCEDURES]
GO

/****** Object:  View [dbo].[A_V_THEORY_PARAGRAPH_WITH_REF_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_PARAGRAPH_WITH_REF_THEORY]
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_WITH_REF_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_THEORY_HEADER_WITH_REF_THEORY]
GO

/****** Object:  View [dbo].[A_V_OBJECTS_MAX_ID_WITH_ROOT]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_OBJECTS_MAX_ID_WITH_ROOT]
GO

/****** Object:  View [dbo].[A_V_A_CUSTOMER_PART_FILE_LINK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_A_CUSTOMER_PART_FILE_LINK]
GO

/****** Object:  View [dbo].[Portal_EquipmentMaintenanceView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_EquipmentMaintenanceView]
GO

/****** Object:  View [dbo].[Portal_LocationsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_LocationsView]
GO

/****** Object:  View [dbo].[A_O_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_LOCATIONS]
GO

/****** Object:  View [dbo].[Portal_ProcedureObjectLaborStepsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProcedureObjectLaborStepsView]
GO

/****** Object:  View [dbo].[A_V_JOB_DESCRIPTION_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_JOB_DESCRIPTION_SEARCH_DATA]
GO

/****** Object:  View [dbo].[A_V_DESCRIPTION_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DESCRIPTION_SEARCH]
GO

/****** Object:  View [dbo].[Portal_TheoryParagraphsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_TheoryParagraphsView]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WEEKLY_REPORT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_WEEKLY_REPORT_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_TOTAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_TOTAL_HOURS]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WORK_TIME_WITH_DATE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_WORK_TIME_WITH_DATE]
GO

/****** Object:  View [dbo].[A_APPROVED_PARTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_PARTS]
GO

/****** Object:  View [dbo].[Portal_PartTypesApprovedVIew]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PartTypesApprovedVIew]
GO

/****** Object:  View [dbo].[A_V_SURVEY_WITH_AUTHOR_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SURVEY_WITH_AUTHOR_NAMES]
GO

/****** Object:  View [dbo].[A_APRROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APRROVED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_Z_FAV]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAV]
GO

/****** Object:  View [dbo].[A_V_ROLES_ASSIGNED_ROLES_TO_A_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_ASSIGNED_ROLES_TO_A_ROLE]
GO

/****** Object:  View [dbo].[Portal_PartTypesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PartTypesView]
GO

/****** Object:  View [dbo].[A_O_PART_TYPES_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PART_TYPES_HISTORY]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_PROCEDURE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_PROCEDURE_ITEMS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN]
GO

/****** Object:  View [dbo].[A_DNR_TASKS_WITH_MONITOR_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_DNR_TASKS_WITH_MONITOR_INFO]
GO

/****** Object:  View [dbo].[A_V_DNR_TASKS_WITH_CORRECTIVE_ACTIONS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DNR_TASKS_WITH_CORRECTIVE_ACTIONS_INFO]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_DATA_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_APPROVED_DATA_BY_ID]
GO

/****** Object:  View [dbo].[A_V_DNR_TASKS_WITH_TEST_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DNR_TASKS_WITH_TEST_INFO]
GO

/****** Object:  View [dbo].[Portal_PrePropSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PrePropSearchView]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_DOCUMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_DOCUMENT_DATA]
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_SELECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURES_SELECT_SEARCH]
GO

/****** Object:  View [dbo].[A_O_PROCEDURES_WITH_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PROCEDURES_WITH_STEPS]
GO

/****** Object:  View [dbo].[Portal_ProceduresVerbsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProceduresVerbsView]
GO

/****** Object:  View [dbo].[Portal_ApprovalGroupsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ApprovalGroupsView]
GO

/****** Object:  View [dbo].[A_V_WF_GROUPS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUPS]
GO

/****** Object:  View [dbo].[Portal_MonitorResults]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_MonitorResults]
GO

/****** Object:  View [dbo].[A_V_MONITOR_TEMPLATES_WITH_RESULTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MONITOR_TEMPLATES_WITH_RESULTS]
GO

/****** Object:  View [dbo].[Portal_InvoiceView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_InvoiceView]
GO

/****** Object:  View [dbo].[A_V_LINKED_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LINKED_IDS]
GO

/****** Object:  View [dbo].[A_V_QUOTE_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_HISTORY_SEARCH]
GO

/****** Object:  View [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFORMATION]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_BASIC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_BASIC_DATA]
GO

/****** Object:  View [dbo].[A_V_NOTES_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_NOTES_SEARCH_DATA]
GO

/****** Object:  View [dbo].[A_V_LINKED_LOGINS_PEOPLE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LINKED_LOGINS_PEOPLE_DATA]
GO

/****** Object:  View [dbo].[Portal_HeadPeopleView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_HeadPeopleView]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_PERSON_TIME_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_PERSON_TIME_DATA]
GO

/****** Object:  View [dbo].[Portal_ProductsSearchDataView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProductsSearchDataView]
GO

/****** Object:  View [dbo].[Portal_RegionsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_RegionsView]
GO

/****** Object:  View [dbo].[A_O_REGIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_REGIONS]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_INVOICE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_INVOICE_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_PAYMENT_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_PAYMENT_LOCATIONS]
GO

/****** Object:  View [dbo].[A_V_APPROVED_COMPANIES_WITH_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_COMPANIES_WITH_LOGOS]
GO

/****** Object:  View [dbo].[A_V_COMPANIES_LOCATION_ADDRESSES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANIES_LOCATION_ADDRESSES]
GO

/****** Object:  View [dbo].[A_APPROVED_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_LOCATIONS]
GO

/****** Object:  View [dbo].[Portal_PurchaseView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PurchaseView]
GO

/****** Object:  View [dbo].[A_O_PURCHASES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PURCHASES]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_OBJ_LINK_DATA_FOR_SUMMING]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_OBJ_LINK_DATA_FOR_SUMMING]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS]
GO

/****** Object:  View [dbo].[A_Z_UNITS_TIME_TO_SECS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_Z_UNITS_TIME_TO_SECS]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS]
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_GET_BY_ID]
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_TKEEP_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_GET_TKEEP_NAME]
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_SCRIBE_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_GET_SCRIBE_NAME]
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_HOST_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_GET_HOST_NAME]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PEOPLE_SENT_TO_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_PEOPLE_SENT_TO_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_SEARCH_DATA]
GO

/****** Object:  View [dbo].[A_V_TASK_SEARCH_STREAM_LINED]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_SEARCH_STREAM_LINED]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_SEARCH_DATA]
GO

/****** Object:  View [dbo].[A_V_PROJECT_SEARCH_ALL_ALLOWED]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_SEARCH_ALL_ALLOWED]
GO

/****** Object:  View [dbo].[A_V_PROJECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_SEARCH]
GO

/****** Object:  View [dbo].[A_V_ORDER_MAIN_DATA_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_MAIN_DATA_BY_APPROVED_ID]
GO

/****** Object:  View [dbo].[A_V_PROJECT_MEMBER_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_MEMBER_DATA]
GO

/****** Object:  View [dbo].[A_V_PROJECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_DATA]
GO

/****** Object:  View [dbo].[A_V_MEETING_EMAIL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_EMAIL_DATA]
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_AGENDA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_GET_AGENDA]
GO

/****** Object:  View [dbo].[A_V_MEETING_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_SEARCH]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_VIEW_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_VIEW_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_TOTAL_OT_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_TOTAL_OT_HOURS]
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_PEOPLE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_ALLOWED_PEOPLE_DATA]
GO

/****** Object:  View [dbo].[A_V_MEETING_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_TASKS]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID]
GO

/****** Object:  View [dbo].[A_V_QUOTE_ORDER_LINK_WITH_COMPANY_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_ORDER_LINK_WITH_COMPANY_NAMES]
GO

/****** Object:  View [dbo].[A_V_MEETING_REPONDANT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MEETING_REPONDANT_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_STATUS_HISTORY_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_STATUS_HISTORY_DATA]
GO

/****** Object:  View [dbo].[A_V_PROJECT_SPONSOR_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROJECT_SPONSOR_DATA]
GO

/****** Object:  View [dbo].[A_V_PROD_REQ_FORM_DATES_WITH_PEOPLE_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_REQ_FORM_DATES_WITH_PEOPLE_NAME]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_TREE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_GET_TREE_DATA]
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_WITH_INITIAL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DISCUSSION_WITH_INITIAL_DATA]
GO

/****** Object:  View [dbo].[Report_WorkInProgress]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Report_WorkInProgress]
GO

/****** Object:  View [dbo].[Report_SerialNumberHistory]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Report_SerialNumberHistory]
GO

/****** Object:  View [dbo].[Report_CombinedFinancialData]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Report_CombinedFinancialData]
GO

/****** Object:  View [dbo].[A_V_PROD_SUPPLIER_APP_OBJ]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_SUPPLIER_APP_OBJ]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_TRAVEL_TO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEM_TRAVEL_TO]
GO

/****** Object:  View [dbo].[A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS]
GO

/****** Object:  View [dbo].[A_O_PROD_PRICE_LISTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PROD_PRICE_LISTS]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA]
GO

/****** Object:  View [dbo].[A_Z_UNITS_BASE_UNIT_CONVERTER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_Z_UNITS_BASE_UNIT_CONVERTER]
GO

/****** Object:  View [dbo].[A_V_APPROVED_ROLES_WITH_NTLOGIN_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_ROLES_WITH_NTLOGIN_ID]
GO

/****** Object:  View [dbo].[A_APPROVED_NOUN_HIERARCHIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_NOUN_HIERARCHIES]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_APPLICABLE_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_APPLICABLE_OBJECTS]
GO

/****** Object:  View [dbo].[A_V_TASK_OBJECT_LINK_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_OBJECT_LINK_ALL_DATA]
GO

/****** Object:  View [dbo].[A_V_TASK_ACTION_OBJECT_LINK_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_ACTION_OBJECT_LINK_ALL_DATA]
GO

/****** Object:  View [dbo].[A_V_NOUN_HIER_CHILDREN_EDITING_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_NOUN_HIER_CHILDREN_EDITING_DATA]
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_WITH_RELATED_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNTS_WITH_RELATED_OBJECTS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_LABOR]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_FOR_PURCHASING_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_FOR_PURCHASING_DATA]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_SELECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_APPROVED_SELECT_DATA]
GO

/****** Object:  View [dbo].[Protal_ObjectsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Protal_ObjectsView]
GO

/****** Object:  View [dbo].[A_O_NOUN_HIER_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_NOUN_HIER_HISTORY]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES]
GO

/****** Object:  View [dbo].[Portal_PeopleView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PeopleView]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_OBJECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_OBJECT_SEARCH]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_DATA]
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PARENT_MESSAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MESSAGES_PARENT_MESSAGES]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_OWNER_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_OWNER_ROLE]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_SIMPLE_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_SIMPLE_SEARCH]
GO

/****** Object:  View [dbo].[A_O_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PEOPLE]
GO

/****** Object:  View [dbo].[Portal_CompanyView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_CompanyView]
GO

/****** Object:  View [dbo].[A_O_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_COMPANIES]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEM_WITH_SUP_AND_CUST]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_ITEM_WITH_SUP_AND_CUST]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_WEEKLY_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_WEEKLY_DATA]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_ACC_RECEIVABLE_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_ACC_RECEIVABLE_ROLE]
GO

/****** Object:  View [dbo].[Portal_FilesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_FilesView]
GO

/****** Object:  View [dbo].[A_V_FILES_SEARCH_BY_SUBORDINATE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILES_SEARCH_BY_SUBORDINATE]
GO

/****** Object:  View [dbo].[A_V_FILES_WITH_SOURCE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILES_WITH_SOURCE]
GO

/****** Object:  View [dbo].[Portal_BuyerView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_BuyerView]
GO

/****** Object:  View [dbo].[A_V_FILLS_ACTUAL_PART_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILLS_ACTUAL_PART_SEARCH]
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATIONS_BY_APPROVED_ID]
GO

/****** Object:  View [dbo].[A_O_EQUIP_EXP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_EQUIP_EXP]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_PARTS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_Z_FAVORITES_PARTS_ITEMS]
GO

/****** Object:  View [dbo].[Portal_PartsApprovedView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PartsApprovedView]
GO

/****** Object:  View [dbo].[A_V_PART_DATA_BY_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PART_DATA_BY_APPROVED_DATA]
GO

/****** Object:  View [dbo].[Portal_TrainingView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_TrainingView]
GO

/****** Object:  View [dbo].[Portal_HelpView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_HelpView]
GO

/****** Object:  View [dbo].[Portal_RolesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_RolesView]
GO

/****** Object:  View [dbo].[A_V_ROLES_WITH_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_WITH_ASSIGNEES]
GO

/****** Object:  View [dbo].[A_O_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_ROLES]
GO

/****** Object:  View [dbo].[Portal_FileSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_FileSearchView]
GO

/****** Object:  View [dbo].[A_V_FILLS_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FILLS_SEARCH]
GO

/****** Object:  View [dbo].[A_O_PARTS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PARTS_HISTORY]
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA]
GO

/****** Object:  View [dbo].[A_V_INVOICE_ITEMS_WITH_SUPPLIER_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_INVOICE_ITEMS_WITH_SUPPLIER_DATA]
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified]
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER]
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP]
GO

/****** Object:  View [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_FORECAST_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_FORECAST_DATA]
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_PURCHASABLE_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNTS_PURCHASABLE_ORDERS]
GO

/****** Object:  View [dbo].[A_V_ORDERS_LOOK_UP_FOR_ACCOUNT]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDERS_LOOK_UP_FOR_ACCOUNT]
GO

/****** Object:  View [dbo].[A_O_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_ORDERS]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEMS]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_BY_APPROVED_ID]
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEM_EDIT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_FORECAST_ITEM_EDIT_DATA]
GO

/****** Object:  View [dbo].[Portal_InvoicesView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_InvoicesView]
GO

/****** Object:  View [dbo].[A_V_INVOICES_WITH_ACCT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_INVOICES_WITH_ACCT_INFORMATION]
GO

/****** Object:  View [dbo].[Portal_ObjectSearch]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ObjectSearch]
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_DATA]
GO

/****** Object:  View [dbo].[Portal_DocumentsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_DocumentsView]
GO

/****** Object:  View [dbo].[A_V_OBJECT_REVISION_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_OBJECT_REVISION_DATA]
GO

/****** Object:  View [dbo].[A_O_THEORY_WITH_PARAGRAPHS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_THEORY_WITH_PARAGRAPHS]
GO

/****** Object:  View [dbo].[A_O_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_THEORY]
GO

/****** Object:  View [dbo].[Portal_CustomerRequirementView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_CustomerRequirementView]
GO

/****** Object:  View [dbo].[A_V_PRODUCT_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCT_SEARCH_DATA]
GO

/****** Object:  View [dbo].[A_V_COMPANIES_DROP_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANIES_DROP_SEARCH]
GO

/****** Object:  View [dbo].[Portal_ProceduresView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ProceduresView]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_HISTORY_SEARCH]
GO

/****** Object:  View [dbo].[A_O_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PROCEDURES]
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLES_WITH_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUP_ROLES_WITH_MEMBERS]
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLE_LINK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WF_GROUP_ROLE_LINK]
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_PEOPLE]
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_WITH_PEOPLE_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLES_APPROVED_WITH_PEOPLE_IDS]
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURE_STEP_LABOR]
GO

/****** Object:  View [dbo].[A_V_TASK_EDIT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_EDIT_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_PROCEDURES]
GO

/****** Object:  View [dbo].[A_V_TASK_ASSIGNMENT_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_ASSIGNMENT_HISTORY]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_WITH_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_PRODUCTS]
GO

/****** Object:  View [dbo].[A_O_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_PRODUCTS]
GO

/****** Object:  View [dbo].[A_V_APPROVED_ROLES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_ROLES_DATA]
GO

/****** Object:  View [dbo].[A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE]
GO

/****** Object:  View [dbo].[A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA]
GO

/****** Object:  View [dbo].[A_V_ADMIN_ROLE_JOBS_WITH_CO_AND_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ADMIN_ROLE_JOBS_WITH_CO_AND_ROLE]
GO

/****** Object:  View [dbo].[A_V_ROLE_DATA_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ROLE_DATA_BY_APPROVED_ID]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEMS_DATA_WITH_SHIPPING]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEMS_DATA_WITH_SHIPPING]
GO

/****** Object:  View [dbo].[A_V_APPROVED_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_APPROVED_OBJECTS]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_SHIPPING]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEM_SHIPPING]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEMS_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEMS_ALL_DATA]
GO

/****** Object:  View [dbo].[A_V_PARTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROD_PRICE_LIST_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_TRAVEL_FROM]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEM_TRAVEL_FROM]
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_SHIPPING_FROM]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_ITEM_SHIPPING_FROM]
GO

/****** Object:  View [dbo].[Portal_ApprovalWorkflowsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ApprovalWorkflowsView]
GO

/****** Object:  View [dbo].[A_V_WORKFLOWS_FOR_ACTIVITIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WORKFLOWS_FOR_ACTIVITIES]
GO

/****** Object:  View [dbo].[A_O_WORKFLOWS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_WORKFLOWS]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_WITH_NORMAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_WITH_NORMAL_HOURS]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WORK_TYPE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALL_WORK_TYPE_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_ROLES]
GO

/****** Object:  View [dbo].[A_APPROVED_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_COMPANIES]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_SELF_BOSS_IF_NULL]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_SELF_BOSS_IF_NULL]
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_NORMAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SERVICE_CALLS_NORMAL_HOURS]
GO

/****** Object:  View [dbo].[Portal_ViewHistryActualPartDetails]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ViewHistryActualPartDetails]
GO

/****** Object:  View [dbo].[Portal_ActualPartsViewHistory]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ActualPartsViewHistory]
GO

/****** Object:  View [dbo].[A_CUSTOMER_PART_FILE_LINK_2]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_CUSTOMER_PART_FILE_LINK_2]
GO

/****** Object:  View [dbo].[A_V_WIP_REPORT_VIEW]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WIP_REPORT_VIEW]
GO

/****** Object:  View [dbo].[A_V_MONITOR_LABEL_PURCHASE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MONITOR_LABEL_PURCHASE_ITEMS]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_RELATED_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_RELATED_FILES]
GO

/****** Object:  View [dbo].[A_V_DELIVERY_TICKET_PURCH_ITEM_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DELIVERY_TICKET_PURCH_ITEM_INFO]
GO

/****** Object:  View [dbo].[A_V_SHIPPER_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SHIPPER_TASKS]
GO

/****** Object:  View [dbo].[A_V_DNR_ACTUAL_PARTS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DNR_ACTUAL_PARTS_INFO]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_TASK_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_TASK_ID]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_INSTALLED_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_INSTALLED_PRODUCTS]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_CALL_DATA_COMPLETE]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_CALL_DATA_COMPLETE]
GO

/****** Object:  View [dbo].[A_V_SHIPPING_TASK_FROM_BATCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_SHIPPING_TASK_FROM_BATCH]
GO

/****** Object:  View [dbo].[A_V_COMPANY_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANY_LOGOS]
GO

/****** Object:  View [dbo].[A_V_BATCH_TASK_CHILDREN]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_BATCH_TASK_CHILDREN]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON]
GO

/****** Object:  View [dbo].[A_V_LOCATION_WITH_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATION_WITH_NAME]
GO

/****** Object:  View [dbo].[A_V_MONITORS_WITH_TASK_AND_RESULT]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_MONITORS_WITH_TASK_AND_RESULT]
GO

/****** Object:  View [dbo].[Portal_MonitorView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_MonitorView]
GO

/****** Object:  View [dbo].[Portal_MonitorsWithTaskAndResults]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_MonitorsWithTaskAndResults]
GO

/****** Object:  View [dbo].[Portal_MonitorsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_MonitorsView]
GO

/****** Object:  View [dbo].[A_V_TASK_PURCHASE_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_PURCHASE_INFORMATION]
GO

/****** Object:  View [dbo].[A_V_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOGOS]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNTS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_TASK_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_TASK_SEARCH]
GO

/****** Object:  View [dbo].[A_V_CO_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_CO_BY_NTLOGIN]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_BY_NTLOGIN]
GO

/****** Object:  View [dbo].[Portal_ActualPartApprovedView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ActualPartApprovedView]
GO

/****** Object:  View [dbo].[Portal_WorkOrders]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_WorkOrders]
GO

/****** Object:  View [dbo].[Portal_PartsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_PartsView]
GO

/****** Object:  View [dbo].[A_V_DOCUMENTS_WITH_LINKED_ITEM]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_DOCUMENTS_WITH_LINKED_ITEM]
GO

/****** Object:  View [dbo].[A_APPROVED_PART_TYPES]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_PART_TYPES]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_QUICK]
GO

/****** Object:  View [dbo].[A_V_ORDER_WITH_QUOTE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDER_WITH_QUOTE_ITEMS]
GO

/****** Object:  View [dbo].[A_V_QUOTES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_QUOTES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART_1]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART_1]
GO

/****** Object:  View [dbo].[A_O_ACTUAL_PARTS_HISTORY_II]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_ACTUAL_PARTS_HISTORY_II]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART]
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_APPROVED_VERBS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_APPROVED_VERBS]
GO

/****** Object:  View [dbo].[A_O_TT_VERBS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_TT_VERBS_HISTORY]
GO

/****** Object:  View [dbo].[A_V_WORKER_SCREEN_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_WORKER_SCREEN_TASKS]
GO

/****** Object:  View [dbo].[A_V_PURCHASES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PURCHASES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_ORDERS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ORDERS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACCOUNTS_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PROCEDURES_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PRODUCTS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_ACTUAL_PARTS_APPROVED_WITH_OBJECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_ACTUAL_PARTS_APPROVED_WITH_OBJECT_DATA]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATIONS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_COMPANIES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANIES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_PARTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PARTS_APPROVED_DATA]
GO

/****** Object:  View [dbo].[Portal_ActualPartsView]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[Portal_ActualPartsView]
GO

/****** Object:  View [dbo].[A_O_ACTUAL_PARTS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_O_ACTUAL_PARTS_HISTORY]
GO

/****** Object:  View [dbo].[A_V_PEOPLE_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_PEOPLE_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_LOCATIONS_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_COMPANIES_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_COMPANIES_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
DROP VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK]
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_ACTUAL_PARTS.ID, dbo.A_ACTUAL_PARTS.HISTORY_REF_ID, dbo.A_ACTUAL_PARTS_HISTORY.OBJECT_ID, 
                      dbo.A_ACTUAL_PARTS_HISTORY.LOCATION, dbo.A_ACTUAL_PARTS_HISTORY.NICK_NAME, dbo.A_ACTUAL_PARTS_HISTORY.MERGABLE, 
                      dbo.A_ACTUAL_PARTS_HISTORY.PARENT_ID, dbo.A_ACTUAL_PARTS_HISTORY.PART_ID, dbo.A_ACTUAL_PARTS_HISTORY.QTY, 
                      dbo.A_ACTUAL_PARTS_HISTORY.SERIAL, dbo.A_ACTUAL_PARTS_HISTORY.CUR_OWNER, dbo.A_ACTUAL_PARTS_HISTORY.ASSEMBLY_WT, 
                      dbo.A_ACTUAL_PARTS_HISTORY.AP_STATUS, dbo.A_ACTUAL_PARTS_HISTORY.ROOT_ID, dbo.A_ACTUAL_PARTS_HISTORY.ROOT_STATUS, 
                      dbo.A_ACTUAL_PARTS_HISTORY.MODIFIED, dbo.A_ACTUAL_PARTS_HISTORY.SYS_NAME, dbo.A_ACTUAL_PARTS_HISTORY.PREV_OWNER, 
                      dbo.A_ACTUAL_PARTS_HISTORY.SUB_PART_ACTION, dbo.A_ACTUAL_PARTS.STATUS, dbo.A_ACTUAL_PARTS_HISTORY.HAS_CHILD, 
                      dbo.A_ACTUAL_PARTS_HISTORY.RESPONSIBLE_PERSON
FROM         dbo.A_ACTUAL_PARTS_HISTORY INNER JOIN
                      dbo.A_ACTUAL_PARTS ON dbo.A_ACTUAL_PARTS_HISTORY.ID = dbo.A_ACTUAL_PARTS.HISTORY_REF_ID
WHERE     (dbo.A_ACTUAL_PARTS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_COMPANIES_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_COMPANIES_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_COMPANIES.ID, dbo.A_COMPANIES.HISTORY_REF_ID, dbo.A_COMPANIES_HISTORY.OBJECT_ID, dbo.A_COMPANIES_HISTORY.NAME, 
                      dbo.A_COMPANIES_HISTORY.CO_TYPE, dbo.A_COMPANIES_HISTORY.PARENT, dbo.A_COMPANIES_HISTORY.PHONE, dbo.A_COMPANIES_HISTORY.LOCATION, 
                      dbo.A_COMPANIES_HISTORY.LOCATION_NAME, dbo.A_COMPANIES_HISTORY.PARENT_NAME, dbo.A_COMPANIES_HISTORY.DRCM, 
                      dbo.A_COMPANIES_HISTORY.MODBY, dbo.A_COMPANIES_HISTORY.ROOT_CO_ID
FROM         dbo.A_COMPANIES INNER JOIN
                      dbo.A_COMPANIES_HISTORY ON dbo.A_COMPANIES.HISTORY_REF_ID = dbo.A_COMPANIES_HISTORY.ID
WHERE     (dbo.A_COMPANIES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LOCATIONS_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, dbo.A_LOCATIONS_HISTORY.NAME, dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION_NAME, dbo.A_LOCATIONS_HISTORY.ADDRESS_1, dbo.A_LOCATIONS_HISTORY.ADDRESS_2, 
                      dbo.A_LOCATIONS_HISTORY.FULL_ADDRESS, dbo.A_LOCATIONS_HISTORY.CITY, dbo.A_LOCATIONS_HISTORY.STATE, 
                      dbo.A_LOCATIONS_HISTORY.COUNTRY, dbo.A_LOCATIONS_HISTORY.POSTAL_CODE, dbo.A_LOCATIONS_HISTORY.REGION, 
                      dbo.A_LOCATIONS_HISTORY.REGION_NAME, dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS, dbo.A_LOCATIONS_HISTORY.OBJECT_ID, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_PATH, dbo.A_LOCATIONS_HISTORY.COMPLETE_NAME
FROM         dbo.A_LOCATIONS_HISTORY INNER JOIN
                      dbo.A_LOCATIONS ON dbo.A_LOCATIONS_HISTORY.ID = dbo.A_LOCATIONS.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_DATA_QUICK]
AS
SELECT     dbo.A_PEOPLE.ID, dbo.A_PEOPLE.HISTORY_REF_ID, dbo.A_PEOPLE_HISTORY.NAME, dbo.A_PEOPLE_HISTORY.LAST_NAME, 
                      dbo.A_PEOPLE_HISTORY.MIDDLE_NAME, dbo.A_PEOPLE_HISTORY.COMPANY, dbo.A_PEOPLE_HISTORY.TIME_ZONE, 
                      dbo.A_PEOPLE_HISTORY.FULL_NAME, dbo.A_PEOPLE_HISTORY.SYSTEM_STATUS, dbo.A_PEOPLE_HISTORY.CO_POSITION, 
                      dbo.A_PEOPLE_HISTORY.ROOT_COMPANY, dbo.A_PEOPLE_HISTORY.TOOL_BOX, dbo.A_PEOPLE_HISTORY.INFO_BOX, 
                      dbo.A_PEOPLE_HISTORY.ADV_SEARCH, dbo.A_PEOPLE_HISTORY.COLOR_KEY, dbo.A_PEOPLE_HISTORY.LOGIN
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID
GO

/****** Object:  View [dbo].[A_O_ACTUAL_PARTS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_ACTUAL_PARTS_HISTORY]
AS
SELECT     partHistory.ID, partHistory.NICK_NAME, partHistory.SERIAL, PAP.ID + ' ' + ISNULL(PAP.NICK_NAME + N'-', N'') + ISNULL('(' + PAP.SERIAL + ')', '') 
                      AS PARENT_NAME, partHistory.LOCATION, partHistory.OBJECT_ID, partHistory.MERGABLE, partHistory.PARENT_ID, partHistory.PART_ID, 
                      partHistory.QTY, partHistory.CUR_OWNER, partHistory.ASSEMBLY_WT, partHistory.AP_STATUS, partHistory.ROOT_ID, partHistory.ROOT_STATUS, 
                      partInfo.PART_TYPE, partInfo.PART_TYPE_NAME, partInfo.NAME AS PART_DESC, partInfo.UNIT, partInfo.SUPPLIER_SEE_INSTALL_BASE, 
                      partInfo.SUPPLIER_SEE_AVAILABILITY, partInfo.CUSTOMER_SEE_AVAILABILITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.OBJECT_ID AS LOCATION_OBJECT_ID, partInfo.COMPANY_PART_NUMBER, 
                      dbo.A_OBJECTS.LOCKED_BY, dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, 
                      dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO, dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, 
                      dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, dbo.A_OBJECTS.CREATING_CO_NAME, partHistory.DRCM, partHistory.MODBY, 
                      dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_OBJECTS.APPROVAL_DATE, 
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.NAME AS CURRENT_OWNER_NAME, partHistory.HAS_CHILD, partHistory.RESPONSIBLE_PERSON, 
                      dbo.A_V_PEOPLE_DATA_QUICK.FULL_NAME AS RESP_PERSON_FULL_NAME, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.NAME AS LOCATION_NAME
FROM         dbo.A_ACTUAL_PARTS_HISTORY partHistory INNER JOIN
                      dbo.A_PARTS ON partHistory.PART_ID = dbo.A_PARTS.ID INNER JOIN
                      dbo.A_PARTS_HISTORY partInfo ON dbo.A_PARTS.PARTS_HISTORY_ID = partInfo.ID INNER JOIN
                      dbo.A_OBJECTS ON partHistory.OBJECT_ID = dbo.A_OBJECTS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK ON partHistory.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK ON partHistory.RESPONSIBLE_PERSON = dbo.A_V_PEOPLE_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK ON partHistory.LOCATION = dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK PAP ON partHistory.PARENT_ID = PAP.ID
GO

/****** Object:  View [dbo].[Portal_ActualPartsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ActualPartsView]
AS
SELECT        PH.ID, PH.NICK_NAME AS NickName, AP.SYS_NAME AS SysName, PH.SERIAL, PH.PARENT_NAME AS ParentName, PH.LOCATION, PH.OBJECT_ID AS ObjectId, PH.MERGABLE, PH.PARENT_ID AS ParentId, 
                         PH.PART_ID AS PartId, PH.QTY, PH.CUR_OWNER AS CurOwner, PH.ASSEMBLY_WT AS AssemblyWT, PH.AP_STATUS AS ApStatus, PH.ROOT_ID AS RootId, PH.ROOT_STATUS AS RootStatus, PH.PART_TYPE AS PartType, 
                         PH.PART_TYPE_NAME AS PartTypeName, PH.PART_DESC AS PartDesc, PH.UNIT, PH.SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, PH.SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvaliability, 
                         PH.CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, PH.LOCATION_OBJECT_ID AS LocationObjectId, PH.COMPANY_PART_NUMBER AS CompanyPartNumber, PH.LOCKED_BY AS LockedBy, 
                         PH.UNLOCKED_BY AS UnlockedBy, PH.CREATED_BY AS CreatedBy, PH.CREATE_DATE AS CreateDate, PH.ROOT, PH.REV_INFO AS RevInfo, PH.CREATING_CO AS Creating, PH.STATUS, PH.REV, PH.WFS_ID AS WfsId, 
                         PH.LOCKED_BY_NAME AS LockedByName, PH.CREATING_CO_NAME AS CreatingCoName, PH.DRCM, PH.MODBY, PH.APPROVAL_ACTIVITY AS ApprovalActivity, PH.APPROVAL_DATE AS ApprovalDate, 
                         PH.CURRENT_OWNER_NAME AS CurrentOwnerName, PH.HAS_CHILD AS HasChild, PH.RESPONSIBLE_PERSON AS ResponsibleName, PH.RESP_PERSON_FULL_NAME AS RespPersonFullName, 
                         PH.LOCATION_NAME AS LocationName, PH.OBJECT_ID AS ObjId
FROM            dbo.A_O_ACTUAL_PARTS_HISTORY AS PH LEFT OUTER JOIN
                         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AS AP ON PH.ID = AP.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_PARTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[A_V_PARTS_APPROVED_DATA]
AS
SELECT     p.ID, p.PARTS_HISTORY_ID AS HISTORY_REF_ID, ph.UNIT, ph.NAME, ph.PART_TYPE, ph.SPARE, ph.CONSUMABLE, ph.TRACK_FROM_START, 
                      ph.DRCM, ph.MODBY, ph.COMPANY, ph.OBJECT_ID, ph.COMPANY_NAME, ph.PART_TYPE_NAME, ph.COMPANY_PART_NUMBER, 
                      ph.UNIT_SHIPPING_WEIGHT, ph.SUPPLIER_SEE_INSTALL_BASE, ph.SUPPLIER_SEE_AVAILABILITY, ph.CUSTOMER_SEE_AVAILABILITY, 
                      ph.WEIGHT_TYPE, u.NAME AS WEIGHT_TYPE_NAME, ph.NAME + '[' + ph.COMPANY_PART_NUMBER + '][ID:' + o.ID + ']' AS NAME_COMBO, o.STATUS
FROM         dbo.A_PARTS p INNER JOIN
                      dbo.A_PARTS_HISTORY ph ON p.PARTS_HISTORY_ID = ph.ID INNER JOIN
                      dbo.A_OBJECTS o ON ph.OBJECT_ID = o.ID LEFT OUTER JOIN
                      dbo.A_UNIT_TYPES u ON ph.WEIGHT_TYPE = u.ID
WHERE     (o.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_COMPANIES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_COMPANIES_APPROVED_DATA]
AS
SELECT     c.ID, c.HISTORY_REF_ID, ch.NAME, ch.CO_TYPE, ch.PARENT, ch.PHONE, ch.LOCATION, ch.LOCATION_NAME, ch.DRCM, ch.MODBY, ch.OBJECT_ID, 
                      dbo.A_FN_COMPANY_APPROVED_GET_TOP_COMPANY(c.ID) AS ROOT_CO, c.STATUS, dbo.A_FN_COMPANY_GET_TOP_COMPANY(ch.ID) 
                      AS TOP_COMPANY, A_COMPANIES_HISTORY_1.NAME AS PARENT_NAME
FROM         dbo.A_COMPANIES_HISTORY A_COMPANIES_HISTORY_1 INNER JOIN
                      dbo.A_COMPANIES A_COMPANIES_1 ON A_COMPANIES_HISTORY_1.ID = A_COMPANIES_1.HISTORY_REF_ID RIGHT OUTER JOIN
                      dbo.A_COMPANIES c INNER JOIN
                      dbo.A_COMPANIES_HISTORY ch ON c.HISTORY_REF_ID = ch.ID ON A_COMPANIES_1.ID = ch.PARENT
WHERE     (c.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_LOCATIONS_APPROVED_DATA]
AS
SELECT     dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, dbo.A_LOCATIONS_HISTORY.NAME, dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION_NAME, dbo.A_LOCATIONS_HISTORY.ADDRESS_1, dbo.A_LOCATIONS_HISTORY.ADDRESS_2, 
                      dbo.A_LOCATIONS_HISTORY.FULL_ADDRESS, dbo.A_LOCATIONS_HISTORY.CITY, dbo.A_LOCATIONS_HISTORY.STATE, 
                      dbo.A_LOCATIONS_HISTORY.COUNTRY, dbo.A_LOCATIONS_HISTORY.POSTAL_CODE, dbo.A_LOCATIONS_HISTORY.REGION, 
                      dbo.A_LOCATIONS_HISTORY.REGION_NAME, dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS, dbo.A_LOCATIONS_HISTORY.OBJECT_ID, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_PATH, dbo.A_LOCATIONS.STATUS, dbo.A_LOCATIONS_HISTORY.COMPLETE_NAME
FROM         dbo.A_LOCATIONS INNER JOIN
                      dbo.A_LOCATIONS_HISTORY ON dbo.A_LOCATIONS.HISTORY_REF_ID = dbo.A_LOCATIONS_HISTORY.ID
WHERE     (dbo.A_LOCATIONS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA]
AS
SELECT     AP.ID, AP.HISTORY_REF_ID, AP.OBJECT_ID, AP.LOCATION, AP.NICK_NAME, AP.MERGABLE, AP.PARENT_ID, AP.PART_ID, AP.QTY, AP.SERIAL, 
                      AP.CUR_OWNER, AP.ASSEMBLY_WT, AP.AP_STATUS, AP.ROOT_ID, AP.ROOT_STATUS, AP.MODIFIED, AP.SYS_NAME, AP.PREV_OWNER, 
                      AP.SUB_PART_ACTION, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CURRENT_OWNER_NAME, part.NAME AS PART_DESC, 
                      part.PART_TYPE_NAME, loc.NAME AS LOCATION_NAME, ISNULL('Actual Part # ' + AP.ID + ', ', '') + ISNULL('(Nick: ' + AP.NICK_NAME + '), ', '') 
                      + ISNULL('(S/N:' + AP.SERIAL + '), ', '') + ISNULL(' ' + part.NAME + '  ', '') + ISNULL('(p/n ' + part.ID + ')  ', '') AS NAME, AP.STATUS, 
                      part.COMPANY_PART_NUMBER, AP.HAS_CHILD, AP.RESPONSIBLE_PERSON
FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AP INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA part ON AP.PART_ID = part.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA loc ON AP.LOCATION = loc.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON AP.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID
WHERE     (AP.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_ACTUAL_PARTS_APPROVED_WITH_OBJECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_ACTUAL_PARTS_APPROVED_WITH_OBJECT_DATA]
AS
SELECT     AP.ID, AP.HISTORY_REF_ID, AP.OBJECT_ID, AP.LOCATION, AP.NICK_NAME, AP.MERGABLE, AP.PARENT_ID, AP.PART_ID, AP.QTY, AP.SERIAL, 
                      AP.CUR_OWNER, AP.ASSEMBLY_WT, AP.AP_STATUS, AP.ROOT_ID, AP.ROOT_STATUS, AP.MODIFIED, AP.SYS_NAME, AP.PREV_OWNER, 
                      AP.SUB_PART_ACTION, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CURRENT_OWNER_NAME, part.NAME AS PART_DESC, 
                      part.PART_TYPE_NAME, loc.NAME AS LOCATION_NAME, ISNULL('Actual Part # ' + AP.ID + ', ', '') + ISNULL('(Nick: ' + AP.NICK_NAME + '), ', '') 
                      + ISNULL('(S/N:' + AP.SERIAL + '), ', '') + ISNULL(' ' + part.NAME + '  ', '') + ISNULL('(p/n ' + part.ID + ')  ', '') AS NAME, part.COMPANY_PART_NUMBER, 
                      AP.HAS_CHILD, AP.RESPONSIBLE_PERSON, dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.REV, dbo.A_OBJECTS.STATUS, 
                      dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.LOCKED_BY, dbo.A_OBJECTS.UNLOCKED_BY
FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AP INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA part ON AP.PART_ID = part.ID INNER JOIN
                      dbo.A_OBJECTS ON AP.OBJECT_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA loc ON AP.LOCATION = loc.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON AP.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PRODUCTS_APPROVED_DATA]
AS
SELECT     dbo.A_PRODUCTS.ID, dbo.A_PRODUCTS.HISTORY_REF_ID, dbo.A_PRODUCTS_HISTORY.NAME, dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID, 
                      dbo.A_PRODUCTS_HISTORY.COMMENTS, dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID, dbo.A_PRODUCTS_HISTORY.APP_OBJECT, 
                      dbo.A_PRODUCTS_HISTORY.SHIP_OR_LABOR, dbo.A_PRODUCTS_HISTORY.REQ_FORM, dbo.A_PRODUCTS_HISTORY.CUSTOMIZABLE, 
                      dbo.A_PRODUCTS_HISTORY.MGR_TEAM, dbo.A_PRODUCTS_HISTORY.SALES_TAX, dbo.A_PRODUCTS_HISTORY.DRCM, 
                      dbo.A_PRODUCTS_HISTORY.MODBY, dbo.A_PRODUCTS_HISTORY.OBJECT_ID, dbo.A_PRODUCTS_HISTORY.PARENT_ID, 
                      dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE, dbo.A_PRODUCTS_HISTORY.SYSTEM_PROCEDURE, dbo.A_PRODUCTS.STATUS, 
                      dbo.A_PRODUCTS_HISTORY.PERSON_SUPPLIER, dbo.A_PRODUCTS_HISTORY.AVAILABILITY,
					  dbo.A_PRODUCTS_HISTORY.TotalSalePrice,
					  dbo.A_PRODUCTS_HISTORY.MaterialCost,
					  dbo.A_PRODUCTS_HISTORY.CycleTime
FROM         dbo.A_PRODUCTS INNER JOIN
                      dbo.A_PRODUCTS_HISTORY ON dbo.A_PRODUCTS.HISTORY_REF_ID = dbo.A_PRODUCTS_HISTORY.ID
WHERE     (dbo.A_PRODUCTS.STATUS = 'APPROVED')

GO

/****** Object:  View [dbo].[A_V_PROCEDURES_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURES_DATA_QUICK]
AS
SELECT     dbo.A_PROCEDURES.ID, dbo.A_PROCEDURES.HISTORY_REF_ID, dbo.A_PROCEDURES_HISTORY.OBJECT_ID, 
                      dbo.A_PROCEDURES_HISTORY.VERB, dbo.A_PROCEDURES_HISTORY.NAME, dbo.A_PROCEDURES_HISTORY.COMMENTS, 
                      dbo.A_PROCEDURES_HISTORY.SECURITY_LEVEL, dbo.A_PROCEDURES_HISTORY.STEPS_IN_AP, dbo.A_PROCEDURES_HISTORY.WIP_MSG, 
                      dbo.A_PROCEDURES_HISTORY.DRCM, dbo.A_PROCEDURES_HISTORY.MODBY, dbo.A_PROCEDURES_HISTORY.IS_SYSTEM, 
                      dbo.A_PROCEDURES_HISTORY.CREATING_DEPT, dbo.A_PROCEDURES_HISTORY.SYSTEM_ID, dbo.A_PROCEDURES_HISTORY.DURATION, 
                      dbo.A_PROCEDURES_HISTORY.DURATION_TYPE, dbo.A_PROCEDURES_HISTORY.Threshold
FROM         dbo.A_PROCEDURES INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURES.HISTORY_REF_ID = dbo.A_PROCEDURES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACCOUNTS_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_ACCOUNTS.ID, dbo.A_ACCOUNTS.HISTORY_REF_ID, dbo.A_ACCOUNTS_HISTORY.OBJECT_ID, dbo.A_ACCOUNTS_HISTORY.NAME, 
                      dbo.A_ACCOUNTS_HISTORY.REFERENCE_PO, dbo.A_ACCOUNTS_HISTORY.REFERENCE_NAME, dbo.A_ACCOUNTS_HISTORY.OPEN_DATE, 
                      dbo.A_ACCOUNTS_HISTORY.SUPPLIER_CO, dbo.A_ACCOUNTS_HISTORY.CLOSE_DATE, dbo.A_ACCOUNTS_HISTORY.CUSTOMER_CO, 
                      dbo.A_ACCOUNTS_HISTORY.CUSTOMER_BILL_CO, dbo.A_ACCOUNTS_HISTORY.MAXIMUM_USES, 
                      dbo.A_ACCOUNTS_HISTORY.TOTAL_PURCHASE_LIMIT, dbo.A_ACCOUNTS_HISTORY.CREDIT_LIMIT, dbo.A_ACCOUNTS_HISTORY.APPROVAL_WF, 
                      dbo.A_ACCOUNTS_HISTORY.INVOICE_TRIGGER, dbo.A_ACCOUNTS_HISTORY.INVOICE_PERIOD_NUMBER, 
                      dbo.A_ACCOUNTS_HISTORY.INVOICE_PERIOD_TYPE, dbo.A_ACCOUNTS_HISTORY.FIRST_INVOICE_DATE, 
                      dbo.A_ACCOUNTS_HISTORY.NEXT_INVOICE_DATE, dbo.A_ACCOUNTS_HISTORY.PAYMENT_GRACE_PERIOD, 
                      dbo.A_ACCOUNTS_HISTORY.LATE_FEE_PERCENTAGE, dbo.A_ACCOUNTS_HISTORY.REAPPLY_LATE_FEE, dbo.A_ACCOUNTS_HISTORY.DRCM, 
                      dbo.A_ACCOUNTS_HISTORY.MODBY, dbo.A_ACCOUNTS_HISTORY.ACCT_TYPE, dbo.A_ACCOUNTS_HISTORY.TOTAL_PURCHASES, 
                      dbo.A_ACCOUNTS_HISTORY.BALANCE, dbo.A_ACCOUNTS_HISTORY.AMT_INVOICED, dbo.A_ACCOUNTS_HISTORY.ACCT_STATUS, 
                      dbo.A_ACCOUNTS_HISTORY.PRODUCT_ID, dbo.A_ACCOUNTS_HISTORY.PARENT_ACCOUNT, dbo.A_ACCOUNTS_HISTORY.LABOR_INCLUDED, 
                      dbo.A_ACCOUNTS_HISTORY.CONSUMABLES_INCLUDED, dbo.A_ACCOUNTS_HISTORY.NONCONSUMABLE_INCLUDED, 
                      dbo.A_ACCOUNTS_HISTORY.HAS_CHILD, dbo.A_ACCOUNTS_HISTORY.INVOICED_BALANCE, dbo.A_ACCOUNTS_HISTORY.UNINVOICED_BALANCE, 
                      dbo.A_ACCOUNTS_HISTORY.TOTAL_CREDITS, dbo.A_ACCOUNTS_HISTORY.TOTAL_DEBITS, dbo.A_ACCOUNTS_HISTORY.BILLING_EMAIL
FROM         dbo.A_ACCOUNTS INNER JOIN
                      dbo.A_ACCOUNTS_HISTORY ON dbo.A_ACCOUNTS.HISTORY_REF_ID = dbo.A_ACCOUNTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_ORDERS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDERS_APPROVED_DATA]
AS
SELECT     dbo.A_ORDERS.ID, dbo.A_ORDERS.HISTORY_REF_ID, dbo.A_ORDERS_HISTORY.OBJECT_ID, dbo.A_ORDERS_HISTORY.CUSTOMER_PERSON, 
                      dbo.A_ORDERS_HISTORY.CUSTOMER_CO, dbo.A_ORDERS_HISTORY.DESCRIPTION, dbo.A_ORDERS_HISTORY.BUDGETARY_ONLY, 
                      dbo.A_ORDERS_HISTORY.EXPIRATION_DATE, dbo.A_ORDERS_HISTORY.PROGRESS, dbo.A_ORDERS_HISTORY.DRCM, 
                      dbo.A_ORDERS_HISTORY.MODBY, dbo.A_ORDERS.STATUS, dbo.A_ORDERS_HISTORY.RFQ_ID, dbo.A_ORDERS_HISTORY.SUPPLIER_ID, 
                      dbo.A_ORDERS_HISTORY.CREATION_DATE, dbo.A_ORDERS_HISTORY.PERSON_SUPPLIER, dbo.A_ORDERS_HISTORY.TYPE, 
                      dbo.A_ORDERS_HISTORY.PRICE
FROM         dbo.A_ORDERS INNER JOIN
                      dbo.A_ORDERS_HISTORY ON dbo.A_ORDERS.HISTORY_REF_ID = dbo.A_ORDERS_HISTORY.ID
WHERE     (dbo.A_ORDERS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_PURCHASES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASES_APPROVED_DATA]
AS
SELECT     dbo.A_PURCHASES.ID, dbo.A_PURCHASES.STATUS, dbo.A_PURCHASES.HISTORY_REF_ID, dbo.A_PURCHASES_HISTORY.DATE_CREATED, 
                      dbo.A_PURCHASES_HISTORY.PURCHASE_STATUS, dbo.A_PURCHASES_HISTORY.ORDER_ID, dbo.A_PURCHASES_HISTORY.OBJECT_ID, 
                      dbo.A_PURCHASES_HISTORY.PURCHASE_TOTAL, dbo.A_PURCHASES_HISTORY.PURCHASER, dbo.A_PURCHASES_HISTORY.PURCHASING_CO, 
                      dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_PERSON, dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO, 
                      dbo.A_V_ORDERS_APPROVED_DATA.DESCRIPTION, dbo.A_V_ORDERS_APPROVED_DATA.PROGRESS, 
                      dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM, dbo.A_PURCHASES_HISTORY.SUP_PURCH_NUM, 
                      dbo.A_PURCHASES_HISTORY.ACCT_FOR_ALL
FROM         dbo.A_PURCHASES INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON dbo.A_PURCHASES.HISTORY_REF_ID = dbo.A_PURCHASES_HISTORY.ID INNER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA ON dbo.A_PURCHASES_HISTORY.ORDER_ID = dbo.A_V_ORDERS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_WORKER_SCREEN_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WORKER_SCREEN_TASKS]
AS
SELECT DISTINCT 
                      p.ID AS PURCHASE_ID, toi.PURCHASE_HIST_ID, toi.PURCHASE_ITEM_ID, c.NAME AS CUSTOMER_NAME, i.DUE_DATE, i.ORIG_DUE_DATE, 
                      t.PROCEDURE_ID AS PROC_ID, c.ID AS CUST_ID, dbo.A_FN_DATE_TIME_ADD_USING_UNITS(i.PROD_TIME_UNIT, i.DUE_DATE, - i.PROD_TIME) 
                      AS START_DATE, t.STATUS, t.REQUESTEE_ID, t.GROUP_REQUESTEE_ID, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, t.ID, 
                      p.CUST_PURCH_NUM, i.ACCOUNT_ID, dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.REFERENCE_PO, 
                      dbo.A_V_PROCEDURES_DATA_QUICK.NAME AS PROC_NAME, i.QTY, t.ACTUAL_START_DATE, t.ACTUAL_STOP_DATE, i.MT_NUM, 
                      dbo.A_TASK_OBJECT_LINK.OBJECT_ID AS ACTUAL_PART_ID, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK.SERIAL
FROM         dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_ORDER_ITEMS i ON toi.PURCHASE_ITEM_ID = i.ID INNER JOIN
                      dbo.A_TASKS t ON toi.TASK_ID = t.ID INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA p ON toi.PURCHASE_HIST_ID = p.HISTORY_REF_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK c ON p.CUSTOMER_CO = c.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON i.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK ON t.PROCEDURE_ID = dbo.A_V_PROCEDURES_DATA_QUICK.ID INNER JOIN
                      dbo.A_TASK_OBJECT_LINK ON t.ID = dbo.A_TASK_OBJECT_LINK.TASK_ID INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK ON 
                      dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK ON i.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.ID
WHERE     (toi.PURCHASE_ITEM_ID IS NOT NULL) AND (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED'))
GO

/****** Object:  View [dbo].[A_O_TT_VERBS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_O_TT_VERBS_HISTORY]
AS
SELECT     v.ID, v.NAME, v.OBJECT_ID, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, 
                      o.REV, o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, o.ID AS OBJ_ID, v.VERB_TYPE, 
                      t.NAME AS VERB_TYPE_NAME
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_TT_VERBS_HISTORY v ON o.ID = v.OBJECT_ID LEFT OUTER JOIN
                      dbo.A_TT_VERBS_TYPE_LOOKUP t ON v.VERB_TYPE = t.ID
GO

/****** Object:  View [dbo].[A_APPROVED_VERBS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_APPROVED_VERBS]
AS
SELECT     dbo.A_O_TT_VERBS_HISTORY.NAME, dbo.A_TT_VERBS.ID, dbo.A_O_TT_VERBS_HISTORY.OBJECT_ID, dbo.A_O_TT_VERBS_HISTORY.LOCKED_BY, 
                      dbo.A_O_TT_VERBS_HISTORY.UNLOCKED_BY, dbo.A_O_TT_VERBS_HISTORY.CREATED_BY, dbo.A_O_TT_VERBS_HISTORY.CREATE_DATE, 
                      dbo.A_O_TT_VERBS_HISTORY.ROOT, dbo.A_O_TT_VERBS_HISTORY.REV_INFO, dbo.A_O_TT_VERBS_HISTORY.CREATING_CO, 
                      dbo.A_O_TT_VERBS_HISTORY.STATUS, dbo.A_O_TT_VERBS_HISTORY.REV, dbo.A_O_TT_VERBS_HISTORY.LOCKED_BY_NAME, 
                      dbo.A_O_TT_VERBS_HISTORY.APPROVAL_ACTIVITY, dbo.A_O_TT_VERBS_HISTORY.CREATING_CO_NAME, dbo.A_O_TT_VERBS_HISTORY.OBJ_ID, 
                      dbo.A_O_TT_VERBS_HISTORY.VERB_TYPE_NAME, dbo.A_O_TT_VERBS_HISTORY.VERB_TYPE
FROM         dbo.A_TT_VERBS INNER JOIN
                      dbo.A_O_TT_VERBS_HISTORY ON dbo.A_TT_VERBS.HISTORY_REF_ID = dbo.A_O_TT_VERBS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROCEDURES_APPROVED_DATA]
AS
SELECT     dbo.A_PROCEDURES.ID, dbo.A_PROCEDURES.HISTORY_REF_ID, dbo.A_PROCEDURES_HISTORY.OBJECT_ID, 
                      dbo.A_PROCEDURES_HISTORY.VERB, dbo.A_PROCEDURES_HISTORY.NAME, dbo.A_PROCEDURES_HISTORY.COMMENTS, 
                      dbo.A_PROCEDURES_HISTORY.SECURITY_LEVEL, dbo.A_PROCEDURES_HISTORY.STEPS_IN_AP, dbo.A_PROCEDURES_HISTORY.WIP_MSG, 
                      dbo.A_PROCEDURES_HISTORY.DRCM, dbo.A_PROCEDURES_HISTORY.MODBY, dbo.A_APPROVED_VERBS.NAME AS VERB_NAME, 
                      dbo.A_PROCEDURES_HISTORY.SYSTEM_ID, dbo.A_PROCEDURES_HISTORY.CREATING_DEPT, dbo.A_PROCEDURES_HISTORY.IS_SYSTEM, 
                      dbo.A_OBJECTS.CREATING_CO, dbo.A_PROCEDURES.STATUS, dbo.A_PROCEDURES_HISTORY.DURATION, 
                      dbo.A_PROCEDURES_HISTORY.DURATION_TYPE,
					  dbo.A_OBJECTS.REV
FROM         dbo.A_PROCEDURES INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURES.HISTORY_REF_ID = dbo.A_PROCEDURES_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_PROCEDURES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_VERBS ON dbo.A_PROCEDURES_HISTORY.VERB = dbo.A_APPROVED_VERBS.ID
WHERE     (dbo.A_PROCEDURES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART]
AS
SELECT     
distinct
--ah.id as HId,
AP.HISTORY_REF_ID AS HId,
dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PROD_NAME,
AP.QTY AS PURCHASE_QTY,
--PURCHASE_ITEM.PURCHASE_HIST_ID, 
dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROC_NAME,
--dbo.A_V_PRODUCTS_APPROVED_DATA.HISTORY_REF_ID AS PROD_HIST_ID, 
--PURCHASE_ITEM.CUST_LINE_ITEM,
PURCHASE_ITEM.PARENT, 
AP.SERIAL, 
part.NAME AS PART_DESC, 
part.PART_TYPE_NAME,
part.COMPANY_PART_NUMBER,
--dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM,
af.DONT_BILL,
AP.PART_ID AS ACTUAL_PART_ID,
dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID,
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.SUPPLIER_CO AS ACCT_SUPPLIER, 
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.CUSTOMER_CO AS ACCT_CUSTOMER,
AP.PARENT_ID,
AP.ID
					

FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AP INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA part ON AP.PART_ID = part.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA loc ON AP.LOCATION = loc.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON AP.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID	
					  inner join A_FILLS af on af.FILL_OBJ_ID= ap.PARENT_ID  OR af.FILL_OBJ_ID =ap.id
					  inner join dbo.A_ORDER_ITEMS PURCHASE_ITEM ON af.PURCH_ITEM_ID = PURCHASE_ITEM.ID
					  INNER JOIN dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK ON 
                      PURCHASE_ITEM.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON af.FILL_OBJ_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID ON 
                      PURCHASE_ITEM.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
WHERE     (AP.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_O_ACTUAL_PARTS_HISTORY_II]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_ACTUAL_PARTS_HISTORY_II]
AS
SELECT     partHistory.ID, partHistory.NICK_NAME, partHistory.SERIAL, PAP.ID + ' ' + ISNULL(PAP.NICK_NAME + N'-', N'') + ISNULL('(' + PAP.SERIAL + ')', '') 
                      AS PARENT_NAME, partHistory.LOCATION, partHistory.OBJECT_ID, partHistory.MERGABLE, partHistory.PARENT_ID, partHistory.PART_ID, 
                      partHistory.QTY, partHistory.CUR_OWNER, partHistory.ASSEMBLY_WT, partHistory.AP_STATUS, partHistory.ROOT_ID, partHistory.ROOT_STATUS, 
                      partInfo.PART_TYPE, partInfo.PART_TYPE_NAME, partInfo.NAME AS PART_DESC, partInfo.UNIT, partInfo.SUPPLIER_SEE_INSTALL_BASE, 
                      partInfo.SUPPLIER_SEE_AVAILABILITY, partInfo.CUSTOMER_SEE_AVAILABILITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.OBJECT_ID AS LOCATION_OBJECT_ID, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.COMPLETE_NAME AS LOCATION_NAME, partInfo.COMPANY_PART_NUMBER, 
                      dbo.A_OBJECTS.LOCKED_BY, dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, 
                      dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO, dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, 
                      dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, dbo.A_OBJECTS.CREATING_CO_NAME, partHistory.DRCM, partHistory.MODBY, 
                      dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_OBJECTS.APPROVAL_DATE, 
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.NAME AS CURRENT_OWNER_NAME
FROM         dbo.A_ACTUAL_PARTS_HISTORY partHistory INNER JOIN
                      dbo.A_PARTS ON partHistory.PART_ID = dbo.A_PARTS.ID INNER JOIN
                      dbo.A_PARTS_HISTORY partInfo ON dbo.A_PARTS.PARTS_HISTORY_ID = partInfo.ID INNER JOIN
                      dbo.A_OBJECTS ON partHistory.OBJECT_ID = dbo.A_OBJECTS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK ON partHistory.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK ON partHistory.LOCATION = dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK PAP ON partHistory.PARENT_ID = PAP.ID
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART_1]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_APPROVED_DATA_FOR_EACH_PART_1]
AS

SELECT     
distinct
dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PROD_NAME,
AP.QTY AS PURCHASE_QTY,
dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROC_NAME,
dbo.A_V_PRODUCTS_APPROVED_DATA.HISTORY_REF_ID AS PROD_HIST_ID, 
PURCHASE_ITEM.CUST_LINE_ITEM,
PURCHASE_ITEM.PARENT, 
AP.SERIAL, 
part.NAME AS PART_DESC, 
part.PART_TYPE_NAME,
part.COMPANY_PART_NUMBER,
PURCHASE_ITEM.ID AS PURCH_ITEM_ID,
dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM,
dbo.A_PURCHASES_HISTORY.OBJECT_ID AS PURCHASE_ID, 
af.DONT_BILL,
AP.PART_ID AS ACTUAL_PART_ID,
dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID,
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.SUPPLIER_CO AS ACCT_SUPPLIER, 
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.CUSTOMER_CO AS ACCT_CUSTOMER,
AP.PARENT_ID,
AP.ID
		

FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA_QUICK AP INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA part ON AP.PART_ID = part.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA loc ON AP.LOCATION = loc.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON AP.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID	
					  inner join A_FILLS af on af.FILL_OBJ_ID= ap.PARENT_ID  OR af.FILL_OBJ_ID =ap.id
					  inner join dbo.A_ORDER_ITEMS PURCHASE_ITEM ON af.PURCH_ITEM_ID = PURCHASE_ITEM.ID INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON PURCHASE_ITEM.PURCHASE_HIST_ID = dbo.A_PURCHASES_HISTORY.ID INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK ON 
                      PURCHASE_ITEM.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON af.FILL_OBJ_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID ON 
                      PURCHASE_ITEM.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
WHERE     (AP.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_QUOTES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_QUOTES_APPROVED_DATA]
AS
SELECT     dbo.A_QUOTES.ID, dbo.A_QUOTES.HISTORY_REF_ID, dbo.A_QUOTES_HISTORY.OBJECT_ID, dbo.A_QUOTES_HISTORY.DESCRIPTION, 
                      dbo.A_QUOTES_HISTORY.PROGRESS, dbo.A_QUOTES_HISTORY.ORDER_ID, dbo.A_QUOTES_HISTORY.CUSTOMER_PERSON, 
                      dbo.A_QUOTES_HISTORY.CUSTOMER_CO, dbo.A_QUOTES_HISTORY.BUGETARY_ONLY, dbo.A_QUOTES_HISTORY.EXPIRATION_DATE, 
                      dbo.A_QUOTES_HISTORY.MODBY, dbo.A_QUOTES_HISTORY.SUPPLIER_ID, dbo.A_QUOTES_HISTORY.CREATION_DATE, 
                      dbo.A_QUOTES_HISTORY.PERSON_SUPPLIER
FROM         dbo.A_QUOTES INNER JOIN
                      dbo.A_QUOTES_HISTORY ON dbo.A_QUOTES.HISTORY_REF_ID = dbo.A_QUOTES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_ORDER_WITH_QUOTE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_ORDER_WITH_QUOTE_ITEMS]
AS
SELECT     qI.ID, dbo.A_QUOTE_ORDER_LINK.ORDER_ID, dbo.A_QUOTE_ORDER_LINK.QUOTE_ID AS QUOTE_HIST_ID, q.ID AS QUOTE_ID, qI.PARENT, 
                      qI.PARENT_QTY, qI.PRODUCT_ID, qI.PROD_PRICE_LIST, qI.PROC_SYS_ID, qI.ADD_COST_ID, qI.TOTAL_QTY, qI.QTY, qI.UNIT_PRICE, 
                      qI.UNIT_ESTIMATE, qI.COMMENTS, qI.DRCM, qI.MODBY, qI.TOTAL_PRICE, qI.DEST, qI.FROM_LOC, qI.TO_LOC, qI.SPECIAL_DISCOUNT, 
                      qI.SPECIAL_DISC_REASON, qI.EXPEDITE_PRODUCTION, qI.EXPEDITE_REASON, qI.FLAT_RATE, qI.EX_DESC, qI.EST_WEIGHT, qI.EST_WEIGHT_UNIT, 
                      qI.PPL_HIST_ID, qI.RECURRING, qI.RECUR_PERIOD, qI.RECUR_COUNT, qI.RECUR_START_DATE, qI.RECUR_STOP_DATE, qI.RECUR_ACCOUNT, 
                      qI.RECUR_AUTO_FILL, qI.SOURCE_ID, qI.PURCHASE_HIST_ID
FROM         dbo.A_V_QUOTES_APPROVED_DATA q RIGHT OUTER JOIN
                      dbo.A_ORDER_ITEMS qI INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON qI.QUOTE_ID = dbo.A_QUOTE_ORDER_LINK.QUOTE_ID ON q.HISTORY_REF_ID = qI.QUOTE_ID
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_QUICK]
AS
SELECT     dbo.A_ACTUAL_PARTS.ID, dbo.A_ACTUAL_PARTS.HISTORY_REF_ID, dbo.A_ACTUAL_PARTS_HISTORY.LOCATION, 
                      dbo.A_ACTUAL_PARTS_HISTORY.OBJECT_ID, dbo.A_ACTUAL_PARTS_HISTORY.NICK_NAME, dbo.A_ACTUAL_PARTS_HISTORY.MERGABLE, 
                      dbo.A_ACTUAL_PARTS_HISTORY.PARENT_ID, dbo.A_ACTUAL_PARTS_HISTORY.PART_ID, dbo.A_ACTUAL_PARTS_HISTORY.QTY, 
                      dbo.A_ACTUAL_PARTS_HISTORY.SERIAL, dbo.A_ACTUAL_PARTS_HISTORY.CUR_OWNER, dbo.A_ACTUAL_PARTS_HISTORY.ASSEMBLY_WT, 
                      dbo.A_ACTUAL_PARTS_HISTORY.AP_STATUS, dbo.A_ACTUAL_PARTS_HISTORY.ROOT_ID, dbo.A_ACTUAL_PARTS_HISTORY.ROOT_STATUS, 
                      dbo.A_ACTUAL_PARTS_HISTORY.SUB_PART_ACTION, dbo.A_ACTUAL_PARTS_HISTORY.HAS_CHILD, 
                      dbo.A_ACTUAL_PARTS_HISTORY.RESPONSIBLE_PERSON
FROM         dbo.A_ACTUAL_PARTS INNER JOIN
                      dbo.A_ACTUAL_PARTS_HISTORY ON dbo.A_ACTUAL_PARTS.HISTORY_REF_ID = dbo.A_ACTUAL_PARTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_APPROVED_PART_TYPES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_APPROVED_PART_TYPES]
AS
SELECT     pt.ID, pt.HISTORY_REF_ID, pth.NAME, pth.SPARE, pth.CONSUMABLE, pth.OBJECT_ID, pth.UNIT, pth.UNIT_SHIPPING_WEIGHT, o.LOCKED_BY, 
                      o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, 
                      o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PART_TYPES_HISTORY pth ON o.ID = pth.OBJECT_ID INNER JOIN
                      dbo.A_PART_TYPES pt ON pth.ID = pt.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_DOCUMENTS_WITH_LINKED_ITEM]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_DOCUMENTS_WITH_LINKED_ITEM]
AS
SELECT     
dbo.A_DOCUMENT_LINK.LINKED_DOC_ID,
dbo.A_DOCUMENTS.NAME, 
dbo.A_DOCUMENTS.DELETED,
dbo.A_DOCUMENTS.DOC_TYPE,
dbo.A_DOCUMENTS.CONTENTTYPE, 
dbo.A_DOCUMENTS.SERVER_PATH, 
dbo.A_DOCUMENT_LINK.TYPE,
dbo.A_DOCUMENT_LINK.OBJECT_ID
FROM dbo.A_DOCUMENT_LINK 
INNER JOIN dbo.A_DOCUMENTS ON dbo.A_DOCUMENT_LINK.LINKED_DOC_ID = dbo.A_DOCUMENTS.ID





GO

/****** Object:  View [dbo].[Portal_PartsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PartsView]
AS
SELECT       
o.LOCKED_BY AS LockedBy,
o.UNLOCKED_BY AS UnLockedBy,
o.CREATED_BY AS CreatedBy, 
o.CREATE_DATE AS CreateDate,
o.ROOT,
o.REV_INFO AS RevInfo, 
o.CREATING_CO AS CreatingCo, 
o.STATUS, o.REV, 
o.WFS_ID AS WfsId,
o.LOCKED_BY_NAME AS LockedByName,
o.CREATING_CO_NAME AS CreatingCoName, 
o.APPROVAL_ACTIVITY AS ApprovalActivity,
PH.OBJECT_ID AS ObjectId, 
PH.ID,
PH.UNIT,
PH.NAME, 
PH.PART_TYPE AS PartType, 
PH.TRACK_FROM_START AS TrackFromStart, 
PH.COMPANY, 
PH.UNIT_SHIPPING_WEIGHT AS UnitShippingWeight,
PH.COMPANY_PART_NUMBER AS CompanyPartNumber, 
PH.SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase,
PH.SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvailability, 
PH.CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability,
c.NAME AS CompanyName, 
PT.NAME AS PartTypeName,
PH.SPARE, 
PH.CONSUMABLE, 
PH.WEIGHT_TYPE AS WeightType, 
PH.CREATE_PROD AS CreateProd, 
PH.PRODUCT_TYPE AS ProductType, 
PH.PROC_VERB AS ProcVerb, 
PH.SUPPLIER_CO AS SupplierCo,
PARENT_CO.NAME AS ParentCoName, 
dbo.A_UNIT_TYPES.NAME AS WeightTypeName,
PH.PRICE AS Price,
isnull(STUFF((
SELECT +','+ DL.NAME+'|'+DL.LINKED_DOC_ID 
FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
WHERE DL.OBJECT_ID = PH.OBJECT_ID
    FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles,
PH.DRCM AS UpdatedDate
FROM dbo.A_PARTS_HISTORY AS PH INNER JOIN
dbo.A_OBJECTS AS o ON PH.OBJECT_ID = o.ID INNER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS c ON PH.COMPANY = c.ID LEFT OUTER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS PARENT_CO ON c.PARENT = PARENT_CO.ID LEFT OUTER JOIN
dbo.A_UNIT_TYPES ON PH.WEIGHT_TYPE = dbo.A_UNIT_TYPES.ID LEFT OUTER JOIN
dbo.A_APPROVED_PART_TYPES AS PT ON PH.PART_TYPE = PT.ROOT
GO

/****** Object:  View [dbo].[Portal_WorkOrders]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_WorkOrders]

AS
SELECT DISTINCT
NewId() AS Id,
t.LATEST_REQUESTEE_NAME AS RequesteeName,
t.ID AS TaskId,
supp.NAME AS SupplierName,
purch.ID AS PurchaseId,
toi.PURCHASE_HIST_ID AS PurchaseHistId,
toi.PURCHASE_ITEM_ID AS PurchaseItemId,
customer.NAME + '-' + toi.PURCHASE_ITEM_ID  AS WoItem,
customer.NAME AS CustomerName,
ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) AS DueDate,
purchItem.ORIG_DUE_DATE AS OrigDueDate,
t.PROCEDURE_ID AS ProcId,
customer.ID AS CustId, 
dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS StartDate,
t.STATUS AS Status, 
t.REQUESTEE_ID AS RequesteeId,
t.GROUP_REQUESTEE_ID AS GroupRequesteeId,
Product.NAME AS ProductName,
purch.CUST_PURCH_NUM AS CustPurchNum,
Account.REFERENCE_PO AS ReferencePo, 
[PROC].NAME AS ProcName,
purchItem.QTY AS Qty,
dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME AS NickName,
dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL AS Serial,
dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ActualPartId,
t.CUR_PLANNED_START_DATE AS StDate, 
ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) AS ActualStopDate,
t.ACTUAL_START_DATE  AS ActualStartDate,
purchItem.MT_NUM AS MtNum, 
toi.FILL_ITEM_ID AS FillItemId,
dbo.A_FILLS.BATCH_PARENT AS BatchParent,
dbo.A_FILLS.BATCHED AS Batched,
dbo.A_FILLS.BATCH_FILL AS BatchEdFill,
dbo.A_FILLS.ID AS FillId,
dbo.A_FILLS.FILL_QTY AS FillQty,
dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE AS PercComplete,
ISNULL(dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE,0) AS TimeComplete,
dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS  AS MyTotHours,
dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS AS MyCompHours,
A_TASK_COMPLETION_STATS.CUR_STEP_TEXT  AS CurStepText,
dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ActPartObjId,
CASE WHEN (          

SELECT count(*)
 FROM A_DOCUMENTS WHERE ID IN
(
SELECT file_id FROM A_ACTUAL_PARTS_RELATED_FILES
WHERE 
ACTUAL_PART_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
AND STATUS = 'ACTIVE'
)) > 0 THEN 1 ELSE 0 END 
AS HasFile,
supp.ID as SupplierId,
isnull('['+STUFF((    SELECT ',' + '{"Date":"'+  FORMAT ( n.CreatedDate, 'MM/dd/yyyy hh:mm') +'","Name":"'+ u.FirstName + ' '+ u.LastName + '","Message":"' +n.message  +'"}'
                        FROM [Portal_Note] n
						INNER JOIN AspNetUsers u ON u.Id = n.CreatedBy
                        WHERE n.EntityId=dbo.A_FILLS.ID
						ORDER BY n.CreatedDate DESC
                        FOR XML PATH('')), 1, 1, '' ) +']'
						,'') AS Notes
,0 AS HasMonitor
,1 AS HasNcr
,[PROC].Threshold
,part.CompanyPartNumber
,pa.LocationName
,[PROC].OBJECT_ID as ProcObjId
,Product.TotalSalePrice
,Product.MaterialCost,
(
CONVERT(nvarchar(100),FORMAT(dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE,'N2')) +'% '+
CONVERT(nvarchar(100), A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE)+'/'+
CONVERT(nvarchar(100), A_TASK_COMPLETION_STATS.NUM_SUB_TASKS) + ' steps complete'
) AS PercCompletedText,
(
CONVERT(nvarchar(100),FORMAT(ISNULL(dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE,0),'N2'))+'% '+
CONVERT(nvarchar(100),A_TASK_COMPLETION_STATS.MY_COMP_HOURS)+'/'+
CONVERT(nvarchar(100),A_TASK_COMPLETION_STATS.MY_TOT_HOURS)+ ' hours complete'
) AS TimeCompletedText,
dbo.A_FILLS.PRICE AS Amount,
purch.PURCHASER AS Purchaser,
Account.CUSTOMER_BILL_CO AS CustMttn,
PA.PartId
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS customer 
INNER JOIN dbo.A_V_PURCHASES_APPROVED_DATA AS purch ON customer.ID = purch.CUSTOMER_CO 
RIGHT OUTER JOIN dbo.A_TASK_COMPLETION_STATS 
RIGHT OUTER JOIN dbo.A_TASK_OBJECT_LINK AS T_OBJ 
INNER JOIN dbo.A_V_PROCEDURES_DATA_QUICK AS [PROC] 
INNER JOIN dbo.A_TASK_ORDER_INFORMATION AS toi
INNER JOIN dbo.A_TASKS AS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID 
INNER JOIN dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID 
INNER JOIN dbo.A_V_PRODUCTS_APPROVED_DATA AS Product 
INNER JOIN dbo.A_ORDER_ITEMS AS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID 
LEFT OUTER JOIN dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK AS Account ON purchItem.ACCOUNT_ID = Account.ID ON purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID 
LEFT OUTER JOIN dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
RIGHT JOIN dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supp on supp.ID = Account.SUPPLIER_CO
INNER JOIN Portal_ActualPartsView AS PA ON PA.ID=dbo.A_V_ACTUAL_PARTS_QUICK.HISTORY_REF_ID
OUTER APPLY 
( 
SELECT top 1 p.CompanyPartNumber FROM Portal_PartsView p
WHERE p.ROOT = dbo.A_V_ACTUAL_PARTS_QUICK.PART_ID
) part
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)
GO

/****** Object:  View [dbo].[Portal_ActualPartApprovedView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ActualPartApprovedView]
AS
SELECT        ID AS Id, HISTORY_REF_ID AS HistoryRefId, OBJECT_ID AS ObjectId, LOCATION AS Location, NICK_NAME AS NickName, MERGABLE AS Mergable, PARENT_ID AS ParentId, PART_ID AS PartId, QTY AS Qty, SERIAL AS Serial, 
                         CUR_OWNER AS CurOwner, ASSEMBLY_WT AS AssemblyWt, AP_STATUS AS ApStatus, ROOT_ID AS RootId, ROOT_STATUS AS RootStatus, MODIFIED AS Modified, SYS_NAME AS SysName, PREV_OWNER AS PrevOwner, 
                         SUB_PART_ACTION AS SubPartAction, CURRENT_OWNER_NAME AS CurrentOwnerName, PART_DESC AS PartDesc, PART_TYPE_NAME AS PartTypeName, LOCATION_NAME AS LocationName, NAME AS Name, 
                         STATUS AS Status, COMPANY_PART_NUMBER AS ComapnyPartNumber, HAS_CHILD AS HasChild, RESPONSIBLE_PERSON AS ResponsiblePerson
FROM            dbo.A_V_ACTUAL_PARTS_APPROVED_DATA
GO

/****** Object:  View [dbo].[A_V_PEOPLE_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_BY_NTLOGIN]
AS
SELECT     dbo.A_PEOPLE.ID AS P_ID, dbo.A_PEOPLE_HISTORY.FULL_NAME AS P_NAME, dbo.A_PEOPLE_HISTORY.LAST_NAME
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE ON dbo.A_PEOPLE_HISTORY.ID = dbo.A_PEOPLE_SEARCH_TABLE.ID
GO

/****** Object:  View [dbo].[A_V_CO_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE  VIEW [dbo].[A_V_CO_BY_NTLOGIN]
AS
SELECT     P.ID, C.ID AS CO_ID, CH.NAME AS CO_NAME
FROM         dbo.A_COMPANIES C INNER JOIN
                      dbo.A_COMPANIES_HISTORY CH ON C.HISTORY_REF_ID = CH.ID INNER JOIN
                      dbo.A_PEOPLE_HISTORY PH ON C.ID = PH.COMPANY INNER JOIN
                      dbo.A_PEOPLE P ON PH.ID = P.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_TASK_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_TASK_SEARCH]
AS
SELECT     TASK.DESCRIPTION, TASK.STATUS, TASK.REQUESTOR, TASK.CHILD_ORDER, TASK.CREATED_BY, TASK.CREATE_DATE, dbo.leadingSpaces(TASK.ID, 
                      50) AS SORT_ID, TASK.SYSTEM_TASK, TASK.PROCEDURE_ID, REQUESTOR.P_NAME AS REQUESTOR_NAME, TASK.REQUESTEE_ID, 
                      TASK.GROUP_REQUESTEE_ID, TASK.ORIG_PLANNED_START_DATE, TASK.ORIG_PLANNED_STOP_DATE, TASK.CUR_PLANNED_START_DATE, 
                      TASK.CUR_PLANNED_STOP_DATE, TASK.ACTUAL_START_DATE, TASK.ACTUAL_STOP_DATE, TASK.CUR_PLANNED_COUNTER_START, 
                      TASK.LATEST_REQUESTEE_NAME, TASK.HAS_DISCUSSION, TASK.HAS_SURVEY, TASK.HAS_CHILD, TASK.HAS_REF_PROC, TASK.HAS_FILE, 
                      TASK.ORIG_REQUESTOR_ID, TASK.HAS_REF_OBJ, TASK.HAS_MONITOR, ORIG_REQUESTOR.FULL_NAME AS ORIG_REQUESTOR_NAME, 
                      TASK.COLOR_CODE,  sur.SURVEY_ID, TASK.LAST_REQUEST_DATE,dbo.getTaskParentList(TASK.ID) 
                      AS PARENT_LIST, TASK.PARENT_ID, dbo.isParentTask(TASK.ID) AS isParent, TASK.PRIORITY, 
                      dbo.A_TASK_PRIORITY_LEVELS.NAME AS PRIORITY_NAME,
                      dbo.A_TASK_COMPANIES_TO_VIEW_LINK.CO_ID AS ALLOWED_CO_ID, 
                      dbo.A_V_CO_BY_NTLOGIN.CO_NAME AS COMPANY_NAME, proj.PROJECT_ID, TASK.ID, TASK.CHILD_STATUS, TASK.PROCEDURE_STEP_ID, 
                      dbo.A_TASK_EMAIL_PEOPLE_LINK.PERSON_ID AS SPECIFIED_PERSON_ID, dbo.A_TASK_COMMENT.COMMENT AS LAST_COMMENT, 
                      dbo.A_TASK_COMMENT.DRCM AS LAST_COMMENT_DRCM, A_V_PEOPLE_BY_NTLOGIN_1.P_NAME AS LAST_COMMENT_WRITER, TASK.IS_QUOTE, 
                      TASK.IS_QUOTE_ACCEPT, TASK.IS_FILL, dbo.A_V_CO_BY_NTLOGIN.CO_ID, dbo.A_TASK_ORDER_INFORMATION.PURCHASE_HIST_ID, 
                      dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID, dbo.A_TASK_ORDER_INFORMATION.FILL_ID, 
                      dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ROLE, TASK.RECURSION_NUMBER, ASSIGNEE.FULL_NAME AS ASSIGNEE_NAME, 
                      ASSIGNEE.ID AS ASSIGNEE_ID, ASSIGNEE.SYSTEM_STATUS AS ASSIGNEE_STATUS
FROM         dbo.A_V_PEOPLE_DATA_QUICK ASSIGNEE INNER JOIN
                      dbo.A_TASK_PROCEDURE_ASSIGNEE ON ASSIGNEE.ID = dbo.A_TASK_PROCEDURE_ASSIGNEE.ASSIGNEE_ID RIGHT OUTER JOIN
                      dbo.A_TASKS TASK ON dbo.A_TASK_PROCEDURE_ASSIGNEE.TASK_ID = TASK.ID LEFT OUTER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON TASK.ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID LEFT OUTER JOIN
                      dbo.A_TASK_LAST_COMMENT INNER JOIN
                      dbo.A_TASK_COMMENT ON dbo.A_TASK_LAST_COMMENT.COMMENT_ID = dbo.A_TASK_COMMENT.ID INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN A_V_PEOPLE_BY_NTLOGIN_1 ON dbo.A_TASK_COMMENT.MODBY = A_V_PEOPLE_BY_NTLOGIN_1.P_ID ON 
                      TASK.ID = dbo.A_TASK_LAST_COMMENT.TASK_ID LEFT OUTER JOIN
                      dbo.A_TASK_EMAIL_PEOPLE_LINK ON TASK.ID = dbo.A_TASK_EMAIL_PEOPLE_LINK.TASK_ID LEFT OUTER JOIN
                      dbo.A_TASK_PROJECT_LINK proj ON TASK.ID = proj.TASK_ID LEFT OUTER JOIN
                      dbo.A_V_CO_BY_NTLOGIN ON TASK.REQUESTEE_ID = dbo.A_V_CO_BY_NTLOGIN.ID LEFT OUTER JOIN
                      dbo.A_TASK_COMPANIES_TO_VIEW_LINK ON TASK.ID = dbo.A_TASK_COMPANIES_TO_VIEW_LINK.TASK_ID 
					  
					  LEFT OUTER JOIN
                      dbo.A_TASK_PRIORITY_LEVELS ON TASK.PRIORITY = dbo.A_TASK_PRIORITY_LEVELS.ID
					  
					  LEFT OUTER JOIN
                      dbo.A_TASK_SURVEY_LINK sur ON TASK.ID = sur.TASK_ID LEFT OUTER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE ORIG_REQUESTOR ON TASK.ORIG_REQUESTOR_ID = ORIG_REQUESTOR.OBJ_ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN REQUESTOR ON TASK.REQUESTOR = REQUESTOR.P_ID
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACCOUNTS_APPROVED_DATA]
AS
SELECT     a.OBJECT_ID, a.NAME, a.REFERENCE_PO, a.REFERENCE_NAME, a.OPEN_DATE, a.CLOSE_DATE, a.SUPPLIER_CO, a.CUSTOMER_CO,dbo.A_OBJECTS.ROOT, 
                      a.CUSTOMER_BILL_CO, a.MAXIMUM_USES, a.TOTAL_PURCHASE_LIMIT, a.CREDIT_LIMIT, a.APPROVAL_WF, a.INVOICE_TRIGGER, 
                      a.INVOICE_PERIOD_NUMBER, a.INVOICE_PERIOD_TYPE, a.FIRST_INVOICE_DATE, a.NEXT_INVOICE_DATE, a.PAYMENT_GRACE_PERIOD, 
                      a.LATE_FEE_PERCENTAGE, a.DRCM, a.MODBY, a.ACCT_TYPE, a.TOTAL_PURCHASES, a.BALANCE, a.AMT_INVOICED, a.ACCT_STATUS, 
                      sup.NAME AS SUPPLIER_NAME, co.NAME AS CUSTOMER_NAME, bill_co.NAME AS CUST_BILL_NAME, dbo.A_ACCOUNTS.ID, 
                      dbo.A_ACCOUNTS.HISTORY_REF_ID, a.REAPPLY_LATE_FEE, a.PRODUCT_ID, a.PARENT_ACCOUNT, dbo.A_OBJECTS.CREATING_CO, 
                      dbo.A_OBJECTS.STATUS, a.INVOICED_BALANCE, a.UNINVOICED_BALANCE, a.HAS_CHILD, a.NONCONSUMABLE_INCLUDED, 
                      a.CONSUMABLES_INCLUDED, a.LABOR_INCLUDED, a.TOTAL_CREDITS, a.TOTAL_DEBITS, a.BILLING_EMAIL, a.TAX_RATE
FROM         dbo.A_ACCOUNTS_HISTORY a INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA sup ON a.SUPPLIER_CO = sup.ID INNER JOIN
                      dbo.A_ACCOUNTS ON a.ID = dbo.A_ACCOUNTS.HISTORY_REF_ID INNER JOIN
                      dbo.A_OBJECTS ON a.OBJECT_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA co ON a.CUSTOMER_CO = co.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA bill_co ON a.CUSTOMER_BILL_CO = bill_co.ID
WHERE     (dbo.A_OBJECTS.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_V_PEOPLE_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_PEOPLE_APPROVED_DATA]
AS
SELECT     p.ID, p.HISTORY_REF_ID, dbo.A_PEOPLE_HISTORY.LOGIN, dbo.A_PEOPLE_HISTORY.NAME, dbo.A_PEOPLE_HISTORY.BOSS, 
                      dbo.A_PEOPLE_HISTORY.SOURCE, dbo.A_PEOPLE_HISTORY.LAST_NAME, dbo.A_PEOPLE_HISTORY.MIDDLE_NAME, 
                      dbo.A_PEOPLE_HISTORY.NICK_NAME, dbo.A_PEOPLE_HISTORY.LANG, dbo.A_PEOPLE_HISTORY.HIRE_DATE, dbo.A_PEOPLE_HISTORY.DRCM, 
                      dbo.A_PEOPLE_HISTORY.MODBY, dbo.A_PEOPLE_HISTORY.OBJECT_ID, dbo.A_PEOPLE_HISTORY.COMPANY, 
                      dbo.A_PEOPLE_HISTORY.TIME_ZONE, dbo.A_PEOPLE_HISTORY.FULL_NAME, dbo.A_PEOPLE_HISTORY.SYSTEM_STATUS, 
                      dbo.A_PEOPLE_HISTORY.CO_POSITION, dbo.A_PEOPLE_HISTORY.ROOT_COMPANY, dbo.A_PEOPLE_HISTORY.IS_HEAD, p.STATUS, 
                      dbo.A_PEOPLE_HISTORY.TOOL_BOX, dbo.A_PEOPLE_HISTORY.INFO_BOX, dbo.A_PEOPLE_HISTORY.ADV_SEARCH, 
                      dbo.A_PEOPLE_HISTORY.COLOR_KEY, dbo.A_PEOPLE_HISTORY.SCREEN_TYPE
FROM         dbo.A_PEOPLE_HISTORY INNER JOIN
                      dbo.A_PEOPLE p ON dbo.A_PEOPLE_HISTORY.ID = p.HISTORY_REF_ID
WHERE     (dbo.A_PEOPLE_HISTORY.SYSTEM_STATUS = 'ACTIVE') AND (p.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LOGOS]
AS
SELECT     dbo.A_DOCUMENT_LINK.TYPE, dbo.A_DOCUMENT_LINK.OBJECT_ID, dbo.A_DOCUMENTS.ID, dbo.A_DOCUMENTS.NAME, 
                      dbo.A_DOCUMENTS.SERVER_PATH, dbo.A_DOCUMENTS.APPROVED, dbo.A_DOCUMENTS.DESCRIPTION, dbo.A_DOCUMENTS.ACTIVE, 
                      dbo.A_DOCUMENTS.CONTENTTYPE
FROM         dbo.A_DOCUMENTS INNER JOIN
                      dbo.A_DOCUMENT_LINK ON dbo.A_DOCUMENTS.ID = dbo.A_DOCUMENT_LINK.LINKED_DOC_ID
WHERE     (dbo.A_DOCUMENT_LINK.TYPE = N'LOGO')
GO

/****** Object:  View [dbo].[A_V_TASK_PURCHASE_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_PURCHASE_INFORMATION]
AS
SELECT     dbo.A_TASKS.ID, dbo.A_TASKS.DESCRIPTION, dbo.A_TASK_ORDER_INFORMATION.PURCHASE_HIST_ID, 
                      dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_PERSON, dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO, 
                      dbo.A_PURCHASES_HISTORY.PURCHASER, dbo.A_PURCHASES_HISTORY.PURCHASING_CO, dbo.A_PURCHASES_HISTORY.ORDER_ID, 
                      CUSTOMER_CO.NAME AS CUSTOMER_NAME, CUSTOMER.FULL_NAME AS CUSTOMER_PERSON_NAME, 
                      PURCHASER.FULL_NAME AS PURCHASER_PERSON_NAME, dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID, 
                      dbo.A_ORDER_ITEMS.ACCOUNT_ID, dbo.A_ORDER_ITEMS.BILL_TYPE, dbo.A_ORDER_ITEMS.TOTAL_PRICE, dbo.A_ORDER_ITEMS.PRODUCT_ID, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, dbo.A_PURCHASES.ID AS PURCHASE_ID, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.NAME AS ACCOUNT_NAME, dbo.A_V_ACCOUNTS_APPROVED_DATA.REFERENCE_PO AS ACCOUNT_REF_PO, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.REFERENCE_NAME AS ACCOUNT_REF_NAME, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.SUPPLIER_NAME AS ACCOUNT_SUPPLIER_NAME, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.CUSTOMER_NAME AS ACCOUNT_CUST_NAME, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA.COMPLETE_NAME AS CUST_LOC_NAME, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA.FULL_ADDRESS AS CUST_ADDRESS, dbo.A_V_LOCATIONS_APPROVED_DATA.CITY AS CUST_CITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA.STATE AS CUST_STATE, dbo.A_V_LOCATIONS_APPROVED_DATA.COUNTRY AS CUST_COUNTRY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA.POSTAL_CODE AS CUST_ZIP, SUPPLIER_CO.NAME AS SUPPLIER_NAME, 
                      A_V_LOCATIONS_APPROVED_DATA_1.COMPLETE_NAME AS SUPPLIER_LOC_NAME, 
                      A_V_LOCATIONS_APPROVED_DATA_1.ADDRESS_1 AS SUPPLIER_ADDRESS, A_V_LOCATIONS_APPROVED_DATA_1.CITY AS SUPPLIER_CITY, 
                      A_V_LOCATIONS_APPROVED_DATA_1.STATE AS SUPPLIER_STATE, A_V_LOCATIONS_APPROVED_DATA_1.COUNTRY AS SUPPLIER_COUNTRY, 
                      A_V_LOCATIONS_APPROVED_DATA_1.POSTAL_CODE AS SUPPLIER_ZIP, dbo.A_V_LOGOS.SERVER_PATH, dbo.A_V_LOGOS.ID AS LOGO_ID, 
                      SUPPLIER_CO.PHONE AS SUPPLIER_PHONE, dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM, dbo.A_PURCHASES_HISTORY.SUP_PURCH_NUM, 
                      SUPPLIER_CO.ID AS SUPPLIER_ID, CUSTOMER.ID AS CUSTOMER_ID
FROM         dbo.A_V_LOCATIONS_APPROVED_DATA A_V_LOCATIONS_APPROVED_DATA_1 RIGHT OUTER JOIN
                      dbo.A_V_LOGOS RIGHT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER_CO ON dbo.A_V_LOGOS.OBJECT_ID = SUPPLIER_CO.OBJECT_ID ON 
                      A_V_LOCATIONS_APPROVED_DATA_1.ID = SUPPLIER_CO.LOCATION RIGHT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_V_PRODUCTS_APPROVED_DATA.ID = dbo.A_ORDER_ITEMS.PRODUCT_ID ON 
                      SUPPLIER_CO.ID = dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ON dbo.A_ORDER_ITEMS.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA.ID RIGHT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA RIGHT OUTER JOIN
                      dbo.A_PURCHASES_HISTORY INNER JOIN
                      dbo.A_PURCHASES ON dbo.A_PURCHASES_HISTORY.ID = dbo.A_PURCHASES.HISTORY_REF_ID INNER JOIN
                      dbo.A_TASKS INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON dbo.A_TASKS.ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID ON 
                      dbo.A_PURCHASES.HISTORY_REF_ID = dbo.A_TASK_ORDER_INFORMATION.PURCHASE_HIST_ID INNER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA ON dbo.A_PURCHASES_HISTORY.ORDER_ID = dbo.A_V_ORDERS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUSTOMER_CO ON dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO = CUSTOMER_CO.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA PURCHASER ON dbo.A_PURCHASES_HISTORY.PURCHASER = PURCHASER.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA CUSTOMER ON dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_PERSON = CUSTOMER.ID ON 
                      dbo.A_V_LOCATIONS_APPROVED_DATA.ID = CUSTOMER_CO.LOCATION ON 
                      dbo.A_ORDER_ITEMS.ID = dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID
GO

/****** Object:  View [dbo].[Portal_MonitorsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Portal_MonitorsView]
AS
SELECT 
p.SERIAL as SERIAL,
I.FILL_ID as FillId,
I.ID as Id,
I.PARENT_ID as ParentId,
I.STATUS as Status,
at.COMMENT AS Comment,
I.REQUESTOR AS Requestor,
at.COMPLETED_BY AS CompletedBy,
ol.MODBY as Modby,
ol.DRCM as Drcm,
I.CHILD_ORDER as ChildOrder,
I.CREATED_BY as CreatedBy,
I.CREATE_DATE as CreateDate,
at.CLOSED as Closed,
at.OPENED_BY as OpenedBy,
at.OPEN_DATE as OpenDate,
I.SYSTEM_TASK as SystemTask,
I.PROCEDURE_ID as ProcedureId,
I.PROCEDURE_STEP_ID as ProcedureStepId,
at.SECURITY_LEVEL as SecurityLevel,
at.COUNTER as Counter,
I.REQUESTEE_ID as RequesteeId,
I.GROUP_REQUESTEE_ID as GroupRequesteeId,
at.COUNTER_NAME as CounterName,
at.COUNTER_VALUE as CounterValue,
I.ORIG_PLANNED_START_DATE as OrigPlannedStartDate,
I.ORIG_PLANNED_STOP_DATE as OrigPlannedStopDate,
I.CUR_PLANNED_START_DATE as CurPlannedStartDate,
I.CUR_PLANNED_STOP_DATE as CurPlannedStopDate,

I.ACTUAL_START_DATE as ActualStartDate,
I.ACTUAL_STOP_DATE as ActualStopDate,

at.ORIG_PLANNED_COUNTER_START as OrigPlannedCounterStart,
at.ORIG_PLANNED_COUNTER_STOP as OrigPlannedCounterStop,
I.CUR_PLANNED_COUNTER_START as CurPlannedCounterStart,
at.CUR_PLANNED_COUNTER_STOP as CurPlannedCounterStop,
at.ACTUAL_COUNTER_START as ActualCounterStart,
at.ACTUAL_COUNTER_STOP as ActualCounterStop,
I.LATEST_REQUESTEE_NAME as LatestRequesteeName,
I.HAS_DISCUSSION as HasDiscussion,
I.HAS_SURVEY as HasSurvey,
I.HAS_CHILD as HasChild,
I.HAS_REF_PROC as HasRefProc,
I.HAS_FILE as HasFile,
I.ORIG_REQUESTOR_ID as OrigRequestorId,
I.HAS_REF_OBJ as HasRefObj,
I.HAS_MONITOR as HasMonitor,
I.COLOR_CODE as ColorCode,
I.LAST_REQUEST_DATE as LastRequestDate,
I.PRIORITY as Priority,
I.CHILD_STATUS as ChildStatus,
I.IS_QUOTE as IsQuote,
I.IS_QUOTE_ACCEPT as IsQuoteAccept,
I.IS_FILL as IsFill,
at.MANAGER as Manager,
at.DNR_TYPE as DnrType,
I.RECURSION_NUMBER as RecursionNumber,
at.REPEAT_FROM_STEP as RepeatFromStep,
I.SORT_ID as SortId,
I.DESCRIPTION as Description,
I.PURCHASE_HIST_ID as PurchaseHistId,
info.CUSTOMER_PERSON as CustomerPerson,
info.CUSTOMER_CO as CustomerCo,
info.PURCHASER as Purchaser,
info.PURCHASING_CO as PurchasingCo,
info.ORDER_ID as OrderId,
info.CUSTOMER_NAME as CustomerName,
info.CUSTOMER_PERSON_NAME as CustomerPersonName,
info.PURCHASER_PERSON_NAME as PurchaserPersonName,
I.PURCHASE_ITEM_ID as PurchaseItemId,
info.ACCOUNT_ID as AccountId,
info.BILL_TYPE as BillType,
info.TOTAL_PRICE as TotalPrice,
info.PRODUCT_ID as ProductId,
info.PRODUCT_NAME as ProductName,
info.PURCHASE_ID as PurchaseId,
info.ACCOUNT_NAME as AccountName,
info.ACCOUNT_REF_PO as AccountRefPo,
info.ACCOUNT_REF_NAME as AccountRefName,
info.ACCOUNT_SUPPLIER_NAME as AccountSupplierName,
info.ACCOUNT_CUST_NAME as AccountCustName,
info.CUST_LOC_NAME as CustLocName,
info.CUST_ADDRESS as CustAddress,
info.CUST_CITY as CustCity,
info.CUST_STATE as CustState,
info.CUST_COUNTRY as CustCountry,
info.CUST_ZIP as CustZip,
info.SUPPLIER_NAME as SupplierName,
info.SUPPLIER_LOC_NAME as SupplierLocName,
info.SUPPLIER_ADDRESS as SupplierAddress,
info.SUPPLIER_CITY as SupplierCity,
info.SUPPLIER_STATE as SupplierState,
info.SUPPLIER_COUNTRY as SupplierCountry,
info.SUPPLIER_ZIP as SupplierZip,
info.SERVER_PATH as ServerPath,
info.LOGO_ID as LogoId,
info.SUPPLIER_PHONE as SupplierPhone,

info.CUST_PURCH_NUM as CustPurchNum,

info.SUP_PURCH_NUM as SupPurchNum,
info.SUPPLIER_ID as SupplierId,
info.CUSTOMER_ID as CustomerId
from 
A_V_TASK_SEARCH AS I
inner JOIN [A_TASKS] at on I.ID = at.ID
	 inner JOIN [A_V_TASK_PURCHASE_INFORMATION] info on I.ID = info.ID
	 inner JOIN [A_TASK_OBJECT_LINK] ol on at.ID = ol.TASK_ID
	  INNER JOIN A_V_ACTUAL_PARTS_APPROVED_DATA p on p.OBJECT_ID = ol.OBJECT_ID
GO

/****** Object:  View [dbo].[Portal_MonitorsWithTaskAndResults]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[Portal_MonitorsWithTaskAndResults]
AS
SELECT     TOP 100 PERCENT 
t.ID,
t.MONITOR_TYPE AS MonitorType,
t.DESCRIPTION AS Description,
t.START_SYSTEM_TASK,
t.START_TYPE,
t.STOP_SYSTEM_TASK,
t.STOP_TYPE, 
t.COUNTER_OR_CLOCK,
t.CLOCK_UNIT,
t.HIGHEST_THRESHOLD,
t.HIGH_THRESHOLD,
t.TARGET,
t.LOW_THRESHOLD,
t.LOWEST_THRESHOLD, 
t.SHOULD_BE,
t.OPINION,
t.TARGET_ANSWER_ID, 
t.RELATED_OBJECT_TYPE,
t.RELATED_OBJECT_DESCRIPTION,
t.HIDE_TARGET,
t.USE_RESULT, 
t.FAIL_STOP,
t.YES_NO_ANSWER, 
t.CORRECT_ANSWER_ID,
t.TEXT_TARGET,
t.TASK_ID AS TaskId,
t.ROLL_UP_ID, 
r.NUM_VAL,
r.TEXT_VAL, 
r.MULT_CHOICE_ANSWER,
r.PRINT_RESULT AS PrintResult, 
r.COMMENT, 
t.CREATED_BY,
t.IS_PASSING,
t.PROCEDURE_ID,
t.STEP_ID, 
t.PEOPLE_ID,
t.PART_ID, 
t.OBJECT_ID,
t.IS_AUTO, 
t.MY_ANSWER, 
t.TOLERANCE, 
t.RELATED_OBJECT_ID,
t.FAIL_ACTION, 
dbo.A_TASKS.STATUS AS TASK_STATUS, 
cast(dbo.A_TASKS.ACTUAL_STOP_DATE AS date) AS TaskStopDate, 
dbo.A_TASKS.PROCEDURE_ID AS TASK_PROCEDURE_ID, 
dbo.A_TASKS.LATEST_REQUESTEE_NAME AS WORKER_NAME, 
dbo.A_TASKS.PARENT_ID AS TASK_PARENT,
t.PRINT_ORDER, 
t.TARGET_OBJECT_TYPE, 
t.TARGET_OBJECT, 
t.SKIP_MODE, 
t.ALWAYS_PASS,
t.CANT_CHANGE, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SYS_NAME, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL AS Serial, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NICK_NAME,
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION_NAME, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC AS PartDescription, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_TYPE_NAME, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NAME, 
dbo.A_OBJECTS.ROOT AS ActualPartId
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_OBJECTS.ROOT = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK ON dbo.A_OBJECTS.ID = dbo.A_TASK_OBJECT_LINK.OBJECT_ID RIGHT OUTER JOIN
                      dbo.A_MONITOR_TEMPLATES t INNER JOIN
                      dbo.A_TASKS ON t.TASK_ID = dbo.A_TASKS.ID ON dbo.A_TASK_OBJECT_LINK.TASK_ID = dbo.A_TASKS.ID LEFT OUTER JOIN
                      dbo.A_MONITOR_RESULTS r ON t.ID = r. MONITOR_TEMPLATE_ID
ORDER BY dbo.A_TASKS.ACTUAL_STOP_DATE DESC
GO

/****** Object:  View [dbo].[Portal_MonitorView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[Portal_MonitorView]
as
SELECT  TOP (100) PERCENT t.ID as Id,
t.MONITOR_TYPE as MonitorType,
t.DESCRIPTION as Description,
t.START_SYSTEM_TASK as StartSystemTask,
t.START_TYPE as StartType,
t.STOP_SYSTEM_TASK as StopSystemTask,
t.STOP_TYPE as StopType, 
t.COUNTER_OR_CLOCK as CounterOrClock,
t.CLOCK_UNIT as ClockUnit, 
t.HIGHEST_THRESHOLD as HighestThreshold,
t.HIGH_THRESHOLD as HighThreshold,
t.TARGET as Target, 
t.LOW_THRESHOLD as LowThreshold, 
t.LOWEST_THRESHOLD as LowestThreshold, 
t.SHOULD_BE as ShouldBe, 
t.OPINION as Opinion, 
t.TARGET_ANSWER_ID as TargetAnswerId,
t.RELATED_OBJECT_TYPE as RelatedObjectType,
t.RELATED_OBJECT_DESCRIPTION as RelatedObjectDescription,
t.HIDE_TARGET as HideTarget,
t.USE_RESULT as UseResult, 
t.FAIL_STOP as FailStop,
t.YES_NO_ANSWER as YesNoAnswer,
t.CORRECT_ANSWER_ID as CorrectAnswerId,
t.TEXT_TARGET as TextTarget,
t.TASK_ID as TaskId,
t.ROLL_UP_ID as RollUpId,
r.NUM_VAL as NumVal, 
r.TEXT_VAL as TextVal, 
r.MULT_CHOICE_ANSWER as MultChoiceAnswer,
r.PRINT_RESULT as PrintResult, 
r.COMMENT as Comment, 
t.CREATED_BY as CreatedBy, 
t.IS_PASSING as IsPassing, 
t.PROCEDURE_ID as ProcedureId, 
t.STEP_ID as StepId, 
t.PEOPLE_ID as PeopleId, 
t.PART_ID as PartId, 
t.OBJECT_ID as ObjectId,
t.IS_AUTO as IsAuto,
t.MY_ANSWER as MyAnswer, 
t.TOLERANCE as Tolerance,
t.RELATED_OBJECT_ID as RelatedObjectId,
t.FAIL_ACTION as FailAction, 
dbo.A_TASKS.STATUS AS  taskStatus, 
dbo.A_TASKS.ACTUAL_STOP_DATE AS TaskStopDate,
dbo.A_TASKS.PROCEDURE_ID AS TaskProcedureId, 
dbo.A_TASKS.LATEST_REQUESTEE_NAME AS WorkerName, 
dbo.A_TASKS.PARENT_ID AS TaskParent,
t.PRINT_ORDER as PrintOrder, 
t.TARGET_OBJECT_TYPE as TargetObjectType,
t.TARGET_OBJECT as TargetObject, 
t.SKIP_MODE as SkipMode,
t.ALWAYS_PASS as AlwaysPass,
t.CANT_CHANGE as CantChange, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SYS_NAME as ActualPartsApprovedDataSys,
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL as ActualPartsApprovedDataSerial, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NICK_NAME as ActualPartsApprovedDataNickName, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION_NAME as ActualPartsApprovedDataLocationName, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME as ActualPartsApprovedDataCurrentOwnerName,
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER as ActualPartsApprovedDataCompnyPartNum, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC as ActualPartsApprovedDataPartDesc,
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_TYPE_NAME as ActualPartsApprovedDataPartTypeName, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY as ActualPartsApprovedDataQty,
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NAME as ActualPartsApprovedDataName, 
dbo.A_OBJECTS.ROOT AS ObjectsRoot,
r.DRCM AS UpdatedDate
FROM            dbo.A_OBJECTS INNER JOIN
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_OBJECTS.ROOT = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID RIGHT OUTER JOIN
dbo.A_TASK_OBJECT_LINK ON dbo.A_OBJECTS.ID = dbo.A_TASK_OBJECT_LINK.OBJECT_ID RIGHT OUTER JOIN
dbo.A_MONITOR_TEMPLATES AS t INNER JOIN
dbo.A_TASKS ON t.TASK_ID = dbo.A_TASKS.ID ON dbo.A_TASK_OBJECT_LINK.TASK_ID = dbo.A_TASKS.ID LEFT OUTER JOIN
dbo.A_MONITOR_RESULTS AS r ON t.ID = r.MONITOR_TEMPLATE_ID
ORDER BY TaskStopDate DESC
GO

/****** Object:  View [dbo].[A_V_MONITORS_WITH_TASK_AND_RESULT]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_MONITORS_WITH_TASK_AND_RESULT]
AS
SELECT     TOP 100 PERCENT t.ID, t.MONITOR_TYPE, t.DESCRIPTION, t.START_SYSTEM_TASK, t.START_TYPE, t.STOP_SYSTEM_TASK, t.STOP_TYPE, 
                      t.COUNTER_OR_CLOCK, t.CLOCK_UNIT, t.HIGHEST_THRESHOLD, t.HIGH_THRESHOLD, t.TARGET, t.LOW_THRESHOLD, t.LOWEST_THRESHOLD, 
                      t.SHOULD_BE, t.OPINION, t.TARGET_ANSWER_ID, t.RELATED_OBJECT_TYPE, t.RELATED_OBJECT_DESCRIPTION, t.HIDE_TARGET, t.USE_RESULT, 
                      t.FAIL_STOP, t.YES_NO_ANSWER, t.CORRECT_ANSWER_ID, t.TEXT_TARGET, t.TASK_ID, t.ROLL_UP_ID, r.NUM_VAL, r.TEXT_VAL, 
                      r.MULT_CHOICE_ANSWER, r.PRINT_RESULT, r.COMMENT, t.CREATED_BY, t.IS_PASSING, t.PROCEDURE_ID, t.STEP_ID, t.PEOPLE_ID, t.PART_ID, 
                      t.OBJECT_ID, t.IS_AUTO, t.MY_ANSWER, t.TOLERANCE, t.RELATED_OBJECT_ID, t.FAIL_ACTION, dbo.A_TASKS.STATUS AS TASK_STATUS, 
                      dbo.A_TASKS.ACTUAL_STOP_DATE AS TASK_STOP_DATE, dbo.A_TASKS.PROCEDURE_ID AS TASK_PROCEDURE_ID, 
                      dbo.A_TASKS.LATEST_REQUESTEE_NAME AS WORKER_NAME, dbo.A_TASKS.PARENT_ID AS TASK_PARENT, t.PRINT_ORDER, 
                      t.TARGET_OBJECT_TYPE, t.TARGET_OBJECT, t.SKIP_MODE, t.ALWAYS_PASS, t.CANT_CHANGE, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SYS_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NICK_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_TYPE_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NAME, 
                      dbo.A_OBJECTS.ROOT AS ACTUAL_PART_ID
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_OBJECTS.ROOT = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK ON dbo.A_OBJECTS.ID = dbo.A_TASK_OBJECT_LINK.OBJECT_ID RIGHT OUTER JOIN
                      dbo.A_MONITOR_TEMPLATES t INNER JOIN
                      dbo.A_TASKS ON t.TASK_ID = dbo.A_TASKS.ID ON dbo.A_TASK_OBJECT_LINK.TASK_ID = dbo.A_TASKS.ID LEFT OUTER JOIN
                      dbo.A_MONITOR_RESULTS r ON t.ID = r.MONITOR_TEMPLATE_ID
ORDER BY dbo.A_TASKS.ACTUAL_STOP_DATE DESC
GO

/****** Object:  View [dbo].[A_V_LOCATION_WITH_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_LOCATION_WITH_NAME]
AS
SELECT     dbo.A_LOCATIONS_HISTORY.NAME AS LOCATION_NAME, dbo.A_LOCATIONS.ID AS L_ID
FROM         dbo.A_LOCATIONS INNER JOIN
                      dbo.A_LOCATIONS_HISTORY ON dbo.A_LOCATIONS.HISTORY_REF_ID = dbo.A_LOCATIONS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PRODUCTS_WITH_ACTUAL_PARTS_THEY_ARE_USED_ON]
AS
SELECT     P_OBJ_USED_ON.OBJECT_ID, PTS.ID AS PART_ID, PH.NAME AS PART_NAME, PROD_HIST.NAME AS PRODUCT_NAME, PROD.ID AS PRODUCT_ID, 
                      APH.LOCATION, APH.NICK_NAME, APH.PART_ID AS AP_PART_ID, APH.QTY, APH.SERIAL, APH.CUR_OWNER, APH.ASSEMBLY_WT, APH.AP_STATUS, 
                      APH.ROOT_ID, APH.ROOT_STATUS, PRO_PART_LINK.ID AS INSTALLED_ID, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS OWNER_NAME, 
                      dbo.A_V_LOCATION_WITH_NAME.LOCATION_NAME, APH.OBJECT_ID AS AP_OBJECT_ID, dbo.A_FN_ACTUAL_PART_HAS_CHILD(AP.ID) AS HAS_CHILD, 
                      APH.PARENT_ID, dbo.A_FN_PRODUCT_INSTALLED_STATUS(PRO_PART_LINK.ID) AS PROD_STATUS, AP.ID AS AP_ID, o.LOCKED_BY, 
                      o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.LOCKED_BY_NAME, 
                      o.CREATING_CO_NAME, o.ID AS OBJ_ID, ISNULL('(' + AP.ID + ')', '') + ISNULL(' ' + APH.NICK_NAME, '') + ISNULL(' (' + APH.SERIAL + ')', '') 
                      + ISNULL(' [' + PH.NAME + '] ', '') AS AP_DESC, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NAME AS PARENT_NAME
FROM         dbo.A_PRODUCTS PROD INNER JOIN
                      dbo.A_PARTS_HISTORY PH INNER JOIN
                      dbo.A_PARTS PTS ON PH.ID = PTS.PARTS_HISTORY_ID INNER JOIN
                      dbo.A_PRODUCT_OBJ_USED_ON_LINK P_OBJ_USED_ON ON PTS.ID = P_OBJ_USED_ON.OBJECT_ID INNER JOIN
                      dbo.A_PRODUCTS_HISTORY PROD_HIST ON P_OBJ_USED_ON.PRODUCT_ID = PROD_HIST.ID INNER JOIN
                      dbo.A_ACTUAL_PARTS AP INNER JOIN
                      dbo.A_ACTUAL_PARTS_HISTORY APH ON AP.HISTORY_REF_ID = APH.ID ON PTS.ID = APH.PART_ID ON 
                      PROD.HISTORY_REF_ID = PROD_HIST.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON APH.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_LOCATION_WITH_NAME ON APH.LOCATION = dbo.A_V_LOCATION_WITH_NAME.L_ID INNER JOIN
                      dbo.A_OBJECTS o ON APH.OBJECT_ID = o.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON APH.PARENT_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_ACTUAL_PART_PRODUCTS_INSIDE_LINK PRO_PART_LINK ON PROD.ID = PRO_PART_LINK.PRODUCT_ID AND 
                      APH.ID = PRO_PART_LINK.ACTUAL_PART_ID
GO

/****** Object:  View [dbo].[A_V_BATCH_TASK_CHILDREN]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_BATCH_TASK_CHILDREN]
AS
SELECT     BATCH_TOI.TASK_ID AS BATCH_TASK_ID, CHILD_TOI.TASK_ID AS CHILD_TASK_ID
FROM         dbo.A_FILLS F INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION CHILD_TOI ON F.ID = CHILD_TOI.FILL_ITEM_ID INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION BATCH_TOI ON F.BATCH_PARENT = BATCH_TOI.FILL_ITEM_ID
GO

/****** Object:  View [dbo].[A_V_COMPANY_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_COMPANY_LOGOS]
AS
SELECT     dbo.A_DOCUMENT_LINK.TYPE, dbo.A_COMPANIES.ID AS CO_ID, dbo.A_DOCUMENT_LINK.LINKED_DOC_ID
FROM         dbo.A_COMPANIES INNER JOIN
                      dbo.A_COMPANIES_HISTORY ON dbo.A_COMPANIES.HISTORY_REF_ID = dbo.A_COMPANIES_HISTORY.ID INNER JOIN
                      dbo.A_DOCUMENT_LINK ON dbo.A_COMPANIES_HISTORY.OBJECT_ID = dbo.A_DOCUMENT_LINK.OBJECT_ID
WHERE     (dbo.A_DOCUMENT_LINK.TYPE = N'LOGO')
GO

/****** Object:  View [dbo].[A_V_SHIPPING_TASK_FROM_BATCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SHIPPING_TASK_FROM_BATCH]
AS
SELECT     SHIP_TASK.ID AS TASK_ID, SHIP_TASK.SYSTEM_TASK, cust.NAME AS CUST_NAME, sup.NAME AS SUP_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, toi.PURCHASE_ITEM_ID, 
                      SHIP_TASK.PARENT_ID AS PARENT_TASK_ID, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROCEDURE_NAME, SHIP_TASK.REQUESTEE_ID, SHIP_TASK.GROUP_REQUESTEE_ID, 
                      dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM, pi.MT_NUM, SHIP_TASK.STATUS, pi.DUE_DATE, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.NAME AS DEST_NAME, SUPPLIER_LOC.NAME AS FROM_NAME, 
                      SUPPLIER_LOC.ADDRESS_1 AS FROM_ADD_1, SUPPLIER_LOC.ADDRESS_2 AS FROM_ADD_2, SUPPLIER_LOC.CITY AS FROM_CITY, 
                      SUPPLIER_LOC.STATE AS FROM_STATE, SUPPLIER_LOC.COUNTRY AS FROM_COUNTRY, SUPPLIER_LOC.POSTAL_CODE AS FROM_ZIP, 
                      SUPPLIER_LOC.INTERNAL_ADDRESS AS FROM_INTERNAL, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_1 AS TO_ADD_1, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_2 AS TO_ADD_2, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.CITY AS TO_CITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.STATE AS TO_STATE, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.COUNTRY AS TO_COUNTRY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.POSTAL_CODE AS TO_ZIP, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.INTERNAL_ADDRESS AS TO_INTERNAL, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID AS TO_LOC_ID, SUPPLIER_LOC.ID AS FROM_LOC_ID, 
                      dbo.A_PURCHASES_HISTORY.OBJECT_ID AS PURCHASE_ID, dbo.A_V_COMPANY_LOGOS.LINKED_DOC_ID AS SUP_LOGO, 
                      A_V_COMPANY_LOGOS_1.LINKED_DOC_ID AS CUST_LOGO, A_V_COMPANY_LOGOS_2.LINKED_DOC_ID AS SUP_ROOT_LOGO, pi.SHIP_DATE, 
                      sup.PHONE AS FROM_PHONE, pi.CUST_LINE_ITEM, toi.FILL_ITEM_ID
FROM         dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK RIGHT OUTER JOIN
                      dbo.A_TASK_ORDER_INFORMATION A_TASK_ORDER_INFORMATION_1 ON 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID = A_TASK_ORDER_INFORMATION_1.ACTUAL_TO_LOC RIGHT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS A_V_COMPANY_LOGOS_1 RIGHT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK acc INNER JOIN
                      dbo.A_ORDER_ITEMS pi ON acc.ID = pi.ACCOUNT_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK sup ON acc.SUPPLIER_CO = sup.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK cust ON acc.CUSTOMER_CO = cust.ID ON toi.PURCHASE_ITEM_ID = pi.ID ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.ID = pi.PRODUCT_ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON pi.PURCHASE_HIST_ID = dbo.A_PURCHASES_HISTORY.ID INNER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK SUPPLIER_LOC ON sup.LOCATION = SUPPLIER_LOC.ID INNER JOIN
                      dbo.A_V_BATCH_TASK_CHILDREN INNER JOIN
                      dbo.A_TASKS BATCH_TASK INNER JOIN
                      dbo.A_TASKS SHIP_TASK ON BATCH_TASK.ID = SHIP_TASK.PARENT_ID ON dbo.A_V_BATCH_TASK_CHILDREN.BATCH_TASK_ID = BATCH_TASK.ID ON 
                      toi.TASK_ID = dbo.A_V_BATCH_TASK_CHILDREN.CHILD_TASK_ID INNER JOIN
                      dbo.A_TASK_OBJECT_LINK INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID ON 
                      dbo.A_V_BATCH_TASK_CHILDREN.CHILD_TASK_ID = dbo.A_TASK_OBJECT_LINK.TASK_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS ON sup.ID = dbo.A_V_COMPANY_LOGOS.CO_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS A_V_COMPANY_LOGOS_2 ON sup.ROOT_CO_ID = A_V_COMPANY_LOGOS_2.CO_ID ON 
                      A_V_COMPANY_LOGOS_1.CO_ID = cust.ID ON A_TASK_ORDER_INFORMATION_1.TASK_ID = SHIP_TASK.ID
WHERE     (SHIP_TASK.STATUS IN ('REQUESTED', 'ACCEPTED')) AND (SHIP_TASK.SYSTEM_TASK IN ('SYS_RECEIVE', 'SYS_SEND'))
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_CALL_DATA_COMPLETE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_CALL_DATA_COMPLETE]
AS
SELECT     dbo.A_ACTUAL_PARTS_CALL_DATA.ACTUAL_PART_ID, dbo.A_ACTUAL_PARTS_CALL_DATA.PURCHASE_HIST_ID, 
                      dbo.A_ACTUAL_PARTS_CALL_DATA.ROOT_TASK, dbo.A_ACTUAL_PARTS_CALL_DATA.REASON, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION,
                       dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NICK_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_ID, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CUR_OWNER, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.AP_STATUS, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_TYPE_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NAME AS AP_NAME
FROM         dbo.A_ACTUAL_PARTS_CALL_DATA INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_ACTUAL_PARTS_CALL_DATA.ACTUAL_PART_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_INSTALLED_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_INSTALLED_PRODUCTS]
AS
SELECT     dbo.A_ACTUAL_PART_PRODUCTS_INSIDE_LINK.PRODUCT_ID AS PRODUCT_ID, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.*, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME
FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA INNER JOIN
                      dbo.A_ACTUAL_PART_PRODUCTS_INSIDE_LINK ON 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID = dbo.A_ACTUAL_PART_PRODUCTS_INSIDE_LINK.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON dbo.A_ACTUAL_PART_PRODUCTS_INSIDE_LINK.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_TASK_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_TASK_ID]
AS
SELECT     dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.*, dbo.A_ACTUAL_PARTS_CALL_DATA.ROOT_TASK AS TASK_ID
FROM         dbo.A_V_ACTUAL_PARTS_APPROVED_DATA INNER JOIN
                      dbo.A_ACTUAL_PARTS_CALL_DATA ON dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID = dbo.A_ACTUAL_PARTS_CALL_DATA.ACTUAL_PART_ID
GO

/****** Object:  View [dbo].[A_V_DNR_ACTUAL_PARTS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_DNR_ACTUAL_PARTS_INFO]
AS
SELECT     dbo.A_DNR_PART_INFO.ACT_PART_ID, dbo.A_DNR_PART_INFO.DNR_ID, dbo.A_DNR_PART_INFO.TASK_ID, 
                      dbo.A_DNR_PART_INFO.CUR_OWNER_NAME, dbo.A_DNR_PART_INFO.CUR_OWNER_ID, dbo.A_DNR_PART_INFO.ORIG_OWNER_NAME, 
                      dbo.A_DNR_PART_INFO.ORIG_OWNER_ID, dbo.A_DNR_PART_INFO.DNR_STATUS, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.NICK_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.AP_STATUS, dbo.A_V_PARTS_APPROVED_DATA.NAME AS PART_NAME, 
                      dbo.A_V_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER
FROM         dbo.A_DNR_PART_INFO INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_DNR_PART_INFO.ACT_PART_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA ON dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_ID = dbo.A_V_PARTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_SHIPPER_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SHIPPER_TASKS]
AS
SELECT     task.ID AS TASK_ID, task.SYSTEM_TASK, cust.NAME AS CUST_NAME, sup.NAME AS SUP_NAME, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.LOCATION_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.CURRENT_OWNER_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.QTY, toi.PURCHASE_ITEM_ID, 
                      task.PARENT_ID AS PARENT_TASK_ID, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROCEDURE_NAME, task.REQUESTEE_ID, task.GROUP_REQUESTEE_ID, 
                      dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM, pi.MT_NUM, task.STATUS, pi.DUE_DATE, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.NAME AS DEST_NAME, SUPPLIER_LOC.NAME AS FROM_NAME, 
                      SUPPLIER_LOC.ADDRESS_1 AS FROM_ADD_1, SUPPLIER_LOC.ADDRESS_2 AS FROM_ADD_2, SUPPLIER_LOC.CITY AS FROM_CITY, 
                      SUPPLIER_LOC.STATE AS FROM_STATE, SUPPLIER_LOC.COUNTRY AS FROM_COUNTRY, SUPPLIER_LOC.POSTAL_CODE AS FROM_ZIP, 
                      SUPPLIER_LOC.INTERNAL_ADDRESS AS FROM_INTERNAL, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_1 AS TO_ADD_1, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_2 AS TO_ADD_2, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.CITY AS TO_CITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.STATE AS TO_STATE, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.COUNTRY AS TO_COUNTRY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.POSTAL_CODE AS TO_ZIP, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.INTERNAL_ADDRESS AS TO_INTERNAL, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID AS TO_LOC_ID, SUPPLIER_LOC.ID AS FROM_LOC_ID, 
                      dbo.A_PURCHASES_HISTORY.OBJECT_ID AS PURCHASE_ID, dbo.A_V_COMPANY_LOGOS.LINKED_DOC_ID AS SUP_LOGO, 
                      A_V_COMPANY_LOGOS_1.LINKED_DOC_ID AS CUST_LOGO, A_V_COMPANY_LOGOS_2.LINKED_DOC_ID AS SUP_ROOT_LOGO, pi.SHIP_DATE, 
                      sup.PHONE AS FROM_PHONE, pi.CUST_LINE_ITEM, toi.FILL_ITEM_ID
FROM         dbo.A_TASKS task INNER JOIN
                      dbo.A_TASK_OBJECT_LINK INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_TASKS parent ON toi.TASK_ID = parent.ID INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK acc INNER JOIN
                      dbo.A_ORDER_ITEMS pi ON acc.ID = pi.ACCOUNT_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK sup ON acc.SUPPLIER_CO = sup.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK cust ON acc.CUSTOMER_CO = cust.ID ON toi.PURCHASE_ITEM_ID = pi.ID ON 
                      dbo.A_TASK_OBJECT_LINK.TASK_ID = parent.ID INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON pi.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON pi.PURCHASE_HIST_ID = dbo.A_PURCHASES_HISTORY.ID INNER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK SUPPLIER_LOC ON sup.LOCATION = SUPPLIER_LOC.ID ON 
                      task.PARENT_ID = parent.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK RIGHT OUTER JOIN
                      dbo.A_TASK_ORDER_INFORMATION A_TASK_ORDER_INFORMATION_1 ON 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID = A_TASK_ORDER_INFORMATION_1.ACTUAL_TO_LOC ON 
                      task.ID = A_TASK_ORDER_INFORMATION_1.TASK_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS ON sup.ID = dbo.A_V_COMPANY_LOGOS.CO_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS A_V_COMPANY_LOGOS_2 ON sup.ROOT_CO_ID = A_V_COMPANY_LOGOS_2.CO_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANY_LOGOS A_V_COMPANY_LOGOS_1 ON cust.ID = A_V_COMPANY_LOGOS_1.CO_ID
WHERE     (task.STATUS IN ('REQUESTED', 'ACCEPTED')) AND (task.SYSTEM_TASK IN ('SYS_RECEIVE', 'SYS_SEND'))
GO

/****** Object:  View [dbo].[A_V_DELIVERY_TICKET_PURCH_ITEM_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_DELIVERY_TICKET_PURCH_ITEM_INFO]
AS
SELECT   
dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PROD_NAME,
PURCHASE_ITEM.QTY AS PURCHASE_QTY,
PURCHASE_ITEM.PURCHASE_HIST_ID, 
dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROC_NAME,
dbo.A_V_PRODUCTS_APPROVED_DATA.HISTORY_REF_ID AS PROD_HIST_ID, 
PURCHASE_ITEM.CUST_LINE_ITEM,
 PURCHASE_ITEM.PARENT, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_TYPE_NAME, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER,
PURCHASE_ITEM.ID AS PURCH_ITEM_ID, 
dbo.A_PURCHASES_HISTORY.CUST_PURCH_NUM,
dbo.A_PURCHASES_HISTORY.OBJECT_ID AS PURCHASE_ID, 
FILLS.DONT_BILL, 
dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID AS ACTUAL_PART_ID,
dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID, 
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.SUPPLIER_CO AS ACCT_SUPPLIER, 
dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.CUSTOMER_CO AS ACCT_CUSTOMER,
-1 AS PARENT_ID,
-1 AS ID
FROM         dbo.A_FILLS FILLS INNER JOIN
                      dbo.A_ORDER_ITEMS PURCHASE_ITEM ON FILLS.PURCH_ITEM_ID = PURCHASE_ITEM.ID INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON PURCHASE_ITEM.PURCHASE_HIST_ID = dbo.A_PURCHASES_HISTORY.ID INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK ON 
                      PURCHASE_ITEM.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON FILLS.FILL_OBJ_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID ON 
                      PURCHASE_ITEM.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
WHERE     (PURCHASE_ITEM.PARENT IS NULL)
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_RELATED_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_RELATED_FILES]
AS
SELECT     AP.ID AS ACTUAL_PART_ID, AP.LOCATION_NAME, AP.COMPANY_PART_NUMBER, AP.NICK_NAME, AP.QTY, AP.SERIAL, AP.CURRENT_OWNER_NAME, 
                      AP.PART_DESC, AP.NAME, APRF.ID AS FILE_LINK_ID, APRF.FILE_ID, APRF.STATUS, APRF.DATE_DELETED, APRF.DELETED_BY, 
                      D.NAME AS FILE_NAME, D.DESCRIPTION AS FILE_DESCRIPTION, P.FULL_NAME AS DELETED_BY_NAME, D.CREATOR_ID
FROM         dbo.A_ACTUAL_PARTS_RELATED_FILES APRF INNER JOIN
                      dbo.A_DOCUMENTS D ON APRF.FILE_ID = D.ID INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA AP ON APRF.ACTUAL_PART_ID = AP.ID LEFT OUTER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE P ON APRF.DELETED_BY = P.OBJ_ID
GO

/****** Object:  View [dbo].[A_V_MONITOR_LABEL_PURCHASE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_MONITOR_LABEL_PURCHASE_ITEMS]
AS
SELECT     dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, PURCHASE_ITEM.ID AS PURCH_ITEM_ID, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID AS ACTUAL_PART_ID, dbo.A_MONITOR_TEMPLATES.DESCRIPTION, 
                      dbo.A_TASKS.LATEST_REQUESTEE_NAME, dbo.A_MONITOR_RESULTS.PRINT_RESULT, dbo.A_MONITOR_TEMPLATES.IS_PASSING, 
                      dbo.A_MONITOR_TEMPLATES.MY_ANSWER, dbo.A_MONITOR_TEMPLATES.MONITOR_TYPE, dbo.A_MONITOR_TEMPLATES.HIGHEST_THRESHOLD, 
                      dbo.A_MONITOR_TEMPLATES.HIGH_THRESHOLD, dbo.A_MONITOR_TEMPLATES.TARGET, dbo.A_MONITOR_TEMPLATES.LOW_THRESHOLD, 
                      dbo.A_MONITOR_TEMPLATES.LOWEST_THRESHOLD, dbo.A_MONITOR_TEMPLATES.SHOULD_BE, PURCHASE_ITEM.PURCHASE_HIST_ID, 
                      dbo.A_TASKS.ACTUAL_STOP_DATE, dbo.A_TASKS.DESCRIPTION AS TASK_DESCRIPTION, dbo.A_MONITOR_RESULTS.COMMENT, 
                      dbo.A_MONITOR_TEMPLATES.YES_NO_ANSWER
FROM         dbo.A_MONITOR_RESULTS INNER JOIN
                      dbo.A_MONITOR_TEMPLATES ON dbo.A_MONITOR_RESULTS.MONITOR_TEMPLATE_ID = dbo.A_MONITOR_TEMPLATES.ID INNER JOIN
                      dbo.A_TASKS ON dbo.A_MONITOR_TEMPLATES.TASK_ID = dbo.A_TASKS.ID INNER JOIN
                      dbo.A_FILLS FILLS INNER JOIN
                      dbo.A_ORDER_ITEMS PURCHASE_ITEM ON FILLS.PURCH_ITEM_ID = PURCHASE_ITEM.ID INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON PURCHASE_ITEM.ID = dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID ON 
                      dbo.A_TASKS.PARENT_ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON FILLS.FILL_OBJ_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_WIP_REPORT_VIEW]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WIP_REPORT_VIEW]
AS
SELECT DISTINCT 
                      toi.PURCHASE_ITEM_ID, p.CUST_PURCH_NUM AS PO_NUMBER, i.DUE_DATE, t.ACTUAL_START_DATE, t.ACTUAL_STOP_DATE, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.COMPANY_PART_NUMBER, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL, c.NAME AS CUSTOMER_NAME, 
                      A_V_COMPANIES_APPROVED_DATA_QUICK_1.NAME AS SUPPLIER_NAME, dbo.A_V_PROCEDURES_DATA_QUICK.NAME AS PROC_NAME, 
                      dbo.A_FILLS.PRICE, dbo.A_ACCOUNT_INVOICE_ITEMS.AMOUNT AS INVOICE_AMOUNT, dbo.A_ACCOUNT_INVOICES.STATUS AS INVOICE_STATUS, 
                      dbo.A_ACCOUNT_INVOICES.INVOICE_DATE, dbo.A_ACCOUNT_INVOICES.DUE_DATE AS INVOICE_DUE_DATE, 
                      dbo.A_ACCOUNT_INVOICES.DATE_SENT_TO_CUSTOMER, dbo.A_ACCOUNT_INVOICE_ITEMS.FAILED_MONITOR, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID, t.STATUS, i.MT_NUM, p.ID AS PURCHASE_ID, c.ID AS CUST_ID, t.ID AS TASK_ID, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.REFERENCE_PO AS BLANKET_PO, i.QTY, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.SUPPLIER_CO AS SUPPLIER_ID, p.HISTORY_REF_ID AS PURCH_HIST_ID, 
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.PART_DESC, dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE, 
                      dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE, dbo.A_TASK_COMPLETION_STATS.TOTAL_TIME, 
                      dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS, dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS, 
                      dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS, dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE, 
                      dbo.A_TASK_COMPLETION_STATS.CUR_STEP_TEXT
FROM         dbo.A_TASK_COMPLETION_STATS INNER JOIN
                      dbo.A_FILLS INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_ORDER_ITEMS i ON toi.PURCHASE_ITEM_ID = i.ID INNER JOIN
                      dbo.A_TASKS t ON toi.TASK_ID = t.ID INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA p ON toi.PURCHASE_HIST_ID = p.HISTORY_REF_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK c ON p.CUSTOMER_CO = c.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON i.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK ON t.PROCEDURE_ID = dbo.A_V_PROCEDURES_DATA_QUICK.ID INNER JOIN
                      dbo.A_TASK_OBJECT_LINK ON t.ID = dbo.A_TASK_OBJECT_LINK.TASK_ID INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID ON 
                      dbo.A_FILLS.FILL_OBJ_ID = dbo.A_TASK_OBJECT_LINK.OBJECT_ID AND dbo.A_FILLS.ID = toi.FILL_ITEM_ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_ACCOUNT_INVOICES INNER JOIN
                      dbo.A_ACCOUNT_INVOICE_ITEMS ON dbo.A_ACCOUNT_INVOICES.ID = dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID ON 
                      toi.FILL_ITEM_ID = dbo.A_ACCOUNT_INVOICE_ITEMS.FILL_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK A_V_COMPANIES_APPROVED_DATA_QUICK_1 INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK ON 
                      A_V_COMPANIES_APPROVED_DATA_QUICK_1.ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.SUPPLIER_CO ON 
                      i.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK.ID
WHERE     (toi.PURCHASE_ITEM_ID IS NOT NULL) AND (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED'))
GO

/****** Object:  View [dbo].[A_CUSTOMER_PART_FILE_LINK_2]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_CUSTOMER_PART_FILE_LINK_2]
AS
SELECT     dbo.A_V_WIP_REPORT_VIEW.TASK_ID, dbo.A_V_WIP_REPORT_VIEW.CUST_ID, dbo.A_DOCUMENTS.NAME, 
                      dbo.A_V_WIP_REPORT_VIEW.CUSTOMER_NAME, dbo.A_V_WIP_REPORT_VIEW.SERIAL, dbo.A_V_WIP_REPORT_VIEW.COMPANY_PART_NUMBER, 
                      dbo.A_DOCUMENTS.SERVER_PATH
FROM         dbo.A_ACTUAL_PARTS_RELATED_FILES INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_ACTUAL_PARTS_RELATED_FILES.FILE_ID = dbo.A_DOCUMENTS.ID INNER JOIN
                      dbo.A_V_WIP_REPORT_VIEW INNER JOIN
                      dbo.A_V_ACTUAL_PARTS_APPROVED_DATA ON dbo.A_V_WIP_REPORT_VIEW.SERIAL = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.SERIAL ON 
                      dbo.A_ACTUAL_PARTS_RELATED_FILES.ACTUAL_PART_ID = dbo.A_V_ACTUAL_PARTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[Portal_ActualPartsViewHistory]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ActualPartsViewHistory]
AS
SELECT        
t.ID,
t.[DESCRIPTION],
t.CUR_PLANNED_COUNTER_START AS CurPlannerCounterStart,
t.CUR_PLANNED_START_DATE AS CurPlannerStartDate,
t.CUR_PLANNED_STOP_DATE AS CurPlannedStopDate, 
t.ACTUAL_START_DATE AS ActualStartDate,
t.ACTUAL_STOP_DATE AS ActualStopDate, 
l.ACTUAL_PART_ID AS ActualPartId,
t.ORIG_REQUESTOR_NAME AS OriginalRequestor,
t.REQUESTOR AS Requester, 
t.LATEST_REQUESTEE_NAME AS LatestRequestee,
t.GROUP_REQUESTEE_ID AS GroupRequesteeId, 
t.REQUESTEE_ID AS RequesteeId, t.COMPANY_NAME AS CompanyName, 
t.CO_ID AS CoId,
t.COLOR_CODE AS ColourCode, 
t.LAST_REQUEST_DATE AS RequestDate,
t.STATUS AS TaskStetTitle, 
t.PRIORITY_NAME AS TaskPriority, 
i.TASK_TYPE AS TaskType,
i.DNR_STATUS AS DnrStatus, 
t.PROCEDURE_ID AS ProcedureId
FROM            dbo.A_V_TASK_SEARCH AS t INNER JOIN
                         dbo.A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED AS l ON t.ID = l.TASK_ID LEFT OUTER JOIN
                         dbo.A_DNR_TASK_INFO AS i ON t.ID = i.TASK_ID
GO

/****** Object:  View [dbo].[Portal_ViewHistryActualPartDetails]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ViewHistryActualPartDetails]
AS
SELECT DISTINCT 
                         ID, SORT_ID, DESCRIPTION, STATUS, REQUESTOR, CHILD_ORDER, CREATED_BY, CREATE_DATE, SYSTEM_TASK, PROCEDURE_ID, REQUESTEE_ID, GROUP_REQUESTEE_ID, ORIG_PLANNED_START_DATE, 
                         ORIG_PLANNED_STOP_DATE, CUR_PLANNED_START_DATE, CUR_PLANNED_STOP_DATE, ACTUAL_START_DATE, ACTUAL_STOP_DATE, CUR_PLANNED_COUNTER_START, LATEST_REQUESTEE_NAME, 
                         HAS_DISCUSSION, HAS_SURVEY, HAS_CHILD, HAS_REF_PROC, HAS_FILE, ORIG_REQUESTOR_ID, HAS_REF_OBJ, HAS_MONITOR, ORIG_REQUESTOR_NAME, COLOR_CODE, LAST_REQUEST_DATE, 
                         PARENT_ID, PARENT_LIST, isParent, PRIORITY, PRIORITY_NAME, COMPANY_NAME, CHILD_STATUS, PROCEDURE_STEP_ID, LAST_COMMENT_WRITER, LAST_COMMENT_DRCM, LAST_COMMENT, IS_QUOTE, 
                         IS_QUOTE_ACCEPT, IS_FILL, CO_ID, FILL_ID, PURCHASE_ITEM_ID, PURCHASE_HIST_ID, PURCHASE_ITEM_ROLE, ASSIGNEE_NAME, ASSIGNEE_ID, ASSIGNEE_STATUS
FROM            dbo.A_V_TASK_SEARCH
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_NORMAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALLS_NORMAL_HOURS]
AS
SELECT     ID, WEEKLY_ID, D, MO, YR, HOUR_TYPE, HOURS, DRCM, MODBY
FROM         dbo.A_SERVICE_CALL_WORK_TIME
WHERE     (HOUR_TYPE = 'NORMAL')
GO

/****** Object:  View [dbo].[A_V_PEOPLE_SELF_BOSS_IF_NULL]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_SELF_BOSS_IF_NULL]
AS
SELECT     p.ID, ISNULL(ph.BOSS, p.ID) AS BOSS, ph.FULL_NAME
FROM         dbo.A_PEOPLE p INNER JOIN
                      dbo.A_PEOPLE_HISTORY ph ON p.HISTORY_REF_ID = ph.ID
GO

/****** Object:  View [dbo].[A_APPROVED_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_APPROVED_COMPANIES]
AS
SELECT     dbo.A_COMPANIES.ID, dbo.A_COMPANIES.HISTORY_REF_ID, dbo.A_COMPANIES_HISTORY.NAME, dbo.A_COMPANIES_HISTORY.CO_TYPE, 
                      dbo.A_COMPANIES_HISTORY.PARENT, dbo.A_COMPANIES_HISTORY.PHONE, dbo.A_COMPANIES_HISTORY.LOCATION, 
                      dbo.A_COMPANIES_HISTORY.LOCATION_NAME, dbo.A_COMPANIES_HISTORY.DRCM, dbo.A_COMPANIES_HISTORY.MODBY, 
                      dbo.A_COMPANIES_HISTORY.OBJECT_ID, dbo.A_OBJECTS.LOCKED_BY, dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, 
                      dbo.A_OBJECTS.CREATE_DATE, dbo.A_OBJECTS.REV_INFO, dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, 
                      dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, dbo.A_OBJECTS.CREATING_CO_NAME, dbo.A_OBJECTS.APPROVAL_ACTIVITY, 
                      dbo.A_COMPANIES.STATUS AS GENERAL_STATUS, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS PARENT_NAME, 
                      A_V_COMPANIES_APPROVED_DATA_1.NAME AS ROOT_CO_NAME, dbo.A_COMPANIES_HISTORY.ROOT_CO_ID
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_COMPANIES_HISTORY ON dbo.A_OBJECTS.ID = dbo.A_COMPANIES_HISTORY.OBJECT_ID INNER JOIN
                      dbo.A_COMPANIES ON dbo.A_COMPANIES_HISTORY.ID = dbo.A_COMPANIES.HISTORY_REF_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      dbo.A_COMPANIES_HISTORY.ROOT_CO_ID = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_COMPANIES_HISTORY.PARENT = dbo.A_V_COMPANIES_APPROVED_DATA.ID
WHERE     (dbo.A_COMPANIES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_APPROVED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_APPROVED_ROLES]
AS
SELECT     dbo.A_ROLES.ID, dbo.A_ROLES_HISTORY.NAME, dbo.A_ROLES_HISTORY.SOURCE, dbo.A_ROLES_HISTORY.HIDDEN, 
                      dbo.A_ROLES_HISTORY.DRCM, dbo.A_ROLES_HISTORY.MODBY, dbo.A_ROLES_HISTORY.OBJECT_ID, dbo.A_OBJECTS.LOCKED_BY, 
                      dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO,
                       dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, 
                      dbo.A_OBJECTS.CREATING_CO_NAME, dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_ROLES.HISTORY_REF_ID, dbo.A_ROLES_HISTORY.IS_ADMIN, 
                      dbo.A_ROLES_HISTORY.SECURITY_LEVEL, dbo.A_ROLES.STATUS AS DEL_STATUS
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_ROLES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
WHERE     (dbo.A_ROLES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WORK_TYPE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALL_WORK_TYPE_DATA]
AS
SELECT     dbo.A_SERVICE_CALLS_WORK_TYPES.ID, dbo.A_APPROVED_COMPANIES.NAME AS SUPPLIER_NAME, '$' + CONVERT(varchar(50), 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.HOUR_RATE) AS NORMAL_RATE, '$' + CONVERT(varchar(50), 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.OT_RATE) AS OVER_RATE, A_APPROVED_COMPANIES_1.NAME AS CUSTOMER_NAME, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.WORK_TYPE_NAME, dbo.A_APPROVED_ROLES.NAME AS APPROVER_ROLE_NAME, 
                      A_APPROVED_ROLES_1.NAME AS PAYER_ROLE_NAME, dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.CUSTOMER_ID, dbo.A_SERVICE_CALLS_WORK_TYPES.APPROVER_ROLE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.PAYER_ROLE, dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.HOUR_RATE, dbo.A_SERVICE_CALLS_WORK_TYPES.OT_RATE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.SKIP_BOSS, dbo.A_SERVICE_CALLS_WORK_TYPES.HIDE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.STATUS
FROM         dbo.A_APPROVED_COMPANIES RIGHT OUTER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES ON 
                      dbo.A_APPROVED_COMPANIES.ID = dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID LEFT OUTER JOIN
                      dbo.A_APPROVED_COMPANIES A_APPROVED_COMPANIES_1 ON 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.CUSTOMER_ID = A_APPROVED_COMPANIES_1.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_SERVICE_CALLS_WORK_TYPES.APPROVER_ROLE = dbo.A_APPROVED_ROLES.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_ROLES A_APPROVED_ROLES_1 ON dbo.A_SERVICE_CALLS_WORK_TYPES.PAYER_ROLE = A_APPROVED_ROLES_1.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_WITH_NORMAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALLS_WITH_NORMAL_HOURS]
AS
SELECT     worker.BOSS, w_type.SUPPLIER_NAME, w_type.CUSTOMER_NAME, w_type.SUPPLIER_ID, w_type.CUSTOMER_ID, w_type.APPROVER_ROLE, 
                      w_type.TAX_RATE / 100 AS ACTUAL_TAX_RATE, w_type.PAYER_ROLE, w_type.APPROVER_ROLE_NAME AS APPROVER, 
                      w_type.PAYER_ROLE_NAME AS PAYER, SC.WORKER_ID, SC.ID, SC.MACHINE_NAME, worker.FULL_NAME, 
                      w_type.WORK_TYPE_NAME AS WORK_TYPE, SUM(n_hours.HOURS) AS NORMAL_HOURS, SC.STATUS, SC.START_DAY, SC.START_MONTH, 
                      SC.START_YEAR, SC.REASON, SC.CHECK_NUM, CONVERT(dateTime, STR(SC.START_MONTH) + '/' + STR(SC.START_DAY) 
                      + '/' + STR(SC.START_YEAR)) AS START_DATE, SC.HOUR_RATE_FIXED, SC.OT_RATE_FIXED, SC.ORDER_NUMBER, 
                      SC.WORK_TYPE AS WORK_TYPE_ID, w_type.TAX_RATE, SC.INVOICE_DATE, SC.EXPENSES_AMOUNT, SC.REASON_TYPE, SC.COMMENTS
FROM         dbo.A_V_SERVICE_CALL_WORK_TYPE_DATA w_type RIGHT OUTER JOIN
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS SC LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_SELF_BOSS_IF_NULL worker ON SC.WORKER_ID = worker.ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALLS_NORMAL_HOURS n_hours ON SC.ID = n_hours.WEEKLY_ID ON w_type.ID = SC.WORK_TYPE
GROUP BY worker.BOSS, w_type.SUPPLIER_NAME, w_type.CUSTOMER_NAME, w_type.SUPPLIER_ID, w_type.CUSTOMER_ID, w_type.APPROVER_ROLE, 
                      w_type.PAYER_ROLE, w_type.APPROVER_ROLE_NAME, SC.STATUS, SC.WORKER_ID, SC.ID, SC.MACHINE_NAME, worker.FULL_NAME, 
                      w_type.WORK_TYPE_NAME, w_type.PAYER_ROLE_NAME, SC.STATUS, SC.START_DAY, SC.START_MONTH, SC.START_YEAR, SC.REASON, 
                      SC.CHECK_NUM, SC.HOUR_RATE_FIXED, SC.OT_RATE_FIXED, SC.ORDER_NUMBER, SC.WORK_TYPE, w_type.TAX_RATE, SC.INVOICE_DATE, 
                      SC.EXPENSES_AMOUNT, SC.REASON_TYPE, SC.COMMENTS
GO

/****** Object:  View [dbo].[A_O_WORKFLOWS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_WORKFLOWS]
AS
SELECT     w.NAME, w.OBJECT_ID, w.ID, w.HIDE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.STATUS, 
                      o.CREATING_CO, o.REV, o.WFS_ID, w.STAMP_NAME, w.STAMP_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_WORKFLOWS w ON o.ID = w.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_WORKFLOWS_FOR_ACTIVITIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WORKFLOWS_FOR_ACTIVITIES]
AS
SELECT     dbo.A_WF_ACT_LINK.WF_ID, dbo.A_WF_ACT_LINK.ACT_ID, dbo.A_WF_ACTIVITIES.ACTIVITY, dbo.A_O_WORKFLOWS.CREATING_CO, 
                      dbo.A_O_WORKFLOWS.NAME AS WF_NAME, dbo.A_O_WORKFLOWS.HIDE
FROM         dbo.A_WF_ACT_LINK INNER JOIN
                      dbo.A_WF_ACTIVITIES ON dbo.A_WF_ACT_LINK.ACT_ID = dbo.A_WF_ACTIVITIES.ID INNER JOIN
                      dbo.A_O_WORKFLOWS ON dbo.A_WF_ACT_LINK.WF_ID = dbo.A_O_WORKFLOWS.ID
WHERE     (ISNULL(dbo.A_O_WORKFLOWS.HIDE, 0) = 0)
GO

/****** Object:  View [dbo].[Portal_ApprovalWorkflowsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ApprovalWorkflowsView]
	AS 
SELECT NAME as Name,
Id As Id, STAMP_ID as Stamp_Id, STAMP_NAME as Stamp_Name, CREATING_CO as Creating_Co, OBJECT_ID as Object_Id, HIDE as Hide
	FROM A_O_WORKFLOWS
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_SHIPPING_FROM]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ORDER_ITEM_SHIPPING_FROM]
AS
SELECT     i.ID, i.PROC_SYS_ID, i.DEST, i.PARENT, i.PRODUCT_ID, p.NAME AS PROD_NAME
FROM         dbo.A_ORDER_ITEMS i INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA p ON i.PRODUCT_ID = p.ID
WHERE     (i.DEST = 'from') AND (i.PROC_SYS_ID = 'SYS_SHIPPING')
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_TRAVEL_FROM]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ORDER_ITEM_TRAVEL_FROM]
AS
SELECT     i.ID, i.PROC_SYS_ID, i.DEST, i.PARENT, i.PRODUCT_ID, p.NAME AS PROD_NAME
FROM         dbo.A_ORDER_ITEMS i INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA p ON i.PRODUCT_ID = p.ID
WHERE     (i.DEST = 'from') AND (i.PROC_SYS_ID = 'SYS_TRAVEL')
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_APPROVED_DATA]
AS
SELECT     ppl.ID, ppl.HISTORY_REF_ID, h.PRODUCT, h.CUSTOMER, h.UNIT, h.MIN_QUANTITY, h.UNIT_PRICE, h.EST_UNIT_PRICE, h.EST_LABOR_PRICE, 
                      h.EST_PARTS_PROV_STAY, h.EST_PARTS_PROV_TAKE_BACK, h.EST_PARTS_CONSUMED, h.INVOICE_FROM, h.PRODUCTION_TIME, 
                      h.PRODUCTION_TIME_UNIT, h.CAPACITY, h.CAPACITY_UNIT, h.DRCM, h.MODBY, h.OBJECT_ID, h.PRODUCT_NAME, h.CUSTOMER_NAME, 
                      prod.SUPPLIER_ID, ppl.STATUS
FROM         dbo.A_PROD_PRICE_LIST ppl INNER JOIN
                      dbo.A_PROD_PRICE_LIST_HISTORY h ON ppl.HISTORY_REF_ID = h.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA prod ON h.PRODUCT = prod.ID
WHERE     (ppl.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_PARTS_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PARTS_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_PARTS.ID, dbo.A_PARTS.PARTS_HISTORY_ID AS HISTORY_REF_ID, dbo.A_PARTS_HISTORY.OBJECT_ID, dbo.A_PARTS_HISTORY.UNIT, 
                      dbo.A_PARTS_HISTORY.NAME, dbo.A_PARTS_HISTORY.PART_TYPE, dbo.A_PARTS_HISTORY.SPARE, dbo.A_PARTS_HISTORY.CONSUMABLE, 
                      dbo.A_PARTS_HISTORY.TRACK_FROM_START, dbo.A_PARTS_HISTORY.COMPANY, dbo.A_PARTS_HISTORY.COMPANY_NAME, 
                      dbo.A_PARTS_HISTORY.PART_TYPE_NAME, dbo.A_PARTS_HISTORY.UNIT_SHIPPING_WEIGHT, dbo.A_PARTS_HISTORY.COMPANY_PART_NUMBER, 
                      dbo.A_PARTS_HISTORY.SUPPLIER_SEE_INSTALL_BASE, dbo.A_PARTS_HISTORY.SUPPLIER_SEE_AVAILABILITY, 
                      dbo.A_PARTS_HISTORY.CUSTOMER_SEE_AVAILABILITY, dbo.A_PARTS_HISTORY.WEIGHT_TYPE, dbo.A_PARTS_HISTORY.CREATE_PROD, 
                      dbo.A_PARTS_HISTORY.SUPPLIER_CO, dbo.A_PARTS_HISTORY.PRODUCT_TYPE, dbo.A_PARTS_HISTORY.PROC_VERB, 
                      dbo.A_PARTS_HISTORY.PRICE
FROM         dbo.A_PARTS INNER JOIN
                      dbo.A_PARTS_HISTORY ON dbo.A_PARTS.PARTS_HISTORY_ID = dbo.A_PARTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEMS_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDER_ITEMS_ALL_DATA]
AS
SELECT     PROD.NAME AS PRODUCT_NAME, PROD.SUPPLIER_ID, SUPPLIER.NAME AS SUPPLIER_NAME, PROD.PROCEDURE_ID, 
                      [PROCEDURE].NAME AS PROC_NAME, dbo.A_OBJECTS.OBJ_DESC AS OBJ_PROD_APPLIES_TO, PROD.HISTORY_REF_ID, 
                      [PROCEDURE].VERB AS PROC_TYPE, dbo.A_ORDERS_HISTORY.OBJECT_ID AS ORDER_OBJ_ID, 
                      dbo.A_FN_ORDER_ITEM_GET_PARENT_LIST(Items.ID) AS PARENT_LIST, [PROCEDURE].SYSTEM_ID, dbo.A_OBJECTS.OBJ_TABLE, 
                      dbo.A_UNIT_TYPES.NAME AS WT_UNIT_NAME, Items.ID, Items.PARENT, Items.PARENT_QTY, Items.ORDER_ID, Items.PRODUCT_ID, 
                      Items.PROD_PRICE_LIST, Items.QUOTE_ID, Items.ADD_COST_ID, Items.TOTAL_QTY, Items.QTY, Items.UNIT_PRICE, Items.UNIT_ESTIMATE, 
                      Items.COMMENTS, CONVERT(money, Items.TOTAL_PRICE) AS TOTAL_PRICE, Items.DEST, Items.FROM_LOC, Items.TO_LOC, 
                      Items.SPECIAL_DISCOUNT, Items.SPECIAL_DISC_REASON, Items.EXPEDITE_PRODUCTION, Items.EXPEDITE_REASON, Items.FLAT_RATE, 
                      Items.EX_DESC, Items.EST_WEIGHT, Items.EST_WEIGHT_UNIT, Items.PPL_HIST_ID, Items.RECURRING, Items.RECUR_PERIOD, 
                      Items.RECUR_COUNT, Items.RECUR_START_DATE, Items.RECUR_STOP_DATE, Items.RECUR_ACCOUNT, Items.RECUR_AUTO_FILL, 
                      Items.SOURCE_ID, Items.PURCHASE_HIST_ID, Items.ACCOUNT_ID, ACCOUNT.NAME AS ACCT_NAME, [PROCEDURE].SYSTEM_ID AS PROC_SYS_ID, 
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.MIN_QUANTITY, dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.CAPACITY, 
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.CAPACITY_UNIT, dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.PRODUCTION_TIME, 
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.PRODUCTION_TIME_UNIT, dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.UNIT AS PROD_UNIT, 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.PRICE_LIST_TYPE, PROD.NAME + N'(' + ISNULL([PROCEDURE].OBJECT_ID, N'') 
                      + N')' AS PROD_SHOW_NAME, Items.BILL_TYPE, dbo.A_OBJECTS.ROOT AS OBJ_PROD_APPLIES_TO_ID, 
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK.UNIT, dbo.A_V_PARTS_APPROVED_DATA_QUICK.NAME, 
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK.COMPANY_PART_NUMBER, Items.DUE_DATE, Items.ORIG_DUE_DATE, Items.ACT_DUE_DATE, 
                      Items.PROD_TIME, Items.PROD_TIME_UNIT, Items.CUST_LINE_ITEM,
					  PROD.CycleTime
FROM         dbo.A_OBJECTS LEFT OUTER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK ON dbo.A_OBJECTS.ID = dbo.A_V_PARTS_APPROVED_DATA_QUICK.ID RIGHT OUTER JOIN
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS RIGHT OUTER JOIN
                      dbo.A_ORDER_ITEMS Items INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA PROD ON SUPPLIER.ID = PROD.SUPPLIER_ID ON Items.PRODUCT_ID = PROD.ID INNER JOIN
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA ON Items.PROD_PRICE_LIST = dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.ID ON 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.ID = Items.ADD_COST_ID ON dbo.A_OBJECTS.ID = PROD.APP_OBJECT LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ACCOUNT ON Items.ACCOUNT_ID = ACCOUNT.ID LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [PROCEDURE] ON PROD.PROCEDURE_ID = [PROCEDURE].ID LEFT OUTER JOIN
                      dbo.A_QUOTES_HISTORY ON Items.QUOTE_ID = dbo.A_QUOTES_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_ORDERS_HISTORY ON Items.ORDER_ID = dbo.A_ORDERS_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_UNIT_TYPES ON Items.EST_WEIGHT_UNIT = dbo.A_UNIT_TYPES.ID
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_SHIPPING]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ORDER_ITEM_SHIPPING]
AS
SELECT     i.ID, i.PROC_SYS_ID, i.DEST, i.PARENT, i.PRODUCT_ID, p.NAME AS PROD_NAME
FROM         dbo.A_ORDER_ITEMS i INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA p ON i.PRODUCT_ID = p.ID
WHERE     (i.DEST = 'to') AND (i.PROC_SYS_ID = 'SYS_SHIPPING')
GO

/****** Object:  View [dbo].[A_V_APPROVED_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_APPROVED_OBJECTS]
AS
SELECT     dbo.A_APPROVED_OBJECTS.ID, dbo.A_APPROVED_OBJECTS.OBJ_REF_ID, dbo.A_OBJECTS.OBJ_TABLE, dbo.A_OBJECTS.OBJ_ID, 
                      dbo.A_OBJECTS.OBJ_DESC, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.REV, 
                      dbo.A_OBJECTS.CREATING_CO_NAME
FROM         dbo.A_APPROVED_OBJECTS INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_APPROVED_OBJECTS.OBJ_REF_ID = dbo.A_OBJECTS.ID
WHERE     (dbo.A_OBJECTS.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEMS_DATA_WITH_SHIPPING]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDER_ITEMS_DATA_WITH_SHIPPING]
AS
SELECT     TRAVEL_FROM.ID AS TRAV_FROM_ID, TRAVEL_FROM.PROD_NAME AS TRAV_FROM_NAME, SHIP_TO.ID AS SHIP_TO_ID, SHIP_TO.PROD_NAME AS SHIP_TO_NAME, 
                      TRAVEL_TO.ID AS TRAV_TO_ID, TRAVEL_TO.PROD_NAME AS TRAV_TO_NAME, SHIP_FROM.ID AS SHIP_FROM_ID, SHIP_FROM.PROD_NAME AS SHIP_FROM_NAME, 
                      dbo.A_FN_ORDER_ITEM_GET_PRECEDENT_LIST(O.ID) AS PREC, ISNULL(oO.LOCKED_BY, '') + ISNULL(qO.LOCKED_BY, '') AS LOCKED_BY, ISNULL(oO.STATUS, '') 
                      + ISNULL(qO.STATUS, '') AS STATUS, TO_LOC.COMPLETE_NAME AS TO_LOC_NAME, FROM_LOC.COMPLETE_NAME AS FROM_LOC_NAME, O.PRODUCT_NAME, 
                      O.SUPPLIER_ID, O.SUPPLIER_NAME, O.PROCEDURE_ID, O.PROC_NAME, O.OBJ_PROD_APPLIES_TO, O.HISTORY_REF_ID, O.PROC_TYPE, O.ORDER_OBJ_ID, 
                      O.PARENT_LIST, O.SYSTEM_ID, O.OBJ_TABLE, O.WT_UNIT_NAME, O.ID, O.PARENT, O.PARENT_QTY, O.ORDER_ID, O.PRODUCT_ID, O.PROD_PRICE_LIST, 
                      O.QUOTE_ID, O.ADD_COST_ID, O.TOTAL_QTY, O.QTY, O.UNIT_PRICE, O.UNIT_ESTIMATE, O.COMMENTS, O.TOTAL_PRICE, O.DEST, O.FROM_LOC, O.TO_LOC, 
                      O.SPECIAL_DISCOUNT, O.SPECIAL_DISC_REASON, O.EXPEDITE_PRODUCTION, O.EXPEDITE_REASON, O.FLAT_RATE, O.EX_DESC, O.EST_WEIGHT, 
                      O.EST_WEIGHT_UNIT, O.PPL_HIST_ID, O.RECURRING, O.RECUR_PERIOD, O.RECUR_COUNT, O.RECUR_START_DATE, O.RECUR_STOP_DATE, O.RECUR_ACCOUNT, 
                      O.RECUR_AUTO_FILL, O.SOURCE_ID, O.PURCHASE_HIST_ID, O.ACCOUNT_ID, O.ACCT_NAME, O.PROC_SYS_ID, O.MIN_QUANTITY, O.CAPACITY, O.CAPACITY_UNIT, 
                      O.PRODUCTION_TIME, O.PRODUCTION_TIME_UNIT, O.PROD_UNIT, O.PRICE_LIST_TYPE, O.PROD_SHOW_NAME, O.BILL_TYPE, O.OBJ_PROD_APPLIES_TO_ID, 
                      O.UNIT, O.NAME, O.COMPANY_PART_NUMBER, O.DUE_DATE, O.ORIG_DUE_DATE, O.ACT_DUE_DATE, O.PROD_TIME, O.PROD_TIME_UNIT, O.CUST_LINE_ITEM, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS APP_OBJ_DESC,
                          (SELECT     NAME
                            FROM          dbo.A_V_COMPANIES_APPROVED_DATA_QUICK
                            WHERE      (ID = dbo.A_V_COMPANIES_APPROVED_DATA.ROOT_CO)) AS SUPPLIER_ROOT_CO_NAME
FROM         dbo.A_V_ORDER_ITEMS_ALL_DATA AS O INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON O.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON O.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_V_PRODUCTS_APPROVED_DATA.APP_OBJECT = dbo.A_V_APPROVED_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA AS FROM_LOC ON O.FROM_LOC = FROM_LOC.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA AS TO_LOC ON O.TO_LOC = TO_LOC.ID LEFT OUTER JOIN
                      dbo.A_QUOTES_HISTORY INNER JOIN
                      dbo.A_OBJECTS AS qO ON dbo.A_QUOTES_HISTORY.OBJECT_ID = qO.ID ON O.QUOTE_ID = dbo.A_QUOTES_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_OBJECTS AS oO INNER JOIN
                      dbo.A_ORDERS_HISTORY ON oO.ID = dbo.A_ORDERS_HISTORY.OBJECT_ID ON O.ORDER_ID = dbo.A_ORDERS_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_ORDER_ITEM_TRAVEL_FROM AS TRAVEL_FROM ON O.ID = TRAVEL_FROM.PARENT LEFT OUTER JOIN
                      dbo.A_V_ORDER_ITEM_SHIPPING AS SHIP_TO ON O.ID = SHIP_TO.PARENT LEFT OUTER JOIN
                      dbo.A_V_ORDER_ITEM_SHIPPING_FROM AS SHIP_FROM ON O.ID = SHIP_FROM.PARENT LEFT OUTER JOIN
                      dbo.A_V_ORDER_ITEM_TRAVEL_FROM AS TRAVEL_TO ON O.ID = TRAVEL_TO.PARENT
GO

/****** Object:  View [dbo].[A_V_ROLE_DATA_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ROLE_DATA_BY_APPROVED_ID]
AS
SELECT     dbo.A_ROLES.ID, dbo.A_ROLES.HISTORY_REF_ID, dbo.A_ROLES_HISTORY.NAME, dbo.A_ROLES_HISTORY.OBJECT_ID, 
                      dbo.A_ROLES_HISTORY.IS_ADMIN, dbo.A_ROLES_HISTORY.SECURITY_LEVEL, dbo.A_ROLES_HISTORY.HIDDEN
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_ADMIN_ROLE_JOBS_WITH_CO_AND_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ADMIN_ROLE_JOBS_WITH_CO_AND_ROLE]
AS
SELECT     dbo.A_V_COMPANIES_APPROVED_DATA.ID AS CO_ID, dbo.A_V_ROLE_DATA_BY_APPROVED_ID.NAME AS ROLE_NAME, 
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID AS ROLE_ID, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CO_NAME, 
                      dbo.A_ADMIN_ROLE_JOBS.JOB
FROM         dbo.A_V_ROLE_DATA_BY_APPROVED_ID INNER JOIN
                      dbo.A_ADMIN_ROLE_JOBS ON dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID = dbo.A_ADMIN_ROLE_JOBS.ROLE_ID RIGHT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_ADMIN_ROLE_JOBS.CO_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROD_REQUEST_FORMS_WITH_ALL_DATA]
AS
SELECT     PROD.NAME AS PRODUCT_NAME, PROD.ID AS PRODUCT_ID, REQ_FORM.ID, REQ_FORM.FILE_ID, REQ_FORM.FILE_KEYWORDS, 
                      REQ_FORM.CUST_PERSON_ID, dbo.A_DOCUMENTS.NAME AS FILE_NAME, A_V_COMPANIES_APPROVED_DATA_1.NAME AS CUSTOMER_NAME, 
                      REQ_FORM.CUSTOMER_CO, REQ_FORM.DATE_UPLOADED, RES_PROD_NAME.NAME AS RES_PROD_NAME, 
                      dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS CUST_PERSON_NAME, CUST_MGR_ROLE.NAME AS CUST_MGR_ROLE_NAME, 
                      REQ_FORM.PRODUCT_ID AS PROD_ID, REQ_FORM.RESULT_PRODUCT, REQ_FORM.DRCM, REQ_FORM.MODBY, REQ_FORM.PROGRESS, 
                      PROD.CUST_MGR_ROLE, REQ_FORM.UPLOADER, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS UPLOADER_NAME, PROD.SUPPLIER_ID, 
                      SUPPLIER.NAME AS SUPPLIER_NAME
FROM         dbo.A_V_ROLE_DATA_BY_APPROVED_ID CUST_MGR_ROLE INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA PROD ON CUST_MGR_ROLE.ID = PROD.CUST_MGR_ROLE LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON PROD.SUPPLIER_ID = SUPPLIER.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN RIGHT OUTER JOIN
                      dbo.A_PROD_REQ_FORMS REQ_FORM INNER JOIN
                      dbo.A_DOCUMENTS ON REQ_FORM.FILE_ID = dbo.A_DOCUMENTS.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON REQ_FORM.CUST_PERSON_ID = dbo.A_V_PEOPLE_APPROVED_DATA.ID ON 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID = REQ_FORM.UPLOADER LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      REQ_FORM.CUSTOMER_CO = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA RES_PROD_NAME ON REQ_FORM.RESULT_PRODUCT = RES_PROD_NAME.ID ON 
                      PROD.ID = REQ_FORM.PRODUCT_ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE]
AS
SELECT     L.QUOTE_ID, L.ORDER_ID, L.STATUS, L.CUSTOMER AS CUST_ID, L.SUPPLIER AS SUPPLIER_ID, J.ROLE_ID, R.NAME AS ROLE_NAME, 
                      C.NAME AS CUST_NAME, S.NAME AS SUPPLIER_NAME, L.ID AS QOL_ID, dbo.A_ORDERS.HISTORY_REF_ID AS ORDER_HIST_ID
FROM         dbo.A_QUOTE_ORDER_LINK L INNER JOIN
                      dbo.A_ADMIN_ROLE_JOBS J ON L.SUPPLIER = J.CO_ID INNER JOIN
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID R ON J.ROLE_ID = R.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA C ON L.CUSTOMER = C.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA S ON L.SUPPLIER = S.ID INNER JOIN
                      dbo.A_ORDERS ON L.ORDER_ID = dbo.A_ORDERS.ID
GO

/****** Object:  View [dbo].[A_V_APPROVED_ROLES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_APPROVED_ROLES_DATA]
AS
SELECT     dbo.A_ROLES.ID, dbo.A_ROLES.HISTORY_REF_ID, dbo.A_ROLES_HISTORY.NAME, dbo.A_ROLES_HISTORY.SOURCE, 
                      dbo.A_ROLES_HISTORY.HIDDEN, dbo.A_ROLES_HISTORY.DRCM, dbo.A_ROLES_HISTORY.MODBY, dbo.A_ROLES_HISTORY.OBJECT_ID, 
                      dbo.A_ROLES_HISTORY.SECURITY_LEVEL
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_O_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PRODUCTS]
AS
SELECT     PH.ID, PH.NAME, PH.SUPPLIER_ID, PH.COMMENTS, PH.PROCEDURE_ID, PH.APP_OBJECT, PH.SHIP_OR_LABOR, PH.CUSTOMIZABLE, 
                      PH.REQ_FORM, PH.MGR_TEAM, PH.SALES_TAX, PH.OBJECT_ID, PH.PARENT_ID, O.LOCKED_BY, O.UNLOCKED_BY, O.CREATED_BY, 
                      O.CREATE_DATE, O.ROOT, O.CREATING_CO, O.REV_INFO, O.STATUS, O.REV, O.WFS_ID, O.LOCKED_BY_NAME, O.CREATING_CO_NAME, 
                      O.APPROVAL_ACTIVITY, dbo.A_FN_PRODUCT_WHERE_USED_CHECK(PH.ID) AS HAS_USAGE, PH.AVAILABILITY
FROM         dbo.A_PRODUCTS_HISTORY PH INNER JOIN
                      dbo.A_OBJECTS O ON PH.OBJECT_ID = O.ID
GO

/****** Object:  View [dbo].[A_APPROVED_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_APPROVED_PRODUCTS]
AS
SELECT     dbo.A_PRODUCTS.ID, dbo.A_PRODUCTS.HISTORY_REF_ID, dbo.A_O_PRODUCTS.NAME, dbo.A_O_PRODUCTS.SUPPLIER_ID, 
                      dbo.A_O_PRODUCTS.COMMENTS, dbo.A_O_PRODUCTS.PROCEDURE_ID, dbo.A_O_PRODUCTS.APP_OBJECT, dbo.A_O_PRODUCTS.SHIP_OR_LABOR, 
                      dbo.A_O_PRODUCTS.CUSTOMIZABLE, dbo.A_O_PRODUCTS.REQ_FORM, dbo.A_O_PRODUCTS.MGR_TEAM, dbo.A_O_PRODUCTS.SALES_TAX, 
                      dbo.A_O_PRODUCTS.OBJECT_ID, dbo.A_O_PRODUCTS.PARENT_ID, dbo.A_O_PRODUCTS.LOCKED_BY, dbo.A_O_PRODUCTS.UNLOCKED_BY, 
                      dbo.A_O_PRODUCTS.CREATED_BY, dbo.A_O_PRODUCTS.CREATE_DATE, dbo.A_O_PRODUCTS.ROOT, dbo.A_O_PRODUCTS.CREATING_CO, 
                      dbo.A_O_PRODUCTS.REV_INFO, dbo.A_O_PRODUCTS.REV, dbo.A_O_PRODUCTS.STATUS, dbo.A_O_PRODUCTS.WFS_ID, 
                      dbo.A_O_PRODUCTS.LOCKED_BY_NAME, dbo.A_O_PRODUCTS.CREATING_CO_NAME, dbo.A_O_PRODUCTS.APPROVAL_ACTIVITY, 
                      dbo.A_PROCEDURES_HISTORY.SYSTEM_ID
FROM         dbo.A_PROCEDURES_HISTORY INNER JOIN
                      dbo.A_PROCEDURES ON dbo.A_PROCEDURES_HISTORY.ID = dbo.A_PROCEDURES.HISTORY_REF_ID RIGHT OUTER JOIN
                      dbo.A_O_PRODUCTS INNER JOIN
                      dbo.A_PRODUCTS ON dbo.A_O_PRODUCTS.ID = dbo.A_PRODUCTS.HISTORY_REF_ID ON 
                      dbo.A_PROCEDURES.ID = dbo.A_O_PRODUCTS.PROCEDURE_ID
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PRODUCTS_WITH_DATA]
AS
SELECT     dbo.A_PRODUCTS_HISTORY.ID, dbo.A_PRODUCTS_HISTORY.NAME, dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID, 
                      dbo.A_PRODUCTS_HISTORY.COMMENTS, dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID, dbo.A_PRODUCTS_HISTORY.APP_OBJECT, 
                      dbo.A_PRODUCTS_HISTORY.SHIP_OR_LABOR, dbo.A_PRODUCTS_HISTORY.CUSTOMIZABLE, dbo.A_PRODUCTS_HISTORY.REQ_FORM, 
                      dbo.A_PRODUCTS_HISTORY.MGR_TEAM, dbo.A_PRODUCTS_HISTORY.SALES_TAX, dbo.A_PRODUCTS_HISTORY.DRCM, 
                      dbo.A_PRODUCTS_HISTORY.MODBY, dbo.A_PRODUCTS_HISTORY.OBJECT_ID, dbo.A_PRODUCTS_HISTORY.PARENT_ID, 
                      dbo.A_V_APPROVED_ROLES_DATA.NAME AS MGR_TEAM_NAME, SUPPLIER.NAME AS SUPPLIER_NAME, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROCEDURE_NAME, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS APP_OBJ_NAME, 
                      dbo.A_APPROVED_PRODUCTS.NAME AS PARENT_NAME, dbo.A_PRODUCTS_HISTORY.SYSTEM_PROCEDURE, 
                      dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE, dbo.A_V_ROLE_DATA_BY_APPROVED_ID.NAME AS CUST_MGR_ROLE_NAME, 
                      dbo.A_PRODUCTS_HISTORY.PERSON_SUPPLIER, dbo.A_PRODUCTS_HISTORY.AVAILABILITY, dbo.A_PRODUCTS_HISTORY.OEM, 
                      dbo.A_PRODUCTS_HISTORY.MODEL, dbo.A_PRODUCTS_HISTORY.AREA, dbo.A_PRODUCTS_HISTORY.CU, dbo.A_PRODUCTS_HISTORY.MM
FROM         dbo.A_PRODUCTS_HISTORY LEFT OUTER JOIN
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID ON 
                      dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE = dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_PRODUCTS ON dbo.A_PRODUCTS_HISTORY.PARENT_ID = dbo.A_APPROVED_PRODUCTS.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_PRODUCTS_HISTORY.APP_OBJECT = dbo.A_V_APPROVED_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID = SUPPLIER.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_ROLES_DATA ON dbo.A_PRODUCTS_HISTORY.MGR_TEAM = dbo.A_V_APPROVED_ROLES_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_TASK_ASSIGNMENT_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_TASK_ASSIGNMENT_HISTORY]
AS
SELECT     ta.ID, ta.TASK_ID, ta.PERSON_ASSIGNED, ta.ROLE_ASSIGNED, p.FULL_NAME AS REQUESTEE_NAME, ta.REQUEST_DATE, ta.ACCEPTED_DATE, 
                      ta.REJECTION_DATE, ta.REASON_REJECTED, ta.REQUESTOR, ISNULL(r.NAME, '') + ISNULL(p.FULL_NAME, '') AS REQ_NAME, r.NAME, 
                      A_V_PEOPLE_APPROVED_DATA_1.FULL_NAME AS REQUESTOR_NAME, ta.STATUS
FROM         dbo.A_TASK_ASSIGNEE ta LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA A_V_PEOPLE_APPROVED_DATA_1 ON ta.REQUESTOR = A_V_PEOPLE_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID r ON ta.ROLE_ASSIGNED = r.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA p ON ta.PERSON_ASSIGNED = p.ID
GO

/****** Object:  View [dbo].[A_APPROVED_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_APPROVED_PROCEDURES]
AS
SELECT     dbo.A_PROCEDURES.ID, dbo.A_PROCEDURES.HISTORY_REF_ID, dbo.A_PROCEDURES_HISTORY.OBJECT_ID, 
                      dbo.A_PROCEDURES_HISTORY.VERB, dbo.A_PROCEDURES_HISTORY.NAME, dbo.A_PROCEDURES_HISTORY.COMMENTS, 
                      dbo.A_PROCEDURES_HISTORY.SECURITY_LEVEL, dbo.A_PROCEDURES_HISTORY.STEPS_IN_AP, dbo.A_PROCEDURES_HISTORY.WIP_MSG, 
                      dbo.A_PROCEDURES_HISTORY.DRCM, dbo.A_PROCEDURES_HISTORY.MODBY
FROM         dbo.A_PROCEDURES INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURES.HISTORY_REF_ID = dbo.A_PROCEDURES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_TASK_EDIT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASK_EDIT_DATA]
AS
SELECT     TASK.ID, TASK.PARENT_ID,
PARENT.DESCRIPTION AS PARENT_NAME,
TASK.Title, 
TASK.DESCRIPTION, 
TASK.STATUS, 
TASK.COMMENT,
TASK.REQUESTOR, 
TASK.COMPLETED_BY,
TASK.CHILD_ORDER, 
TASK.CREATED_BY, 
TASK.CREATE_DATE, 
TASK.CLOSED, 
TASK.OPENED_BY,
TASK.OPEN_DATE, 
TASK.SYSTEM_TASK,
TASK.PROCEDURE_ID, 
TASK.PROCEDURE_STEP_ID,
[PROC].NAME AS PROCEDURE_NAME, 
TASK.SECURITY_LEVEL, 
TASK.COUNTER AS COUNTER_ID,
REQUESTOR.P_NAME AS REQUESTOR_NAME,
TASK.REQUESTEE_ID, 
REQUESTEE.P_NAME AS REQUESTEE_NAME,
TASK.GROUP_REQUESTEE_ID, 
GROUP_REQUESTEE.NAME AS GROUP_REQUESTEE_NAME, 
TASK.COUNTER_NAME, 
TASK.COUNTER_VALUE, 
TASK.ORIG_PLANNED_START_DATE,
TASK.ORIG_PLANNED_STOP_DATE, 
TASK.CUR_PLANNED_START_DATE,
TASK.CUR_PLANNED_STOP_DATE, 
TASK.ACTUAL_START_DATE, 
TASK.ACTUAL_STOP_DATE, 
TASK.ORIG_PLANNED_COUNTER_START, 
TASK.ORIG_PLANNED_COUNTER_STOP,
TASK.CUR_PLANNED_COUNTER_START, 
TASK.CUR_PLANNED_COUNTER_STOP, 
TASK.ACTUAL_COUNTER_START, 
TASK.ACTUAL_COUNTER_STOP, 
TASK.LATEST_REQUESTEE_NAME, 
TASK.HAS_DISCUSSION,
TASK.HAS_SURVEY, 
TASK.HAS_CHILD, 
TASK.HAS_REF_PROC,
TASK.HAS_FILE, 
dbo.A_SECURITY_LEVELS.NAME AS SEC_LEV_NAME, 
TASK.ORIG_REQUESTOR_ID, 
TASK.HAS_REF_OBJ, 
TASK.HAS_MONITOR, 
ORIG_REQUESTOR.FULL_NAME AS ORIG_REQUESTOR_NAME, 
TASK.COLOR_CODE, 
dbo.A_TASK_SURVEY_LINK.SURVEY_ID,
TASK.LAST_REQUEST_DATE, 
TASK.PRIORITY,
TASK.IS_FILL, 
TASK.IS_QUOTE_ACCEPT, 
TASK.IS_QUOTE
FROM         dbo.A_TASK_SURVEY_LINK RIGHT OUTER JOIN
dbo.A_TASKS PARENT RIGHT OUTER JOIN
dbo.A_TASKS TASK ON PARENT.ID = TASK.PARENT_ID ON 
dbo.A_TASK_SURVEY_LINK.TASK_ID = TASK.ID LEFT OUTER JOIN
dbo.A_SECURITY_LEVELS ON TASK.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID LEFT OUTER JOIN
dbo.A_V_PEOPLE_APPROVED_DATA ORIG_REQUESTOR ON TASK.ORIG_REQUESTOR_ID = ORIG_REQUESTOR.ID LEFT OUTER JOIN
dbo.A_V_PEOPLE_BY_NTLOGIN REQUESTEE ON TASK.REQUESTEE_ID = REQUESTEE.P_ID LEFT OUTER JOIN
dbo.A_V_ROLE_DATA_BY_APPROVED_ID GROUP_REQUESTEE ON TASK.GROUP_REQUESTEE_ID = GROUP_REQUESTEE.ID LEFT OUTER JOIN
dbo.A_V_PEOPLE_BY_NTLOGIN REQUESTOR ON TASK.REQUESTOR = REQUESTOR.P_ID LEFT OUTER JOIN
dbo.A_APPROVED_PROCEDURES [PROC] ON TASK.PROCEDURE_ID = [PROC].ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEP_LABOR]
AS
SELECT     dbo.A_V_ROLE_DATA_BY_APPROVED_ID.NAME AS ROLE_NAME, dbo.A_PROCEDURE_STEPS.PROCEDURE_ID, 
                      dbo.A_PROCEDURE_STEPS.ID AS STEP_ID, dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID AS ROLE_ID, 
                      dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP, dbo.A_PROCEDURE_OBJECT_LINK.LABOR_ROLE, dbo.A_PROCEDURE_OBJECT_LINK.QTY, 
                      dbo.A_PROCEDURE_OBJECT_LINK.QTY_TYPE, dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID AS OBJ_REF_ID, 
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID.HISTORY_REF_ID AS OBJ_ID, dbo.A_V_ROLE_DATA_BY_APPROVED_ID.NAME AS OBJ_DESC, 
                      dbo.A_PROCEDURE_OBJECT_LINK.ID
FROM         dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID INNER JOIN
                      dbo.A_V_ROLE_DATA_BY_APPROVED_ID ON 
                      dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_V_ROLE_DATA_BY_APPROVED_ID.ID
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_WITH_PEOPLE_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ROLES_APPROVED_WITH_PEOPLE_IDS]
AS
SELECT     dbo.A_ROLES.ID AS ROLE_ID, dbo.A_ROLES.HISTORY_REF_ID, dbo.A_ROLE_ASSIGNEE.PERSON, dbo.A_ROLES_HISTORY.IS_ADMIN
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ON dbo.A_ROLES_HISTORY.ID = dbo.A_ROLE_ASSIGNEE.ROLE
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DATA_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ROLES_APPROVED_DATA_QUICK]
AS
SELECT     dbo.A_OBJECTS.CREATING_CO, dbo.A_ROLES_HISTORY.NAME, dbo.A_ROLES.ID, dbo.A_ROLES.HISTORY_REF_ID
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_ROLES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_APPROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_APPROVED_PEOPLE]
AS
SELECT     P.ID, PH.LOGIN, PH.NAME, PH.PASSWORD, PH.BOSS, PH.SOURCE, PH.LAST_NAME, PH.MIDDLE_NAME, PH.NICK_NAME, PH.LANG, PH.HIRE_DATE, PH.DRCM, 
                      PH.MODBY, PH.OBJECT_ID, PH.COMPANY, PH.TIME_ZONE, PH.FULL_NAME, PH.SYSTEM_STATUS, PH.CO_POSITION, PH.ROOT_COMPANY, 
                      ROOT_CO.NAME AS ROOT_CO_NAME, CO.NAME AS CO_NAME, dbo.A_TIME_ZONES.G_DIFF, PH.IS_HEAD, P.STATUS, PH.TOOL_BOX, PH.INFO_BOX, 
                      PH.ADV_SEARCH, PH.COLOR_KEY, PH.SCREEN_TYPE, PH.CHANGE_PASS, dbo.A_V_ROLES_APPROVED_DATA_QUICK.NAME AS POSITION_NAME, 
                      P.HISTORY_REF_ID
FROM         dbo.A_PEOPLE AS P INNER JOIN
                      dbo.A_PEOPLE_HISTORY AS PH ON P.HISTORY_REF_ID = PH.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON PH.CO_POSITION = dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_TIME_ZONES ON PH.TIME_ZONE = dbo.A_TIME_ZONES.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS CO ON PH.COMPANY = CO.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS ROOT_CO ON PH.ROOT_COMPANY = ROOT_CO.ID
WHERE     (P.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_ROLES_APPROVED_DATA]
AS
SELECT     dbo.A_ROLES.ID, dbo.A_ROLES.HISTORY_REF_ID, dbo.A_ROLES_HISTORY.NAME, dbo.A_ROLES_HISTORY.OBJECT_ID, 
                      dbo.A_ROLES_HISTORY.IS_ADMIN, dbo.A_ROLES_HISTORY.SECURITY_LEVEL, dbo.A_ROLES_HISTORY.HIDDEN, dbo.A_ROLES.STATUS
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID
WHERE     (dbo.A_ROLES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLE_LINK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_WF_GROUP_ROLE_LINK]
AS
SELECT     l.WF_GROUP_ID AS GROUP_ID, l.ROLE_ID, dbo.A_V_ROLES_APPROVED_DATA.NAME AS ROLE_NAME
FROM         dbo.A_WF_GROUP_ROLE_LINK l INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA ON l.ROLE_ID = dbo.A_V_ROLES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLES_WITH_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WF_GROUP_ROLES_WITH_MEMBERS]
AS
SELECT     dbo.A_V_WF_GROUP_ROLE_LINK.GROUP_ID, dbo.A_V_WF_GROUP_ROLE_LINK.ROLE_ID, p.FULL_NAME, 
                      dbo.A_V_ROLES_APPROVED_WITH_PEOPLE_IDS.PERSON
FROM         dbo.A_V_ROLES_APPROVED_WITH_PEOPLE_IDS INNER JOIN
                      dbo.A_V_WF_GROUP_ROLE_LINK ON 
                      dbo.A_V_ROLES_APPROVED_WITH_PEOPLE_IDS.ROLE_ID = dbo.A_V_WF_GROUP_ROLE_LINK.ROLE_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON dbo.A_V_ROLES_APPROVED_WITH_PEOPLE_IDS.PERSON = p.ID
GO

/****** Object:  View [dbo].[A_O_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_O_PROCEDURES]
AS
SELECT     dbo.leadingSpaces(o.ROOT, 30) AS SPECIAL_ROOT, dbo.leadingSpaces(ph.ID, 30) AS SPECIAL_ID, ph.ID, ph.OBJECT_ID, ph.VERB, ph.NAME, 
                      ph.COMMENTS, ph.SECURITY_LEVEL, ph.STEPS_IN_AP, ph.WIP_MSG, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, 
                      o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, 
                      dbo.A_APPROVED_VERBS.NAME AS VERB_NAME, dbo.A_APPROVED_VERBS.ID AS VERB_ID, o.ID AS OBJ_ID, ph.CREATING_DEPT, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, ph.IS_SYSTEM, 
                      ph.SYSTEM_ID, ph.DURATION_TYPE, ph.DURATION, ph.Threshold
FROM         dbo.A_PROCEDURES_HISTORY ph INNER JOIN
                      dbo.A_OBJECTS o ON ph.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON ph.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_SECURITY_LEVELS ON ph.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_VERBS ON ph.VERB = dbo.A_APPROVED_VERBS.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_HISTORY_SEARCH]
AS
SELECT        dbo.leadingSpaces(o.ROOT, 30) AS SPECIAL_ROOT, dbo.leadingSpaces(ph.ID, 30) AS SPECIAL_ID, ph.ID, ph.OBJECT_ID, ph.VERB, ph.NAME, ph.SECURITY_LEVEL, o.LOCKED_BY, o.CREATED_BY, o.ROOT, o.CREATING_CO, 
                         o.STATUS, o.REV, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, dbo.A_APPROVED_VERBS.NAME AS VERB_NAME, dbo.A_APPROVED_VERBS.ID AS VERB_ID, o.ID AS OBJ_ID, ph.CREATING_DEPT, 
                         dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, ph.IS_SYSTEM, ph.IsActive,
						 ph.DRCM
FROM            dbo.A_PROCEDURES_HISTORY AS ph INNER JOIN
                         dbo.A_OBJECTS AS o ON ph.OBJECT_ID = o.ID INNER JOIN
                         dbo.A_V_COMPANIES_APPROVED_DATA ON ph.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                         dbo.A_SECURITY_LEVELS ON ph.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID LEFT OUTER JOIN
                         dbo.A_APPROVED_VERBS ON ph.VERB = dbo.A_APPROVED_VERBS.ID
GO

/****** Object:  View [dbo].[Portal_ProceduresView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ProceduresView]
AS
SELECT        PH.SPECIAL_ROOT AS SpecialRoot, PH.SPECIAL_ID AS SpecialId, PH.ID, PH.OBJECT_ID AS ObjectId, PH.VERB, PH.NAME, PH.SECURITY_LEVEL AS SecurityLevel, PH.LOCKED_BY AS LockedBy, 
                         PH.CREATED_BY AS CreatedBy, PH.ROOT, PH.CREATING_CO AS CreatingCo, PH.STATUS, PH.REV, PH.LOCKED_BY_NAME AS LockedByName, PH.CREATING_CO_NAME AS CreatingCoName, PH.VERB_NAME AS VerbName, 
                         PH.VERB_ID AS VerbId, PH.OBJ_ID AS ObjId, PH.CREATING_DEPT AS CreatingDept, PH.DEPT_NAME AS DeptName, PH.SECURITY_NAME AS SecurityName, PH.IS_SYSTEM AS IsSystem, PH.IsActive, AP.COMMENTS, 
                         AP.STEPS_IN_AP AS StepInAp, AP.WIP_MSG AS WipMsg, AP.DURATION, AP.DURATION_TYPE AS DurationType, AP.SYSTEM_ID AS SystemId, AP.Threshold,
isnull(STUFF((
SELECT +','+ DL.NAME+'|'+DL.LINKED_DOC_ID 
FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
WHERE DL.OBJECT_ID = AP.OBJECT_ID
    FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles,
ph.DRCM AS UpdatedDate
FROM            dbo.A_V_PROCEDURE_HISTORY_SEARCH AS PH LEFT OUTER JOIN
                         dbo.A_O_PROCEDURES AS AP ON AP.OBJECT_ID = PH.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_COMPANIES_DROP_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_COMPANIES_DROP_SEARCH]
AS
SELECT     c.NAME AS ROOT_NAME, c.ROOT_CO_ID, c.ID, CASE WHEN (c.ROOT_CO_ID = c.ID) THEN c.NAME + ' - [ID:' + c.ID + ']' ELSE ISNULL('  [' + r.NAME + ']', '') 
                      + c.NAME + ' - [ID:' + c.ID + ']' END AS NAME
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS c LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS r ON c.ROOT_CO_ID = r.ID
GO

/****** Object:  View [dbo].[A_V_PRODUCT_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


create view [dbo].[A_V_PRODUCT_SEARCH_DATA]
AS
SELECT     ph.ID, ph.OBJECT_ID, ph.PARENT_ID, ph.NAME, ph.SUPPLIER_ID, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS SUPPLIER_NAME, 
                      ph.COMMENTS, ph.PROCEDURE_ID, dbo.A_V_PROCEDURES_APPROVED_DATA.VERB, dbo.A_V_PROCEDURES_APPROVED_DATA.VERB_NAME, 
                      ph.APP_OBJECT, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS APP_OBJ_NAME, ph.SHIP_OR_LABOR, ph.CUSTOMIZABLE, ph.REQ_FORM, 
                      ph.MGR_TEAM, dbo.A_APPROVED_ROLES.NAME AS MGR_TEAM_NAME, ph.SALES_TAX, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROCEDURE_NAME, dbo.A_OBJECTS.ID AS OBJ_ID, dbo.A_OBJECTS.LOCKED_BY, 
                      dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO,
                       dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, 
                      dbo.A_OBJECTS.CREATING_CO_NAME, dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_FN_PRODUCT_WHERE_USED_CHECK(ph.ID) AS HAS_USAGE, 
                      ph.CUST_MGR_ROLE,
					  ph.CustomerRequirementId,
					  ph.MaterialCost,
					  ph.TotalSalePrice,
					  ph.IsProduct,
					  ph.Division,
					  ph.LocationId,
					  PQP.CUST_ID AS CustomerId,
					  ph.CycleTime
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_PRODUCTS_HISTORY ph ON dbo.A_OBJECTS.ID = ph.OBJECT_ID LEFT OUTER JOIN
                      dbo.A_APPROVED_ROLES ON ph.MGR_TEAM = dbo.A_APPROVED_ROLES.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON ph.APP_OBJECT = dbo.A_V_APPROVED_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON ph.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON ph.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID LEFT OUTER JOIN 
					  dbo.A_PRODUCTS_QUICK_PRICE PQP ON ph.ID = PQP.PROD_HIST_ID




GO

/****** Object:  View [dbo].[Portal_CustomerRequirementView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Portal_CustomerRequirementView]
AS

SELECT 
NEWID() AS Id,
c.Id AS CustomerSubmitId,
sup.ROOT_NAME AS Company,
c.Description,
p.SUPPLIER_ID AS SupplierId,
p.SUPPLIER_NAME AS SupplierName,
p.LocationId,
c.SubmittedDate,
p.PROCEDURE_ID AS ProcedureId,
p.PROCEDURE_NAME AS ProcedureName,
p.APP_OBJECT AS PartId,
c.QuoteJson,
c.CustomerRequirementJson,
p.NAME AS ProductName,
p.ID AS ProductId,
c.Respresentative,
p.Division,
c.PartKitNo,
c.SubmittedBy,
p.TotalSalePrice,
c.Price,
p.MaterialCost,
c.LeadTime,
c.ProductWorkflowId,
p.REV AS Rev,
p.IsProduct,
p.STATUS,
p.OBJECT_ID AS ObjectId,
pqp.CUST_ID as CustomerId,
Customer.NAME AS CustomerName,
p.CycleTime
FROM  A_V_PRODUCT_SEARCH_DATA p
INNER JOIN [dbo].[Portal_CustomerSubmittedRequirement] c ON c.Id = p.CustomerRequirementId
LEFT JOIN Portal_ProceduresView pv ON p.PROCEDURE_ID = pv.ObjectId
LEFT  JOIN A_V_COMPANIES_DROP_SEARCH sup on sup.ID = p.SUPPLIER_ID
LEFT JOIN A_PRODUCTS_QUICK_PRICE pqp ON P.ID = pqp.PROD_HIST_ID
OUTER APPLY (
SELECT cus.Name FROM A_V_COMPANIES_DROP_SEARCH cus
WHERE cus.Id= pqp.CUST_ID
) AS Customer
GO

/****** Object:  View [dbo].[A_O_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_O_THEORY]
AS
SELECT     dbo.leadingSpaces(o.ROOT, '30') AS SPECIAL_ROOT, dbo.leadingSpaces(th.ID, '30') AS SPECIAL_ID, th.ID, th.OBJECT_ID, th.NAME, th.COMMENTS, 
                      th.SECURITY_LEVEL, th.CREATING_DEPT, o.ID AS OBJ_ID, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO,
                       o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME
FROM         dbo.A_THEORY_HISTORY th INNER JOIN
                      dbo.A_OBJECTS o ON th.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON th.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_SECURITY_LEVELS ON th.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID
GO

/****** Object:  View [dbo].[A_O_THEORY_WITH_PARAGRAPHS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_O_THEORY_WITH_PARAGRAPHS]
AS
SELECT     dbo.leadingSpaces(o.ROOT, '30') AS SPECIAL_ROOT, dbo.leadingSpaces(th.ID, '30') AS SPECIAL_ID, th.ID, th.OBJECT_ID, th.NAME, 
                      th.SECURITY_LEVEL, th.CREATING_DEPT, o.ID AS OBJ_ID, o.LOCKED_BY, o.CREATED_BY, o.ROOT, o.CREATING_CO, o.STATUS, o.REV, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, 
                      dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, dbo.A_THEORY_PARAGRAPHS.PARAGRAPH_TEXT, o.APPROVAL_DATE,
					  th.DRCM
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_THEORY_HISTORY th ON o.ID = th.OBJECT_ID INNER JOIN
                      dbo.A_SECURITY_LEVELS ON th.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON th.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_THEORY_PARAGRAPHS ON th.ID = dbo.A_THEORY_PARAGRAPHS.THEORY_ID
GO

/****** Object:  View [dbo].[A_V_OBJECT_REVISION_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[A_V_OBJECT_REVISION_DATA]
AS
SELECT     dbo.A_OBJECTS.ID AS OBJECT_ID, dbo.A_OBJECTS.OBJ_ID AS HIST_ID, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.CREATED_BY, 
                      dbo.A_PEOPLE_HISTORY.FULL_NAME AS CREATOR_NAME, dbo.A_OBJECTS.REV_INFO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.CREATE_DATE, 
                      dbo.A_OBJECTS.APPROVAL_DATE, dbo.A_WORKFLOWS.NAME AS WF_NAME, dbo.A_OBJECTS.REV
FROM         dbo.A_PEOPLE_HISTORY INNER JOIN
                      dbo.A_PEOPLE ON dbo.A_PEOPLE_HISTORY.ID = dbo.A_PEOPLE.HISTORY_REF_ID INNER JOIN
                      dbo.A_OBJECTS INNER JOIN
                      dbo.A_WORKFLOWS_STARTED ON dbo.A_OBJECTS.WFS_ID = dbo.A_WORKFLOWS_STARTED.ID INNER JOIN
                      dbo.A_WORKFLOWS ON dbo.A_WORKFLOWS_STARTED.WF_ID = dbo.A_WORKFLOWS.ID ON dbo.A_PEOPLE.ID = dbo.A_OBJECTS.CREATED_BY
GO

/****** Object:  View [dbo].[Portal_DocumentsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_DocumentsView]
AS
SELECT DISTINCT 
tp.SPECIAL_ROOT AS SpecialRoot,
tp.SPECIAL_ID AS SpecialID,
tp.ID, tp.OBJECT_ID AS ObjectId,
tp.SECURITY_LEVEL AS SecurityLevel,
tp.CREATING_DEPT AS CreatingDept,
tp.OBJ_ID AS ObjId, tp.LOCKED_BY AS LockedBy, 
tp.CREATED_BY AS CreatedBy, 
tp.ROOT,
tp.CREATING_CO AS CreatingCo,
tp.NAME, tp.CREATING_CO_NAME AS CreatingCoName,
tp.DEPT_NAME AS DeptName,
tp.REV,
tp.STATUS, 
tp.LOCKED_BY_NAME AS LockedByName, 
tp.SECURITY_NAME AS SecurityName,
tp.APPROVAL_DATE AS ApprovalDate, 
t.COMMENTS,
isnull(STUFF((
SELECT +','+ DL.NAME+'|'+DL.LINKED_DOC_ID 
FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
WHERE DL.OBJECT_ID = tp.OBJECT_ID
FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles,
avo.CREATE_DATE AS UpdatedDate,
avo.CREATOR_NAME AS UpdatedBy
FROM dbo.A_O_THEORY_WITH_PARAGRAPHS AS tp 
INNER JOIN dbo.A_O_THEORY AS t ON tp.OBJ_ID = t.OBJ_ID
LEFT JOIN A_V_OBJECT_REVISION_DATA  AS avo ON avo.OBJECT_ID =tp.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_DATA]
AS
SELECT     PARENT_TASK.ID AS PARENT_ID, STEP_TASK.ID AS STEP_ID, STEP_TASK.DESCRIPTION, STEP_TASK.STATUS, STEP_TASK.REQUESTOR, 
STEP_TASK.COMPLETED_BY, PROCEDURE_STEPS.ID AS PH_STEP_ID, PROCEDURE_STEPS.PRINT_ORDER, PROC_PREC_STEP_LINK.PREV_STEP, 
PREV_STEP_TASK.ID AS PREV_TASK_ID, PREV_STEP_TASK.STATUS AS PREV_TASK_STATUS, STEP_TASK.RECURSION_NUMBER, 
STEP_TASK.HAS_SURVEY, STEP_TASK.HAS_CHILD, STEP_TASK.HAS_REF_PROC, STEP_TASK.HAS_FILE, STEP_TASK.SYSTEM_TASK, 
STEP_TASK.HAS_MONITOR, STEP_TASK.REQUESTEE_ID, STEP_TASK.GROUP_REQUESTEE_ID, STEP_TASK.CHILD_STATUS, 
STEP_TASK.LATEST_REQUESTEE_NAME, PROCEDURE_STEPS.PROCEDURE_ID AS PROC_HIST_ID,oProc.VERB_NAME,
PROCEDURE_STEPS.TITLE,
PROCEDURE_STEPS.Roles,
PROCEDURE_STEPS.EquipmentTime,
PROCEDURE_STEPS.OLD_STEP_ID as OldStepId,
PROC_PREC_STEP_LINK.DRCM AS UpdatedDate
FROM         dbo.A_TASKS PARENT_TASK RIGHT OUTER JOIN
dbo.A_TASKS STEP_TASK ON PARENT_TASK.ID = STEP_TASK.PARENT_ID LEFT OUTER JOIN
dbo.A_PROCEDURE_STEP_PRECEDING_STEPS PROC_PREC_STEP_LINK INNER JOIN
dbo.A_TASKS PREV_STEP_TASK ON PROC_PREC_STEP_LINK.PREV_STEP = PREV_STEP_TASK.PROCEDURE_STEP_ID ON 
STEP_TASK.RECURSION_NUMBER = PREV_STEP_TASK.RECURSION_NUMBER AND 
STEP_TASK.PROCEDURE_STEP_ID = PROC_PREC_STEP_LINK.MY_STEP AND PARENT_TASK.ID = PREV_STEP_TASK.PARENT_ID LEFT OUTER JOIN
dbo.A_PROCEDURE_STEPS PROCEDURE_STEPS ON STEP_TASK.PROCEDURE_STEP_ID = PROCEDURE_STEPS.ID
LEFT JOIN dbo.A_V_PROCEDURES_APPROVED_DATA oProc on PROCEDURE_STEPS.PROCEDURE_ID = oProc.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[Portal_ObjectSearch]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ObjectSearch]
AS

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, COMMENTS AS Description, UpdatedDate, 'Procedures' AS ItemType FROM Portal_ProceduresView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, Name, COMMENTS AS Description, UpdatedDate, 'Documents' AS ItemType FROM Portal_DocumentsView

UNION

SELECT NEWID() AS Id, ObjectId AS ObjectId, MonitorType,  Description AS Description, UpdatedDate, 'Monitors' as ItemType FROM Portal_MonitorView

UNION

SELECT NEWID() AS Id, toi.FILL_ITEM_ID AS ObjectId, TITLE AS Name, td.DESCRIPTION,  td.UpdatedDate, 'Steps' AS ItemType  
FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA td
INNER JOIN A_TASK_ORDER_INFORMATION toi ON toi.TASK_ID = td.PARENT_ID
GO

/****** Object:  View [dbo].[A_V_INVOICES_WITH_ACCT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_INVOICES_WITH_ACCT_INFORMATION]
AS
SELECT     INVOICE.ID AS INVOICE_ID, ACCOUNT.NAME AS ACCT_NAME, INVOICE.INVOICE_DATE, INVOICE.STATUS, INVOICE.AMT_PAID, 
                      INVOICE.NEW_ITEMS_AMT, INVOICE.PREVIOUS_BALANCE, INVOICE.PAYMENT_AMOUNT, INVOICE.DISPUTED_AMOUNT, INVOICE.TOTAL_DUE, 
                      INVOICE.DUE_DATE, ACCOUNT.ACCT_TYPE, ACCOUNT.CREATING_CO, ACCOUNT.REFERENCE_PO, ACCOUNT.REFERENCE_NAME, 
                      ACCOUNT.OPEN_DATE, ACCOUNT.CLOSE_DATE, ACCOUNT.SUPPLIER_CO, ACCOUNT.CUSTOMER_CO, ACCOUNT.CUSTOMER_BILL_CO, 
                      ACCOUNT.TOTAL_PURCHASE_LIMIT, ACCOUNT.CREDIT_LIMIT, ACCOUNT.SUPPLIER_NAME, ACCOUNT.CUSTOMER_NAME, 
                      ACCOUNT.CUST_BILL_NAME, ACCOUNT.PRODUCT_ID, PROD.NAME AS PRODUCT_NAME, INVOICE.ACCOUNT_ID, INVOICE.LATE_FEES, 
                      ACCOUNT.TOTAL_PURCHASES, INVOICE.DATE_SENT_TO_CUSTOMER, INVOICE.PURCHASE_ID, INVOICE.NAME AS INVOICE_NAME, 
                      INVOICE.CREATE_DATE, INVOICE.INVOICE_TYPE, ACCOUNT.MAXIMUM_USES, ACCOUNT.INVOICE_TRIGGER, ACCOUNT.INVOICE_PERIOD_NUMBER, 
                      ACCOUNT.INVOICE_PERIOD_TYPE, ACCOUNT.FIRST_INVOICE_DATE, ACCOUNT.NEXT_INVOICE_DATE, ACCOUNT.PAYMENT_GRACE_PERIOD, 
                      ACCOUNT.LATE_FEE_PERCENTAGE, ACCOUNT.AMT_INVOICED, ACCOUNT.BALANCE, ACCOUNT.INVOICED_BALANCE, 
                      ACCOUNT.UNINVOICED_BALANCE, ACCOUNT.BILLING_EMAIL, ACCOUNT.TOTAL_DEBITS, ACCOUNT.TOTAL_CREDITS, 
                      PURCHASE.CUST_PURCH_NUM, INVOICE.INVOICE_BALANCE, INVOICE.TOTAL_TAX, INVOICE.PO_NUMBER
FROM         dbo.A_V_PURCHASES_APPROVED_DATA PURCHASE RIGHT OUTER JOIN
                      dbo.A_ACCOUNT_INVOICES INVOICE INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ACCOUNT ON INVOICE.ACCOUNT_ID = ACCOUNT.ID ON 
                      PURCHASE.ID = INVOICE.PURCHASE_ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA PROD ON ACCOUNT.PRODUCT_ID = PROD.ID
GO

/****** Object:  View [dbo].[Portal_InvoicesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

create view [dbo].[Portal_InvoicesView]
AS

select 
INVOICE.ID as InvoiceId,
ACCOUNT.NAME as AcctName,
INVOICE.INVOICE_DATE as InvoiceDate,
INVOICE.STATUS as Status,
INVOICE.AMT_PAID  as AmtPaid,
INVOICE.NEW_ITEMS_AMT as NewItemsAmt,
INVOICE.PREVIOUS_BALANCE as PreviousBalance,
INVOICE.PAYMENT_AMOUNT as PaymentAmount,
INVOICE.DISPUTED_AMOUNT as DisputedAmount,
INVOICE.TOTAL_DUE as TotalDue,
INVOICE.DUE_DATE as DueDate,
ACCOUNT.ACCT_TYPE as AcctType,
ACCOUNT.CREATING_CO as CreatingCo,
ACCOUNT.REFERENCE_PO as ReferencePo,
ACCOUNT.REFERENCE_NAME as ReferenceName,
ACCOUNT.OPEN_DATE as OpenDate,
ACCOUNT.CLOSE_DATE as CloseDate,
ACCOUNT.SUPPLIER_CO  as SupplierCo,
ACCOUNT.CUSTOMER_CO  as CustomerCo,
ACCOUNT.CUSTOMER_BILL_CO as CustomerBillCo,
ACCOUNT.TOTAL_PURCHASE_LIMIT as TotalPurchaseLimit,
ACCOUNT.CREDIT_LIMIT as CreaditLimit,
ACCOUNT.SUPPLIER_NAME as SupplierName,
ACCOUNT.CUSTOMER_NAME  as CustomerName,
ACCOUNT.CUST_BILL_NAME as CustBillName,
ACCOUNT.PRODUCT_ID  as ProductId,
PROD.NAME as ProductName,
INVOICE.ACCOUNT_ID  as AccountId,
INVOICE.LATE_FEES as LateFees,
ACCOUNT.TOTAL_PURCHASES  as TotalPurchases,
INVOICE.DATE_SENT_TO_CUSTOMER as DateSentToCustomer,
INVOICE.PURCHASE_ID as PurchaseId, 
INVOICE.NAME   as InvoiceName,
INVOICE.CREATE_DATE as CreateDate,
INVOICE.INVOICE_TYPE as InvoiceType, 
ACCOUNT.MAXIMUM_USES as MaximumUses,
ACCOUNT.INVOICE_TRIGGER  as InvoiceTrigger,
ACCOUNT.INVOICE_PERIOD_NUMBER  as InvoicePeriodNumber,
ACCOUNT.INVOICE_PERIOD_TYPE as InvoicePeriodType,
ACCOUNT.FIRST_INVOICE_DATE as FirstInvoiceDate, 
ACCOUNT.NEXT_INVOICE_DATE as NextInvoiceDate,
ACCOUNT.PAYMENT_GRACE_PERIOD as PaymentGracePeriod,
ACCOUNT.LATE_FEE_PERCENTAGE  as LateFeePercentage,
ACCOUNT.AMT_INVOICED as AmtInvoiced,
ACCOUNT.BALANCE as Balance,
ACCOUNT.INVOICED_BALANCE as InvoicedBalance,
ACCOUNT.UNINVOICED_BALANCE as UninvoicedBalance,
ACCOUNT.BILLING_EMAIL as BellingEmail,
ACCOUNT.TOTAL_DEBITS as TotalDebits,
ACCOUNT.TOTAL_CREDITS as TotalCredits,
PURCHASE.CUST_PURCH_NUM as CustPurchNum,
INVOICE.INVOICE_BALANCE as InvoiceBalance,
INVOICE.TOTAL_TAX as TotalTax,
INVOICE.PO_NUMBER as PoNumber
FROM  dbo.A_V_PURCHASES_APPROVED_DATA AS PURCHASE RIGHT OUTER JOIN
dbo.A_ACCOUNT_INVOICES AS INVOICE INNER JOIN
dbo.A_V_ACCOUNTS_APPROVED_DATA AS ACCOUNT ON INVOICE.ACCOUNT_ID = ACCOUNT.ID ON PURCHASE.ID = INVOICE.PURCHASE_ID LEFT OUTER JOIN
dbo.A_V_PRODUCTS_APPROVED_DATA AS PROD ON ACCOUNT.PRODUCT_ID = PROD.ID
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEM_EDIT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_FORECAST_ITEM_EDIT_DATA]
AS
SELECT     ACCOUNTS.SUPPLIER_NAME, ACCOUNTS.CUSTOMER_NAME, F_ITEM.*, PRODUCTS.NAME AS PRODUCT_NAME, PRODUCTS.ID AS PRODUCT_ID, 
                      ACCOUNTS.NAME AS ACCT_NAME, ACCOUNTS.CUSTOMER_CO AS CUSTOMER_ID, ACCOUNTS.SUPPLIER_CO AS SUPPLIER_ID, 
                      dbo.A_FORECASTS_HISTORY.CO AS FORECAST_CO
FROM         dbo.A_FORECAST_ITEMS F_ITEM INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ACCOUNTS ON F_ITEM.ACCOUNT_ID = ACCOUNTS.ID INNER JOIN
                      dbo.A_FORECASTS_HISTORY ON F_ITEM.FORECAST_ID = dbo.A_FORECASTS_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA PRODUCTS ON ACCOUNTS.PRODUCT_ID = PRODUCTS.ID
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_BY_APPROVED_ID]
AS
SELECT     dbo.A_PROD_PRICE_LIST.ID, dbo.A_PROD_PRICE_LIST.HISTORY_REF_ID, dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCT, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.CUSTOMER, dbo.A_PROD_PRICE_LIST_HISTORY.UNIT, dbo.A_PROD_PRICE_LIST_HISTORY.MIN_QUANTITY, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.UNIT_PRICE, dbo.A_PROD_PRICE_LIST_HISTORY.EST_UNIT_PRICE, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.EST_LABOR_PRICE, dbo.A_PROD_PRICE_LIST_HISTORY.EST_PARTS_PROV_STAY, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.EST_PARTS_PROV_TAKE_BACK, dbo.A_PROD_PRICE_LIST_HISTORY.EST_PARTS_CONSUMED, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.INVOICE_FROM, dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCTION_TIME, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCTION_TIME_UNIT, dbo.A_PROD_PRICE_LIST_HISTORY.CAPACITY, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.CAPACITY_UNIT, dbo.A_PROD_PRICE_LIST_HISTORY.DRCM, dbo.A_PROD_PRICE_LIST_HISTORY.MODBY, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.OBJECT_ID, dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCT_NAME, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.CUSTOMER_NAME, dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID, dbo.A_PROD_PRICE_LIST.STATUS, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.FOR_INDIVIDUAL_SALE
FROM         dbo.A_PROD_PRICE_LIST INNER JOIN
                      dbo.A_PROD_PRICE_LIST_HISTORY ON dbo.A_PROD_PRICE_LIST.HISTORY_REF_ID = dbo.A_PROD_PRICE_LIST_HISTORY.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCT = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
WHERE     (dbo.A_PROD_PRICE_LIST.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_FORECAST_ITEMS]
AS
SELECT     dbo.A_FORECASTS_HISTORY.OBJECT_ID AS F_OBJ_ID, dbo.A_FORECASTS_HISTORY.NAME AS F_NAME, dbo.A_FORECAST_ITEMS.ID, 
                      dbo.A_FORECASTS_HISTORY.START_DATE, dbo.A_FORECASTS_HISTORY.STOP_DATE, dbo.A_FORECAST_ITEMS.ACCOUNT_ID, 
                      dbo.A_FORECAST_ITEMS.F_TYPE, dbo.A_FORECAST_ITEMS.QTY, dbo.A_FORECAST_ITEMS.F_AMT, dbo.A_FORECAST_ITEMS.PERCENT_OF_REV, 
                      dbo.A_FORECAST_ITEMS.PROGRESS, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, C_ACCOUNT.NAME AS ACCOUNT_NAME, 
                      P_ACCOUNT.CUSTOMER_CO, P_ACCOUNT.SUPPLIER_CO, PROD_PRICE_LIST.UNIT_PRICE, P_ACCOUNT.PARENT_ACCOUNT, 
                      P_ACCOUNT.NAME AS PARENT_ACCT_NAME, SUPPLIER.NAME AS SUPPLIER_NAME, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CUSTOMER_NAME, dbo.A_FORECAST_ITEMS.AVG_MONTHLY, 
                      dbo.A_FORECAST_ITEMS.CONFIDENCE, dbo.A_FORECAST_ITEMS.FORECAST_ID, dbo.A_FORECAST_ITEMS.NOTE, 
                      dbo.A_FORECAST_ITEMS.AMT_INVOICED, dbo.A_FORECAST_ITEMS.CUSTOMER_OWNER, dbo.A_FORECAST_ITEMS.SUPPLIER_OWNER, 
                      dbo.A_FORECAST_ITEMS.PRIORITY, dbo.A_FORECAST_ITEMS.STATUS, dbo.A_FORECAST_ITEMS.ACT_FIRST_PURCHASE_DATE, 
                      dbo.A_FORECAST_ITEMS.EST_FIRST_PURCHASE_DATE, dbo.A_FORECAST_ITEMS.ACT_QUAL_START_DATE, 
                      dbo.A_FORECAST_ITEMS.EST_QUAL_START_DATE, dbo.A_FORECAST_ITEMS.DATE_ADDED
FROM         dbo.A_FORECAST_ITEMS INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA C_ACCOUNT ON dbo.A_FORECAST_ITEMS.ACCOUNT_ID = C_ACCOUNT.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON C_ACCOUNT.SUPPLIER_CO = SUPPLIER.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON C_ACCOUNT.CUSTOMER_CO = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA P_ACCOUNT ON C_ACCOUNT.PARENT_ACCOUNT = P_ACCOUNT.ID LEFT OUTER JOIN
                      dbo.A_V_PROD_PRICE_LIST_BY_APPROVED_ID PROD_PRICE_LIST ON C_ACCOUNT.CUSTOMER_CO = PROD_PRICE_LIST.CUSTOMER AND 
                      C_ACCOUNT.PRODUCT_ID = PROD_PRICE_LIST.PRODUCT LEFT OUTER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON C_ACCOUNT.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_FORECASTS_HISTORY ON dbo.A_FORECAST_ITEMS.FORECAST_ID = dbo.A_FORECASTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_O_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_O_ORDERS]
AS
SELECT     H.ID, H.OBJECT_ID, H.CUSTOMER_PERSON, H.CUSTOMER_CO, H.DESCRIPTION, H.BUDGETARY_ONLY, H.EXPIRATION_DATE, H.DRCM, H.MODBY, 
                      O.ID AS OBJ_ID, O.LOCKED_BY, O.UNLOCKED_BY, O.CREATED_BY, O.CREATE_DATE, O.ROOT, O.REV_INFO, O.CREATING_CO, O.STATUS, O.REV, 
                      O.WFS_ID, O.LOCKED_BY_NAME, O.CREATING_CO_NAME, O.APPROVAL_ACTIVITY, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CUSTOMER_NAME, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS CUST_PERSON_NAME, 
                      H.PROGRESS, dbo.A_V_COMPANIES_APPROVED_DATA.ROOT_CO, A_V_COMPANIES_APPROVED_DATA_1.NAME AS CUSTOMER_ROOT_CO_NAME, 
                      dbo.A_QUOTES_HISTORY.OBJECT_ID AS QUOTE_ID, dbo.A_QUOTES_HISTORY.SUPPLIER_ID, H.PRICE, 
                      A_V_COMPANIES_APPROVED_DATA_2.NAME AS SUPPLIER_NAME
FROM         dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_2 INNER JOIN
                      dbo.A_QUOTES_HISTORY ON A_V_COMPANIES_APPROVED_DATA_2.ID = dbo.A_QUOTES_HISTORY.SUPPLIER_ID RIGHT OUTER JOIN
                      dbo.A_ORDERS_HISTORY H INNER JOIN
                      dbo.A_OBJECTS O ON H.OBJECT_ID = O.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON H.CUSTOMER_CO = dbo.A_V_COMPANIES_APPROVED_DATA.ID ON 
                      dbo.A_QUOTES_HISTORY.ORDER_ID = H.OBJECT_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      dbo.A_V_COMPANIES_APPROVED_DATA.ROOT_CO = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON H.CUSTOMER_PERSON = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_ORDERS_LOOK_UP_FOR_ACCOUNT]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDERS_LOOK_UP_FOR_ACCOUNT]
AS
SELECT     '(' + o.CUSTOMER_ROOT_CO_NAME + ') ' + o.DESCRIPTION + ' [supplier: ' + supplier.NAME + ']' AS NAME, o.OBJECT_ID AS ORDER_ID, 
                      o.CUSTOMER_CO, o.SUPPLIER_ID, o.PRICE, o.CUSTOMER_ROOT_CO_NAME, supplier.NAME AS SUPPLIER_NAME, supplier.ROOT_CO_ID, 
                      o.PROGRESS, o.EXPIRATION_DATE
FROM         dbo.A_O_ORDERS o INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supplier ON o.SUPPLIER_ID = supplier.ID
WHERE     (o.PROGRESS = 'ALL_QUOTES_ACCEPTED')
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_PURCHASABLE_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACCOUNTS_PURCHASABLE_ORDERS]
AS
SELECT     dbo.A_ACCOUNT_PURCHASABLE_ORDERS_LINK.ACCOUNT_ID AS ACCOUNT_OBJECT_ID, 
                      dbo.A_ACCOUNT_PURCHASABLE_ORDERS_LINK.ORDER_ID, dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT.NAME, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.ID AS ACCT_ID, dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT.PROGRESS, 
                      dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT.EXPIRATION_DATE
FROM         dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT INNER JOIN
                      dbo.A_ACCOUNT_PURCHASABLE_ORDERS_LINK ON 
                      dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT.ORDER_ID = dbo.A_ACCOUNT_PURCHASABLE_ORDERS_LINK.ORDER_ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ON 
                      dbo.A_ACCOUNT_PURCHASABLE_ORDERS_LINK.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA.OBJECT_ID
WHERE     (dbo.A_V_ORDERS_LOOK_UP_FOR_ACCOUNT.PROGRESS = 'ALL_QUOTES_ACCEPTED')
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_FORECAST_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_FORECAST_DATA]
AS
SELECT     dbo.A_FORECAST_ITEMS.ACCOUNT_ID, dbo.A_FORECAST_ITEMS.ID AS FORECAST_ITEM_ID, 
                      dbo.A_FORECASTS_HISTORY.ID AS FORECAST_HIST_ID, dbo.A_FORECASTS_HISTORY.START_MONTH, dbo.A_FORECASTS_HISTORY.START_YEAR, 
                      dbo.A_FORECASTS_HISTORY.STOP_MONTH, dbo.A_FORECASTS_HISTORY.STOP_YEAR, dbo.A_FORECASTS_HISTORY.START_DATE, 
                      dbo.A_FORECASTS_HISTORY.STOP_DATE, dbo.A_FORECAST_ITEMS.F_TYPE AS FI_TYPE, dbo.A_FORECAST_ITEMS.QTY, 
                      dbo.A_FORECAST_ITEMS.F_AMT, dbo.A_FORECAST_ITEMS.PERCENT_OF_REV, dbo.A_FORECAST_ITEMS.PROGRESS, 
                      dbo.A_FORECAST_ITEMS.AMT_INVOICED, dbo.A_V_ACCOUNTS_APPROVED_DATA.NAME, dbo.A_V_ACCOUNTS_APPROVED_DATA.SUPPLIER_CO, 
                      dbo.A_V_ACCOUNTS_APPROVED_DATA.CUSTOMER_CO, dbo.A_V_ACCOUNTS_APPROVED_DATA.PRODUCT_ID
FROM         dbo.A_FORECASTS_HISTORY INNER JOIN
                      dbo.A_FORECAST_ITEMS ON dbo.A_FORECASTS_HISTORY.ID = dbo.A_FORECAST_ITEMS.FORECAST_ID INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA ON dbo.A_FORECAST_ITEMS.ACCOUNT_ID = dbo.A_V_ACCOUNTS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA]
AS
SELECT     dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS PURCHASER_NAME, dbo.A_V_PURCHASES_APPROVED_DATA.CUST_PURCH_NUM, 
                      dbo.A_V_PURCHASES_APPROVED_DATA.SUP_PURCH_NUM, dbo.A_V_PURCHASES_APPROVED_DATA.DATE_CREATED, 
                      dbo.A_V_PURCHASES_APPROVED_DATA.PURCHASE_STATUS, I.ID, I.INVOICE_ID, I.ACCOUNT_ID, I.PURCH_ITEM_ID, I.STATUS, I.DRCM, I.MODBY, 
                      I.AMOUNT, I.DESCRIPTION, I.DATE_INVOICED, I.AMT_PAID, I.LATE_FEES, I.TOTAL, I.PURCHASER_ID, I.DISPUTED_LINK, I.COMMENTS, 
                      I.PURCHASE_ID, I.ITEM_TYPE, I.CUSTOMER_CO, I.SUPPLIER_CO, I.QUOTE_ID, I.DATE_POSTED, dbo.A_ORDER_ITEMS.CUST_LINE_ITEM, I.FILL_ID, 
                      I.FAILED_MONITOR, CUSTOMER.NAME AS CUSTOMER_NAME, SUPPLIER.NAME AS SUPPLIER_NAME, I.TAX, I.TAX_RATE, I.QTY, I.UNIT_PRICE
FROM         dbo.A_ACCOUNT_INVOICE_ITEMS I LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK CUSTOMER ON I.CUSTOMER_CO = CUSTOMER.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK SUPPLIER ON I.SUPPLIER_CO = SUPPLIER.ID LEFT OUTER JOIN
                      dbo.A_ORDER_ITEMS ON I.PURCH_ITEM_ID = dbo.A_ORDER_ITEMS.ID LEFT OUTER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA ON I.PURCHASE_ID = dbo.A_V_PURCHASES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON I.PURCHASER_ID = dbo.A_V_PEOPLE_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP]
AS
SELECT DISTINCT 
                      purch.ID AS PURCHASE_ID, toi.PURCHASE_HIST_ID, toi.PURCHASE_ITEM_ID, customer.NAME AS CUSTOMER_NAME, purchItem.DUE_DATE, 
                      purchItem.ORIG_DUE_DATE, t.PROCEDURE_ID AS PROC_ID, customer.ID AS CUST_ID, 
                      dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS START_DATE, t.STATUS, 
                      t.REQUESTEE_ID, t.GROUP_REQUESTEE_ID, Product.NAME AS PRODUCT_NAME, t.ID, purch.CUST_PURCH_NUM, purchItem.ACCOUNT_ID, 
                      Account.REFERENCE_PO, [PROC].NAME AS PROC_NAME, purchItem.QTY, dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME, 
                      dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL, dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ACTUAL_PART_ID, t.CUR_PLANNED_START_DATE AS ST_DATE, 
                      t.ACTUAL_STOP_DATE, t.ACTUAL_START_DATE, purchItem.MT_NUM, toi.FILL_ITEM_ID, purch.DATE_CREATED, dbo.A_FILLS.BATCH_PARENT, 
                      dbo.A_FILLS.BATCHED, dbo.A_FILLS.BATCH_FILL, dbo.A_FILLS.ID AS FILL_ID, dbo.A_FILLS.FILL_QTY, 
                      dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE, dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE, 
                      dbo.A_TASK_COMPLETION_STATS.TOTAL_TIME, dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS, 
                      dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS, dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS, 
                      dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE,
                      (CASE 
                      WHEN PATINDEX('%<<li/>>%',A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )>0 THEN SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT,1 ,PATINDEX('%<<li/>>%',A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )+7)
                      WHEN PATINDEX('%<</bb>>%',A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )>0 THEN SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT,1 ,PATINDEX('%<</bb>>%',A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )+7)
                      ELSE SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT, 1, CHARINDEX(' ', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT ))
                      END) AS CUR_STEP_TEXT ,
                      --SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT,1 ,PATINDEX('%<</bb>>%',A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )+7) AS CUR_STEP_TEXT ,
                      dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ACT_PART_OBJ_ID, t.HAS_FILE
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK customer INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA purch ON customer.ID = purch.CUSTOMER_CO RIGHT OUTER JOIN
                      dbo.A_TASK_COMPLETION_STATS RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK T_OBJ INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK [PROC] INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_TASKS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID INNER JOIN
                      dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA Product INNER JOIN
                      dbo.A_ORDER_ITEMS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK Account ON purchItem.ACCOUNT_ID = Account.ID ON 
                      purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASE_ITEMS_WITH_ACCOUNT_SUPPLIER]
AS
SELECT     i.ID AS PURCHASE_ITEM_ID, dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.NAME AS SUPPLIER_NAME, a.SUPPLIER_CO AS SUPPLIER_ID
FROM         dbo.A_ORDER_ITEMS i INNER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK a ON i.ACCOUNT_ID = a.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK ON a.SUPPLIER_CO = dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.ID
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified]
AS
SELECT DISTINCT 
purch.ID AS PURCHASE_ID,
toi.PURCHASE_HIST_ID,
toi.PURCHASE_ITEM_ID,
customer.NAME AS CUSTOMER_NAME,
ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) AS DUE_DATE,
purchItem.ORIG_DUE_DATE,
t.PROCEDURE_ID AS PROC_ID,
customer.ID AS CUST_ID, 
dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS START_DATE,
t.STATUS, 
t.REQUESTEE_ID,
t.GROUP_REQUESTEE_ID,
Product.NAME AS PRODUCT_NAME,
t.ID,
purch.CUST_PURCH_NUM,
purchItem.ACCOUNT_ID,
Account.REFERENCE_PO, 
[PROC].NAME AS PROC_NAME,
purchItem.QTY,
dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME,
dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL, 
dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ACTUAL_PART_ID,
t.CUR_PLANNED_START_DATE AS ST_DATE, ISNULL(t.ACTUAL_STOP_DATE, purchItem.DUE_DATE) 
AS ACTUAL_STOP_DATE,
t.ACTUAL_START_DATE,
purchItem.MT_NUM, 
toi.FILL_ITEM_ID,
purch.DATE_CREATED, 
dbo.A_FILLS.BATCH_PARENT, 
dbo.A_FILLS.BATCHED,
dbo.A_FILLS.BATCH_FILL,
dbo.A_FILLS.ID AS FILL_ID,
dbo.A_FILLS.FILL_QTY,
dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE, 
dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE, 
dbo.A_TASK_COMPLETION_STATS.TOTAL_TIME,
dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS, 
dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS,
 dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS, 
dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE, 
(CASE WHEN CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT) > 0 THEN SUBSTRING(A_TASK_COMPLETION_STATS.CUR_STEP_TEXT, 0, CHARINDEX('<<nl/>>', A_TASK_COMPLETION_STATS.CUR_STEP_TEXT )) 
ELSE A_TASK_COMPLETION_STATS.CUR_STEP_TEXT END) AS CUR_STEP_TEXT, dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ACT_PART_OBJ_ID, t.HAS_FILE,
supp.ID as SUPPLIER_ID,
supp.NAME AS SUPPLIER_NAME
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK AS customer INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA AS purch ON customer.ID = purch.CUSTOMER_CO RIGHT OUTER JOIN
                      dbo.A_TASK_COMPLETION_STATS RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK AS T_OBJ INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK AS [PROC] INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION AS toi INNER JOIN
                      dbo.A_TASKS AS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID INNER JOIN
                      dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA AS Product INNER JOIN
                      dbo.A_ORDER_ITEMS AS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK AS Account ON purchItem.ACCOUNT_ID = Account.ID ON 
                      purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
					  RIGHT JOIN dbo.A_V_COMPANIES_APPROVED_DATA_QUICK supp on supp.ID = Account.SUPPLIER_CO
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)
GO

/****** Object:  View [dbo].[A_V_INVOICE_ITEMS_WITH_SUPPLIER_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_INVOICE_ITEMS_WITH_SUPPLIER_DATA]
AS
SELECT     dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID, dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID, dbo.A_ACCOUNT_INVOICE_ITEMS.ID, 
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.NAME AS SUP_NAME, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_1, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ADDRESS_2, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.CITY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.STATE, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.COUNTRY, 
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.POSTAL_CODE, dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.INTERNAL_ADDRESS, 
                      dbo.A_DOCUMENT_LINK.LINKED_DOC_ID
FROM         dbo.A_ACCOUNT_INVOICE_ITEMS INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_ACCOUNT_INVOICE_ITEMS.PURCH_ITEM_ID = dbo.A_ORDER_ITEMS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON dbo.A_ORDER_ITEMS.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.ID INNER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK ON 
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.LOCATION = dbo.A_V_LOCATIONS_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_DOCUMENT_LINK ON dbo.A_V_COMPANIES_APPROVED_DATA_QUICK.OBJECT_ID = dbo.A_DOCUMENT_LINK.OBJECT_ID
WHERE     (dbo.A_DOCUMENT_LINK.TYPE = N'LOGO')
GO

/****** Object:  View [dbo].[A_V_ENGINEER_SCREEN_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ENGINEER_SCREEN_DATA]
AS
SELECT DISTINCT 
                      purch.ID AS PURCHASE_ID, toi.PURCHASE_HIST_ID, toi.PURCHASE_ITEM_ID, customer.NAME AS CUSTOMER_NAME, purchItem.DUE_DATE, 
                      purchItem.ORIG_DUE_DATE, t.PROCEDURE_ID AS PROC_ID, customer.ID AS CUST_ID, 
                      dbo.A_FN_DATE_TIME_ADD_USING_UNITS(purchItem.PROD_TIME_UNIT, purchItem.DUE_DATE, - purchItem.PROD_TIME) AS START_DATE, t.STATUS, 
                      t.REQUESTEE_ID, t.GROUP_REQUESTEE_ID, Product.NAME AS PRODUCT_NAME, t.ID, purch.CUST_PURCH_NUM, purchItem.ACCOUNT_ID, 
                      Account.REFERENCE_PO, [PROC].NAME AS PROC_NAME, purchItem.QTY, dbo.A_V_ACTUAL_PARTS_QUICK.NICK_NAME, 
                      dbo.A_V_ACTUAL_PARTS_QUICK.SERIAL, dbo.A_V_ACTUAL_PARTS_QUICK.ID AS ACTUAL_PART_ID, t.CUR_PLANNED_START_DATE AS ST_DATE, 
                      t.ACTUAL_STOP_DATE, t.ACTUAL_START_DATE, purchItem.MT_NUM, toi.FILL_ITEM_ID, purch.DATE_CREATED, dbo.A_FILLS.BATCH_PARENT, 
                      dbo.A_FILLS.BATCHED, dbo.A_FILLS.BATCH_FILL, dbo.A_FILLS.ID AS FILL_ID, dbo.A_FILLS.FILL_QTY, 
                      dbo.A_TASK_COMPLETION_STATS.PERC_COMPLETE, dbo.A_TASK_COMPLETION_STATS.TIME_COMPLETE, 
                      dbo.A_TASK_COMPLETION_STATS.TOTAL_TIME, dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS, 
                      dbo.A_TASK_COMPLETION_STATS.MY_TOT_HOURS, dbo.A_TASK_COMPLETION_STATS.MY_COMP_HOURS, 
                      dbo.A_TASK_COMPLETION_STATS.NUM_SUB_TASKS_COMPLETE, dbo.A_TASK_COMPLETION_STATS.CUR_STEP_TEXT, 
                      dbo.A_V_ACTUAL_PARTS_QUICK.OBJECT_ID AS ACT_PART_OBJ_ID, t.HAS_FILE
FROM         dbo.A_V_COMPANIES_APPROVED_DATA_QUICK customer INNER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA purch ON customer.ID = purch.CUSTOMER_CO RIGHT OUTER JOIN
                      dbo.A_TASK_COMPLETION_STATS RIGHT OUTER JOIN
                      dbo.A_TASK_OBJECT_LINK T_OBJ INNER JOIN
                      dbo.A_V_PROCEDURES_DATA_QUICK [PROC] INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION toi INNER JOIN
                      dbo.A_TASKS t ON toi.TASK_ID = t.ID ON [PROC].ID = t.PROCEDURE_ID ON T_OBJ.TASK_ID = t.ID INNER JOIN
                      dbo.A_FILLS ON toi.FILL_ITEM_ID = dbo.A_FILLS.ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA Product INNER JOIN
                      dbo.A_ORDER_ITEMS purchItem ON Product.ID = purchItem.PRODUCT_ID ON dbo.A_FILLS.PURCH_ITEM_ID = purchItem.ID ON 
                      dbo.A_TASK_COMPLETION_STATS.TASK_ID = t.ID LEFT OUTER JOIN
                      dbo.A_V_ACCOUNTS_APPROVED_DATA_QUICK Account ON purchItem.ACCOUNT_ID = Account.ID ON 
                      purch.HISTORY_REF_ID = toi.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_V_ACTUAL_PARTS_QUICK ON T_OBJ.OBJECT_ID = dbo.A_V_ACTUAL_PARTS_QUICK.ID
WHERE     (t.STATUS IN ('REQUESTED', 'ACCEPTED', 'CLOSED', 'FINISHED')) AND (toi.PURCHASE_ITEM_ID IS NOT NULL)
GO

/****** Object:  View [dbo].[A_O_PARTS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PARTS_HISTORY]
AS
SELECT     o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, PH.OBJECT_ID AS OBJ_ID, PH.ID, PH.UNIT, PH.NAME, PH.PART_TYPE, 
                      PH.TRACK_FROM_START, PH.COMPANY, PH.UNIT_SHIPPING_WEIGHT, PH.COMPANY_PART_NUMBER, PH.SUPPLIER_SEE_INSTALL_BASE, 
                      PH.SUPPLIER_SEE_AVAILABILITY, PH.CUSTOMER_SEE_AVAILABILITY, c.NAME AS COMPANY_NAME, PT.NAME AS PART_TYPE_NAME, PH.SPARE, 
                      PH.CONSUMABLE, PH.WEIGHT_TYPE, PARENT_CO.NAME AS ROOT_CO_NAME, dbo.A_UNIT_TYPES.NAME AS WEIGHT_TYPE_NAME
FROM         dbo.A_PARTS_HISTORY PH INNER JOIN
                      dbo.A_OBJECTS o ON PH.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK c ON PH.COMPANY = c.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA_QUICK PARENT_CO ON c.PARENT = PARENT_CO.ID LEFT OUTER JOIN
                      dbo.A_UNIT_TYPES ON PH.WEIGHT_TYPE = dbo.A_UNIT_TYPES.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_PART_TYPES PT ON PH.PART_TYPE = PT.ROOT
GO

/****** Object:  View [dbo].[A_V_FILLS_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_FILLS_SEARCH]
AS
SELECT     CUST.NAME AS CUST_NAME, SUP.NAME AS SUP_NAME, FILL_OBJ.OBJ_DESC AS FILL_OBJ_DESC, FILL_PERSON.FULL_NAME AS FILLER_NAME, 
                      PURCHASE_ITEM.PRODUCT_ID AS PROD_ID, dbo.A_PURCHASES_HISTORY.PURCHASER AS PURCHASER_ID, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PROD_NAME, dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID AS PROC_ID, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.SYSTEM_ID AS SYS_PROC_ID, A_V_APPROVED_OBJECTS_1.OBJ_DESC AS APP_OBJ_DESC, 
                      PURCHASE_ITEM.ADD_COST_ID AS PRICING_TABLE_ID, dbo.A_QUOTES_HISTORY.CUSTOMER_CO AS CUSTOMER, 
                      dbo.A_QUOTES_HISTORY.SUPPLIER_ID AS SUPPLIER, PURCHASE_ITEM.QTY AS PURCHASE_QTY, PURCHASE_ITEM.UNIT_PRICE, 
                      PURCHASE_ITEM.TOTAL_PRICE, PURCHASE_ITEM.DEST, PURCHASE_ITEM.FROM_LOC, PURCHASE_ITEM.TO_LOC, 
                      PURCHASE_ITEM.PURCHASE_HIST_ID, PURCHASE_ITEM.ACCOUNT_ID, PURCHASE_ITEM.TOTAL_QTY AS TOT_QTY, PURCHASE_ITEM.PARENT_QTY, 
                      PURCHASE_ITEM.EST_WEIGHT AS WEIGHT, PURCHASE_ITEM.EST_WEIGHT_UNIT AS WEIGHT_UNIT, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.APP_OBJECT AS OBJ_PROD_APPLIES_TO, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.HISTORY_REF_ID AS PROCEDURE_HIST_ID, dbo.A_V_PROCEDURES_APPROVED_DATA.STEPS_IN_AP, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROC_NAME, dbo.A_V_PRODUCTS_APPROVED_DATA.HISTORY_REF_ID AS PROD_HIST_ID, 
                      dbo.A_QUOTES_HISTORY.CUSTOMER_PERSON, dbo.A_PURCHASES_HISTORY.OBJECT_ID AS PURCHASE_ID, FILLS.ID, FILLS.PURCH_ITEM_ID, 
                      FILLS.FILL_BY, FILLS.FILL_OBJ_ID, FILLS.FILL_QTY, FILLS.FILLER, FILLS.TASK_ID, PURCHASE_ITEM.QTY_FILLED, 
                      PURCHASE_ITEM.QTY_NEEDS_FILLING, FILLS.SUB_FILL_FOR, FILLS.DRCM AS FILL_DATE, 
                      PURCHASE_ITEM.PARENT AS PURCHASE_ITEM_PARENT_ID, PURCHASE_ITEM.PROD_PRICE_LIST, PURCHASE_ITEM.CUST_LINE_ITEM, 
                      FILLS.BATCH_FILL, FILLS.BATCH_PARENT,
					  dbo.A_V_PROCEDURES_APPROVED_DATA.OBJECT_ID as ProcObjId,
					  dbo.A_V_PROCEDURES_APPROVED_DATA.REV
FROM         dbo.A_V_PRODUCTS_APPROVED_DATA LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS A_V_APPROVED_OBJECTS_1 ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.APP_OBJECT = A_V_APPROVED_OBJECTS_1.ID RIGHT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS FILL_OBJ RIGHT OUTER JOIN
                      dbo.A_FILLS FILLS LEFT OUTER JOIN
                      dbo.A_PURCHASES_HISTORY INNER JOIN
                      dbo.A_QUOTES_HISTORY INNER JOIN
                      dbo.A_ORDER_ITEMS PURCHASE_ITEM ON dbo.A_QUOTES_HISTORY.ID = PURCHASE_ITEM.QUOTE_ID ON 
                      dbo.A_PURCHASES_HISTORY.ID = PURCHASE_ITEM.PURCHASE_HIST_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUST ON dbo.A_QUOTES_HISTORY.CUSTOMER_CO = CUST.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUP ON dbo.A_QUOTES_HISTORY.SUPPLIER_ID = SUP.ID ON 
                      FILLS.PURCH_ITEM_ID = PURCHASE_ITEM.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA FILL_PERSON ON FILLS.FILLER = FILL_PERSON.ID ON FILL_OBJ.ID = FILLS.FILL_OBJ_ID ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.ID = PURCHASE_ITEM.PRODUCT_ID
GO

/****** Object:  View [dbo].[Portal_FileSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[Portal_FileSearchView]

AS
SELECT 
Id,
SUP_NAME AS SupName,
FILL_OBJ_DESC As FillObjDesc,
PURCH_ITEM_ID AS PurchItemId
FROM A_V_FILLS_SEARCH
GO

/****** Object:  View [dbo].[A_O_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[A_O_ROLES]
AS
SELECT     r.ID, r.NAME AS ROLE_NAME, r.OBJECT_ID AS OBJ_ID, o.STATUS, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, 
                      o.REV_INFO, o.CREATING_CO, o.REV, o.WFS_ID, r.HIDDEN, r.SOURCE, r.OBJECT_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, 
                      o.APPROVAL_ACTIVITY, r.SECURITY_LEVEL, r.IS_ADMIN, r.COMMENTS, r.TRAININGIDREV, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_LEVEL_NAME
FROM         dbo.A_ROLES_HISTORY r INNER JOIN
                      dbo.A_OBJECTS o ON r.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_SECURITY_LEVELS ON r.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID
GO

/****** Object:  View [dbo].[A_V_ROLES_WITH_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[A_V_ROLES_WITH_ASSIGNEES]
AS
SELECT     r.ROLE_NAME, r.STATUS, r.ID AS ROLE_ID, r.COMMENTS, r.TRAININGIDREV, p.ID AS PERSON_ID, p.FULL_NAME AS MEMBER_NAME, ra.STATUS AS RA_STATUS, 
                      p.SYSTEM_STATUS AS PERSON_STATUS, r.CREATING_CO, r.HIDDEN, ra.ROLE AS ID, r.OBJ_ID, r.LOCKED_BY, r.UNLOCKED_BY, r.CREATED_BY, 
                      r.CREATE_DATE, r.ROOT, r.REV_INFO, r.SOURCE, r.LOCKED_BY_NAME, r.CREATING_CO_NAME, r.REV, r.WFS_ID, r.SECURITY_LEVEL, r.IS_ADMIN, 
                      r.SECURITY_LEVEL_NAME
FROM         dbo.A_APPROVED_PEOPLE p RIGHT OUTER JOIN
                      dbo.A_ROLE_ASSIGNEE ra ON p.ID = ra.PERSON RIGHT OUTER JOIN
                      dbo.A_O_ROLES r ON ra.ROLE = r.ID
GO

/****** Object:  View [dbo].[Portal_RolesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Portal_RolesView]
AS
SELECT DISTINCT 
                         SECURITY_LEVEL AS SecurityLevel, 
						 SECURITY_LEVEL_NAME AS SecurityLevelName, 
						 ROOT AS Root, 
						 ID AS Id, 
						 ROLE_ID,
						 ROLE_NAME AS RoleName, 
						 COMMENTS as Comments,
						 TRAININGIDREV as TrainingIdRev,
						 OBJ_ID AS ObjectId, 
						 STATUS AS Status, 
						 LOCKED_BY AS LockedBy, 
						 UNLOCKED_BY AS UnLockedBy, 
						 CREATED_BY AS CreatedBy, 
						 CREATING_CO AS CreatingCo, 
						 REV AS Revision, 
						 WFS_ID AS WFSID, 
						 HIDDEN AS Hidden, 
						 SOURCE AS Source, 
                         LOCKED_BY_NAME AS LockedByName
FROM            dbo.A_V_ROLES_WITH_ASSIGNEES
GO

/****** Object:  View [dbo].[Portal_HelpView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

 CREATE view [dbo].[Portal_HelpView]
AS
select
Ph.id,
Ph.title,
Ph.FriendlyUrl,
Ph.Content,
ph.roles,
(
select distinct +','+PR.RoleName
from Portal_RolesView PR
where ','+Ph.Roles+',' like '%,'+PR.Root+'%'
for xml path(''), type
).value('substring(text()[1], 2)', 'varchar(max)') as RoleName
FROM Portal_HelpPage Ph;
GO

/****** Object:  View [dbo].[Portal_TrainingView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_TrainingView]
AS
SELECT        RA.ID, RA.StartDate, RA.EndDate, PST.FULL_NAME AS FullName, rv.RoleName AS PositionName, rv.TrainingIdRev, CASE WHEN RA.EndDate >= CAST(GetDate() AS DATE) THEN 'Active' WHEN RA.EndDate <= CAST(GetDate() 
                         AS DATE) THEN 'Deactive' ELSE 'NA' END AS Status
FROM            dbo.A_ROLE_ASSIGNEE AS RA INNER JOIN
                         dbo.A_PEOPLE_SEARCH_TABLE AS PST ON RA.PERSON = PST.OBJ_ID INNER JOIN
                         dbo.Portal_RolesView AS rv ON rv.Id = RA.ROLE
WHERE        (RA.STATUS = 'ACTIVE') AND (RA.EndDate IS NOT NULL) AND (RA.StartDate IS NOT NULL)
GO

/****** Object:  View [dbo].[A_V_PART_DATA_BY_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PART_DATA_BY_APPROVED_DATA]
AS
SELECT     dbo.A_PARTS.ID, dbo.A_PARTS.PARTS_HISTORY_ID, dbo.A_PARTS_HISTORY.UNIT, dbo.A_PARTS_HISTORY.COMPANY_PART_NUMBER, 
                      dbo.A_PARTS_HISTORY.NAME, dbo.A_PARTS_HISTORY.PART_TYPE, dbo.A_PARTS_HISTORY.SPARE, dbo.A_PARTS_HISTORY.CONSUMABLE, 
                      dbo.A_PARTS_HISTORY.TRACK_FROM_START, dbo.A_PARTS_HISTORY.DRCM, dbo.A_PARTS_HISTORY.MODBY, dbo.A_PARTS_HISTORY.COMPANY, 
                      dbo.A_PARTS_HISTORY.OBJECT_ID, dbo.A_PARTS_HISTORY.PART_TYPE_NAME, dbo.A_PARTS_HISTORY.UNIT_SHIPPING_WEIGHT, 
                      dbo.A_OBJECTS.CREATING_CO, dbo.A_PARTS_HISTORY.WEIGHT_TYPE, dbo.A_PARTS_HISTORY.CUSTOMER_SEE_AVAILABILITY, 
                      dbo.A_PARTS_HISTORY.SUPPLIER_SEE_AVAILABILITY, dbo.A_PARTS_HISTORY.SUPPLIER_SEE_INSTALL_BASE, 
                      A_V_COMPANIES_APPROVED_DATA_1.NAME AS ROOT_CO_NAME, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS COMPANY_NAME, 
                      dbo.A_UNIT_TYPES.NAME AS WT_TYPE_NAME, dbo.A_OBJECTS.STATUS
FROM         dbo.A_PARTS INNER JOIN
                      dbo.A_PARTS_HISTORY ON dbo.A_PARTS.PARTS_HISTORY_ID = dbo.A_PARTS_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_PARTS_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PARTS_HISTORY.COMPANY = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      dbo.A_V_COMPANIES_APPROVED_DATA.ROOT_CO = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_UNIT_TYPES ON dbo.A_PARTS_HISTORY.WEIGHT_TYPE = dbo.A_UNIT_TYPES.ID
GO

/****** Object:  View [dbo].[Portal_PartsApprovedView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PartsApprovedView]
AS
SELECT        ID AS Id, PARTS_HISTORY_ID AS PartsHistoryId, UNIT AS Unit, COMPANY_PART_NUMBER AS ComapnyPartNumber, NAME AS Name, PART_TYPE AS PartType, SPARE AS Spare, CONSUMABLE AS Consumable, 
                         TRACK_FROM_START AS TrackFromStart, DRCM AS Drcm, MODBY AS ModBy, COMPANY AS Comapny, OBJECT_ID AS ObjectId, PART_TYPE_NAME AS PartTypeName, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, 
                         CREATING_CO AS CreatingCo, WEIGHT_TYPE AS WeightType, CUSTOMER_SEE_AVAILABILITY AS CustomerSeeAvailability, SUPPLIER_SEE_AVAILABILITY AS SupplierSeeAvailability, 
                         SUPPLIER_SEE_INSTALL_BASE AS SupplierSeeInstallBase, ROOT_CO_NAME AS RootCoName, COMPANY_NAME AS ComapnyName, WT_TYPE_NAME AS WtTypeName, STATUS AS Status, OBJECT_ID AS ObjId
FROM            dbo.A_V_PART_DATA_BY_APPROVED_DATA
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_PARTS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_Z_FAVORITES_PARTS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_V_PART_DATA_BY_APPROVED_DATA.NAME
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_V_PART_DATA_BY_APPROVED_DATA ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_V_PART_DATA_BY_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_O_EQUIP_EXP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE  VIEW [dbo].[A_O_EQUIP_EXP]
AS
SELECT     	e.ID,
			e.OBJECT_ID, 
			e.PERSON_ID, 
			e.PART_ID, 
			e.FIRST_EXPOSURE_DATE, 
			e.LAST_EXPOSURE_DATE, 
			e.HW_INSTALL_EXP_LEVEL, 'EE_LEVEL_' + convert(nvarchar(50),e.HW_INSTALL_EXP_LEVEL) AS HW_INSTALL_EXP_LEVEL_SHOW, 
            e.PROCESS_SETUP_EXP_LEVEL, 'EE_LEVEL_' + convert(nvarchar(50),e.PROCESS_SETUP_EXP_LEVEL) AS PROCESS_SETUP_EXP_LEVEL_SHOW,
			e.OPERATION_EXP_LEVEL, 'EE_LEVEL_' + convert(nvarchar(50),e.OPERATION_EXP_LEVEL) AS OPERATION_EXP_LEVEL_SHOW, 
			e.SM_EXP_LEVEL, 'EE_LEVEL_' + convert(nvarchar(50),e.SM_EXP_LEVEL) AS SM_EXP_LEVEL_SHOW, 
			e.UM_EXP_LEVEL, 'EE_LEVEL_' + convert(nvarchar(50),e.UM_EXP_LEVEL) AS UM_EXP_LEVEL_SHOW, 
			e.FORMALLY_TRAINED,
			e.CERTIFIED,
            o.ID AS OBJ_ID, 
			o.LOCKED_BY, 
			o.UNLOCKED_BY, 
			o.CREATED_BY,
			o.CREATE_DATE, 
			o.ROOT, 
			o.REV_INFO, 
			o.CREATING_CO, 
			o.STATUS, o.REV, 
            o.WFS_ID, 
			o.LOCKED_BY_NAME, 
			o.CREATING_CO_NAME, 
			o.APPROVAL_ACTIVITY, 
			part.NAME AS PART_NAME, 
            e.HW_INSTALL_EXP_LEVEL + e.PROCESS_SETUP_EXP_LEVEL + e.OPERATION_EXP_LEVEL + e.SM_EXP_LEVEL + e.UM_EXP_LEVEL AS OVERALL_EXP_LEVEL,
			dbo.A_FN_EE_GET_OVERALL_SHOW(e.HW_INSTALL_EXP_LEVEL + e.PROCESS_SETUP_EXP_LEVEL + e.OPERATION_EXP_LEVEL + e.SM_EXP_LEVEL + e.UM_EXP_LEVEL) AS OVERALL_EXP_LEVEL_SHOW,
            DATEDIFF(m, e.FIRST_EXPOSURE_DATE, e.LAST_EXPOSURE_DATE) AS CUMULATIVE_EXPOSURE, 
			person.FULL_NAME AS PERSON_NAME
FROM        dbo.A_OBJECTS o INNER JOIN
            dbo.A_EQUIP_EXP_HISTORY e ON o.ID = e.OBJECT_ID LEFT OUTER JOIN
            dbo.A_V_PEOPLE_APPROVED_DATA person ON e.PERSON_ID = person.ID LEFT OUTER JOIN
            dbo.A_V_PART_DATA_BY_APPROVED_DATA part ON e.PART_ID = part.ID
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LOCATIONS_BY_APPROVED_ID]
AS
SELECT     l.ID, l.HISTORY_REF_ID, lh.NAME, lh.PARENT_LOCATION, lh.PARENT_LOCATION_NAME, lh.ADDRESS_1, lh.ADDRESS_2, lh.FULL_ADDRESS, lh.CITY, 
                      lh.STATE, lh.COUNTRY, lh.POSTAL_CODE, lh.REGION, lh.REGION_NAME, lh.INTERNAL_ADDRESS, lh.OBJECT_ID, lh.DRCM, lh.MODBY, 
                      lh.PARENT_PATH, o.CREATING_CO, lh.COMPLETE_NAME
FROM         dbo.A_LOCATIONS l INNER JOIN
                      dbo.A_LOCATIONS_HISTORY lh ON l.HISTORY_REF_ID = lh.ID INNER JOIN
                      dbo.A_OBJECTS o ON lh.OBJECT_ID = o.ID
GO

/****** Object:  View [dbo].[A_V_FILLS_ACTUAL_PART_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_FILLS_ACTUAL_PART_SEARCH]
AS
SELECT     AP.ID, AP.HISTORY_REF_ID, APH.LOCATION, APH.OBJECT_ID, APH.DRCM, APH.MODBY, APH.NICK_NAME, APH.MERGABLE, APH.PARENT_ID, 
                      APH.PART_ID, APH.QTY, APH.SERIAL, APH.CUR_OWNER, APH.ASSEMBLY_WT, APH.AP_STATUS, APH.ROOT_ID, APH.ROOT_STATUS, 
                      part.COMPANY_PART_NUMBER, part.NAME AS PART_DESC, part.PART_TYPE_NAME, loc.NAME AS LOCATION_NAME, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CURRENT_OWNER_NAME, dbo.A_FN_ACTUAL_PART_HAS_CHILD(AP.ID) AS HAS_CHILD, 
                      ISNULL('Actual Part # ' + AP.ID + ', ', '') + ISNULL('(Nick: ' + APH.NICK_NAME + '), ', '') + ISNULL('(S/N:' + APH.SERIAL + '), ', '') 
                      + ISNULL(' ' + part.NAME + '  ', '') + ISNULL('(p/n ' + part.ID + ')  ', '') AS NAME, AP.STATUS, dbo.A_OBJECTS.CREATING_CO, 
                      part.PART_TYPE AS PART_TYPE_ID
FROM         dbo.A_ACTUAL_PARTS AP INNER JOIN
                      dbo.A_ACTUAL_PARTS_HISTORY APH ON AP.HISTORY_REF_ID = APH.ID INNER JOIN
                      dbo.A_OBJECTS ON APH.OBJECT_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON APH.CUR_OWNER = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PART_DATA_BY_APPROVED_DATA part ON APH.PART_ID = part.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID loc ON APH.LOCATION = loc.ID
WHERE     (AP.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[Portal_BuyerView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Portal_BuyerView]
AS

SELECT DISTINCT 
NEWID() AS Id,
t.SUPPLIER_NAME AS SupplierName,
t.SUPPLIER_ID AS SupplierId,
t.ID AS TaskId,
t.NUM_SUB_TASKS AS NumSubTasks,
t.NUM_SUB_TASKS_COMPLETE AS NumSubTasksComplete,
t.MY_TOT_HOURS AS MyTotHours,
t.MY_COMP_HOURS AS MyCompHours,
t.CUR_STEP_TEXT  AS CurStepText,
ISNULL(t.TIME_COMPLETE,0) AS TimeComplete,
t.PERC_COMPLETE AS PercComplete,
t.BATCH_PARENT AS BatchParent,
t.BATCHED AS Batched,
t.BATCH_FILL AS BatchEdFill,
t.FILL_ITEM_ID AS FillItemId,

t.NICK_NAME AS NickName,
t.SERIAL AS Serial,
t.PURCHASE_ID AS PurchaseId,
t.PURCHASE_HIST_ID AS PurchaseHistId,
t.PURCHASE_ITEM_ID PurchaseItemId,
t.CUSTOMER_NAME AS CustomerName,
t.DUE_DATE AS DueDate,
t.ORIG_DUE_DATE AS OrigDueDate,
t.ACTUAL_PART_ID AS ActualPartId,
t.ACT_PART_OBJ_ID AS ActPartObjId,
t.ST_DATE AS StDate,
t.PROC_ID AS ProcId,
t.CUST_ID AS CustId,
t.START_DATE AS StartDate,
t.REQUESTEE_ID AS RequesteeId,
t.GROUP_REQUESTEE_ID AS GroupRequesteeId,
t.PRODUCT_NAME AS ProductName,
t.CUST_PURCH_NUM AS CustPurchNum,
t.REFERENCE_PO AS ReferencePo,
t.PROC_NAME AS ProcName,
t.QTY AS Qty,
t.FILL_QTY AS FillQty,
t.STATUS AS Status,
t.FILL_ID AS FillId,
t.MT_NUM AS MtNum,
t.ACTUAL_START_DATE AS ActualStartDate,
t.ACTUAL_STOP_DATE AS ActualStopDate,
invoice.NEW_ITEMS_AMT AS InvoiceAmount,
invoice.INVOICE_DATE AS InvoiceDate,
invoice.STATUS InvoiceStatus,
invoice.INVOICE_ID AS InvoiceId,
ii.UNIT_PRICE AS Price,
CASE WHEN (          

SELECT count(*)
 FROM A_DOCUMENTS WHERE ID IN
(
SELECT file_id FROM A_V_ACTUAL_PARTS_RELATED_FILES
WHERE 
ACTUAL_PART_ID = t.ACTUAL_PART_ID
AND (FILE_NAME LIKE '%%' OR FILE_NAME is NULL ) 
AND  (FILE_DESCRIPTION LIKE '%%' OR FILE_DESCRIPTION is NULL ) 
AND STATUS = 'ACTIVE'
)) > 0THEN 1 ELSE 0 END 
AS HasFile,
0 AS HasMonitor,
1 AS HasNcr
FROM A_V_ENGINEER_SCREEN_DATA_WIP_ONE_STEP_Simplified t with (noLock) 
LEFT JOIN A_V_INVOICES_WITH_ACCT_INFORMATION invoice ON invoice.PURCHASE_ID = t.PURCHASE_ID
LEFT JOIN A_V_ACCOUNT_INVOICE_ITEMS_ALL_DATA ii ON ii.INVOICE_ID = invoice.INVOICE_ID
--WHERE (CUST_ID = '2' OR (  GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494') 
--OR  REQUESTEE_ID = '110332' OR  exists( 	  SELECT ID FROM A_TASKS ts 	
--  WHERE ts.PARENT_ID = t.ID and       (ts.REQUESTEE_ID = '110332' or ts.GROUP_REQUESTEE_ID IN ('','1502','1490','1498','1506','1510','1494') )	  )  ) )
--  AND (STATUS IN ('REQUESTED','ACCEPTED')) AND  (SERIAL LIKE '%%' OR SERIAL is NULL ) AND  (CUST_PURCH_NUM LIKE '%%' OR CUST_PURCH_NUM is NULL )
--   AND  (PROC_ID LIKE '%%' OR PROC_ID is NULL ) AND  (PRODUCT_NAME LIKE '%%' OR PRODUCT_NAME is NULL ) AND  (PURCHASE_ITEM_ID LIKE '%%' OR PURCHASE_ITEM_ID is NULL ) 
--   ORDER BY ST_DATE
GO

/****** Object:  View [dbo].[A_V_FILES_WITH_SOURCE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_FILES_WITH_SOURCE]
AS
SELECT     DOC.ID, DOC.NAME, DOC.SOURCE_ID, DOC.DESCRIPTION, DOC.ACTIVE, SOURCE.NAME AS SRC_NAME, SOURCE.ID AS SRC_ID, 
                      SOURCE.DESCRIPTION AS SRC_DESCRIPTION, DOC.CREATOR_ID
FROM         dbo.A_DOCUMENTS DOC LEFT OUTER JOIN
                      dbo.A_DOCUMENTS SOURCE ON DOC.SOURCE_ID = SOURCE.ID AND DOC.SOURCE_ID = SOURCE.ID
GO

/****** Object:  View [dbo].[A_V_FILES_SEARCH_BY_SUBORDINATE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_FILES_SEARCH_BY_SUBORDINATE]
AS
SELECT     dbo.A_V_FILES_WITH_SOURCE.ID, dbo.A_V_FILES_WITH_SOURCE.NAME, dbo.A_V_FILES_WITH_SOURCE.DESCRIPTION, 
                      dbo.leadingSpaces(dbo.A_V_FILES_WITH_SOURCE.ID, 50) AS SORT_ID, dbo.A_V_FILES_WITH_SOURCE.SRC_NAME, 
                      dbo.A_PEOPLE_SUB_LOOKUP_TABLE.BOSS, dbo.A_V_FILES_WITH_SOURCE.CREATOR_ID, dbo.A_V_FILES_WITH_SOURCE.SRC_ID
FROM         dbo.A_PEOPLE_SUB_LOOKUP_TABLE RIGHT OUTER JOIN
                      dbo.A_V_FILES_WITH_SOURCE ON dbo.A_PEOPLE_SUB_LOOKUP_TABLE.SUBORDINATE = dbo.A_V_FILES_WITH_SOURCE.CREATOR_ID
GO

/****** Object:  View [dbo].[Portal_FilesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_FilesView]
	AS 
	SELECT DISTINCT 
	ID AS Id, 
	SORT_ID AS SortId, 
	NAME AS Name, 
	DESCRIPTION AS Description, 
	SRC_NAME AS SrcName, 
	SRC_ID AS SrcId, 
	CREATOR_ID AS CreatorId, 
	BOSS AS Boss
FROM            dbo.A_V_FILES_SEARCH_BY_SUBORDINATE
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_ACC_RECEIVABLE_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALLS_ACC_RECEIVABLE_ROLE]
AS
SELECT     co.ID AS CO_ID, co.NAME AS CO_NAME, sc.ID, sc.RECIEVABLE_ROLE AS ROLE_ID, sc.PAYMENT_LOCATION AS LOCATION_ID, 
                      ro.NAME AS ROLE_NAME, l.NAME AS LOCATION_NAME
FROM         dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE sc RIGHT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA co ON sc.COMPANY_ID = co.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA ro ON sc.RECIEVABLE_ROLE = ro.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID l ON sc.PAYMENT_LOCATION = l.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_WEEKLY_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALLS_WEEKLY_DATA]
AS
SELECT     SERVICE_CALL.ACTUAL_START_DATE, SERVICE_CALL.ID, SERVICE_CALL.WORKER_ID, WORKER.NAME AS WORKER_FIRST_NAME, 
                      SERVICE_CALL.MACHINE_NAME, SERVICE_CALL.COMMENTS, SERVICE_CALL.WORK_TYPE, WORK_TYPES.SUPPLIER_ID, 
                      SUPPLIER.NAME AS SUPPLIER_NAME, CUSTOMER.NAME AS CUSTOMER_NAME, WORK_TYPES.CUSTOMER_ID, WORK_TYPES.WORK_TYPE_NAME,
                       WORK_TYPES.APPROVER_ROLE, CUSTOMER_APPROVER_ROLE.NAME AS APPROVER_ROLE_NAME, WORK_TYPES.PAYER_ROLE, 
                      PAYER_ROLE.NAME AS PAYER_ROLE_NAME, WORK_TYPES.HOUR_RATE, WORK_TYPES.SKIP_BOSS, SERVICE_CALL_HOURS.NT_0, 
                      SERVICE_CALL_HOURS.NT_1, SERVICE_CALL_HOURS.NT_2, SERVICE_CALL_HOURS.NT_3, SERVICE_CALL_HOURS.NT_4, 
                      SERVICE_CALL_HOURS.NT_5, SERVICE_CALL_HOURS.NT_6, SERVICE_CALL_HOURS.NORMAL_HOURS, SERVICE_CALL.INVOICE_DATE, 
                      SERVICE_CALL.ORDER_NUMBER, SERVICE_CALL.REASON_TYPE, SERVICE_CALL.REASON, SERVICE_CALL.STATUS, SERVICE_CALL.CHECK_NUM, 
                      SERVICE_CALL.EXPENSES_AMOUNT, SERVICE_CALL.EXPENSES_DESCRIPTION, WORKER.BOSS, BOSS.NAME AS BOSS_NAME, 
                      ACCTS_RECEIVABLE.ROLE_ID AS RECEIVABLE_ROLE, ACCTS_RECEIVABLE.ROLE_NAME AS RECEIVABLE_ROLE_NAME, 
                      WORKER.LAST_NAME AS WORKER_LAST_NAME, WORKER.FULL_NAME AS WORKER_NAME, SERVICE_CALL_HOURS.OT_0, 
                      SERVICE_CALL_HOURS.OT_1, SERVICE_CALL_HOURS.OT_2, SERVICE_CALL_HOURS.OT_3, SERVICE_CALL_HOURS.OT_4, 
                      SERVICE_CALL_HOURS.OT_5, SERVICE_CALL_HOURS.OT_6, SERVICE_CALL_HOURS.OT_HOURS, SERVICE_CALL_HOURS.TOTAL_HOURS
FROM         dbo.A_SERVICE_CALLS_WEEKLY_REPORTS SERVICE_CALL INNER JOIN
                      dbo.A_SERVICE_CALLS_TOTAL_HOURS SERVICE_CALL_HOURS ON SERVICE_CALL.ID = SERVICE_CALL_HOURS.WEEKLY_ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA WORKER ON SERVICE_CALL.WORKER_ID = WORKER.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES WORK_TYPES ON SERVICE_CALL.WORK_TYPE = WORK_TYPES.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA CUSTOMER_APPROVER_ROLE ON 
                      WORK_TYPES.APPROVER_ROLE = CUSTOMER_APPROVER_ROLE.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON WORK_TYPES.SUPPLIER_ID = SUPPLIER.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA PAYER_ROLE ON WORK_TYPES.PAYER_ROLE = PAYER_ROLE.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUSTOMER ON WORK_TYPES.CUSTOMER_ID = CUSTOMER.ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALLS_ACC_RECEIVABLE_ROLE ACCTS_RECEIVABLE ON 
                      WORK_TYPES.SUPPLIER_ID = ACCTS_RECEIVABLE.CO_ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA BOSS ON WORKER.BOSS = BOSS.ID
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID]
AS
SELECT DISTINCT dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID, dbo.A_ORDER_ITEMS.ID, dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID
FROM         dbo.A_ORDER_ITEMS INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON dbo.A_ORDER_ITEMS.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID
WHERE     (NOT (dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID IS NULL))
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEM_WITH_SUP_AND_CUST]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASE_ITEM_WITH_SUP_AND_CUST]
AS
SELECT     dbo.A_PURCHASES_HISTORY.PURCHASING_CO AS CUST_ID, dbo.A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID.SUPPLIER_ID, 
                      dbo.A_ORDER_ITEMS.ID, dbo.A_ORDER_ITEMS.ACCOUNT_ID
FROM         dbo.A_ORDER_ITEMS INNER JOIN
                      dbo.A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID ON dbo.A_ORDER_ITEMS.ID = dbo.A_V_PURCHASE_ITEMS_WITH_SUPPLIER_ID.ID INNER JOIN
                      dbo.A_PURCHASES_HISTORY ON dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID = dbo.A_PURCHASES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_O_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_O_COMPANIES]
AS
SELECT     o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, dbo.A_FN_COMPANY_HAS_CHILD(o.ROOT) AS CHILDREN_COUNT, 
                      dbo.A_FN_COMPANY_GET_TOP_COMPANY(c.ID) AS TOP_COMPANY, dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT(c.OBJECT_ID, 'LOGO') 
                      AS picRecord, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS ROOT_CO_NAME, c.ID, c.NAME, c.CO_TYPE, c.PARENT, c.PHONE, c.LOCATION, 
                      c.LOCATION_NAME, c.DRCM, c.MODBY, c.OBJECT_ID, c.ROOT_CO_ID, A_V_COMPANIES_APPROVED_DATA_1.NAME AS PARENT_NAME, 
                      dbo.A_COMPANIES_IMPORT.EXTERNAL_ID
FROM         dbo.A_COMPANIES_HISTORY c INNER JOIN
                      dbo.A_OBJECTS o ON c.OBJECT_ID = o.ID LEFT OUTER JOIN
                      dbo.A_COMPANIES_IMPORT ON o.ROOT = dbo.A_COMPANIES_IMPORT.ROOT_ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      c.PARENT = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON c.ROOT_CO_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[Portal_CompanyView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_CompanyView]
AS

SELECT        
ID AS Id, 
EXTERNAL_ID AS ExternalId, 
NAME AS Name, 
CO_TYPE AS CoType, 
OBJECT_ID AS ObjectId, 
STATUS AS Status, 
LOCKED_BY AS LockedBy, 
UNLOCKED_BY AS UnlockedBy, 
CREATED_BY AS CreatedBy, 
CREATING_CO AS CreatingCo, 
REV AS Rev, 
WFS_ID AS WfsID, 
LOCKED_BY_NAME AS LockedByName, 
ROOT AS Root, 
CHILDREN_COUNT AS ChildrenCount, 
TOP_COMPANY AS TopCompany, 
picRecord AS PicRecord, 
ROOT_CO_NAME AS RootCoName, 
PARENT_NAME AS ParentName,
PARENT as Parent,
PHONE AS Phone,
LOCATION AS Location,
LOCATION_NAME AS LocationName,
DRCM,
isnull(STUFF((
	SELECT +','+ DL.NAME+'|'+DL.LINKED_DOC_ID 
	FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
	WHERE DL.OBJECT_ID = A_V_COMPANIES_APPROVED_DATA.OBJECT_ID
    FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles
FROM dbo.A_O_COMPANIES A_V_COMPANIES_APPROVED_DATA
GO

/****** Object:  View [dbo].[A_O_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PEOPLE]
AS
SELECT     o.OBJ_ID AS OBJ_ID, o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, 
                      o.CREATE_DATE AS CREATE_DATE, o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, 
                      o.REV AS REV, o.WFS_ID AS WFS_ID, dbo.isBoss(o.ROOT) AS isBoss, p.ID, p.LOGIN, p.NAME, p.PASSWORD, p.BOSS, p.SOURCE, p.LAST_NAME, 
                      p.MIDDLE_NAME, p.NICK_NAME, p.LANG, p.HIRE_DATE, p.DRCM, p.MODBY, p.OBJECT_ID, p.COMPANY, p.TIME_ZONE, p.FULL_NAME, 
                      p.SYSTEM_STATUS, p.CO_POSITION, p.ROOT_COMPANY, p.IS_HEAD, p.TOOL_BOX, p.INFO_BOX, p.ADV_SEARCH, p.COLOR_KEY, 
                      p.SCREEN_TYPE
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PEOPLE_HISTORY p ON o.ID = p.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_SIMPLE_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_SIMPLE_SEARCH]
AS
SELECT     p.*, c.NAME AS COMPANY_NAME, ISNULL(boss.NAME, '') + ' ' + ISNULL(boss.LAST_NAME, '') AS BOSS_NAME
FROM         dbo.A_PEOPLE_HISTORY boss RIGHT OUTER JOIN
                      dbo.A_O_PEOPLE p ON boss.ID = p.BOSS LEFT OUTER JOIN
                      dbo.A_O_COMPANIES c INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ca ON c.ID = ca.ROLE ON p.ID = ca.PERSON
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_OWNER_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROCEDURE_STEP_OWNER_ROLE]
AS
SELECT     RELATIONSHIP, LABOR_ROLE, APPROVED_OBJECT_ID AS OWNER_ROLE_ID, STEP_ID, QTY, QTY_TYPE
FROM         dbo.A_PROCEDURE_OBJECT_LINK
WHERE     (RELATIONSHIP = N'LABOR_PROVIDE_TAKE_BACK') AND (LABOR_ROLE = 'LABOR_OWNER')
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR]
AS
SELECT     dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.RELATIONSHIP AS RELATIONSHIP, 
                      dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.LABOR_ROLE AS LABOR_ROLE, 
                      dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.OWNER_ROLE_ID AS OWNER_ROLE_ID, 
                      dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.QTY AS OWNER_LABOR_TIME, 
                      dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.QTY_TYPE AS OWNER_LABOR_TYPE, dbo.A_PROCEDURE_STEPS.ID, 
                      dbo.A_PROCEDURE_STEPS.PROCEDURE_ID, 
					  dbo.A_PROCEDURE_STEPS.STEP_TEXT,
					  dbo.A_PROCEDURE_STEPS.TITLE,
					  dbo.A_PROCEDURE_STEPS.START_ON_COUNTER, 
                      dbo.A_PROCEDURE_STEPS.COUNTER_VALUE, dbo.A_PROCEDURE_STEPS.COUNTER_UNIT, dbo.A_PROCEDURE_STEPS.REL_OR_ABS, 
                      dbo.A_PROCEDURE_STEPS.FROM_START_OR_STOP, dbo.A_PROCEDURE_STEPS.SYSTEM_TASK, dbo.A_PROCEDURE_STEPS.DESTINATION, 
                      dbo.A_PROCEDURE_STEPS.SPECIFIC_LOCATION, dbo.A_PROCEDURE_STEPS.REFERENCE_VERB, 
                      dbo.A_PROCEDURE_STEPS.REFERENCE_OBJECT, dbo.A_PROCEDURE_STEPS.COMMENTS, dbo.A_PROCEDURE_STEPS.GOTO_STEP, 
                      dbo.A_PROCEDURE_STEPS.GOTO_STEP_ID, dbo.A_PROCEDURE_STEPS.CYCLES, dbo.A_PROCEDURE_STEPS.CYCLE_ON_COUNTER, 
                      dbo.A_PROCEDURE_STEPS.CYCLE_COUNT, dbo.A_PROCEDURE_STEPS.CYCLE_UNIT, dbo.A_PROCEDURE_STEPS.DRCM, 
                      dbo.A_PROCEDURE_STEPS.MODBY, dbo.A_PROCEDURE_STEPS.PRINT_ORDER, dbo.A_PROCEDURE_STEPS.OLD_PROCEDURE_ID, 
                      dbo.A_PROCEDURE_STEPS.OLD_STEP_ID, dbo.A_PROCEDURE_STEPS.DURATION, dbo.A_PROCEDURE_STEPS.DURATION_TYPE, 
                      dbo.A_PROCEDURE_STEPS.DATE_LAST_MODIFIED
FROM         dbo.A_PROCEDURE_STEPS LEFT OUTER JOIN
                      dbo.A_V_PROCEDURE_STEP_OWNER_ROLE ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_V_PROCEDURE_STEP_OWNER_ROLE.STEP_ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PARENT_MESSAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_PARENT_MESSAGES]
AS
SELECT     dbo.A_MESSAGES.MESSAGE AS PARENT_MESSAGE, dbo.A_MESSAGES.PARENT_ID, dbo.A_MESSAGES.ID, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS PARENT_SENDER_NAME, dbo.A_MESSAGES.SENDER AS PARENT_SENDER, 
                      dbo.A_MESSAGES.HIDE_MESSAGE AS PARENT_HIDE_MESSAGE
FROM         dbo.A_MESSAGES LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_MESSAGES.SENDER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_DATA]
AS
SELECT     dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS SENDER_NAME, dbo.A_MESSAGES.ID, dbo.A_MESSAGES.MESSAGE, dbo.A_MESSAGES.IMPORTANCE, 
                      dbo.A_MESSAGES.DATE_CREATED, dbo.A_MESSAGES.STATUS, dbo.A_MESSAGES.PARENT_ID, 
                      dbo.A_V_MESSAGES_PARENT_MESSAGES.PARENT_MESSAGE, dbo.A_MESSAGES.DATE_SENT, dbo.A_MESSAGES.SENDER, 
                      dbo.A_V_MESSAGES_PARENT_MESSAGES.PARENT_SENDER_NAME, dbo.A_V_MESSAGES_PARENT_MESSAGES.PARENT_SENDER, 
                      dbo.A_MESSAGES.HIDE_MESSAGE, dbo.A_V_MESSAGES_PARENT_MESSAGES.PARENT_HIDE_MESSAGE, dbo.A_MESSAGES.NOTIFY
FROM         dbo.A_MESSAGES LEFT OUTER JOIN
                      dbo.A_V_MESSAGES_PARENT_MESSAGES ON dbo.A_MESSAGES.PARENT_ID = dbo.A_V_MESSAGES_PARENT_MESSAGES.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_MESSAGES.SENDER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER]
AS
SELECT     dbo.A_PROCEDURE_STEP_PRECEDING_STEPS.MY_STEP, dbo.A_PROCEDURE_STEP_PRECEDING_STEPS.PREV_STEP, 
                      dbo.A_PROCEDURE_STEPS.PRINT_ORDER
FROM         dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURE_STEP_PRECEDING_STEPS ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_STEP_PRECEDING_STEPS.PREV_STEP
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_LABOR_AND_PRINT_ORDER]
AS
SELECT     dbo.A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER.PREV_STEP, 
                      dbo.A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER.PRINT_ORDER AS P_ORDER, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.LABOR_ROLE, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.RELATIONSHIP, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.OWNER_ROLE_ID, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.OWNER_LABOR_TIME, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.OWNER_LABOR_TYPE, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.ID, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.PROCEDURE_ID, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.STEP_TEXT, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.START_ON_COUNTER, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.COUNTER_VALUE, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.COUNTER_UNIT, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.REL_OR_ABS, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.FROM_START_OR_STOP, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.SYSTEM_TASK, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.DESTINATION, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.SPECIFIC_LOCATION, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.REFERENCE_VERB, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.REFERENCE_OBJECT, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.COMMENTS, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.GOTO_STEP, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.GOTO_STEP_ID, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.CYCLES, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.CYCLE_ON_COUNTER, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.CYCLE_COUNT, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.CYCLE_UNIT, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.DRCM, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.MODBY, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.PRINT_ORDER, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.OLD_PROCEDURE_ID, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.OLD_STEP_ID, dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.DURATION, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.DURATION_TYPE, 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.DATE_LAST_MODIFIED
FROM         dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR LEFT OUTER JOIN
                      dbo.A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER ON 
                      dbo.A_V_PROCEDURE_STEPS_WITH_OWNER_LABOR.ID = dbo.A_V_PROCEDURE_STEP_PREV_W_PRNT_ORDER.MY_STEP
GO

/****** Object:  View [dbo].[A_V_PEOPLE_OBJECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_OBJECT_SEARCH]
AS
SELECT     p.*, o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, o.CREATE_DATE AS CREATE_DATE, 
                      dbo.leadingSpaces(o.ROOT, 50) AS SORT_ROOT, LTRIM(STR(YEAR(p.HIRE_DATE))) + '-' + dbo.leadingZeros(LTRIM(STR(MONTH(p.HIRE_DATE))), 2) 
                      + '-' + dbo.leadingZeros(LTRIM(STR(DAY(p.HIRE_DATE))), 2) AS DATE_HIRED, o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, 
                      o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, o.REV AS REV, o.WFS_ID AS WFS_ID, o.LOCKED_BY_NAME AS LOCKED_BY_NAME, 
                      o.CREATING_CO_NAME AS CREATING_CO_NAME, dbo.isBoss(o.ROOT) AS isBoss, dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT(p.OBJ_ID, 
                      'PICTURE') AS picRecord, dbo.A_PEOPLE_HISTORY.ROOT_COMPANY AS ROOT_CO_ID, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS ROOT_CO_NAME, dbo.A_PEOPLE_HISTORY.LOGIN AS LOGIN
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE p ON o.ID = p.OBJ_ID INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON p.ID = dbo.A_PEOPLE_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PEOPLE_HISTORY.ROOT_COMPANY = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[Portal_PeopleView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PeopleView]
AS
select
pb.ID as Id,
pb.OBJ_ID AS ObjectId,
pb.NAME AS FirstName,
p.PASSWORD as Password,
pb.LAST_NAME AS LastName,
pb.FULL_NAME AS FullName,
pb.TIME_ZONE AS
TimeZone,
pb.PRIMARY_PHONE_NUMBER AS PrimaryPhone,
pb.WORK_EMAIL_ADDRESS AS Email,
pb.LOGIN as Login,
pb.STATUS as Status, 
pb.COMPANY_NAME AS CompanyName,
pb.POSITION_NAME AS TItle,
'ClientAdmin' AS RoleName,
pb.CREATE_DATE AS CreatedDate,
ae.ADDY AS EmailAddress
FROM dbo.A_V_PEOPLE_OBJECT_SEARCH AS pb 
INNER JOIN A_PEOPLE_HISTORY AS p on p.OBJECT_ID=pb.OBJ_ID
INNER JOIN dbo.A_EMAILS ae ON p.OBJECT_ID = ae.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES]
AS
SELECT     dbo.A_ROLES_HISTORY.NAME AS ROLE_NAME, dbo.A_ROLES.ID AS ROLE_ID, dbo.A_ROLE_ASSIGNEE.STATUS AS ROLE_STATUS, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH.*, dbo.A_PEOPLE_HISTORY.ROOT_COMPANY AS Expr1
FROM         dbo.A_ROLES_HISTORY INNER JOIN
                      dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ON dbo.A_PEOPLE.ID = dbo.A_ROLE_ASSIGNEE.PERSON ON 
                      dbo.A_ROLES_HISTORY.ID = dbo.A_ROLE_ASSIGNEE.ROLE INNER JOIN
                      dbo.A_ROLES ON dbo.A_ROLES_HISTORY.ID = dbo.A_ROLES.HISTORY_REF_ID INNER JOIN
                      dbo.A_V_PEOPLE_OBJECT_SEARCH ON dbo.A_PEOPLE_HISTORY.OBJECT_ID = dbo.A_V_PEOPLE_OBJECT_SEARCH.OBJ_ID
GO

/****** Object:  View [dbo].[A_O_NOUN_HIER_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_NOUN_HIER_HISTORY]
AS
SELECT     HierHistory.OBJECT_ID, HierHistory.ID, HierHistory.NAME, HIER_O.LOCKED_BY, HIER_O.UNLOCKED_BY, HIER_O.CREATED_BY, 
                      HIER_O.CREATE_DATE, HIER_O.ROOT, HIER_O.REV_INFO, HIER_O.CREATING_CO, HIER_O.STATUS, HIER_O.REV, HIER_O.WFS_ID, 
                      HIER_O.LOCKED_BY_NAME, HIER_O.CREATING_CO_NAME, HIER_O.APPROVAL_ACTIVITY, HIER_O.ID AS OBJ_ID, HierHistory.ROOT_CHILD, 
                      FIRST_CHILD_O.OBJ_DESC, FIRST_CHILD_O.OBJ_TABLE AS OBJ_TYPE
FROM         dbo.A_V_APPROVED_OBJECTS FIRST_CHILD_O INNER JOIN
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING FIRST_CHILD ON FIRST_CHILD_O.ID = FIRST_CHILD.APPROVED_OBJ_ID RIGHT OUTER JOIN
                      dbo.A_NOUN_HIERARCHIES_HISTORY HierHistory INNER JOIN
                      dbo.A_OBJECTS HIER_O ON HierHistory.OBJECT_ID = HIER_O.ID ON FIRST_CHILD.HIERARCHY_ID = HierHistory.ID AND 
                      FIRST_CHILD.ID = HierHistory.ROOT_CHILD
GO

/****** Object:  View [dbo].[Protal_ObjectsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Protal_ObjectsView]
AS
SELECT        ID AS Id, OBJ_REF_ID AS ObjectRefId, OBJ_TABLE AS ObjectTable, OBJ_ID AS ObjectId, OBJ_DESC AS ObjectDesc, STATUS AS Status, CREATING_CO AS CreatingCo, REV AS Rev, 
                         CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_V_APPROVED_OBJECTS
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_SELECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PRODUCTS_APPROVED_SELECT_DATA]
AS
SELECT     dbo.A_PRODUCTS_HISTORY.OBJECT_ID, dbo.A_PRODUCTS_HISTORY.PARENT_ID, dbo.A_PRODUCTS_HISTORY.NAME, 
                      dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS SUPPLIER_NAME, 
                      dbo.A_PRODUCTS_HISTORY.COMMENTS, dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID, dbo.A_V_PROCEDURES_APPROVED_DATA.VERB, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.VERB_NAME, dbo.A_PRODUCTS_HISTORY.APP_OBJECT, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS APP_OBJ_NAME, dbo.A_PRODUCTS_HISTORY.SHIP_OR_LABOR, 
                      dbo.A_PRODUCTS_HISTORY.CUSTOMIZABLE, dbo.A_PRODUCTS_HISTORY.REQ_FORM, dbo.A_PRODUCTS_HISTORY.MGR_TEAM, 
                      dbo.A_APPROVED_ROLES.NAME AS MGR_TEAM_NAME, dbo.A_PRODUCTS_HISTORY.SALES_TAX, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROCEDURE_NAME, dbo.A_PRODUCTS.ID, dbo.A_PRODUCTS.HISTORY_REF_ID, 
                      dbo.A_OBJECTS.CREATING_CO_NAME, dbo.A_OBJECTS.CREATING_CO, dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE, dbo.A_PRODUCTS.STATUS, 
                      dbo.A_PRODUCTS_HISTORY.PERSON_SUPPLIER, dbo.A_PRODUCTS_HISTORY.AVAILABILITY, 
                      dbo.A_PRODUCTS_HISTORY.SYSTEM_PROCEDURE,
					  dbo.A_PRODUCTS_HISTORY.CustomerRequirementId
FROM         dbo.A_PRODUCTS_HISTORY INNER JOIN
                      dbo.A_PRODUCTS ON dbo.A_PRODUCTS_HISTORY.ID = dbo.A_PRODUCTS.HISTORY_REF_ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_PRODUCTS_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_PRODUCTS_HISTORY.MGR_TEAM = dbo.A_APPROVED_ROLES.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_PRODUCTS_HISTORY.APP_OBJECT = dbo.A_V_APPROVED_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON 
                      dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID
WHERE     (dbo.A_PRODUCTS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_FOR_PURCHASING_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_PRODUCTS_FOR_PURCHASING_DATA]
AS
SELECT     PROD.ID, PROD_HIST.NAME AS PRODUCT_NAME, PROD_HIST.SUPPLIER_ID, PPL_CUST.CUST_ID AS CUSTOMER_ID, 
                      SUP_CO.NAME AS SUPPLIER_NAME, PPL_H.UNIT_PRICE, PROD_HIST.CUSTOMIZABLE, PPL_H.UNIT, PPL_H.MIN_QUANTITY, 
                      PPL_H.EST_UNIT_PRICE, PPL_H.EST_LABOR_PRICE, PPL_H.EST_PARTS_PROV_STAY, PPL_H.EST_PARTS_PROV_TAKE_BACK, 
                      PPL_H.EST_PARTS_CONSUMED, PPL_H.PRODUCTION_TIME, PPL_H.PRODUCTION_TIME_UNIT, PPL_H.CAPACITY, PPL_H.CAPACITY_UNIT, 
                      PPL_H.CUSTOMER_NAME, PROD_HIST.SALES_TAX, PROD_HIST.REQ_FORM, PROD_HIST.CUST_MGR_ROLE, PROD_HIST.PROCEDURE_ID, 
                      [PROC].SYSTEM_ID AS PROC_SYS_ID, [PROC].NAME AS PROC_NAME, PPL.ID AS PRICE_LIST_ID, CUSTOMER.NAME AS CUST_NAME, PROD.STATUS, 
                      SUP_CO.ROOT_CO AS SUP_ROOT_CO, A_V_COMPANIES_APPROVED_DATA_1.NAME AS SUP_ROOT_CO_NAME, PPL_H.FOR_INDIVIDUAL_SALE, 
                      PPL.STATUS AS PPL_STAT, dbo.A_V_APPROVED_OBJECTS.ID AS APP_OBJ_ID, ISNULL(dbo.A_PARTS_HISTORY.NAME + N' ', N'') 
                      + ISNULL(N'(' + dbo.A_PARTS_HISTORY.COMPANY_PART_NUMBER + N') ', N'') AS APP_OBJ_PART_DESC, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC,
                       PROD_HIST.AVAILABILITY, PROD_HIST.OBJECT_ID AS OBJ_ID, PROD_HIST.OEM, PROD_HIST.MODEL, PROD_HIST.AREA, PROD_HIST.CU, 
                      PROD_HIST.MM
FROM         dbo.A_PRODUCTS PROD INNER JOIN
                      dbo.A_PRODUCTS_HISTORY PROD_HIST ON PROD.HISTORY_REF_ID = PROD_HIST.ID INNER JOIN
                      dbo.A_PROD_PRICE_LIST_HISTORY PPL_H ON PROD.ID = PPL_H.PRODUCT INNER JOIN
                      dbo.A_PROD_PRICE_LIST PPL ON PPL_H.ID = PPL.HISTORY_REF_ID INNER JOIN
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS PPL_CUST ON PPL_H.ID = PPL_CUST.PP_LIST_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUP_CO ON PROD_HIST.SUPPLIER_ID = SUP_CO.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUSTOMER ON PPL_CUST.CUST_ID = CUSTOMER.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      SUP_CO.ROOT_CO = A_V_COMPANIES_APPROVED_DATA_1.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON PROD_HIST.APP_OBJECT = dbo.A_V_APPROVED_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_PARTS_HISTORY ON dbo.A_V_APPROVED_OBJECTS.OBJ_ID = dbo.A_PARTS_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [PROC] ON PROD_HIST.PROCEDURE_ID = [PROC].ID
WHERE     (PROD.STATUS = 'APPROVED') AND (PPL.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_LABOR]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROCEDURE_LABOR]
AS
SELECT     dbo.A_PROCEDURES_HISTORY.NAME AS PROCEDURE_NAME, dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID AS PROC_ID, 
                      dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID AS PROC_STEP, dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID AS ROLE_ID, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS ROLE_NAME, dbo.A_PROCEDURE_OBJECT_LINK.QTY, dbo.A_PROCEDURE_OBJECT_LINK.QTY_TYPE, 
                      dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP, dbo.A_PROCEDURE_OBJECT_LINK.LABOR_ROLE
FROM         dbo.A_PROCEDURES_HISTORY INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_PROCEDURES_HISTORY.ID = dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_V_APPROVED_OBJECTS.ID
WHERE     (dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP = N'LABOR_PROVIDE_TAKE_BACK')
GO

/****** Object:  View [dbo].[A_V_ACCOUNTS_WITH_RELATED_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_ACCOUNTS_WITH_RELATED_OBJECTS]
AS
SELECT     dbo.A_ACCOUNTS_HISTORY.ID AS ACCT_ID, dbo.A_ACCOUNTS_RELATED_OBJECTS.OBJ_ID, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC
FROM         dbo.A_ACCOUNTS_HISTORY INNER JOIN
                      dbo.A_ACCOUNTS_RELATED_OBJECTS ON dbo.A_ACCOUNTS_HISTORY.ID = dbo.A_ACCOUNTS_RELATED_OBJECTS.ACCOUNT_ID INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_ACCOUNTS_RELATED_OBJECTS.OBJ_ID = dbo.A_V_APPROVED_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_NOUN_HIER_CHILDREN_EDITING_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_NOUN_HIER_CHILDREN_EDITING_DATA]
AS
SELECT     dbo.A_V_APPROVED_OBJECTS.OBJ_DESC AS NAME, dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.ID, 
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.ROOT_ID, dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.APPROVED_OBJ_ID, 
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.PARENT_ID, dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.HIERARCHY_ID, 
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.DRCM, dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.MODBY, 
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.BEEN_APPROVED, dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.HIDDEN
FROM         dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING.APPROVED_OBJ_ID = dbo.A_V_APPROVED_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_TASK_ACTION_OBJECT_LINK_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASK_ACTION_OBJECT_LINK_ALL_DATA]
AS
SELECT     dbo.A_TASK_ACTION_OBJECT_LINK.TASK_ID, dbo.A_TASK_ACTION_OBJECT_LINK.OBJECT_ID, dbo.A_TASK_ACTION_OBJECT_LINK.RELATIONSHIP, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_DESC, dbo.A_TASKS.DESCRIPTION AS TASK_DESCRIPTION, dbo.A_TASK_ACTION_OBJECT_LINK.QTY, 
                      dbo.A_TASK_ACTION_OBJECT_LINK.ID
FROM         dbo.A_TASK_ACTION_OBJECT_LINK INNER JOIN
                      dbo.A_TASKS ON dbo.A_TASK_ACTION_OBJECT_LINK.TASK_ID = dbo.A_TASKS.ID INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_TASK_ACTION_OBJECT_LINK.OBJECT_ID = dbo.A_V_APPROVED_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_TASK_OBJECT_LINK_ALL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_OBJECT_LINK_ALL_DATA]
AS
SELECT     TOP 100 PERCENT dbo.A_TASK_OBJECT_LINK.TASK_ID, dbo.A_TASK_OBJECT_LINK.OBJECT_ID, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_TABLE, dbo.A_V_APPROVED_OBJECTS.OBJ_ID
FROM         dbo.A_TASK_OBJECT_LINK INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_V_APPROVED_OBJECTS.ID
ORDER BY dbo.A_TASK_OBJECT_LINK.DRCM DESC
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_APPLICABLE_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROCEDURE_STEP_APPLICABLE_OBJECTS]
AS
SELECT     dbo.A_PROCEDURE_OBJECT_LINK.ID, dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID, dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID, 
                      dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID, dbo.A_PROCEDURE_OBJECT_LINK.QTY, dbo.A_PROCEDURE_OBJECT_LINK.QTY_TYPE, 
                      dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC
FROM         dbo.A_PROCEDURE_OBJECT_LINK LEFT OUTER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_V_APPROVED_OBJECTS.ID
WHERE     (dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP = N'STEP_APPLICABLE_OBJECT')
GO

/****** Object:  View [dbo].[A_APPROVED_NOUN_HIERARCHIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_APPROVED_NOUN_HIERARCHIES]
AS
SELECT     dbo.A_NOUN_HIERARCHIES.ID, dbo.A_NOUN_HIERARCHIES.HISTORY_REF_ID, dbo.A_NOUN_HIERARCHIES.DRCM, 
                      dbo.A_NOUN_HIERARCHIES.MODBY, dbo.A_NOUN_HIERARCHY_CHILDREN.ID AS ROOT_CHILD, dbo.A_NOUN_HIERARCHY_CHILDREN.PARENT_ID, 
                      dbo.A_NOUN_HIERARCHIES_HISTORY.NAME, dbo.A_V_APPROVED_OBJECTS.OBJ_TABLE, dbo.A_V_APPROVED_OBJECTS.OBJ_DESC, 
                      dbo.A_V_APPROVED_OBJECTS.OBJ_ID
FROM         dbo.A_NOUN_HIERARCHIES INNER JOIN
                      dbo.A_NOUN_HIERARCHIES_HISTORY ON dbo.A_NOUN_HIERARCHIES.HISTORY_REF_ID = dbo.A_NOUN_HIERARCHIES_HISTORY.ID INNER JOIN
                      dbo.A_NOUN_HIERARCHY_CHILDREN ON dbo.A_NOUN_HIERARCHIES.ID = dbo.A_NOUN_HIERARCHY_CHILDREN.HIERARCHY_ID INNER JOIN
                      dbo.A_V_APPROVED_OBJECTS ON dbo.A_NOUN_HIERARCHY_CHILDREN.APPROVED_OBJ_ID = dbo.A_V_APPROVED_OBJECTS.ID
WHERE     (dbo.A_NOUN_HIERARCHY_CHILDREN.PARENT_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_APPROVED_ROLES_WITH_NTLOGIN_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_APPROVED_ROLES_WITH_NTLOGIN_ID]
AS
SELECT     dbo.A_APPROVED_PEOPLE.ID AS PERSON, dbo.A_APPROVED_PEOPLE.FULL_NAME, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.ROLE_NAME, dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.ROLE_ID AS ROLE, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.BOSS_ID, dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.BOSS_NAME, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.COMPANY_ID, dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.COMPANY_NAME, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.POSITION_ID, dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.POSITION_NAME, 
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.ROLE_STATUS, dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.OBJ_ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES ON 
                      dbo.A_APPROVED_PEOPLE.OBJECT_ID = dbo.A_V_PEOPLE_OBJECT_SEARCH_WITH_ROLES.OBJ_ID
GO

/****** Object:  View [dbo].[A_Z_UNITS_BASE_UNIT_CONVERTER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_Z_UNITS_BASE_UNIT_CONVERTER]
AS
SELECT     dbo.A_UNIT_RELATIONS.FIRST_UNIT AS FROM_UNIT, dbo.A_UNIT_RELATIONS.SECOND_UNIT AS STD_UNIT, 
                      dbo.A_UNIT_RELATIONS.X_SECOND AS STD_UNIT_QTY, dbo.A_UNIT_TYPES.UNIT_TYPE
FROM         dbo.A_UNIT_RELATIONS INNER JOIN
                      dbo.A_UNIT_TYPES ON dbo.A_UNIT_RELATIONS.FIRST_UNIT = dbo.A_UNIT_TYPES.ID
WHERE     (dbo.A_UNIT_RELATIONS.SECOND_UNIT IN ('TIME_SYS_SECONDS', 'UNIT', 'VOL_GALLONS', 'WT_OZ'))
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PRODUCTS_WITH_PRICE_AND_PROC_DATA]
AS
SELECT     PROD.ID, PROD.NAME, PROD.SUPPLIER_ID, PROD.PROCEDURE_ID, PP_LIST.UNIT_PRICE, PROD.COMMENTS, [PROC].SYSTEM_ID, 
                      [PROC].NAME AS PROC_NAME, PROD.APP_OBJECT AS PART_ID, PP_LIST.ID AS PP_LIST_ID, 
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID, PP_LIST.UNIT, 
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT_QTY AS STD_UNIT_QTY
FROM         dbo.A_V_PRODUCTS_APPROVED_DATA PROD INNER JOIN
                      dbo.A_V_PROD_PRICE_LIST_BY_APPROVED_ID PP_LIST ON PROD.ID = PP_LIST.PRODUCT INNER JOIN
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS ON 
                      PP_LIST.HISTORY_REF_ID = dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.PP_LIST_ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [PROC] ON PROD.PROCEDURE_ID = [PROC].ID INNER JOIN
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER ON PP_LIST.UNIT = dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.FROM_UNIT
GO

/****** Object:  View [dbo].[A_O_PROD_PRICE_LISTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_O_PROD_PRICE_LISTS]
AS
SELECT     O.ROOT, O.CREATING_CO, O.LOCKED_BY, O.UNLOCKED_BY, O.CREATED_BY, O.CREATE_DATE, O.REV_INFO, O.STATUS, O.REV, 
                      O.LOCKED_BY_NAME, O.CREATING_CO_NAME, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, PL.ID, PL.PRODUCT, PL.UNIT, 
                      PL.MIN_QUANTITY, PL.UNIT_PRICE, PL.EST_UNIT_PRICE, PL.EST_LABOR_PRICE, PL.EST_PARTS_PROV_STAY, PL.EST_PARTS_CONSUMED, 
                      PL.EST_PARTS_PROV_TAKE_BACK, PL.INVOICE_FROM, PL.PRODUCTION_TIME, PL.PRODUCTION_TIME_UNIT, PL.CAPACITY, PL.CAPACITY_UNIT, 
                      PL.DRCM, PL.MODBY, PL.OBJECT_ID, O.OBJ_TABLE, O.ID AS OBJ_ID, dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CUSTOMER, dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID, 
                      A_V_COMPANIES_APPROVED_DATA_1.NAME AS SUPPLIER_NAME, PL.FOR_INDIVIDUAL_SALE
FROM         dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID = A_V_COMPANIES_APPROVED_DATA_1.ID RIGHT OUTER JOIN
                      dbo.A_OBJECTS O INNER JOIN
                      dbo.A_PROD_PRICE_LIST_HISTORY PL ON O.ID = PL.OBJECT_ID LEFT OUTER JOIN
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID ON 
                      PL.ID = dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.PP_LIST_ID ON dbo.A_V_PRODUCTS_APPROVED_DATA.ID = PL.PRODUCT
GO

/****** Object:  View [dbo].[A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS]
AS
SELECT     dbo.A_V_ORDERS_APPROVED_DATA.ID AS ORDER_ID, dbo.A_V_ORDERS_APPROVED_DATA.HISTORY_REF_ID AS ORDER_HIST_REF_ID, 
                      dbo.A_ORDER_ITEMS.PRODUCT_ID, dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID, dbo.A_V_PRODUCTS_APPROVED_DATA.APP_OBJECT, 
                      dbo.A_V_ORDERS_APPROVED_DATA.DESCRIPTION, dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO, 
                      dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_PERSON, dbo.A_V_ORDERS_APPROVED_DATA.BUDGETARY_ONLY, 
                      dbo.A_V_ORDERS_APPROVED_DATA.EXPIRATION_DATE, dbo.A_V_PRODUCTS_APPROVED_DATA.NAME AS PRODUCT_NAME, 
                      CUSTOMER.NAME AS CUSTOMER_NAME, SUPPLIER.NAME AS SUPPLIER_NAME, dbo.A_V_ORDERS_APPROVED_DATA.PROGRESS, 
                      dbo.A_ORDER_ITEMS.PROC_SYS_ID, dbo.A_ORDER_ITEMS.SOURCE_ID, dbo.A_ORDER_ITEMS.ADD_COST_ID, dbo.A_ORDER_ITEMS.PARENT, 
                      dbo.A_V_ORDERS_APPROVED_DATA.STATUS
FROM         dbo.A_V_ORDERS_APPROVED_DATA INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_V_ORDERS_APPROVED_DATA.HISTORY_REF_ID = dbo.A_ORDER_ITEMS.ORDER_ID INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA ON dbo.A_ORDER_ITEMS.PRODUCT_ID = dbo.A_V_PRODUCTS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUSTOMER ON dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO = CUSTOMER.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID = SUPPLIER.ID
GO

/****** Object:  View [dbo].[A_V_ORDER_ITEM_TRAVEL_TO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ORDER_ITEM_TRAVEL_TO]
AS
SELECT     I.ID, I.PROC_SYS_ID, I.DEST, I.PARENT, I.PRODUCT_ID, P.NAME AS PROD_NAME
FROM         dbo.A_ORDER_ITEMS I INNER JOIN
                      dbo.A_V_PRODUCTS_APPROVED_DATA P ON I.PRODUCT_ID = P.ID
WHERE     (I.DEST = 'to') AND (I.PROC_SYS_ID = 'SYS_TRAVEL')
GO

/****** Object:  View [dbo].[A_V_PROD_SUPPLIER_APP_OBJ]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROD_SUPPLIER_APP_OBJ]
AS
SELECT     dbo.A_V_PRODUCTS_APPROVED_DATA.APP_OBJECT, dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID, 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.SUPPLIER_ID
FROM         dbo.A_V_PRODUCTS_APPROVED_DATA INNER JOIN
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA ON 
                      dbo.A_V_PRODUCTS_APPROVED_DATA.ID = dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.PRODUCT INNER JOIN
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS ON 
                      dbo.A_V_PROD_PRICE_LIST_APPROVED_DATA.HISTORY_REF_ID = dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.PP_LIST_ID
GO

/****** Object:  View [dbo].[Report_CombinedFinancialData]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Report_CombinedFinancialData]
AS

SELECT 
pwo.CustPurchNum AS WONumber,
pwo.StartDate AS WOCreationDate,
pwo.DueDate AS DueDate,
pwo.OrigDueDate AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.WoItem AS KitName,
pwo.ReferencePo AS pono,
pwo.CustMttn AS mttn,
pwo.Amount,
pwo.FillQty,
i.Id AS InvoiceId,
i.InvoiceDate AS InvoiceDate,
i.SubTotal AS SubTotal,
i.Tax AS WTax
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN Portal_InvoiceWorkItem iw ON iw.ItemId = pwo.FillItemId
INNER JOIN Portal_Invoice i ON i.Id = iw.InvoiceId
GO

/****** Object:  View [dbo].[Report_SerialNumberHistory]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Report_SerialNumberHistory]
AS

SELECT DISTINCT
pwo.Serial AS SerialNumber,
pwo.CustPurchNum AS WONumber,
pwo.StartDate AS WOCreationDate,
pwo.DueDate AS DueDate,
pwo.OrigDueDate AS ShipDate, 
pwo.LocationName AS MSRFSRFacility,
pwo.CustomerName,
pwo.ProcName AS specno,
pwo.WoItem AS KitName,
'Not sure where to get' AS NCRNumber,
pwo.ReferencePo AS pono,
pwo.CustMttn AS mttn,
PTLCount.CycleCount
FROM dbo.Portal_WorkOrders pwo 
INNER JOIN PartsTransactionLog AS pt ON pt.PartId = pwo.PartId AND pt.SerialNumber=pwo.Serial
OUTER APPLY(
			SELECT count(SerialNumber) AS CycleCount 
			FROM PartsTransactionLog AS pt
			WHERE pt.PartId = pwo.PartId AND pt.SerialNumber=pwo.Serial
			) PTLCount
GO

/****** Object:  View [dbo].[Report_WorkInProgress]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Report_WorkInProgress]
AS

SELECT pwo.Serial AS [W/O #],
pwo.DueDate,
('Specification: '+ COALESCE(pwo.ProcName,'') + ' | '+
'  ATTN: ' + 'N/A ' + ' | '+ 
'P.O.: '+ COALESCE(pwo.CustPurchNum,'') + ' | '+
'Kit: ' + COALESCE(pwo.ProductName,'') + ' | '+
'Tool: ' + ' | '+
'Mittn: '+ COALESCE(pwo.CustMttn,'')) AS Details,
Status,
pwo.ProcName
,pwo.CustMttn
FROM Portal_WorkOrders pwo
WHERE pwo.Status IN ('PENDING_PARENT_ACCEPTANCE','REQUESTED')
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_WITH_INITIAL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DISCUSSION_WITH_INITIAL_DATA]
AS
SELECT     p.FULL_NAME AS INITIATOR_NAME, d.ID, d.SUBJECT, d.INITIATOR AS INITIATOR_ID, d.DATE_CREATED, d.STATUS, r.RESPONSE AS STATEMENT, 
                      r.COLOR, r.PARENT_ID, r.ID AS RESPONSE_ID, d.LAST_RESPONSE
FROM         dbo.A_APPROVED_PEOPLE p RIGHT OUTER JOIN
                      dbo.A_DISCUSSIONS d ON p.ID = d.INITIATOR LEFT OUTER JOIN
                      dbo.A_DISCUSSION_RESPONSE r ON d.ID = r.DISCUSSION_ID
WHERE     (r.PARENT_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_TREE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DISCUSSION_GET_TREE_DATA]
AS
SELECT     dbo.A_V_DISCUSSION_WITH_INITIAL_DATA.INITIATOR_NAME AS WRITER_NAME, 
                      dbo.A_V_DISCUSSION_WITH_INITIAL_DATA.INITIATOR_ID AS INITIATOR, dbo.A_DISCUSSION_RESPONSE.DRCM, 
                      dbo.A_V_DISCUSSION_WITH_INITIAL_DATA.ID, dbo.A_DISCUSSION_RESPONSE.ID AS R_ID, 
                      dbo.A_V_DISCUSSION_WITH_INITIAL_DATA.DATE_CREATED, dbo.A_DISCUSSION_ATTACHMENTS.DOC_ID, dbo.A_DISCUSSION_RESPONSE.COLOR, 
                      dbo.A_DISCUSSION_RESPONSE.RESPONSE
FROM         dbo.A_V_DISCUSSION_WITH_INITIAL_DATA LEFT OUTER JOIN
                      dbo.A_DISCUSSION_RESPONSE ON 
                      dbo.A_V_DISCUSSION_WITH_INITIAL_DATA.RESPONSE_ID = dbo.A_DISCUSSION_RESPONSE.ID LEFT OUTER JOIN
                      dbo.A_DISCUSSION_ATTACHMENTS ON dbo.A_DISCUSSION_RESPONSE.ID = dbo.A_DISCUSSION_ATTACHMENTS.RESPONSE_ID
GO

/****** Object:  View [dbo].[A_V_PROD_REQ_FORM_DATES_WITH_PEOPLE_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROD_REQ_FORM_DATES_WITH_PEOPLE_NAME]
AS
SELECT     dbo.A_PROD_REQ_FORM_DATES.ID, dbo.A_PROD_REQ_FORM_DATES.PRF, dbo.A_PROD_REQ_FORM_DATES.DATE_TYPE, 
                      dbo.A_PROD_REQ_FORM_DATES.DT, dbo.A_PROD_REQ_FORM_DATES.DRCM, dbo.A_PROD_REQ_FORM_DATES.MODBY, 
                      dbo.A_PROD_REQ_FORM_DATES.PERSON, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME
FROM         dbo.A_PROD_REQ_FORM_DATES INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_PROD_REQ_FORM_DATES.PERSON = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_SPONSOR_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_SPONSOR_DATA]
AS
SELECT     dbo.A_PROJECT_SPONSORS.PROJECT_ID, dbo.A_PROJECT_SPONSORS.PEOPLE_ID AS SPONSOR_ID, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS SPONSOR_NAME
FROM         dbo.A_PROJECT_SPONSORS INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_PROJECT_SPONSORS.PEOPLE_ID = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_STATUS_HISTORY_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_STATUS_HISTORY_DATA]
AS
SELECT     dbo.A_SERVICE_CALLS_STATUS_HISTORY.WEEKLY_ID, dbo.A_SERVICE_CALLS_STATUS_HISTORY.STATUS_CHANGED_TO, 
                      dbo.A_SERVICE_CALLS_STATUS_HISTORY.DATE_CHANGED, dbo.A_SERVICE_CALLS_STATUS_HISTORY.CHANGED_BY, 
                      dbo.A_SERVICE_CALLS_STATUS_HISTORY.REASON, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS CHANGED_BY_NAME, 
                      dbo.A_SERVICE_CALLS_STATUS_HISTORY.REASON_TYPE
FROM         dbo.A_SERVICE_CALLS_STATUS_HISTORY INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_SERVICE_CALLS_STATUS_HISTORY.CHANGED_BY = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_MEETING_REPONDANT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MEETING_REPONDANT_DATA]
AS
SELECT     dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME, dbo.A_MEETING_RESPONSES.MEETING_ID, dbo.A_MEETING_RESPONSES.RESPONSE
FROM         dbo.A_MEETING_RESPONSES INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_MEETING_RESPONSES.PERSON_ID = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_ORDER_LINK_WITH_COMPANY_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_QUOTE_ORDER_LINK_WITH_COMPANY_NAMES]
AS
SELECT     QOL.QUOTE_ID, QOL.ORDER_ID, QOL.STATUS, QOL.CUSTOMER, QOL.SUPPLIER, C.NAME AS CUSTOMER_NAME, S.NAME AS SUPPLIER_NAME, 
                      ORD_HIST.DESCRIPTION AS ORDER_DESCRIPTION, ORD_HIST.CUSTOMER_PERSON, CUST_PERSON.P_NAME AS CUSTOMER_PERSON_NAME, 
                      ORD_HIST.BUDGETARY_ONLY, ORD_HIST.EXPIRATION_DATE, ORD_HIST.PROGRESS, 
                      dbo.A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE.ROLE_ID, dbo.A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE.ROLE_NAME, 
                      QOL.ID AS QOL_ID
FROM         dbo.A_QUOTE_ORDER_LINK QOL INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA C ON QOL.CUSTOMER = C.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA S ON QOL.SUPPLIER = S.ID INNER JOIN
                      dbo.A_ORDERS ORD ON QOL.ORDER_ID = ORD.ID INNER JOIN
                      dbo.A_ORDERS_HISTORY ORD_HIST ON ORD.HISTORY_REF_ID = ORD_HIST.ID INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN CUST_PERSON ON ORD_HIST.CUSTOMER_PERSON = CUST_PERSON.P_ID INNER JOIN
                      dbo.A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE ON QOL.ID = dbo.A_V_QUOTE_ORDER_LINK_WITH_RESPONSIBLE_ROLE.QOL_ID
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DISCUSSION_GET_RESPONSE_DATA_BY_ID]
AS
SELECT     dbo.A_DISCUSSION_RESPONSE.ID, dbo.A_DISCUSSION_RESPONSE.DRCM, dbo.A_DISCUSSION_RESPONSE.RESPONSE, 
                      dbo.A_DISCUSSION_RESPONSE.WRITER AS INITIATOR, dbo.A_DISCUSSION_ATTACHMENTS.DOC_ID, 
                      dbo.A_DISCUSSION_ATTACHMENTS.RESPONSE_ID, dbo.A_DISCUSSION_RESPONSE.COLOR, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS WRITER_NAME, dbo.A_DISCUSSION_RESPONSE.DISCUSSION_ID, 
                      dbo.A_DISCUSSION_RESPONSE.PARENT_ID, dbo.A_DISCUSSIONS.STATUS
FROM         dbo.A_DISCUSSION_RESPONSE INNER JOIN
                      dbo.A_DISCUSSIONS ON dbo.A_DISCUSSION_RESPONSE.DISCUSSION_ID = dbo.A_DISCUSSIONS.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_DISCUSSION_RESPONSE.WRITER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID LEFT OUTER JOIN
                      dbo.A_DISCUSSION_ATTACHMENTS ON dbo.A_DISCUSSION_RESPONSE.ID = dbo.A_DISCUSSION_ATTACHMENTS.RESPONSE_ID
GO

/****** Object:  View [dbo].[A_V_MEETING_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_MEETING_TASKS]
AS
SELECT     TASK.DESCRIPTION, dbo.A_TASK_MEETING_LINK.MEETING_ID, TASK.STATUS, TASK.REQUESTOR, TASK.COMPLETED_BY, 
                      TASK.LATEST_REQUESTEE_NAME, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS ORIG_REQUESTOR_NAME, dbo.A_TASK_COMMENT.COMMENT, 
                      TASK.ID AS TASK_ID, dbo.A_TASK_LAST_COMMENT.COMMENT_ID
FROM         dbo.A_TASK_COMMENT INNER JOIN
                      dbo.A_TASK_LAST_COMMENT ON dbo.A_TASK_COMMENT.ID = dbo.A_TASK_LAST_COMMENT.COMMENT_ID RIGHT OUTER JOIN
                      dbo.A_TASK_MEETING_LINK INNER JOIN
                      dbo.A_TASKS TASK ON dbo.A_TASK_MEETING_LINK.TASK_ID = TASK.ID ON dbo.A_TASK_LAST_COMMENT.TASK_ID = TASK.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON TASK.REQUESTOR = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_PEOPLE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_ALLOWED_PEOPLE_DATA]
AS
SELECT     dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS ALLOWED_PEOPLE_NAME, dbo.A_PROJECT_ALLOWED_PEOPLE.PEOPLE_ID AS ALLOWED_PEOPLE_ID,
                       dbo.A_PROJECT_ALLOWED_PEOPLE.PROJECT_ID
FROM         dbo.A_V_PEOPLE_BY_NTLOGIN INNER JOIN
                      dbo.A_PROJECT_ALLOWED_PEOPLE ON dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID = dbo.A_PROJECT_ALLOWED_PEOPLE.PEOPLE_ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_TOTAL_OT_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_TOTAL_OT_HOURS]
AS
SELECT     dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID, SUM(dbo.A_SERVICE_CALL_WORK_TIME.HOURS) AS OT_HOURS, 
                      dbo.A_SERVICE_CALL_WORK_TIME.HOUR_TYPE
FROM         dbo.A_SERVICE_CALLS_WEEKLY_REPORTS INNER JOIN
                      dbo.A_SERVICE_CALL_WORK_TIME ON dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID = dbo.A_SERVICE_CALL_WORK_TIME.WEEKLY_ID
GROUP BY dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID, dbo.A_SERVICE_CALL_WORK_TIME.HOUR_TYPE
HAVING      (dbo.A_SERVICE_CALL_WORK_TIME.HOUR_TYPE = 'OVER')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_VIEW_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_VIEW_DATA]
AS
SELECT     sc.BOSS, sc.SUPPLIER_NAME, dbo.A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG(sc.STATUS) AS STANDARD_SEARCH, sc.WORK_TYPE, 
                      total_hours.NT_0 + total_hours.OT_0 AS TOTAL_0, total_hours.NT_1 + total_hours.OT_1 AS TOTAL_1, total_hours.NT_2 + total_hours.OT_2 AS TOTAL_2,
                       total_hours.NT_3 + total_hours.OT_3 AS TOTAL_3, total_hours.NT_4 + total_hours.OT_4 AS TOTAL_4, 
                      total_hours.NT_5 + total_hours.OT_5 AS TOTAL_5, total_hours.NT_6 + total_hours.OT_6 AS TOTAL_6, sc.CUSTOMER_NAME, sc.SUPPLIER_ID, 
                      sc.CUSTOMER_ID, sc.APPROVER_ROLE, sc.PAYER_ROLE, sc.APPROVER, sc.PAYER, sc.STATUS, sc.WORKER_ID, sc.ID, sc.MACHINE_NAME, 
                      sc.FULL_NAME, sc.NORMAL_HOURS, ot_hours.OT_HOURS, sc.START_DAY, sc.START_MONTH, sc.START_YEAR, total_hours.TOTAL_HOURS, 
                      total_hours.NT_0, total_hours.NT_1, total_hours.NT_2, total_hours.NT_3, total_hours.NT_4, total_hours.NT_5, total_hours.NT_6, total_hours.OT_0, 
                      total_hours.OT_1, total_hours.OT_2, total_hours.OT_3, total_hours.OT_4, total_hours.OT_5, total_hours.OT_6, 
                      worker_name.P_NAME AS WORKER_NAME, approver_role_name.NAME AS APPROVER_NAME, payer_role_name.NAME AS PAYER_NAME, 
                      recievable_role.RECIEVABLE_ROLE, sc.START_DATE, A_V_PEOPLE_BY_NTLOGIN_1.P_NAME AS BOSS_NAME, sc.REASON_TYPE
FROM         dbo.A_V_SERVICE_CALLS_WITH_NORMAL_HOURS sc INNER JOIN
                      dbo.A_V_SERVICE_CALL_TOTAL_OT_HOURS ot_hours ON sc.ID = ot_hours.ID INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN worker_name ON sc.WORKER_ID = worker_name.P_ID INNER JOIN
                      dbo.A_APPROVED_ROLES payer_role_name ON sc.PAYER_ROLE = payer_role_name.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_TOTAL_HOURS total_hours ON sc.ID = total_hours.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN A_V_PEOPLE_BY_NTLOGIN_1 ON sc.BOSS = A_V_PEOPLE_BY_NTLOGIN_1.P_NAME LEFT OUTER JOIN
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE recievable_role ON sc.SUPPLIER_ID = recievable_role.COMPANY_ID LEFT OUTER JOIN
                      dbo.A_APPROVED_ROLES approver_role_name ON sc.APPROVER_ROLE = approver_role_name.ID
GROUP BY sc.BOSS, sc.SUPPLIER_NAME, sc.CUSTOMER_NAME, sc.SUPPLIER_ID, sc.CUSTOMER_ID, sc.APPROVER_ROLE, sc.PAYER_ROLE, sc.APPROVER, 
                      sc.PAYER, sc.STATUS, sc.WORKER_ID, sc.ID, sc.MACHINE_NAME, sc.FULL_NAME, sc.WORK_TYPE, sc.NORMAL_HOURS, ot_hours.OT_HOURS, 
                      sc.START_DAY, sc.START_MONTH, sc.START_YEAR, total_hours.TOTAL_HOURS, total_hours.NT_0, total_hours.NT_1, total_hours.NT_2, 
                      total_hours.NT_3, total_hours.NT_4, total_hours.NT_5, total_hours.NT_6, total_hours.OT_0, total_hours.OT_1, total_hours.OT_2, total_hours.OT_3, 
                      total_hours.OT_4, total_hours.OT_5, total_hours.OT_6, worker_name.P_NAME, approver_role_name.NAME, payer_role_name.NAME, 
                      recievable_role.RECIEVABLE_ROLE, sc.START_DATE, A_V_PEOPLE_BY_NTLOGIN_1.P_NAME, sc.REASON_TYPE
GO

/****** Object:  View [dbo].[A_V_MEETING_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MEETING_SEARCH]
AS
SELECT     m.MEETING_NAME, m.START_DATE, m.STOP_DATE, m.OWNER, m.HOST, m.TIME_KEEP, m.SCRIBE, m.WEB_LOCATION, m.DATE_CREATED, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.NAME AS LOCATION_NAME, m.SETTING, 
                      dbo.A_FN_MEETING_GET_STANDARD_SEARCH_FLAG(m.START_DATE, m.STOP_DATE, GETDATE()) AS STANDARD_SEARCH, m.ID, 
                      m.DATE_EMAIL_SENT, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS HOST_NAME
FROM         dbo.A_MEETINGS m INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON m.HOST = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID ON m.LOCATION = dbo.A_V_LOCATIONS_BY_APPROVED_ID.ID
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_AGENDA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MEETING_GET_AGENDA]
AS
SELECT     I.ID AS A_ID, I.TEXT, I.FACILITATOR, I.START_DATE, I.DURATION, I.ITEM, I.ROOT AS M_ID, I.PARENT, I.AUTHOR, I.DRCM, dbo.A_MEETINGS.SCRIBE, 
                      dbo.A_MEETINGS.HOST, I.MODBY, I.RELATED_ITEM, HOST.P_NAME AS HOST_NAME, 
                      A_V_PEOPLE_BY_NTLOGIN_1.P_NAME AS FACILITATOR_NAME,
                          (SELECT     TOP 1 DOC_ID
                            FROM          A_MEETING_AGENDA_ITEM_DOC_LINK
                            WHERE      AGENDA_ID = I.ID) AS HAS_DOC, dbo.A_MEETINGS.MEETING_NAME
FROM         dbo.A_V_PEOPLE_BY_NTLOGIN A_V_PEOPLE_BY_NTLOGIN_1 RIGHT OUTER JOIN
                      dbo.A_MEETING_AGENDA_ITEMS I ON A_V_PEOPLE_BY_NTLOGIN_1.P_ID = I.FACILITATOR LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN HOST INNER JOIN
                      dbo.A_MEETINGS ON HOST.P_ID = dbo.A_MEETINGS.HOST ON I.ROOT = dbo.A_MEETINGS.ID
GO

/****** Object:  View [dbo].[A_V_MEETING_EMAIL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MEETING_EMAIL_DATA]
AS
SELECT     m.ID, m.MEETING_NAME, m.COMMENT, m.START_DATE, m.STOP_DATE, m.OWNER, m.HOST, m.TIME_KEEP, m.SCRIBE, m.LOCATION, 
                      m.WEB_LOCATION, m.DATE_CREATED, dbo.A_FN_MEETING_GET_STANDARD_SEARCH_FLAG(m.START_DATE, m.STOP_DATE, GETDATE()) 
                      AS STANDARD_SEARCH, dbo.A_MEETING_INV_ROLE.ROLE_ID, dbo.A_V_LOCATIONS_BY_APPROVED_ID.NAME AS LOCATION_NAME, m.SETTING, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS CREATOR_NAME, dbo.A_MEETING_INV_PEOPLE.PEOPLE_ID
FROM         dbo.A_MEETINGS m INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON m.OWNER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID LEFT OUTER JOIN
                      dbo.A_MEETING_INV_PEOPLE ON m.ID = dbo.A_MEETING_INV_PEOPLE.MEETING_ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID ON m.LOCATION = dbo.A_V_LOCATIONS_BY_APPROVED_ID.ID LEFT OUTER JOIN
                      dbo.A_MEETING_INV_ROLE ON m.ID = dbo.A_MEETING_INV_ROLE.MEETING_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_DATA]
AS
SELECT     dbo.A_PROJECTS.ID, dbo.A_PROJECTS.DATE_CREATED, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS INITIATOR_NAME, dbo.A_PROJECTS.NAME, 
                      dbo.A_PROJECTS.PRIORITY_LEVEL, dbo.A_PROJECTS.OBJECTIVE, dbo.A_PROJECTS.LEADER, 
                      A_V_PEOPLE_BY_NTLOGIN_1.P_NAME AS LEADER_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_LEVEL_NAME, 
                      dbo.A_PROJECTS.SECURITY_LEVEL, dbo.A_PROJECTS.ORIGINAL_PLANNED_STOP_DATE, dbo.A_PROJECTS.CURRENT_PLANNED_STOP_DATE, 
                      dbo.A_PROJECTS.MONTH_GOALS, dbo.A_PROJECTS.ACTUAL_STOP_DATE, dbo.A_PROJECTS.ISSUES, dbo.A_PROJECTS.SUMMARY, 
                      dbo.A_PROJECTS.INITIATOR
FROM         dbo.A_PROJECTS LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_PROJECTS.INITIATOR = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN A_V_PEOPLE_BY_NTLOGIN_1 ON dbo.A_PROJECTS.LEADER = A_V_PEOPLE_BY_NTLOGIN_1.P_ID LEFT OUTER JOIN
                      dbo.A_SECURITY_LEVELS ON dbo.A_PROJECTS.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_MEMBER_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_MEMBER_DATA]
AS
SELECT     dbo.A_PROJECT_MEMBERS.PEOPLE_ID AS MEMBER_ID, dbo.A_PROJECT_MEMBERS.PROJECT_ID, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS MEMBER_NAME
FROM         dbo.A_PROJECT_MEMBERS INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_PROJECT_MEMBERS.PEOPLE_ID = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_ORDER_MAIN_DATA_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ORDER_MAIN_DATA_BY_APPROVED_ID]
AS
SELECT     o.ID AS ORDER_ID, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS CUSTOMER_PERSON_NAME, c.NAME AS CUSTOMER_CO_NAME, h.ID, 
                      h.OBJECT_ID, h.CUSTOMER_PERSON, h.CUSTOMER_CO, h.DESCRIPTION, h.BUDGETARY_ONLY, h.EXPIRATION_DATE, h.DRCM, h.MODBY, 
                      h.PROGRESS, h.RFQ_ID, h.SUPPLIER_ID, h.CREATION_DATE, h.PERSON_SUPPLIER, h.TYPE, h.PRICE
FROM         dbo.A_ORDERS o INNER JOIN
                      dbo.A_ORDERS_HISTORY h ON o.HISTORY_REF_ID = h.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA c ON h.CUSTOMER_CO = c.ID INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON h.CUSTOMER_PERSON = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_SEARCH]
AS
SELECT     dbo.A_PROJECTS.ID, dbo.A_PROJECTS.NAME AS PROJECT_NAME, dbo.A_PROJECTS.DATE_CREATED, 
                      dbo.A_PROJECTS.ORIGINAL_PLANNED_STOP_DATE, dbo.A_PROJECTS.CURRENT_PLANNED_STOP_DATE, dbo.A_PROJECTS.ACTUAL_STOP_DATE, 
                      dbo.A_TASK_PRIORITY_LEVELS.NAME AS PRIORITY_NAME, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS LEADER_NAME, 
                      dbo.A_PROJECTS.SUMMARY
FROM         dbo.A_TASK_PRIORITY_LEVELS RIGHT OUTER JOIN
                      dbo.A_PROJECTS ON dbo.A_TASK_PRIORITY_LEVELS.ID = dbo.A_PROJECTS.PRIORITY_LEVEL LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_PROJECTS.LEADER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_SEARCH_ALL_ALLOWED]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_SEARCH_ALL_ALLOWED]
AS
SELECT     p.ID, p.NAME, p.DATE_CREATED, p.ORIGINAL_PLANNED_STOP_DATE, p.CURRENT_PLANNED_STOP_DATE, p.ACTUAL_STOP_DATE, 
                      p.PRIORITY_LEVEL AS PRIORITY, p.LEADER, p.SUMMARY, p.STATUS, dbo.A_PROJECT_ALLOWED_PEOPLE.PEOPLE_ID AS INV_PEOPLE_ID, 
                      dbo.A_PROJECT_ALLOWED_ROLES.ROLE_ID, dbo.A_PROJECT_ALLOWED_COMPANIES.COMPANY_ID, 
                      dbo.A_PROJECT_SPONSORS.PEOPLE_ID AS SPONSOR_ID, dbo.A_PROJECT_MEMBERS.PEOPLE_ID AS MEMBER_ID, p.INITIATOR, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS CURRENT_LEADER_NAME
FROM         dbo.A_PROJECTS p LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON p.LEADER = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID LEFT OUTER JOIN
                      dbo.A_PROJECT_MEMBERS ON p.ID = dbo.A_PROJECT_MEMBERS.PROJECT_ID LEFT OUTER JOIN
                      dbo.A_PROJECT_SPONSORS ON p.ID = dbo.A_PROJECT_SPONSORS.PROJECT_ID LEFT OUTER JOIN
                      dbo.A_PROJECT_ALLOWED_COMPANIES ON p.ID = dbo.A_PROJECT_ALLOWED_COMPANIES.PROJECT_ID LEFT OUTER JOIN
                      dbo.A_PROJECT_ALLOWED_PEOPLE ON p.ID = dbo.A_PROJECT_ALLOWED_PEOPLE.PROJECT_ID LEFT OUTER JOIN
                      dbo.A_PROJECT_ALLOWED_ROLES ON p.ID = dbo.A_PROJECT_ALLOWED_ROLES.PROJECT_ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_SEARCH_DATA]
AS
SELECT     m.ID, m.MESSAGE, m.SENDER, m.PARENT_ID, m.DATE_CREATED, m.STATUS, parent_message.MESSAGE AS PARENT_MESSAGE, m.DATE_SENT, 
                      dbo.A_FN_MESSAGES_HAS_CHILD_MESSAGE(m.ID) AS hasChild, sender_name.P_NAME AS SENDER_NAME, m.HIDE_MESSAGE, m.IMPORTANCE, 
                      m.TO_COUNT, m.TO_READ_COUNT, m.CC_COUNT, m.CC_READ_COUNT, m.TO_COUNT + ISNULL(m.CC_COUNT, 0) AS TOTAL_COUNT, 
                      m.TO_READ_COUNT + ISNULL(m.CC_READ_COUNT, 0) AS TOTAL_READ_COUNT, parent_message.SENDER AS PARENT_SENDER
FROM         dbo.A_MESSAGES parent_message RIGHT OUTER JOIN
                      dbo.A_MESSAGES m INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN sender_name ON m.SENDER = sender_name.P_ID ON parent_message.ID = m.PARENT_ID
GO

/****** Object:  View [dbo].[A_V_TASK_SEARCH_STREAM_LINED]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_SEARCH_STREAM_LINED]
AS
SELECT     TASK.DESCRIPTION, TASK.STATUS, TASK.REQUESTOR, TASK.CHILD_ORDER, TASK.CREATED_BY, TASK.CREATE_DATE, dbo.leadingSpaces(TASK.ID, 
                      50) AS SORT_ID, TASK.SYSTEM_TASK, TASK.PROCEDURE_ID, REQUESTOR.P_NAME AS REQUESTOR_NAME, TASK.REQUESTEE_ID, 
                      TASK.GROUP_REQUESTEE_ID, TASK.ORIG_PLANNED_START_DATE, TASK.ORIG_PLANNED_STOP_DATE, TASK.CUR_PLANNED_START_DATE, 
                      TASK.CUR_PLANNED_STOP_DATE, TASK.ACTUAL_START_DATE, TASK.ACTUAL_STOP_DATE, TASK.CUR_PLANNED_COUNTER_START, 
                      TASK.LATEST_REQUESTEE_NAME, TASK.HAS_DISCUSSION, TASK.HAS_SURVEY, TASK.HAS_CHILD, TASK.HAS_REF_PROC, TASK.HAS_FILE, 
                      TASK.ORIG_REQUESTOR_ID, TASK.HAS_REF_OBJ, TASK.HAS_MONITOR, ORIG_REQUESTOR.FULL_NAME AS ORIG_REQUESTOR_NAME, 
                      TASK.COLOR_CODE, TASK.LAST_REQUEST_DATE, dbo.getTaskParentList(TASK.ID) AS PARENT_LIST, TASK.PARENT_ID, dbo.isParentTask(TASK.ID) 
                      AS isParent, TASK.PRIORITY, dbo.A_TASK_PRIORITY_LEVELS.NAME AS PRIORITY_NAME, 
                      dbo.A_TASK_COMPANIES_TO_VIEW_LINK.CO_ID AS ALLOWED_CO_ID, TASK_CO.COMPANY_NAME, TASK.ID, TASK.CHILD_STATUS, 
                      TASK.PROCEDURE_STEP_ID, T_EMAIL_LINK.PERSON_ID AS SPECIFIED_PERSON_ID, dbo.A_TASK_COMMENT.COMMENT AS LAST_COMMENT, 
                      dbo.A_TASK_COMMENT.DRCM AS LAST_COMMENT_DRCM, TASK.IS_QUOTE, TASK.IS_QUOTE_ACCEPT, TASK.IS_FILL, 
                      TASK_CO.COMPANY_ID AS CO_ID, dbo.A_TASK_ORDER_INFORMATION.PURCHASE_HIST_ID, 
                      dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID, dbo.A_TASK_ORDER_INFORMATION.FILL_ID, 
                      dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ROLE, TASK.RECURSION_NUMBER, ASSIGNEE.FULL_NAME AS ASSIGNEE_NAME, 
                      ASSIGNEE.SYSTEM_STATUS AS ASSIGNEE_STATUS, dbo.A_PEOPLE_SEARCH_TABLE.NAME AS LAST_COMMENT_WRITER, 
                      dbo.A_TASK_PROCEDURE_ASSIGNEE.ASSIGNEE_ID
FROM         dbo.A_PEOPLE_SEARCH_TABLE INNER JOIN
                      dbo.A_TASK_LAST_COMMENT INNER JOIN
                      dbo.A_TASK_COMMENT ON dbo.A_TASK_LAST_COMMENT.COMMENT_ID = dbo.A_TASK_COMMENT.ID ON 
                      dbo.A_PEOPLE_SEARCH_TABLE.ID = dbo.A_TASK_COMMENT.WRITER RIGHT OUTER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE ASSIGNEE INNER JOIN
                      dbo.A_TASK_PROCEDURE_ASSIGNEE ON ASSIGNEE.ID = dbo.A_TASK_PROCEDURE_ASSIGNEE.ASSIGNEE_ID RIGHT OUTER JOIN
                      dbo.A_TASKS TASK ON dbo.A_TASK_PROCEDURE_ASSIGNEE.TASK_ID = TASK.ID LEFT OUTER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON TASK.ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID ON 
                      dbo.A_TASK_LAST_COMMENT.TASK_ID = TASK.ID LEFT OUTER JOIN
                      dbo.A_TASK_EMAIL_PEOPLE_LINK T_EMAIL_LINK ON TASK.ID = T_EMAIL_LINK.TASK_ID LEFT OUTER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE TASK_CO ON TASK.REQUESTEE_ID = TASK_CO.ID LEFT OUTER JOIN
                      dbo.A_TASK_COMPANIES_TO_VIEW_LINK ON TASK.ID = dbo.A_TASK_COMPANIES_TO_VIEW_LINK.TASK_ID LEFT OUTER JOIN
                      dbo.A_TASK_PRIORITY_LEVELS ON TASK.PRIORITY = dbo.A_TASK_PRIORITY_LEVELS.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ORIG_REQUESTOR ON TASK.ORIG_REQUESTOR_ID = ORIG_REQUESTOR.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN REQUESTOR ON TASK.REQUESTOR = REQUESTOR.P_ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALL_SEARCH_DATA]
AS
SELECT     sc.BOSS, sc.SUPPLIER_NAME, CONVERT(VARCHAR(50), sc.START_DATE, 1) AS START_DATE_STRING, sc.WORK_TYPE, 
                      dbo.A_FN_SERVICE_CALL_GET_STANDARD_SEARCH_FLAG(sc.STATUS) AS STANDARD_SEARCH, sc.CUSTOMER_NAME, sc.SUPPLIER_ID, 
                      sc.CUSTOMER_ID, sc.APPROVER_ROLE, sc.PAYER_ROLE, sc.APPROVER AS APPROVER_NAME, sc.PAYER AS PAYER_NAME, sc.WORKER_ID, sc.ID, 
                      sc.MACHINE_NAME, sc.FULL_NAME, sc.STATUS, sc.REASON, worker_name.P_NAME AS WORKER_NAME, sc.START_DAY, sc.START_YEAR, 
                      sc.START_MONTH, sc.START_DATE, ac_ar_link.RECIEVABLE_ROLE, sc.REASON_TYPE, ar.NAME AS AR_NAME, sc.COMMENTS, 
                      boss_name.P_NAME AS BOSS_NAME, sc.ORDER_NUMBER, dbo.A_PEOPLES_FAVORITES.[GROUP] AS GROUP_ID, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.FAV_TYPE, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_SERVICE_CALLS_TOTAL_HOURS.NORMAL_HOURS, 
                      dbo.A_SERVICE_CALLS_TOTAL_HOURS.OT_HOURS, dbo.A_SERVICE_CALLS_TOTAL_HOURS.TOTAL_HOURS, 
                      sc.EXPENSES_AMOUNT AS EXPENSES
FROM         dbo.A_V_PEOPLE_BY_NTLOGIN boss_name RIGHT OUTER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS INNER JOIN
                      dbo.A_PEOPLES_FAVORITES ON dbo.A_PEOPLES_FAVORITE_GROUPS.ID = dbo.A_PEOPLES_FAVORITES.[GROUP] RIGHT OUTER JOIN
                      dbo.A_V_SERVICE_CALLS_WITH_NORMAL_HOURS sc INNER JOIN
                      dbo.A_SERVICE_CALLS_TOTAL_HOURS ON sc.ID = dbo.A_SERVICE_CALLS_TOTAL_HOURS.WEEKLY_ID ON 
                      dbo.A_PEOPLES_FAVORITES.ITEM = sc.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN worker_name ON sc.WORKER_ID = worker_name.P_ID ON boss_name.P_ID = sc.BOSS LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA ar INNER JOIN
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE ac_ar_link ON ar.ID = ac_ar_link.RECIEVABLE_ROLE ON sc.SUPPLIER_ID = ac_ar_link.COMPANY_ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PEOPLE_SENT_TO_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_PEOPLE_SENT_TO_DATA]
AS
SELECT     dbo.A_MESSAGES_PEOPLE_LINK.PERSON_ID AS P_ID, dbo.A_V_PEOPLE_BY_NTLOGIN.P_NAME AS PERSON_NAME, 
                      dbo.A_MESSAGES_PEOPLE_LINK.MESSAGE_ID, dbo.A_MESSAGES_PEOPLE_LINK.IS_CC_MESSAGE, 
                      dbo.A_V_PEOPLE_BY_NTLOGIN.LAST_NAME
FROM         dbo.A_MESSAGES_PEOPLE_LINK INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN ON dbo.A_MESSAGES_PEOPLE_LINK.PERSON_ID = dbo.A_V_PEOPLE_BY_NTLOGIN.P_ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_PEOPLE_LINK_WITH_NAMES]
AS
SELECT     M.PERSON_ID, P.P_NAME AS RECIPIENT_NAME, M.MESSAGE_ID, M.IS_CC_MESSAGE, M.IS_READ, M.DRCM, M.MODBY, M.DATE_READ, M.ID, 
                      P.LAST_NAME
FROM         dbo.A_MESSAGES_PEOPLE_LINK M LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN P ON M.PERSON_ID = P.P_ID
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_HOST_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_MEETING_GET_HOST_NAME]
AS
SELECT     dbo.A_APPROVED_PEOPLE.FULL_NAME AS HOST_NAME, dbo.A_MEETINGS.ID, dbo.A_MEETINGS.HOST AS H_ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_MEETINGS ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_MEETINGS.HOST
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_SCRIBE_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_MEETING_GET_SCRIBE_NAME]
AS
SELECT     dbo.A_APPROVED_PEOPLE.FULL_NAME AS SCRIBE_NAME, dbo.A_MEETINGS.SCRIBE AS S_ID, dbo.A_MEETINGS.ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_MEETINGS ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_MEETINGS.SCRIBE
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_TKEEP_NAME]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_MEETING_GET_TKEEP_NAME]
AS
SELECT     dbo.A_APPROVED_PEOPLE.FULL_NAME AS TKEEP_NAME, dbo.A_MEETINGS.TIME_KEEP AS T_ID, dbo.A_MEETINGS.ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_MEETINGS ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_MEETINGS.TIME_KEEP
GO

/****** Object:  View [dbo].[A_V_MEETING_GET_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MEETING_GET_BY_ID]
AS
SELECT     dbo.A_MEETINGS.ID, dbo.A_MEETINGS.MEETING_NAME, dbo.A_MEETINGS.COMMENT, dbo.A_MEETINGS.START_DATE, 
                      dbo.A_MEETINGS.STOP_DATE, dbo.A_MEETINGS.OWNER, dbo.A_MEETINGS.SCRIBE, dbo.A_MEETINGS.LOCATION, 
                      dbo.A_MEETINGS.WEB_LOCATION, dbo.A_MEETINGS.DATE_CREATED, dbo.A_MEETINGS.STATUS, dbo.A_MEETINGS.DRCM, dbo.A_MEETINGS.HOST, 
                      dbo.A_MEETINGS.TIME_KEEP, dbo.A_V_LOCATION_WITH_NAME.LOCATION_NAME, dbo.A_V_MEETING_GET_HOST_NAME.HOST_NAME, 
                      dbo.A_V_MEETING_GET_TKEEP_NAME.TKEEP_NAME, dbo.A_V_MEETING_GET_SCRIBE_NAME.SCRIBE_NAME, dbo.A_MEETINGS.SETTING, 
                      initiator_name.P_NAME AS INITIATOR_NAME
FROM         dbo.A_MEETINGS INNER JOIN
                      dbo.A_V_PEOPLE_BY_NTLOGIN initiator_name ON dbo.A_MEETINGS.OWNER = initiator_name.P_ID LEFT OUTER JOIN
                      dbo.A_V_MEETING_GET_HOST_NAME ON dbo.A_MEETINGS.ID = dbo.A_V_MEETING_GET_HOST_NAME.ID LEFT OUTER JOIN
                      dbo.A_V_MEETING_GET_TKEEP_NAME ON dbo.A_MEETINGS.ID = dbo.A_V_MEETING_GET_TKEEP_NAME.ID LEFT OUTER JOIN
                      dbo.A_V_MEETING_GET_SCRIBE_NAME ON dbo.A_MEETINGS.ID = dbo.A_V_MEETING_GET_SCRIBE_NAME.ID LEFT OUTER JOIN
                      dbo.A_V_LOCATION_WITH_NAME ON dbo.A_MEETINGS.LOCATION = dbo.A_V_LOCATION_WITH_NAME.L_ID
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_ACCOUNT_PARENTS]
AS
SELECT     dbo.A_V_FORECAST_ITEMS.F_NAME, dbo.A_V_FORECAST_ITEMS.ID, dbo.A_V_FORECAST_ITEMS.START_DATE, 
                      dbo.A_V_FORECAST_ITEMS.STOP_DATE, dbo.A_V_FORECAST_ITEMS.ACCOUNT_ID, dbo.A_V_FORECAST_ITEMS.SUPPLIER_NAME, 
                      dbo.A_V_FORECAST_ITEMS.CUSTOMER_NAME, dbo.A_V_FORECAST_ITEMS.F_TYPE, dbo.A_V_FORECAST_ITEMS.QTY, 
                      dbo.A_V_FORECAST_ITEMS.F_AMT, dbo.A_V_FORECAST_ITEMS.PERCENT_OF_REV, dbo.A_V_FORECAST_ITEMS.PROGRESS, 
                      dbo.A_V_FORECAST_ITEMS.PRODUCT_NAME, dbo.A_V_FORECAST_ITEMS.ACCOUNT_NAME, 
                      A_V_FORECAST_ITEMS_1.ID AS PARENT_FORECAST_ITEM, dbo.A_V_FORECAST_ITEMS.F_OBJ_ID, dbo.A_V_FORECAST_ITEMS.AVG_MONTHLY, 
                      dbo.A_V_FORECAST_ITEMS.CONFIDENCE, dbo.A_V_FORECAST_ITEMS.FORECAST_ID, dbo.A_V_FORECAST_ITEMS.NOTE, 
                      dbo.A_V_FORECAST_ITEMS.AMT_INVOICED, dbo.A_V_FORECAST_ITEMS.DATE_ADDED, dbo.A_V_FORECAST_ITEMS.EST_QUAL_START_DATE, 
                      dbo.A_V_FORECAST_ITEMS.ACT_QUAL_START_DATE, dbo.A_V_FORECAST_ITEMS.EST_FIRST_PURCHASE_DATE, 
                      dbo.A_V_FORECAST_ITEMS.ACT_FIRST_PURCHASE_DATE, dbo.A_V_FORECAST_ITEMS.STATUS, dbo.A_V_FORECAST_ITEMS.PRIORITY, 
                      dbo.A_V_FORECAST_ITEMS.SUPPLIER_OWNER, dbo.A_V_FORECAST_ITEMS.CUSTOMER_OWNER
FROM         dbo.A_ACCOUNTS_HISTORY INNER JOIN
                      dbo.A_ACCOUNTS ON dbo.A_ACCOUNTS_HISTORY.ID = dbo.A_ACCOUNTS.HISTORY_REF_ID INNER JOIN
                      dbo.A_V_FORECAST_ITEMS ON dbo.A_ACCOUNTS.ID = dbo.A_V_FORECAST_ITEMS.ACCOUNT_ID LEFT OUTER JOIN
                      dbo.A_V_FORECAST_ITEMS A_V_FORECAST_ITEMS_1 ON 
                      dbo.A_ACCOUNTS_HISTORY.PARENT_ACCOUNT = A_V_FORECAST_ITEMS_1.ACCOUNT_ID AND 
                      dbo.A_V_FORECAST_ITEMS.F_OBJ_ID = A_V_FORECAST_ITEMS_1.F_OBJ_ID
GO

/****** Object:  View [dbo].[A_Z_UNITS_TIME_TO_SECS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_Z_UNITS_TIME_TO_SECS]
AS
SELECT     A_UNIT_TYPES_1.NAME AS FROM_NAME, dbo.A_UNIT_RELATIONS.FIRST_UNIT AS FROM_UNIT, 
                      dbo.A_UNIT_RELATIONS.SECOND_UNIT AS TO_UNIT, dbo.A_UNIT_TYPES.NAME AS TO_NAME, dbo.A_UNIT_RELATIONS.X_SECOND AS SECS
FROM         dbo.A_UNIT_TYPES INNER JOIN
                      dbo.A_UNIT_RELATIONS INNER JOIN
                      dbo.A_UNIT_TYPES A_UNIT_TYPES_1 ON dbo.A_UNIT_RELATIONS.FIRST_UNIT = A_UNIT_TYPES_1.ID ON 
                      dbo.A_UNIT_TYPES.ID = dbo.A_UNIT_RELATIONS.SECOND_UNIT
WHERE     (dbo.A_UNIT_RELATIONS.SECOND_UNIT IN ('TIME_SYS_SECONDS', 'UNIT', 'VOL_GALLONS', 'WT_OZ'))
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEPS]
AS
SELECT     
s.ID, s.PROCEDURE_ID, s.STEP_TEXT, s.START_ON_COUNTER, s.COUNTER_VALUE, s.REL_OR_ABS, s.COUNTER_UNIT, s.FROM_START_OR_STOP, 
s.SYSTEM_TASK as StepSystemTask, 
s.DESTINATION, 
s.SPECIFIC_LOCATION,
s.REFERENCE_VERB,
s.REFERENCE_OBJECT,
s.COMMENTS,
s.GOTO_STEP, 
s.GOTO_STEP_ID,
s.CYCLES,
s.CYCLE_ON_COUNTER,
s.CYCLE_COUNT,
s.CYCLE_UNIT, 
s.DRCM, 
s.MODBY, 
s.PRINT_ORDER, 
dbo.A_FN_PROCEDURE_STEPS_MAKE_PRECEDING_LIST(s.ID) AS PRE_STEP, s.DURATION, s.DURATION_TYPE, 
dbo.A_V_LOCATIONS_APPROVED_DATA.COMPLETE_NAME AS SPEC_LOC_NAME, dbo.A_Z_UNITS_TIME_TO_SECS.FROM_NAME AS DURATION_TYPE_NAME, 
dbo.A_Z_UNITS_TIME_TO_SECS.SECS,s.TITLE,
s.EquipmentTime,
s.Roles,
s.ReplacementCost,
s.Utilization,
s.UsefulLife,
CASE
    WHEN prevSteps.TITLE <> '' THEN prevSteps.TITLE
    ELSE 'N/A'
END AS PrevStepName
FROM dbo.A_PROCEDURE_STEPS s LEFT OUTER JOIN
                      dbo.A_Z_UNITS_TIME_TO_SECS ON s.DURATION_TYPE = dbo.A_Z_UNITS_TIME_TO_SECS.FROM_UNIT LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_APPROVED_DATA ON s.SPECIFIC_LOCATION = dbo.A_V_LOCATIONS_APPROVED_DATA.ID
OUTER APPLY (
			SELECT s1.TITLE FROM A_PROCEDURE_STEP_PRECEDING_STEPS ps
			INNER JOIN A_PROCEDURE_STEPS s1 ON s1.ID = ps.PREV_STEP
			WHERE ps.MY_STEP = s.ID AND ps.[PROCEDURE_ID] = s.PROCEDURE_ID
			) AS prevSteps
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE_STANDARDIZED]
AS
SELECT     TAB.ID, TAB.MIN_NUM * BASE.STD_UNIT_QTY AS MIN_NUM, TAB.MAX_NUM * BASE.STD_UNIT_QTY AS MAX_NUM, TAB.PER_UNIT_COST, 
                      TAB.FLAT_RATE, TAB.PER_UNIT_APPLIES_OVER * BASE.STD_UNIT_QTY AS PER_UNIT_APPLIES_OVER, PPLEC.UNIT AS ORIG_UNIT, 
                      BASE.STD_UNIT, TAB.PPLEC_ID, TAB.MIN_NUM AS Expr1, TAB.MAX_NUM AS Expr2, BASE.STD_UNIT_QTY, 
                      TAB.PER_UNIT_APPLIES_OVER AS Expr3
FROM         dbo.A_PROD_PRICE_LIST_EXTRA_COSTS_PRICE_TABLE TAB INNER JOIN
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS PPLEC ON TAB.PPLEC_ID = PPLEC.ID INNER JOIN
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER BASE ON PPLEC.UNIT = BASE.FROM_UNIT
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT]
AS
SELECT     P.ID AS PARENT_ID, P.HISTORY_REF_ID, C.ID AS CHILD_ID, C.PRODUCT, C.UNIT_PRICE, C.PRODUCT_NAME, C.SUPPLIER_ID, L.APPROVED_OBJ_ID, 
                      L.RELATIONSHIP, L.UNIT_TYPE, dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT_QTY, C.UNIT, 
                      L.QTY / dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT_QTY AS QTY
FROM         dbo.A_V_PROD_PRICE_LIST_BY_APPROVED_ID P INNER JOIN
                      dbo.A_PROD_PRICE_LIST_SUB_PRICE_LISTS L ON P.HISTORY_REF_ID = L.PARENT INNER JOIN
                      dbo.A_V_PROD_PRICE_LIST_BY_APPROVED_ID C ON L.CHILD = C.ID INNER JOIN
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER ON C.UNIT = dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.FROM_UNIT
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_OBJ_LINK_DATA_FOR_SUMMING]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROCEDURE_OBJ_LINK_DATA_FOR_SUMMING]
AS
SELECT     l.ID, ISNULL(l.QTY * dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT_QTY, 0) AS QTY, l.PROCEDURE_ID, l.RELATIONSHIP, 
                      l.APPROVED_OBJECT_ID, dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT, dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.STD_UNIT_QTY, 
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.UNIT_TYPE
FROM         dbo.A_PROCEDURE_OBJECT_LINK l INNER JOIN
                      dbo.A_Z_UNITS_BASE_UNIT_CONVERTER ON l.QTY_TYPE = dbo.A_Z_UNITS_BASE_UNIT_CONVERTER.FROM_UNIT
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_CHILDREN_PROD_FILLS]
AS
SELECT     OBJ.OBJ_DESC, L.ID, L.PARENT, L.CHILD, CHILD.PRODUCT, CHILD.UNIT_PRICE, CHILD.PRODUCT_NAME, CHILD.UNIT AS PROD_UNIT, 
                      L.APPROVED_OBJ_ID, L.QTY, L.RELATIONSHIP, CHILD.UNIT_PRICE * (L.QTY / b.STD_UNIT_QTY) AS PRICE, b.STD_UNIT_QTY
FROM         dbo.A_Z_UNITS_BASE_UNIT_CONVERTER b RIGHT OUTER JOIN
                      dbo.A_V_PROD_PRICE_LIST_BY_APPROVED_ID CHILD ON b.FROM_UNIT = CHILD.UNIT RIGHT OUTER JOIN
                      dbo.A_OBJECTS OBJ INNER JOIN
                      dbo.A_PROD_PRICE_LIST_SUB_PRICE_LISTS L ON OBJ.ID = L.APPROVED_OBJ_ID ON CHILD.ID = L.CHILD
GO

/****** Object:  View [dbo].[A_O_PURCHASES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PURCHASES]
AS
SELECT     PH.ID, PH.DATE_CREATED, PH.PURCHASE_STATUS, PH.ORDER_ID, PH.DRCM, PH.MODBY, PH.OBJECT_ID, O.LOCKED_BY, O.UNLOCKED_BY, 
                      O.CREATED_BY, O.CREATE_DATE, O.ROOT, O.CREATING_CO, O.STATUS, O.LOCKED_BY_NAME, O.CREATING_CO_NAME, OH.CUSTOMER_PERSON, 
                      OH.CUSTOMER_CO, OH.DESCRIPTION, PH.PURCHASE_TOTAL, PH.PURCHASER, PH.PURCHASING_CO, PH.CUST_PURCH_NUM, 
                      PH.SUP_PURCH_NUM, O.REV, PH.ACCT_FOR_ALL, PH.DUE_DATE
FROM         dbo.A_PURCHASES_HISTORY PH INNER JOIN
                      dbo.A_OBJECTS O ON PH.OBJECT_ID = O.ID INNER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA OH ON PH.ORDER_ID = OH.ID
GO

/****** Object:  View [dbo].[Portal_PurchaseView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PurchaseView]
AS

SELECT DISTINCT
Id,
OBJECT_ID AS ObjectId,
CUST_PURCH_NUM AS CustPurchNum,
Description,
PURCHASE_STATUS AS PurchaseStatus,
Status,
DATE_CREATED AS DateCreated,
ROOT,
ID
SUP_PURCH_NUM
FROM A_O_PURCHASES WHERE ID IS NOT NULL
GO

/****** Object:  View [dbo].[A_APPROVED_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_APPROVED_LOCATIONS]
AS
SELECT     l.ID, lh.NAME, lh.PARENT_LOCATION, lh.PARENT_LOCATION_NAME, lh.ADDRESS_1, lh.ADDRESS_2, lh.FULL_ADDRESS, lh.CITY, lh.STATE, 
                      lh.COUNTRY, lh.POSTAL_CODE, lh.REGION, lh.REGION_NAME, lh.INTERNAL_ADDRESS, lh.OBJECT_ID, lh.DRCM, lh.MODBY, lh.PARENT_PATH, 
                      o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY
FROM         dbo.A_LOCATIONS_HISTORY lh INNER JOIN
                      dbo.A_LOCATIONS l ON lh.ID = l.HISTORY_REF_ID INNER JOIN
                      dbo.A_OBJECTS o ON lh.OBJECT_ID = o.ID
GO

/****** Object:  View [dbo].[A_V_COMPANIES_LOCATION_ADDRESSES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_COMPANIES_LOCATION_ADDRESSES]
AS
SELECT     dbo.A_APPROVED_LOCATIONS.STATE, dbo.A_APPROVED_LOCATIONS.CITY, dbo.A_APPROVED_LOCATIONS.FULL_ADDRESS, 
                      dbo.A_APPROVED_LOCATIONS.COUNTRY, dbo.A_APPROVED_COMPANIES.ID, dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME, 
                      dbo.A_APPROVED_LOCATIONS.NAME AS LOCATION_NAME, dbo.A_APPROVED_LOCATIONS.ADDRESS_1, dbo.A_APPROVED_LOCATIONS.ADDRESS_2, 
                      dbo.A_APPROVED_LOCATIONS.POSTAL_CODE, dbo.A_APPROVED_LOCATIONS.REGION, dbo.A_APPROVED_LOCATIONS.REGION_NAME, 
                      dbo.A_APPROVED_LOCATIONS.INTERNAL_ADDRESS, dbo.A_APPROVED_COMPANIES.PHONE
FROM         dbo.A_APPROVED_COMPANIES LEFT OUTER JOIN
                      dbo.A_APPROVED_LOCATIONS ON dbo.A_APPROVED_COMPANIES.LOCATION = dbo.A_APPROVED_LOCATIONS.ID
GO

/****** Object:  View [dbo].[A_V_APPROVED_COMPANIES_WITH_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_APPROVED_COMPANIES_WITH_LOGOS]
AS
SELECT     dbo.A_COMPANIES.ID, dbo.A_DOCUMENT_LINK.LINKED_DOC_ID, dbo.A_DOCUMENT_LINK.TYPE, dbo.A_COMPANIES.HISTORY_REF_ID, 
                      dbo.A_COMPANIES_HISTORY.OBJECT_ID AS OBJID, dbo.A_V_LOCATIONS_BY_APPROVED_ID.NAME AS LOC_NAME, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.ADDRESS_1, dbo.A_V_LOCATIONS_BY_APPROVED_ID.FULL_ADDRESS, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.ADDRESS_2, dbo.A_V_LOCATIONS_BY_APPROVED_ID.CITY, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.STATE, dbo.A_V_LOCATIONS_BY_APPROVED_ID.COUNTRY, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.POSTAL_CODE, dbo.A_V_LOCATIONS_BY_APPROVED_ID.INTERNAL_ADDRESS, 
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID.PARENT_PATH, dbo.A_COMPANIES_HISTORY.LOCATION, dbo.A_COMPANIES_HISTORY.NAME AS CO_NAME, 
                      dbo.A_COMPANIES_HISTORY.PHONE, dbo.A_COMPANIES.STATUS
FROM         dbo.A_COMPANIES_HISTORY INNER JOIN
                      dbo.A_COMPANIES ON dbo.A_COMPANIES_HISTORY.ID = dbo.A_COMPANIES.HISTORY_REF_ID INNER JOIN
                      dbo.A_DOCUMENT_LINK ON dbo.A_COMPANIES_HISTORY.OBJECT_ID = dbo.A_DOCUMENT_LINK.OBJECT_ID LEFT OUTER JOIN
                      dbo.A_V_LOCATIONS_BY_APPROVED_ID ON dbo.A_COMPANIES_HISTORY.LOCATION = dbo.A_V_LOCATIONS_BY_APPROVED_ID.ID
WHERE     (dbo.A_DOCUMENT_LINK.TYPE = N'LOGO') AND (dbo.A_COMPANIES.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_PAYMENT_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALL_PAYMENT_LOCATIONS]
AS
SELECT     ar_role.RECIEVABLE_ROLE, ar_role.PAYMENT_LOCATION, ar_locaiton.NAME AS SUPPLIER_LOCATION_NAME, 
                      company_logo_phone.LINKED_DOC_ID, ar_locaiton.ADDRESS_1 AS SUPPLIER_ADDRESS, ar_locaiton.CITY AS SUPPLIER_CITY, 
                      ar_locaiton.STATE AS SUPPLIER_STATE, ar_locaiton.POSTAL_CODE AS SUPPLIER_POSTAL_CODE, 
                      company_logo_phone.PHONE AS SUPPLIER_PHONE, company_logo_phone.ID AS COMPANY_ID
FROM         dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE ar_role INNER JOIN
                      dbo.A_APPROVED_LOCATIONS ar_locaiton ON ar_role.PAYMENT_LOCATION = ar_locaiton.ID LEFT OUTER JOIN
                      dbo.A_V_APPROVED_COMPANIES_WITH_LOGOS company_logo_phone ON ar_role.COMPANY_ID = company_logo_phone.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_INVOICE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALL_INVOICE_DATA]
AS
SELECT     dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.ID, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.WORKER_ID, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.WORKER_NAME, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.NORMAL_HOURS, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.CUSTOMER_NAME, ca.ADDRESS_1 AS CUSTOMER_ADDRESS, ca.CITY AS CUSTOMER_CITY, 
                      ca.STATE AS CUSTOMER_STATE, ca.POSTAL_CODE AS CUSTOMER_POSTAL_CODE, ca.PHONE AS CUSTOMER_PHONE, 
                      supplier_payment_location.RECIEVABLE_ROLE, supplier_payment_location.PAYMENT_LOCATION, 
                      supplier_payment_location.SUPPLIER_LOCATION_NAME, supplier_payment_location.SUPPLIER_ADDRESS, 
                      supplier_payment_location.SUPPLIER_CITY, supplier_payment_location.SUPPLIER_STATE, supplier_payment_location.SUPPLIER_POSTAL_CODE, 
                      supplier_payment_location.SUPPLIER_PHONE, supplier_payment_location.LINKED_DOC_ID AS LOGO_ID, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.HOUR_RATE, dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.BOSS, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.RECEIVABLE_ROLE, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.PAYER_ROLE, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.APPROVER_ROLE, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.SUPPLIER_ID, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.CUSTOMER_ID, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.ACTUAL_START_DATE, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.STATUS, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.SUPPLIER_NAME, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.INVOICE_DATE, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.ORDER_NUMBER, dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.WORK_TYPE_NAME, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.HOUR_RATE * ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.NORMAL_HOURS, 0) 
                      + ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_AMOUNT, 0) AS SUB_TOTAL, 
                      (ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.HOUR_RATE, 0) * ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.NORMAL_HOURS, 0) 
                      + ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_AMOUNT, 0)) * (ISNULL(dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, 0) / 100) 
                      AS TAXES, ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.HOUR_RATE, 0) * ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.NORMAL_HOURS, 
                      0) + ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_AMOUNT, 0) + (ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.HOUR_RATE, 0) 
                      * ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.NORMAL_HOURS, 0) + ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_AMOUNT, 
                      0)) * (ISNULL(dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, 0) / 100) AS TOTAL, 
                      ISNULL(dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_AMOUNT, 0) AS EXPENSES_AMOUNT, 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.EXPENSES_DESCRIPTION
FROM         dbo.A_V_SERVICE_CALLS_WEEKLY_DATA INNER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES ON 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.WORK_TYPE = dbo.A_SERVICE_CALLS_WORK_TYPES.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_LOCATION_ADDRESSES ca ON dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.CUSTOMER_ID = ca.ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_PAYMENT_LOCATIONS supplier_payment_location ON 
                      dbo.A_V_SERVICE_CALLS_WEEKLY_DATA.SUPPLIER_ID = supplier_payment_location.COMPANY_ID
GO

/****** Object:  View [dbo].[A_O_REGIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_O_REGIONS]
AS
SELECT     r.*, o.ID AS OBJ_ID, o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, 
                      o.CREATE_DATE AS CREATE_DATE, o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, 
                      o.REV AS REV, o.WFS_ID AS WFS_ID, o.LOCKED_BY_NAME AS LOCKED_BY_NAME, o.CREATING_CO_NAME AS CREATING_CO_NAME
FROM         dbo.A_REGIONS_HISTORY r INNER JOIN
                      dbo.A_OBJECTS o ON r.OBJECT_ID = o.ID
GO

/****** Object:  View [dbo].[Portal_RegionsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_RegionsView]
AS
SELECT        ID AS Id, NAME AS Name, MODBY AS ModBy, DRCM AS Drcm, OBJECT_ID AS ObjectId, OBJ_ID AS ObjId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, CREATED_BY AS CreatedBy, 
                         CREATE_DATE AS CreatedDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatedCo, STATUS AS Status, REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, 
                         CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_O_REGIONS
GO

/****** Object:  View [dbo].[Portal_ProductsSearchDataView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE View [dbo].[Portal_ProductsSearchDataView]
AS

SELECT     
o.Id,
'(' + orders.CUSTOMER_ROOT_CO_NAME + ') ' + ph.NAME +  ' (R-' + CONVERT(varchar(MAX),o.rev) + ') [supplier: ' + ad.NAME + ']' AS NAME,
ph.SUPPLIER_ID AS SupplierId,
pqp.CUST_ID AS CustomerId,
pqp.Order_Id AS OrderId,
o.STATUS AS Status
FROM dbo.A_OBJECTS o
INNER JOIN dbo.A_PRODUCTS_HISTORY ph ON o.ID = ph.OBJECT_ID
INNER JOIN dbo.A_V_COMPANIES_APPROVED_DATA ad ON ph.SUPPLIER_ID = ad.ID 
INNER JOIN dbo.A_V_PROCEDURES_APPROVED_DATA pad ON ph.PROCEDURE_ID = pad.ID 
INNER JOIN  dbo.A_PRODUCTS_QUICK_PRICE pqp ON ph.ID = PQP.PROD_HIST_ID
INNER JOIN [A_O_ORDERS] orders ON orders.OBJECT_ID = pqp.Order_Id
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_PERSON_TIME_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALL_PERSON_TIME_DATA]
AS
SELECT     WORKER.ID AS PERSON_ID, dbo.A_PEOPLE_HISTORY.NAME, dbo.A_PEOPLE_HISTORY.LAST_NAME, CONVERT(dateTime, CONVERT(varchar(10), 
                      tim.MO) + '/' + CONVERT(varchar(10), tim.D) + '/' + CONVERT(varchar(10), tim.YR)) AS ACTUAL_DATE, tim.HOUR_TYPE, tim.WEEKLY_ID, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.STATUS, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORK_TYPE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.WORK_TYPE_NAME, dbo.A_SERVICE_CALLS_WORK_TYPES.APPROVER_ROLE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.PAYER_ROLE, dbo.A_PEOPLE_HISTORY.BOSS, 
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.RECIEVABLE_ROLE, cust.NAME AS CUSTOMER_NAME, sup.NAME AS SUPPLIER_NAME, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ACTUAL_START_DATE, dbo.A_SERVICE_CALLS_WORK_TYPES.HOUR_RATE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.OT_RATE, dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, dbo.A_SERVICE_CALLS_WORK_TYPES.HIDE, 
                      dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS BOSS_NAME, dbo.A_FN_MAKE_ZERO_NULL(tim.HOURS) AS HOURS, tim.ID AS TID, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID, dbo.A_SERVICE_CALLS_WORK_TYPES.CUSTOMER_ID, 
                      dbo.A_PEOPLE_SEARCH_TABLE.LOCATION_ID, dbo.A_PEOPLE_SEARCH_TABLE.LOCATION_NAME
FROM         dbo.A_SERVICE_CALL_WORK_TIME tim INNER JOIN
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS ON tim.WEEKLY_ID = dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID INNER JOIN
                      dbo.A_PEOPLE WORKER ON dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORKER_ID = WORKER.ID INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON WORKER.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES ON 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORK_TYPE = dbo.A_SERVICE_CALLS_WORK_TYPES.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA cust ON dbo.A_SERVICE_CALLS_WORK_TYPES.CUSTOMER_ID = cust.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA sup ON dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID = sup.ID LEFT OUTER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE ON dbo.A_PEOPLE_HISTORY.ID = dbo.A_PEOPLE_SEARCH_TABLE.ID LEFT OUTER JOIN
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE ON 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID = dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.COMPANY_ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON dbo.A_PEOPLE_HISTORY.BOSS = dbo.A_V_PEOPLE_APPROVED_DATA.ID
WHERE     (WORKER.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[Portal_HeadPeopleView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_HeadPeopleView]
	AS 
	SELECT p.ID AS Id,

p.FULL_NAME AS FullName
FROM A_COMPANY_HEAD_PEOPLE h,A_V_PEOPLE_APPROVED_DATA p
GO

/****** Object:  View [dbo].[A_V_LINKED_LOGINS_PEOPLE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LINKED_LOGINS_PEOPLE_DATA]
AS
SELECT     dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS ROOT_NAME, dbo.A_V_PEOPLE_APPROVED_DATA.ROOT_COMPANY AS ROOT_CO, 
                      A_V_PEOPLE_APPROVED_DATA_1.FULL_NAME AS LINKED_NAME, dbo.A_PEOPLE_LINKED_LOGINS.ROOT_ID, 
                      dbo.A_PEOPLE_LINKED_LOGINS.LINKED_ID, A_V_PEOPLE_APPROVED_DATA_1.ROOT_COMPANY AS LINKED_CO
FROM         dbo.A_PEOPLE_LINKED_LOGINS INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON dbo.A_PEOPLE_LINKED_LOGINS.ROOT_ID = dbo.A_V_PEOPLE_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA A_V_PEOPLE_APPROVED_DATA_1 ON 
                      dbo.A_PEOPLE_LINKED_LOGINS.LINKED_ID = A_V_PEOPLE_APPROVED_DATA_1.ID
GO

/****** Object:  View [dbo].[A_V_NOTES_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_NOTES_SEARCH_DATA]
AS
SELECT     dbo.A_NOTES.ID, dbo.A_NOTES.TXT, dbo.A_NOTES.PEOPLE_SECURE, dbo.A_NOTES.COMPANY_SECURE, dbo.A_NOTES.ROLE_SECURE, 
                      dbo.A_NOTES.SECURITY_LEVEL, dbo.A_NOTES.NOTE_TYPE, dbo.A_NOTES.SECURITY_TYPE, dbo.A_NOTES.RESPONSE_ALLOWED, 
                      dbo.A_NOTES.STATUS, dbo.A_NOTES.AUTHOR, dbo.A_NOTES.REVISION, dbo.A_NOTES.TO_READ_COUNT, dbo.A_NOTES.DATE_CREATED, 
                      dbo.A_NOTES.HIDE_NOTE, dbo.A_NOTES.DATE_SENT, dbo.A_NOTES.NOTIFY, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_LEVEL_NAME, 
                      dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS AUTHOR_NAME
FROM         dbo.A_NOTES INNER JOIN
                      dbo.A_SECURITY_LEVELS ON dbo.A_NOTES.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON dbo.A_NOTES.AUTHOR = dbo.A_V_PEOPLE_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_BASIC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_SERVICE_CALLS_BASIC_DATA]
AS
SELECT     dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME AS WORKER_NAME, A_V_PEOPLE_APPROVED_DATA_1.FULL_NAME AS BOSS_NAME, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORKER_ID, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.MACHINE_NAME, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.COMMENTS, 
                      Sup.NAME AS SUPPLIER_NAME, Cust.NAME AS CUSTOMER_NAME, dbo.A_SERVICE_CALLS_WORK_TYPES.WORK_TYPE_NAME, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.INVOICE_DATE, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ORDER_NUMBER, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_DAY, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_MONTH, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_YEAR, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.EXPENSES_DESCRIPTION, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.EXPENSES_AMOUNT, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.HOUR_RATE_FIXED, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.OT_RATE_FIXED, dbo.A_SERVICE_CALLS_WORK_TYPES.HOUR_RATE, 
                      dbo.A_SERVICE_CALLS_WORK_TYPES.OT_RATE, dbo.A_SERVICE_CALLS_WORK_TYPES.TAX_RATE, CONVERT(dateTime, CONVERT(varchar(2), 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_MONTH) + '/' + CONVERT(varchar(2), dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_DAY) 
                      + '/' + CONVERT(varchar(4), dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.START_YEAR)) AS START_DATE, 
                      dbo.A_V_PEOPLE_APPROVED_DATA.LAST_NAME AS WORKER_LAST_NAME, dbo.A_V_PEOPLE_APPROVED_DATA.NAME AS WORKER_FIRST_NAME, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.STATUS
FROM         dbo.A_SERVICE_CALLS_WEEKLY_REPORTS INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA ON 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORKER_ID = dbo.A_V_PEOPLE_APPROVED_DATA.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES ON 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.WORK_TYPE = dbo.A_SERVICE_CALLS_WORK_TYPES.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA Sup ON dbo.A_SERVICE_CALLS_WORK_TYPES.SUPPLIER_ID = Sup.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA Cust ON dbo.A_SERVICE_CALLS_WORK_TYPES.CUSTOMER_ID = Cust.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA A_V_PEOPLE_APPROVED_DATA_1 ON 
                      dbo.A_V_PEOPLE_APPROVED_DATA.BOSS = A_V_PEOPLE_APPROVED_DATA_1.ID
GO

/****** Object:  View [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFORMATION]
AS
SELECT     dbo.A_ACCOUNT_INVOICE_ITEMS.ID, dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID, dbo.A_ACCOUNT_INVOICE_ITEMS.ACCOUNT_ID, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.PURCH_ITEM_ID, dbo.A_ACCOUNT_INVOICE_ITEMS.STATUS, dbo.A_ACCOUNT_INVOICE_ITEMS.DRCM, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.MODBY, dbo.A_ACCOUNT_INVOICE_ITEMS.AMOUNT, dbo.A_ACCOUNT_INVOICE_ITEMS.DESCRIPTION, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.DATE_INVOICED, dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.PAYMENT_METHOD, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.CHECK_NO, dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.EXP_MO, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.EXP_YEAR, dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.RECORDED_BY, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.CC_NO, dbo.A_V_PEOPLE_APPROVED_DATA.FULL_NAME, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.DATE_POSTED, dbo.A_ACCOUNT_INVOICE_ITEMS.COMMENTS
FROM         dbo.A_V_PEOPLE_APPROVED_DATA INNER JOIN
                      dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO ON 
                      dbo.A_V_PEOPLE_APPROVED_DATA.ID = dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.RECORDED_BY INNER JOIN
                      dbo.A_ACCOUNT_INVOICE_ITEMS ON dbo.A_ACCOUNT_INVOICE_ITEMS_PAYMENT_INFO.INVOICE_ITEM_ID = dbo.A_ACCOUNT_INVOICE_ITEMS.ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_QUOTE_HISTORY_SEARCH]
AS
SELECT     dbo.A_QUOTES_HISTORY.ID, dbo.A_QUOTES_HISTORY.OBJECT_ID, dbo.A_QUOTES_HISTORY.DESCRIPTION, 
                      dbo.A_QUOTES_HISTORY.PROGRESS AS QUOTE_PROGRESS, dbo.A_QUOTES_HISTORY.ORDER_ID, 
                      CUST_PERSON.FULL_NAME AS CUST_PERSON_NAME, dbo.A_QUOTES_HISTORY.CUSTOMER_PERSON AS CUST_PERSON_ID, 
                      dbo.A_QUOTES_HISTORY.CUSTOMER_CO AS CUST_CO_ID, CUST.NAME AS CUST_CO_NAME, dbo.A_QUOTES_HISTORY.SUPPLIER_ID, 
                      SUP.NAME AS SUPPLIER_NAME, dbo.A_QUOTES_HISTORY.BUGETARY_ONLY, dbo.A_QUOTES_HISTORY.EXPIRATION_DATE, 
                      dbo.A_QUOTES_HISTORY.CREATION_DATE, dbo.A_ORDERS_HISTORY.DESCRIPTION AS ORDER_DESCRIPTION, dbo.A_OBJECTS.LOCKED_BY, 
                      dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO,
                       dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, dbo.A_OBJECTS.WFS_ID, dbo.A_OBJECTS.LOCKED_BY_NAME, 
                      dbo.A_OBJECTS.CREATING_CO_NAME, dbo.A_OBJECTS.APPROVAL_ACTIVITY, dbo.A_OBJECTS.APPROVAL_DATE, 
                      dbo.A_QUOTES_HISTORY.OBJECT_ID AS OBJ_ID
FROM         dbo.A_QUOTES_HISTORY INNER JOIN
                      dbo.A_ORDERS ON dbo.A_QUOTES_HISTORY.ORDER_ID = dbo.A_ORDERS.ID INNER JOIN
                      dbo.A_ORDERS_HISTORY ON dbo.A_ORDERS.HISTORY_REF_ID = dbo.A_ORDERS_HISTORY.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUST ON dbo.A_QUOTES_HISTORY.CUSTOMER_CO = CUST.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUP ON dbo.A_QUOTES_HISTORY.SUPPLIER_ID = SUP.ID INNER JOIN
                      dbo.A_V_PEOPLE_APPROVED_DATA CUST_PERSON ON dbo.A_QUOTES_HISTORY.CUSTOMER_PERSON = CUST_PERSON.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_QUOTES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_LINKED_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LINKED_IDS]
AS
SELECT     TOP 100 PERCENT ll.ID, ll.ROOT_ID, ll.LINKED_ID, ll.NICK_NAME, ll.LINKED_PASS, ll.DRCM, ll.MODBY, 
                      dbo.A_COMPANIES_IMPORT.EXTERNAL_ID AS EXT_CO_ID
FROM         dbo.A_V_PEOPLE_APPROVED_DATA p INNER JOIN
                      dbo.A_PEOPLE_LINKED_LOGINS ll ON p.ID = ll.LINKED_ID LEFT OUTER JOIN
                      dbo.A_COMPANIES_IMPORT ON p.ROOT_COMPANY = dbo.A_COMPANIES_IMPORT.ROOT_ID
ORDER BY p.NICK_NAME
GO

/****** Object:  View [dbo].[Portal_InvoiceView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE view [dbo].[Portal_InvoiceView]
AS

SELECT  
(LEFT( i.InvoiceClass,2)) +'-'+RIGHT(YEAR(i.InvoiceDate),2) +'-'+ REPLACE(STR(i.Id, 5), SPACE(1), '0')  as InvoiceNumber,
i.Id,
i.Description,
i.Status,
i.CustPo,
i.InvoiceDate,
i.SubTotal,
i.Total,
i.Tax,
i.PaymentsAndCredits,
i.Debits,
i.LateFees,
i.Items,
i.Supplier,
i.InvoiceClass,
c.ROOT_NAME AS Client
FROM 
Portal_Invoice AS i
left JOIN A_V_COMPANIES_DROP_SEARCH c ON c.ID = i.client
GO

/****** Object:  View [dbo].[A_V_MONITOR_TEMPLATES_WITH_RESULTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE   VIEW [dbo].[A_V_MONITOR_TEMPLATES_WITH_RESULTS]
AS
SELECT     t.ID, t.MONITOR_TYPE, t.Input_type, t.DESCRIPTION, t.START_SYSTEM_TASK, t.START_TYPE, t.STOP_SYSTEM_TASK, t.STOP_TYPE, t.COUNTER_OR_CLOCK, 
                      t.CLOCK_UNIT, t.HIGHEST_THRESHOLD, t.HIGH_THRESHOLD, t.TARGET, t.LOW_THRESHOLD, t.LOWEST_THRESHOLD, t.SHOULD_BE, t.OPINION, 
                      t.TARGET_ANSWER_ID, t.RELATED_OBJECT_TYPE, t.RELATED_OBJECT_DESCRIPTION, t.HIDE_TARGET, t.USE_RESULT, t.FAIL_STOP, 
                      t.YES_NO_ANSWER, t.CORRECT_ANSWER_ID, t.TEXT_TARGET, t.TASK_ID, t.ROLL_UP_ID, r.NUM_VAL, r.TEXT_VAL, r.MULT_CHOICE_ANSWER, 
                      r.PRINT_RESULT, r.COMMENT, t.CREATED_BY, t.IS_PASSING, t.PROCEDURE_ID, t.STEP_ID, t.PEOPLE_ID, t.PART_ID, t.OBJECT_ID, t.IS_AUTO, 
                      t.MY_ANSWER, t.TOLERANCE, t.RELATED_OBJECT_ID, t.FAIL_ACTION, t.TARGET_OBJECT_TYPE, t.TARGET_OBJECT,t.PRINT_ORDER,t.CANT_CHANGE,t.ALWAYS_PASS
FROM         dbo.A_MONITOR_TEMPLATES t LEFT OUTER JOIN
                      dbo.A_MONITOR_RESULTS r ON t.ID = r.MONITOR_TEMPLATE_ID
GO

/****** Object:  View [dbo].[Portal_MonitorResults]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_MonitorResults]

AS
SELECT
Id,
TASK_ID AS TaskId,
MONITOR_TYPE AS MonitorType
,DESCRIPTION AS Description
,SHOULD_BE AS ShouldBe
,OPINION AS Opinion
,HIDE_TARGET AS HideTarget
,USE_RESULT AS UseResult
,FAIL_STOP AS FailStop
,YES_NO_ANSWER AS YesNoAnswer
,TEXT_VAL As TextVal
,CASE 
WHEN MONITOR_TYPE = 'YES_NO' THEN REPLACE (REPLACE (YES_NO_ANSWER, 1, 'Yes'), 0, 'No') 
WHEN MONITOR_TYPE = 'USER_NUMBER' THEN CAST(NUM_VAL AS NVARCHAR) 
WHEN MONITOR_TYPE = 'NUMBER' THEN CAST(NUM_VAL AS NVARCHAR)
WHEN MONITOR_TYPE = 'TEXT' THEN TEXT_VAL 
ELSE PRINT_RESULT END AS PrintResult
,COMMENT AS Comment,
CASE
WHEN (MONITOR_TYPE = 'TEXT' AND (TEXT_TARGET IS NULL OR TEXT_TARGET = ''))  AND IS_PASSING  = 0 THEN  'Pass'
WHEN (MONITOR_TYPE = 'TEXT' AND (TEXT_TARGET IS NOT NULL OR TEXT_TARGET <> ''))  AND IS_PASSING  = 0 THEN  'Fail'
ELSE  REPLACE (REPLACE (IS_PASSING, 1, 'Pass'), 0, 'Fail')
END AS IsPassing
,MY_ANSWER AS MyAnswer
,FAIL_ACTION AS FailAction
,PRINT_ORDER AS PrintOrder
,CANT_CHANGE AS CantChange
,ALWAYS_PASS AS AlwaysPass
FROM
[dbo].A_V_MONITOR_TEMPLATES_WITH_RESULTS

--  exec A_SP_MONITOR_TEMPLATES_GET_DATA_FOR_OBJECT NULL,NULL,'111991','1618'
GO

/****** Object:  View [dbo].[A_V_WF_GROUPS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WF_GROUPS]
AS
SELECT     dbo.A_OBJECTS.OBJ_TABLE, dbo.A_OBJECTS.ID AS OBJ_ID, dbo.A_OBJECTS.OBJ_ID AS ID, dbo.A_OBJECTS.OBJ_DESC, dbo.A_OBJECTS.DRCM, 
                      dbo.A_OBJECTS.MODBY, dbo.A_OBJECTS.CO_PART_NUM, dbo.A_OBJECTS.PART_TYPE, dbo.A_OBJECTS.PART_CO, dbo.A_OBJECTS.LOCKED_BY, 
                      dbo.A_OBJECTS.UNLOCKED_BY, dbo.A_OBJECTS.CREATED_BY, dbo.A_OBJECTS.CREATE_DATE, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.REV_INFO,
                       dbo.A_OBJECTS.CREATING_CO, dbo.A_OBJECTS.STATUS, dbo.A_OBJECTS.REV, dbo.A_OBJECTS.WFS_ID, dbo.A_WF_GROUPS.NAME, 
                      dbo.A_WF_GROUPS.HIDE
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_WF_GROUPS ON dbo.A_OBJECTS.ID = dbo.A_WF_GROUPS.OBJECT_ID
WHERE     (dbo.A_OBJECTS.OBJ_TABLE = N'A_WF_GROUPS')
GO

/****** Object:  View [dbo].[Portal_ApprovalGroupsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ApprovalGroupsView]
AS
SELECT        OBJ_TABLE AS ObjectTable, OBJ_ID AS ObjId, ID AS Id, OBJ_DESC AS ObjDesc, DRCM AS Drcm, MODBY AS ModBy, CO_PART_NUM AS CoPartNum, PART_TYPE AS PartType, PART_CO AS PartCo, LOCKED_BY AS LockedBy, 
                         UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, REV AS Rev, WFS_ID AS WfsId, 
                         NAME AS Name, HIDE AS Hide
FROM            dbo.A_V_WF_GROUPS
GO

/****** Object:  View [dbo].[Portal_ProceduresVerbsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ProceduresVerbsView]
AS
SELECT        ID AS Id,
              NAME AS Name,
			  OBJECT_ID AS ObjectId,
			  LOCKED_BY AS LockedBy, 
			  UNLOCKED_BY AS UnLockedBy, 
			  CREATED_BY AS CreatedBy, 
			  CREATE_DATE AS CreateDate, 
			  ROOT AS Root, 
			  REV_INFO AS RevInfo, 
			  CREATING_CO AS CreatingCo, 
			  STATUS AS Status, 
			  REV AS Revision, 
			  WFS_ID AS WFSID, 
			  LOCKED_BY_NAME AS LockedByName, 
			  CREATING_CO_NAME AS CreatingCoName, 
              APPROVAL_ACTIVITY AS ApprovalActivity, 
			  OBJ_ID AS ObjId, 
			  VERB_TYPE AS VerbType, 
			  VERB_TYPE_NAME AS VerbTypeName
FROM          dbo.A_O_TT_VERBS_HISTORY
GO

/****** Object:  View [dbo].[A_O_PROCEDURES_WITH_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_O_PROCEDURES_WITH_STEPS]
AS
SELECT     dbo.leadingSpaces(o.ROOT, 30) AS SPECIAL_ROOT, dbo.leadingSpaces(ph.ID, 30) AS SPECIAL_ID, ph.ID, ph.OBJECT_ID, ph.VERB, ph.NAME, 
                      ph.SECURITY_LEVEL, o.LOCKED_BY, o.CREATED_BY, o.ROOT, o.CREATING_CO, o.STATUS, o.REV, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, 
                      dbo.A_APPROVED_VERBS.NAME AS VERB_NAME, dbo.A_APPROVED_VERBS.ID AS VERB_ID, o.ID AS OBJ_ID, ph.CREATING_DEPT, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, ph.IS_SYSTEM, 
                      dbo.A_PROCEDURE_STEPS.STEP_TEXT, ph.SYSTEM_ID, ph.DURATION, ph.DURATION_TYPE
FROM         dbo.A_PROCEDURES_HISTORY ph INNER JOIN
                      dbo.A_OBJECTS o ON ph.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON ph.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_SECURITY_LEVELS ON ph.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID INNER JOIN
                      dbo.A_PROCEDURE_STEPS ON ph.ID = dbo.A_PROCEDURE_STEPS.PROCEDURE_ID LEFT OUTER JOIN
                      dbo.A_APPROVED_VERBS ON ph.VERB = dbo.A_APPROVED_VERBS.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_SELECT_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURES_SELECT_SEARCH]
AS
SELECT     dbo.leadingSpaces(o.ROOT, 30) AS SPECIAL_ROOT, dbo.leadingSpaces(ph.ID, 30) AS SPECIAL_ID, ph.ID, ph.OBJECT_ID, ph.VERB, ph.NAME, 
                      dbo.A_APPROVED_VERBS.NAME AS VERB_NAME, o.ID AS OBJ_ID, ph.CREATING_DEPT, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, ph.IS_SYSTEM, 
                      ph.SECURITY_LEVEL, o.CREATING_CO, o.STATUS, o.ROOT, o.LOCKED_BY, o.CREATING_CO_NAME, dbo.A_APPROVED_VERBS.VERB_TYPE_NAME, 
                      dbo.A_APPROVED_VERBS.VERB_TYPE
FROM         dbo.A_PROCEDURES_HISTORY ph INNER JOIN
                      dbo.A_OBJECTS o ON ph.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON ph.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_SECURITY_LEVELS ON ph.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID LEFT OUTER JOIN
                      dbo.A_APPROVED_VERBS ON ph.VERB = dbo.A_APPROVED_VERBS.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_DOCUMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_PROCEDURE_STEP_DOCUMENT_DATA]
AS
SELECT     dbo.A_PROCEDURE_STEP_FILE_LINK.STEP_ID, dbo.A_DOCUMENTS.*
FROM         dbo.A_PROCEDURE_STEP_FILE_LINK INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_PROCEDURE_STEP_FILE_LINK.FILE_ID = dbo.A_DOCUMENTS.ID
GO

/****** Object:  View [dbo].[Portal_PrePropSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PrePropSearchView]
AS
SELECT        dbo.A_PROCEDURE_STEPS.ID, p.OBJECT_ID AS ObjectId,
o.LOCKED_BY AS LockedBy, o.UNLOCKED_BY AS UnlockedBy, o.CREATED_BY AS CreatedBy, 
o.CREATE_DATE AS CreatedDate, o.ROOT, o.REV_INFO AS RevInfo, 
                         o.CREATING_CO AS CreatingCo, o.STATUS, o.REV, o.WFS_ID AS WfsId, 
						 o.LOCKED_BY_NAME AS LockedByName, o.CREATING_CO_NAME AS CreatingCoName, o.APPROVAL_ACTIVITY AS ApprovalActivity, o.OBJ_ID AS ObjId, 
                         p.PROC_STEP_ID AS ProcStepId, dbo.A_PROCEDURE_STEPS.STEP_TEXT AS StepText, dbo.A_PROCEDURE_STEPS.COMMENTS, dbo.A_PROCEDURE_STEPS.DURATION, 
                         dbo.A_PROCEDURE_STEPS.DURATION_TYPE AS DurationType, dbo.A_PROCEDURE_STEPS.REFERENCE_OBJECT AS ReferenceObject, dbo.A_PROCEDURE_STEPS.REFERENCE_VERB AS ReferenceVerb, 
                         dbo.A_PROCEDURE_STEPS.SYSTEM_TASK AS SystemTask, dbo.A_PROCEDURE_STEPS.START_ON_COUNTER AS StartOnCounter,
						 dbo.A_PROCEDURE_STEPS.TITLE,
						 isnull(STUFF((
SELECT +','+ DL.NAME+'|'+DL.DOC_ID
FROM A_V_PROCEDURE_STEP_DOCUMENT_DATA AS DL 
WHERE DL.STEP_ID = o.OBJ_DESC
    FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles,
dbo.A_PROCEDURE_STEPS.Roles AS Roles,
dbo.A_PROCEDURE_STEPS.ReplacementCost,
dbo.A_PROCEDURE_STEPS.Utilization,
dbo.A_PROCEDURE_STEPS.UsefulLife,
A_PROCEDURE_STEPS.DRCM AS UpdatedDate
FROM            dbo.A_OBJECTS AS o INNER JOIN
                         dbo.A_PREPOP_HISTORY AS p ON o.ID = p.OBJECT_ID INNER JOIN
                         dbo.A_PROCEDURE_STEPS ON p.PROC_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
GO

/****** Object:  View [dbo].[A_V_DNR_TASKS_WITH_TEST_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_DNR_TASKS_WITH_TEST_INFO]
AS
SELECT DISTINCT 
                      task.ID AS TASK_ID, task.PARENT_ID AS PARENT_TASK_ID, task.DESCRIPTION AS TASK_DESCRIPTION, task.STATUS AS TASK_STATUS, 
                      task.SYSTEM_TASK, task.PROCEDURE_ID, task.ACTUAL_START_DATE, task.ACTUAL_STOP_DATE, DNR_TASK_INFO.DNR_ID, 
                      DNR_TASK_INFO.DNR_STATUS, DNR_TASK_INFO.TASK_TYPE, mon_template.MONITOR_TYPE, 
                      mon_template.DESCRIPTION AS MONITOR_DESCRIPTION, mon_template.ROLL_UP_ID, mon_template.IS_PASSING, 
                      [proc].HISTORY_REF_ID AS PROC_HIST_ID, [proc].NAME AS PROC_NAME, mon_template.MY_ANSWER, mon_template.USE_RESULT, 
                      mon_template.CORRECT_ANSWER_ID
FROM         dbo.A_TASKS task INNER JOIN
                      dbo.A_DNR_TASK_INFO DNR_TASK_INFO ON task.ID = DNR_TASK_INFO.TASK_ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [proc] ON task.PROCEDURE_ID = [proc].ID INNER JOIN
                      dbo.A_MONITOR_TEMPLATES mon_template ON task.ID = mon_template.TASK_ID
GO

/****** Object:  View [dbo].[A_V_PRODUCTS_APPROVED_DATA_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PRODUCTS_APPROVED_DATA_BY_ID]
AS
SELECT     dbo.A_PRODUCTS.ID, dbo.A_PRODUCTS_HISTORY.NAME, dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID, 
                      dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS SUPPLIER_NAME, dbo.A_PRODUCTS_HISTORY.COMMENTS, 
                      dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID, dbo.A_V_PROCEDURES_APPROVED_DATA.NAME AS PROC_NAME, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.SYSTEM_ID AS PROC_SYS_ID, dbo.A_PRODUCTS_HISTORY.APP_OBJECT, 
                      dbo.A_PRODUCTS_HISTORY.SHIP_OR_LABOR, dbo.A_PRODUCTS_HISTORY.CUSTOMIZABLE, dbo.A_PRODUCTS_HISTORY.REQ_FORM, 
                      dbo.A_PRODUCTS_HISTORY.MGR_TEAM, dbo.A_PRODUCTS_HISTORY.SALES_TAX, dbo.A_PRODUCTS_HISTORY.OBJECT_ID AS PROD_OBJ_ID, 
                      dbo.A_PRODUCTS_HISTORY.PARENT_ID, dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.OBJECT_ID AS PROC_OBJ_ID, 
                      dbo.A_V_PROCEDURES_APPROVED_DATA.HISTORY_REF_ID AS PROC_HIST_ID, dbo.A_PRODUCTS_HISTORY.AVAILABILITY, 
                      dbo.A_PRODUCTS_HISTORY.PERSON_SUPPLIER, dbo.A_PRODUCTS_HISTORY.SYSTEM_PROCEDURE
FROM         dbo.A_PRODUCTS INNER JOIN
                      dbo.A_PRODUCTS_HISTORY ON dbo.A_PRODUCTS.HISTORY_REF_ID = dbo.A_PRODUCTS_HISTORY.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID = dbo.A_V_PROCEDURES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_DNR_TASKS_WITH_CORRECTIVE_ACTIONS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_DNR_TASKS_WITH_CORRECTIVE_ACTIONS_INFO]
AS
SELECT DISTINCT 
                      task.ID AS TASK_ID, task.PARENT_ID AS PARENT_TASK_ID, task.DESCRIPTION AS TASK_DESCRIPTION, task.STATUS AS TASK_STATUS, 
                      task.SYSTEM_TASK, task.PROCEDURE_ID, task.ACTUAL_START_DATE, task.ACTUAL_STOP_DATE, DNR_TASK_INFO.DNR_ID, 
                      DNR_TASK_INFO.DNR_STATUS, DNR_TASK_INFO.TASK_TYPE, [proc].HISTORY_REF_ID AS PROC_HIST_ID, [proc].NAME AS PROC_NAME
FROM         dbo.A_TASKS task INNER JOIN
                      dbo.A_DNR_TASK_INFO DNR_TASK_INFO ON task.ID = DNR_TASK_INFO.TASK_ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [proc] ON task.PROCEDURE_ID = [proc].ID
WHERE     (DNR_TASK_INFO.TASK_TYPE = 'Corrective Action')
GO

/****** Object:  View [dbo].[A_DNR_TASKS_WITH_MONITOR_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_DNR_TASKS_WITH_MONITOR_INFO]
AS
SELECT DISTINCT 
                      task.ID AS TASK_ID, task.PARENT_ID AS PARENT_TASK_ID, task.DESCRIPTION AS TASK_DESCRIPTION, task.STATUS AS TASK_STATUS, 
                      task.SYSTEM_TASK, task.PROCEDURE_ID, task.ACTUAL_START_DATE, task.ACTUAL_STOP_DATE, DNR_TASK_INFO.DNR_ID, 
                      DNR_TASK_INFO.DNR_STATUS, DNR_TASK_INFO.TASK_TYPE, mon_template.MONITOR_TYPE, 
                      mon_template.DESCRIPTION AS MONITOR_DESCRIPTION, mon_template.ROLL_UP_ID, mon_template.IS_PASSING
FROM         dbo.A_TASKS task INNER JOIN
                      dbo.A_DNR_TASK_INFO DNR_TASK_INFO ON task.ID = DNR_TASK_INFO.TASK_ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA [proc] ON task.PROCEDURE_ID = [proc].ID INNER JOIN
                      dbo.A_MONITOR_TEMPLATES mon_template ON task.ID = mon_template.TASK_ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURES_APPROVED_DATA_DROP_DOWN] AS
--This view is for the drop down page on products
SELECT NAME + ' [ID:' + ID + ']' AS SHOWNAME
	  ,[ID]
      ,[HISTORY_REF_ID]
      ,[OBJECT_ID]
      ,[VERB]
      ,[NAME]
      ,[COMMENTS]
      ,[SECURITY_LEVEL]
      ,[STEPS_IN_AP]
      ,[WIP_MSG]
      ,[DRCM]
      ,[MODBY]
      ,[VERB_NAME]
      ,[SYSTEM_ID]
      ,[CREATING_DEPT]
      ,[IS_SYSTEM]
      ,[CREATING_CO]
      ,[STATUS]
      ,[DURATION]
      ,[DURATION_TYPE]
  FROM [dbo].[A_V_PROCEDURES_APPROVED_DATA]
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_PROCEDURE_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_Z_FAVORITES_PROCEDURE_ITEMS]
AS
SELECT     dbo.A_V_PROCEDURES_APPROVED_DATA.NAME, dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID AS ID, 
                      dbo.A_PEOPLES_FAVORITES.NUM
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_V_PROCEDURES_APPROVED_DATA ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_V_PROCEDURES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_O_PART_TYPES_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_O_PART_TYPES_HISTORY]
AS
SELECT     o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, o.ID AS OBJ_ID, pth.ID, pth.NAME, pth.DRCM, pth.MODBY, pth.OBJECT_ID, 
                      pth.UNIT, pth.UNIT_SHIPPING_WEIGHT, pth.SPARE, pth.CONSUMABLE
FROM         dbo.A_PART_TYPES_HISTORY pth INNER JOIN
                      dbo.A_OBJECTS o ON pth.OBJECT_ID = o.ID
GO

/****** Object:  View [dbo].[Portal_PartTypesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PartTypesView]
AS
SELECT        LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreatedDate, ROOT AS Root, REV_INFO AS RevlInfo, CREATING_CO AS CreatingCo, STATUS AS Status,
                          REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity, ID AS Id, NAME AS Name, DRCM AS Drcm, 
                         MODBY AS ModBy, OBJECT_ID AS ObjectId, UNIT AS Unit, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, SPARE AS Spare, CONSUMABLE AS Consumable,
DRCM AS UpdatedDate
FROM            dbo.A_O_PART_TYPES_HISTORY
GO

/****** Object:  View [dbo].[A_V_ROLES_ASSIGNED_ROLES_TO_A_ROLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_ROLES_ASSIGNED_ROLES_TO_A_ROLE]
AS
SELECT     TOP 100 PERCENT rh.ROLE_NAME, rh.STATUS, rh.ID AS ROLE_ID, ra.STATUS AS RA_STATUS, ra.ROLE AS ID, rh.OBJ_ID, 
                      ar.ID AS ROLE_ASSIGNED_ID, ar.HISTORY_REF_ID AS ROLE_ASSIGNED_HISTORY_REF_ID, rh.ROOT, ra.PERSON
FROM         dbo.A_APPROVED_ROLES ar RIGHT OUTER JOIN
                      dbo.A_ROLE_ASSIGNEE ra ON ar.ID = ra.ROLE_ASSIGNED RIGHT OUTER JOIN
                      dbo.A_O_ROLES rh ON ra.ROLE = rh.ID
WHERE     (rh.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_Z_FAV]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_Z_FAV]
AS
SELECT     f.[GROUP], f.ID, g.GROUP_NAME, g.FAV_TYPE, m.ROLE_NAME AS NAME, f.NUM
FROM         dbo.A_PEOPLES_FAVORITES f INNER JOIN
                      dbo.A_O_ROLES m ON f.ITEM = m.ID RIGHT OUTER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS g ON f.[GROUP] = g.ID
WHERE     (m.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_APRROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_APRROVED_PEOPLE]
AS
SELECT     dbo.A_PEOPLE.ID, dbo.A_PEOPLE_HISTORY.LOGIN, dbo.A_PEOPLE_HISTORY.NAME, dbo.A_PEOPLE_HISTORY.PASSWORD, 
                      dbo.A_PEOPLE_HISTORY.BOSS, dbo.A_PEOPLE_HISTORY.SOURCE, dbo.A_PEOPLE_HISTORY.LAST_NAME, 
                      dbo.A_PEOPLE_HISTORY.MIDDLE_NAME, dbo.A_PEOPLE_HISTORY.NICK_NAME, dbo.A_PEOPLE_HISTORY.LANG, 
                      dbo.A_PEOPLE_HISTORY.HIRE_DATE, dbo.A_PEOPLE_HISTORY.DRCM, dbo.A_PEOPLE_HISTORY.MODBY, dbo.A_PEOPLE_HISTORY.OBJECT_ID, 
                      dbo.A_PEOPLE_HISTORY.COMPANY, dbo.A_PEOPLE_HISTORY.TIME_ZONE, dbo.A_PEOPLE_HISTORY.FULL_NAME, 
                      dbo.A_PEOPLE_HISTORY.SYSTEM_STATUS, dbo.A_PEOPLE_HISTORY.CO_POSITION, dbo.A_PEOPLE_HISTORY.ROOT_COMPANY
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_SURVEY_WITH_AUTHOR_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SURVEY_WITH_AUTHOR_NAMES]
AS
SELECT     dbo.A_SURVEY_REPLIES.ID AS R_ID, dbo.A_SURVEY_REPLIES.TEXT, dbo.A_SURVEY_REPLIES.ROOT, dbo.A_SURVEY_REPLIES.PARENT, 
                      dbo.A_SURVEY_REPLIES.AUTHOR, dbo.A_APRROVED_PEOPLE.FULL_NAME AS AUTHOR_NAME, dbo.A_SURVEY_REPLIES.DRCM, 
                      dbo.A_SURVEY_ATTACHMENTS.DOC_ID, dbo.A_SURVEY_REPLIES.COLOR
FROM         dbo.A_SURVEY_REPLIES INNER JOIN
                      dbo.A_APRROVED_PEOPLE ON dbo.A_SURVEY_REPLIES.AUTHOR = dbo.A_APRROVED_PEOPLE.ID LEFT OUTER JOIN
                      dbo.A_SURVEY_ATTACHMENTS ON dbo.A_SURVEY_REPLIES.ID = dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID
GO

/****** Object:  View [dbo].[Portal_PartTypesApprovedVIew]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PartTypesApprovedVIew]
AS
SELECT        ID AS Id, HISTORY_REF_ID AS HistoryRefId, NAME AS Name, SPARE AS Spare, CONSUMABLE AS Consumable, OBJECT_ID AS ObjectId, UNIT AS Unit, UNIT_SHIPPING_WEIGHT AS UnitShippingWeight, 
                         LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, REV AS Rev, 
                         WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity
FROM            dbo.A_APPROVED_PART_TYPES
GO

/****** Object:  View [dbo].[A_APPROVED_PARTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_APPROVED_PARTS]
AS
SELECT     dbo.A_PARTS.ID AS APPROVED_ID, dbo.A_O_PARTS_HISTORY.*
FROM         dbo.A_PARTS INNER JOIN
                      dbo.A_O_PARTS_HISTORY ON dbo.A_PARTS.PARTS_HISTORY_ID = dbo.A_O_PARTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WORK_TIME_WITH_DATE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_WORK_TIME_WITH_DATE]
AS
SELECT     ID, WEEKLY_ID, D, MO, YR, HOUR_TYPE, HOURS, CONVERT(datetime, STR(MO) + '/' + STR(D) + '/' + STR(YR)) AS DT
FROM         dbo.A_SERVICE_CALL_WORK_TIME
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_TOTAL_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_TOTAL_HOURS]
AS
SELECT     ot.OT_HOURS, n.ID, n.NORMAL_HOURS, ot.OT_HOURS + n.NORMAL_HOURS AS TOTAL_HOURS, n.START_DAY, n.START_MONTH, n.START_YEAR, 
                      NT_0.HOURS AS NT_0, NT_1.HOURS AS NT_1, NT_2.HOURS AS NT_2, NT_3.HOURS AS NT_3, NT_4.HOURS AS NT_4, NT_5.HOURS AS NT_5, 
                      NT_6.HOURS AS NT_6, OT_0.HOURS AS OT_0, OT_1.HOURS AS OT_1, OT_2.HOURS AS OT_2, OT_3.HOURS AS OT_3, OT_4.HOURS AS OT_4, 
                      OT_5.HOURS AS OT_5, OT_6.HOURS AS OT_6
FROM         dbo.A_V_SERVICE_CALL_TOTAL_OT_HOURS ot INNER JOIN
                      dbo.A_V_SERVICE_CALLS_WITH_NORMAL_HOURS n ON ot.ID = n.ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_0 ON DATEADD(dd, 0, n.START_DATE) = NT_0.DT AND 
                      n.ID = NT_0.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_1 ON DATEADD(dd, 1, n.START_DATE) = NT_1.DT AND 
                      n.ID = NT_1.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_2 ON DATEADD(dd, 2, n.START_DATE) = NT_2.DT AND 
                      n.ID = NT_2.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_3 ON DATEADD(dd, 3, n.START_DATE) = NT_3.DT AND 
                      n.ID = NT_3.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_4 ON DATEADD(dd, 4, n.START_DATE) = NT_4.DT AND 
                      n.ID = NT_4.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_5 ON DATEADD(dd, 5, n.START_DATE) = NT_5.DT AND 
                      n.ID = NT_5.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE NT_6 ON DATEADD(dd, 6, n.START_DATE) = NT_6.DT AND 
                      n.ID = NT_6.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_0 ON DATEADD(dd, 0, n.START_DATE) = OT_0.DT AND 
                      n.ID = OT_0.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_1 ON DATEADD(dd, 1, n.START_DATE) = OT_1.DT AND 
                      n.ID = OT_1.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_2 ON DATEADD(dd, 2, n.START_DATE) = OT_2.DT AND 
                      n.ID = OT_2.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_3 ON DATEADD(dd, 3, n.START_DATE) = OT_3.DT AND 
                      n.ID = OT_3.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_4 ON DATEADD(dd, 4, n.START_DATE) = OT_4.DT AND 
                      n.ID = OT_4.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_5 ON DATEADD(dd, 5, n.START_DATE) = OT_5.DT AND 
                      n.ID = OT_5.WEEKLY_ID LEFT OUTER JOIN
                      dbo.A_V_SERVICE_CALL_WORK_TIME_WITH_DATE OT_6 ON DATEADD(dd, 6, n.START_DATE) = OT_6.DT AND n.ID = OT_6.WEEKLY_ID
WHERE     (NT_0.HOUR_TYPE = 'NORMAL') AND (NT_1.HOUR_TYPE = 'NORMAL') AND (NT_2.HOUR_TYPE = 'NORMAL') AND (NT_3.HOUR_TYPE = 'NORMAL') 
                      AND (NT_4.HOUR_TYPE = 'NORMAL') AND (NT_5.HOUR_TYPE = 'NORMAL') AND (NT_6.HOUR_TYPE = 'NORMAL') AND (OT_1.HOUR_TYPE = 'OVER') 
                      AND (OT_2.HOUR_TYPE = 'OVER') AND (OT_3.HOUR_TYPE = 'OVER') AND (OT_4.HOUR_TYPE = 'OVER') AND (OT_5.HOUR_TYPE = 'OVER') AND 
                      (OT_6.HOUR_TYPE = 'OVER') AND (OT_0.HOUR_TYPE = 'OVER')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_WEEKLY_REPORT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_WEEKLY_REPORT_DATA]
AS
SELECT     weekly_reports.ID, weekly_reports.MACHINE_NAME, weekly_reports.WORKER_ID, weekly_reports.WORK_TYPE, weekly_reports.COMMENTS, 
                      supplier_name.NAME AS SUPPLIER_NAME, work_types.SUPPLIER_ID, customer_name.NAME AS CUSTOMER_NAME, work_types.CUSTOMER_ID, 
                      weekly_reports.START_DAY, weekly_reports.START_MONTH, weekly_reports.START_YEAR, weekly_reports.STATUS, 
                      weekly_reports.ORDER_NUMBER, work_types.APPROVER_ROLE, work_types.PAYER_ROLE, recievable_role.RECIEVABLE_ROLE, 
                      weekly_reports.EXPENSES_DESCRIPTION, weekly_reports.EXPENSES_AMOUNT, dbo.A_V_PEOPLE_SELF_BOSS_IF_NULL.BOSS, 
                      dbo.A_V_PEOPLE_SELF_BOSS_IF_NULL.FULL_NAME AS WORKER_NAME, approver_name.NAME AS APPROVER_NAME, 
                      payer_role.NAME AS PAYER_NAME
FROM         dbo.A_V_ROLES_APPROVED_DATA approver_name RIGHT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA payer_role RIGHT OUTER JOIN
                      dbo.A_APPROVED_COMPANIES customer_name INNER JOIN
                      dbo.A_SERVICE_CALLS_WORK_TYPES work_types ON customer_name.ID = work_types.CUSTOMER_ID INNER JOIN
                      dbo.A_APPROVED_COMPANIES supplier_name ON work_types.SUPPLIER_ID = supplier_name.ID ON payer_role.ID = work_types.PAYER_ROLE ON 
                      approver_name.ID = work_types.APPROVER_ROLE LEFT OUTER JOIN
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE recievable_role ON work_types.SUPPLIER_ID = recievable_role.COMPANY_ID RIGHT OUTER JOIN
                      dbo.A_V_PEOPLE_SELF_BOSS_IF_NULL RIGHT OUTER JOIN
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS weekly_reports ON dbo.A_V_PEOPLE_SELF_BOSS_IF_NULL.ID = weekly_reports.WORKER_ID ON 
                      work_types.ID = weekly_reports.WORK_TYPE
GO

/****** Object:  View [dbo].[Portal_TheoryParagraphsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_TheoryParagraphsView]
AS
SELECT        ID AS Id, NAME AS Name, CREATING_CO_NAME AS CreatingCoName, ROOT AS SendId, ROOT AS Root, STATUS AS Status
FROM            dbo.A_O_THEORY
GO

/****** Object:  View [dbo].[A_V_DESCRIPTION_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DESCRIPTION_SEARCH]
AS
SELECT     dbo.A_APPROVED_PROCEDURES.ID, dbo.A_APPROVED_PROCEDURES.NAME AS PROCEDURE_NAME, 
                      dbo.A_PROCEDURE_STEPS.STEP_TEXT AS DESCRIPTION, dbo.A_PROCEDURE_STEPS.ID AS STEP_NUMBER, dbo.A_ROLES.ID AS ROLE_ID
FROM         dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID INNER JOIN
                      dbo.A_ROLES ON dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_ROLES.ID RIGHT OUTER JOIN
                      dbo.A_APPROVED_PROCEDURES ON 
                      dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID = dbo.A_APPROVED_PROCEDURES.HISTORY_REF_ID
WHERE     (dbo.A_PROCEDURE_STEPS.ID IS NOT NULL)
GO

/****** Object:  View [dbo].[A_V_JOB_DESCRIPTION_SEARCH_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_JOB_DESCRIPTION_SEARCH_DATA]
AS
SELECT     dbo.A_APPROVED_PROCEDURES.ID, dbo.A_APPROVED_PROCEDURES.NAME AS PROCEDURE_NAME, 
                      dbo.A_PROCEDURE_STEPS.STEP_TEXT AS DESCRIPTION, dbo.A_PROCEDURE_STEPS.ID AS STEP_NUMBER, 
                      dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME, dbo.A_APPROVED_ROLES.ID AS ROLE_ID, procedureObjects.CREATING_CO, 
                      procedureObjects.LOCKED_BY, procedureObjects.STATUS, dbo.A_APPROVED_ROLES.SECURITY_LEVEL
FROM         dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_APPROVED_ROLES.ID INNER JOIN
                      dbo.A_OBJECTS procedureObjects ON dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID = procedureObjects.OBJ_ID RIGHT OUTER JOIN
                      dbo.A_APPROVED_PROCEDURES ON 
                      dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID = dbo.A_APPROVED_PROCEDURES.HISTORY_REF_ID
WHERE     (dbo.A_PROCEDURE_STEPS.ID IS NOT NULL)
GO

/****** Object:  View [dbo].[Portal_ProcedureObjectLaborStepsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ProcedureObjectLaborStepsView]
AS
SELECT        ROLE_NAME AS RoleName, PROCEDURE_ID AS ProcedureId, STEP_ID AS StepId, ROLE_ID AS RoleId, RELATIONSHIP AS RelationShip, LABOR_ROLE AS LaborRole, QTY AS Qty, QTY_TYPE AS QtyType, 
                         OBJ_REF_ID AS ObjRefId, OBJ_ID AS ObjId, OBJ_DESC AS ObjDesc, ID AS Id
FROM            dbo.A_V_PROCEDURE_STEP_LABOR
GO

/****** Object:  View [dbo].[A_O_LOCATIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_O_LOCATIONS]
AS
SELECT     p.*, o.ID AS OBJ_ID, o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, 
                      o.CREATE_DATE AS CREATE_DATE, o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, 
                      dbo.isChildLocation(o.ROOT) AS isChildLocation, o.REV AS REV, o.WFS_ID AS WFS_ID, o.LOCKED_BY_NAME AS LOCKED_BY_NAME, 
                      o.CREATING_CO_NAME AS CREATING_CO_NAME
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_LOCATIONS_HISTORY p ON o.ID = p.OBJECT_ID
GO

/****** Object:  View [dbo].[Portal_LocationsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_LocationsView]
	AS
	SELECT        ID AS Id, NAME AS Name, PARENT_LOCATION AS ParentLocation, PARENT_LOCATION_NAME AS ParentLocationName, ADDRESS_1 AS Address1, ADDRESS_2 AS Address2, FULL_ADDRESS AS FullAddress, 
						 CITY AS City, STATE AS State, COUNTRY AS Country, POSTAL_CODE AS PostalCode, REGION AS Region, REGION_NAME AS RegionName, INTERNAL_ADDRESS AS InternalAddress, OBJECT_ID AS ObjectId, 
						 DRCM AS Drcm, MODBY AS ModBy, PARENT_PATH AS ParentPath, COMPLETE_NAME AS CompleteName, OBJ_ID AS ObjId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnlockedBy, 
						 CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, ROOT AS Root, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, isChildLocation AS IsChildLocation, REV AS Revision, 
						 WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName
FROM            dbo.A_O_LOCATIONS
GO

/****** Object:  View [dbo].[Portal_EquipmentMaintenanceView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[Portal_EquipmentMaintenanceView]
AS
SELECT        
em.Id,
em.ObjectId,
em.ScanBarcode,
(SELECT        Name FROM            dbo.Portal_LocationsView WHERE        (ObjectId = em.RoomEquipment)) AS RoomEquipment, 
em.RoomEquipment AS RoomEquipmentId,
em.DateTime,
ap.FULL_NAME AS RequestedBy, 
em.RequestedById, 
em.TroubleState,
em.MaintenanceTask,
em.Comments,
em.Status,
abyp.FULL_NAME AssignedTo,
em.PemLastCompletedDate,
em.FrequencyField,
em.CreatedDate,
em.UpdatedDate
FROM            dbo.Portal_EquipmentMaintenance em
LEFT JOIN A_APPROVED_PEOPLE ap ON ap.Id = em.RequestedById
LEFT JOIN A_APPROVED_PEOPLE abyp ON abyp.ID = em.AssignedToId

GO

/****** Object:  View [dbo].[A_V_A_CUSTOMER_PART_FILE_LINK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_A_CUSTOMER_PART_FILE_LINK]
AS
SELECT     dbo.A_V_WIP_REPORT_VIEW.TASK_ID, dbo.A_V_WIP_REPORT_VIEW.CUST_ID, dbo.A_TASK_REF_FILES.FILE_ID, dbo.A_DOCUMENTS.NAME, 
                      dbo.A_V_WIP_REPORT_VIEW.CUSTOMER_NAME, dbo.A_V_WIP_REPORT_VIEW.SERIAL, dbo.A_V_WIP_REPORT_VIEW.COMPANY_PART_NUMBER, 
                      dbo.A_DOCUMENTS.SERVER_PATH
FROM         dbo.A_DOCUMENTS INNER JOIN
                      dbo.A_TASK_REF_FILES ON dbo.A_DOCUMENTS.ID = dbo.A_TASK_REF_FILES.FILE_ID INNER JOIN
                      dbo.A_V_WIP_REPORT_VIEW ON dbo.A_TASK_REF_FILES.TASK_ID = dbo.A_V_WIP_REPORT_VIEW.TASK_ID
GO

/****** Object:  View [dbo].[A_V_OBJECTS_MAX_ID_WITH_ROOT]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE  VIEW [dbo].[A_V_OBJECTS_MAX_ID_WITH_ROOT]
AS
SELECT     MAX(ID) AS ID, ROOT
FROM         dbo.A_OBJECTS
GROUP BY ROOT
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_WITH_REF_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[A_V_THEORY_HEADER_WITH_REF_THEORY]
AS
SELECT     dbo.A_OBJECTS.OBJ_DESC AS THEORY_NAME, dbo.A_OBJECTS.ID AS LINKED_THEORY_OBJ_ID, 
                      dbo.A_THEORY_HISTORY.ID AS THEORY_HIST_ID, dbo.A_THEORY_REFERENCE_THEORY.THEORY_ID, 
                      dbo.A_THEORY_REFERENCE_THEORY.THEORY_LINK, dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ROOT, 
                      dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ID AS MAX_ID
FROM         dbo.A_THEORY_REFERENCE_THEORY INNER JOIN
                      dbo.A_THEORY_HISTORY ON dbo.A_THEORY_REFERENCE_THEORY.THEORY_ID = dbo.A_THEORY_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ID = dbo.A_OBJECTS.ID ON 
                      dbo.A_THEORY_REFERENCE_THEORY.THEORY_LINK = dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ROOT
GO

/****** Object:  View [dbo].[A_V_THEORY_PARAGRAPH_WITH_REF_THEORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_THEORY_PARAGRAPH_WITH_REF_THEORY]
AS
SELECT     dbo.A_THEORY_PARAGRAPHS.ID AS PARAGRAPH_ID, dbo.A_THEORY_PARAGRAPH_THEORY_LINK.THEORY_LINK, 
                      dbo.A_OBJECTS.OBJ_DESC AS THEORY_NAME, dbo.A_OBJECTS.ID AS THEORY_OBJ_ID, 
                      dbo.A_THEORY_HISTORY.NAME AS APPROVED_NAME
FROM         dbo.A_THEORY INNER JOIN
                      dbo.A_THEORY_HISTORY ON dbo.A_THEORY.HISTORY_REF_ID = dbo.A_THEORY_HISTORY.ID RIGHT OUTER JOIN
                      dbo.A_THEORY_PARAGRAPHS INNER JOIN
                      dbo.A_THEORY_PARAGRAPH_THEORY_LINK ON 
                      dbo.A_THEORY_PARAGRAPHS.ID = dbo.A_THEORY_PARAGRAPH_THEORY_LINK.THEORY_PARAGRAPH INNER JOIN
                      dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT ON 
                      dbo.A_THEORY_PARAGRAPH_THEORY_LINK.THEORY_LINK = dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ROOT INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ID = dbo.A_OBJECTS.ID ON 
                      dbo.A_THEORY.ID = dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ROOT
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEP_WITH_REF_PROCEDURES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE  VIEW [dbo].[A_V_PROCEDURE_STEP_WITH_REF_PROCEDURES]
AS
SELECT     dbo.A_PROCEDURE_STEPS.ID AS STEP_ID, dbo.A_PROCEDURE_STEP_PROCEDURE_LINK.PROCEDURE_LINK, 
                      dbo.A_OBJECTS.OBJ_DESC AS PROC_NAME, dbo.A_OBJECTS.ID AS PROC_OBJ_ID
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT ON dbo.A_OBJECTS.ID = dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ID INNER JOIN
                      dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURE_STEP_PROCEDURE_LINK ON 
                      dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_STEP_PROCEDURE_LINK.PROCEDURE_STEP ON 
                      dbo.A_V_OBJECTS_MAX_ID_WITH_ROOT.ROOT = dbo.A_PROCEDURE_STEP_PROCEDURE_LINK.PROCEDURE_LINK
GO

/****** Object:  View [dbo].[A_V_PARTS_EXTERNAL_PART_LOOKUP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PARTS_EXTERNAL_PART_LOOKUP]
AS
SELECT     INT_PART.ID AS INT_PART_ID, INT_PH.ID AS INT_PH_ID, EXT_PART.ID AS EXT_PART_ID, EXT_PART_HIST.ID AS EXT_PH_ID, 
                      INT_PH.COMPANY AS INT_CO, EXT_PART_HIST.COMPANY AS EXT_CO
FROM         dbo.A_PARTS_EXTERNAL_EQUALS INNER JOIN
                      dbo.A_PARTS_HISTORY INT_PH ON dbo.A_PARTS_EXTERNAL_EQUALS.PART_ID = INT_PH.ID INNER JOIN
                      dbo.A_PARTS EXT_PART ON dbo.A_PARTS_EXTERNAL_EQUALS.EQUAL_PART_ID = EXT_PART.ID INNER JOIN
                      dbo.A_PARTS_HISTORY EXT_PART_HIST ON EXT_PART.PARTS_HISTORY_ID = EXT_PART_HIST.ID LEFT OUTER JOIN
                      dbo.A_PARTS INT_PART ON INT_PH.ID = INT_PART.PARTS_HISTORY_ID
GO

/****** Object:  View [dbo].[A_V_EXTERNAL_PARTS_WITH_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_EXTERNAL_PARTS_WITH_COMPANY]
AS
SELECT     dbo.A_V_PARTS_EXTERNAL_PART_LOOKUP.INT_PART_ID, dbo.A_V_PARTS_EXTERNAL_PART_LOOKUP.EXT_PART_ID, 
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK.COMPANY AS INT_CO, dbo.A_V_PARTS_APPROVED_DATA_QUICK.COMPANY_NAME AS INT_CO_NAME, 
                      A_V_PARTS_APPROVED_DATA_QUICK_2.COMPANY AS EXT_CO, A_V_PARTS_APPROVED_DATA_QUICK_2.COMPANY_NAME AS EXT_CO_NAME, 
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK.NAME AS INT_NAME, A_V_PARTS_APPROVED_DATA_QUICK_2.NAME AS EXT_NAME
FROM         dbo.A_V_PARTS_APPROVED_DATA_QUICK INNER JOIN
                      dbo.A_V_PARTS_EXTERNAL_PART_LOOKUP ON 
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK.ID = dbo.A_V_PARTS_EXTERNAL_PART_LOOKUP.INT_PART_ID INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK A_V_PARTS_APPROVED_DATA_QUICK_2 ON 
                      dbo.A_V_PARTS_EXTERNAL_PART_LOOKUP.EXT_PART_ID = A_V_PARTS_APPROVED_DATA_QUICK_2.ID
GO

/****** Object:  View [dbo].[A_V_INVOICES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_INVOICES]
AS
SELECT     dbo.A_ACCOUNT_INVOICES.ID, dbo.A_ACCOUNT_INVOICES.ACCOUNT_ID, dbo.A_ACCOUNT_INVOICES.INVOICE_DATE, 
                      dbo.A_ACCOUNT_INVOICES.DRCM, dbo.A_ACCOUNT_INVOICES.MODBY, dbo.A_ACCOUNT_INVOICES.STATUS, dbo.A_ACCOUNT_INVOICES.AMT_PAID, 
                      dbo.A_ACCOUNT_INVOICES.NEW_ITEMS_AMT, dbo.A_ACCOUNT_INVOICES.PREVIOUS_BALANCE, dbo.A_ACCOUNT_INVOICES.PAYMENT_AMOUNT, 
                      dbo.A_ACCOUNT_INVOICES.DISPUTED_AMOUNT, dbo.A_ACCOUNT_INVOICES.TOTAL_DUE, dbo.A_ACCOUNT_INVOICES.DUE_DATE, 
                      dbo.A_ACCOUNT_INVOICES.LATE_FEES, dbo.A_ACCOUNT_INVOICES.DATE_SENT_TO_CUSTOMER, dbo.A_ACCOUNT_INVOICES.PURCHASE_ID, 
                      dbo.A_V_PURCHASES_APPROVED_DATA.STATUS AS Expr1, dbo.A_V_PURCHASES_APPROVED_DATA.CUST_PURCH_NUM, 
                      dbo.A_V_PURCHASES_APPROVED_DATA.SUP_PURCH_NUM, dbo.A_ACCOUNT_INVOICES.NAME, dbo.A_ACCOUNT_INVOICES.INVOICE_TYPE
FROM         dbo.A_ACCOUNT_INVOICES LEFT OUTER JOIN
                      dbo.A_V_PURCHASES_APPROVED_DATA ON dbo.A_ACCOUNT_INVOICES.PURCHASE_ID = dbo.A_V_PURCHASES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_TASK_WITH_ORDER_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_WITH_ORDER_INFORMATION]
AS
SELECT     T.ID, TOI.TASK_ID, TOI.FILL_ID, TOI.ACTUAL_TO_LOC, TOI.ACTUAL_FROM_LOC, T.PARENT_ID, T.DESCRIPTION, T.STATUS AS TASK_STATUS, 
                      T.SYSTEM_TASK, T.PROCEDURE_ID, T.PROCEDURE_STEP_ID, T.REQUESTOR, T.REQUESTEE_ID, T.GROUP_REQUESTEE_ID, 
                      TOI.PURCHASE_ITEM_ID, TOI.PURCHASE_HIST_ID, dbo.A_V_ORDERS_APPROVED_DATA.CUSTOMER_CO, TOI.PURCHASE_ITEM_ROLE, 
                      TOI.FILL_ITEM_ID
FROM         dbo.A_PURCHASES_HISTORY INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_PURCHASES_HISTORY.ID = dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID INNER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA ON dbo.A_PURCHASES_HISTORY.ORDER_ID = dbo.A_V_ORDERS_APPROVED_DATA.ID INNER JOIN
                      dbo.A_TASKS T INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION TOI ON T.ID = TOI.TASK_ID ON dbo.A_ORDER_ITEMS.ID = TOI.PURCHASE_ITEM_ID
GO

/****** Object:  View [dbo].[A_V_PROPOSAL_ORDERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROPOSAL_ORDERS]
AS
SELECT     dbo.A_PROPOSALS_QUOTE_LINK.PROP_HIST_ID, dbo.A_PROPOSALS_QUOTE_LINK.QUOTE_ID, 
                      dbo.A_V_ORDERS_APPROVED_DATA.DESCRIPTION
FROM         dbo.A_PROPOSALS_QUOTE_LINK INNER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA ON dbo.A_PROPOSALS_QUOTE_LINK.QUOTE_ID = dbo.A_V_ORDERS_APPROVED_DATA.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_TASK_QUOTE_ORDER_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASK_QUOTE_ORDER_INFORMATION]
AS
SELECT     dbo.A_QUOTE_ORDER_LINK.QUOTE_ID, dbo.A_QUOTE_ORDER_LINK.ORDER_ID, dbo.A_QUOTE_ORDER_LINK.STATUS, 
                      dbo.A_QUOTE_ORDER_LINK.CUSTOMER, dbo.A_QUOTE_ORDER_LINK.SUPPLIER, dbo.A_QUOTE_ORDER_LINK.TASK_ID, 
                      dbo.A_V_ORDERS_APPROVED_DATA.PROGRESS AS ORDER_PROGRESS, dbo.A_QUOTE_ORDER_LINK.ID, 
                      dbo.A_QUOTES_HISTORY.OBJECT_ID AS QUOTE_OBJECT_ID, dbo.A_QUOTES_HISTORY.PROGRESS AS QUOTE_PROGRESS, 
                      dbo.A_OBJECTS.STATUS AS QUOTE_STATUS, dbo.A_QUOTES_HISTORY.ID AS Expr1, dbo.A_OBJECTS.ROOT, 
                      dbo.A_QUOTE_ORDER_LINK.QA_TASK_ID
FROM         dbo.A_OBJECTS RIGHT OUTER JOIN
                      dbo.A_QUOTES_HISTORY ON dbo.A_OBJECTS.ID = dbo.A_QUOTES_HISTORY.OBJECT_ID RIGHT OUTER JOIN
                      dbo.A_TASKS INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_TASKS.ID = dbo.A_QUOTE_ORDER_LINK.TASK_ID ON 
                      dbo.A_QUOTES_HISTORY.ORDER_ID = dbo.A_QUOTE_ORDER_LINK.ORDER_ID LEFT OUTER JOIN
                      dbo.A_V_ORDERS_APPROVED_DATA ON dbo.A_QUOTE_ORDER_LINK.ORDER_ID = dbo.A_V_ORDERS_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_PERSON_ROLES_WITH_SECURITY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_PERSON_ROLES_WITH_SECURITY]
AS
SELECT     dbo.A_PERSON_ROLES.PERSON_ID, dbo.A_PERSON_ROLES.ROLE_ID, dbo.A_V_ROLES_APPROVED_DATA.SECURITY_LEVEL, 
                      dbo.A_V_ROLES_APPROVED_DATA.IS_ADMIN, dbo.A_V_ROLES_APPROVED_DATA.NAME AS ROLE_NAME
FROM         dbo.A_PERSON_ROLES INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA ON dbo.A_PERSON_ROLES.ROLE_ID = dbo.A_V_ROLES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_MESSAGES_ROLES_SENT_TO_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_MESSAGES_ROLES_SENT_TO_DATA]
AS
SELECT     dbo.A_MESSAGES_ROLE_LINK.MESSAGE_ID, dbo.A_MESSAGES_ROLE_LINK.ROLE_ID, dbo.A_MESSAGES_ROLE_LINK.IS_CC_MESSAGE, 
                      dbo.A_V_ROLES_APPROVED_DATA.NAME AS ROLE_NAME
FROM         dbo.A_MESSAGES_ROLE_LINK INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA ON dbo.A_MESSAGES_ROLE_LINK.ROLE_ID = dbo.A_V_ROLES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_PARTS_SAFETY_STOCK_LEVELS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PARTS_SAFETY_STOCK_LEVELS]
AS
SELECT     l.ROOT AS LOCATION_ID, l.CREATING_CO, l.COMPLETE_NAME AS LOCATION_COMPLETE_NAME, l.NAME AS LOCATION_NAME, lev.PART_ID, 
                      p.NAME AS PART_NAME, lev.DRCM, lev.MODBY, lev.CUR_LEVEL, lev.MIN_WARNING_LEVEL, lev.MAX_WARNING_LEVEL, lev.MIN_LEVEL, 
                      lev.MAX_LEVEL, lev.ID, lev.PART_HIST_ID, lev.PART_OBJ_ID, lev.STATUS, l.STATUS AS LOC_STAT, lev.OLD_LEVEL
FROM         dbo.A_V_PARTS_APPROVED_DATA_QUICK p INNER JOIN
                      dbo.A_PARTS_SAFETY_STOCK_LEVELS lev ON p.ID = lev.PART_ID RIGHT OUTER JOIN
                      dbo.A_O_LOCATIONS l ON lev.LOCATION_ID = l.ROOT
WHERE     (l.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_V_PARTS_GET_SUB_PART_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PARTS_GET_SUB_PART_DATA]
AS
SELECT     subP.COMPANY_PART_NUMBER AS SUB_PART_CO_NUM, subP.NAME AS SUB_PART_NAME, parent.ID AS ROOT, subP.ID AS SUB_PART_ID, 
                      parent.OBJECT_ID AS PARENT_OBJECT_ID, link.QTY, link.ID, link.NICK_NAME
FROM         dbo.A_PARTS_HISTORY parent INNER JOIN
                      dbo.A_PARTS_SUB_PARTS link ON parent.ID = link.PARENT INNER JOIN
                      dbo.A_V_PARTS_APPROVED_DATA_QUICK subP ON link.PART_ID = subP.ID
GO

/****** Object:  View [dbo].[A_V_MEETING_LOC_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_MEETING_LOC_WITH_NAMES]
AS
SELECT     dbo.A_V_LOCATION_WITH_NAME.L_ID, dbo.A_V_LOCATION_WITH_NAME.LOCATION_NAME, dbo.A_MEETINGS.ID AS M_ID
FROM         dbo.A_MEETINGS LEFT OUTER JOIN
                      dbo.A_V_LOCATION_WITH_NAME ON dbo.A_MEETINGS.LOCATION = dbo.A_V_LOCATION_WITH_NAME.L_ID
GO

/****** Object:  View [dbo].[Portal_PeopleObjectSearchView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PeopleObjectSearchView]
	AS
SELECT       
p.ID,
p.NAME AS FirstName,
p.LAST_NAME AS LastName,
p.POSITION_NAME AS PositionName,
p.POSITION_ID AS OfficialPosition,
p.BOSS_NAME AS BossName,
p.BOSS_ID AS BossId,
p.COMPANY_NAME AS CompanyName,
p.LOCATION_NAME AS LocationName, 
p.PRIMARY_PHONE_NUMBER AS PrimaryPhoneNumber,
p.SECONDARY_PHONE_NUMBER AS SecondaryPhoneNumber,
ae.ADDY AS EmailAddress, 
p.SYSTEM_STATUS AS SystemStatus, 
LTRIM(STR(YEAR(p.HIRE_DATE))) + '-' + dbo.leadingZeros(LTRIM(STR(MONTH(p.HIRE_DATE))), 2) + '-' + dbo.leadingZeros(LTRIM(STR(DAY(p.HIRE_DATE))), 2) AS DateHired,
o.STATUS, o.REV, 
dbo.A_FN_FILES_SHOW_ALL_XML_DATA_FOR_OBJECT(p.OBJ_ID, 'PICTURE') AS PicRecord,
dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS RootCoName,
p.OBJ_ID AS ObjectId, 
dbo.A_PEOPLE_HISTORY.SCREEN_TYPE AS ScreenType,
dbo.A_PEOPLE_HISTORY.LANG AS LanguageId, 
dbo.A_PEOPLE_HISTORY.IS_HEAD AS IsHead,
dbo.A_PEOPLE_HISTORY.TIME_ZONE AS TimeZone, 
dbo.A_PEOPLE_HISTORY.LOGIN AS LoginId,
dbo.A_PEOPLE_HISTORY.COMPANY AS Company,
'' LockedByName,
o.ROOT,
isnull(STUFF((
	SELECT +','+ DL.NAME+'|'+DL.LINKED_DOC_ID 
	FROM A_V_DOCUMENTS_WITH_LINKED_ITEM AS DL 
	WHERE DL.OBJECT_ID = p.OBJ_ID
    FOR XML PATH('')), 1, 1,''),'') AS ReferenceFiles
FROM dbo.A_OBJECTS AS o INNER JOIN
dbo.A_PEOPLE_SEARCH_TABLE AS p ON o.ID = p.OBJ_ID INNER JOIN
dbo.A_PEOPLE_HISTORY ON p.ID = dbo.A_PEOPLE_HISTORY.ID LEFT OUTER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PEOPLE_HISTORY.ROOT_COMPANY = dbo.A_V_COMPANIES_APPROVED_DATA.ID
INNER JOIN dbo.A_EMAILS ae ON dbo.A_PEOPLE_HISTORY.OBJECT_ID = ae.OBJECT_ID
GO

/****** Object:  View [dbo].[Portal_ProductsActualPart]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ProductsActualPart]
AS
SELECT DISTINCT 
                         ORDER_ID AS Id, ORDER_HIST_REF_ID AS OrderHistId, PRODUCT_ID AS ProductId, SUPPLIER_ID AS SupplierId, APP_OBJECT AS AppObject, DESCRIPTION, CUSTOMER_CO AS CustomerCo, 
                         CUSTOMER_PERSON AS CustomerPerson, BUDGETARY_ONLY AS BudgetaryOnly, EXPIRATION_DATE AS ExpirationDate, PRODUCT_NAME AS ProductName, CUSTOMER_NAME AS CustomerName, 
                         SUPPLIER_NAME AS SupplierName, PROGRESS, PROC_SYS_ID AS ProcSysId, SOURCE_ID AS SourceId, ADD_COST_ID AS AddCostId, PARENT, STATUS
FROM            dbo.A_V_ORDERS_WITH_ITEMS_AND_APPLICABLE_OBJECTS
GO

/****** Object:  View [dbo].[A_O_WF_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_WF_STAGES]
AS
SELECT     s.NAME, s.OBJECT_ID, s.ID, s.HIDE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.STATUS, 
                      o.CREATING_CO, o.REV, o.WFS_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_WF_STAGES s ON o.ID = s.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_WF_STAGES_WITH_GROUPS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WF_STAGES_WITH_GROUPS]
AS
SELECT     l.WF_GROUP_ID, g.NAME AS GROUP_NAME, dbo.A_O_WF_STAGES.CREATING_CO, dbo.A_O_WF_STAGES.NAME AS STAGE_NAME, 
                      dbo.A_O_WF_STAGES.ID AS STAGE_ID, dbo.A_O_WF_STAGES.HIDE, g.HIDE AS GROUP_HIDE
FROM         dbo.A_WF_STAGE_GROUP_LINK l INNER JOIN
                      dbo.A_WF_GROUPS g ON l.WF_GROUP_ID = g.ID RIGHT OUTER JOIN
                      dbo.A_O_WF_STAGES ON l.WF_STAGE_ID = dbo.A_O_WF_STAGES.ID
GO

/****** Object:  View [dbo].[Portal_ApprovalStagesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ApprovalStagesView]
AS
SELECT        WF_GROUP_ID AS WfGroupId, GROUP_NAME AS GroupName, CREATING_CO AS CreatingCo, STAGE_NAME AS StageName, STAGE_ID AS Id, HIDE AS Hide, GROUP_HIDE AS GroupHide
FROM            dbo.A_V_WF_STAGES_WITH_GROUPS
GO

/****** Object:  View [dbo].[Portal_ProcedureListView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[Portal_ProcedureListView]
AS

SELECT  
phs.SPECIAL_ROOT as RootCompany,
phs.ID as Id,
phs.NAME as ProcedureName,
phs.SECURITY_LEVEL as SecurityClearanceLevel,
phs.CREATED_BY as CreatedBy,
phs.CREATING_CO_NAME as CreatingCompany,
phs.STATUS as ApprovalStatus,
phs.REV as Revision                         
FROM dbo.A_V_PROCEDURE_HISTORY_SEARCH AS phs
GO

/****** Object:  View [dbo].[Portal_ProductsView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ProductsView]
AS
SELECT        OBJECT_ID AS ObjectId, PARENT_ID AS ParentId, NAME AS Name, SUPPLIER_ID AS SupplierId, SUPPLIER_NAME AS SupplierName, COMMENTS AS Comments, PROCEDURE_ID AS ProcedureId, VERB AS Verb, 
                         VERB_NAME AS VerbName, APP_OBJECT AS AppObject, APP_OBJ_NAME AS AppObjName, SHIP_OR_LABOR AS ShipOrLabor, CUSTOMIZABLE AS Customizable, REQ_FORM AS ReqForm, MGR_TEAM AS MgrTeam, 
                         MGR_TEAM_NAME AS MgrTeamName, SALES_TAX AS SalesTax, PROCEDURE_NAME AS ProcedureName, ID AS Id, HISTORY_REF_ID AS HistoryRefId, CREATING_CO_NAME AS CreatingCoName, CREATING_CO AS CreatingCo, 
                         CUST_MGR_ROLE AS CustMgrRole, STATUS AS Status, PERSON_SUPPLIER AS PersonSupplier, AVAILABILITY AS Availability, SYSTEM_PROCEDURE AS SystemProcedure,
						 CustomerRequirementId
FROM            dbo.A_V_PRODUCTS_APPROVED_SELECT_DATA
GO

/****** Object:  View [dbo].[Portal_ApprovedPeople]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Portal_ApprovedPeople]
AS
SELECT     P.ID, PH.LOGIN, PH.NAME, PH.PASSWORD, PH.BOSS, PH.SOURCE, PH.LAST_NAME AS LastName, PH.MIDDLE_NAME, PH.NICK_NAME, PH.LANG, PH.HIRE_DATE, PH.DRCM, 
                      PH.MODBY, PH.OBJECT_ID, PH.COMPANY, PH.TIME_ZONE, PH.FULL_NAME,
					   PH.SYSTEM_STATUS AS SystemStatus,
					   PH.CO_POSITION, PH.ROOT_COMPANY, 
                      ROOT_CO.NAME AS ROOT_CO_NAME, CO.NAME AS CO_NAME, dbo.A_TIME_ZONES.G_DIFF, PH.IS_HEAD, P.STATUS, PH.TOOL_BOX, PH.INFO_BOX, 
                      PH.ADV_SEARCH, PH.COLOR_KEY, PH.SCREEN_TYPE, PH.CHANGE_PASS, dbo.A_V_ROLES_APPROVED_DATA_QUICK.NAME AS POSITION_NAME, 
                      P.HISTORY_REF_ID
FROM         dbo.A_PEOPLE AS P INNER JOIN
                      dbo.A_PEOPLE_HISTORY AS PH ON P.HISTORY_REF_ID = PH.ID LEFT OUTER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON PH.CO_POSITION = dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID LEFT OUTER JOIN
                      dbo.A_TIME_ZONES ON PH.TIME_ZONE = dbo.A_TIME_ZONES.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS CO ON PH.COMPANY = CO.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA AS ROOT_CO ON PH.ROOT_COMPANY = ROOT_CO.ID
WHERE     (P.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ROLES_APPROVED_DPARTMENT_SUB_ROLES]
AS
SELECT     dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID, dbo.A_ROLES_DEPARTMENT_SUB_ROLES.DEPARTMENT, 
                      dbo.A_ROLES_DEPARTMENT_SUB_ROLES.SUB_ROLE
FROM         dbo.A_ROLES_DEPARTMENT_SUB_ROLES INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON 
                      dbo.A_ROLES_DEPARTMENT_SUB_ROLES.ROLE_HIST_ID = dbo.A_V_ROLES_APPROVED_DATA_QUICK.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_PARTS_SAFETY_STOCK_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PARTS_SAFETY_STOCK_ROLES]
AS
SELECT     dbo.A_PARTS_SAFETY_STOCK_ROLES.SAFETY_STOCK_ID, dbo.A_V_ROLES_APPROVED_DATA_QUICK.NAME AS ROLE_NAME, 
                      dbo.A_PARTS_SAFETY_STOCK_ROLES.ROLE_ID, dbo.A_PARTS_SAFETY_STOCK_ROLES.EMAIL_TYPE, 
                      dbo.A_PARTS_SAFETY_STOCK_LEVELS.PART_ID
FROM         dbo.A_PARTS_SAFETY_STOCK_LEVELS INNER JOIN
                      dbo.A_PARTS_SAFETY_STOCK_ROLES ON 
                      dbo.A_PARTS_SAFETY_STOCK_LEVELS.ID = dbo.A_PARTS_SAFETY_STOCK_ROLES.SAFETY_STOCK_ID INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON dbo.A_PARTS_SAFETY_STOCK_ROLES.ROLE_ID = dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_ROLES]
AS
SELECT     dbo.A_V_PEOPLE_DATA_QUICK.FULL_NAME AS PERSON_NAME, dbo.A_PERSON_ROLES.PERSON_ID, dbo.A_PERSON_ROLES.ROLE_ID, 
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK.NAME AS ROLE_NAME
FROM         dbo.A_PERSON_ROLES INNER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK ON dbo.A_PERSON_ROLES.PERSON_ID = dbo.A_V_PEOPLE_DATA_QUICK.ID INNER JOIN
                      dbo.A_V_ROLES_APPROVED_DATA_QUICK ON dbo.A_PERSON_ROLES.ROLE_ID = dbo.A_V_ROLES_APPROVED_DATA_QUICK.ID
GO

/****** Object:  View [dbo].[A_V_THEORY_HISTORY_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_THEORY_HISTORY_SEARCH]
AS
SELECT     dbo.leadingSpaces(o.ROOT, '30') AS SPECIAL_ROOT, dbo.leadingSpaces(th.ID, '30') AS SPECIAL_ID, th.ID, th.OBJECT_ID, th.NAME, 
                      th.SECURITY_LEVEL, th.CREATING_DEPT, o.ID AS OBJ_ID, o.LOCKED_BY, o.CREATED_BY, o.ROOT, o.CREATING_CO, o.STATUS, o.REV, 
                      o.LOCKED_BY_NAME, o.CREATING_CO_NAME, dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS DEPT_NAME, 
                      dbo.A_SECURITY_LEVELS.NAME AS SECURITY_NAME, o.APPROVAL_DATE
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_THEORY_HISTORY th ON o.ID = th.OBJECT_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON th.CREATING_DEPT = dbo.A_V_COMPANIES_APPROVED_DATA.ID LEFT OUTER JOIN
                      dbo.A_SECURITY_LEVELS ON th.SECURITY_LEVEL = dbo.A_SECURITY_LEVELS.ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PAGE_HITS_RAW_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_PAGE_HITS_RAW_DATA]
AS
SELECT     TOP 100 PERCENT p.USER_ID, ph.FULL_NAME, p.DRCM, GETDATE() AS NOW_DATE, DATEDIFF(n, p.DRCM, GETDATE()) AS LAPSED, p.PAGE, 
                      MONTH(p.DRCM) AS MO, YEAR(p.DRCM) AS YR, ph.ROOT_COMPANY, co.NAME AS CO_NAME, dep.NAME AS DEPT_NAME, DAY(p.DRCM) AS DA, p.ID, 
                      p.QUERYSTRING
FROM         dbo.A_PEOPLE_PAGE_TRACKING p INNER JOIN
                      dbo.A_PEOPLE p_1 ON p.USER_ID = p_1.ID INNER JOIN
                      dbo.A_PEOPLE_HISTORY ph ON p_1.HISTORY_REF_ID = ph.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA co ON ph.ROOT_COMPANY = co.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA dep ON ph.COMPANY = dep.ID
ORDER BY p.DRCM DESC
GO

/****** Object:  View [dbo].[A_O_ACCOUNTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_ACCOUNTS]
AS
SELECT     a.ID, a.OBJECT_ID, a.NAME, a.REFERENCE_PO, a.REFERENCE_NAME, dbo.d2v(a.OPEN_DATE) AS OPEN_DATE, dbo.d2v(a.CLOSE_DATE) 
                      AS CLOSE_DATE, a.SUPPLIER_CO, a.CUSTOMER_CO, a.CUSTOMER_BILL_CO, a.MAXIMUM_USES, a.TOTAL_PURCHASE_LIMIT, a.CREDIT_LIMIT, 
                      a.APPROVAL_WF, a.INVOICE_TRIGGER, a.INVOICE_PERIOD_NUMBER, a.INVOICE_PERIOD_TYPE, a.FIRST_INVOICE_DATE, a.NEXT_INVOICE_DATE, 
                      a.PAYMENT_GRACE_PERIOD, a.LATE_FEE_PERCENTAGE, a.REAPPLY_LATE_FEE, a.DRCM, a.MODBY, a.ACCT_TYPE, a.TOTAL_PURCHASES, 
                      a.BALANCE, a.AMT_INVOICED, a.ACCT_STATUS, sup.NAME AS SUPPLIER_NAME, co.NAME AS CUSTOMER_NAME, bill_co.NAME AS CUST_BILL_NAME,
                       o.ID AS OBJ_ID, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, 
                      o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, a.INVOICED_BALANCE, a.UNINVOICED_BALANCE, 
                      a.TOTAL_CREDITS, a.TOTAL_DEBITS, a.BILLING_EMAIL
FROM         dbo.A_ACCOUNTS_HISTORY a INNER JOIN
                      dbo.A_OBJECTS o ON a.OBJECT_ID = o.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA sup ON a.SUPPLIER_CO = sup.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA co ON a.CUSTOMER_CO = co.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA bill_co ON a.CUSTOMER_BILL_CO = bill_co.ID
GO

/****** Object:  View [dbo].[A_V_PRODUCT_WITH_SUPPLIER_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PRODUCT_WITH_SUPPLIER_INFO]
AS
SELECT     dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS SUPPLIER_NAME, dbo.A_PRODUCTS.ID, dbo.A_PRODUCTS_HISTORY.NAME AS PRODUCT_NAME, 
                      dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID, dbo.A_PRODUCTS_HISTORY.COMMENTS, dbo.A_PRODUCTS_HISTORY.PROCEDURE_ID, 
                      dbo.A_PRODUCTS_HISTORY.APP_OBJECT, dbo.A_PRODUCTS_HISTORY.SHIP_OR_LABOR, dbo.A_PRODUCTS_HISTORY.CUSTOMIZABLE, 
                      dbo.A_PRODUCTS_HISTORY.REQ_FORM, dbo.A_PRODUCTS_HISTORY.MGR_TEAM, dbo.A_PRODUCTS_HISTORY.SALES_TAX, 
                      dbo.A_PRODUCTS_HISTORY.DRCM, dbo.A_PRODUCTS_HISTORY.MODBY, dbo.A_PRODUCTS_HISTORY.OBJECT_ID, 
                      dbo.A_PRODUCTS_HISTORY.PARENT_ID, dbo.A_PRODUCTS_HISTORY.SYSTEM_PROCEDURE, dbo.A_PRODUCTS_HISTORY.CUST_MGR_ROLE
FROM         dbo.A_PRODUCTS INNER JOIN
                      dbo.A_PRODUCTS_HISTORY ON dbo.A_PRODUCTS.HISTORY_REF_ID = dbo.A_PRODUCTS_HISTORY.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON dbo.A_PRODUCTS_HISTORY.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_PURCHASES_WITH_SUPPLIER_QUOTES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASES_WITH_SUPPLIER_QUOTES]
AS
SELECT     ph.ID AS PURCH_HIST_ID, ph.ORDER_ID, q.ID AS QUOTE_ID, qh.DESCRIPTION, qh.CUSTOMER_PERSON, qh.CUSTOMER_CO, qh.SUPPLIER_ID, 
                      qh.CREATION_DATE, c.NAME AS SUPPLIER_NAME, qh.ID AS QH_ID, dbo.A_PURCHASE_QUOTE_STATUS.STATUS AS ITEM_STATUS, 
                      dbo.A_PURCHASE_QUOTE_STATUS.ID, ph.PURCHASE_STATUS, ph.OBJECT_ID, dbo.A_OBJECTS.STATUS AS PURCHASE_OBJECT_STATUS, 
                      dbo.A_OBJECTS.LOCKED_BY, A_V_COMPANIES_APPROVED_DATA_1.NAME AS ROOT_CO_NAME, ph.CUST_PURCH_NUM, 
                      ph.SUP_PURCH_NUM
FROM         dbo.A_ORDERS o INNER JOIN
                      dbo.A_PURCHASES_HISTORY ph ON o.ID = ph.ORDER_ID INNER JOIN
                      dbo.A_QUOTES_HISTORY qh INNER JOIN
                      dbo.A_QUOTES q ON qh.ID = q.HISTORY_REF_ID ON o.ID = qh.ORDER_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA c ON qh.SUPPLIER_ID = c.ID INNER JOIN
                      dbo.A_PURCHASE_QUOTE_STATUS ON ph.ID = dbo.A_PURCHASE_QUOTE_STATUS.PURCHASE_ID AND 
                      qh.ID = dbo.A_PURCHASE_QUOTE_STATUS.QUOTE_ID INNER JOIN
                      dbo.A_OBJECTS ON ph.OBJECT_ID = dbo.A_OBJECTS.ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA A_V_COMPANIES_APPROVED_DATA_1 ON c.ROOT_CO = A_V_COMPANIES_APPROVED_DATA_1.ID
GO

/****** Object:  View [dbo].[A_O_NEEDS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_O_NEEDS]
AS
SELECT     n.ID, o.ID AS OBJ_ID, n.OBJECT_ID, n.DESCRIPTION, n.CUSTOMER_CO, co.NAME AS CUSTOMER_NAME, n.TIMEFRAME, n.IMPORTANCE, 
                      n.NEED_SIZE, n.ADVERTISING_START_DATE, n.ADVERTISING_STOP_DATE, n.ADVERSTISE_PUBLICLY, n.CONTACT_NAME, n.CONTACT_EMAIL, 
                      n.CONTACT_PHONE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, 
                      o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, o.APPROVAL_DATE, 
                      dbo.A_FN_NEEDS_GET_STANDARD_SEARCH_FLAG(n.ADVERTISING_START_DATE, n.ADVERTISING_STOP_DATE, GETDATE()) AS STANDARD_SORT, 
                      dbo.A_FN_NEEDS_HAS_DOCUMENT(n.OBJECT_ID) AS HAS_DOCUMENT, DATEDIFF(d, ISNULL(n.ADVERTISING_START_DATE, o.CREATE_DATE), 
                      GETDATE()) AS AGE
FROM         dbo.A_NEEDS_HISTORY n INNER JOIN
                      dbo.A_OBJECTS o ON n.OBJECT_ID = o.ID LEFT OUTER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA co ON n.CUSTOMER_CO = co.ID
GO

/****** Object:  View [dbo].[A_V_ORDERS_SHARED_SUPPLIER_LIST]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_ORDERS_SHARED_SUPPLIER_LIST]
AS
SELECT     dbo.A_V_COMPANIES_APPROVED_DATA.NAME AS CO_NAME, dbo.A_V_COMPANIES_APPROVED_DATA.ID AS CO_ID, 
                      dbo.A_ORDERS_HISTORY.ID AS ORDER_ID
FROM         dbo.A_ORDERS_HISTORY INNER JOIN
                      dbo.A_ORDERS_SUPPLIERS_TO_SHARE_WITH ON 
                      dbo.A_ORDERS_HISTORY.ID = dbo.A_ORDERS_SUPPLIERS_TO_SHARE_WITH.ORDER_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA ON 
                      dbo.A_ORDERS_SUPPLIERS_TO_SHARE_WITH.SUPPLIER_ID = dbo.A_V_COMPANIES_APPROVED_DATA.ID
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_PRODUCTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_PRODUCTS]
AS
SELECT     F_ITEMS.ID AS F_ITEM_ID, F_ITEMS.FORECAST_ID, F_HISTORY.OBJECT_ID AS FORECAST_OBJECT_ID, F_ITEMS.ACCOUNT_ID, 
                      ACCTS_HIST.NAME AS ACCT_NAME, PROD_HIST.NAME AS PROD_NAME, PRODS.ID AS PROD_ID, PRICE_HIST.ID AS PRICE_ID, 
                      PRICE_HIST.UNIT_PRICE, PRICE_HIST.EST_UNIT_PRICE, CUSTOMER.NAME AS CUSTOMER_NAME, PROD_HIST.SUPPLIER_ID, 
                      SUPPLIER.NAME AS SUPPLIER_NAME, F_ITEMS.F_TYPE, F_ITEMS.QTY, F_ITEMS.F_AMT, F_ITEMS.PERCENT_OF_REV, F_ITEMS.PROGRESS, 
                      ACCTS_HIST.CUSTOMER_CO AS CUSTOMER_ID, F_ITEMS.CONFIDENCE, F_ITEMS.AVG_MONTHLY, F_HISTORY.CO AS FORECAST_CO, 
                      F_ITEMS.NOTE, F_ITEMS.AMT_INVOICED
FROM         dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA CUSTOMER ON dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID = CUSTOMER.ID RIGHT OUTER JOIN
                      dbo.A_FORECASTS_HISTORY F_HISTORY INNER JOIN
                      dbo.A_FORECAST_ITEMS F_ITEMS INNER JOIN
                      dbo.A_ACCOUNTS ACCTS ON F_ITEMS.ACCOUNT_ID = ACCTS.ID INNER JOIN
                      dbo.A_ACCOUNTS_HISTORY ACCTS_HIST ON ACCTS.HISTORY_REF_ID = ACCTS_HIST.ID ON 
                      F_HISTORY.ID = F_ITEMS.FORECAST_ID LEFT OUTER JOIN
                      dbo.A_PRODUCTS_HISTORY PROD_HIST INNER JOIN
                      dbo.A_PRODUCTS PRODS ON PROD_HIST.ID = PRODS.HISTORY_REF_ID INNER JOIN
                      dbo.A_V_COMPANIES_APPROVED_DATA SUPPLIER ON PROD_HIST.SUPPLIER_ID = SUPPLIER.ID ON 
                      ACCTS_HIST.PRODUCT_ID = PRODS.ID LEFT OUTER JOIN
                      dbo.A_PROD_PRICE_LIST PRICE_LIST INNER JOIN
                      dbo.A_PROD_PRICE_LIST_HISTORY PRICE_HIST ON PRICE_LIST.HISTORY_REF_ID = PRICE_HIST.ID ON PRODS.ID = PRICE_HIST.PRODUCT ON 
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.PP_LIST_ID = PRICE_HIST.ID AND 
                      dbo.A_PROD_PRICE_LIST_REAL_CUSTOMERS.CUST_ID = ACCTS_HIST.CUSTOMER_CO
GO

/****** Object:  View [dbo].[Portal_PurchaseOrders]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_PurchaseOrders]
AS

SELECT    
a.ID as Id,
a.OBJECT_ID as ObjectId,
a.NAME as Name, 
a.REFERENCE_PO as ReferencePo,
a.REFERENCE_NAME as ReferenceName, 
dbo.d2v(a.OPEN_DATE) AS OpenDate ,
dbo.d2v(a.CLOSE_DATE) 
AS CloseDate,
a.SUPPLIER_CO as SupplierCo, 
a.CUSTOMER_CO as CustomerCo, 
a.CUSTOMER_BILL_CO as CustomerBill,
a.MAXIMUM_USES as MaximumUses,
a.TOTAL_PURCHASE_LIMIT as TotalPurchaseLimit,
a.CREDIT_LIMIT as CreditLimit, 
a.APPROVAL_WF as ApprovalWf, 
a.INVOICE_TRIGGER as InvoiceTrigger,
a.INVOICE_PERIOD_NUMBER as InvoicePeriodNumber,
a.INVOICE_PERIOD_TYPE as InvoicePeriodType, 
a.FIRST_INVOICE_DATE as FirstInvoiceDate,
a.NEXT_INVOICE_DATE as NextInvoiceDate, 
a.PAYMENT_GRACE_PERIOD as PaymentGracePeriod, 
a.LATE_FEE_PERCENTAGE as LateFeePercentage, 
a.REAPPLY_LATE_FEE as ReapplyLateFee,
a.DRCM as Drcm,
a.MODBY as ModBy , 
a.ACCT_TYPE as AccType, 
a.TOTAL_PURCHASES as TotalPurchases, 
a.BALANCE as Balance, 
a.AMT_INVOICED as AmtInvoiced, 
a.ACCT_STATUS as AcctStatus, 
sup.NAME AS SupplierName,
co.NAME AS CustomerName,
bill_co.NAME AS CustBillName,
o.ID AS ObjId, 
o.LOCKED_BY as LockedBy,
o.UNLOCKED_BY as UnlockedBy, 
o.CREATED_BY as CreatedBy, 
o.CREATE_DATE as CreateDate, 
o.ROOT as Root, 
o.REV_INFO as RevInfo,
o.CREATING_CO as CreatingCo,
o.STATUS as Status, 
o.REV as Rev, 
o.WFS_ID as WfsId, 
o.LOCKED_BY_NAME as LockedByName,
o.CREATING_CO_NAME as CreatingCoName, 
o.APPROVAL_ACTIVITY as ApprovalActivity,
a.INVOICED_BALANCE as InvoicedBalance, 
a.UNINVOICED_BALANCE as UninvoicedBalance, 
a.TOTAL_CREDITS as TotalCredits, 
a.TOTAL_DEBITS as TotalDebits,
a.BILLING_EMAIL as BillingEmail,
TOTAL_PURCHASE_LIMIT-TOTAL_DEBITS AS UnusedAmount,
a.TAX_RATE AS TaxRate,
(SELECT +','+ po.ORDER_ID FROM A_ACCOUNT_PURCHASABLE_ORDERS_LINK AS po WHERE po.ACCOUNT_ID = a.OBJECT_ID for xml path(''), type).value('substring(text()[1], 2)', 'varchar(max)') AS Product
FROM dbo.A_ACCOUNTS_HISTORY a INNER JOIN
dbo.A_OBJECTS o ON a.OBJECT_ID = o.ID LEFT OUTER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA sup ON a.SUPPLIER_CO = sup.ID LEFT OUTER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA co ON a.CUSTOMER_CO = co.ID LEFT OUTER JOIN
dbo.A_V_COMPANIES_APPROVED_DATA bill_co ON a.CUSTOMER_BILL_CO = bill_co.ID 
WHERE a.ACCT_TYPE IN ('PURCHASING_ACCOUNT','WARRANTY_ACCOUNT')
OR
o.ROOT IN (SELECT ACCOUNT_ID FROM A_ACCOUNT_INVOICES)
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_ROLE_PEOPLE_2]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_WF_GROUP_ROLE_PEOPLE_2]
AS
SELECT     wfgrl.GROUP_ID, wfgrl.ROLE_ID, p.FULL_NAME, r.PERSON_ID AS PERSON
FROM         dbo.A_PERSON_ROLES r INNER JOIN
                      dbo.A_V_WF_GROUP_ROLE_LINK wfgrl ON r.ROLE_ID = wfgrl.ROLE_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON r.PERSON_ID = p.ID
GO

/****** Object:  View [dbo].[Protal_PendingApprovals]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Protal_PendingApprovals]
AS

SELECT        
O.OBJ_TABLE AS ItemType,
WFA.Title AS ITEMTYPETITLE,
 O.OBJ_DESC AS ItemName, 
 O.ROOT AS ItemNumber, 
O.ID AS ObjectId,
O.REV AS Revision,
O.REV_INFO AS RevInfo,
wf.NAME AS WfName, 
s.NAME AS StageName, 
g.NAME AS GroupName, 
STARTER.FULL_NAME AS Initiator, 
APPROVER.FULL_NAME AS ApprovalName,
wfs.START_DATE AS DateStarted,
o.Status AS Status,
d.PERSON AS PersonId,
gs.WFS_ID AS WfsId,
gs.WF_STAGE_ID AS WfStageId, 
gs.WF_GROUP_ID AS WfGroupId,
APPROVER AS Approver,
'IN_WORKFLOW' AS NotificationType
FROM         dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS d INNER JOIN
                      dbo.A_WORKFLOW_GROUP_STARTED gs ON d.WFGS_ID = gs.ID INNER JOIN
                      dbo.A_WF_STAGES s ON gs.WF_STAGE_ID = s.ID INNER JOIN
                      dbo.A_WF_GROUPS g ON gs.WF_GROUP_ID = g.ID INNER JOIN
                      dbo.A_WORKFLOWS_STARTED wfs ON gs.WFS_ID = wfs.ID INNER JOIN
                      dbo.A_WORKFLOWS wf ON wfs.WF_ID = wf.ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE APPROVER ON d.PERSON = APPROVER.ID INNER JOIN
                      dbo.A_OBJECTS O ON wfs.ID = O.WFS_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE STARTER ON wfs.STARTED_BY = STARTER.ID LEFT JOIN
					  dbo.A_WF_ACTIVITIES WFA ON WFA.ACTIVITY = O.OBJ_TABLE
WHERE O.OBJ_TABLE IN (
'A_PROCEDURES_HISTORY',
'A_PARTS_HISTORY',
'A_PART_TYPES_HISTORY',
'A_ROLES_HISTORY',
'A_PROCEDURES_HISTORY',
'A_PEOPLE_HISTORY',
'A_REGIONS_HISTORY',
'A_LOCATIONS_HISTORY',
'A_COMPANIES_HISTORY',
'A_THEORY_HISTORY',
'A_ACCOUNTS_HISTORY', --'Purchase Orders
'A_PRODUCTS_HISTORY'
)

UNION

SELECT 
o.OBJ_TABLE AS ITEM_TYPE,
WFA.Title AS ITEMTYPETITLE,
o.OBJ_DESC AS ITEM_NAME,
o.ROOT AS ITEM_NUMBER,
o.ID AS ObjectId, 
o.REV AS Revision, 
O.REV_INFO AS RevInfo,
wf.NAME AS WF_NAME,
'' AS StageName, 
'' AS GroupName, 
STARTER.FULL_NAME AS Initiator, 
'' AS ApprovalName, 
'' AS DateStarted, 
o.STATUS, 
o.LOCKED_BY AS PersonId, 
'' AS WfsId, 
'' AS WfStageId, 
'' AS WfGroupId, 
'' AS Approver, 
'CREATING' AS NotificationType
FROM A_OBJECTS o
LEFT JOIN dbo.A_WORKFLOWS_STARTED wfs ON wfs.ID = O.WFS_ID
LEFT JOIN dbo.A_WORKFLOWS wf ON wfs.WF_ID = wf.ID 
LEFT JOIN dbo.A_APPROVED_PEOPLE STARTER ON wfs.STARTED_BY = STARTER.ID 
LEFT JOIN dbo.A_WF_ACTIVITIES WFA ON WFA.ACTIVITY = O.OBJ_TABLE
WHERE o.STATUS ='CREATING' AND o.OBJ_TABLE IN (
'A_PROCEDURES_HISTORY',
'A_PARTS_HISTORY',
'A_PART_TYPES_HISTORY',
'A_ROLES_HISTORY',
'A_PROCEDURES_HISTORY',
'A_PEOPLE_HISTORY',
'A_REGIONS_HISTORY',
'A_LOCATIONS_HISTORY',
'A_COMPANIES_HISTORY',
'A_THEORY_HISTORY',
'A_ACCOUNTS_HISTORY', --'Purchase Orders
'A_PRODUCTS_HISTORY'
)
GO

/****** Object:  View [dbo].[A_V_SURVEY_SEARCH_ALL_INVITED]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SURVEY_SEARCH_ALL_INVITED]
AS
SELECT     s.ID, s.SUBJECT, s.OWNER, s.DATE_CREATED, s.STATUS, dbo.A_SURVEY_INV_PEOPLE.PEOPLE_ID AS P_ID, dbo.A_SURVEY_INV_ROLE.ROLE_ID, 
                      dbo.A_APPROVED_PEOPLE.FULL_NAME AS INITIATOR_NAME, dbo.A_TASK_SURVEY_LINK.TASK_ID, 
                      dbo.A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG(s.STATUS) AS STANDARD_SEARCH, 
                      dbo.A_SURVEY_REALLY_INIVTED_COMPANY.COMPANY_ID, dbo.A_SURVEY_REPLIES.AUTHOR, dbo.A_SURVEY_REPLIES.PARENT, 
                      dbo.A_SURVEY_REPLIES.NUM
FROM         dbo.A_SURVEYS s LEFT OUTER JOIN
                      dbo.A_SURVEY_REPLIES ON s.ID = dbo.A_SURVEY_REPLIES.ROOT LEFT OUTER JOIN
                      dbo.A_APPROVED_PEOPLE ON s.OWNER = dbo.A_APPROVED_PEOPLE.ID LEFT OUTER JOIN
                      dbo.A_SURVEY_REALLY_INIVTED_COMPANY ON s.ID = dbo.A_SURVEY_REALLY_INIVTED_COMPANY.SURVEY_ID LEFT OUTER JOIN
                      dbo.A_TASK_SURVEY_LINK ON s.ID = dbo.A_TASK_SURVEY_LINK.SURVEY_ID LEFT OUTER JOIN
                      dbo.A_SURVEY_INV_ROLE ON s.ID = dbo.A_SURVEY_INV_ROLE.SURVEY_ID LEFT OUTER JOIN
                      dbo.A_SURVEY_INV_PEOPLE ON s.ID = dbo.A_SURVEY_INV_PEOPLE.SURVEY_ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_PEOPLE]
AS
SELECT     dbo.A_MEETING_INV_PEOPLE.MEETING_ID AS M_ID, dbo.A_APPROVED_PEOPLE.FULL_NAME, dbo.A_APPROVED_PEOPLE.ID AS P_ID, 
                      dbo.A_MEETING_INV_PEOPLE.OPTIONAL
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_MEETING_INV_PEOPLE ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_MEETING_INV_PEOPLE.PEOPLE_ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_PEOPLE]
AS
SELECT     dbo.A_SURVEY_INV_PEOPLE.SURVEY_ID AS S_ID, dbo.A_SURVEY_INV_PEOPLE.PEOPLE_ID AS P_ID, 
                      dbo.A_APPROVED_PEOPLE.FULL_NAME AS NAME
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_SURVEY_INV_PEOPLE ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_SURVEY_INV_PEOPLE.PEOPLE_ID
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_WF_GROUP_MEMBERS]
AS
SELECT     g.NAME AS GROUP_NAME, g.ID AS GROUP_ID, g.OBJECT_ID, p.NAME + ' ' + p.LAST_NAME AS PERSON_NAME, p.MIDDLE_NAME, 
                      pl.USER_ID AS PERSON_ID
FROM         dbo.A_WF_GROUPS g INNER JOIN
                      dbo.A_WF_GROUP_PEOPLE_LINK pl ON g.ID = pl.WF_GROUP_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON pl.USER_ID = p.ID
GO

/****** Object:  View [dbo].[A_V_SURVEY_WITH_INITIAL_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SURVEY_WITH_INITIAL_DATA]
AS
SELECT     dbo.A_SURVEYS.ID AS S_ID, dbo.A_SURVEYS.OWNER AS INITIATOR_ID, dbo.A_SURVEYS.SUBJECT, dbo.A_SURVEYS.DATE_CREATED, 
                      dbo.A_SURVEY_REPLIES.ROOT, dbo.A_SURVEY_REPLIES.TEXT, dbo.A_APPROVED_PEOPLE.FULL_NAME AS INITIATOR_NAME, 
                      dbo.A_SURVEYS.STATUS, dbo.A_APPROVED_PEOPLE.ID AS P_ID, dbo.A_SURVEYS.DRCM, dbo.A_SURVEY_REPLIES.ID AS R_ID, 
                      dbo.A_SURVEY_REPLIES.NUM
FROM         dbo.A_SURVEYS LEFT OUTER JOIN
                      dbo.A_APPROVED_PEOPLE ON dbo.A_SURVEYS.OWNER = dbo.A_APPROVED_PEOPLE.ID LEFT OUTER JOIN
                      dbo.A_SURVEY_REPLIES ON dbo.A_SURVEYS.ID = dbo.A_SURVEY_REPLIES.ROOT
GO

/****** Object:  View [dbo].[A_APPROVED_ROLES_WITH_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_APPROVED_ROLES_WITH_ASSIGNEES]
AS
SELECT     dbo.A_APPROVED_ROLES.ID, dbo.A_APPROVED_ROLES.NAME, dbo.A_APPROVED_PEOPLE.FULL_NAME AS MEMBER_NAME, 
                      dbo.A_APPROVED_ROLES.CREATING_CO, dbo.A_APPROVED_ROLES.OBJECT_ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ON dbo.A_APPROVED_PEOPLE.ID = dbo.A_ROLE_ASSIGNEE.PERSON INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_ROLE_ASSIGNEE.ROLE = dbo.A_APPROVED_ROLES.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_ORG_CHART_TREE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PEOPLE_ORG_CHART_TREE_DATA]
AS
SELECT     dbo.A_APPROVED_PEOPLE.ID, dbo.A_APPROVED_PEOPLE.BOSS, dbo.A_ROLES_HISTORY.NAME AS TITLE, dbo.A_APPROVED_PEOPLE.NAME, 
                      dbo.A_APPROVED_PEOPLE.LAST_NAME, dbo.A_APPROVED_PEOPLE.SYSTEM_STATUS, dbo.A_APPROVED_PEOPLE.STATUS
FROM         dbo.A_ROLES_HISTORY INNER JOIN
                      dbo.A_ROLES ON dbo.A_ROLES_HISTORY.ID = dbo.A_ROLES.HISTORY_REF_ID RIGHT OUTER JOIN
                      dbo.A_APPROVED_PEOPLE ON dbo.A_ROLES.ID = dbo.A_APPROVED_PEOPLE.CO_POSITION
WHERE     (dbo.A_APPROVED_PEOPLE.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_WF_STARTED_WITH_DENIAL_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_WF_STARTED_WITH_DENIAL_INFO]
AS
SELECT     wfs.ID AS WFS_ID, wfs.STARTED_BY AS WF_STARTED_BY, wfs.START_DATE AS WF_START_DATE, wfs.DENIAL_REASON, 
                      wfs.FINISHED_DATE AS DENIAL_DATE, o.OBJ_DESC AS OBJ_NAME, p.FULL_NAME AS DENIER, o.ID AS OBJECT_ID, 
                      p.OBJECT_ID AS DENIER_OBJ_ID
FROM         dbo.A_WORKFLOWS_STARTED wfs INNER JOIN
                      dbo.A_OBJECTS o ON wfs.ID = o.WFS_ID INNER JOIN
                      dbo.A_WORKFLOW_GROUP_STARTED wfgs ON wfs.ID = wfgs.WFS_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON wfgs.DENIER = p.ID
GO

/****** Object:  View [dbo].[A_V_TIME_ZONE_WITH_PERSON_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_TIME_ZONE_WITH_PERSON_ID]
AS
SELECT     dbo.A_APPROVED_PEOPLE.ID, dbo.A_TIME_ZONES.DESCRIPTION, dbo.A_TIME_ZONES.G_DIFF, dbo.A_TIME_ZONES.ID AS TIME_ZONE_ID
FROM         dbo.A_APPROVED_PEOPLE INNER JOIN
                      dbo.A_TIME_ZONES ON dbo.A_APPROVED_PEOPLE.TIME_ZONE = dbo.A_TIME_ZONES.ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_PEOPLE]
AS
SELECT     A_APPROVED_PEOPLE_1.FULL_NAME AS NAME, A_DISCUSSION_INV_PEOPLE_1.DISCUSSION_ID AS D_ID, 
                      A_DISCUSSION_INV_PEOPLE_1.PEOPLE_ID
FROM         dbo.A_DISCUSSION_INV_PEOPLE A_DISCUSSION_INV_PEOPLE_1 INNER JOIN
                      dbo.A_APPROVED_PEOPLE A_APPROVED_PEOPLE_1 ON A_DISCUSSION_INV_PEOPLE_1.PEOPLE_ID = A_APPROVED_PEOPLE_1.ID
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DISCUSSION_SEARCH]
AS
SELECT     d.ID, d.SUBJECT, d.INITIATOR AS I_ID, d.DATE_CREATED, dbo.A_FN_DISCUSSION_GET_STANDARD_SEARCH_FLAG(d.STATUS) 
                      AS STANDARD_SEARCH, d.DRCM, d.MODBY, d.STATUS, INITIATOR.FULL_NAME AS INITIATOR_NAME, dbo.A_TASK_DISCUSSION_LINK.TASK_ID, 
                      OPENING_STATEMENT.ID AS RESPONSE_ID, OPENING_STATEMENT.PARENT_ID, d.LAST_RESPONSE
FROM         dbo.A_DISCUSSIONS d LEFT OUTER JOIN
                      dbo.A_DISCUSSION_RESPONSE OPENING_STATEMENT ON d.ID = OPENING_STATEMENT.DISCUSSION_ID LEFT OUTER JOIN
                      dbo.A_TASK_DISCUSSION_LINK ON d.ID = dbo.A_TASK_DISCUSSION_LINK.DISCUSSION_ID LEFT OUTER JOIN
                      dbo.A_APPROVED_PEOPLE INITIATOR ON d.INITIATOR = INITIATOR.ID
WHERE     (OPENING_STATEMENT.PARENT_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_PEOPLE_WITH_COMPANIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_WITH_COMPANIES]
AS
SELECT     p.NAME, c.NAME AS COMPANY_NAME, c.ID AS CO, p.ID AS PERSON
FROM         dbo.A_APPROVED_COMPANIES c INNER JOIN
                      dbo.A_APPROVED_PEOPLE p ON c.ID = p.ROOT_COMPANY
GO

/****** Object:  View [dbo].[A_V_PEOPLE_BOSS_SUB_LOOKUP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_BOSS_SUB_LOOKUP]
AS
SELECT     TOP 100 PERCENT B.ID AS BOSS_ID, B.FULL_NAME AS BOSS, S.ID AS SUB_ID, S.FULL_NAME AS SUB
FROM         dbo.A_APPROVED_PEOPLE B INNER JOIN
                      dbo.A_PEOPLE_SUB_LOOKUP_TABLE l ON B.ID = l.BOSS INNER JOIN
                      dbo.A_APPROVED_PEOPLE S ON l.SUBORDINATE = S.ID
ORDER BY B.FULL_NAME
GO

/****** Object:  View [dbo].[A_V_SURVEY_BY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SURVEY_BY_ID]
AS
SELECT     s.ID, s.SUBJECT, s.OWNER, s.DATE_CREATED, dbo.A_FN_SURVEYS_GET_STANDARD_SEARCH_FLAG(s.STATUS) AS STANDARD_SEARCH, 
                      dbo.A_APPROVED_PEOPLE.FULL_NAME AS INITIATOR_NAME, s.STATUS
FROM         dbo.A_SURVEYS s LEFT OUTER JOIN
                      dbo.A_APPROVED_PEOPLE ON s.OWNER = dbo.A_APPROVED_PEOPLE.ID
GO

/****** Object:  View [dbo].[A_V_APPROVALS_PENDING]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_APPROVALS_PENDING]
AS
SELECT     O.OBJ_TABLE AS ITEM_TYPE, O.OBJ_DESC AS ITEM_NAME, O.ROOT AS ITEM_NUMBER, wf.NAME AS WF_NAME, s.NAME AS STAGE_NAME, 
                      g.NAME AS GROUP_NAME, STARTER.FULL_NAME AS INITIATOR, APPROVER.FULL_NAME AS APPROVER_NAME, wfs.START_DATE AS DATE_STARTED,
                       wfs.STATUS_ON_COMPLETION AS STATUS, O.ID AS OBJECT_ID, O.REV AS REVISION, d.PERSON AS PERSON_ID, gs.WFS_ID, gs.WF_STAGE_ID, 
                      gs.WF_GROUP_ID, gs.APPROVER, O.REV_INFO
FROM         dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS d INNER JOIN
                      dbo.A_WORKFLOW_GROUP_STARTED gs ON d.WFGS_ID = gs.ID INNER JOIN
                      dbo.A_WF_STAGES s ON gs.WF_STAGE_ID = s.ID INNER JOIN
                      dbo.A_WF_GROUPS g ON gs.WF_GROUP_ID = g.ID INNER JOIN
                      dbo.A_WORKFLOWS_STARTED wfs ON gs.WFS_ID = wfs.ID INNER JOIN
                      dbo.A_WORKFLOWS wf ON wfs.WF_ID = wf.ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE APPROVER ON d.PERSON = APPROVER.ID INNER JOIN
                      dbo.A_OBJECTS O ON wfs.ID = O.WFS_ID INNER JOIN
                      dbo.A_APPROVED_PEOPLE STARTER ON wfs.STARTED_BY = STARTER.ID
GO

/****** Object:  View [dbo].[A_V_APPROVED_COMPANIES_WITH_ADDRESS_AND_LOGOS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_APPROVED_COMPANIES_WITH_ADDRESS_AND_LOGOS]
AS
SELECT     dbo.A_APPROVED_LOCATIONS.STATE, dbo.A_APPROVED_LOCATIONS.CITY, dbo.A_APPROVED_LOCATIONS.FULL_ADDRESS, 
                      dbo.A_APPROVED_LOCATIONS.COUNTRY, dbo.A_APPROVED_COMPANIES.ID, dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME, 
                      dbo.A_APPROVED_LOCATIONS.NAME AS LOCATION_NAME, dbo.A_APPROVED_LOCATIONS.ADDRESS_1, dbo.A_APPROVED_LOCATIONS.ADDRESS_2, 
                      dbo.A_APPROVED_LOCATIONS.POSTAL_CODE, dbo.A_APPROVED_LOCATIONS.REGION, dbo.A_APPROVED_LOCATIONS.REGION_NAME, 
                      dbo.A_APPROVED_LOCATIONS.INTERNAL_ADDRESS, dbo.A_APPROVED_COMPANIES.PHONE,
					  A_DOCUMENT_LINK.LINKED_DOC_ID as LOGO_ID
FROM         dbo.A_APPROVED_COMPANIES LEFT OUTER JOIN
                      dbo.A_APPROVED_LOCATIONS ON dbo.A_APPROVED_COMPANIES.LOCATION = dbo.A_APPROVED_LOCATIONS.ID
					  LEFT OUTER JOIN
					  dbo.A_DOCUMENT_LINK ON dbo.A_APPROVED_COMPANIES.OBJECT_ID = dbo.A_DOCUMENT_LINK.OBJECT_ID
					  AND (dbo.A_DOCUMENT_LINK.TYPE = N'LOGO')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_PAYMENT_LOCATION_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_PAYMENT_LOCATION_NAMES]
AS
SELECT     dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.COMPANY_ID, dbo.A_APPROVED_LOCATIONS.NAME AS show, 
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.PAYMENT_LOCATION AS [value]
FROM         dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE INNER JOIN
                      dbo.A_APPROVED_LOCATIONS ON dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.PAYMENT_LOCATION = dbo.A_APPROVED_LOCATIONS.ID
GO

/****** Object:  View [dbo].[Portal_ApprovedCompaniesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_ApprovedCompaniesView]
AS
SELECT DISTINCT 
                         ID AS Id, HISTORY_REF_ID AS HistoryRefId, NAME AS Name, CO_TYPE AS CoType, PARENT AS Parent, PHONE AS Phone, LOCATION AS Location, LOCATION_NAME AS LocationName, DRCM AS Drcm, MODBY AS ModBy, 
                         OBJECT_ID AS ObjectId, LOCKED_BY AS LockedBy, UNLOCKED_BY AS UnLockedBy, CREATED_BY AS CreatedBy, CREATE_DATE AS CreateDate, REV_INFO AS RevInfo, CREATING_CO AS CreatingCo, STATUS AS Status, 
                         REV AS Rev, WFS_ID AS WfsId, LOCKED_BY_NAME AS LockedByName, CREATING_CO_NAME AS CreatingCoName, APPROVAL_ACTIVITY AS ApprovalActivity, GENERAL_STATUS AS GeneralStatus, 
                         PARENT_NAME AS ParentName, ROOT_CO_NAME AS RootCoName, ROOT_CO_ID AS RootCoId
FROM            dbo.A_APPROVED_COMPANIES
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_COMPANY]
AS
SELECT     dbo.A_DISCUSSION_INV_COMPANY.COMPANY_ID AS C_ID, dbo.A_DISCUSSION_INV_COMPANY.DISCUSSION_ID AS D_ID, 
                      dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME
FROM         dbo.A_APPROVED_COMPANIES INNER JOIN
                      dbo.A_DISCUSSION_INV_COMPANY ON dbo.A_APPROVED_COMPANIES.ID = dbo.A_DISCUSSION_INV_COMPANY.COMPANY_ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_COMPANY]
AS
SELECT     dbo.A_APPROVED_COMPANIES.ID AS C_ID, dbo.A_MEETING_INV_COMPANY.MEETING_ID AS M_ID, 
                      dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME, dbo.A_MEETING_INV_COMPANY.OPTIONAL
FROM         dbo.A_MEETING_INV_COMPANY INNER JOIN
                      dbo.A_APPROVED_COMPANIES ON dbo.A_MEETING_INV_COMPANY.COMPANY_ID = dbo.A_APPROVED_COMPANIES.ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_COMPANIES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_ALLOWED_COMPANIES_DATA]
AS
SELECT     dbo.A_PROJECT_ALLOWED_COMPANIES.PROJECT_ID, dbo.A_PROJECT_ALLOWED_COMPANIES.COMPANY_ID, 
                      dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME
FROM         dbo.A_PROJECT_ALLOWED_COMPANIES INNER JOIN
                      dbo.A_APPROVED_COMPANIES ON dbo.A_PROJECT_ALLOWED_COMPANIES.COMPANY_ID = dbo.A_APPROVED_COMPANIES.ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_COMPANY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_COMPANY]
AS
SELECT     dbo.A_SURVEY_INV_COMPANY.SURVEY_ID AS S_ID, dbo.A_SURVEY_INV_COMPANY.COMPANY_ID AS C_ID, 
                      dbo.A_APPROVED_COMPANIES.NAME AS COMPANY_NAME
FROM         dbo.A_SURVEY_INV_COMPANY INNER JOIN
                      dbo.A_APPROVED_COMPANIES ON dbo.A_SURVEY_INV_COMPANY.COMPANY_ID = dbo.A_APPROVED_COMPANIES.ID
GO

/****** Object:  View [dbo].[A_O_PROPOSALS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PROPOSALS]
AS
SELECT     ph.ID AS HIST_ID, o.ROOT, ph.OBJECT_ID AS OBJ_ID, ph.CUSTOMER_CO, ph.SUPPLIER_CO, ph.PROP_DATE, ph.PROP_VALID_DATE, ph.NAME, 
                      ph.DRCM, ph.MODBY, ph.OPENING, ph.ADD_NOTES, ph.CLOSING, ph.CREATOR_ID, o.LOCKED_BY_NAME, o.STATUS, o.CREATING_CO, 
                      o.UNLOCKED_BY, o.LOCKED_BY, o.CREATING_CO_NAME, o.WFS_ID, o.REV, o.REV_INFO, o.CREATE_DATE, o.CREATED_BY, 
                      CUST.NAME AS CUST_NAME, SUP.NAME AS SUPPLIER_NAME, cre.FULL_NAME AS CREATOR_NAME, ph.OBJECT_ID, ph.CREATOR_TITLE
FROM         dbo.A_PROPOSALS_HISTORY ph INNER JOIN
                      dbo.A_OBJECTS o ON ph.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_COMPANIES CUST ON ph.CUSTOMER_CO = CUST.ID INNER JOIN
                      dbo.A_COMPANIES SUP ON ph.SUPPLIER_CO = SUP.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK cre ON ph.CREATOR_ID = cre.ID
GO

/****** Object:  View [dbo].[A_V_ENGINEER_REPORTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_ENGINEER_REPORTS]
AS
SELECT     dbo.A_V_PEOPLE_DATA_QUICK.FULL_NAME AS CREATOR_NAME, dbo.A_ENGINEER_SCREEN_REPORTS.ID, 
                      dbo.A_ENGINEER_SCREEN_REPORTS.NAME, dbo.A_ENGINEER_SCREEN_REPORTS.FILE_ID, dbo.A_ENGINEER_SCREEN_REPORTS.DESCRIPTION, 
                      dbo.A_ENGINEER_SCREEN_REPORTS.ROOT_COMPANY, dbo.A_ENGINEER_SCREEN_REPORTS.DATE_ADDED, 
                      dbo.A_ENGINEER_SCREEN_REPORTS.DATE_REMOVED, dbo.A_ENGINEER_SCREEN_REPORTS.ADDED_BY, 
                      dbo.A_ENGINEER_SCREEN_REPORTS.DRCM, dbo.A_ENGINEER_SCREEN_REPORTS.MODBY, dbo.A_DOCUMENTS.NAME AS FILE_NAME
FROM         dbo.A_ENGINEER_SCREEN_REPORTS LEFT OUTER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_ENGINEER_SCREEN_REPORTS.FILE_ID = dbo.A_DOCUMENTS.ID LEFT OUTER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK ON dbo.A_ENGINEER_SCREEN_REPORTS.ADDED_BY = dbo.A_V_PEOPLE_DATA_QUICK.ID
GO

/****** Object:  View [dbo].[A_V_NAV_HISTORY_WITH_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_NAV_HISTORY_WITH_NAMES]
AS
SELECT     TOP 100 PERCENT dbo.A_NAV_HISTORY.DRCM, dbo.A_V_PEOPLE_DATA_QUICK.FULL_NAME, DATEDIFF(n, dbo.A_NAV_HISTORY.DRCM, GETDATE()) 
                      AS myMins, GETDATE() AS NOW_DATE, dbo.A_NAV_HISTORY.PAGE, dbo.A_NAV_HISTORY.QS
FROM         dbo.A_NAV_HISTORY INNER JOIN
                      dbo.A_V_PEOPLE_DATA_QUICK ON dbo.A_NAV_HISTORY.USER_ID = dbo.A_V_PEOPLE_DATA_QUICK.ID
ORDER BY DATEDIFF(ss, dbo.A_NAV_HISTORY.DRCM, GETDATE())
GO

/****** Object:  View [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_DISCUSSION_INVITED_ROLES]
AS
SELECT     dbo.A_DISCUSSION_INV_ROLE.DISCUSSION_ID AS D_ID, dbo.A_APPROVED_ROLES.ID AS R_ID, 
                      dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME
FROM         dbo.A_APPROVED_ROLES INNER JOIN
                      dbo.A_DISCUSSION_INV_ROLE ON dbo.A_APPROVED_ROLES.ID = dbo.A_DISCUSSION_INV_ROLE.ROLE_ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_MEETING_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_POP_FILL_MEETING_INVITED_ROLES]
AS
SELECT     dbo.A_MEETING_INV_ROLE.MEETING_ID AS M_ID, dbo.A_APPROVED_ROLES.ID AS R_ID, dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME, 
                      dbo.A_MEETING_INV_ROLE.OPTIONAL
FROM         dbo.A_APPROVED_ROLES INNER JOIN
                      dbo.A_MEETING_INV_ROLE ON dbo.A_APPROVED_ROLES.ID = dbo.A_MEETING_INV_ROLE.ROLE_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_ALLOWED_ROLES_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_ALLOWED_ROLES_DATA]
AS
SELECT     dbo.A_PROJECT_ALLOWED_ROLES.ROLE_ID, dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME, 
                      dbo.A_PROJECT_ALLOWED_ROLES.PROJECT_ID
FROM         dbo.A_PROJECT_ALLOWED_ROLES INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_PROJECT_ALLOWED_ROLES.ROLE_ID = dbo.A_APPROVED_ROLES.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_ROLE_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALL_ACC_RECIEVABLE_ROLE_NAMES]
AS
SELECT     dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.COMPANY_ID, dbo.A_APPROVED_ROLES.NAME AS show, 
                      dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.RECIEVABLE_ROLE AS [value]
FROM         dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_SERVICE_CALLS_ACC_RECIEVABLE_ROLE.RECIEVABLE_ROLE = dbo.A_APPROVED_ROLES.ID
GO

/****** Object:  View [dbo].[A_V_POP_FILL_SURVEY_INVITED_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_POP_FILL_SURVEY_INVITED_ROLES]
AS
SELECT     dbo.A_SURVEY_INV_ROLE.SURVEY_ID AS S_ID, dbo.A_SURVEY_INV_ROLE.ROLE_ID AS R_ID, 
                      dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME
FROM         dbo.A_SURVEY_INV_ROLE INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_SURVEY_INV_ROLE.ROLE_ID = dbo.A_APPROVED_ROLES.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_ROLES_TO_VIEW]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROCEDURE_ROLES_TO_VIEW]
AS
SELECT     dbo.A_PROCEDURES_HISTORY.ID AS PROCEDURE_ID, dbo.A_APPROVED_ROLES.NAME AS ROLE_NAME, 
                      dbo.A_PROCEDURES_HISTORY.OBJECT_ID AS PROCEDURE_OBJ_ID, dbo.A_APPROVED_ROLES.ID AS ROLE_ID, 
                      dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP
FROM         dbo.A_PROCEDURES_HISTORY INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_PROCEDURES_HISTORY.ID = dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID INNER JOIN
                      dbo.A_APPROVED_ROLES ON dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID = dbo.A_APPROVED_ROLES.ID
WHERE     (dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP = N'ROLE_TO_VIEW')
GO

/****** Object:  View [dbo].[Portal_ActivitiesView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[Portal_ActivitiesView]
AS

-- JUSTIN TITLE FIX - 05-11-2019

SELECT A.ID
	, A.ACTIVITY
	, A.TITLE
	, A.TITLE + ' (' + A.ACTIVITY + ')' AS TITLEACTIVITY
	, A.MODBY
	, A.DRCM
FROM A_WF_ACTIVITIES A

-- PREV
-- SELECT ID AS ID, ACTIVITY AS ACTIVITY FROM A_WF_ACTIVITIES

GO

/****** Object:  View [dbo].[A_V_PURCHASE_HISTORY_WITH_ITEMS_AND_FILLS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PURCHASE_HISTORY_WITH_ITEMS_AND_FILLS]
AS
SELECT     dbo.A_PURCHASES_HISTORY.ID AS PURCHASE_HIST_ID, dbo.A_PURCHASES_HISTORY.PURCHASE_STATUS, 
                      dbo.A_PURCHASES_HISTORY.PURCHASER, dbo.A_ORDER_ITEMS.ID AS PURCHASE_ITEM_ID, dbo.A_FILLS.ID AS FILL_ID, dbo.A_FILLS.FILL_OBJ_ID, 
                      dbo.A_FILLS.MODBY
FROM         dbo.A_PURCHASES_HISTORY INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_PURCHASES_HISTORY.ID = dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID LEFT OUTER JOIN
                      dbo.A_FILLS ON dbo.A_ORDER_ITEMS.ID = dbo.A_FILLS.PURCH_ITEM_ID
GO

/****** Object:  View [dbo].[A_V_WF_PENDING_APPROVALS_WITH_STARTER_AND_INITER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_WF_PENDING_APPROVALS_WITH_STARTER_AND_INITER]
AS
SELECT     dbo.A_WORKFLOWS_STARTED.STARTED_BY, dbo.A_WORKFLOW_GROUP_STARTED.WFS_ID, 
                      dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS.PERSON, dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS.WFGS_ID
FROM         dbo.A_WORKFLOWS_STARTED INNER JOIN
                      dbo.A_WORKFLOW_GROUP_STARTED ON dbo.A_WORKFLOWS_STARTED.ID = dbo.A_WORKFLOW_GROUP_STARTED.WFS_ID INNER JOIN
                      dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS ON 
                      dbo.A_WORKFLOW_GROUP_STARTED.ID = dbo.A_WF_GROUP_PEOPLE_WITH_PENDING_APPROVALS.WFGS_ID
GO

/****** Object:  View [dbo].[A_V_FORECASTS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_FORECASTS_APPROVED_DATA]
AS
SELECT     dbo.A_FORECASTS.ID, dbo.A_FORECASTS.HISTORY_REF_ID, dbo.A_FORECASTS_HISTORY.OBJECT_ID, dbo.A_FORECASTS_HISTORY.NAME, 
                      dbo.A_FORECASTS_HISTORY.START_MONTH, dbo.A_FORECASTS_HISTORY.START_YEAR, dbo.A_FORECASTS_HISTORY.DRCM, 
                      dbo.A_FORECASTS_HISTORY.MODBY, dbo.A_FORECASTS_HISTORY.CO, dbo.A_FORECASTS_HISTORY.STOP_MONTH, 
                      dbo.A_FORECASTS_HISTORY.STOP_YEAR, dbo.A_FORECASTS_HISTORY.START_DATE, dbo.A_FORECASTS_HISTORY.STOP_DATE, 
                      dbo.A_FORECASTS_HISTORY.F_TYPE
FROM         dbo.A_FORECASTS INNER JOIN
                      dbo.A_FORECASTS_HISTORY ON dbo.A_FORECASTS.HISTORY_REF_ID = dbo.A_FORECASTS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_DISCUSSION_TASKS]
AS
SELECT     dbo.A_TASKS.DESCRIPTION, dbo.A_TASK_DISCUSSION_LINK.DISCUSSION_ID, dbo.A_TASKS.STATUS, dbo.A_TASKS.REQUESTOR, 
                      dbo.A_TASKS.COMPLETED_BY, dbo.A_TASKS.LATEST_REQUESTEE_NAME
FROM         dbo.A_TASKS INNER JOIN
                      dbo.A_TASK_DISCUSSION_LINK ON dbo.A_TASKS.ID = dbo.A_TASK_DISCUSSION_LINK.TASK_ID
GO

/****** Object:  View [dbo].[A_V_FORECAST_ITEMS_WITH_PARENT_ITEM_NAM]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_FORECAST_ITEMS_WITH_PARENT_ITEM_NAM]
AS
SELECT     dbo.A_FORECAST_ITEMS.ID AS CHILD_ID, A_FORECAST_ITEMS_1.ID AS PARENT_ID, 
                      A_ACCOUNTS_HISTORY_1.NAME AS PARENT_ACCOUNT_NAME
FROM         dbo.A_ACCOUNTS_HISTORY INNER JOIN
                      dbo.A_FORECAST_ITEMS INNER JOIN
                      dbo.A_ACCOUNTS ON dbo.A_FORECAST_ITEMS.ACCOUNT_ID = dbo.A_ACCOUNTS.ID ON 
                      dbo.A_ACCOUNTS_HISTORY.ID = dbo.A_ACCOUNTS.HISTORY_REF_ID INNER JOIN
                      dbo.A_ACCOUNTS A_ACCOUNTS_1 INNER JOIN
                      dbo.A_FORECAST_ITEMS A_FORECAST_ITEMS_1 ON A_ACCOUNTS_1.ID = A_FORECAST_ITEMS_1.ACCOUNT_ID ON 
                      dbo.A_ACCOUNTS_HISTORY.PARENT_ACCOUNT = A_ACCOUNTS_1.ID AND 
                      dbo.A_FORECAST_ITEMS.FORECAST_ID = A_FORECAST_ITEMS_1.FORECAST_ID INNER JOIN
                      dbo.A_ACCOUNTS_HISTORY A_ACCOUNTS_HISTORY_1 ON A_ACCOUNTS_1.HISTORY_REF_ID = A_ACCOUNTS_HISTORY_1.ID
GO

/****** Object:  View [dbo].[A_APPROVED_REGIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_APPROVED_REGIONS]
AS
SELECT     r.ID, r.HISTORY_REF_ID, rh.OBJECT_ID, o.ID AS OBJ_ID, rh.NAME, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, 
                      o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY
FROM         dbo.A_REGIONS r INNER JOIN
                      dbo.A_REGIONS_HISTORY rh ON r.HISTORY_REF_ID = rh.ID INNER JOIN
                      dbo.A_OBJECTS o ON rh.OBJECT_ID = o.ID
GO

/****** Object:  View [dbo].[A_O_FORECASTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_FORECASTS]
AS
SELECT     FH.*, O.ID AS OBJ_ID, O.LOCKED_BY AS LOCKED_BY, O.UNLOCKED_BY AS UNLOCKED_BY, O.CREATED_BY AS CREATED_BY, 
                      O.CREATE_DATE AS CREATE_DATE, O.ROOT AS ROOT, O.REV_INFO AS REV_INFO, O.CREATING_CO AS CREATING_CO, O.STATUS AS STATUS, 
                      O.REV AS REV, O.WFS_ID AS WFS_ID, O.LOCKED_BY_NAME AS LOCKED_BY_NAME, O.CREATING_CO_NAME AS CREATING_CO_NAME, 
                      O.APPROVAL_ACTIVITY AS APPROVAL_ACTIVITY
FROM         dbo.A_FORECASTS_HISTORY FH INNER JOIN
                      dbo.A_OBJECTS O ON FH.OBJECT_ID = O.ID
GO

/****** Object:  View [dbo].[A_V_TASK_IS_QUICK_PRICE_CHECK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_IS_QUICK_PRICE_CHECK]
AS
SELECT     dbo.A_QUOTE_ORDER_LINK.TASK_ID, dbo.A_PRODUCTS_QUICK_PRICE.ORDER_ID
FROM         dbo.A_PRODUCTS_QUICK_PRICE INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_PRODUCTS_QUICK_PRICE.ORDER_ID = dbo.A_QUOTE_ORDER_LINK.ORDER_ID
GO

/****** Object:  View [dbo].[A_V_DISCUSSION_GET_FILE_ATTACHMENT_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_DISCUSSION_GET_FILE_ATTACHMENT_INFO]
AS
SELECT     dbo.A_DISCUSSION_RESPONSE.ID, dbo.A_DISCUSSION_ATTACHMENTS.DOC_ID AS VALUE, dbo.A_DOCUMENTS.NAME AS SHOW, 
                      dbo.A_DOCUMENTS.DESCRIPTION
FROM         dbo.A_DISCUSSION_RESPONSE INNER JOIN
                      dbo.A_DISCUSSION_ATTACHMENTS ON dbo.A_DISCUSSION_RESPONSE.ID = dbo.A_DISCUSSION_ATTACHMENTS.RESPONSE_ID INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_DISCUSSION_ATTACHMENTS.DOC_ID = dbo.A_DOCUMENTS.ID
GO

/****** Object:  View [dbo].[A_APPROVED_ROLE_ASSIGNEES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_APPROVED_ROLE_ASSIGNEES]
AS
SELECT     dbo.A_ROLES.ID AS ROLE_ID, dbo.A_ROLES_HISTORY.NAME AS ROLE_NAME, dbo.A_ROLE_ASSIGNEE.ROLE, dbo.A_ROLE_ASSIGNEE.PERSON, 
                      dbo.A_ROLE_ASSIGNEE.STATUS, dbo.A_ROLE_ASSIGNEE.ROLE_ASSIGNED, dbo.A_ROLE_ASSIGNEE.TYPE,
dbo.A_ROLE_ASSIGNEE.StartDate,
dbo.A_ROLE_ASSIGNEE.EndDate
FROM         dbo.A_ROLES INNER JOIN
                      dbo.A_ROLE_ASSIGNEE ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLE_ASSIGNEE.ROLE INNER JOIN
                      dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID
WHERE     (dbo.A_ROLE_ASSIGNEE.STATUS = N'ACTIVE')
GO

/****** Object:  View [dbo].[A_V_WORKFLOWS_WITH_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_WORKFLOWS_WITH_STAGES]
AS
SELECT     dbo.A_WORKFLOW_STAGE_LINK.WF_ID, dbo.A_WORKFLOW_STAGE_LINK.WF_STAGE_ID, dbo.A_WORKFLOWS.NAME AS WF_NAME, 
                      dbo.A_WF_STAGES.NAME AS WF_STAGE_NAME, dbo.A_WORKFLOW_STAGE_LINK.NUM, dbo.A_WF_STAGES.HIDE AS STAGE_HIDE, 
                      dbo.A_WORKFLOWS.HIDE
FROM         dbo.A_WORKFLOWS INNER JOIN
                      dbo.A_WORKFLOW_STAGE_LINK ON dbo.A_WORKFLOWS.ID = dbo.A_WORKFLOW_STAGE_LINK.WF_ID INNER JOIN
                      dbo.A_WF_STAGES ON dbo.A_WORKFLOW_STAGE_LINK.WF_STAGE_ID = dbo.A_WF_STAGES.ID
GO

/****** Object:  View [dbo].[A_V_OBJECTS_WITH_LOCKED_BY_BOSS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_OBJECTS_WITH_LOCKED_BY_BOSS]
AS
SELECT     A_PEOPLE_1.ID AS BOSS_ID, dbo.A_OBJECTS.*
FROM         dbo.A_OBJECTS INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_OBJECTS.LOCKED_BY = dbo.A_PEOPLE_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_PEOPLE_HISTORY A_PEOPLE_1 ON dbo.A_PEOPLE_HISTORY.BOSS = A_PEOPLE_1.ID
GO

/****** Object:  View [dbo].[Portal_MenuView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

Create View [dbo].[Portal_MenuView]
AS
SELECT 
Id,
URL,
NAME,
INFO,
Num,
MENU_GROUP,
Icon,
GroupIcon AS GroupIcon,
MENU_GROUP AS GroupMenu,
OrderNumber AS OrderNumber,
IsParent
FROM A_MENUS
GO

/****** Object:  View [dbo].[Portal_WorkflowStages]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE View [dbo].[Portal_WorkflowStages]
	as
	select ID as Id, NAME as Name FROM A_WF_STAGES
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_PROCEDURE_STEPS_PREV_STEP_BY_OLD_STEP_RELATIONSHIP]
AS
SELECT     PREV_STEP.ID AS PREV_STEP, MY_STEP.ID AS MY_STEP, PREV_STEP.PROCEDURE_ID
FROM         dbo.A_PROCEDURE_STEPS MY_STEP INNER JOIN
                      dbo.A_PROCEDURE_STEP_PRECEDING_STEPS LINK ON MY_STEP.OLD_STEP_ID = LINK.MY_STEP INNER JOIN
                      dbo.A_PROCEDURE_STEPS PREV_STEP ON LINK.PREV_STEP = PREV_STEP.OLD_STEP_ID
GO

/****** Object:  View [dbo].[Portal_ViewHistryDetails]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[Portal_ViewHistryDetails]
as
select
ID as Id,
PARENT_ID as ParentId,
DESCRIPTION as Description,
STATUS as Status,
COMMENT as Comment,
REQUESTOR as Requestor,
COMPLETED_BY as CompletedBy,
MODBY as ModBy,
DRCM as Drcm,
CHILD_ORDER as ChildOrder,
CREATED_BY as CreatedBy,
CREATE_DATE as CreateDate,
CLOSED as Closed,
OPENED_BY as OpenedBy,
OPEN_DATE as OpenDate,
SYSTEM_TASK as SystemTask,
PROCEDURE_ID as ProcedureId,
PROCEDURE_STEP_ID as ProcedureStepId,
SECURITY_LEVEL as SecurityLevel,
COUNTER as Counter,
REQUESTEE_ID as RequesteeId,
GROUP_REQUESTEE_ID as GroupRequesteeId,
COUNTER_NAME as CounterName,
COUNTER_VALUE as CounterValue,
ORIG_PLANNED_START_DATE as OrigPlannedStartDate,
ORIG_PLANNED_STOP_DATE as OrigPlannedStopDate,
CUR_PLANNED_START_DATE as CurPlannedStartDate,
CUR_PLANNED_STOP_DATE as CurPlannedStopDate,
ACTUAL_START_DATE as ActualStartDate,
ACTUAL_STOP_DATE as ActualStopDate,
ORIG_PLANNED_COUNTER_START as OrigPlannedCounterStart,
ORIG_PLANNED_COUNTER_STOP as OrigPlannedCounterStop,
CUR_PLANNED_COUNTER_START as CurPlannedCounterStart,
CUR_PLANNED_COUNTER_STOP as CurPlannedCounterStop,
ACTUAL_COUNTER_START as ActualCounterStart,
ACTUAL_COUNTER_STOP as ActualCounterStop,
LATEST_REQUESTEE_NAME as LatestRequesteeName,
HAS_DISCUSSION as HasDiscussion,
HAS_SURVEY as HasSurvey,
HAS_CHILD as HasChild,
HAS_REF_PROC as HasRefProc,
HAS_FILE as HasFile,
ORIG_REQUESTOR_ID as OrigRequestorId,
HAS_REF_OBJ as HasRefObj,
HAS_MONITOR as HasMonitor,
COLOR_CODE as ColorCode,
LAST_REQUEST_DATE as LastRequestDate,
PRIORITY as Priority,
CHILD_STATUS as ChildStatus,
IS_QUOTE as IsQuote,
IS_QUOTE_ACCEPT as IsQuoteAccept,
IS_FILL as IsFill,
MANAGER as Manager,
DNR_TYPE as DnrType,
RECURSION_NUMBER as RecurisionNumber,
REPEAT_FROM_STEP as RepeatFromStep
from A_TASKS
GO

/****** Object:  View [dbo].[Portal_UserView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_UserView]
AS


SELECT
 u.[Id]
,u.[Email]
,u.[EmailConfirmed]
,u.[PasswordHash]
,u.[SecurityStamp]
,u.[PhoneNumber] AS Phone
,u.Phone2
,u.[PhoneNumberConfirmed]
,u.[TwoFactorEnabled]
,u.[LockoutEndDateUtc]
,u.[LockoutEnabled]
,u.[AccessFailedCount]
,u.[UserName]
,u.[FirstName]
,u.[LastName]
,u.[FirstName]+ ' ' +u.[LastName] AS FullName
,u.[IsActive]
,u.[CreatedDate]
,u.[TimeZone]
,r.Id AS RoleId
,r.Name AS RoleName
,u.ParentId
,c.NAME AS CompanyName
,u.CompanyId
,u.IsActive AS Status
,'' AS Title
FROM [AspNetUsers] u 
LEFT JOIN AspNetUserRoles ur ON ur.UserId = u.Id
LEFT JOIN AspNetRoles r ON r.Id = ur.RoleId
LEFT JOIN [dbo].[A_COMPANIES] c ON c.ID =u.CompanyId
GO

/****** Object:  View [dbo].[A_V_MEETING_AGENDA_ITEM_GET_DOC_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_MEETING_AGENDA_ITEM_GET_DOC_INFO]
AS
SELECT     dbo.A_DOCUMENTS.ID, dbo.A_DOCUMENTS.NAME AS SHOW, dbo.A_DOCUMENTS.DESCRIPTION, 
                      dbo.A_MEETING_AGENDA_ITEM_DOC_LINK.AGENDA_ID, dbo.A_MEETING_AGENDA_ITEM_DOC_LINK.DOC_ID AS VALUE, 
                      dbo.A_MEETING_AGENDA_ITEM_DOC_LINK.CREATOR_ID
FROM         dbo.A_MEETING_AGENDA_ITEM_DOC_LINK LEFT OUTER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_MEETING_AGENDA_ITEM_DOC_LINK.DOC_ID = dbo.A_DOCUMENTS.DOC_ID
GO

/****** Object:  View [dbo].[Portal_TimeZoneView]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE view [dbo].[Portal_TimeZoneView]
AS
SELECT
Id,
DESCRIPTION + '(' + cast(DATEADD(hh,G_DIFF,getDate()) AS nvarchar(50)) + ')' AS Description, NUM as Num
FROM A_TIME_ZONES
GO

/****** Object:  View [dbo].[Portal_TimeZones]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_TimeZones]
	as
SELECT ID as Id, DESCRIPTION + '(' + cast(DATEADD(hh,G_DIFF,getDate()) AS nvarchar(50)) + ')' AS Description, NUM as Num FROM A_TIME_ZONES
GO

/****** Object:  View [dbo].[Portal_RolesApprovedDataQuick]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_RolesApprovedDataQuick]
	AS
SELECT dbo.A_OBJECTS.CREATING_CO as creatingCo, dbo.A_ROLES_HISTORY.NAME as Name, dbo.A_ROLES.ID as Id, dbo.A_ROLES.HISTORY_REF_ID
as HistoryRefId
FROM dbo.A_ROLES INNER JOIN
dbo.A_ROLES_HISTORY ON dbo.A_ROLES.HISTORY_REF_ID = dbo.A_ROLES_HISTORY.ID INNER JOIN
dbo.A_OBJECTS ON dbo.A_ROLES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_PROC_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_PROC_DATA]
AS
SELECT     dbo.A_PROCEDURE_STEPS.ID AS STEP_ID, dbo.A_PROCEDURE_STEPS.STEP_TEXT, dbo.A_PROCEDURE_STEPS.SYSTEM_TASK, 
                      dbo.A_OBJECTS.CREATING_CO, dbo.A_PROCEDURES_HISTORY.ID AS PROC_HIST_ID
FROM         dbo.A_PROCEDURE_STEPS INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURE_STEPS.PROCEDURE_ID = dbo.A_PROCEDURES_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_PROCEDURES_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER_II]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER_II]
AS
SELECT     dbo.A_PROCEDURE_STEPS.PRINT_ORDER AS STEP_ORDER, dbo.A_TASKS.*
FROM         dbo.A_TASKS LEFT OUTER JOIN
                      dbo.A_PROCEDURE_STEPS ON dbo.A_TASKS.PROCEDURE_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
GO

/****** Object:  View [dbo].[A_V_INVOICE_TASK_STOP_DATES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE  VIEW [dbo].[A_V_INVOICE_TASK_STOP_DATES]
AS
SELECT     dbo.A_ACCOUNT_INVOICES.ID AS INVOICE_ID, dbo.A_TASKS.ACTUAL_STOP_DATE
FROM         dbo.A_TASKS INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON dbo.A_TASKS.ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID INNER JOIN
                      dbo.A_ACCOUNT_INVOICES INNER JOIN
                      dbo.A_ACCOUNT_INVOICE_ITEMS ON dbo.A_ACCOUNT_INVOICES.ID = dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID ON 
                      dbo.A_TASK_ORDER_INFORMATION.FILL_ITEM_ID = dbo.A_ACCOUNT_INVOICE_ITEMS.FILL_ID
GO

/****** Object:  View [dbo].[A_O_NOUN_HIERARCHIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_NOUN_HIERARCHIES]
AS
SELECT     nh.ID, nh.NAME, nh.ROOT_CHILD, nh.OBJECT_ID, o.ID AS OBJ_ID, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, 
                      o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, 
                      c.APPROVED_OBJ_ID, A_OBJECTS_1.OBJ_DESC AS CHILD_DESC
FROM         dbo.A_NOUN_HIERARCHIES_HISTORY nh INNER JOIN
                      dbo.A_OBJECTS o ON nh.OBJECT_ID = o.ID INNER JOIN
                      dbo.A_NOUN_HIERARCHY_CHILDREN_EDITING c ON nh.ROOT_CHILD = c.ID AND nh.ID = c.HIERARCHY_ID INNER JOIN
                      dbo.A_APPROVED_OBJECTS ON c.APPROVED_OBJ_ID = dbo.A_APPROVED_OBJECTS.ID INNER JOIN
                      dbo.A_OBJECTS A_OBJECTS_1 ON dbo.A_APPROVED_OBJECTS.OBJ_REF_ID = A_OBJECTS_1.ID
GO

/****** Object:  View [dbo].[Portal_Languages]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[Portal_Languages]
	as
	SELECT LANG_CODE as LanguageCode, LANGUAGE as Language FROM A_LANGUAGES
GO

/****** Object:  View [dbo].[A_V_TASK_WITH_QUOTE_ORDER_LINK_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASK_WITH_QUOTE_ORDER_LINK_INFO]
AS
SELECT     dbo.A_QUOTE_ORDER_LINK.ID AS QOL_ID, dbo.A_TASKS.*, dbo.A_QUOTE_ORDER_LINK.QUOTE_ID, dbo.A_QUOTE_ORDER_LINK.ORDER_ID, 
                      dbo.A_QUOTE_ORDER_LINK.STATUS AS QOL_STATUS, dbo.A_QUOTE_ORDER_LINK.CUSTOMER, dbo.A_QUOTE_ORDER_LINK.SUPPLIER
FROM         dbo.A_TASKS INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_TASKS.ID = dbo.A_QUOTE_ORDER_LINK.TASK_ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PERFERENCES_GET_HISTORY_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PEOPLE_PERFERENCES_GET_HISTORY_ID]
AS
SELECT     dbo.A_OBJECTS.ID, dbo.A_OBJECTS.ROOT, dbo.A_OBJECTS.OBJ_ID AS peopleHistID
FROM         dbo.A_PEOPLE_HISTORY INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_PEOPLE_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_ITEMS_WITH_QUOTE_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_QUOTE_ITEMS_WITH_QUOTE_DATA]
AS
SELECT     dbo.A_QUOTES.ID AS QUOTE_ID, dbo.A_QUOTES.HISTORY_REF_ID AS QUOTE_HIST_ID, dbo.A_ORDER_ITEMS.PROD_PRICE_LIST, 
                      dbo.A_QUOTES_HISTORY.DESCRIPTION, dbo.A_ORDER_ITEMS.ADD_COST_ID, dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID, 
                      dbo.A_ORDER_ITEMS.PPL_HIST_ID, dbo.A_QUOTES_HISTORY.PROGRESS, dbo.A_QUOTES_HISTORY.EXPIRATION_DATE, 
                      dbo.A_QUOTES_HISTORY.CREATION_DATE, dbo.A_QUOTES_HISTORY.CUSTOMER_CO, 
                      dbo.A_ORDERS_HISTORY.DESCRIPTION AS ORDER_DESCRIPTION, dbo.A_ORDERS.ID AS ORDER_ID, 
                      dbo.A_ORDERS.HISTORY_REF_ID AS ORDER_HIST_ID, dbo.A_ORDERS_HISTORY.OBJECT_ID AS ORDER_OBJ_ID, 
                      dbo.A_QUOTES_HISTORY.OBJECT_ID AS QUOTE_OBJ_ID, dbo.A_OBJECTS.STATUS
FROM         dbo.A_QUOTES_HISTORY INNER JOIN
                      dbo.A_QUOTES ON dbo.A_QUOTES_HISTORY.ID = dbo.A_QUOTES.HISTORY_REF_ID INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_QUOTES_HISTORY.ID = dbo.A_ORDER_ITEMS.QUOTE_ID INNER JOIN
                      dbo.A_ORDERS INNER JOIN
                      dbo.A_ORDERS_HISTORY ON dbo.A_ORDERS.HISTORY_REF_ID = dbo.A_ORDERS_HISTORY.ID INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_ORDERS.ID = dbo.A_QUOTE_ORDER_LINK.ORDER_ID ON 
                      dbo.A_QUOTES_HISTORY.ID = dbo.A_QUOTE_ORDER_LINK.QUOTE_ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_ORDERS_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
WHERE     (dbo.A_ORDER_ITEMS.ADD_COST_ID IS NULL) AND (dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_COUNTERS_BY_APPROVED_ID]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_COUNTERS_BY_APPROVED_ID]
AS
SELECT     dbo.A_COUNTERS.ID, dbo.A_COUNTERS.HISTORY_REF_ID, dbo.A_COUNTERS_HISTORY.NAME, dbo.A_COUNTERS_HISTORY.OBJECT_ID, 
                      dbo.A_COUNTERS_HISTORY.CUR_VAL, dbo.A_COUNTERS_HISTORY.DT_RECORDED, dbo.A_COUNTERS_HISTORY.REL_OBJECT_ID, 
                      dbo.A_COUNTERS_HISTORY.REL_OBJEC_NAME
FROM         dbo.A_COUNTERS INNER JOIN
                      dbo.A_COUNTERS_HISTORY ON dbo.A_COUNTERS.HISTORY_REF_ID = dbo.A_COUNTERS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_PEOPLE_PERFERENCES_GET_DATA_BY_NTLOGIN]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_PERFERENCES_GET_DATA_BY_NTLOGIN]
AS
SELECT     dbo.A_PEOPLE.ID, dbo.A_PEOPLE_HISTORY.LOGIN, dbo.A_PEOPLE_HISTORY.PASSWORD, dbo.A_PEOPLE_SEARCH_TABLE.TIME_ZONE, 
                      dbo.A_PEOPLE_HISTORY.LANG, dbo.A_PEOPLE_HISTORY.FULL_NAME, dbo.A_PEOPLE_HISTORY.TOOL_BOX
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_PEOPLE_SEARCH_TABLE ON dbo.A_PEOPLE_HISTORY.ID = dbo.A_PEOPLE_SEARCH_TABLE.ID
GO

/****** Object:  View [dbo].[A_V_EMAILS_FOR_APPROVED_PEOPLE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_EMAILS_FOR_APPROVED_PEOPLE]
AS
SELECT     dbo.A_PEOPLE.ID, dbo.A_EMAILS.ADDY, dbo.A_EMAILS.TYPE AS EMAIL_TYPE, dbo.A_EMAILS.EMAIL_TYPE AS EMAIL_BODY_TYPE
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_EMAILS ON dbo.A_PEOPLE_HISTORY.OBJECT_ID = dbo.A_EMAILS.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_WF_STAGES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_WF_STAGES]
AS
SELECT     s.NAME, s.OBJECT_ID, s.ID, s.HIDE, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.STATUS, 
                      o.CREATING_CO, o.REV, o.WFS_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_WF_STAGES s ON o.ID = s.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_TT_VERBS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TT_VERBS_APPROVED_DATA]
AS
SELECT     dbo.A_TT_VERBS.ID, dbo.A_TT_VERBS_HISTORY.NAME, dbo.A_TT_VERBS_HISTORY.SHOW_NAME, dbo.A_OBJECTS.STATUS, 
                      dbo.A_OBJECTS.CREATING_CO
FROM         dbo.A_TT_VERBS INNER JOIN
                      dbo.A_TT_VERBS_HISTORY ON dbo.A_TT_VERBS.HISTORY_REF_ID = dbo.A_TT_VERBS_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_TT_VERBS_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
WHERE     (dbo.A_OBJECTS.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_REF_THEORIES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_THEORY_HEADER_REF_THEORIES]
AS
SELECT     dbo.A_THEORY_HISTORY.ID AS HIST_ID, dbo.A_THEORY_HISTORY.OBJECT_ID AS OBJECT_ID, dbo.A_THEORY.ID AS LINKED_THEORY_ID, 
                      A_THEORY_HISTORY_1.NAME AS LINKED_THEORY_NAME
FROM         dbo.A_THEORY INNER JOIN
                      dbo.A_THEORY_HISTORY A_THEORY_HISTORY_1 ON dbo.A_THEORY.HISTORY_REF_ID = A_THEORY_HISTORY_1.ID INNER JOIN
                      dbo.A_THEORY_HISTORY INNER JOIN
                      dbo.A_THEORY_REFERENCE_THEORY ON dbo.A_THEORY_HISTORY.ID = dbo.A_THEORY_REFERENCE_THEORY.THEORY_ID ON 
                      dbo.A_THEORY.ID = dbo.A_THEORY_REFERENCE_THEORY.THEORY_LINK
GO

/****** Object:  View [dbo].[A_V_THEORY_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_THEORY_APPROVED_DATA]
AS
SELECT     dbo.A_THEORY.ID, dbo.A_THEORY.HISTORY_REF_ID, dbo.A_THEORY_HISTORY.OBJECT_ID, dbo.A_THEORY_HISTORY.NAME, 
                      dbo.A_THEORY_HISTORY.COMMENTS, dbo.A_THEORY_HISTORY.SECURITY_LEVEL, dbo.A_THEORY_HISTORY.CREATING_DEPT, 
                      dbo.A_OBJECTS.CREATING_CO, dbo.A_THEORY_HISTORY.DRCM, dbo.A_THEORY_HISTORY.MODBY
FROM         dbo.A_THEORY INNER JOIN
                      dbo.A_THEORY_HISTORY ON dbo.A_THEORY.HISTORY_REF_ID = dbo.A_THEORY_HISTORY.ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_THEORY_HISTORY.OBJECT_ID = dbo.A_OBJECTS.ID
GO

/****** Object:  View [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASKS_WITH_PROCEDURE_STEP_ORDER]
AS
SELECT     dbo.A_PROCEDURE_STEPS.PRINT_ORDER AS STEP_ORDER, dbo.A_TASKS.*
FROM         dbo.A_TASKS LEFT OUTER JOIN
                      dbo.A_PROCEDURE_STEPS ON dbo.A_TASKS.PROCEDURE_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
GO

/****** Object:  View [dbo].[A_V_NEEDS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_NEEDS_APPROVED_DATA]
AS
SELECT     dbo.A_NEEDS.ID, dbo.A_NEEDS.HISTORY_REF_ID, dbo.A_NEEDS_HISTORY.OBJECT_ID, dbo.A_NEEDS_HISTORY.DESCRIPTION, 
                      dbo.A_NEEDS_HISTORY.CUSTOMER_CO, dbo.A_NEEDS_HISTORY.TIMEFRAME, dbo.A_NEEDS_HISTORY.IMPORTANCE, 
                      dbo.A_NEEDS_HISTORY.NEED_SIZE, dbo.A_NEEDS_HISTORY.ADVERTISING_START_DATE, dbo.A_NEEDS_HISTORY.ADVERTISING_STOP_DATE, 
                      dbo.A_NEEDS_HISTORY.ADVERSTISE_PUBLICLY, dbo.A_NEEDS_HISTORY.CONTACT_NAME, dbo.A_NEEDS_HISTORY.CONTACT_EMAIL, 
                      dbo.A_NEEDS_HISTORY.CONTACT_PHONE, dbo.A_NEEDS_HISTORY.DRCM, dbo.A_NEEDS_HISTORY.MODBY
FROM         dbo.A_NEEDS INNER JOIN
                      dbo.A_NEEDS_HISTORY ON dbo.A_NEEDS.HISTORY_REF_ID = dbo.A_NEEDS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_ACCOUNT_INVOICE_ITEMS_WITH_ACCT_AND_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_ACCOUNT_INVOICE_ITEMS_WITH_ACCT_AND_STATUS]
AS
SELECT     dbo.A_ACCOUNT_INVOICES.ACCOUNT_ID, dbo.A_ACCOUNT_INVOICES.STATUS, dbo.A_ACCOUNT_INVOICE_ITEMS.AMOUNT, 
                      dbo.A_ACCOUNT_INVOICE_ITEMS.TAX
FROM         dbo.A_ACCOUNT_INVOICE_ITEMS INNER JOIN
                      dbo.A_ACCOUNT_INVOICES ON dbo.A_ACCOUNT_INVOICE_ITEMS.INVOICE_ID = dbo.A_ACCOUNT_INVOICES.ID
GO

/****** Object:  View [dbo].[A_V_TASK_REF_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_REF_FILES]
AS
SELECT     T.ID AS TASK_ID, T.DESCRIPTION, TRF.FILE_ID, TRF.STATUS, TRF.DELETED_BY_NAME, TRF.DATE_DELETED, TRF.DELETED_BY, 
                      D.NAME AS FILE_NAME, D.DESCRIPTION AS FILE_DESCRIPTION, TRF.ID, D.SERVER_PATH AS PATH, D.CONTENTTYPE, D.DRCM
FROM         dbo.A_TASK_REF_FILES TRF INNER JOIN
                      dbo.A_DOCUMENTS D ON TRF.FILE_ID = D.ID INNER JOIN
                      dbo.A_TASKS T ON TRF.TASK_ID = T.ID
GO

/****** Object:  View [dbo].[A_V_TASK_QUOTE_ACCEPT_INFORMATION]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_TASK_QUOTE_ACCEPT_INFORMATION]
AS
SELECT     dbo.A_QUOTE_ORDER_LINK.QA_TASK_ID, dbo.A_QUOTE_ORDER_LINK.TASK_ID, dbo.A_QUOTE_ORDER_LINK.SUPPLIER, 
                      dbo.A_QUOTE_ORDER_LINK.CUSTOMER, dbo.A_QUOTE_ORDER_LINK.ORDER_ID, dbo.A_QUOTE_ORDER_LINK.QUOTE_ID AS QUOTE_HIST_ID, 
                      dbo.A_QUOTES_HISTORY.OBJECT_ID AS QUOTE_ID, dbo.A_QUOTES_HISTORY.PROGRESS AS QUOTE_PROGRESS, 
                      dbo.A_QUOTES_HISTORY.DESCRIPTION AS QUOTE_DESCRIPTION, dbo.A_TASKS.STATUS, dbo.A_TASKS.REQUESTEE_ID, 
                      dbo.A_TASKS.GROUP_REQUESTEE_ID, dbo.A_QUOTE_ORDER_LINK.ID
FROM         dbo.A_QUOTE_ORDER_LINK INNER JOIN
                      dbo.A_TASKS ON dbo.A_QUOTE_ORDER_LINK.QA_TASK_ID = dbo.A_TASKS.ID INNER JOIN
                      dbo.A_QUOTES_HISTORY ON dbo.A_QUOTE_ORDER_LINK.QUOTE_ID = dbo.A_QUOTES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_STEPS_WITH_PREVIOUS_STEP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_STEPS_WITH_PREVIOUS_STEP]
AS
SELECT     dbo.A_PROCEDURE_STEP_PRECEDING_STEPS.PREV_STEP AS PREV_STEP, dbo.A_PROCEDURE_STEPS.ID, 
                      dbo.A_PROCEDURE_STEPS.PROCEDURE_ID, dbo.A_PROCEDURE_STEPS.STEP_TEXT, dbo.A_PROCEDURE_STEPS.START_ON_COUNTER, 
                      dbo.A_PROCEDURE_STEPS.COUNTER_VALUE, dbo.A_PROCEDURE_STEPS.COUNTER_UNIT, dbo.A_PROCEDURE_STEPS.REL_OR_ABS, 
                      dbo.A_PROCEDURE_STEPS.FROM_START_OR_STOP, dbo.A_PROCEDURE_STEPS.SYSTEM_TASK, dbo.A_PROCEDURE_STEPS.DESTINATION, 
                      dbo.A_PROCEDURE_STEPS.SPECIFIC_LOCATION, dbo.A_PROCEDURE_STEPS.REFERENCE_VERB, 
                      dbo.A_PROCEDURE_STEPS.REFERENCE_OBJECT, dbo.A_PROCEDURE_STEPS.COMMENTS, dbo.A_PROCEDURE_STEPS.GOTO_STEP, 
                      dbo.A_PROCEDURE_STEPS.GOTO_STEP_ID, dbo.A_PROCEDURE_STEPS.CYCLES, dbo.A_PROCEDURE_STEPS.CYCLE_ON_COUNTER, 
                      dbo.A_PROCEDURE_STEPS.CYCLE_COUNT, dbo.A_PROCEDURE_STEPS.CYCLE_UNIT, dbo.A_PROCEDURE_STEPS.DRCM, 
                      dbo.A_PROCEDURE_STEPS.MODBY, dbo.A_PROCEDURE_STEPS.PRINT_ORDER, dbo.A_PROCEDURE_STEPS.OLD_PROCEDURE_ID, 
                      dbo.A_PROCEDURE_STEPS.OLD_STEP_ID, dbo.A_PROCEDURE_STEPS.DURATION, dbo.A_PROCEDURE_STEPS.DURATION_TYPE, 
                      dbo.A_PROCEDURE_STEPS.DATE_LAST_MODIFIED
FROM         dbo.A_PROCEDURE_STEPS LEFT OUTER JOIN
                      dbo.A_PROCEDURE_STEP_PRECEDING_STEPS ON dbo.A_PROCEDURE_STEPS.ID = dbo.A_PROCEDURE_STEP_PRECEDING_STEPS.MY_STEP
GO

/****** Object:  View [dbo].[A_V_TASK_PART_PURCH_ITEM_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_TASK_PART_PURCH_ITEM_QUICK]
AS
SELECT     dbo.A_TASK_ORDER_INFORMATION.TASK_ID, dbo.A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED.ACTUAL_PART_ID, 
                      dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID, dbo.A_TASKS.STATUS
FROM         dbo.A_TASKS INNER JOIN
                      dbo.A_TASK_ORDER_INFORMATION ON dbo.A_TASKS.ID = dbo.A_TASK_ORDER_INFORMATION.TASK_ID INNER JOIN
                      dbo.A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED ON 
                      dbo.A_TASKS.ID = dbo.A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED.TASK_ID
WHERE     (dbo.A_TASK_ORDER_INFORMATION.PURCHASE_ITEM_ID = '215214') AND 
                      (dbo.A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED.ACTUAL_PART_ID = '211377')
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS]
AS
SELECT        dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, dbo.A_LOCATIONS_HISTORY.NAME, dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION, 
                         dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION_NAME, dbo.A_LOCATIONS_HISTORY.ADDRESS_1, dbo.A_LOCATIONS_HISTORY.ADDRESS_2, 
                         dbo.A_LOCATIONS_HISTORY.FULL_ADDRESS, dbo.A_LOCATIONS_HISTORY.CITY, dbo.A_LOCATIONS_HISTORY.STATE, dbo.A_LOCATIONS_HISTORY.COUNTRY, 
                         dbo.A_LOCATIONS_HISTORY.POSTAL_CODE, dbo.A_LOCATIONS_HISTORY.REGION, dbo.A_LOCATIONS_HISTORY.REGION_NAME, 
                         dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS, dbo.A_LOCATIONS_HISTORY.OBJECT_ID, dbo.A_LOCATIONS_HISTORY.PARENT_PATH, 
                         dbo.A_LOCATIONS_HISTORY.COMPLETE_NAME
FROM            dbo.A_LOCATIONS_HISTORY INNER JOIN
                         dbo.A_LOCATIONS ON dbo.A_LOCATIONS_HISTORY.ID = dbo.A_LOCATIONS.HISTORY_REF_ID
WHERE        (LEN(dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS) <= 4) AND (dbo.A_LOCATIONS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_OT_HOURS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALLS_OT_HOURS]
AS
SELECT     SUM(HOURS) AS OT_HOURS, ID, WEEKLY_ID, D, MO, YR, HOUR_TYPE, HOURS
FROM         dbo.A_SERVICE_CALL_WORK_TIME
WHERE     (HOUR_TYPE = 'OVER')
GROUP BY ID, WEEKLY_ID, D, MO, YR, HOUR_TYPE, HOURS
GO

/****** Object:  View [dbo].[A_V_PREPOP_QUICK]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE  VIEW [dbo].[A_V_PREPOP_QUICK]
AS
SELECT     TOP 100 PERCENT dbo.A_PROCEDURE_STEPS.STEP_TEXT, dbo.A_PREPOP.ID, dbo.A_PREPOP.HISTORY_REF_ID, 
                      dbo.A_PREPOP_HISTORY.OBJECT_ID, dbo.A_PREPOP.CREATING_CO, dbo.A_PREPOP.STATUS, dbo.A_PROCEDURE_STEPS.ID AS STEP_ID,
					  TITLE
FROM         dbo.A_PREPOP INNER JOIN
                      dbo.A_PREPOP_HISTORY ON dbo.A_PREPOP.HISTORY_REF_ID = dbo.A_PREPOP_HISTORY.ID INNER JOIN
                      dbo.A_PROCEDURE_STEPS ON dbo.A_PREPOP_HISTORY.PROC_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
WHERE     (dbo.A_PREPOP.STATUS = 'APPROVED')
ORDER BY dbo.A_PROCEDURE_STEPS.STEP_TEXT
GO

/****** Object:  View [dbo].[A_V_PURCHASE_QUOTE_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PURCHASE_QUOTE_STATUS]
AS
SELECT     ph.ID AS PURCHASE_HIST_ID, p.ID AS PURCHASE_ID, ph.PURCHASE_STATUS, qH.ID AS QUOTE_HIST_ID, qH.PROGRESS AS QUOTE_PROGRESS, 
                      PQ_STAT.STATUS AS PQ_STATUS, q.ID AS QUOTE_ID
FROM         dbo.A_PURCHASE_QUOTE_STATUS PQ_STAT INNER JOIN
                      dbo.A_PURCHASES_HISTORY ph ON PQ_STAT.PURCHASE_ID = ph.ID INNER JOIN
                      dbo.A_QUOTES_HISTORY qH ON PQ_STAT.QUOTE_ID = qH.ID INNER JOIN
                      dbo.A_QUOTES q ON qH.ID = q.HISTORY_REF_ID LEFT OUTER JOIN
                      dbo.A_PURCHASES p ON ph.ID = p.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_PURCHASE_ITEM_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PURCHASE_ITEM_DATA]
AS
SELECT     dbo.A_ORDER_ITEMS.ID, dbo.A_ORDER_ITEMS.ORDER_ID, dbo.A_ORDER_ITEMS.PRODUCT_ID, dbo.A_ORDER_ITEMS.QTY, 
                      dbo.A_ORDER_ITEMS.RECURRING, dbo.A_ORDER_ITEMS.RECUR_PERIOD, dbo.A_ORDER_ITEMS.RECUR_COUNT, 
                      dbo.A_ORDER_ITEMS.RECUR_START_DATE, dbo.A_ORDER_ITEMS.RECUR_STOP_DATE, dbo.A_ORDER_ITEMS.RECUR_ACCOUNT, 
                      dbo.A_ORDER_ITEMS.RECUR_AUTO_FILL, dbo.A_ORDER_ITEMS.SPECIAL_DISCOUNT, dbo.A_ORDER_ITEMS.SPECIAL_DISC_REASON, 
                      dbo.A_ORDER_ITEMS.UNIT_PRICE, dbo.A_ORDER_ITEMS.UNIT_ESTIMATE, dbo.A_ORDER_ITEMS.EXPEDITE_PRODUCTION, 
                      dbo.A_ORDER_ITEMS.EXPEDITE_REASON, dbo.A_ORDER_ITEMS.COMMENTS, dbo.A_ORDER_ITEMS.DRCM, dbo.A_ORDER_ITEMS.MODBY, 
                      dbo.A_ORDER_ITEMS.PARENT, dbo.A_ORDER_ITEMS.PROD_PRICE_LIST, dbo.A_ORDER_ITEMS.QUOTE_ID, dbo.A_ORDER_ITEMS.TOTAL_QTY, 
                      dbo.A_ORDER_ITEMS.TOTAL_PRICE, dbo.A_ORDER_ITEMS.PARENT_QTY, dbo.A_ORDER_ITEMS.PROC_SYS_ID, dbo.A_ORDER_ITEMS.DEST, 
                      dbo.A_ORDER_ITEMS.FROM_LOC, dbo.A_ORDER_ITEMS.TO_LOC, dbo.A_ORDER_ITEMS.ADD_COST_ID, dbo.A_ORDER_ITEMS.FLAT_RATE, 
                      dbo.A_ORDER_ITEMS.EX_DESC, dbo.A_ORDER_ITEMS.EST_WEIGHT, dbo.A_ORDER_ITEMS.EST_WEIGHT_UNIT, 
                      dbo.A_ORDER_ITEMS.PPL_HIST_ID, dbo.A_ORDER_ITEMS.SOURCE_ID, dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID, 
                      dbo.A_ORDER_ITEMS.ACCOUNT_ID, dbo.A_ORDER_ITEMS.BILL_TYPE, dbo.A_ORDER_ITEMS.QTY_FILLED, 
                      dbo.A_ORDER_ITEMS.QTY_NEEDS_FILLING, dbo.A_ORDER_ITEMS_PURCHASE_DATA.PRODUCT_HIST_ID, 
                      dbo.A_ORDER_ITEMS_PURCHASE_DATA.PROCEDURE_HIST_ID, dbo.A_ORDER_ITEMS.DUE_DATE, dbo.A_ORDER_ITEMS.ORIG_DUE_DATE, 
                      dbo.A_ORDER_ITEMS.ACT_DUE_DATE
FROM         dbo.A_ORDER_ITEMS INNER JOIN
                      dbo.A_ORDER_ITEMS_PURCHASE_DATA ON dbo.A_ORDER_ITEMS.ID = dbo.A_ORDER_ITEMS_PURCHASE_DATA.ORDER_ITEM_ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_NEEDS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_NEEDS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_NEEDS_HISTORY.ID AS ITEM_ID, dbo.A_NEEDS_HISTORY.DESCRIPTION
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_NEEDS_HISTORY ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_NEEDS_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_APPROVED_PEOPLE_SIMPLE_SEARCH]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_APPROVED_PEOPLE_SIMPLE_SEARCH]
AS
SELECT     dbo.A_PEOPLE.ID, dbo.A_PEOPLE_SEARCH_TABLE.NAME, dbo.A_PEOPLE_SEARCH_TABLE.LAST_NAME, 
                      dbo.A_PEOPLE_SEARCH_TABLE.MIDDLE_NAME, dbo.A_PEOPLE_SEARCH_TABLE.FULL_NAME, dbo.A_PEOPLE_SEARCH_TABLE.TIME_ZONE, 
                      dbo.A_PEOPLE_SEARCH_TABLE.SYSTEM_STATUS, dbo.A_PEOPLE_SEARCH_TABLE.BOSS_ID, dbo.A_PEOPLE_SEARCH_TABLE.BOSS_NAME, 
                      dbo.A_PEOPLE_SEARCH_TABLE.HIRE_DATE, dbo.A_PEOPLE_SEARCH_TABLE.COMPANY_ID, dbo.A_PEOPLE_SEARCH_TABLE.COMPANY_NAME, 
                      dbo.A_PEOPLE_SEARCH_TABLE.POSITION_ID, dbo.A_PEOPLE_SEARCH_TABLE.POSITION_NAME, dbo.A_PEOPLE_SEARCH_TABLE.LOCATION_ID, 
                      dbo.A_PEOPLE_SEARCH_TABLE.LOCATION_NAME, dbo.A_PEOPLE_SEARCH_TABLE.PRIMARY_PHONE_ID, 
                      dbo.A_PEOPLE_SEARCH_TABLE.PRIMARY_PHONE_NUMBER, dbo.A_PEOPLE_SEARCH_TABLE.SECONDARY_PHONE_ID, 
                      dbo.A_PEOPLE_SEARCH_TABLE.SECONDARY_PHONE_NUMBER, dbo.A_PEOPLE_SEARCH_TABLE.WORK_EMAIL_ID, 
                      dbo.A_PEOPLE_SEARCH_TABLE.WORK_EMAIL_ADDRESS
FROM         dbo.A_PEOPLE_SEARCH_TABLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE_SEARCH_TABLE.ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_PEOPLE ON dbo.A_PEOPLE_HISTORY.ID = dbo.A_PEOPLE.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_TASK_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_TASK_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, dbo.A_TASKS.ID AS ITEM_ID, 
                      dbo.A_TASKS.DESCRIPTION AS NAME
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_TASKS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_TASKS.ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_SURVEYS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_SURVEYS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_SURVEYS.ID AS ITEM_ID, 
                      dbo.A_SURVEYS.SUBJECT AS NAME, dbo.A_PEOPLES_FAVORITES.NUM
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_SURVEYS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_SURVEYS.ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_SERVICE_CALLS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_SERVICE_CALLS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID AS ITEM_ID, dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.MACHINE_NAME AS NAME
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID
GO

/****** Object:  View [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COST_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_PROD_PRICE_LIST_EXTRA_COST_DATA]
AS
SELECT     dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCT, dbo.A_PROD_PRICE_LIST_HISTORY.CUSTOMER, 
                      dbo.A_PROD_PRICE_LIST_HISTORY.PRODUCT_NAME, dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.PROD_PRICE_LIST, 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.DESCRIPTION, dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.PRICE_LIST_TYPE, 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.ID AS EXTRA_COST_ID, dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.DRCM, 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.MODBY, dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.UNIT, 
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.UNIT_PRICE
FROM         dbo.A_PROD_PRICE_LIST_HISTORY INNER JOIN
                      dbo.A_PROD_PRICE_LIST_EXTRA_COSTS ON dbo.A_PROD_PRICE_LIST_HISTORY.ID = dbo.A_PROD_PRICE_LIST_EXTRA_COSTS.PROD_PRICE_LIST
GO

/****** Object:  View [dbo].[A_V_PEOPLE_WITH_EMAILS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PEOPLE_WITH_EMAILS]
AS
SELECT     dbo.A_EMAILS.ADDY, dbo.A_EMAILS.TYPE, dbo.A_PEOPLE_HISTORY.FULL_NAME, dbo.A_PEOPLE.ID
FROM         dbo.A_PEOPLE INNER JOIN
                      dbo.A_PEOPLE_HISTORY ON dbo.A_PEOPLE.HISTORY_REF_ID = dbo.A_PEOPLE_HISTORY.ID INNER JOIN
                      dbo.A_EMAILS ON dbo.A_PEOPLE_HISTORY.OBJECT_ID = dbo.A_EMAILS.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_HEADER_REFERENCE_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROCEDURE_HEADER_REFERENCE_FILES]
AS
SELECT     dbo.A_PROCEDURES_HISTORY.ID AS PROC_ID, dbo.A_DOCUMENTS.*, dbo.A_PROCEDURES_HISTORY.OBJECT_ID AS P_OBJ_ID
FROM         dbo.A_DOCUMENT_LINK INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_DOCUMENT_LINK.LINKED_DOC_ID = dbo.A_DOCUMENTS.ID INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_DOCUMENT_LINK.OBJECT_ID = dbo.A_PROCEDURES_HISTORY.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_ORDERS_WITH_QUOTE_STATUS_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_ORDERS_WITH_QUOTE_STATUS_INFO]
AS
SELECT     dbo.A_ORDERS.ID AS ORDER_ID, dbo.A_ORDERS.HISTORY_REF_ID AS ORDER_HIST_ID, dbo.A_QUOTES.ID AS QUOTE_ID, 
                      dbo.A_QUOTES_HISTORY.ID AS QUOTE_HIST_ID, dbo.A_QUOTES_HISTORY.PROGRESS, dbo.A_QUOTES_HISTORY.EXPIRATION_DATE, 
                      dbo.A_ORDERS_HISTORY.RFQ_ID, dbo.A_ORDER_ITEMS.PROD_PRICE_LIST, dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID
FROM         dbo.A_ORDERS_HISTORY INNER JOIN
                      dbo.A_ORDERS ON dbo.A_ORDERS_HISTORY.ID = dbo.A_ORDERS.HISTORY_REF_ID INNER JOIN
                      dbo.A_QUOTES INNER JOIN
                      dbo.A_QUOTES_HISTORY ON dbo.A_QUOTES.HISTORY_REF_ID = dbo.A_QUOTES_HISTORY.ID INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_QUOTES_HISTORY.ID = dbo.A_QUOTE_ORDER_LINK.QUOTE_ID ON 
                      dbo.A_ORDERS.ID = dbo.A_QUOTE_ORDER_LINK.ORDER_ID INNER JOIN
                      dbo.A_ORDER_ITEMS ON dbo.A_QUOTES_HISTORY.ID = dbo.A_ORDER_ITEMS.QUOTE_ID
WHERE     (dbo.A_ORDER_ITEMS.PURCHASE_HIST_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_MENUS_WITH_ROLES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_MENUS_WITH_ROLES]
AS
SELECT     dbo.A_MENUS.ID, dbo.A_MENUS.URL, dbo.A_MENUS.DRCM, dbo.A_MENUS.MODBY, dbo.A_MENUS.NUM, dbo.A_MENUS.NAME, dbo.A_MENUS.INFO, 
                      dbo.A_MENUS.MENU_GROUP, dbo.A_MENU_ROLES.ROLE_ID
FROM         dbo.A_MENUS LEFT OUTER JOIN
                      dbo.A_MENU_ROLES ON dbo.A_MENUS.ID = dbo.A_MENU_ROLES.MENU_ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MESSAGES_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_MESSAGES_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_MESSAGES.ID AS ITEM_ID, dbo.A_MESSAGES.MESSAGE
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_MESSAGES ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_MESSAGES.ID
GO

/****** Object:  View [dbo].[A_V_LOCATIONS_DROP_DOWN_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_LOCATIONS_DROP_DOWN_DATA]
AS
SELECT     o.STATUS, o.REV, o.CREATING_CO_NAME, o.CREATING_CO, dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, p.NAME, 
                      p.COMPLETE_NAME
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_LOCATIONS_HISTORY p ON o.ID = p.OBJECT_ID INNER JOIN
                      dbo.A_LOCATIONS ON p.ID = dbo.A_LOCATIONS.HISTORY_REF_ID
WHERE     (o.STATUS LIKE 'APPROVED%')
GO

/****** Object:  View [dbo].[A_V_PART_TYPES_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PART_TYPES_APPROVED_DATA]
AS
SELECT     dbo.A_PART_TYPES.ID, dbo.A_PART_TYPES.HISTORY_REF_ID, dbo.A_PART_TYPES_HISTORY.NAME, dbo.A_PART_TYPES_HISTORY.SPARE, 
                      dbo.A_PART_TYPES_HISTORY.CONSUMABLE, dbo.A_PART_TYPES_HISTORY.DRCM, dbo.A_PART_TYPES_HISTORY.MODBY, 
                      dbo.A_PART_TYPES_HISTORY.OBJECT_ID, dbo.A_PART_TYPES_HISTORY.UNIT, dbo.A_PART_TYPES_HISTORY.UNIT_SHIPPING_WEIGHT
FROM         dbo.A_PART_TYPES INNER JOIN
                      dbo.A_PART_TYPES_HISTORY ON dbo.A_PART_TYPES.HISTORY_REF_ID = dbo.A_PART_TYPES_HISTORY.ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_ITEM_PARENTS_USING_ORDER_ITEM_FOR_RELATIONSHIP]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_QUOTE_ITEM_PARENTS_USING_ORDER_ITEM_FOR_RELATIONSHIP]
AS
SELECT     A_ORDER_ITEMS_1.ID AS PARENT_ID, A_ORDER_ITEMS_2.ID AS CHILD_ID, A_ORDER_ITEMS_2.QUOTE_ID
FROM         dbo.A_ORDER_ITEMS O_PARENT INNER JOIN
                      dbo.A_ORDER_ITEMS O_CHILD ON O_PARENT.ID = O_CHILD.PARENT INNER JOIN
                      dbo.A_ORDER_ITEMS A_ORDER_ITEMS_1 ON O_PARENT.ID = A_ORDER_ITEMS_1.SOURCE_ID INNER JOIN
                      dbo.A_ORDER_ITEMS A_ORDER_ITEMS_2 ON O_CHILD.ID = A_ORDER_ITEMS_2.SOURCE_ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_MONITORS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_PROCEDURE_MONITORS]
AS
SELECT     dbo.A_MONITOR_TEMPLATES.*
FROM         dbo.A_MONITOR_TEMPLATES
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MENU_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_Z_FAVORITES_MENU_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITES.[GROUP], dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.FAV_TYPE, dbo.A_MENUS.NAME, dbo.A_PEOPLES_FAVORITES.NUM
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_MENUS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_MENUS.ID RIGHT OUTER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID
GO

/****** Object:  View [dbo].[A_V_FILLS_WITH_PRECEDENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_FILLS_WITH_PRECEDENTS]
AS
SELECT     PREV_FILL.ID AS PREV_ID, FOL_FILL.ID AS FOL_ID, PREV_FILL.TASK_ID AS PREV_TASK_ID, FOL_FILL.TASK_ID AS FOL_TASK_ID, 
                      FOL_PURCH_ITEM.DEST AS FOL_DEST, PREV_PURCH_ITEM.DEST AS PREV_DEST, FOL_PURCH_ITEM.PURCHASE_HIST_ID
FROM         dbo.A_FILLS FOL_FILL INNER JOIN
                      dbo.A_ORDER_ITEMS FOL_PURCH_ITEM ON FOL_FILL.PURCH_ITEM_ID = FOL_PURCH_ITEM.ID LEFT OUTER JOIN
                      dbo.A_ORDER_ITEMS PREV_PURCH_ITEM INNER JOIN
                      dbo.A_ORDER_ITEM_PRECEDENTS Precedents ON PREV_PURCH_ITEM.ID = Precedents.PREV INNER JOIN
                      dbo.A_FILLS PREV_FILL ON PREV_PURCH_ITEM.ID = PREV_FILL.PURCH_ITEM_ID ON FOL_PURCH_ITEM.ID = Precedents.FOL
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_MEETINGS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_Z_FAVORITES_MEETINGS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_MEETINGS.MEETING_NAME AS NAME, dbo.A_MEETINGS.ID AS ITEM_ID
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_MEETINGS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_MEETINGS.ID
GO

/****** Object:  View [dbo].[A_V_FILL_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_FILL_TASKS]
AS
SELECT     dbo.A_TASKS.ID AS TASK_ID, dbo.A_FILLS.PURCH_ITEM_ID, dbo.A_TASKS.REQUESTEE_ID
FROM         dbo.A_FILLS INNER JOIN
                      dbo.A_TASKS ON dbo.A_FILLS.TASK_ID = dbo.A_TASKS.ID
GO

/****** Object:  View [dbo].[A_V_Z_FAVORITES_DISCUSSIONS_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_Z_FAVORITES_DISCUSSIONS_ITEMS]
AS
SELECT     dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS GROUP_ID, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON, dbo.A_PEOPLES_FAVORITES.ID, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_DISCUSSIONS.ID AS ITEM_ID, dbo.A_DISCUSSIONS.SUBJECT AS NAME
FROM         dbo.A_PEOPLES_FAVORITES INNER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID INNER JOIN
                      dbo.A_DISCUSSIONS ON dbo.A_PEOPLES_FAVORITES.ITEM = dbo.A_DISCUSSIONS.ID
GO

/****** Object:  View [dbo].[A_V_SURVEY_GET_QUESTIONS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SURVEY_GET_QUESTIONS]
AS
SELECT     dbo.A_SURVEYS.ID AS S_ID, dbo.A_SURVEY_REPLIES.ID AS R_ID, dbo.A_SURVEY_REPLIES.TEXT, dbo.A_SURVEY_REPLIES.NUM, 
                      dbo.A_SURVEY_REPLIES.ROOT, dbo.A_SURVEY_REPLIES.COLOR, dbo.A_SURVEY_REPLIES.PARENT, dbo.A_SURVEY_REPLIES.AUTHOR, 
                      dbo.A_SURVEY_REPLIES.MODBY, dbo.A_SURVEY_ATTACHMENTS.DOC_ID, dbo.A_SURVEY_REPLIES.DRCM
FROM         dbo.A_SURVEYS INNER JOIN
                      dbo.A_SURVEY_REPLIES ON dbo.A_SURVEYS.ID = dbo.A_SURVEY_REPLIES.ROOT LEFT OUTER JOIN
                      dbo.A_SURVEY_ATTACHMENTS ON dbo.A_SURVEY_REPLIES.ID = dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID
WHERE     (dbo.A_SURVEY_REPLIES.PARENT IS NULL)
GO

/****** Object:  View [dbo].[A_V_FAVORITES_COMPLETE]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_FAVORITES_COMPLETE]
AS
SELECT     g.FAV_TYPE, g.GROUP_NAME, g.PERSON, f.ITEM, f.NUM, g.ID
FROM         dbo.A_PEOPLES_FAVORITES f RIGHT OUTER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS g ON f.[GROUP] = g.ID
GO

/****** Object:  View [dbo].[A_V_PROCEDURE_OBJECT_LINK_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_PROCEDURE_OBJECT_LINK_DATA]
AS
SELECT     dbo.A_APPROVED_OBJECTS.OBJ_REF_ID, dbo.A_OBJECTS.OBJ_TABLE, dbo.A_OBJECTS.OBJ_ID, 
                      dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID, dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID, dbo.A_OBJECTS.OBJ_DESC, 
                      dbo.A_PROCEDURE_OBJECT_LINK.STEP_ID, dbo.A_PROCEDURE_OBJECT_LINK.QTY, dbo.A_PROCEDURE_OBJECT_LINK.QTY_TYPE, 
                      dbo.A_PROCEDURE_OBJECT_LINK.RELATIONSHIP, dbo.A_PROCEDURE_OBJECT_LINK.ID, dbo.A_PROCEDURE_OBJECT_LINK.LABOR_ROLE, 
                      dbo.A_PROCEDURES.ID AS APPROVED_PROC_ID, dbo.A_PROCEDURES_HISTORY.ID AS PROC_HIST_ID
FROM         dbo.A_APPROVED_OBJECTS INNER JOIN
                      dbo.A_PROCEDURE_OBJECT_LINK ON dbo.A_APPROVED_OBJECTS.ID = dbo.A_PROCEDURE_OBJECT_LINK.APPROVED_OBJECT_ID INNER JOIN
                      dbo.A_OBJECTS ON dbo.A_APPROVED_OBJECTS.OBJ_REF_ID = dbo.A_OBJECTS.ID LEFT OUTER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURE_OBJECT_LINK.PROCEDURE_ID = dbo.A_PROCEDURES_HISTORY.ID LEFT OUTER JOIN
                      dbo.A_PROCEDURES ON dbo.A_PROCEDURES_HISTORY.ID = dbo.A_PROCEDURES.HISTORY_REF_ID
GO

/****** Object:  View [dbo].[A_V_COMPANY_CONTACTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_V_COMPANY_CONTACTS]
AS
SELECT     co.NAME AS CO_NAME, cc.FIRST_NAME, cc.LAST_NAME, cc.PHONE, cc.COMPANY_ID, cc.CREATING_CO, cc.FULL_NAME, cc.ID, 
                      ISNULL('(' + co.NAME + ') ', '(No Co) ') + ISNULL(cc.FULL_NAME, 'No Name ') + ISNULL(' [' + cc.PHONE + ']', '') AS DISPLAY_DATA
FROM         dbo.A_COMPANY_CONTACTS cc INNER JOIN
                      dbo.A_COMPANIES co ON cc.COMPANY_ID = co.ID
GO

/****** Object:  View [dbo].[A_V_ACTUAL_PARTS_WITH_RECENT_TASKS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE VIEW [dbo].[A_V_ACTUAL_PARTS_WITH_RECENT_TASKS]
AS
SELECT DISTINCT 
                      dbo.A_TASKS.PROCEDURE_ID, dbo.A_TASKS.ORIG_REQUESTOR_ID, dbo.A_TASKS.LAST_REQUEST_DATE, dbo.A_TASKS.GROUP_REQUESTEE_ID, 
                      dbo.A_TASKS.REQUESTEE_ID, dbo.A_ACTUAL_PARTS.ID AS ACTUAL_PART_ID
FROM         dbo.A_TASK_OBJECT_LINK INNER JOIN
                      dbo.A_TASKS ON dbo.A_TASK_OBJECT_LINK.TASK_ID = dbo.A_TASKS.ID INNER JOIN
                      dbo.A_ACTUAL_PARTS ON dbo.A_TASK_OBJECT_LINK.OBJECT_ID = dbo.A_ACTUAL_PARTS.ID
WHERE     (dbo.A_TASKS.LAST_REQUEST_DATE > DATEADD(n, - 20, GETDATE()))
GO

/****** Object:  View [dbo].[A_ORDERS_WITH_QUOTE_STATUS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_ORDERS_WITH_QUOTE_STATUS]
AS
SELECT     dbo.A_ORDERS.ID AS ORDER_ID, dbo.A_ORDERS.HISTORY_REF_ID AS ORDER_HIST_ID, dbo.A_QUOTES.ID AS QUOTE_ID, 
                      dbo.A_QUOTES_HISTORY.ID AS QUOTE_HIST_ID, dbo.A_QUOTES_HISTORY.PROGRESS, dbo.A_QUOTES_HISTORY.EXPIRATION_DATE
FROM         dbo.A_ORDERS_HISTORY INNER JOIN
                      dbo.A_ORDERS ON dbo.A_ORDERS_HISTORY.ID = dbo.A_ORDERS.HISTORY_REF_ID INNER JOIN
                      dbo.A_QUOTES INNER JOIN
                      dbo.A_QUOTES_HISTORY ON dbo.A_QUOTES.HISTORY_REF_ID = dbo.A_QUOTES_HISTORY.ID INNER JOIN
                      dbo.A_QUOTE_ORDER_LINK ON dbo.A_QUOTES_HISTORY.ID = dbo.A_QUOTE_ORDER_LINK.QUOTE_ID ON 
                      dbo.A_ORDERS.ID = dbo.A_QUOTE_ORDER_LINK.ORDER_ID
GO

/****** Object:  View [dbo].[A_LOCATIONS_APPROVED_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_LOCATIONS_APPROVED_DATA]
AS
SELECT     dbo.A_LOCATIONS.ID, dbo.A_LOCATIONS.HISTORY_REF_ID, dbo.A_LOCATIONS_HISTORY.NAME, dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_LOCATION_NAME, dbo.A_LOCATIONS_HISTORY.ADDRESS_1, dbo.A_LOCATIONS_HISTORY.ADDRESS_2, 
                      dbo.A_LOCATIONS_HISTORY.FULL_ADDRESS, dbo.A_LOCATIONS_HISTORY.CITY, dbo.A_LOCATIONS_HISTORY.STATE, 
                      dbo.A_LOCATIONS_HISTORY.COUNTRY, dbo.A_LOCATIONS_HISTORY.POSTAL_CODE, dbo.A_LOCATIONS_HISTORY.REGION, 
                      dbo.A_LOCATIONS_HISTORY.REGION_NAME, dbo.A_LOCATIONS_HISTORY.INTERNAL_ADDRESS, dbo.A_LOCATIONS_HISTORY.OBJECT_ID, 
                      dbo.A_LOCATIONS_HISTORY.PARENT_PATH, dbo.A_LOCATIONS.STATUS
FROM         dbo.A_LOCATIONS INNER JOIN
                      dbo.A_LOCATIONS_HISTORY ON dbo.A_LOCATIONS.HISTORY_REF_ID = dbo.A_LOCATIONS_HISTORY.ID
WHERE     (dbo.A_LOCATIONS.STATUS = 'APPROVED')
GO

/****** Object:  View [dbo].[A_O_COUNTERS_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_COUNTERS_HISTORY]
AS
SELECT     c.*, o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, o.CREATE_DATE AS CREATE_DATE, 
                      o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, o.REV AS REV, o.WFS_ID AS WFS_ID, 
                      o.LOCKED_BY_NAME AS LOCKED_BY_NAME, o.CREATING_CO_NAME AS CREATING_CO_NAME, o.APPROVAL_ACTIVITY AS APPROVAL_ACTIVITY, 
                      o.ID AS OBJ_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_COUNTERS_HISTORY c ON o.ID = c.OBJECT_ID
GO

/****** Object:  View [dbo].[A_O_DOCUMENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_O_DOCUMENTS]
AS
SELECT     o.LOCKED_BY AS LOCKED_BY, o.UNLOCKED_BY AS UNLOCKED_BY, o.CREATED_BY AS CREATED_BY, o.CREATE_DATE AS CREATE_DATE, 
                      o.ROOT AS ROOT, o.REV_INFO AS REV_INFO, o.CREATING_CO AS CREATING_CO, o.STATUS AS STATUS, o.REV AS REV, o.WFS_ID AS WFS_ID, d.ID, 
                      d.DOC_ID, d.NAME, d.SOURCE_ID, d.DRCM, d.MODBY, d.DELETED, d.DOC_TYPE, d.SERVER_PATH, d.APPROVED, d.DESCRIPTION, d.ACTIVE, 
                      d.CONTENTTYPE, d.OBJECT_ID, d.CREATOR_ID
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_DOCUMENTS d ON o.ID = d.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_PROJECT_ITEMS_AND_NAMES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PROJECT_ITEMS_AND_NAMES]
AS
SELECT     pil.PROJECT_ID, pil.ITEM_TYPE, pil.ITEM_ID, ISNULL(s.SUBJECT, N'') + ISNULL(m.MEETING_NAME, N'') + ISNULL(p.NAME, N'') + ISNULL(d.SUBJECT, 
                      N'') + ISNULL(t.DESCRIPTION, N'') + ISNULL(mes.MESSAGE, N'') AS ITEM_NAME, ISNULL(s.STATUS, '') + ISNULL(m.STATUS, '') + ISNULL(p.STATUS, '') 
                      + ISNULL(d.STATUS, '') + ISNULL(t.STATUS, '') AS ITEM_STATUS
FROM         dbo.A_PROJECT_ITEM_LINK pil LEFT OUTER JOIN
                      dbo.A_TASKS t ON pil.ITEM_ID = t.ID LEFT OUTER JOIN
                      dbo.A_DISCUSSIONS d ON pil.ITEM_ID = d.ID LEFT OUTER JOIN
                      dbo.A_MEETINGS m ON pil.ITEM_ID = m.ID LEFT OUTER JOIN
                      dbo.A_PROJECTS p ON pil.ITEM_ID = p.ID LEFT OUTER JOIN
                      dbo.A_SURVEYS s ON pil.ITEM_ID = s.ID LEFT OUTER JOIN
                      dbo.A_MESSAGES mes ON pil.ITEM_ID = mes.ID
GO

/****** Object:  View [dbo].[A_O_PREPOP_HISTORY]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[A_O_PREPOP_HISTORY]
AS
SELECT     p.ID, p.OBJECT_ID, o.LOCKED_BY, o.UNLOCKED_BY, o.CREATED_BY, o.CREATE_DATE, o.ROOT, o.REV_INFO, o.CREATING_CO, o.STATUS, o.REV, 
                      o.WFS_ID, o.LOCKED_BY_NAME, o.CREATING_CO_NAME, o.APPROVAL_ACTIVITY, o.ID AS OBJ_ID, p.PROC_STEP_ID, 
                      dbo.A_PROCEDURE_STEPS.STEP_TEXT
FROM         dbo.A_OBJECTS o INNER JOIN
                      dbo.A_PREPOP_HISTORY p ON o.ID = p.OBJECT_ID INNER JOIN
                      dbo.A_PROCEDURE_STEPS ON p.PROC_STEP_ID = dbo.A_PROCEDURE_STEPS.ID
GO

/****** Object:  View [dbo].[A_V_SERVICE_CALLS_ATTACHMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_SERVICE_CALLS_ATTACHMENT_DATA]
AS
SELECT     dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID, dbo.A_SERVICE_CALLS_ATTACHMENTS.DOC_ID AS [VALUE], dbo.A_DOCUMENTS.NAME AS SHOW, 
                      dbo.A_DOCUMENTS.DESCRIPTION
FROM         dbo.A_SERVICE_CALLS_ATTACHMENTS INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_SERVICE_CALLS_ATTACHMENTS.DOC_ID = dbo.A_DOCUMENTS.ID INNER JOIN
                      dbo.A_SERVICE_CALLS_WEEKLY_REPORTS ON 
                      dbo.A_SERVICE_CALLS_ATTACHMENTS.WEEKLY_ID = dbo.A_SERVICE_CALLS_WEEKLY_REPORTS.ID
GO

/****** Object:  View [dbo].[A_V_APPROVED_PROCEDURE_STEPS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_APPROVED_PROCEDURE_STEPS]
AS
SELECT     dbo.A_PROCEDURES.ID AS APPROVED_PROC_ID, dbo.A_PROCEDURES.HISTORY_REF_ID AS PROC_HIST_ID, 
                      dbo.A_PROCEDURES_HISTORY.NAME AS PROC_NAME, dbo.A_PROCEDURE_STEPS.ID AS STEP_ID, dbo.A_PROCEDURE_STEPS.STEP_TEXT
FROM         dbo.A_PROCEDURES INNER JOIN
                      dbo.A_PROCEDURES_HISTORY ON dbo.A_PROCEDURES.HISTORY_REF_ID = dbo.A_PROCEDURES_HISTORY.ID INNER JOIN
                      dbo.A_PROCEDURE_STEPS ON dbo.A_PROCEDURES_HISTORY.ID = dbo.A_PROCEDURE_STEPS.PROCEDURE_ID
GO

/****** Object:  View [dbo].[A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_PARTS_WITH_RELATED_EXTERNAL_CO_INFO]
AS
SELECT     dbo.A_OBJECTS.CREATING_CO AS EXTERNAL_CO, A_OBJECTS_1.CREATING_CO AS LOCAL_CO, A_OBJECTS_1.ROOT AS LOCAL_PART_ID, 
                      dbo.A_PARTS.ID AS EXTERNAL_PART_ID, dbo.A_PARTS_HISTORY.ID AS LOCAL_PH_ID, 
                      dbo.A_PARTS.PARTS_HISTORY_ID AS EXTERNAL_PH_ID
FROM         dbo.A_OBJECTS A_OBJECTS_1 INNER JOIN
                      dbo.A_PARTS_HISTORY ON A_OBJECTS_1.ID = dbo.A_PARTS_HISTORY.OBJECT_ID INNER JOIN
                      dbo.A_OBJECTS INNER JOIN
                      dbo.A_PARTS ON dbo.A_OBJECTS.ROOT = dbo.A_PARTS.ID INNER JOIN
                      dbo.A_PARTS_EXTERNAL_EQUALS ON dbo.A_PARTS.ID = dbo.A_PARTS_EXTERNAL_EQUALS.EQUAL_PART_ID ON 
                      dbo.A_PARTS_HISTORY.ID = dbo.A_PARTS_EXTERNAL_EQUALS.PART_ID
GO

/****** Object:  View [dbo].[A_V_DOCUMENTS_LINKED]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_DOCUMENTS_LINKED]
AS
SELECT     dbo.A_DOCUMENTS.DOC_ID, dbo.A_DOCUMENTS.NAME AS DOC_NAME, dbo.A_DOCUMENT_LINK.TYPE, dbo.A_DOCUMENT_LINK.OBJECT_ID
FROM         dbo.A_DOCUMENTS INNER JOIN
                      dbo.A_DOCUMENT_LINK ON dbo.A_DOCUMENTS.ID = dbo.A_DOCUMENT_LINK.LINKED_DOC_ID
GO

/****** Object:  View [dbo].[A_V_THEORY_PARAGRAPH_DOCUMENT_DATA]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_THEORY_PARAGRAPH_DOCUMENT_DATA]
AS
SELECT     dbo.A_THEORY_PARAGRAPH_FILE_LINK.PARAGRAPH_ID, dbo.A_DOCUMENTS.*
FROM         dbo.A_THEORY_PARAGRAPH_FILE_LINK INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_THEORY_PARAGRAPH_FILE_LINK.FILE_ID = dbo.A_DOCUMENTS.ID
GO

/****** Object:  View [dbo].[A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_SURVEY_REPLIES_GET_FILE_ATTACHMENT_INFO]
AS
SELECT     dbo.A_SURVEY_REPLIES.ID, dbo.A_SURVEY_ATTACHMENTS.DOC_ID AS VALUE, dbo.A_DOCUMENTS.NAME AS SHOW, 
                      dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID, dbo.A_DOCUMENTS.DESCRIPTION
FROM         dbo.A_SURVEY_ATTACHMENTS INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_SURVEY_ATTACHMENTS.DOC_ID = dbo.A_DOCUMENTS.ID INNER JOIN
                      dbo.A_SURVEY_REPLIES ON dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID = dbo.A_SURVEY_REPLIES.ID
GO

/****** Object:  View [dbo].[A_V_FAVORITE_MENU_ITEMS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_FAVORITE_MENU_ITEMS]
AS
SELECT     dbo.A_MENUS.ID, dbo.A_MENUS.URL, dbo.A_MENUS.NAME, dbo.A_PEOPLES_FAVORITES.NUM, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.ID AS FAV_GROUP, dbo.A_PEOPLES_FAVORITE_GROUPS.GROUP_NAME, 
                      dbo.A_PEOPLES_FAVORITE_GROUPS.PERSON
FROM         dbo.A_MENUS INNER JOIN
                      dbo.A_PEOPLES_FAVORITES ON dbo.A_MENUS.ID = dbo.A_PEOPLES_FAVORITES.ITEM RIGHT OUTER JOIN
                      dbo.A_PEOPLES_FAVORITE_GROUPS ON dbo.A_PEOPLES_FAVORITES.[GROUP] = dbo.A_PEOPLES_FAVORITE_GROUPS.ID
GO

/****** Object:  View [dbo].[A_V_SURVEY_GET_FILE_ATTACHMENTS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO





CREATE VIEW [dbo].[A_V_SURVEY_GET_FILE_ATTACHMENTS]
AS
SELECT     dbo.A_SURVEYS.ID, dbo.A_DOCUMENTS.NAME AS SHOW, dbo.A_SURVEY_ATTACHMENTS.DOC_ID AS VALUE, 
                      dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID, dbo.A_SURVEY_ATTACHMENTS.SURVEY_ID
FROM         dbo.A_SURVEYS INNER JOIN
                      dbo.A_SURVEY_ATTACHMENTS ON dbo.A_SURVEYS.ID = dbo.A_SURVEY_ATTACHMENTS.SURVEY_ID INNER JOIN
                      dbo.A_DOCUMENTS ON dbo.A_SURVEY_ATTACHMENTS.DOC_ID = dbo.A_DOCUMENTS.DOC_ID
WHERE     (dbo.A_SURVEY_ATTACHMENTS.RESPONSE_ID IS NULL)
GO

/****** Object:  View [dbo].[A_V_THEORY_HEADER_REFERENCE_FILES]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE VIEW [dbo].[A_V_THEORY_HEADER_REFERENCE_FILES]
AS
SELECT     h.ID AS PROC_ID, d.*, h.OBJECT_ID AS P_OBJ_ID
FROM         dbo.A_DOCUMENT_LINK dl INNER JOIN
                      dbo.A_DOCUMENTS d ON dl.LINKED_DOC_ID = d.ID INNER JOIN
                      dbo.A_THEORY_HISTORY h ON dl.OBJECT_ID = h.OBJECT_ID
GO

/****** Object:  View [dbo].[A_V_WF_GROUP_SPECIAL_MEMBERS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO






CREATE VIEW [dbo].[A_V_WF_GROUP_SPECIAL_MEMBERS]
AS
SELECT     link.WF_GROUP_ID AS GROUP_ID, sm.NAME, link.SPECIAL_CODE AS SPECIAL_ID
FROM         dbo.A_WF_GROUP_SPECIALS_LINK link INNER JOIN
                      dbo.A_WF_GROUPS g ON link.WF_GROUP_ID = g.ID INNER JOIN
                      dbo.A_WF_GROUP_SPECIAL_MEMBERS sm ON link.SPECIAL_CODE = sm.ID
GO

/****** Object:  View [dbo].[A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS]    Script Date: 6/3/2019 1:33:41 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



CREATE VIEW [dbo].[A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS]
AS
SELECT     PREV.ID AS PREV_ID, PREV.QUOTE_ID, FOL.ID AS FOL_ID, PREV.PURCHASE_HIST_ID
FROM         dbo.A_ORDER_ITEMS FOL INNER JOIN
                      dbo.A_ORDER_ITEM_PRECEDENTS ON FOL.SOURCE_ID = dbo.A_ORDER_ITEM_PRECEDENTS.FOL INNER JOIN
                      dbo.A_ORDER_ITEMS PREV ON dbo.A_ORDER_ITEM_PRECEDENTS.PREV = PREV.SOURCE_ID
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "P"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 132
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PH"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 114
               Right = 406
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_V_ROLES_APPROVED_DATA_QUICK"
            Begin Extent = 
               Top = 114
               Left = 38
               Bottom = 222
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_TIME_ZONES"
            Begin Extent = 
               Top = 114
               Left = 242
               Bottom = 222
               Right = 393
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "CO"
            Begin Extent = 
               Top = 222
               Left = 38
               Bottom = 330
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "ROOT_CO"
            Begin Extent = 
               Top = 222
               Left = 242
               Bottom = 330
               Right = 408
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
    ' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane2', @value=N'     Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=2 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_APPROVED_PEOPLE'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "A_LOCATIONS_HISTORY"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 135
               Right = 274
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "A_LOCATIONS"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 267
               Right = 215
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
      Begin ColumnWidths = 19
         Width = 284
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
         Width = 1500
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_LOCATIONS_APPROVED_SITES_AND_ROOMS'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "c"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 114
               Right = 204
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "r"
            Begin Extent = 
               Top = 6
               Left = 242
               Bottom = 114
               Right = 408
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_COMPANIES_DROP_SEARCH'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'A_V_COMPANIES_DROP_SEARCH'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPane1', @value=N'[0E232FF0-B466-11cf-A24F-00AA00A3EFFF, 1.00]
Begin DesignProperties = 
   Begin PaneConfigurations = 
      Begin PaneConfiguration = 0
         NumPanes = 4
         Configuration = "(H (1[40] 4[20] 2[20] 3) )"
      End
      Begin PaneConfiguration = 1
         NumPanes = 3
         Configuration = "(H (1 [50] 4 [25] 3))"
      End
      Begin PaneConfiguration = 2
         NumPanes = 3
         Configuration = "(H (1 [50] 2 [25] 3))"
      End
      Begin PaneConfiguration = 3
         NumPanes = 3
         Configuration = "(H (4 [30] 2 [40] 3))"
      End
      Begin PaneConfiguration = 4
         NumPanes = 2
         Configuration = "(H (1 [56] 3))"
      End
      Begin PaneConfiguration = 5
         NumPanes = 2
         Configuration = "(H (2 [66] 3))"
      End
      Begin PaneConfiguration = 6
         NumPanes = 2
         Configuration = "(H (4 [50] 3))"
      End
      Begin PaneConfiguration = 7
         NumPanes = 1
         Configuration = "(V (3))"
      End
      Begin PaneConfiguration = 8
         NumPanes = 3
         Configuration = "(H (1[56] 4[18] 2) )"
      End
      Begin PaneConfiguration = 9
         NumPanes = 2
         Configuration = "(H (1 [75] 4))"
      End
      Begin PaneConfiguration = 10
         NumPanes = 2
         Configuration = "(H (1[66] 2) )"
      End
      Begin PaneConfiguration = 11
         NumPanes = 2
         Configuration = "(H (4 [60] 2))"
      End
      Begin PaneConfiguration = 12
         NumPanes = 1
         Configuration = "(H (1) )"
      End
      Begin PaneConfiguration = 13
         NumPanes = 1
         Configuration = "(V (4))"
      End
      Begin PaneConfiguration = 14
         NumPanes = 1
         Configuration = "(V (2))"
      End
      ActivePaneConfig = 0
   End
   Begin DiagramPane = 
      Begin Origin = 
         Top = 0
         Left = 0
      End
      Begin Tables = 
         Begin Table = "RA"
            Begin Extent = 
               Top = 6
               Left = 38
               Bottom = 136
               Right = 214
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "PST"
            Begin Extent = 
               Top = 138
               Left = 38
               Bottom = 268
               Right = 293
            End
            DisplayFlags = 280
            TopColumn = 0
         End
         Begin Table = "rv"
            Begin Extent = 
               Top = 6
               Left = 252
               Bottom = 136
               Right = 442
            End
            DisplayFlags = 280
            TopColumn = 0
         End
      End
   End
   Begin SQLPane = 
   End
   Begin DataPane = 
      Begin ParameterDefaults = ""
      End
   End
   Begin CriteriaPane = 
      Begin ColumnWidths = 11
         Column = 1440
         Alias = 900
         Table = 1170
         Output = 720
         Append = 1400
         NewValue = 1170
         SortType = 1350
         SortOrder = 1410
         GroupBy = 1350
         Filter = 1350
         Or = 1350
         Or = 1350
         Or = 1350
      End
   End
End
' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Portal_TrainingView'
GO

EXEC sys.sp_addextendedproperty @name=N'MS_DiagramPaneCount', @value=1 , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'VIEW',@level1name=N'Portal_TrainingView'
GO

