














CREATE                  PROCEDURE dbo.A_SP_QUOTE_ORDER_LINK_ACCEPT_ORDER_AND_CREATE_QUOTE
@newID varchar(50) OUTPUT,
@msg nvarchar(4000) OUTPUT,
@QOL_ID varchar(50),
@strNTLogin varchar(50)
AS

BEGIN TRANSACTION
declare @RET_STATUS varchar(50),@MSGS varchar(500),@TASK_ID varchar(50),
	@taskStat varchar(50),@taskReq varchar(50)
SELECT @TASK_ID = ID, @taskStat = STATUS, @taskReq = REQUESTEE_ID
	FROM A_V_TASK_WITH_QUOTE_ORDER_LINK_INFO WHERE QOL_ID = @QOL_ID
if @taskReq = @strNTLogin and @taskStat = 'ACCEPTED'
	goto carryOn

exec A_SP_TASK_ACCEPT @RET_STATUS OUTPUT,@MSGS OUTPUT,@TASK_ID,@strNTLogin
if @@error <> 0 goto PROBLEM
if @RET_STATUS <> ''  
	begin
		set @msg = 'Error accepting Task'
		print 'Error Accepting Quote Task'
		print @RET_STATUS
		goto PROBLEM
	end
carryOn:
print 'Accepting the QOL with the ID of ' + isNull(@QOL_ID,'NULL')

declare @orderID varchar(50),@supplier varchar(50),@quoteCheckID varchar(50),@supplierName varchar(50)
SELECT 	@supplier = SUPPLIER,
		@orderID = ORDER_ID,
		@quoteCheckID = QUOTE_ID
	FROM A_QUOTE_ORDER_LINK 
	WHERE ID = @QOL_ID
if @@error <> 0 goto PROBLEM

SELECT @supplierName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplier

if @quoteCheckID is not null
	begin
	set @msg =  'There is already a quote made for this order'
	goto problem
	end

print 'The ORder ID = '
print @orderID
print 'Copying all the order info to the quote'
declare @quoteID varchar(50)
exec sp_getUniqueID3 @quoteID OUTPUT
print 'First the header data'
INSERT INTO A_QUOTES_HISTORY 
	(ID,DESCRIPTION,ORDER_ID,CUSTOMER_PERSON,
	CUSTOMER_CO,BUGETARY_ONLY,EXPIRATION_DATE,
	DRCM,MODBY,SUPPLIER_ID,CREATION_DATE,PROGRESS)
	SELECT 
		@quoteID AS ID,DESCRIPTION + '(supplier: ' + @supplierName + ')',@orderID as ORDER_ID,CUSTOMER_PERSON,
		CUSTOMER_CO,BUDGETARY_ONLY,EXPIRATION_DATE,
		getDate(),@strNTLogin,@supplier,getDate(),'QUOTE_CREATING'
		FROM A_ORDERS o inner join A_ORDERS_HISTORY h on o.HISTORY_REF_ID = h.ID
	WHERE
		o.ID = @orderID
if @@error <> 0 goto PROBLEM


declare @ordHistID varchar(50)
SELECT @ordHistID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @orderID

print 'Made the header now to copy all the items'
Declare @it nvarchar(50), @curs Cursor, @itemID varchar(50)
set @curs = Cursor For SELECT ID FROM A_V_ORDER_ITEMS_DATA_WITH_SHIPPING WHERE ORDER_ID = @ordHistID AND SUPPLIER_ID = @supplier
open @curs
if @@error <> 0 goto PROBLEM
Fetch Next from @curs Into @it
if @@error <> 0 goto PROBLEM
while (@@fetch_status = 0)
Begin
	exec sp_GetUniqueID3 @itemID OUTPUT
	print 'Adding a new Quote Item with the ID of ' + @itemID + ' It is for old item # ' + @it
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
	[RECUR_ACCOUNT], [RECUR_AUTO_FILL])
	SELECT 
	@itemID, [ID], [PARENT_QTY],  
	[PRODUCT_ID], [PROD_PRICE_LIST], @quoteID, 
	[PROC_SYS_ID], [ADD_COST_ID], [TOTAL_QTY], 
	[QTY], [UNIT_PRICE], [UNIT_ESTIMATE], 
	[COMMENTS], getDate(), @strNTLogin, [TOTAL_PRICE], 
	[DEST], [FROM_LOC], [TO_LOC], [SPECIAL_DISCOUNT], 
	[SPECIAL_DISC_REASON], [EXPEDITE_PRODUCTION], 
	[EXPEDITE_REASON], [FLAT_RATE], [EX_DESC], 
	[EST_WEIGHT], [EST_WEIGHT_UNIT], [PPL_HIST_ID], 
	[RECURRING], [RECUR_PERIOD], [RECUR_COUNT], 
	[RECUR_START_DATE], [RECUR_STOP_DATE], 
	[RECUR_ACCOUNT], [RECUR_AUTO_FILL]
	FROM A_ORDER_ITEMS WHERE ID = @it
	if @@error <> 0 goto PROBLEM	
	print 'Inserted an Order ITem'

	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

print 'Added all the items now I need to put all the precedents in'
INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY)
SELECT newID(),PREV_ID,FOL_ID,getDate(),@strNTLogin FROM A_V_QUOTE_PRECEDENTS_FROM_ORDER_IDS WHERE QUOTE_ID = @quoteID

print 'Now I need to set up all the parent child relationships'
UPDATE A_ORDER_ITEMS SET PARENT = dbo.A_FN_QUOTE_ITEM_GET_PARENT_USING_ORDER_ITEM(ID) WHERE QUOTE_ID = @quoteID
if @@error <> 0 goto PROBLEM

print 'Tell the Quote Order Link that we made a quote and the number is ' + @quoteID
UPDATE A_QUOTE_ORDER_LINK SET 
	STATUS = 'QUOTE_STARTED',
	QUOTE_ID = @quoteID
	WHERE ID = @QOL_ID
if @@error <> 0 goto PROBLEM




fin:
print 'Finished the Creating the Quote with no problem'
SELECT @newID =  OBJECT_ID FROM A_QUOTES_HISTORY WHERE ID = @quoteID
if @@trancount > 0 COMMIT TRANSACTION
return 0

PROBLEM:
print 'There was some sort of problem makig the quote and we are going to not finish'
if @@TRANCOUNT > 0 ROLLBACK TRANSACTION
return 1














