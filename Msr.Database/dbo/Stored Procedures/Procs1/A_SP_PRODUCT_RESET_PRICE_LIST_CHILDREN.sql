
CREATE  PROCEDURE dbo.A_SP_PRODUCT_RESET_PRICE_LIST_CHILDREN 
@prodID varchar(50),
@procID varchar(50),
@strNTLogin varchar(50)
AS
print 'In here'
print 'Updating the Price Lists for the product = ' + isNull(@prodID,'NULL')
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT ID FROM A_PROD_PRICE_LIST_HISTORY WHERE PRODUCT = @prodID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'The product price list we are looking at is = ' + @it
	exec A_SP_PROD_PRICE_LIST_ADD_CHILDREN_AND_FILL_THEM @prodID,@it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

