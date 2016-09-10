CREATE PROCEDURE dbo.A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED 
@taskID varchar(50)
AS
DELETE FROM A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED WHERE TASK_ID = @taskID
declare @curs as cursor,@objID as varchar(50)
set @curs = cursor for 
	SELECT OBJECT_ID FROM A_TASK_OBJECT_LINK WHERE TASK_ID = @taskID
open @curs
fetch next from @curs into @objID
while @@fetch_status = 0
	begin
		exec A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED_II @taskID,@objID
		fetch next from @curs into @objID
	end
close @curs
deallocate @curs



