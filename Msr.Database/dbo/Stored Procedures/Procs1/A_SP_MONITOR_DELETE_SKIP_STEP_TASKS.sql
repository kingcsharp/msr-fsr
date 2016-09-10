

CREATE   PROCEDURE dbo.A_SP_MONITOR_DELETE_SKIP_STEP_TASKS 
@origTaskID varchar(50),
@monID varchar(50),
@strNTLogin varchar(50)
AS
print 'Skipping Steps'
declare @recurNum int,@parentID varchar(50)
SELECT @parentID = PARENT_ID, @recurNum = RECURSION_NUMBER FROM A_TASKS WHERE ID = @origTaskID
declare @curs cursor,@stepID varchar(50),@taskID varchar(50),@origR varchar(50)
print 'SELECT STEP_ID FROM A_MONITOR_SKIP_STEPS WHERE MONITOR_ID = ''' + @monID + ''''
set @curs = CURSOR FOR SELECT STEP_ID FROM A_MONITOR_SKIP_STEPS WHERE MONITOR_ID = @monID
open @curs
fetch next from @curs into @stepID
while @@fetch_status = 0
	begin
	print 'skipping step ' + @stepID
	SELECT @taskID = ID,@origR = ORIG_REQUESTOR_ID FROM A_TASKS WHERE PROCEDURE_STEP_ID = @stepID AND RECURSION_NUMBER = @recurNum and PARENT_ID = @parentID and (STATUS not in ('FINISHED','CLOSED','COMPLETED'))
	print 'Deleting TASK = ' + @taskID
	exec A_SP_TASK_DELETE_TASK_FOREVER @taskID,@origR
	fetch next from @curs into @stepID
	end
close @curs
deallocate @curs


