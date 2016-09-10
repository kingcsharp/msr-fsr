

CREATE PROCEDURE [dbo].[A_SP_TASK_CANCEL_UNFINISHED_CHILD_STEPS]
@RET_STATUS as varchar(500) OUTPUT,
@MSGS as varchar(50) OUTPUT,
@taskID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Deleting all unfinished steps for task = ' + @taskID
declare @curs as cursor, @childTask varchar(50)
set @curs = cursor for SELECT ID FROM A_TASKS WHERE PARENT_ID = @taskID AND STATUS not in ('CLOSED','FINISHED')
open @curs
fetch next from @curs into @childTask
while @@FETCH_STATUS = 0
begin
	print 'Child Task = ' + @childTask
	exec A_SP_TASK_CANCEL_UNFINISHED_CHILD_STEPS @RET_STATUS OUTPUT,@MSGS OUTPUT, @CHILDTask, @strNTLogin
	DELETE FROM A_TASKS WHERE ID = @childTask
	fetch next from @curs into @childTask
end
