


CREATE VIEW dbo.A_V_SERVICE_CALL_VIEW_DATA
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