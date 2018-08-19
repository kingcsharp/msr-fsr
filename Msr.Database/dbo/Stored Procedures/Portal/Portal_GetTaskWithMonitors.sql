CREATE procedure [dbo].Portal_GetTaskWithMonitors

@fillID varchar(50)

AS

create TABLE #tempTasks (ID varchar(50))
INSERT INTO #tempTasks 
	SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION with (noLock) WHERE FILL_ITEM_ID = @fillID
	
while exists(SELECT * FROM 	A_TASKS WHERE PARENT_ID in (SELECT ID FROM #tempTasks) and ID not in (SELECT ID FROM #tempTasks))
begin
	INSERT INTO #tempTasks 
		SELECT ID FROM A_TASKS with (noLock) WHERE PARENT_ID IN 
		(SELECT ID FROM #tempTasks) and ID not in (SELECT ID FROM #tempTasks)
end


SELECT DISTINCT  [STEP_ID] AS TaskId,TITLE, 
DESCRIPTION
FROM A_V_TASKS_WITH_PROCEDURE_STEP_DATA  with (noLock) 
	WHERE STEP_ID IN (SELECT t1.ID FROM #tempTasks t1) AND HAS_MONITOR = 1


--exec A_SP_TASKS_FIND_FOR_PURCHASE_ITEM_AND_ACT_PART '366965','351792'

GO




