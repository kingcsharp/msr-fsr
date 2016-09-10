




CREATE PROCEDURE dbo.A_SP_TASK_MANUALLY_UPDATE_MINUTES
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@minID nvarchar(50),
@taskID varchar(50),
@ACTUAL_MINUTES varchar(50),
@PLANNED_MINUTES varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction
print 'Updating the minutes for task = ' + isNull(@taskID,'NULL')
if @taskID is null goto fin
if @minID is null
	begin
	print 'This is a new one'
	set @minID = newID()
	INSERT INTO A_TASK_MINUTES (ID,TASK_ID,ACTUAL_MINUTES,PLANNED_MINUTES,WORKER_ID)
	VALUES (@minID,@taskID,@ACTUAL_MINUTES,@PLANNED_MINUTES,@strNTLogin)
	end

UPDATE A_TASK_MINUTES SET
ACTUAL_MINUTES = @ACTUAL_MINUTES,
PLANNED_MINUTES = @PLANNED_MINUTES
WHERE ID = @minID


fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_WF_FINISH_WF with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_WF_FINISH_WF and we will terminate and not finish anything '
return 1









