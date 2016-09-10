



CREATE    PROCEDURE dbo.A_SP_ACCOUNT_INVOICE_ITEM_DEBIT_ALL_INTERNAL_ACCOUNTS
@ID varchar(50),
@strNTLogin varchar(50)
AS
print 'Internally Invoicing order Item # ' + @ID
declare @supplierID varchar(50),@customerID varchar(50),@productID varchar(50),@quoteID varchar(50)

SELECT @productID = PRODUCT_ID,	@quoteID = QUOTE_ID FROM A_ORDER_ITEMS WHERE ID = @ID
SELECT @supplierID = SUPPLIER_ID,@customerID = CUSTOMER_CO FROM A_QUOTES_HISTORY WHERE ID = @quoteID

print 'Searching for all accounts where '
print 'supplierID = ' + isNull(@supplierID,'NULL')
print 'customerID = ' + isNull(@customerID,'NULL')
print 'productID = ' + isNull(@productID,'NULL')

Declare @it nvarchar(50),@curs Cursor
set @curs = Cursor For 
	SELECT ID FROM A_V_ACCOUNTS_APPROVED_DATA 
	WHERE (
		(SUPPLIER_CO = @supplierID AND CUSTOMER_CO = @customerID and PRODUCT_ID =@productID)
	OR
		(SUPPLIER_CO = @supplierID AND CUSTOMER_CO = @customerID and PRODUCT_ID IS NULL)
	OR
		(SUPPLIER_CO = @supplierID AND CUSTOMER_CO is null and PRODUCT_ID = @productID)
	OR
		(SUPPLIER_CO is null AND CUSTOMER_CO = @customerID and PRODUCT_ID = @productID)
	OR
		(SUPPLIER_CO is null AND CUSTOMER_CO = @customerID and PRODUCT_ID is NULL)
	OR
		(SUPPLIER_CO = @supplierID AND CUSTOMER_CO is null and PRODUCT_ID is null)
	)
	AND
	ACCT_TYPE <> 'PURCHASING_ACCOUNT'
open @curs
Fetch Next from @curs Into @it
while (@@fetch_status = 0)
Begin
	exec A_SP_PURCHASE_ITEM_INVOICE_ITEM_TO_ACCOUNT @ID,@it,@strNTLogin
	Fetch Next from @curs Into @it
End
close @curs
Deallocate @curs


fin:










