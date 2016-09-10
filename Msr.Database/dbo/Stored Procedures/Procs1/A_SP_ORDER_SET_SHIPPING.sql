
CREATE  PROCEDURE dbo.A_SP_ORDER_SET_SHIPPING 
@orderObjID varchar(50),
@shippingPPLID varchar(50),
@supplierID varchar(50),
@appliesTo varchar(50),
@strDest varchar(50),
@strNTLogin varchar(50)
AS
print 'Setting the shipping product for supplier = ' + @supplierID
declare @strOrderID varchar(50)
declare @msg as nvarchar(50), @newID as nvarchar(50)

SELECT @strOrderID = ID FROM A_ORDERS_HISTORY WHERE OBJECT_ID = @orderObjID

declare @curs as Cursor, @ID as varchar(50)
set @curs = CURSOR FOR SELECT i.ID FROM A_ORDER_ITEMS i,A_V_PRODUCTS_APPROVED_DATA prod,
				A_V_PROCEDURES_APPROVED_DATA pr WHERE i.ORDER_ID = @strOrderID
				AND prod.ID = i.PRODUCT_ID 
				AND prod.PROCEDURE_ID = pr.ID
				AND pr.SYSTEM_ID = 'SYS_PROVIDE_AND_STAY'
				AND i.PARENT is null
				AND prod.SUPPLIER_ID = @supplierID

SELECT * FROM A_ORDER_ITEMS i,A_V_PRODUCTS_APPROVED_DATA prod,
				A_V_PROCEDURES_APPROVED_DATA pr WHERE i.ORDER_ID = @strOrderID
				AND prod.ID = i.PRODUCT_ID 
				AND prod.PROCEDURE_ID = pr.ID
				AND pr.SYSTEM_ID = 'SYS_PROVIDE_AND_STAY'
				AND i.PARENT is null
				AND prod.SUPPLIER_ID = @supplierID
declare @currentShipping varchar(50),@update as tinyInt
open @curs
fetch next from @curs into @ID
while @@fetch_status = 0
	begin
	set @update = 0
	print 'Adding shipping to id = ' + @ID
	print 'First find out if it already has shipping'
	set @currentShipping = null
	SELECT @currentShipping = ID FROM A_ORDER_ITEMS WHERE PARENT = @ID AND PROC_SYS_ID = 'SYS_SHIPPING' AND DEST = @strDest
	if @currentShipping is null
		begin
		print 'This one does not have shipping'
		set @update = 1
		end
	else
		begin
		print 'This one has shipping, so if we are doing only blank ons then we need to not update this one.'
		if @appliesTo <> 'BLANK'
			begin
			print 'We are doing blank and others so erase this one and update it'
			exec A_SP_ORDER_ITEM_DELETE @currentShipping, @strNTLogin
			set @update = 1
			end
		end 
	if @update = 1
		begin
		exec A_SP_ORDER_ITEM_UPDATE_ITEM @newID OUTPUT,@msg OUTPUT,
		@orderObjID, --The Value for ORDER_OBJ_ID
		null, --The Value for ID
		@shippingPPLID, --The Value for PRODUCT
		'1', --The Value for QTY
		'0',--The Value for RECURRING
		null,--The Value for SPECIAL_DISCOUNT is
		null, --The Value for PRICE_LIST
		null,--The Value for SPECIALDIS_REASON
		NULL,--The Value for COMMENT
		@ID,--The Value for PARENT_ID
		'SYS_SHIPPING',--The Value for PROC_SYS_ID
		@strDest,--The Value for DEST
		null,--The Value for FROM_LOC
		@strDest,--The Value for TO_LOC
		@strNTLogin	--The value for strNTLogin
		end
	
	fetch next from @curs into @ID
	end
close @curs
deallocate @curs



















