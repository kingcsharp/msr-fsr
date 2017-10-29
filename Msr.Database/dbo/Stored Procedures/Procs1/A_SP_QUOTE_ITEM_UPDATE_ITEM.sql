CREATE                 PROCEDURE dbo.A_SP_QUOTE_ITEM_UPDATE_ITEM
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@QUOTE_OBJ_ID varchar(50),
@ID varchar(50),
@PROD_OR_LIST varchar(50),
@QTY varchar(50),
@RECURRING nvarchar(50),
@SPECIAL_DISCOUNT varchar(50),
@PROD_PRICE_LIST nvarchar(50),
@SPECIAL_DIS_REASON nvarchar(4000),
@COMMENT nvarchar(4000),
@PARENT_ID varchar(50),
@PROC_SYS_ID varchar(50),
@dest varchar(50),
@FROM_LOC varchar(50),
@TO_LOC varchar(50),
@UNIT_PRICE varchar(50),
@UNIT_ESTIMATE varchar(50),
@TYP_PROD_TIME varchar(50),
@TYP_PROD_TIME_UNIT varchar(50),
@TYP_PROD_CAPACITY varchar(50),
@TYP_PROD_CAPACITY_UNIT varchar(50),
@MAX_QTY_AT_ONE_TIME varchar(50),
@MAX_QTY_EVER varchar(50),
@MIN_QTY varchar(50),
@DEBIT_ACCOUNT_TRIGGER varchar(50),
@CANCELATION_FEE varchar(50),
@strNTLogin varchar(50)
AS
if @QUOTE_OBJ_ID is null or @PROD_OR_LIST is null goto fin

print 'In A_SP_QUOTE_ITEM_UPDATE_ITEM'
declare @PRODUCT as varchar(50)

print 'So, PROD_OR_LIST is either the price list or the product, we need to check that out'
print 'Check to see if it is the Product first'
declare @prodTester as varchar(50)
SELECT @prodTester = ID FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PROD_OR_LIST
if @prodTester is null
	begin
	print 'This is not the product, so it must be the list price'
	SELECT @PRODUCT = PRODUCT FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = @PROD_OR_LIST
	print 'They must have picked a new list price so set the list price = ' + @PROD_OR_LIST
	SET @PROD_PRICE_LIST = @PROD_OR_LIST
	end
else
	begin
	print 'The selected Item is still a product so we need to assume they did not change anything.'
	SET @PRODUCT = @PROD_OR_LIST
	end

print 'Got the Product ID of ' + @PRODUCT
print 'Got the Product List Price ID of  ' + @PROD_PRICE_LIST

print 'Since the quote Object ID could be need to get the real ID'
declare @QUOTE_ID as varchar(50)
SELECT @QUOTE_ID = ID FROM A_QUOTES_HISTORY WHERE OBJECT_ID = @QUOTE_OBJ_ID
print 'Got the real Quote ID of ' + @QUOTE_ID

print 'A_SP_QUOTE_ITEM_UPDATE_ITEM for quote = ' + isNull(@QUOTE_ID,'NULL')
if @ID is null
	begin
	exec sp_GetUniqueID3 @ID OUTPUT
	print 'Got a new ID for this one ' + @ID
	INSERT INTO A_QUOTE_ITEMS (ID,QUOTE_ID,PRODUCT_ID,DRCM,MODBY)
	VALUES (@ID,@QUOTE_ID,@PRODUCT,getDate(),@strNTLogin)
	end
UPDATE A_QUOTE_ITEMS
SET
PRODUCT_ID = @PRODUCT,
DRCM = getDate(),
MODBY = @strNTLogin,
QTY = @QTY,
RECURRING = @RECURRING,
SPECIAL_DISCOUNT = @SPECIAL_DISCOUNT,
SPECIAL_DISC_REASON = @SPECIAL_DIS_REASON,
COMMENTS = @COMMENT,
PROD_PRICE_LIST = @PROD_PRICE_LIST,
PROC_SYS_ID = @PROC_SYS_ID,
DEST = @DEST,
FROM_LOC = @FROM_LOC,
TO_LOC = @TO_LOC,
UNIT_PRICE = @UNIT_PRICE,
UNIT_ESTIMATE = @UNIT_ESTIMATE,
TYP_PROD_TIME = @TYP_PROD_TIME,
TYP_PROD_TIME_UNIT = @TYP_PROD_TIME_UNIT,
TYP_PROD_CAPACITY = @TYP_PROD_CAPACITY,
MAX_QTY_AT_ONE_TIME = @MAX_QTY_AT_ONE_TIME,
MAX_QTY_EVER = @MAX_QTY_EVER,
MIN_QTY = @MIN_QTY,
DEBIT_ACCOUNT_TRIGGER = @DEBIT_ACCOUNT_TRIGGER,
TYP_PROD_CAPACITY_UNIT = @TYP_PROD_CAPACITY_UNIT,
CANCELATION_FEE = @CANCELATION_FEE
WHERE ID = @ID

if @PARENT_ID is not null UPDATE  A_QUOTE_ITEMS SET PARENT = @PARENT_ID WHERE ID = @ID

print 'If this was a shipping procedure we will need to make the precedents of the parent to follow it'
if @PROC_SYS_ID = 'SYS_SHIPPING'
	begin
		if @DEST = 'to'
			begin
			print 'Update the precednce of the parent to follow this one'
			DELETE FROM A_QUOTE_ITEM_PRECEDENTS WHERE FOL = @PARENT_ID AND PREV = @ID
			INSERT INTO A_QUOTE_ITEM_PRECEDENTS (ID,FOL,PREV,DRCM,MODBY) VALUES(newID(),@PARENT_ID,@ID,getDate(),@strNTLogin)
			end
		if @DEST = 'from'
			begin
			print 'Update the precednce of the child to follow the parent'
			DELETE FROM A_QUOTE_ITEM_PRECEDENTS WHERE FOL =  @ID AND PREV = @PARENT_ID
			INSERT INTO A_QUOTE_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY) VALUES(newID(),@PARENT_ID,@ID,getDate(),@strNTLogin)
			end
	end

exec A_SP_QUOTE_ITEM_UPDATE_CHILD_QUOTE_ITEMS @ID,@strNTLogin
exec A_SP_QUOTE_ITEM_SET_PRICE @ID,@strNTLogin
--Update the unit price to the overide price if necessary
declare @parent as varchar(50)
SELECT @parent = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
exec A_SP_QUOTE_ITEM_SET_PRICE @parent,@strNTLogin
set @newID = @ID

fin:





