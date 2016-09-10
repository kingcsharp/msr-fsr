CREATE PROCEDURE dbo.A_SP_ORDERS_CREATE_ALL_CUSTOMER_PARTS
AS
declare @strNTLogin varchar(50),@it nvarchar(50), @curs Cursor

set @curs = Cursor For SELECT ID,MODBY FROM A_ORDERS
open @curs
Fetch Next from @curs Into @it,@strNTLogin
while (@@fetch_status = 0)
	Begin
	print 'Looking at Order = ' + @it
	exec dbo.A_SP_ORDER_SET_UP_CUSTOMER_PARTS @it,@strNTLogin
	Fetch Next from @curs Into @it,@strNTLogin
	End
close @curs
Deallocate @curs

