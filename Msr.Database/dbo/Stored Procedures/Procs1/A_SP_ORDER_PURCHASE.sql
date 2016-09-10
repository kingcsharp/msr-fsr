












CREATE              PROCEDURE DBO.A_SP_ORDER_PURCHASE 
@newID varchar(50) OUTPUT,
@msg nvarchar(4000) OUTPUT,
@orderID varchar(50),
@strNTLogin varchar(50)
AS
begin Transaction
declare @purchID varchar(50)
print 'Purchasing order number = ' + @orderID
print 'so first we need to make a purchase'
exec sp_getUniqueID3 @purchID OUTPUT
INSERT INTO A_PURCHASES_HISTORY 
	(ID,DATE_CREATED,PURCHASE_STATUS,ORDER_ID,DRCM,MODBY,PURCHASER)
VALUES
	(@purchID,getDate(),'Creating',@orderID,getDate(),@strNTLogin,@strNTLogin)
if @@ERROR <> 0 goto problem

print 'Adding all the items'
Declare @it nvarchar(50), @curs Cursor, @itemID varchar(50)
set @curs = Cursor For SELECT ID FROM A_V_ORDER_WITH_QUOTE_ITEMS WHERE ORDER_ID = @orderID and PURCHASE_HIST_ID IS NULL
open @curs
if @@error <> 0 goto PROBLEM
Fetch Next from @curs Into @it
if @@error <> 0 goto PROBLEM
declare @itemCount int
set @itemCount = 0
while (@@fetch_status = 0)
Begin
	set @itemCount = @itemCount + 1
	print 'Creating Item number ' + convert(varchar(50),@itemCount)
	exec sp_GetUniqueID3 @itemID OUTPUT
	print 'Adding a new Purchase Item with the ID of ' + @itemID + ' It is for old item # ' + @it
	INSERT INTO [dbo].[A_ORDER_ITEMS]
	([ID], [SOURCE_ID], [PARENT_QTY],  
	[PRODUCT_ID], [PROD_PRICE_LIST], [QUOTE_ID], 
	[PROC_SYS_ID], [ADD_COST_ID], [TOTAL_QTY], 
	[QTY], [UNIT_PRICE], [UNIT_ESTIMATE], 
	[COMMENTS], [DRCM], [MODBY], [TOTAL_PRICE], 
	[DEST], [FROM_LOC], [TO_LOC], [SPECIAL_DISCOUNT], 
	[SPECIAL_DISC_REASON], [EXPEDITE_PRODUCTION], 
	[EXPEDITE_REASON], [FLAT_RATE], [EX_DESC], 
	[EST_WEIGHT], [EST_WEIGHT_UNIT], [PPL_HIST_ID], 
	[RECURRING], [RECUR_PERIOD], [RECUR_COUNT], 
	[RECUR_START_DATE], [RECUR_STOP_DATE], 
	[RECUR_ACCOUNT], [RECUR_AUTO_FILL], [PURCHASE_HIST_ID])
	SELECT 
	@itemID, [ID], [PARENT_QTY],  
	[PRODUCT_ID], [PROD_PRICE_LIST], [QUOTE_ID], 
	[PROC_SYS_ID], [ADD_COST_ID], [TOTAL_QTY], 
	[QTY], [UNIT_PRICE], [UNIT_ESTIMATE], 
	[COMMENTS], getDate(), @strNTLogin, [TOTAL_PRICE], 
	[DEST], [FROM_LOC], [TO_LOC], [SPECIAL_DISCOUNT], 
	[SPECIAL_DISC_REASON], [EXPEDITE_PRODUCTION], 
	[EXPEDITE_REASON], [FLAT_RATE], [EX_DESC], 
	[EST_WEIGHT], [EST_WEIGHT_UNIT], [PPL_HIST_ID], 
	[RECURRING], [RECUR_PERIOD], [RECUR_COUNT], 
	[RECUR_START_DATE], [RECUR_STOP_DATE], 
	[RECUR_ACCOUNT], [RECUR_AUTO_FILL], @purchID
	FROM A_ORDER_ITEMS WHERE ID = @it
	if @@error <> 0 goto PROBLEM	
	print 'Inserted an Order ITem'
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Added all the items now I need to put all the precedents in'
INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY)
SELECT newID(),PREV_ID,FOL_ID,getDate(),@strNTLogin FROM A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS WHERE PURCHASE_HIST_ID = @purchID

print 'Now I need to set up all the parent child relationships'
UPDATE A_ORDER_ITEMS SET PARENT = dbo.A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM(ID) WHERE PURCHASE_HIST_ID = @purchID
if @@error <> 0 goto PROBLEM


print 'Update the status of the whole purchase now'
exec A_SP_PURCHASE_UPDATE_STATUS @purchID


fin:
print 'Finished the Creating the Purchase with no problem'
SELECT @newID =  OBJECT_ID FROM A_PURCHASES_HISTORY WHERE ID = @purchID
if @@trancount > 0 COMMIT TRANSACTION


declare @prodTime float,@prodUnit varchar(50),@parent varchar(50)
SELECT @prodTime = PRODUCTION_TIME, @prodUnit = PRODUCTION_TIME_UNIT,@parent = PARENT
	FROM A_V_ORDER_ITEMS_ALL_DATA WHERE ID = @itemID
print 'ORDER ITEM = ' + @itemID
print 'prod Unit = ' + @prodUnit
print @prodTime
if @prodTime is not null
	begin
	UPDATE A_ORDER_ITEMS SET DUE_DATE = dbo.A_FN_DATE_TIME_ADD_USING_UNITS(@prodUnit,getDate(),@prodTime) WHERE ID = @itemID
	UPDATE A_ORDER_ITEMS SET DUE_DATE = 
		(SELECT MAX(DUE_DATE) FROM A_ORDER_ITEMS WHERE PARENT = @parent)
		WHERE ID = @parent

	end




return 0

PROBLEM:
print 'There was some sort of problem makig the quote and we are going to not finish'
if @@TRANCOUNT > 0 ROLLBACK TRANSACTION
return 1











