


CREATE           PROCEDURE dbo.A_SP_ORDER_ITEM_UPDATE_CHILD_ORDER_ITEMS 
@ID varchar(50),
@strNTLogin varchar(50)
AS
declare @myOrdID as varchar(50), @pplID varchar(50),@myQuoteID varchar(50),
	@qty as real,@parentID as varchar(50),@myProd varchar(50),
	@newID varchar(50),	@CHILD varchar(50),	@CHILD_QTY varchar(50),	
	@CHILD_PRODUCT_ID varchar(50), @it nvarchar(50), 
	@exID varchar(50),@newExID varchar(50), 
	@curs Cursor, @exCurs Cursor

print 'Adding the child orders for order item = ' + @ID
SELECT @myOrdID = ORDER_ID, @myQuoteID = QUOTE_ID, @pplID = PROD_PRICE_LIST,@qty = QTY, @parentID = PARENT 
		FROM A_ORDER_ITEMS WHERE ID = @ID
SELECT @myProd = PRODUCT FROM A_V_PROD_PRICE_LIST_APPROVED_DATA WHERE ID = @pplID

print 'Data:ordID='+isNull(@myOrdID,'NULL')+',pplID='+isNull(@pplID,'NULL')+',qty='+isNull(convert(varchar(50),@qty),'NULL')+
	',parentID='+isNull(isNull(@parentID,'NULL'),'NULL')+',prod='+isNull(@myProd,'NULL')

print 'ADDING Extra Costs for this one'
set @exCurs = Cursor For SELECT ID FROM A_PROD_PRICE_LIST_EXTRA_COSTS 
				WHERE PROD_PRICE_LIST = (SELECT HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @pplID)
open @exCurs
Fetch Next from @exCurs Into @exID
while (@@fetch_status = 0)
	Begin
	print 'Adding Extra cost = ' + @exID
	exec sp_GetUniqueID3 @newExID OUTPUT
	INSERT INTO A_ORDER_ITEMS (ID,ORDER_ID,QUOTE_ID,PARENT,PROD_PRICE_LIST,ADD_COST_ID,DRCM,MODBY,QTY,PRODUCT_ID,EX_DESC)
	SELECT @newExID,@myOrdID,@myQuoteID,@ID,@pplID,@exID,getDate(),@strNTLogin,@qty,@myProd,[DESCRIPTION]
		FROM A_PROD_PRICE_LIST_EXTRA_COSTS WHERE ID = @exID
	exec A_SP_ORDER_ITEM_SET_PRICE @newExID,@strNTLogin
	Fetch Next from @exCurs Into @exID
	End
close @exCurs
deallocate @exCurs

set @curs = Cursor For SELECT CHILD_ID,QTY,PRODUCT
	FROM A_V_PROD_PRICE_LIST_APPROVED_CHILDREN_WITH_PARENT WHERE PARENT_ID = @pplID
	AND CHILD_ID NOT IN (SELECT PROD_PRICE_LIST FROM A_ORDER_ITEMS WHERE PARENT = @ID)
open @curs
Fetch Next from @curs Into @CHILD,@CHILD_QTY,@CHILD_PRODUCT_ID
while (@@fetch_status = 0)
Begin
	exec sp_GetUniqueID3 @newID OUTPUT
	INSERT INTO A_ORDER_ITEMS (ID,ORDER_ID,QUOTE_ID,PARENT,PROD_PRICE_LIST,DRCM,MODBY,QTY,PRODUCT_ID)
	values (@newID,@myOrdID,@myQuoteID,@ID,@CHILD,getDate(),@strNTLogin,@CHILD_QTY,@CHILD_PRODUCT_ID)
	EXEC A_SP_ORDER_ITEM_UPDATE_CHILD_ORDER_ITEMS @newID,@strNTLogin
	Fetch Next from @curs Into @CHILD,@CHILD_QTY,@CHILD_PRODUCT_ID
End
close @curs
Deallocate @curs

exec A_SP_ORDER_ITEM_SET_PRICE @ID,@strNTLogin



