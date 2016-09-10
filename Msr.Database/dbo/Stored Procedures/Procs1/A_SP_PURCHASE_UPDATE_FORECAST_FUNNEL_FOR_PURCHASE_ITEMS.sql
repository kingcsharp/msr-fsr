


CREATE    PROCEDURE dbo.A_SP_PURCHASE_UPDATE_FORECAST_FUNNEL_FOR_PURCHASE_ITEMS 
@purchaseHistID varchar(50),
@strNTLogin varchar(50)
AS
goto fin
Declare @it varchar(50),@curs Cursor
set @curs = Cursor For SELECT PRODUCT_ID
	FROM A_ORDER_ITEMS WHERE PURCHASE_HIST_ID = @purchaseHistID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'going to update funnel step of Forecast = ' + @it
	exec A_SP_PRODUCT_UPDATE_FORECAST_FUNNEL_STEPS @it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

fin:

