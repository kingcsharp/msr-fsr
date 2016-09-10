

CREATE   PROCEDURE DBO.A_SP_PURCHASE_ITEM_FIGURE_PRICE
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'entering A_SP_PURCHASE_ITEM_FIGURE_PRICE '

declare @curs as cursor
declare @it varchar(50)
set @curs = cursor for SELECT ID FROM A_PURCHASE_ITEMS WHERE PARENT_ID = @ID
open @curs
fetch next from @curs into @it
while @@fetch_status = 0
	begin
	exec A_SP_PURCHASE_ITEM_FIGURE_PRICE @it,@strNTLogin
	fetch next from @curs into @it
	end
close @curs
deallocate @curs

declare @quoteItemID varchar(50),@purchID varchar(50)
SELECT @quoteItemID = QUOTE_ITEM_ID, @purchID = PURCHASE_ID FROM A_PURCHASE_ITEMS WHERE ID = @ID
print 'The quote Item ID = ' + @quoteItemID
print 'Usually we would go check the volume and special discount tables here, but since those are not really ready'
print 'we will just go to the unit price and get it.'
declare @unitPrice money
SELECT @unitPrice = UNIT_PRICE FROM A_QUOTE_ITEMS WHERE ID = @quoteItemID
UPDATE A_PURCHASE_ITEMS 
	SET ITEM_TOTAL = ((@unitPrice * convert(money,QTY)) + (SELECT isNull(SUM(ITEM_TOTAL),0) 
	FROM A_PURCHASE_ITEMS WHERE PARENT_ID = @ID)),UNIT_PRICE = @unitPrice WHERE ID = @ID








print 'Exiting A_SP_PURCHASE_ITEM_FIGURE_PRICE'








