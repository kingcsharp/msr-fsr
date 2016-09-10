
CREATE  PROCEDURE DBO.A_SP_Z_FORECAST_ITEMS_RESET_ALL_INVOICES
AS
Declare @ID varchar(50)
Declare @curs Cursor
set @curs = 
	Cursor For SELECT ID FROM A_FORECAST_ITEMS
open @curs
Fetch Next from @curs Into @ID
while (@@fetch_status = 0)
	Begin
	print 'item = ' + @ID
	exec A_FORECAST_ITEM_FIGURE_INVOICED_AMOUNTS @ID,'SYS'
	Fetch Next from @curs Into @ID
	End
close @curs
Deallocate @curs


