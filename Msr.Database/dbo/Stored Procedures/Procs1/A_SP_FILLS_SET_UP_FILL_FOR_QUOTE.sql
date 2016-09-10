

CREATE   PROCEDURE DBO.A_SP_FILLS_SET_UP_FILL_FOR_QUOTE
@phID varchar(50),
@qID varchar(50),
@strNTLogin varchar(50)
AS
print 'Setting up the fills for quote id = ' + @qID

Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS WHERE QUOTE_ID = @qID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'setting up quote item =  ' + @it
	exec A_SP_FILLS_SET_UP_FILL_FOR_QUOTE_ITEM @it,@qID,@phID,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


