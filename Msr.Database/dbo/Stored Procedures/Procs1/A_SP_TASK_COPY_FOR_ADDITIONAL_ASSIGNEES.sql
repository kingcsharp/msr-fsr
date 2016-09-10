CREATE PROCEDURE dbo.A_SP_TASK_COPY_FOR_ADDITIONAL_ASSIGNEES
@nt varchar(8000) OUTPUT,
@messages varchar(8000) OUTPUT,
@taskID varchar(50),
@strNTLogin varchar(50)
AS
print 'Making copies for everyone in the additional assignees list'
Declare @it nvarchar(50),@newTaskID nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT PERSON_ID FROM A_TASK_ADDITIONAL_ASSIGNEES_LINK WHERE TASK_ID = @taskID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Copying the task and assigning to ' + @it
	print 'declare @newTaskID varchar(50)'
	print 'exec A_SP_TASK_COPY_TASK @newTaskID OUTPUT,''' + @taskID + ''',''' + @it + ''',''' + @strNTLogin + ''''
	exec A_SP_TASK_COPY_TASK @newTaskID OUTPUT,@taskID,@it,@strNTLogin
	set @nt = isNull(@nt + ',','') + @newTaskID
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
