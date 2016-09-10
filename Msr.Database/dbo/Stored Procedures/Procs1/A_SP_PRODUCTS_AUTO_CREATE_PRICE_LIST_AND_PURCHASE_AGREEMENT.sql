



CREATE            PROCEDURE dbo.A_SP_PRODUCTS_AUTO_CREATE_PRICE_LIST_AND_PURCHASE_AGREEMENT
@prodObjID varchar(50),
@strNTLogin varchar(50)
AS
print 'Time = ' + convert(varchar(50),getDate())
declare @strProdHistID varchar(50),@prodName varchar(50),@pqpID varchar(50)
declare @pplObjID varchar(50),@msgs varchar(3000)
SELECT @prodName = NAME,@strProdHistID = ID FROM A_PRODUCTS_HISTORY WHERE OBJECT_ID = @prodObjID
if not exists (SELECT PROD_HIST_ID FROM A_PRODUCTS_QUICK_PRICE 
				WHERE PROD_HIST_ID = @strProdHistID AND CREATE_PRICE_LIST = 1 and QUOTE_ID is null)
	goto fin
declare @custID varchar(50),@price float, @prodTime float,@prodTimeUnit varchar(50),@capacity float, @capUnit varchar(50),
	@prodID varchar(50),@KIT_ID varchar(500),@KIT_QTY float,@IS_KIT tinyint
SELECT @prodID = ID FROM A_PRODUCTS WHERE HISTORY_REF_ID = @strProdHistID
if @prodID is null
	begin
	print 'ERROR COULD NOT USE THIS PRODUCT ' + @strProdHistID
	goto PROBLEM
	end

declare @pqpCurs as cursor
set @pqpCurs = cursor for 
			SELECT ID FROM A_PRODUCTS_QUICK_PRICE 
			WHERE PROD_HIST_ID = @strProdHistID and QUOTE_ID IS NULL
open @pqpCurs
fetch next from @pqpCurs into 	@pqpID
while @@fetch_status = 0
	begin
	SELECT 		@pplObjID = PRICE_LIST_MADE_OBJ_ID,
				@custID = CUST_ID,
				@price = PRICE, 
				@prodTime = PROD_TIME,
				@prodTimeUnit = PROD_TIME_UNIT,
				@capacity = CAPACITY, 
				@capUnit = CAPACITY_UNITS,
				@KIT_ID = KIT_ID,
				@KIT_QTY = KIT_QTY,
				@IS_KIT = IS_KIT
	FROM A_PRODUCTS_QUICK_PRICE WHERE ID = @pqpID

if exists (SELECT ID FROM A_OBJECTS WHERE ID = @pplObjID AND STATUS = 'APPROVED')
	begin
	print 'Price List Already Made'
	goto CreateOrder
	end
if @pplObjID is null
	begin
	declare @pplHistID varchar(50)
	print 'Need to see if there is a price list we can just add ourselves too'
	SELECT @pplHistID = HISTORY_REF_ID,@pplObjID = OBJECT_ID FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE
		PRODUCT = @prodID AND UNIT_PRICE = @price
		AND PRODUCTION_TIME = @prodTime
		AND PRODUCTION_TIME_UNIT = @prodTimeUnit
--		AND CAPACITY = @capacity
--		AND CAPACITY_UNIT = @capUnit

	print 'SELECT * FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE
		PRODUCT = ''' + isNull(@prodID,'') + '''
		AND UNIT_PRICE = ''' + convert(varchar(50),isNull(@price,'')) + '''
		AND PRODUCTION_TIME = ''' + convert(varchar(50),isNull(@prodTime,'')) + '''
		AND PRODUCTION_TIME_UNIT = ''' + isNull(@prodTimeUnit,'') + ''''
	if @pplObjID is not null
		begin
		print 'Found a suitable price list'
		if not exists(SELECT * FROM A_PROD_PRICE_CUSTOMERS WHERE PP_LIST_ID = @pplHistID AND CUST_ID = @custID)
			begin
			print 'We need to add this customer to the price list customers real and here'
			INSERT INTO A_PROD_PRICE_CUSTOMERS (ID,CUST_ID,PP_LIST_ID,DRCM,MODBY)
				VALUES (newID(),@custID,@pplHistID,getDate(),@strNTLogin)
			INSERT INTO A_PROD_PRICE_LIST_REAL_CUSTOMERS (ID,CUST_ID,PP_LIST_ID,DRCM,MODBY)
				VALUES (newID(),@custID,@pplHistID,getDate(),@strNTLogin)
			end
		else
			begin
			print 'This Customer was already on this price list so we dont need to add them.'
			end
		UPDATE A_PRODUCTS_QUICK_PRICE 
			SET PRICE_LIST_MADE_OBJ_ID = @pplObjID 
			WHERE ID = @pqpID
		goto CreateOrder
		end
	end

