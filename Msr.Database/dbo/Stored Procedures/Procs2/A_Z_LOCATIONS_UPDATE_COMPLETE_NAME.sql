CREATE PROCEDURE DBO.A_Z_LOCATIONS_UPDATE_COMPLETE_NAME
AS
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_LOCATIONS_HISTORY
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_LOCATION_UPDATE_PARENT_PATH @it
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs
