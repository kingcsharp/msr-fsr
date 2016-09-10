




CREATE                     PROCEDURE dbo.A_SP_ORDER_ITEM_UPDATE_ITEM
@newID nvarchar(50) OUTPUT,
@messages nvarchar(2000) OUTPUT,
@objID varchar(50),
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
@strNTLogin varchar(50)
AS
print 'In A_SP_ORDER_ITEM_UPDATE_ITEM'

print 'First lets determine the product info'
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

declare @firstTime varchar(10), @orderID varchar(50),@quoteID varchar(50),@strType varchar(50)

if @ID is null
	begin
	print 'This is a new one'
	set @firstTime = 'true'
	exec sp_GetUniqueID3 @ID OUTPUT
	print 'Got a new ID for this one ' + @ID
	print 'Now to figure out the quoteID or orderID'
	SELECT @quoteID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID and OBJ_TABLE = 'A_QUOTES_HISTORY'
	SELECT @orderID = OBJ_ID FROM A_OBJECTS WHERE ID = @objID and OBJ_TABLE = 'A_ORDERS_HISTORY'
	if @orderID is null and @quoteID is NULL goto problem
	INSERT INTO A_ORDER_ITEMS (ID,ORDER_ID,QUOTE_ID,PRODUCT_ID,DRCM,MODBY)
	VALUES (@ID,@orderID,@quoteID,@PRODUCT,getDate(),@strNTLogin)
	end
else
	begin
	print 'This is not a new one'
	end


UPDATE A_ORDER_ITEMS
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
PARENT = @PARENT_ID
WHERE ID = @ID
set @newID = @ID
print 'If this was a shipping procedure we will need to make the precedents of the parent to follow it'
if @PROC_SYS_ID = 'SYS_SHIPPING'
	begin
		print 'Set the weight to be that of the parent'
		UPDATE A_ORDER_ITEMS
			SET EST_WEIGHT = (SELECT EST_WEIGHT FROM A_ORDER_ITEMS WHERE ID = @PARENT_ID),
				EST_WEIGHT_UNIT = (SELECT EST_WEIGHT_UNIT FROM A_ORDER_ITEMS WHERE ID = @PARENT_ID)
			WHERE ID = @ID
		if @DEST = 'to'
			begin
			print 'Update the precednce of the parent to follow this one'
			DELETE FROM A_ORDER_ITEM_PRECEDENTS WHERE FOL = @PARENT_ID AND PREV = @ID
			INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,FOL,PREV,DRCM,MODBY) VALUES(newID(),@PARENT_ID,@ID,getDate(),@strNTLogin)
		
			end
		if @DEST = 'from'
			begin
			print 'Update the precednce of the child to follow the parent'
			DELETE FROM A_ORDER_ITEM_PRECEDENTS WHERE FOL =  @ID AND PREV = @PARENT_ID
			INSERT INTO A_ORDER_ITEM_PRECEDENTS (ID,PREV,FOL,DRCM,MODBY) VALUES(newID(),@PARENT_ID,@ID,getDate(),@strNTLogin)
			end
	end

declare @myProcID varchar(50),@mySysID varchar(50)
SELECT @myProcID = PROCEDURE_ID  FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @PRODUCT
SELECT @mySysID = SYSTEM_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @myProcID
UPDATE A_ORDER_ITEMS SET BILL_TYPE = 
	(select case
		when @mySysID in ('SYS_PROVIDE_AND_STAY','SYS_PROVIDE_AND_CONSUMED','SYS_SHIPPING') then 'ACT_EST'
		else 'ESTIMATE'
	end)
	WHERE ID = @ID


if @firstTime = 'true'
	begin
	print 'This is the first time this thing was added to this order so we need to add its children'
	exec A_SP_ORDER_ITEM_UPDATE_CHILD_ORDER_ITEMS @ID,@strNTLogin
	end
	


exec A_SP_ORDER_ITEM_SET_PRICE @ID,@strNTLogin
declare @parent as varchar(50)
SELECT @parent = PARENT FROM A_ORDER_ITEMS WHERE ID = @ID
exec A_SP_ORDER_ITEM_SET_PRICE @parent,@strNTLogin

set @newID = @ID

fin:

return 0


problem:
print 'THERE WAS A PROBLEM'






