


CREATE    PROCEDURE DBO.A_SP_PROD_PRICE_LIST_SET_UP_CUSTOMER_PARTS
@PPL_ID varchar(50),
@strNTLogin varchar(50)
AS
declare @pID varchar(50),
	@ppID varchar(50),
	@phID varchar(50),
	@appObj varchar(50),
	@appObjType varchar(50),
	@appObjRoot varchar(50),
	@appObjName varchar(50),
	@it nvarchar(50),
	@curs Cursor

print 'We need to check to see if the applicable objects in this product are parts and if they are'
print 'Then we need to add them to the customers parts lists with a reference to this part'
print 'get the ID of this revision of the ppl'
SELECT @ppID = HISTORY_REF_ID FROM A_PROD_PRICE_LIST WHERE ID = @PPL_ID
print 'get the product ID'
SELECT @pID = PRODUCT FROM A_PROD_PRICE_LIST_HISTORY WHERE ID = @ppID
print 'We have a product with the ID of ' + isNULL(@pID,'NULL')
SELECT @phID = HISTORY_REF_ID FROM A_PRODUCTS WHERE ID = @pID
print 'We have a phID of ' + isNull(@phID,'NULL')
SELECT @appObj = APP_OBJECT FROM A_PRODUCTS_HISTORY WHERE ID = @phID
print 'We have an APP_OBJ = ' + @appObj
SELECT top 1 @appObjType = OBJ_TABLE,@appObjRoot = ROOT FROM A_OBJECTS WHERE ROOT = @appObj
print 'We have an object that is ' + isNull(@appObjType,'NULL')
if @appObjType = 'A_PARTS_HISTORY'
	begin
		print '####We need to make a part for all the customers just like this part'
		set @curs = Cursor For SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = @ppID
		open @curs
		Fetch Next from @curs Into @it
		while (@@fetch_status = 0)
			Begin
			print 'Looking at PRod Price List = ' + @it
			exec A_SP_PART_CREATE_A_PART_FOR_CUSTOMER_LIKE_MINE @appObj,null,1,@it,@strNTLogin
			Fetch Next from @curs Into @it
			End
		close @curs
		Deallocate @curs
	end










