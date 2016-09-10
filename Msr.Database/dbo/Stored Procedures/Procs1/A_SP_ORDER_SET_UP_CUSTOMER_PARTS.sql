


CREATE    PROCEDURE dbo.A_SP_ORDER_SET_UP_CUSTOMER_PARTS
@ORDER_ID varchar(50),
@strNTLogin varchar(50)
AS

declare 
	@plID varchar(50),
	@plHistID varchar(50),
	@prodID varchar(50),
	@prodHistID varchar(50),
	@appObj varchar(50),
	@appObjType varchar(50),
	@appObjRoot varchar(50),
	@appObjName varchar(50),
	@it nvarchar(50),
	@curs Cursor,
	@ordHistID varchar(50),
	@ordItemID varchar(50),
	@custID varchar(50),
	@supplierID varchar(50),
	@supplierRoot varchar(50),
	@custRoot varchar(50)

SELECT @ordHistID = HISTORY_REF_ID FROM A_ORDERS WHERE ID = @ORDER_ID
SELECT @custID = CUSTOMER_CO FROM A_ORDERS_HISTORY WHERE ID = @ordHistID
SELECT @custRoot = 	ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @custID


set @curs = Cursor For SELECT ID FROM A_ORDER_ITEMS WHERE ORDER_ID = @ordHistID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
	Begin
	print 'Looking at Order Item = ' + @it
	SELECT @plID = PROD_PRICE_LIST FROM A_ORDER_ITEMS WHERE ID = @it
	print 'Looking at price list = ' + isNull(@plID,'NULL')
	print 'We need to check to see if the applicable objects in this product are parts and if they are'
	print 'Then we need to add them to the customers parts lists with a reference to this part'
	print 'get the ID of this revision of the ppl'
	SELECT @plHistID = HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @plID
	print 'get the product ID'
	SELECT @prodID = PRODUCT FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @plHistID
	print 'We have a product with the ID of ' + isNULL(@prodID,'NULL')
	SELECT @prodHistID = HISTORY_REF_ID FROM A_PRODUCTS WHERE ID = @prodID
	print 'We have a prodHistID of ' + isNull(@prodHistID,'NULL')
	SELECT @supplierID = SUPPLIER_ID FROM A_PRODUCTS_HISTORY WHERE ID = @prodHistID
	print 'We have a supplier ID of ' + isNull(@prodHistID,'NULL')
	SELECT @supplierRoot = 	ROOT_CO FROM A_V_COMPANIES_APPROVED_DATA WHERE ID = @supplierID
	if @supplierRoot <> @custRoot
		begin
		SELECT @appObj = APP_OBJECT FROM A_PRODUCTS_HISTORY WHERE ID = @prodHistID
		print 'We have an APP_OBJ = ' + @appObj
		SELECT top 1 @appObjType = OBJ_TABLE,@appObjRoot = ROOT FROM A_OBJECTS WHERE ROOT = @appObj
		print 'We have an object that is ' + isNull(@appObjType,'NULL')
		if @appObjType = 'A_PARTS_HISTORY' and @appObj is not null
			begin
			print '####We need to make a part for the customer just like this part'
			exec A_SP_PART_CREATE_A_PART_FOR_CUSTOMER_LIKE_MINE @appObj,null,1,@custID,@strNTLogin
			end
		end
	Fetch Next from @curs Into @it
	End
close @curs
Deallocate @curs











