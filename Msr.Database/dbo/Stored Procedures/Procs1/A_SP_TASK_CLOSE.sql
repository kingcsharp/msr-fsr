




CREATE    PROCEDURE A_SP_TASK_CLOSE
@RET_STATUS as varchar(50) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Closing a task'
print 'Verifying that I am the person who can Close it'
declare @origReq as varchar(50)
SELECT @origReq = ORIG_REQUESTOR_ID FROM A_TASKS WHERE ID = @ID
if @origReq = @strNTLogin
	begin
		UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @ID
		print 'Updated now continueing'
	end

fin: 





