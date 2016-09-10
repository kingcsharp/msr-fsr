CREATE PROCEDURE A_X_ACTUAL_PARTS_UPDATE_DATE_TO_UPDATE_NAME
AS
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_ACTUAL_PARTS_HISTORY
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	UPDATE A_ACTUAL_PARTS_HISTORY SET DRCM = getDate()	WHERE ID = @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs



