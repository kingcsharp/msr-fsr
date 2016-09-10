


CREATE VIEW dbo.A_V_SERVICE_CALL_WEEKLY_REPORT_DATA
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



