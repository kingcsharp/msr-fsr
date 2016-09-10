


CREATE   PROCEDURE dbo.A_SP_TASK_COMPLETELY_UPDATE_CHILD_TASK_TABLE
AS
--First delete Everything in the sunordinates table
DELETE FROM A_TASKS_RELATION_TABLE
--Now get a cursor for all tasks that are not parents
Declare @oneTask nvarchar(50)
Declare @Cur Cursor
set @Cur = Cursor
For SELECT ID FROM A_TASKS t WHERE
NOT EXISTS (SELECT ID FROM A_TASKS c WHERE c.ID <> t.ID AND c.PARENT_ID = t.ID)
 
open @Cur
Fetch Next from @Cur
Into @oneTask
while (@@fetch_status = 0)
Begin
	print 'This task has no children' + @oneTask
	exec A_SP_TASKS_UPDATE_ONE_TASK_SUB_TABLE @oneTask,0
	Fetch Next from @Cur
	Into @oneTask
End
close @Cur
Deallocate @Cur









