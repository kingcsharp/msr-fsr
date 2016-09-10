


CREATE    PROCEDURE dbo.A_SP_TASK_FINISHED_CHECK_TO_SEE_IF_CAN_CLOSE
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @canClose tinyInt
set @canClose = 0
print 'Task finished checking to see if we can close it'
print 'First check to see if it is a task for Making a quote'
if exists (SELECT ID FROM A_QUOTE_ORDER_LINK WHERE TASK_ID = @taskID) set @canClose = 1
if exists (SELECT ID FROM A_QUOTE_ORDER_LINK WHERE QA_TASK_ID = @taskID) set @canClose = 1
if exists (SELECT TASK_ID FROM A_TASK_ORDER_INFORMATION WHERE TASK_ID = @taskID) set @canClose = 1





if @canClose = 1
	begin
	declare @REQ varchar(50)
	print 'We can close this task'
	SELECT @REQ = REQUESTOR FROM A_TASKS WHERE ID = @taskID
	exec A_SP_TASK_CLOSE null,null,@taskID,@REQ
	end





