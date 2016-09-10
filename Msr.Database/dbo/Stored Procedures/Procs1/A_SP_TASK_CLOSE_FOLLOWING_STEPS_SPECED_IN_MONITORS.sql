CREATE PROCEDURE dbo.A_SP_TASK_CLOSE_FOLLOWING_STEPS_SPECED_IN_MONITORS 
@taskID varchar(50),
@strNTLogin varchar(50)
AS
print 'Checking monitors to see if there are any tasks we need to close.'
declare @curs cursor,@monID varchar(50),@skipMode varchar(50),@isPassing int
set @curs = CURSOR FOR SELECT ID,SKIP_MODE,IS_PASSING FROM A_MONITOR_TEMPLATES WHERE TASK_ID = @taskID
open @curs
fetch next from @curs into @monID,@skipMode,@isPassing
while @@fetch_status = 0
	begin
	print 'looking at monitor ' + @monID
	if ((isnull(@skipMode,'') = 'SkipPass' and  @isPassing = 1) or  (isnull(@skipMode,'') = 'SkipFail' and  @isPassing = 0))
		begin
		print 'We should skip the steps'
		exec A_SP_MONITOR_DELETE_SKIP_STEP_TASKS @taskID,@monID,@strNTLogin
		end		
	set @skipMode = null
	fetch next from @curs into @monID,@skipMode,@isPassing
	end
close @curs
deallocate @curs
