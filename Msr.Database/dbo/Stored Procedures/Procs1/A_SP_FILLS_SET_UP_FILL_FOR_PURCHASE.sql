



CREATE     PROCEDURE DBO.A_SP_FILLS_SET_UP_FILL_FOR_PURCHASE
@purchHistID varchar(50),
@strNTLogin varchar(50)
AS
print 'Running A_SP_PURCHASE_FILLS_GET_FILLS_SET_UP with ID = ' + @purchHistID

print 'SELECT ID FROM A_V_ORDER_ITEMS_ALL_DATA 
	WHERE PURCHASE_HIST_ID = ''' + @purchHistID + ''' AND ADD_COST_ID IS NULL
	AND (SYSTEM_ID <> ''SYS_PROVIDE_TAKE_BACK'' OR SYSTEM_ID IS NULL)'


Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_V_ORDER_ITEMS_ALL_DATA 
	WHERE PURCHASE_HIST_ID = @purchHistID AND ADD_COST_ID IS NULL
	AND (SYSTEM_ID <> 'SYS_PROVIDE_TAKE_BACK' OR SYSTEM_ID IS NULL)

open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'setting up purchase item =  ' + @it
	exec A_SP_FILLS_SET_UP_FILL_FOR_PURCHASE_ITEM @it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs







