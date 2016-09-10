


CREATE    PROCEDURE dbo.A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO
@taskID varchar(50),
@strNTLogin varchar(50)
AS
CREATE TABLE #tempProcsAlreadyProcessed(PROC_ID varchar(50),STEP_ID varchar(50))
print 'Creating monitors for a task based on procedure info'
print 'First to find out my procedure info'
declare @procID varchar(50),@stepID varchar(50)
SELECT @procID = PROCEDURE_ID,@stepID = PROCEDURE_STEP_ID FROM A_TASKS WHERE ID = @taskID
print 'The task ID = ' + isNull(@taskID,'NULL') + ' procedure ID = ' + isNull(@procID,'NULL') + ' and step ID = ' + isNull(@stepID,'NULL')
exec A_SP_TASK_CREATE_MONITORS_FROM_PROCEDURE_INFO_II @taskID,@procID,@stepID,0,@strNTLogin
DROP TABLE #tempProcsAlreadyProcessed



