

CREATE   PROCEDURE DBO.A_SP_FILLS_SET_UP_FILL_FOR_PURCHASE_ITEM 
@purchItemID varchar(50),
@strNTLogin varchar(50)
AS
BEGIN TRANSACTION
print 'In procedure A_SP_FILLS_SET_UP_FILL_FOR_PURCHASE_ITEM'
declare
@newID varchar(50),
@PROCEDURE_ID varchar(50),
@PROCEDURE_HIST_ID varchar(50),
@SYSTEM_PROCEDURE varchar(50),
@purchItemParent varchar(50),
@productID varchar(50),
@exCostID varchar(50)

exec sp_GetUniqueID3 @newID OUTPUT
SELECT 
	@purchItemParent = PARENT,
	@productID = PRODUCT_ID,
	@exCostID = ADD_COST_ID
	FROM A_ORDER_ITEMS 
	WHERE ID = @purchItemID

if @exCostID is not NULL goto fin

SELECT @PROCEDURE_ID = PROCEDURE_ID	FROM A_V_PRODUCTS_APPROVED_DATA WHERE ID = @productID

SELECT @SYSTEM_PROCEDURE = SYSTEM_ID FROM A_V_PROCEDURES_APPROVED_DATA WHERE ID = @PROCEDURE_ID

print 'We are just going to make a guess at who should fill this'
print 'if the system procedure is null then we are goin gto assume the'
print 'customer should fill this item.'
declare @FILL_BY varchar(50)
if @SYSTEM_PROCEDURE is null 
	begin
		set @FILL_BY = 'CUSTOMER'
		goto jump
	end
if @SYSTEM_PROCEDURE = 'SYS_DNR' 
	begin
		set @FILL_BY = 'CUSTOMER'
		goto jump
	end
if @SYSTEM_PROCEDURE <> 'SYS_SHIPPING' 
	begin
		set @FILL_BY = 'SUPPLIER'
		goto jump
	end

if @purchItemParent is null 
	set @FILL_BY = 'SUPPLIER'
else
	set @FILL_BY = 'PARENT_FILL_ITEM'

jump:


INSERT INTO A_FILLS
([ID], [PURCH_ITEM_ID], [DRCM], [MODBY], [FILL_BY])
VALUES
(@newID,@purchItemID,getDate(),@strNTLogin,@FILL_BY)


if @@ERROR <> 0 goto problem
fin:
if @@trancount > 0 COMMIT TRANSACTION
print 'Finished A_SP_FILLS_SET_UP_FILL_FOR_QUOTE_ITEM with no errors'
return 0

PROBLEM:
if @@trancount > 0 	ROLLBACK TRANSACTION
print ' There was a problem in A_SP_FILLS_SET_UP_FILL_FOR_QUOTE_ITEM and we will terminate and not finish anything '
return 1






