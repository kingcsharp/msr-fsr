
CREATE  PROCEDURE A_X_TASKS_UPDATE_DATE_TO_FIRE_TRIGGER
AS
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_TASKS
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	UPDATE A_TASKS SET DRCM = getDate()	WHERE ID = @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs




