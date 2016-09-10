
CREATE  PROCEDURE dbo.A_SP_PROCEDURE_UPDATE_PRODUCT_SUB_PRICES
@procID varchar(50),
@strNTLogin varchar(50)
AS
print 'Updating the product Price Lists for products of this procedure = ' + @procID
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_V_PRODUCTS_APPROVED_DATA 
	WHERE PROCEDURE_ID = @procID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'The product we are looking at is = ' + @it
	exec A_SP_PRODUCT_RESET_PRICE_LIST_CHILDREN @it,@procID,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


