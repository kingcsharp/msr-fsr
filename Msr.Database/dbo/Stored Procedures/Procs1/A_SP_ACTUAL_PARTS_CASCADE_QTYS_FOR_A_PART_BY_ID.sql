CREATE PROCEDURE DBO.A_SP_ACTUAL_PARTS_CASCADE_QTYS_FOR_A_PART_BY_ID
@mult float,
@PARENT_ID varchar(50)
AS
Declare @cID varchar(50)
Declare @cOID varchar(50)
Declare @myRoot varchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID,OBJECT_ID 
FROM A_ACTUAL_PARTS_HISTORY 
WHERE PARENT_ID = @PARENT_ID
open @curs
Fetch Next from @curs Into @cID,@cOID
while (@@fetch_status = 0)
Begin
	print 'Updating part ID = ' + @cID
	UPDATE A_ACTUAL_PARTS_HISTORY SET QTY = (QTY * @mult) WHERE ID = @cID
	SELECT @myRoot = ROOT FROM A_OBJECTS WHERE ID = @cOID
	Fetch Next from @curs Into @cID,@cOID
End
close @curs
Deallocate @curs



