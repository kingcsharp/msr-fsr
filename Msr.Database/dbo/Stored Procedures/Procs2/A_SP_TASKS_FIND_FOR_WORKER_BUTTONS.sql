
CREATE  PROCEDURE [dbo].[A_SP_TASKS_FIND_FOR_WORKER_BUTTONS]
@fillID varchar(50),
@strNTLogin varchar(50)
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


SELECT distinct t.* FROM A_TASKS t INNER JOIN #tempTasks tt on t.ID = tt.ID

