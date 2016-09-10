CREATE PROCEDURE DBO.A_SP_ORDER_LOCK_IN_PRICE_LISTS 
@myRoot varchar(50),
@strNTLogin varchar(50)
AS
declare @ordHistID varchar(50)
SELECT @ordHistID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @myRoot

print 'We are going to lock in the prices for the order ' + @myRoot
declare @curs as CURSOR,@itemID varchar(50),@pplHistID varchar(50),@pplID varchar(50)
set @curs = CURSOR FOR SELECT ID FROM A_ORDER_ITEMS WHERE ORDER_ID = @ordHistID
open @curs
fetch next from @curs into @itemID
while @@fetch_status = 0
	begin
	print 'Locking in the price list for item = ' + @itemID
	select @pplID = PROD_PRICE_LIST FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @itemID
	select @pplHistID = HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @pplID
	UPDATE A_ORDER_ITEMS SET PPL_HIST_ID = @pplHistID WHERE ID = @itemID
	fetch next from @curs into @itemID
	end
close @curs
deallocate @curs
print 'Done Locking in the price'




