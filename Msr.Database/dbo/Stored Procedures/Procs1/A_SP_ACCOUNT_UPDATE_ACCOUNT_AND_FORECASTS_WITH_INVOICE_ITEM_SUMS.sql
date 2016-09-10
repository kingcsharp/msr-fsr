
CREATE  PROCEDURE dbo.A_SP_ACCOUNT_UPDATE_ACCOUNT_AND_FORECASTS_WITH_INVOICE_ITEM_SUMS
@acctID varchar(50)
AS
UPDATE A_ACCOUNTS_HISTORY 
	SET TOTAL_PURCHASES = 
		isNull((SELECT SUM(AMOUNT) FROM A_ACCOUNT_INVOICE_ITEMS WHERE ACCOUNT_ID = @acctID),0)
			WHERE ID IN (SELECT OBJ_ID FROM A_OBJECTS WHERE ROOT = @acctID)

Declare @fiID varchar(50),@START_MONTH int,@START_YEAR int,@STOP_MONTH int,@STOP_YEAR int
Declare @curs Cursor
set @curs = 
	Cursor For 
		SELECT ID FROM  A_FORECAST_ITEMS WHERE ACCOUNT_ID = @acctID
open @curs
Fetch Next from @curs Into @fiID
while (@@fetch_status = 0)
Begin
	print 'Updating forecast Item = ' + @fiID
	exec A_FORECAST_ITEM_FIGURE_INVOICED_AMOUNTS @fiID,'SYSTEM'
	Fetch Next from @curs Into @fiID
End
close @curs
Deallocate @curs

