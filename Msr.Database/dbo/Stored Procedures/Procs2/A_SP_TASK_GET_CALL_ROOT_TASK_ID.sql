


CREATE    PROCEDURE dbo.A_SP_TASK_GET_CALL_ROOT_TASK_ID 
@callRootTaskID varchar(50) OUTPUT,
@taskID varchar(50)
AS
print 'Finding the root Call Task ID for the task = ' + @taskID
declare @parentID varchar(50),@pRunner varchar(50),@sysTask varchar(50)
set @pRunner = @taskID
SELECT @parentID = PARENT_ID,@sysTask = SYSTEM_TASK  FROM A_TASKS WHERE ID = @taskID
while @parentID is not null and @sysTask <> 'SYS_PURCHASE'
	begin
	set @pRunner = @parentID
	set @parentID = null
	SELECT @parentID = PARENT_ID,@sysTask = SYSTEM_TASK  FROM A_TASKS WHERE ID = @pRunner
	end
set @callRootTaskid = @pRunner





