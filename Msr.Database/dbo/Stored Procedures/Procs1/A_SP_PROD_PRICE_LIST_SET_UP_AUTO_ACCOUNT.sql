




CREATE     PROCEDURE A_SP_PROD_PRICE_LIST_SET_UP_AUTO_ACCOUNT
@PROD_ID as varchar(50),
@strNTLogin as varchar(50)
AS
print 'Creating the Main Account for this Product Price'
declare @newID as varchar(50),
		@SUPPLIER_CO as varchar(50),
		@CUSTOMER_CO as varchar(50),
		@PRODUCT as varchar(50),
		@tester as varchar(50),
		@PP_ID as varchar(50)
SELECT
@SUPPLIER_CO = SUPPLIER_ID,
@PRODUCT = PRODUCT,
@PP_ID = HISTORY_REF_ID
FROM A_V_PROD_PRICE_LIST_BY_APPROVED_ID WHERE ID = @PROD_ID

if @PRODUCT is null
	begin
	print 'Could not find this product'	
	goto fin
	end

print 'Now we need to make all the customer accounts'
print 'The Prod Price List ID = ' + @PP_ID
print 'The query to get the customer list is '
print 'SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = ''' + @PP_ID + ''''
Declare @it nvarchar(50)
Declare @curs Cursor
set @curs = Cursor For 
	SELECT CUST_ID FROM A_PROD_PRICE_LIST_REAL_CUSTOMERS WHERE PP_LIST_ID = @PP_ID
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	print 'Adding Customer Account for Customer = ' + @it
	exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @newID OUTPUT,
	@SUPPLIER_CO,@it,NULL,'CUSTOMER_ACCOUNT',@strNTLogin
	exec A_SP_ACCOUNTS_AUTO_CREATE_PRODUCT_ACCOUNT @newID OUTPUT,
	@SUPPLIER_CO,@it,@PRODUCT,'PRODUCT_ACCOUNT',@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs

fin:





