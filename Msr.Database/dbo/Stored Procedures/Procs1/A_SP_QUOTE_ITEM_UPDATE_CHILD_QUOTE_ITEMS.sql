





CREATE       PROCEDURE dbo.A_SP_QUOTE_ITEM_UPDATE_CHILD_QUOTE_ITEMS 
@ID varchar(50),
@strNTLogin varchar(50)
AS

declare @myQuoteID as varchar(50)

print 'Adding the child quotes for quote item = ' + @ID
print 'We need to get the quote id'
SELECT @myQuoteID = QUOTE_ID FROM A_QUOTE_ITEMS WHERE ID = @ID

print 'Now Insert all the child prod prices and qtys as sub order items'
declare @pplID varchar(50),@qty as real
SELECT @pplID = PROD_PRICE_LIST,@qty = QTY FROM A_QUOTE_ITEMS WHERE ID = @ID

print 'We have the price list now we need to figure out what the children price lists are and add them if they are not in here already'
declare @newID varchar(50),
		@CHILD varchar(50),
		@CHILD_QTY varchar(50),
		@CHILD_PRODUCT_ID varchar(50)
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For SELECT CHILD_ID,QTY,PRODUCT
	FROM A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT WHERE PARENT_ID = @pplID
	AND CHILD_ID NOT IN (SELECT PROD_PRICE_LIST FROM A_QUOTE_ITEMS WHERE PARENT = @ID)
open @curs
Fetch Next from @curs Into @CHILD,@CHILD_QTY,@CHILD_PRODUCT_ID
while (@@fetch_status = 0)
Begin
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_QUOTE_ITEMS (ID,QUOTE_ID,PARENT,PROD_PRICE_LIST,DRCM,MODBY,QTY,PRODUCT_ID)
	values (@newID,@myQuoteID,@ID,@CHILD,getDate(),@strNTLogin,@CHILD_QTY,@CHILD_PRODUCT_ID)
	Fetch Next from @curs Into @CHILD,@CHILD_QTY,@CHILD_PRODUCT_ID
End
close @curs
Deallocate @curs

print 'Just finished adding them'
print 'Updating the children quote Items figuring out their price and all that jazz ' + @pplID
declare @pDisc as varchar(50), @cnt as int
declare @pDiscReason as nvarchar(4000)
set @cnt = 0
set @curs = Cursor For SELECT ID FROM A_QUOTE_ITEMS WHERE PARENT = @ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'We are looking at the child which is  = ' + @it
	if @cnt = 0 
		begin
		select @pDisc = SPECIAL_DISCOUNT, @pDiscReason = SPECIAL_DISC_REASON FROM A_ORDER_ITEMS WHERE ID = @ID
		UPDATE A_QUOTE_ITEMS SET SPECIAL_DISCOUNT = NULL, SPECIAL_DISC_REASON = NULL WHERE ID = @ID
		end
	if @pDisc is not null
		begin
		
		UPDATE A_QUOTE_ITEMS SET SPECIAL_DISCOUNT = @pDisc, SPECIAL_DISC_REASON = @pDiscReason WHERE ID = @it
		end
	exec A_SP_QUOTE_ITEM_UPDATE_CHILD_QUOTE_ITEMS @it,@strNTLogin
	exec A_SP_QUOTE_ITEM_SET_PRICE @it,@strNTLogin
	set @cnt = @cnt + 1
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

exec A_SP_QUOTE_ITEM_SET_PRICE @ID,@strNTLogin





