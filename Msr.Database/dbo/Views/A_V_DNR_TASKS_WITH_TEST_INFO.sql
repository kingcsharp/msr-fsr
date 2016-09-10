CREATE VIEW dbo.A_V_DNR_TASKS_WITH_TEST_INFO
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
