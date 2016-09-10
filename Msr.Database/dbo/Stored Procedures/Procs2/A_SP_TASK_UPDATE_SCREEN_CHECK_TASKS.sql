
CREATE  PROCEDURE dbo.A_SP_TASK_UPDATE_SCREEN_CHECK_TASKS
@fillID varchar(50),
@strNTLogin varchar(50)
AS
create TABLE #tempTasks (ID varchar(50))
INSERT INTO #tempTasks 
	SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE FILL_ITEM_ID = @fillID
INSERT INTO #tempTasks 
	SELECT ID FROM A_TASKS WHERE PARENT_ID IN 
	(SELECT ID FROM #tempTasks)
declare @oneTask varchar(50)
SELECT @oneTask = ID FROM #tempTasks
exec A_SP_TASK_START_FOLLOWING_STEPS @oneTask,@strNTLogin

