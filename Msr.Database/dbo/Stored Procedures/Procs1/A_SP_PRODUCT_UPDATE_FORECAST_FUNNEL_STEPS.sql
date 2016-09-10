

CREATE  PROCEDURE dbo.A_SP_PRODUCT_UPDATE_FORECAST_FUNNEL_STEPS
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating all forecasts for this product'
Declare @it varchar(50),@curs Cursor
set @curs = Cursor For SELECT F_ITEM_ID
	FROM A_V_FORECAST_ITEMS_WITH_PRODUCTS WHERE PROD_ID = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'going to update funnel step of Forecast Item = ' + @it + ' And prod ID = ' + @ID
	exec A_SP_FORECAST_ITEM_FIGURE_PROGRESS @it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