print 'There is no price list so I need to make one'
exec A_SP_PROD_PRICE_LIST_UPDATE
@pplObjID OUTPUT, 	--@newObjID varchar(50) OUTPUT,
@msgs OUTPUT, 		--@messages nvarchar(2000) OUTPUT,
null,				--@objID varchar(50),
@prodID,			--@PRODUCT varchar(50),
@custID,			--@CUSTOMERS varchar(8000),
'UNIT',				--@UNIT varchar(50),
null,				--@MIN_QUANTITY varchar(50),
@price,				--@UNIT_PRICE varchar(50),
null,				--@EST_UNIT_PRICE varchar(50),
null,				--@EST_LABOR_PRICE varchar(50),
null,				--@EST_PARTS_PROV_STAY varchar(50),
null,				--@EST_PARTS_PROV_TAKE_BACK varchar(50),
null,				--@EST_PARTS_CONSUMED varchar(50),
'ESTIMATED',		--@INVOICE_FROM varchar(50),
@prodTime,			--@PRODUCTION_TIME varchar(50),
@prodTimeUnit,		--@PRODUCTION_TIME_UNIT varchar(50),
@capacity,			--@CAPACITY varchar(50),
@capUnit,			--@CAPACITY_UNIT varchar(50),
null,				--@SPECIAL_CUSTOMER varchar(8000),
null,				--@CUSTOMER_EXCEPTIONS varchar(8000),
1,					--@FOR_INDIVIDUAL_SALE tinyInt,
@strNTLogin 		--@strNTLogin varchar(50)

UPDATE A_PRODUCTS_QUICK_PRICE SET PRICE_LIST_MADE_OBJ_ID = @pplObjID WHERE ID = @pqpID

exec A_SP_PROD_PRICE_LIST_QUICK_ADD_UNIT_COST 
@pplObjID,		--PPL Object ID
@price, 		--Price
'Cost of Item', --Description
'QTY_BASED',    --PRICE_LIST_TYPE
'UNIT',			--Cost Unit
@strNTLogin

exec A_SP_OBJECTS_QUICK_APPROVE @pplObjID,@strNTLogin
exec A_SP_PROD_PRICE_LIST_FINISH_APPROVAL_WF null,@pplObjID,@strNTLogin

print 'We have made the price list now'

CreateOrder:
if @price is null
	goto fin
if @IS_KIT = 1
	goto reallyCreateOrder
declare @orderObjID varchar(50),@paName varchar(2000),@custPersonID varchar(50),@ordHistID varchar(50)
SELECT @orderObjID = ORDER_ID FROM A_PRODUCTS_QUICK_PRICE WHERE ID = @pqpID
if @orderObjID is not null
	begin
	print 'The order was already made so I need to make the quote'
	goto createQuote
	end
SELECT @orderObjID = o.OBJECT_ID 
	FROM A_ORDER_ITEMS i, A_V_ORDERS_APPROVED_DATA o 
	WHERE i.ORDER_ID = o.HISTORY_REF_ID 
	AND o.CUSTOMER_CO = @custID AND PROD_PRICE_LIST = @pplObjID
if @orderObjID is not null
	begin
	print 'Found an order that has this item on it so not making another'
	UPDATE A_PRODUCTS_QUICK_PRICE SET ORDER_ID = @orderObjID WHERE ID = @pqpID
	goto createQuote
	end

