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