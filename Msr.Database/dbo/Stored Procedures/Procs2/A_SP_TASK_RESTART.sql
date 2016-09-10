
CREATE      PROCEDURE dbo.A_SP_TASK_RESTART
@newID varchar(200) OUTPUT,
@msgs varchar(200) OUTPUT,
@taskID varchar(50),
@strNTLogin varchar(50)
AS
declare @mainReq varchar(50)
print 'Restarting task ' + @taskID
UPDATE A_TASKS SET STATUS = 'REQUESTED' WHERE ID = @taskID
SELECT @mainReq = REQUESTOR FROM A_TASKS WHERE ID = @taskID
declare @curs as CURSOR, @it varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_TASKS WHERE PARENT_ID = @taskID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	exec A_SP_TASK_DELETE_TASK_FOREVER @it,@mainReq
	fetch next from @curs into @it
	end 
deallocate @curs
exec A_SP_TASK_CREATE_CHILD_TASKS_FOR_PROCEDURE_STEPS @taskID,@mainReq

