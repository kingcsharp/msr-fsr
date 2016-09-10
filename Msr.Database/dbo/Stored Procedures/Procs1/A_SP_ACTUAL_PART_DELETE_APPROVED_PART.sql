CREATE PROCEDURE DBO.A_SP_ACTUAL_PART_DELETE_APPROVED_PART
@ID varchar(50),
@strNTLogin varchar(50)
AS

Declare @it as nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For 
	SELECT ID FROM A_V_ACTUAL_PARTS_APPROVED_DATA 
	WHERE PARENT_ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_SP_ACTUAL_PART_DELETE_APPROVED_PART @it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs 


declare @histID varchar(50)
SELECT @histID = HISTORY_REF_ID FROM A_ACTUAL_PARTS WHERE ID = @ID
DELETE FROM A_OBJECTS WHERE ROOT = @ID
