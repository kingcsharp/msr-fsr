
CREATE  procedure dbo.A_SP_ACTUAL_PART_MANUALLY_ADD_TO_TASK 
@AP_ID varchar(50),
@TASK_ID varchar(50),
@strNTLogin varchar(50)
as
declare @apHistID varchar(50)
SELECT @apHistID = ID FROM A_ACTUAL_PARTS_HISTORY WHERE OBJECT_ID = @AP_ID
print 'Adding a part manually to a task'
if exists(SELECT * FROM A_OBJECTS WHERE ID = @AP_ID AND STATUS <> 'APPROVED')
	begin
	UPDATE A_OBJECTS SET STATUS = 'APPROVED' WHERE ID = @AP_ID
	exec A_SP_ACTUAL_PARTS_FINISH_WF @apHistID,@AP_ID,@strNTLogin
	end

exec A_SP_ACTUAL_PART_ADD_TO_CALL @AP_ID,	@TASK_ID, NULL,	@strNTLogin


