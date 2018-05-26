CREATE VIEW dbo.Portal_ActualPartsViewHistory
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
