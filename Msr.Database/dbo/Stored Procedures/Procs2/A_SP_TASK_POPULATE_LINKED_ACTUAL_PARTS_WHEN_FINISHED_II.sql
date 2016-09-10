CREATE procedure dbo.A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED_II 
@taskID varchar(50),
@apID varchar(50)
AS
INSERT INTO A_TASK_ACTUAL_PARTS_LINKED_WHEN_FINISHED (ID,TASK_ID,ACTUAL_PART_ID,DRCM,MODBY)
VALUES (newID(),@taskID,@apID,getDAte(),null)
declare @curs as cursor,@objID as varchar(50)
set @curs = cursor for 
	SELECT ID FROM A_V_ACTUAL_PARTS_QUICK WHERE PARENT_ID = @apID
open @curs
fetch next from @curs into @objID
while @@fetch_status = 0
	begin
		exec A_SP_TASK_POPULATE_LINKED_ACTUAL_PARTS_WHEN_FINISHED_II @taskID,@objID
		fetch next from @curs into @objID
	end
close @curs
deallocate @curs