reallyCreateOrder:
print 'Creating the Order now'
SELECT @custPersonID = ID FROM A_V_PEOPLE_APPROVED_DATA p 
	WHERE 
		ROOT_COMPANY = (SELECT ROOT_CO_ID FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = @custID)
 	AND 
		p.ID IN (SELECT PERSON_ID FROM A_V_ROLES_WITH_ASSIGNEES WHERE IS_ADMIN = 1)

if @custPersonID is null
	begin
	print 'The customer person id null'
	SELECT ROOT_CO_ID FROM A_V_COMPANIES_APPROVED_DATA_QUICK WHERE ID = @custID	
	goto fin
	end

set @paName = 'Quote for ' + @prodName
exec A_SP_ORDERS_UPDATE_ORDER
@orderObjID OUTPUT,						--nvarchar(50) OUTPUT,
@msgs OUTPUT,							--nvarchar(2000) OUTPUT,
null,									--@objID varchar(50),
null,									--@ID varchar(50),
@custPersonID,							--@CUSTOMER_PERSON varchar(50),
@custID,								--@CUSTOMER_CO varchar(200),
@paName,							 	--@DESCRIPTION varchar(200),
0,										--@BUDGETARY_ONLY varchar(50),
null,									--@SUPPLIERS_TO_SHARE_WITH varchar(8000),
null,									--@EXPIRATION_DATE  varchar(50),
@custPersonID							--@strNTLogin varchar(50)


declare @itemID varchar(50),@prodRootID varchar(50)
SELECT @prodRootID = ROOT FROM A_OBJECTS WHERE ID = @prodObjID
exec A_SP_ORDER_ITEM_UPDATE_ITEM
@itemID OUTPUT,						--@newID nvarchar(50) OUTPUT,
@msgs,								--@messages nvarchar(2000) OUTPUT,
@orderObjID,						--@objID varchar(50),
null,								--@ID varchar(50),
@prodRootID,						--@PROD_OR_LIST varchar(50),
1,									--@QTY varchar(50),
0,									--@RECURRING nvarchar(50),
null,								--@SPECIAL_DISCOUNT varchar(50),
@pplObjID,							--@PROD_PRICE_LIST nvarchar(50),
null,								--@SPECIAL_DIS_REASON nvarchar(4000),
null,								--@COMMENT nvarchar(4000),
null,								--@PARENT_ID varchar(50),
null,								--@PROC_SYS_ID varchar(50),
null,								--@dest varchar(50),
null,								--@FROM_LOC varchar(50),
null,								--@TO_LOC varchar(50),
@custPersonID

