CREATE PROCEDURE dbo.A_SP_TASK_ACCEPT_BATCHED_TASKS 
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @curs as cursor,@childTaskID as varchar(50)
set @curs = CURSOR FOR SELECT CHILD_TASK_ID FROM A_V_BATCH_TASK_CHILDREN WHERE BATCH_TASK_ID = @ID
open @curs
fetch next from @curs into @childTaskID
while @@fetch_status = 0
	begin
	print 'Accepting ' + @childTaskID
	exec A_SP_TASK_ACCEPT null,null,@childTaskID,@strNTLogin
	fetch next from @curs into @childTaskID
	end


