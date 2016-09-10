
CREATE  PROCEDURE dbo.A_SP_ORDER_UPDATE_FORECAST_FUNNEL_FOR_ORDER_ITEMS 
@ordHistID varchar(50),
@strNTLogin varchar(50)
AS
Declare @it varchar(50),@curs Cursor
set @curs = Cursor For SELECT PRODUCT_ID
	FROM A_ORDER_ITEMS WHERE ORDER_ID = @ordHistID AND QUOTE_ID IS NULL AND PURCHASE_HIST_ID IS NULL
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'going to update funnel step of Product = ' + @it
	exec A_SP_PRODUCT_UPDATE_FORECAST_FUNNEL_STEPS @it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