if @IS_KIT = 1
	begin
	print 'This is a KIT so ADD the other things'
	print 'SELECT PRICE_LIST_MADE_OBJ_ID FROM A_PRODUCTS_QUICK_PRICE WHERE KIT_ID = ''' + @KIT_ID + ''' AND KIT_QTY IS NOT NULL'
	declare @subProdID varchar(50),@pcurs as CURSOR,@kQ varchar
	set @pCurs = CURSOR FOR SELECT PRICE_LIST_MADE_OBJ_ID,KIT_QTY FROM A_PRODUCTS_QUICK_PRICE WHERE KIT_ID = @KIT_ID AND KIT_QTY IS NOT NULL
	open @pCurs
	fetch next from @pCurs into @subProdID,@kQ
	while @@fetch_status = 0
		begin
		print 'Qty = ' + isnull(@kQ,'NULL')
		exec A_SP_ORDER_ITEM_UPDATE_ITEM
		@itemID OUTPUT,						--@newID nvarchar(50) OUTPUT,
		@msgs,								--@messages nvarchar(2000) OUTPUT,
		@orderObjID,						--@objID varchar(50),
		null,								--@ID varchar(50),
		@subProdID,						--@PROD_OR_LIST varchar(50),
		@kQ,									--@QTY varchar(50),
		0,									--@RECURRING nvarchar(50),
		100,								--@SPECIAL_DISCOUNT varchar(50),
		null,							--@PROD_PRICE_LIST nvarchar(50),
		null,								--@SPECIAL_DIS_REASON nvarchar(4000),
		null,								--@COMMENT nvarchar(4000),
		null,								--@PARENT_ID varchar(50),
		null,								--@PROC_SYS_ID varchar(50),
		null,								--@dest varchar(50),
		null,								--@FROM_LOC varchar(50),
		null,								--@TO_LOC varchar(50),
		@custPersonID
		fetch next from @pCurs into @subProdID,@kQ
		end	


	end




print 'order made now approve it'
exec A_SP_OBJECTS_QUICK_APPROVE @orderObjID,@strNTLogin
print 'Finish the workflow on the Order'
exec A_SP_ORDERS_FINISH_WF null,@orderObjID,@custPersonID
print 'Created the Order no problem'
UPDATE A_PRODUCTS_QUICK_PRICE SET ORDER_ID = @orderObjID WHERE ID = @pqpID

createQuote:
--We need to get the quote order link now
declare @QOL_ID varchar(50),@msg varchar(2000)
SELECT @QOL_ID = ID FROM A_QUOTE_ORDER_LINK WHERE ORDER_ID = @orderObjID
declare @quoteObjID varchar(50)
SELECT @quoteObjID = QUOTE_ID FROM A_PRODUCTS_QUICK_PRICE WHERE ID = @pqpID
if @quoteObjID is not null
	begin
	print 'The quote was already made'
	goto fin
	end

SELECT @quoteObjID = o.OBJECT_ID 
	FROM A_ORDER_ITEMS i, A_V_QUOTES_APPROVED_DATA o 
	WHERE i.QUOTE_ID = o.HISTORY_REF_ID 
	AND o.CUSTOMER_CO = @custID AND PROD_PRICE_LIST = @pplObjID

if @quoteObjID is not null and not(@IS_KIT = 1) 
	begin
	print 'Found a Quote that has this item on it so not making another'
	UPDATE A_PRODUCTS_QUICK_PRICE SET QUOTE_ID = @quoteObjID WHERE ID = @pqpID
	goto acceptQuote
	end



print 'Making the quote now'
declare 	@orderID varchar(50),@supplier varchar(50),
			@quoteCheckID varchar(50),@supplierName varchar(50)
SELECT 	@supplier = SUPPLIER,
		@orderID = ORDER_ID,
		@quoteCheckID = QUOTE_ID
	FROM A_QUOTE_ORDER_LINK 
	WHERE ID = @QOL_ID
if @@error <> 0 
	begin
	print 'ERROR -  1234'
	goto PROBLEM
	end

SELECT @supplierName = NAME FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplier

if @quoteCheckID is not null
	begin
	set @msg =  'There is already a quote made for this order'
	print 'There is already a quote made for this order'
	UPDATE A_PRODUCTS_QUICK_PRICE SET QUOTE_ID = @quoteCheckID WHERE ID = @pqpID
	goto acceptQuote
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

SELECT @quoteObjID = OBJECT_ID FROM A_QUOTES_HISTORY WHERE ID = @quoteID

if @@error <> 0 goto PROBLEM
SELECT @ordHistID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @orderID
print 'Made the header now to copy all the items'
Declare @it nvarchar(50), @curs Cursor
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


print 'quote made now approve it'
exec A_SP_OBJECTS_QUICK_APPROVE @quoteObjID,@strNTLogin
print 'Finish the workflow on the Order'
exec A_SP_QUOTES_FINISH_WF @quoteID,@quoteObjID,@custPersonID

if @@error <> 0 goto PROBLEM
print 'Created the quote no problem'

UPDATE A_PRODUCTS_QUICK_PRICE SET QUOTE_ID = @quoteObjID WHERE ID = @pqpID


acceptQuote:
print 'Accepting the Quote'
exec A_SP_QUOTE_ACCEPT_BY_OBJ_ID null,null,@quoteObjID,@custPersonID

print 'delete the tasks if there are any'
declare @qTask varchar(50),@qaTask varchar(50),@qPerID varchar(50)
SELECT @qTask = TASK_ID,
		@qaTask = QA_TASK_ID
	FROM A_QUOTE_ORDER_LINK WHERE ID = @QOL_ID
if @qaTask is not null
	UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @qaTask and STATUS <> 'CLOSED'
if @qTask is not null
	UPDATE A_TASKS SET STATUS = 'CLOSED' WHERE ID = @qTask and STATUS <> 'CLOSED'


fetch next from @pqpCurs into @pqpID
end


goto fin

PROBLEM:
print 'There was a problem'

fin:













