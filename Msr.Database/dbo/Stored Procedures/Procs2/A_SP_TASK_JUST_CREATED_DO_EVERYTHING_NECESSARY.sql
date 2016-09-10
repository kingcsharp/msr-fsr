


CREATE    PROCEDURE dbo.A_SP_TASK_JUST_CREATED_DO_EVERYTHING_NECESSARY
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @parentTaskID varchar(50), @pSysID varchar(50),@purchID varchar(50)
SELECT @parentTaskID = PARENT_ID FROM A_TASKS WHERE ID = @taskID
SELECT @pSysID = SYSTEM_TASK FROM A_TASKS WHERE ID = @parentTaskID
if @pSysID = 'SYS-PURCHASE'
	begin
	print 'This is a direct child of a purchase'
	exec A_SP_TASK_REQUEST_PURCHASE_TASK_IF_IT_IS_TIME @taskID
	end
declare @apObj varchar(50)
SELECT @apObj = OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
if @apObj is not null
	exec A_SP_ACTUAL_PART_ADD_TO_CALL @apObj,@taskID,'WorkingOn',@strNTLogin

